using Xadrez.Tabuleiro; 
namespace Xadrez.Jogo
{
    class PartidaXadrez
    {
        public Tabuleiro.Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro.Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.BRANCA;
            Terminada = false; 
            ColocarPecas(); 
        }

        public void RealizaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino); // Ainda não estou usando para nada. Vai servir para mostrar no console as peças capturadas
            Tab.ColocarPeca(p, destino); 
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            RealizaMovimento(origem, destino);
            Turno++;
            MudaJogador(); 

        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.BRANCA)
            {
                JogadorAtual = Cor.PRETA; 
            }
            else
            {
                JogadorAtual = Cor.BRANCA;
            }
        }

        private void ColocarPecas()
        {
            // Método auxiliar para colocar as peças iniciais no programa...
            Tab.ColocarPeca(new Torre(Cor.BRANCA, Tab), new PosicaoXadrez('c', 1).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.BRANCA, Tab), new PosicaoXadrez('c', 2).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.BRANCA, Tab), new PosicaoXadrez('d', 2).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.BRANCA, Tab), new PosicaoXadrez('e', 2).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.BRANCA, Tab), new PosicaoXadrez('e', 1).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Rei(Cor.BRANCA, Tab), new PosicaoXadrez('d', 1).PosicaoXadrezParaMatriz());

            Tab.ColocarPeca(new Torre(Cor.PRETA, Tab), new PosicaoXadrez('c', 7).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.PRETA, Tab), new PosicaoXadrez('c', 8).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.PRETA, Tab), new PosicaoXadrez('d', 7).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.PRETA, Tab), new PosicaoXadrez('e', 7).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Torre(Cor.PRETA, Tab), new PosicaoXadrez('e', 8).PosicaoXadrezParaMatriz());
            Tab.ColocarPeca(new Rei(Cor.PRETA, Tab), new PosicaoXadrez('d', 8).PosicaoXadrezParaMatriz());
        }
    }
}
