using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestGetHeroiById : IRequest<Result>
    {
        public RequestGetHeroiById(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
