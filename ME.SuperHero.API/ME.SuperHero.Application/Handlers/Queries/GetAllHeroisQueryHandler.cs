using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class GetAllHeroisQueryHandler : IRequestHandler<RequestGetAllHerois, List<ResponseGetHeroi>>
    {
        private readonly AppDbContext _dbContext;
        public GetAllHeroisQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ResponseGetHeroi>> Handle(RequestGetAllHerois request, CancellationToken cancellationToken)
        {
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
                        .Select(hs => new SuperpoderesResponse
                        {
                            Id = hs.Superpoder.Id,
                            Superpoder = hs.Superpoder.Superpoder,
                            Descricao = hs.Superpoder.Descricao
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);               

            return responseList;
        }
    }
}
