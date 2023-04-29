namespace XadrezConsole.Tabuleiro;

public class Peca
{
    public Posicao Posicao { get; set; }

    public Cor Cor { get; protected set; }

    public int qteMovimentos { get; protected set; }

    public TabuleiroC Tabuleiro { get; protected set; }

    public Peca(Cor cor, TabuleiroC tabuleiro)
    {
        Cor = cor;
        Posicao = null;
        qteMovimentos = 0;
        Tabuleiro = tabuleiro;
    }

    public void IncrementarMovimentos()
    {
        qteMovimentos++;
    }
}
