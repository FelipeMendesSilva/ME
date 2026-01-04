using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ME.SuperHero.Infra.Data.Repositories
{
    public class SuperpoderesRepository : ISuperpoderesRepository
    {
        private readonly AppDbContext _context;

        public SuperpoderesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Superpoderes>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Superpoderes.ToListAsync(cancellationToken);
        }
    }
}
