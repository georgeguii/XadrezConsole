using XadrezConsole;
using XadrezConsole.Xadrez;
using XadrezConsole.Tabuleiro;

try
{
    TabuleiroC tab = new TabuleiroC(8, 8);

    tab.PosicionarPeca(new Torre(Cor.Preto, tab), new Posicao(0, 1));
    tab.PosicionarPeca(new Torre(Cor.Preto, tab), new Posicao(1, 3));
    tab.PosicionarPeca(new Rei(Cor.Preto, tab), new Posicao(0, 2));


    tab.PosicionarPeca(new Rei(Cor.Branco, tab), new Posicao(3, 5));

    Tela.ImprimirTabuleiro(tab);

}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}