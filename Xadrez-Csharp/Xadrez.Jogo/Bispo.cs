using Xadrez.Tabuleiro;
namespace Xadrez.Jogo
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool PodeMoverPara(Posicao pos)
        {
            Peca p = Tab.RetornaPecaPosicao(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matMovimentosPossiveis = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //Checar movimentos diagonal superior esquerda
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna - 1); 
            while(Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true; 
                if(Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break; 
                }
                
                pos.DefinirValoresPosicao(pos.Linha - 1, pos.Coluna - 1); 
            }

            //Checar movimentos diagonal superior direita
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirValoresPosicao(pos.Linha - 1, pos.Coluna + 1);
            }

            //Checar movimentos diagonal inferior direita
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirValoresPosicao(pos.Linha + 1, pos.Coluna + 1);
            }

            //Checar movimentos diagonal inferior esquerda
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirValoresPosicao(pos.Linha + 1, pos.Coluna - 1);
            }

            return matMovimentosPossiveis;
        }
    }
}
