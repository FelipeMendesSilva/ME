namespace ME.SuperHero.Domain.Entities
{
    public class Herois
    {
        public int Id { get; private set; } = 0;
        public string Nome { get; private set; }
        public string NomeHeroi { get; private set; }        
        public DateTime? DataNascimento { get; private set; }
        public double Altura { get; private set; }
        public double Peso { get; private set; }

        public virtual IEnumerable<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

        public Herois() { }

        public Herois(
        string nome,
        string nomeHeroi,
        DateTime? dataNascimento,
        double altura,
        double peso)
         {
             Nome = nome;
             NomeHeroi = nomeHeroi;
             DataNascimento = dataNascimento;
             Altura = altura;
             Peso = peso;
         }

        public void ChangeNome(string novoNome) => Nome = novoNome; 
        public void ChangeNomeHeroi(string novoNomeHeroi) => NomeHeroi = novoNomeHeroi; 
        public void ChangeDataNascimento(DateTime? novaData) => DataNascimento = novaData; 
        public void ChangeAltura(double novaAltura) => Altura = novaAltura; 
        public void ChangePeso(double novoPeso) => Peso = novoPeso;
    }
}

