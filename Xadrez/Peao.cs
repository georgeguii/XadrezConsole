using System.Runtime.ConstrainedExecution;
using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;
public class Peao : Peca
{
    private PartidaDeXadrez partida;
    public Peao(Cor Cor, TabuleiroC tabuleiro, PartidaDeXadrez partida) : base(Cor, tabuleiro)
    {
        this.partida = partida;
    }

    public override string ToString()
    {
        return "P";
    }

    private bool ExisteInimigo(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p != null && p.Cor != Cor;
    }

    private bool Livre(Posicao pos)
    {
        return Tabuleiro.Peca(pos) == null;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);

        if (Cor == Cor.Branco)
        {
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && Livre(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
            Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(p2) && Livre(p2) && Tabuleiro.PosicaoValida(pos) && Livre(pos) && qteMovimentos == 0)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // JOGADA ESPECIAL EN PASSANT
            if (Posicao.Linha == 3)
            {
                Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(esquerda) &&
                    ExisteInimigo(esquerda) &&
                    Tabuleiro.Peca(esquerda) == partida.VuneravelEnPassant
                    )
                {
                    mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                }

                Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(direita) &&
                    ExisteInimigo(direita) &&
                    Tabuleiro.Peca(direita) == partida.VuneravelEnPassant
                    )
                {
                    mat[direita.Linha - 1, direita.Coluna] = true;
                }
            }
        }
        else
        {
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && Livre(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
            Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(p2) && Livre(p2) && Tabuleiro.PosicaoValida(pos) && Livre(pos) && qteMovimentos == 0)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // JOGADA ESPECIAL EN PASSANT
            if (Posicao.Linha == 4)
            {
                Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(esquerda) &&
                    ExisteInimigo(esquerda) &&
                    Tabuleiro.Peca(esquerda) == partida.VuneravelEnPassant
                    )
                {
                    mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                }

                Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(direita) &&
                    ExisteInimigo(direita) &&
                    Tabuleiro.Peca(direita) == partida.VuneravelEnPassant
                    )
                {
                    mat[direita.Linha + 1, direita.Coluna] = true;
                }
            }
        }

        return mat;
    }
}
