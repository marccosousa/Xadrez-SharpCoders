using Xadrez.Tabuleiro; 
namespace Xadrez.Jogo
{
    class Jogador
    {
        public string Nome { get; private set; }
        public Cor Cor { get; private set; }
        public int Vitorias { get; private set; }
        public PartidaXadrez Partida { get; private set; }

        public Jogador(string nome, Cor cor)
        {
            Nome = nome;
            Cor = cor;
            Vitorias = 0; 
        }

        public void IncrementarVitorias()
        {
            Vitorias++;
        }

        public override string ToString()
        {
            return $"{Nome}, {Cor}"; 
        }
    }
}
