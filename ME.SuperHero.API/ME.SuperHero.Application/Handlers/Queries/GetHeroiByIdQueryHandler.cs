using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Result;
using ME.SuperHero.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class GetHeroiByIdQueryHandler : IRequestHandler<RequestGetHeroiById, Result>
    {
        private readonly AppDbContext _dbContext;
        public GetHeroiByIdQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RequestGetHeroiById request, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Herois
                .AsNoTracking()
                .Where(h => h.Id == request.Id)
                .Select(h => new ResponseGetHeroi
                {
                    Id = h.Id,
                    Nome = h.Nome,
                    NomeHeroi = h.NomeHeroi,
                    DataNascimento = h.DataNascimento,
                    Altura = h.Altura,
                    Peso = h.Peso,
                    Superpoderes = h.HeroisSuperpoderes.Select(
                        hs => new ResponseSuperpoderes
                        {
                            Id = hs.Superpoder.Id,
                            Superpoder = hs.Superpoder.Superpoder,
                            Descricao = hs.Superpoder.Descricao
                        }
                    )
                .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (response != null)
                return Result.Success(response);
            else
                return Result.Failure("Hero not found", System.Net.HttpStatusCode.NotFound);
        }
    }
}
