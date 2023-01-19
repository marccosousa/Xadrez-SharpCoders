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
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada da peça: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE-MATE");
                Console.WriteLine("Vitória: " + partida.JogadorAtual);
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
                    if (posicoesPossiveis[i, j])
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

        public static void ImprimeCadastro(PartidaXadrez partida)
        {
            Console.WriteLine("---------- T E L A DE C A D A S T R O ----------");
            Console.WriteLine();
            Console.WriteLine("Olá! você precisa cadastrar 2 jogadores para começar!");
            Console.WriteLine();
            for (int i = 1; i <= 2; i++)
            {
                Console.Write($"Digite o login que deseja: ");
                string login = Console.ReadLine();
                Console.Write("Digite uma senha: ");
                string senha = Console.ReadLine();
                Console.Write("Digite o seu nome: ");
                string nome = Console.ReadLine();
                Console.WriteLine();
                partida.RealizaCadastro(login, senha, nome);
                Console.WriteLine($"{i}a cadastro realidado com sucesso!");
                if(i == 1)
                {
                    Console.WriteLine("Aperte qualquer tecla para o próximo cadastro.");
                    Console.ReadKey();
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Cadastros feitos. Aperte qualquer tecla para ir até a tela de login.");
            Console.ReadKey();
        }

        public static void ImprimeLogin(PartidaXadrez partida)
        {
            Console.WriteLine("---------- T E L A DE L O G I N ----------");
            Console.WriteLine();
            Console.Write($"Digite o login: ");
            string login = Console.ReadLine();
            Console.Write("Digite a sua senha: ");
            string senha = Console.ReadLine();
            bool usuarioLogado = partida.RealizaLogin(login, senha);
            if (!usuarioLogado)
            {
                throw new PartidaException("Usuário ou senha inválidos.");
            }
            Console.WriteLine("Login efetuado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para a próxima tela"); 
            Console.ReadKey();
            if(partida.JogadorLogado1 != null && partida.JogadorLogado2 != null)
            {
                Console.Write("Bem-vindos! " + partida.JogadorLogado1.Nome + " e ");
                Console.WriteLine(partida.JogadorLogado2.Nome + "!! Espero que se divirtam!");
                Console.WriteLine("Pressione qualquer tecla para ir para o jogo!");
                Console.ReadKey();
            }
                       
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


        public static void ImprimeConjunto(HashSet<Peca> conjuntopecas)
        {
            Console.Write("[ ");
            foreach (Peca x in conjuntopecas)
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
