using Xadrez.Tabuleiro;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimeTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for(int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.RetornaPeca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.RetornaPeca(i, j) + " ");
                    }                    
                }
                Console.WriteLine();
            }
        }
    }
}
