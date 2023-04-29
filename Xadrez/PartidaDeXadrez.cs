using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class PartidaDeXadrez
{
    public TabuleiroC Tab { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Finalizada { get; private set; }

    public PartidaDeXadrez()
    {
        Tab = new TabuleiroC(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branco;
        Finalizada = false;
        colocarPecas();
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetirarPeca(origem);
        p.IncrementarMovimentos();
        Peca pecaCapturada = Tab.RetirarPeca(destino);
        Tab.PosicionarPeca(p, destino);
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

    private void colocarPecas()
    {
        Tab.PosicionarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('c', 1).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('c', 2).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('d', 2).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('e', 2).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('e', 1).toPosicao());
        Tab.PosicionarPeca(new Rei(Cor.Branco, Tab), new PosicaoXadrez('d', 1).toPosicao());

        Tab.PosicionarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('c', 7).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('c', 8).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('d', 7).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('e', 7).toPosicao());
        Tab.PosicionarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('e', 8).toPosicao());
        Tab.PosicionarPeca(new Rei(Cor.Preto, Tab), new PosicaoXadrez('d', 8).toPosicao());
    }

}
