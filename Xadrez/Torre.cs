using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class Torre : Peca
{
    public Torre(Cor cor, TabuleiroC tabuleiro) : base(cor, tabuleiro)
    {
    }

    public override string ToString()
    {
        return "T";
    }
}
