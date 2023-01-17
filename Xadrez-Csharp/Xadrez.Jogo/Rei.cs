using Xadrez.Tabuleiro; 

namespace Xadrez.Jogo
{
    class Rei : Peca
    {
        private PartidaXadrez Partida { get; set; } // Associando rei com a partida de xadrez e recebendo a partida como argumento

        public Rei(Cor cor, Tabuleiro.Tabuleiro tab, PartidaXadrez partida) : base(cor, tab)
        {
            Partida = partida;
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

        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.RetornaPecaPosicao(pos); 
            return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0; 
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

            // ### JOGADAS ESPECIAIS ###

            // Roque pequeno
            if(QtdMovimentos == 0 && !Partida.Xeque)
            {
                Posicao posicaoDaTorre = new Posicao(Posicao.Linha, Posicao.Coluna + 3); 
                if(TesteTorreParaRoque(posicaoDaTorre))
                {
                    Posicao vaga1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao vaga2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if(Tab.RetornaPecaPosicao(vaga1) == null && Tab.RetornaPecaPosicao(vaga2) == null)
                    {
                        matMovimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true; 
                    }
                }
            }


            return matMovimentosPossiveis;
        }
    }
}
