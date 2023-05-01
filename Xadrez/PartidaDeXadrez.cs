using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class PartidaDeXadrez
{
    public TabuleiroC Tab { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Finalizada { get; private set; }
    public bool Xeque { get; private set; }
    public Peca VuneravelEnPassant { get; private set; }

    private HashSet<Peca> pecas;
    private HashSet<Peca> capturadas;

    public PartidaDeXadrez()
    {
        Tab = new TabuleiroC(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branco;
        Finalizada = false;
        VuneravelEnPassant = null;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        ColocarPecas();
    }

    public Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tab.RetirarPeca(destino);
        Tab.PosicionarPeca(p, destino);
        if (pecaCapturada is not null)
        {
            capturadas.Add(pecaCapturada);
        }

        //JOGADA ESPECIAL ROQUE PEQUENO
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = Tab.RetirarPeca(origemT);
            T.IncrementarMovimentos();
            Tab.PosicionarPeca(T, destinoT);
        }

        //JOGADA ESPECIAL ROQUE GRANDE
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = Tab.RetirarPeca(origemT);
            T.IncrementarMovimentos();
            Tab.PosicionarPeca(T, destinoT);
        }

        //JOGADA ESPECIAL EN PASSANT
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna &&
                pecaCapturada is null)
            {
                Posicao posP;
                if (p.Cor == Cor.Branco)
                {
                    posP = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                pecaCapturada = Tab.RetirarPeca(posP);
                capturadas.Add(pecaCapturada);
            }
        }


        return pecaCapturada;
    }

    public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = Tab.RetirarPeca(destino);
        p.DecrementarMovimentos();
        if (pecaCapturada is not null)
        {
            Tab.PosicionarPeca(pecaCapturada, destino);
            capturadas.Remove(pecaCapturada);
        }
        Tab.PosicionarPeca(p, origem);

        //JOGADA ESPECIAL ROQUE PEQUENO
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = Tab.RetirarPeca(destinoT);
            T.DecrementarMovimentos();
            Tab.PosicionarPeca(T, origemT);
        }

        //JOGADA ESPECIAL ROQUE GRANDE
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = Tab.RetirarPeca(destinoT);
            T.DecrementarMovimentos();
            Tab.PosicionarPeca(T, origemT);
        }

        //JOGADA ESPECIAL EN PASSANT
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna &&
                pecaCapturada == VuneravelEnPassant)
            {
                Peca peao = Tab.RetirarPeca(destino);
                Posicao posP;
                if (p.Cor == Cor.Branco)
                {
                    posP = new Posicao(3, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(4, destino.Coluna);
                }
                Tab.PosicionarPeca(peao, posP);
            }
        }
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        Peca pecaCapturada = ExecutaMovimento(origem, destino);

        if (ReiEstaEmXeque(JogadorAtual))
        {
            DesfazerMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em Xeque!");
        }

        Peca p = Tab.Peca(destino);

        // JOGADA ESPECIAL PROMOCAO
        if (p is Peao)
        {
            if ((p.Cor == Cor.Branco && destino.Linha == 0) ||
                (p.Cor == Cor.Preto && destino.Linha == 7))
            {
                p = Tab.RetirarPeca(destino);
                pecas.Remove(p);
                Peca dama = new Dama(p.Cor, Tab);
                Tab.PosicionarPeca(dama, destino);
                pecas.Add(dama);
            }
        }

        if (ReiEstaEmXeque(Adversaria(JogadorAtual)))
            Xeque = true;
        else
            Xeque = false;

        if (TesteXequeMate(Adversaria(JogadorAtual)))
            Finalizada = true;
        else
        {
            Turno++;
            MudaJogador();
        }

        // JOGADA ESPECIAL ENPASSANT
        if (p is Peao &&
            (destino.Linha == origem.Linha - 2 ||
            destino.Linha == origem.Linha + 2))
        {
            VuneravelEnPassant = p;
        } else
        {
            VuneravelEnPassant = null;
        }
    }

    public void ValidarPosicaoOrigem(Posicao pos)
    {
        if (Tab.Peca(pos) == null)
        {
            throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        }

        if (JogadorAtual != Tab.Peca(pos).Cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");
        }

        if (!Tab.Peca(pos).ExistemMovimentosPossiveis())
        {
            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
    }

    public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
    {
        if (!Tab.Peca(origem).MovimentoPossivel(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }

    public void MudaJogador()
    {
        if (JogadorAtual == Cor.Branco)
        {
            JogadorAtual = Cor.Preto;
        }
        else
        {
            JogadorAtual = Cor.Branco;
        }
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        //HashSet<Peca> aux = new();
        //foreach (Peca x in capturadas)
        //{
        //    if (x.Cor == cor)
        //    {
        //        aux.Add(x);
        //    }
        //}
        HashSet<Peca> aux = capturadas.Where(x => x.Cor == cor).ToHashSet();
        return aux;
    }

    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
        //HashSet<Peca> aux = new();
        //foreach (Peca x in pecas)
        //{
        //    if (x.Cor == cor)
        //    {
        //        aux.Add(x);
        //    }
        //}
        HashSet<Peca> aux = pecas.Where(x => x.Cor == cor).ToHashSet();
        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }

    private Cor Adversaria(Cor cor)
    {
        if (cor == Cor.Branco)
            return Cor.Preto;
        else
            return Cor.Branco;
    }

    private Peca Rei(Cor cor)
    {
        foreach (Peca p in PecasEmJogo(cor))
        {
            if (p is Rei)
                return p;
        }
        return null;
    }

    public bool ReiEstaEmXeque(Cor cor)
    {
        Peca r = Rei(cor);
        if (r is null)
            throw new TabuleiroException($"Não existe rei da cor {cor} no tabuleiro!");

        foreach (Peca x in PecasEmJogo(Adversaria(cor)))
        {
            bool[,] mat = x.MovimentosPossiveis();
            if (mat[r.Posicao.Linha, r.Posicao.Coluna])
            {
                return true;
            }
        }
        return false;
    }

    public bool TesteXequeMate(Cor cor)
    {
        if (!ReiEstaEmXeque(cor))
            return false;

        foreach (Peca p in PecasEmJogo(cor))
        {
            bool[,] mat = p.MovimentosPossiveis();
            for (var i = 0; i < Tab.Linhas; i++)
            {
                for (var j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        Posicao origem = p.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = ExecutaMovimento(origem, destino);
                        bool testeXeque = ReiEstaEmXeque(cor);
                        DesfazerMovimento(origem, destino, pecaCapturada);
                        if (!testeXeque)
                            return false;
                    }
                }
            }
        }
        return true;
    }

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tab.PosicionarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        pecas.Add(peca);
    }

    private void ColocarPecas()
    {
        ColocarNovaPeca('a', 1, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('b', 1, new Cavalo(Cor.Branco, Tab));
        ColocarNovaPeca('c', 1, new Bispo(Cor.Branco, Tab));
        ColocarNovaPeca('d', 1, new Dama(Cor.Branco, Tab));
        ColocarNovaPeca('e', 1, new Rei(Cor.Branco, Tab, this));
        ColocarNovaPeca('f', 1, new Bispo(Cor.Branco, Tab));
        ColocarNovaPeca('g', 1, new Cavalo(Cor.Branco, Tab));
        ColocarNovaPeca('h', 1, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('a', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('b', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('c', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('d', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('e', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('f', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('g', 2, new Peao(Cor.Branco, Tab, this));
        ColocarNovaPeca('h', 2, new Peao(Cor.Branco, Tab, this));

        ColocarNovaPeca('a', 8, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('b', 8, new Cavalo(Cor.Preto, Tab));
        ColocarNovaPeca('c', 8, new Bispo(Cor.Preto, Tab));
        ColocarNovaPeca('d', 8, new Dama(Cor.Preto, Tab));
        ColocarNovaPeca('e', 8, new Rei(Cor.Preto, Tab, this));
        ColocarNovaPeca('f', 8, new Bispo(Cor.Preto, Tab));
        ColocarNovaPeca('g', 8, new Cavalo(Cor.Preto, Tab));
        ColocarNovaPeca('h', 8, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('a', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('b', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('c', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('d', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('e', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('f', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('g', 7, new Peao(Cor.Preto, Tab, this));
        ColocarNovaPeca('h', 7, new Peao(Cor.Preto, Tab, this));
    }

}
