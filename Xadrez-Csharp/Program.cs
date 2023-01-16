using System.Linq.Expressions;
using Xadrez.Jogo;
using Xadrez.Tabuleiro; 

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('c', 7);
            Console.WriteLine(pos.PosicaoXadrezParaMatriz());
        }
    }
}
