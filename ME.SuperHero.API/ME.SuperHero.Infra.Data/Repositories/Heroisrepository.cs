using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Infra.Data.Repositories
{
    public class HeroisRepository : IHeroisRepository
    {
        private readonly AppDbContext _context;

        public HeroisRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Herois?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Herois
                .Include(x => x.HeroisSuperpoderes)
                .FirstOrDefaultAsync<Herois>(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> CreateAsync(Herois heroi, CancellationToken cancellationToken)
        {
            await _context.Herois.AddAsync(heroi, cancellationToken);
            var success = await _context.SaveChangesAsync(cancellationToken);
            return success > 0;
        }

        public async Task<bool> AddPowersAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken)
        {
            await _context.HeroisSuperpoderes.AddAsync(hs, cancellationToken);
            var success = await _context.SaveChangesAsync(cancellationToken);
            return success > 0;
        }
    }
}
