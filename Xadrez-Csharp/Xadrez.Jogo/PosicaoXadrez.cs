using Xadrez.Tabuleiro; 
namespace Xadrez.Jogo
{
    class PosicaoXadrez
    {
        //Essa classe tem o método que faz a conversão da posição de xadrez para uma posição que é aceita na matriz.
        public char Coluna { get; set; }
        public int Linha {get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao PosicaoXadrezParaMatriz()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha; 
        }
    }
}
