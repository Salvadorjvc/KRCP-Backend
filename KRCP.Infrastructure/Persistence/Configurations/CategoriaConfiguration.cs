using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class CategoriaConfiguration: IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.CategoriaId);
            builder.Property(c => c.CategoriaId).HasColumnName("CategoriaID");

            builder.Property(c => c.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(c => c.Nombre).IsUnique();

            builder.Property(c => c.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(255);

            builder.Property(c => c.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);
        }

    }
}
