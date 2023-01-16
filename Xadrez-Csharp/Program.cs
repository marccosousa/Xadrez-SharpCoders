using System.Linq.Expressions;
using Xadrez.Jogo;
using Xadrez.Tabuleiro; 

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Torre(Cor.PRETA, tab), new Posicao(2, 4));
                tab.ColocarPeca(new Torre(Cor.PRETA, tab), new Posicao(2, 5));
                tab.ColocarPeca(new Torre(Cor.PRETA, tab), new Posicao(2, 6));
                tab.ColocarPeca(new Rei(Cor.PRETA, tab), new Posicao(2, 3));
                tab.ColocarPeca(new Torre(Cor.PRETA, tab), new Posicao(9, 9));
                Tela.ImprimeTabuleiro(tab);
            } 
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
