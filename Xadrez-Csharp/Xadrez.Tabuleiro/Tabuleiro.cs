namespace Xadrez.Tabuleiro
{
    class Tabuleiro
    {   //Criar um tabuleiro com a qtd de linhas e colunas. Depois, uma nova matriz de Peca no construtor;
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
            //Retorna a peça que está na linha e coluna que foi passada
            return Pecas[linha, coluna]; 
        }

        public Peca RetornaPecaPosicao(Posicao pos)
        {
            //Retorna a peça que está na posição que foi passada
            return Pecas[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            // Coloca a peça (p), numa posição (pos) que foi passada. 
            if(ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos; 
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida"); 
            }
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return RetornaPecaPosicao(pos) != null;  
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (RetornaPecaPosicao(pos) == null)
            {
                return null; 
            }
            Peca atual = RetornaPecaPosicao(pos);
            atual.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null; 
            return atual;
        }
    }
}
