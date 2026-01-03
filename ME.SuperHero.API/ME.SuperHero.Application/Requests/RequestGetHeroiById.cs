using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Entities;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestGetHeroiById : IRequest<ResponseGetHeroi>
    {
        public int Id { get; set; }
    }
}
