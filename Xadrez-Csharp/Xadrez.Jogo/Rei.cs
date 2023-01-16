using Xadrez.Tabuleiro; 

namespace Xadrez.Jogo
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMoverPara(Posicao pos)
        {
            Peca p = Tab.RetornaPecaPosicao(pos);
            return p == null || p.Cor != Cor; 
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matMovimentosPossiveis = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0,0);

            //Checar movimentos possíveis acima:
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna);
            if(Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;   
            }

            //diagonal superior direita :
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }


            //direita :
            pos.DefinirValoresPosicao(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //diagonal inferior direita:
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //abaixo:
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //diagonal inferior esquerda:
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //esquerda:
            pos.DefinirValoresPosicao(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }
            
            //diagonal superior esquerda:
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            return matMovimentosPossiveis;
        }
    }
}
