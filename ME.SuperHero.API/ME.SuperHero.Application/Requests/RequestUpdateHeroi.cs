using ME.SuperHero.Application.Responses;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestUpdateHeroi : IRequest<bool>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string NomeHeroi { get; set; }
        public required List<int> Superpoderes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
    }

    //public class SuperpoderesRequestUpdate
    //{
    //    public int Id { get; set; }
    //    public required string Superpoder { get; set; }
    //    public string? Descricao { get; set; }
    //}
}
