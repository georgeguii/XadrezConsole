using XadrezConsole.Tabuleiro;

namespace XadrezConsole.Xadrez;

public class Rei : Peca
{
    private PartidaDeXadrez partida;

    public Rei(Cor cor, TabuleiroC tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
    {
        this.partida = partida;
    }

    private bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p == null || p.Cor != Cor; //Cor da propria classe
    }

    private bool TesteTorreParaRoque(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p != null &&
            p is Torre &&
            p.Cor == Cor &&
            p.qteMovimentos == 0;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);

        //Norte
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Nordeste
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Leste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Sudeste
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Sul
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //Sudoeste
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna -1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Oeste
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //Noroeste
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        // JOGADA ESPECIAL ROQUE
        if (qteMovimentos == 0 && !partida.Xeque)
        {
            //ROQUE PEQUENO
            Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            if(TesteTorreParaRoque(posT1))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                if (Tabuleiro.Peca(p1) is null &&
                    Tabuleiro.Peca(p2) is null)
                {
                    mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
            }
            //ROQUE GRANDE
            Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            if (TesteTorreParaRoque(posT2))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                if (Tabuleiro.Peca(p1) is null &&
                    Tabuleiro.Peca(p2) is null &&
                    Tabuleiro.Peca(p3) is null)
                {
                    mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }
        }

        return mat;
    }

    public override string ToString()
    {
        return "R";
    }
}
