using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class PosicaoXadrez
{
    public int Linha { get; set; }

    public char Coluna { get; set; }

    public PosicaoXadrez(char coluna, int linha)
    {
        Coluna = coluna;
        Linha = linha;
    }

    public Posicao toPosicao()
    {
        return new Posicao(8 - Linha, Coluna - 'a');
    }

    public override string ToString()
    {
        return $"{Coluna}{Linha}";
    }
}
