using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class UpdateHeroiCommandHandler : IRequestHandler<RequestUpdateHeroi, bool>
    {
        private readonly IHeroisRepository _heroisRepository;
        public UpdateHeroiCommandHandler(IHeroisRepository heroisRepository)
        {
            _heroisRepository = heroisRepository;
        }
        public async Task<bool> Handle(RequestUpdateHeroi request, CancellationToken cancellationToken)
        {
            //criar novo Heroi
            //var heroi = new Herois();

            //return await _heroisRepository.CreateAsync(heroi, cancellationToken);
            return true;
        }
    }
}
