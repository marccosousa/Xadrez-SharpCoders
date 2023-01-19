using System.IO;
using Xadrez.Tabuleiro;
namespace Xadrez.Jogo
{
    class PartidaXadrez
    {
        public Tabuleiro.Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public List<Jogador> Jogadores { get; private set; }
        public Jogador JogadorLogado1 { get; private set; }
        public Jogador JogadorLogado2 { get; private set; }
        public bool Logado { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> Pecas { get; private set; }
        public HashSet<Peca> PecasCapturadas { get; private set; }
        public bool Xeque { get; private set; }
        public Peca PodeEnPassant { get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro.Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.BRANCA;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            Jogadores = new List<Jogador>();
            Logado = false; 
            Xeque = false;
            PodeEnPassant = null;
            ColocarPecas();
        }

        public void RealizaCadastro(string login, string senha, string nome)
        {
            Jogador jogador = new Jogador(login, senha, nome);
            Jogadores.Add(jogador);
        }
        public bool RealizaLogin(string login, string senha)
        {
            bool logarConta = Jogadores.Exists(x => x.Login == login && x.Senha == senha);
            
            if (logarConta)
            {
                if (JogadorLogado1 == null)
                {
                    JogadorLogado1 = Jogadores.Find(x => x.Login == login && x.Senha == senha);
                }
                else
                {
                    if(Jogadores.Find(x => x.Login == login && x.Senha == senha) == JogadorLogado1)
                    {
                        throw new PartidaException("Esse usuário já está logado.");
                    }
                    else
                    {
                        JogadorLogado2 = Jogadores.Find(x => x.Login == login && x.Senha == senha);
                        Logado = true; 
                    }
                }                
                return true;
            }
            return false;
        }

        public Peca RealizaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }

            // ## Roque pequeno 
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3); // A origem é feita em relação ao rei
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(origemDaTorre);
                torre.IncrementarMovimento();
                Tab.ColocarPeca(torre, destinoDaTorre);
            }

            // ## Roque grande 
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(origemDaTorre);
                torre.IncrementarMovimento();
                Tab.ColocarPeca(torre, destinoDaTorre);
            }

            // ## En passant 
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posicaoDoPeao;
                    if (p.Cor == Cor.BRANCA)
                    {
                        posicaoDoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posicaoDoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posicaoDoPeao);
                    PecasCapturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = RealizaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não se coloque em xeque! Repita a jogada...");
            }

            Peca p = Tab.RetornaPecaPosicao(destino);

            //## Jogada promoção
            if (p is Peao)
            {
                if ((p.Cor == Cor.BRANCA && destino.Linha == 0) || (p.Cor == Cor.PRETA && destino.Linha == 7))
                {
                    p = Tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca rainha = new Rainha(p.Cor, Tab);
                    Tab.ColocarPeca(rainha, destino);
                    Pecas.Add(rainha);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }


            if (EstaEmXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // ## En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {   // Verificando se o peão moveu a primeira vez, se for true, ele pode levar um En Passant
                PodeEnPassant = p;
            }
            else
            {
                PodeEnPassant = null;
            }
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            //Caso o movimento coloque a sua própria peça em xeque, esse método desfaz o movimento
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarMovimento();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);

            // ## Roque pequeno -- desfazendo movimento da torre
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(destinoDaTorre);
                torre.DecrementarMovimento();
                Tab.ColocarPeca(torre, origemDaTorre);
            }

            // ## Roque grande -- desfazendo movimento da torre
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(destinoDaTorre);
                torre.DecrementarMovimento();
                Tab.ColocarPeca(torre, origemDaTorre);
            }

            // ## En passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == PodeEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posicaoDoPeao;
                    if (p.Cor == Cor.BRANCA)
                    {
                        posicaoDoPeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posicaoDoPeao = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posicaoDoPeao);
                }
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.BRANCA)
            {
                JogadorAtual = Cor.PRETA;
            }
            else
            {
                JogadorAtual = Cor.BRANCA;
            }
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca p)
        {
            Tab.ColocarPeca(p, new PosicaoXadrez(coluna, linha).PosicaoXadrezParaMatriz());
            Pecas.Add(p);
        }

        public HashSet<Peca> AddCapturadas(Cor cor)
        {
            // Método para ver as peças capturadas de uma determinada cor.
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach (Peca x in PecasCapturadas)
            {
                if (x.Cor == cor)
                {
                    temp.Add(x);
                }
            }
            return temp;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            // Método para fazer um hashset com as peças em jogo. Essa diferença é feita usando .ExceptWith
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    temp.Add(x);
                }
            }
            temp.ExceptWith(AddCapturadas(cor));
            return temp;
        }


        private void ColocarPecas()
        {
            // Método auxiliar para colocar as peças iniciais no programa...
            //BRANCAS
            ColocarNovaPeca('a', 1, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.BRANCA, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.BRANCA, Tab));
            ColocarNovaPeca('d', 1, new Rainha(Cor.BRANCA, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.BRANCA, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.BRANCA, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.BRANCA, Tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.BRANCA, Tab, this));

            //PRETAS
            ColocarNovaPeca('a', 8, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.PRETA, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.PRETA, Tab));
            ColocarNovaPeca('d', 8, new Rainha(Cor.PRETA, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.PRETA, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.PRETA, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.PRETA, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.PRETA, Tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.PRETA, Tab, this));
        }

        //Métodos para controlar as movimentações
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.RetornaPecaPosicao(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição (origem)");
            }

            if (JogadorAtual != Tab.RetornaPecaPosicao(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua");
            }

            if (!Tab.RetornaPecaPosicao(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existe movimentos possíveis para essa peça.");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.RetornaPecaPosicao(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        //Métodos para a lógica de xeque
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.BRANCA)
            {
                return Cor.PRETA;
            }
            else
            {
                return Cor.BRANCA;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            // Se a posição do rei estiver dentro da matriz de movimentos possíveis de qualquer peça, retorna true
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Sem rei;"); // Enquanto não é implementada a lógica de fim de jogo 
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] matMovimentosPossiveis = x.MovimentosPossiveis();
                if (matMovimentosPossiveis[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EstaEmXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                // Método vai verificar cada movimento possível das peças em jogo - peça x
                // Se pelo menos um movimento de alguma peça retirar do xeque, retorna false; 
                // Se rodar a matriz inteira e não tiver movimento para retirar do xeque, return true e xeque mate
                bool[,] matMovimentosPossiveis = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Linhas; j++)
                    {
                        if (matMovimentosPossiveis[i, j]) // Se a posição tiver marcada como true, quer dizer que é um movimento possível
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = RealizaMovimento(origem, destino); // Realizando movimento apenas para teste
                            bool testeXeque = EstaEmXeque(cor); // Feito o teste, chamamos o método para desfazer o movimento
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
