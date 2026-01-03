using ME.SuperHero.Domain.Entities;

namespace ME.SuperHero.Domain.Interfaces
{
    public interface IHeroisRepository
    {
        Task<Herois?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Herois movement, CancellationToken cancellationToken);
        Task<bool> AddPowersAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken);
    }
}
