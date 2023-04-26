using XadrezConsole;
using XadrezConsole.Xadrez;
using XadrezConsole.Tabuleiro;

try
{
    TabuleiroC tab = new TabuleiroC(8, 8);

    tab.PosicionarPeca(new Torre(Cor.Preto, tab), new Posicao(0, 0));
    tab.PosicionarPeca(new Torre(Cor.Preto, tab), new Posicao(1, 3));
    tab.PosicionarPeca(new Rei(Cor.Preto, tab), new Posicao(2, 4));

    Tela.ImprimirTabuleiro(tab);

}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}