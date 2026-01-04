using ME.SuperHero.Domain.Entities;

namespace ME.SuperHero.Domain.Interfaces
{
    public interface IHeroisSuperpoderesRepository
    {
        Task AddPowerAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken);
        Task<bool> RemovePowerBySuperpoderIdAsync(int superpoderId, CancellationToken cancellationToken);
    }
}
