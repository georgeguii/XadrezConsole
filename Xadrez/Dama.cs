using System.Runtime.ConstrainedExecution;
using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class Dama : Peca
{
    public Dama(Cor Cor, TabuleiroC tabuleiro) : base(Cor, tabuleiro)
    {
    }

    public override string ToString()
    {
        return "D";
    }

    private bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p == null || p.Cor != Cor;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);

        // Oeste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha, pos.Coluna - 1);
        }

        // Leste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha, pos.Coluna + 1);
        }

        // Norte
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha - 1, pos.Coluna);
        }

        // Sul
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha + 1, pos.Coluna);
        }

        // NO
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
        }

        // NE
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
        }

        // SE
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
        }

        // SO
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
        }

        return mat;
    }
}
