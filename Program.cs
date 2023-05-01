using XadrezConsole;
using XadrezConsole.Xadrez;
using XadrezConsole.Tabuleiro;

try
{
    PartidaDeXadrez partida = new PartidaDeXadrez();

    while (!partida.Finalizada)
    {
        try
        {
            Console.Clear();
            Tela.ImprimirPartida(partida);

            Console.WriteLine();
            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
            partida.ValidarPosicaoOrigem(origem);

            bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();

            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

            Console.WriteLine();
            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
            partida.ValidarPosicaoDestino(origem, destino);

            partida.RealizaJogada(origem, destino);
        }
        catch (TabuleiroException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
    Console.Clear();
    Tela.ImprimirPartida(partida);

}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}