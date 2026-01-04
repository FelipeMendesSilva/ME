using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Infra.Data.Repositories
{
    public class HeroisSuperpoderesRepository : IHeroisSuperpoderesRepository
    {
        private readonly AppDbContext _context;

        public HeroisSuperpoderesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPowerAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken)
        {
            await _context.HeroisSuperpoderes.AddAsync(hs, cancellationToken);
            return;
        }

        public async Task<bool> RemovePowerBySuperpoderIdAsync(int superpoderId, CancellationToken cancellationToken)
        {
            var success = await _context.HeroisSuperpoderes.Where(hs => hs.SuperpoderId == superpoderId).ExecuteDeleteAsync(cancellationToken);
            return success > 0;
        }

        public async Task<bool> RemoveAllPowersByHeroiIdAsync(int heroiId, CancellationToken cancellationToken)
        {
            var success = await _context.HeroisSuperpoderes.Where(hs => hs.HeroiId == heroiId).ExecuteDeleteAsync(cancellationToken);
            return success > 0;
        }
    }
}
