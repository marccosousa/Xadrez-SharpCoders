using Xadrez.Tabuleiro;
namespace Xadrez.Jogo
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool PodeMoverPara(Posicao pos)
        {
            Peca p = Tab.RetornaPecaPosicao(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {   
            // No cavalo não usamos while, pois ele só pode se mover de uma casa p/ outra em L. Não passa pelas outras casas
            bool[,] matMovimentosPossiveis = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //Checar movimentos L esquerda acima
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L esquerda superior
            pos.DefinirValoresPosicao(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L direita superior
            pos.DefinirValoresPosicao(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }
            
            //L direita acima
            pos.DefinirValoresPosicao(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L direita abaixo
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L direita inferior
            pos.DefinirValoresPosicao(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L esquerda inferior
            pos.DefinirValoresPosicao(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            //L esquerda abaixo
            pos.DefinirValoresPosicao(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos) && PodeMoverPara(pos))
            {
                matMovimentosPossiveis[pos.Linha, pos.Coluna] = true;
            }

            return matMovimentosPossiveis;
        }
    }
}
