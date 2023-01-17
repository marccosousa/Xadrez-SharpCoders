using Xadrez.Tabuleiro;
namespace Xadrez.Jogo
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "P";
        }


        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.RetornaPecaPosicao(pos);
            return p != null && p.Cor != Cor;
        }

        private bool PosicaoLivre(Posicao pos)
        {
            return Tab.RetornaPecaPosicao(pos) == null; 
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matMovimentosPossiveis = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.BRANCA) // Como ele só anda pra um sentido, tive que diferenciar o movimento com a cor.
            {
                pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PosicaoLivre(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha - 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PosicaoLivre(pos) && QtdMovimentos == 0) // Pode andar 2x quando a qtd for zero
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna - 1); // Peão só captura peça na diagonal, pos isso a mudança do método.
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {
                pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PosicaoLivre(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PosicaoLivre(pos) && QtdMovimentos == 0)
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                }
            }
            return matMovimentosPossiveis;
        }
    }
}

