namespace Xadrez.Tabuleiro
{
    class Peca
    {   // Toda peça tem: Uma posição, cor, qtdmovimentos(peão) e está em um tabuleiro;
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null; // Quando a peça é criada ela ainda não tem posição
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }

        public void IncrementarMovimento()
        {
            QtdMovimentos++;
        }
    }
}
