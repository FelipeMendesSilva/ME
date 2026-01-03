namespace ME.SuperHero.Application.Responses
{
    public class GetHeroiResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string NomeHeroi { get; set; }
        public required List<SuperpoderesResponse>? Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }

    public class SuperpoderesResponse
    {
        public int Id { get; set; }
        public required string Superpoder { get; set; }
        public string? Descricao { get; set; }
    }
}
