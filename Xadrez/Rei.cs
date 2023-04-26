using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class Rei : Peca
{
    public Rei(Cor cor, TabuleiroC tabuleiro) : base(cor, tabuleiro)
    {
    }

    public override string ToString()
    {
        return "R";
    }
}
