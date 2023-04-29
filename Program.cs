using XadrezConsole;
using XadrezConsole.Xadrez;
using XadrezConsole.Tabuleiro;

try
{
    PartidaDeXadrez partida = new PartidaDeXadrez();

    while (!partida.Finalizada)
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab);

        Console.WriteLine();

        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

        partida.ExecutaMovimento(origem, destino);
    }


}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}