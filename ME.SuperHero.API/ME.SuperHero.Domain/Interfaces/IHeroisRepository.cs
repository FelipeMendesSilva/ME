using ME.SuperHero.Domain.Entities;

namespace ME.SuperHero.Domain.Interfaces
{
    public interface IHeroisRepository
    {
        Task<Herois?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(Herois movement, CancellationToken cancellationToken);
        Task AddPowersAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken);
        Task<bool> ExistsHeroiByNameAsync(string nomeHeroi, CancellationToken cancellationToken);
        Task UpdateAsync(Herois heroi, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
