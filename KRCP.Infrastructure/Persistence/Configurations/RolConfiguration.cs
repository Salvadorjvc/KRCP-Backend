using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class RolConfiguration: IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles"); //nombre rial g de la tabla

            builder.HasKey(r => r.RolId);
            builder.Property(r => r.RolId).HasColumnName("RolID");

            builder.Property(r => r.NombreRol)
                .HasColumnName("NombreRol")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(r => r.NombreRol).IsUnique();

            builder.Property(r => r.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);
        }
    }
}
