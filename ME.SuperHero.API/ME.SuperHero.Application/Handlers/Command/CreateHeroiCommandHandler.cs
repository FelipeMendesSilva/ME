using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class CreateHeroiCommandHandler : IRequestHandler<RequestCreateHeroi, Result>
    {
        private readonly IHeroisRepository _heroisRepository;
        private readonly IHeroisSuperpoderesRepository _heroisSupRepository;
        private readonly IUow _uow;

        public CreateHeroiCommandHandler(IHeroisRepository heroisRepository, IHeroisSuperpoderesRepository heroisSupRepository, IUow uow)
        {
            _heroisRepository = heroisRepository;
            _heroisSupRepository = heroisSupRepository;
            _uow = uow;
        }
        public async Task<Result> Handle(RequestCreateHeroi request, CancellationToken cancellationToken)
        {
            if (await _heroisRepository.ExistsHeroiByNameAsync(request.NomeHeroi, cancellationToken))
                return Result.Failure("NomeHeroi already exists", System.Net.HttpStatusCode.BadRequest);

            var heroi = new Herois(
                request.Nome,
                request.NomeHeroi,
                request.DataNascimento,
                request.Altura,
                request.Peso
                );

            await _heroisRepository.CreateAsync(heroi, cancellationToken);

            foreach (var poderId in request.Superpoderes)
            {
                var vinculo = new HeroisSuperpoderes
                {
                    HeroiId = heroi.Id, // usa o Id gerado
                    SuperpoderId = poderId
                };

                await _heroisSupRepository.AddPowerAsync(vinculo, cancellationToken);
            }

            bool saved = await _uow.SaveChangesAsync(cancellationToken);

            if (saved)
                return Result.Success("Created", System.Net.HttpStatusCode.Created);
            else
                return Result.Failure("Creating not completed", System.Net.HttpStatusCode.BadRequest);
        }
    }
}
