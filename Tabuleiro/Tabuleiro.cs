namespace XadrezConsole.Tabuleiro;

public class TabuleiroC
{
    public int Linhas { get; set; }

    public int Colunas { get; set; }

    private Peca[,] Pecas;

    public TabuleiroC(int linhas, int colunas)
    {
        Linhas = linhas;
        Colunas = colunas;
        Pecas = new Peca[Linhas, Colunas];
    }

    public Peca Peca(int linha, int coluna)
        => Pecas[linha, coluna];

    public Peca Peca(Posicao pos)
        => Pecas[pos.Linha, pos.Coluna];

    public bool ExistePecaPosicao(Posicao pos)
    {
        ValidarPosicao(pos);
        return Peca(pos) != null;
    }

    public void PosicionarPeca(Peca peca, Posicao posicao)
    {
        if (ExistePecaPosicao(posicao))
            throw new TabuleiroException("Já existe uma peça nessa posição!");
        Pecas[posicao.Linha, posicao.Coluna] = peca;
        peca.Posicao = posicao;
    }

    public bool PosicaoValida(Posicao pos)
    {
        if (pos.Linha < 0 || pos.Linha > 7 || pos.Coluna < 0 || pos.Coluna > 7)
        {
            return false;
        }
        return true;
    }

    public void ValidarPosicao(Posicao pos)
    {
        if(!PosicaoValida(pos))
        {
            throw new TabuleiroException("Posição Inválida");
        }
    }
}
