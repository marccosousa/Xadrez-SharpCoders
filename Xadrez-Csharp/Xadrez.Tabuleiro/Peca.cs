namespace Xadrez.Tabuleiro
{
    class Peca
    {   // Toda peça tem: Uma posição, cor, qtdmovimentos(peão) e está em um tabuleiro;
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
        {
            Posicao = posicao;
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }
    }
}
