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
                PartidaXadrez partida = new PartidaXadrez();
                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimePartida(partida);
                        Console.WriteLine();
                        Console.Write("Posição da peça de origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().PosicaoXadrezParaMatriz();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.RetornaPecaPosicao(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimeTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Posição de destino da peça: ");
                        Posicao destino = Tela.LerPosicaoXadrez().PosicaoXadrezParaMatriz();
                        partida.ValidarPosicaoDestino(origem, destino); 
                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimePartida(partida);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
