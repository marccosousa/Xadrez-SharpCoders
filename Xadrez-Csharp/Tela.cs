using Xadrez.Jogo;
using Xadrez.Tabuleiro;

namespace XadrezConsole
{
    class Tela
    {
        
        public static void ImprimePartida(PartidaXadrez partida)
        {
            ImprimeTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimePecasCapturadas(partida); 
            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada da peça: " + partida.JogadorAtual);
            if(partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }
        
        public static void ImprimeTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimePeca(tab.RetornaPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimeTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            //Sobrecarga do método para imprimir as movimentações possíveis quando é escolhida a peça
            ConsoleColor atual = Console.BackgroundColor;
            ConsoleColor alterarFundo = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i,j])
                    {
                        Console.BackgroundColor = alterarFundo; 
                    }
                    else
                    {
                        Console.BackgroundColor = atual; 
                    }
                    
                    ImprimePeca(tab.RetornaPeca(i, j));
                    Console.BackgroundColor = atual;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = atual;
        }
        public static void ImprimePecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimeConjunto(partida.AddCapturadas(Cor.BRANCA));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor atual = Console.ForegroundColor; 
            Console.ForegroundColor = ConsoleColor.Red;
            ImprimeConjunto(partida.AddCapturadas(Cor.PRETA));
            Console.ForegroundColor = atual; 
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ImprimePeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.Cor == Cor.BRANCA)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor atual = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = atual;
                }
                Console.Write(" ");
            }
        }


        public static void ImprimeConjunto(HashSet<Peca> conjuntopecas )
        {
            Console.Write("[ ");
            foreach(Peca x in conjuntopecas)
            {
                Console.Write(x + " ");
            }
            Console.Write(" ]");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
