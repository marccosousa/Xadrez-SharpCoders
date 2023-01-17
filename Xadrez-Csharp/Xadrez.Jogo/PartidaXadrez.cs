using Xadrez.Tabuleiro;
namespace Xadrez.Jogo
{
    class PartidaXadrez
    {
        public Tabuleiro.Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> Pecas { get; private set; }
        public HashSet<Peca> PecasCapturadas { get; private set; }
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro.Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.BRANCA;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            Xeque = false;
            ColocarPecas();
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
            return pecaCapturada; 
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = RealizaMovimento(origem, destino);
            
            if(EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não se coloque em xeque! Repita a jogada...");
            }
            if(EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true; 
            }
            else
            {
                Xeque = false; 
            }           

            Turno++;
            MudaJogador();

        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            //Caso o movimento coloque a sua própria peça em xeque, o método desfaz o movimento
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarMovimento();
            if(pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino); 
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
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
            foreach(Peca x in PecasCapturadas)
            {
                if(x.Cor == cor)
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
            ColocarNovaPeca('c', 1, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.BRANCA, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.BRANCA, Tab));

            ColocarNovaPeca('c', 7, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.PRETA, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.PRETA, Tab));
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
            if (!Tab.RetornaPecaPosicao(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }
        
        //Métodos para a lógica de xeque
        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.BRANCA)
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
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei) 
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
            if(R == null)
            {
                throw new TabuleiroException("Sem rei;"); // Enquanto não é implementada a lógica de fim de jogo 
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor))) {
                bool[,] matMovimentosPossiveis = x.MovimentosPossiveis();
                if (matMovimentosPossiveis[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}
