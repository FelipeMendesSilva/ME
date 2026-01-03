using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestCreateHeroi : IRequest<int>
    {
        public required string Nome { get; set; }
        public required string NomeHeroi { get; set; }
        public required List<SuperpoderesRequestCreate> Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }

    public class SuperpoderesRequestCreate
    {
        public int Id { get; set; }
        public required string Superpoder { get; set; }
        public string? Descricao { get; set; }
    }
}
