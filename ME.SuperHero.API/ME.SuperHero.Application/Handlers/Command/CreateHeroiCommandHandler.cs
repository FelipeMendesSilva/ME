using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class CreateHeroiCommandHandler : IRequestHandler<RequestCreateHeroi, int>
    {
        private readonly IHeroisRepository _heroisRepository;
        public CreateHeroiCommandHandler(IHeroisRepository manualMovementsRepository)
        {
            _heroisRepository = manualMovementsRepository;
        }
        public async Task<int> Handle(RequestCreateHeroi novoMovimentoManual, CancellationToken cancellationToken)
        {
            //criar novo Heroi
            //var heroi = new Herois();

            //return await _heroisRepository.CreateAsync(heroi, cancellationToken);
            return 1;
        }
    }
}
