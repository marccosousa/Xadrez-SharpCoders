using Xadrez.Tabuleiro;

namespace Xadrez.Jogo
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "T";
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

            //Checar movimentos possíveis acima
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna); 
            while(Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if(Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break; 
                }
                pos.Linha = pos.Linha - 1; 
            }

            //Abaixo
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            //Direita
            pos.DefinirValoresPosicao(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            //Esquerda
            pos.DefinirValoresPosicao(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
                if (Tab.RetornaPecaPosicao(pos) != null && Tab.RetornaPecaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }

            return matMovimentosPossiveis;
        }
    }
}
