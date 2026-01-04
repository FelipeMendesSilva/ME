using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Domain.Result;
using MediatR;

namespace ME.SuperHero.Application.Handlers.Command
{
    public class DeleteHeroiCommandHandler : IRequestHandler<RequestDeleteHeroi, Result>
    {
        private readonly IHeroisRepository _heroisRepository;
        private readonly IHeroisSuperpoderesRepository _heroisSupRepository;
        private readonly IUow _uow;

        public DeleteHeroiCommandHandler(IHeroisRepository heroisRepository, IHeroisSuperpoderesRepository heroisSupRepository, IUow uow)
        {
            _heroisRepository = heroisRepository;
            _heroisSupRepository = heroisSupRepository;
            _uow = uow;
        }
        public async Task<Result> Handle(RequestDeleteHeroi request, CancellationToken cancellationToken)
        {
            var heroi = await _heroisRepository.GetByIdAsync(request.Id, cancellationToken);

            if (heroi == null)
                return Result.Failure("Hero not found", System.Net.HttpStatusCode.NotFound);

            await _heroisSupRepository.RemoveAllPowersAsync(request.Id, cancellationToken);
            await _heroisRepository.DeleteAsync(request.Id, cancellationToken);
            bool deleted = await _uow.SaveChangesAsync(cancellationToken);

            if (deleted)
                return Result.Success("Deleted");
            else
                return Result.Failure("Deleting not completed", System.Net.HttpStatusCode.BadRequest);
        }
    }
}
