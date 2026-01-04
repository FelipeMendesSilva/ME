using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Result;
using ME.SuperHero.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class GetAllSuperpoderesQueryHandler : IRequestHandler<RequestGetSuperpoderes, Result>
    {

        private readonly AppDbContext _dbContext;
        public GetAllSuperpoderesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Handle(RequestGetSuperpoderes request, CancellationToken cancellationToken)
        {

            var response = await _dbContext.Superpoderes
                .AsNoTracking()
                .Select(h => new ResponseSuperpoderes
                {
                    Id = h.Id,
                    Superpoder = h.Superpoder,
                    Descricao = h.Descricao
                }
                    )
                .ToListAsync(cancellationToken);

            if (response.Any())
                return Result.Success(response);
            else
                return Result.Failure("Superpowers not found", System.Net.HttpStatusCode.NotFound);
        }
    }
}
