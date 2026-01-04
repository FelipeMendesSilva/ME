using ME.SuperHero.Domain.Entities;

namespace ME.SuperHero.Domain.Interfaces
{
    public interface ISuperpoderesRepository
    {
        Task<List<Superpoderes>> GetAllAsync(CancellationToken cancellationToken);
    }
}
