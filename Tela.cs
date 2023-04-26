using XadrezConsole.Tabuleiro;

namespace XadrezConsole;

public class Tela
{
    public static void ImprimirTabuleiro(TabuleiroC tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            for (int j = 0; j < tab.Colunas; j++)
            {
                if (tab.Peca(i, j) is null)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write($"{tab.Peca(i, j)} ");
                }
            }
            Console.WriteLine();
        }
    }
}
