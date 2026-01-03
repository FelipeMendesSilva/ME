using ME.SuperHero.Domain.Entities;

namespace ME.SuperHero.Domain.Interfaces
{
    public interface IHeroisRepository
    {
        Task<bool> CreateAsync(Herois movement, CancellationToken cancellationToken);
    }
}
