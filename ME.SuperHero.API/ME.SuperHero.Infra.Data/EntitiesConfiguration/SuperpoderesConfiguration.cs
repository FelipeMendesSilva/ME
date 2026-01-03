using ME.SuperHero.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ME.SuperHero.Infra.Data.EntitiesConfiguration
{
    public class SuperpoderesConfiguration : IEntityTypeConfiguration<Superpoderes>
    {   
        public void Configure(EntityTypeBuilder<Superpoderes> builder)
        {
            builder.ToTable("superpoderes");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("id")
                   .HasColumnType("int");

            builder.Property(s => s.Superpoder)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("superpoder")
                   .HasColumnType("varchar(100)");

            builder.Property(s => s.Descricao)
                   .HasMaxLength(255)
                   .HasColumnName("descricao")
                   .HasColumnType("varchar(255)");
        }
    }
}
