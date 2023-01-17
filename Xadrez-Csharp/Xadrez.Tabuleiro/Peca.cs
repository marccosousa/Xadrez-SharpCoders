namespace Xadrez.Tabuleiro
{
    abstract class Peca
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
        public bool ExisteMovimentosPossiveis()
        {
            //Vai retornar true se existir pelo menos um movimento possível;
            bool[,] matExisteMovimentosPossiveis = MovimentosPossiveis();
            for(int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (matExisteMovimentosPossiveis[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false; 
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna]; 
        }

        public abstract bool[,] MovimentosPossiveis();
        // Fazendo um método abstrato para aproveitar o método nas subclasses.
        // O método faz o polimorfismo em tempo de execução
        
    }
}
