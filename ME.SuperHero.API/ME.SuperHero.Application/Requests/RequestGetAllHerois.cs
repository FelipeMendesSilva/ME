using ME.SuperHero.Application.Responses;
using MediatR;

namespace ME.SuperHero.Application.Requests
{
    public class RequestGetAllHerois : IRequest<List<ResponseGetHeroi>>
    {       
    }
}
