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
                .ThenInclude(hs => hs.Superpoder)
                .FirstOrDefaultAsync<Herois>(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsHeroiByNameAsync(string nomeHeroi, CancellationToken cancellationToken)
        => await _context.Herois.AnyAsync(x => x.NomeHeroi == nomeHeroi, cancellationToken);

        public async Task CreateAsync(Herois heroi, CancellationToken cancellationToken)
        {
            await _context.Herois.AddAsync(heroi, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        public async Task AddPowersAsync(HeroisSuperpoderes hs, CancellationToken cancellationToken)
        {
            await _context.HeroisSuperpoderes.AddAsync(hs, cancellationToken);
            return;
        }

        public async Task UpdateAsync(Herois heroi, CancellationToken cancellationToken)
        {
            _context.Herois.Update(heroi);
            return;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var heroi = await _context.Herois
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

            if (heroi == null)
                return false;

            _context.Herois.Remove(heroi);

            return true;
        }

    }
}
