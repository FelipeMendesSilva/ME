using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Result;
using ME.SuperHero.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class GetAllHeroisQueryHandler : IRequestHandler<RequestGetAllHerois, Result>
    {
        private readonly AppDbContext _dbContext;
        public GetAllHeroisQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(RequestGetAllHerois request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            var responseList = await _dbContext.Herois
                .Include(h => h.HeroisSuperpoderes)
                .ThenInclude(hs => hs.Superpoder)
                .Select(h => new ResponseGetHeroi
                {
                    Id = h.Id,
                    Nome = h.Nome,
                    NomeHeroi = h.NomeHeroi,
                    DataNascimento = h.DataNascimento,
                    Altura = h.Altura,
                    Peso = h.Peso,
                    Superpoderes = h.HeroisSuperpoderes
                        .Select(hs => new ResponseSuperpoderes
                        {
                            Id = hs.Superpoder.Id,
                            Superpoder = hs.Superpoder.Superpoder,
                            Descricao = hs.Superpoder.Descricao
                        })
                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (responseList.Any())
                return Result.Success(responseList);
            else
                return Result.Failure("Hero not found", System.Net.HttpStatusCode.NotFound);

        }
    }
}
