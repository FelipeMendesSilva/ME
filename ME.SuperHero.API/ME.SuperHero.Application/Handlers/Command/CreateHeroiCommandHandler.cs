using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class CreateHeroiCommandHandler : IRequestHandler<RequestCreateHeroi, int>
    {
        private readonly IHeroisRepository _heroisRepository;
        public CreateHeroiCommandHandler(IHeroisRepository heroisRepository)
        {
            _heroisRepository = heroisRepository;
        }
        public async Task<int> Handle(RequestCreateHeroi request, CancellationToken cancellationToken)
        {


            var heroi = new Herois(
                request.Nome,
                request.NomeHeroi,
                request.DataNascimento,
                request.Altura,
                request.Peso
                );

            bool saved = await _heroisRepository.CreateAsync(heroi, cancellationToken);

            if (saved)
            {
                foreach (var poderId in request.Superpoderes)
                {
                    var vinculo = new HeroisSuperpoderes
                    {
                        HeroiId = heroi.Id, // usa o Id gerado
                        SuperpoderId = poderId
                    };

                    await _heroisRepository.AddPowersAsync(vinculo, cancellationToken);

                }
            }
            //return 
            return heroi.Id;
        }
    }
}
