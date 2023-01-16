using Xadrez.Tabuleiro;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(2, 3);
            Console.WriteLine(p);

            Tabuleiro tab = new Tabuleiro(8, 8);
            Tela.ImprimeTabuleiro(tab);
        }
    }
}
