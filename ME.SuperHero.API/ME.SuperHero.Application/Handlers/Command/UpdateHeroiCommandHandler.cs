using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class UpdateHeroiCommandHandler : IRequestHandler<RequestUpdateHeroi, Result>
    {
        private readonly IHeroisRepository _heroisRepository;
        private readonly IHeroisSuperpoderesRepository _heroisSupRepository;
        private readonly IUow _uow;

        public UpdateHeroiCommandHandler(IHeroisRepository heroisRepository, IHeroisSuperpoderesRepository heroisSupRepository, IUow uow)
        {
            _heroisRepository = heroisRepository;
            _heroisSupRepository = heroisSupRepository;
            _uow = uow;
        }

        public async Task<Result> Handle(RequestUpdateHeroi request, CancellationToken cancellationToken)
        {

            var heroi = await _heroisRepository.GetByIdAsync(request.Id, cancellationToken);

            if (heroi == null)
                return Result.Failure("Hero not found", System.Net.HttpStatusCode.NotFound);

            if (heroi?.NomeHeroi != request.NomeHeroi && await _heroisRepository.ExistsHeroiByNameAsync(request.NomeHeroi, cancellationToken))
                return Result.Failure("NomeHeroi already exists", System.Net.HttpStatusCode.BadRequest);


            heroi.ChangeNome(request.Nome);
            heroi.ChangeNomeHeroi(request.NomeHeroi);
            heroi.ChangeDataNascimento(request.DataNascimento);
            heroi.ChangeAltura(request.Altura);
            heroi.ChangePeso(request.Peso);

            await _heroisRepository.UpdateAsync(heroi, cancellationToken);

            await UpdateHeroiSuperpoderesAsync(heroi, request, cancellationToken);

            bool updated = await _uow.SaveChangesAsync(cancellationToken);

            if (updated)
                return Result.Success("Updated");
            else
                return Result.Failure("Updating not completed", System.Net.HttpStatusCode.BadRequest);

        }

        private async Task UpdateHeroiSuperpoderesAsync(Herois heroi, RequestUpdateHeroi request, CancellationToken cancellationToken)
        {
            var powerToAdd = request.Superpoderes.Except(heroi.GetSuperpoderesIds()).ToList();
            var powerToRemove = heroi.GetSuperpoderesIds().Except(request.Superpoderes).ToList();

            foreach (var superpodereId in powerToAdd)
            {
                var vinculo = new HeroisSuperpoderes
                {
                    HeroiId = heroi.Id,
                    SuperpoderId = superpodereId
                };

                await _heroisSupRepository.AddPowerAsync(vinculo, cancellationToken);
            }

            foreach (var superpodereId in powerToRemove)
            {
                await _heroisSupRepository.RemovePowerBySuperpoderIdAsync(superpodereId, cancellationToken);
            }
        }
    }
}
