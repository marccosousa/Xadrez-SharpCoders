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
                Console.WriteLine("Aguardando jogada da peça: " + partida.JogadorAtual + " - " + partida.JogadorAtualLogado);
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE-MATE");
                Console.WriteLine("Vitória: " + partida.JogadorAtual + " - " + partida.JogadorAtualLogado);
                if (partida.JogadorAtualLogado == partida.JogadorLogado1)
                {
                    partida.JogadorLogado1.IncrementarVitorias();
                }
                else
                {
                    partida.JogadorLogado2.IncrementarVitorias();
                }
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

        public static void ImprimeLoginOuCadastro(PartidaXadrez partida)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("---------- M E N U  I N I C I A L ---------");
                Console.WriteLine("Se vocês já têm conta e precisam logar - [1]");
                Console.WriteLine("Se vocês precisam se cadastrar - [2]");
                Console.WriteLine("Mostrar jogadores cadastrados - [3]");
                Console.Write("Digite a sua opção: ");
                opcao = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (opcao)
                {

                    case 1:
                        while (!partida.Logado)
                        {
                            try
                            {
                                ImprimeLogin(partida);
                                Console.Clear();
                            }
                            catch (PartidaException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Digite qualquer tecla para tentar novamente.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;
                    case 2:
                        try
                        {
                            ImprimeCadastro(partida);
                            Console.WriteLine("Cadastro realizado.");
                            Console.WriteLine("Digite qualquer tecla para o menu anterior.");
                            Console.ReadKey();
                        }
                        catch (PartidaException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Digite qualquer tecla.");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        ImprimeJogadores(partida);
                        Console.WriteLine("Digite qualquer tecla para o menu anterior.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Número inválido. Digite qualquer tecla para o menu anterior.");
                        Console.ReadKey();
                        break;
                }
            } while (opcao < 1 || opcao > 3 || !partida.Logado);
        }

        public static void ImprimeCadastro(PartidaXadrez partida)
        {
            Console.WriteLine("---------- T E L A DE C A D A S T R O ----------");
            Console.WriteLine();
            Console.WriteLine("Olá! Cadastre seu usuário: !");
            Console.WriteLine();
            Console.Write($"Digite o login que deseja: ");
            string login = Console.ReadLine();
            Console.Write("Digite uma senha: ");
            string senha = Console.ReadLine();
            Console.Write("Digite o seu nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine();
            partida.RealizaCadastro(login, senha, nome);
            Console.WriteLine($"Cadastro realidado com sucesso!");

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
            Console.ReadKey();
            Console.Clear();
            if (partida.JogadorLogado1 != null && partida.JogadorLogado2 != null)
            {
                Console.Write("Bem-vindos! " + partida.JogadorLogado1.Nome + " e ");
                Console.WriteLine(partida.JogadorLogado2.Nome + "!! Espero que se divirtam!");
                Console.WriteLine("Pressione qualquer tecla para ir para o jogo!");
                Console.ReadKey();
            }

        }

        public static void ImprimeJogadores(PartidaXadrez partida)
        {
            Console.WriteLine("Jogadores cadastrados: ");
            foreach (Jogador j in partida.Jogadores)
            {
                Console.WriteLine($"Login: {j.Login} | Nome: {j.Nome} | Vitórias: {j.Vitorias}");
                Console.WriteLine();
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
