using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestCreateHeroi : IRequest<Result>
    {
        public required string Nome { get; set; }
        public required string NomeHeroi { get; set; }
        public required List<int> Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }
}
