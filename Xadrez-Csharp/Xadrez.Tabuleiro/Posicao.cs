namespace Xadrez.Tabuleiro
{
    class Posicao // Essa classe vai dizer em qual LINHA e COLUNA uma peça está.
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValoresPosicao(int linha, int coluna)
        {
            //Método para auxiliar na hora de mostrar os movimentos possíveis
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"{Linha}, {Coluna}";
        }
    }
}
