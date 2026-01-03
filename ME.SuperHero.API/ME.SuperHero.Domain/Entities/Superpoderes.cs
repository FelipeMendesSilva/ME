namespace ME.SuperHero.Domain.Entities
{
    public class Superpoderes
    {
        public int Id { get; set; }
        public required string Superpoder { get; set; }
        public string? Descricao { get; set; }

        public virtual IEnumerable<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }
    }
}
