namespace ME.SuperHero.Domain.Entities
{
    public class Herois
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }        
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }

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
    }
}
