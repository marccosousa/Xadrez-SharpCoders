namespace Xadrez.Tabuleiro
{
    class Tabuleiro
    {   //Instanciar um tabuleiro com a qtd de linhas e colunas. Depois, uma nova matriz de Peca;
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas { get; private set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca RetornaPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna]; 
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos; 
        }
    }
}
