using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestUpdateHeroi : IRequest<Result>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string NomeHeroi { get; set; }
        public List<int>? Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }
}
