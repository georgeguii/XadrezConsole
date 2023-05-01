namespace XadrezConsole.Tabuleiro;

public abstract class Peca
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

    public void DecrementarMovimentos()
    {
        qteMovimentos--;
    }

    public bool ExistemMovimentosPossiveis()
    {
        bool[,] mat = MovimentosPossiveis();

        for (int i = 0; i < Tabuleiro.Linhas; i++)
        {
            for (int j = 0; j < Tabuleiro.Colunas; j++)
            {
                if (mat[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool PodeMoverPara(Posicao pos)
    {
        return MovimentosPossiveis()[pos.Linha, pos.Coluna];
    }

    public abstract bool[,] MovimentosPossiveis();
}
