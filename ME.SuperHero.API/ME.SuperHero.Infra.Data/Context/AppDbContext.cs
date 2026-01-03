using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ME.SuperHero.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Herois> Herois { get; set; }
        public DbSet<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }
        public DbSet<Superpoderes> Superpoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HeroisConfiguration());
            builder.ApplyConfiguration(new HeroisSuperpoderesConfiguration());
            builder.ApplyConfiguration(new SuperpoderesConfiguration());

            base.OnModelCreating(builder);
        }
    }
}

