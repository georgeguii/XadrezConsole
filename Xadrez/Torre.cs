using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class Torre : Peca
{
    public Torre(Cor cor, TabuleiroC tabuleiro) : base(cor, tabuleiro)
    {
    }

    private bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p == null || p.Cor != Cor; //Cor da propria classe
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);

        //Norte
        pos.DefinirValores(Posicao.Linha -1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if(Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.Linha -= 1;
        }

        //Sul
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.Linha += 1;
        }

        //Leste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.Coluna += 1;
        }

        //Oeste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.Coluna -= 1;
        }

        return mat;
    }

    public override string ToString()
    {
        return "T";
    }
}
