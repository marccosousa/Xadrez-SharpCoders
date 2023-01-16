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

        public override bool[,] MovimentosPossiveis()
        {
            //throw new NotImplementedException();
        }
    }
}
