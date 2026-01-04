using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestDeleteHeroi : IRequest<Result>
    {
        public RequestDeleteHeroi(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
