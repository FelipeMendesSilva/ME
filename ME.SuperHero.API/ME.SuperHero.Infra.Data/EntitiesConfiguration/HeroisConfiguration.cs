using ME.SuperHero.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ME.SuperHero.Infra.Data.EntitiesConfiguration
{
    public class HeroisConfiguration : IEntityTypeConfiguration<Herois>
    {   
        public void Configure(EntityTypeBuilder<Herois> builder)
        {
            builder.ToTable("herois");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                   .HasColumnName("id")
                   .HasColumnType("int");

            builder.Property(h => h.Nome)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("nome")
                   .HasColumnType("varchar(100)");

            builder.Property(h => h.NomeHeroi)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("nome_heroi")
                   .HasColumnType("varchar(100)");

            builder.Property(h => h.DataNascimento)
                   .HasColumnName("data_nascimento")
                   .HasColumnType("date");

            builder.Property(h => h.Altura)
                   .IsRequired()
                   .HasColumnName("altura")
                   .HasColumnType("double");

            builder.Property(h => h.Peso)
                   .IsRequired()
                   .HasColumnName("peso")
                   .HasColumnType("double");

            //builder.HasMany(h => h.HeroisSuperpoderes)
            //    .WithOne(sp => sp.Heroi)
            //    .HasForeignKey(sp => sp.HeroiId);
        }
    }
}
