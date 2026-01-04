using ME.SuperHero.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ME.SuperHero.Infra.Data.EntitiesConfiguration
{
    public class HeroisSuperpoderesConfiguration : IEntityTypeConfiguration<HeroisSuperpoderes>
    {
        public void Configure(EntityTypeBuilder<HeroisSuperpoderes> builder)
        {
            builder.ToTable("herois_superpoderes");

            builder.HasKey(sp => new { sp.HeroiId, sp.SuperpoderId });

            builder.Property(sp => sp.HeroiId)
                   .IsRequired()
                   .HasColumnName("heroi_id")
                   .HasColumnType("int");

            builder.Property(sp => sp.SuperpoderId)
                   .IsRequired()
                   .HasColumnName("superpoder_id")
                   .HasColumnType("int");

            builder.HasOne(sp => sp.Heroi)
                   .WithMany(h => h.HeroisSuperpoderes)
                   .HasForeignKey(sp => sp.HeroiId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(sp => sp.Superpoder)
                   .WithMany(s => s.HeroisSuperpoderes)
                   .HasForeignKey(sp => sp.SuperpoderId);
        }
    }
}
