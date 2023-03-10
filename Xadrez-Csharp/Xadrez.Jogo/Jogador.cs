using Xadrez.Tabuleiro; 
namespace Xadrez.Jogo
{
    class Jogador
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; private set; }
        public int Vitorias { get; private set; }
        
        public Jogador(string login, string senha, string nome)
        {
            Login = login;
            Senha = senha; 
            Nome = nome;
        }

        public void IncrementarVitorias()
        {
            Vitorias++;
        }

        public override string ToString()
        {
            return $"Login: {Login} | Nome: {Nome} | Vitórias: {Vitorias}";
        }
    }
}
