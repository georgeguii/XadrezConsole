using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class PartidaDeXadrez
{
    public TabuleiroC Tab { get; private set; }
    private int turno;
    private Cor jogadorAtual;
    public bool Finalizada { get; private set; }

    public PartidaDeXadrez()
    {
        Tab = new TabuleiroC(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
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
