using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class GetHeroiByIdQueryHandler : IRequestHandler<RequestGetHeroiById, ResponseGetHeroi>
    {

        private readonly AppDbContext _dbContext;
        public GetHeroiByIdQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseGetHeroi> Handle(RequestGetHeroiById request, CancellationToken cancellationToken)
        {
            var heroi = await _dbContext.Herois
                    .Include(h => h.HeroisSuperpoderes)
                    .ThenInclude(hs => hs.Superpoder)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var superpoderList = heroi?.HeroisSuperpoderes?.Select(hs => new SuperpoderesResponse
            {
                Id = hs.Superpoder.Id,
                Superpoder = hs.Superpoder.Superpoder,
                Descricao = hs.Superpoder.Descricao
            })
            .ToList();

            var response = new ResponseGetHeroi()
            {
                Id = heroi.Id,
                Nome = heroi.Nome,
                NomeHeroi = heroi.NomeHeroi,
                Superpoderes = superpoderList,
                DataNascimento = heroi.DataNascimento,
                Altura = heroi.Altura,
                Peso = heroi.Peso
            };

            return response;
        }
    }
}
