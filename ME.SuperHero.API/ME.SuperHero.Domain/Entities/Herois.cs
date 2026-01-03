namespace ME.SuperHero.Domain.Entities
{
    public class Herois
    {
        public int Id { get; set; }
        public required string Nome { get; set; } 
        public required string NomeHeroi { get; set; } 
        public required List<HeroisSuperpoderes>? Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }
}
