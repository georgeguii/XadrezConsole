using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class PartidaDeXadrez
{
    public TabuleiroC Tab { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Finalizada { get; private set; }

    private HashSet<Peca> pecas;
    private HashSet<Peca> capturadas;

    public PartidaDeXadrez()
    {
        Tab = new TabuleiroC(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branco;
        Finalizada = false;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        colocarPecas();
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tab.RetirarPeca(destino);
        Tab.PosicionarPeca(p, destino);
        if (pecaCapturada is not null)
        {
            capturadas.Add(pecaCapturada);
        }
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        ExecutaMovimento(origem, destino);
        Turno++;
        MudaJogador();
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
        if (!Tab.Peca(origem).PodeMoverPara(destino))
        {
            throw new TabuleiroException("POsição de destino inválida!");
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

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tab.PosicionarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        pecas.Add(peca);
    }

    private void colocarPecas()
    {
        ColocarNovaPeca('c', 1, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('c', 2, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('d', 2, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('e', 2, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('e', 1, new Torre(Cor.Branco, Tab));
        ColocarNovaPeca('d', 1, new Torre(Cor.Branco, Tab));

        ColocarNovaPeca('c', 7, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('c', 8, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('d', 7, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('e', 7, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('e', 8, new Torre(Cor.Preto, Tab));
        ColocarNovaPeca('d', 8, new Torre(Cor.Preto, Tab));
    }

}
