using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class UbicacionConfiguration: IEntityTypeConfiguration<Ubicacion>
    {
        public void Configure(EntityTypeBuilder<Ubicacion> builder)
        {
            builder.ToTable("Ubicaciones");

            builder.HasKey(u => u.UbicacionId);
            builder.Property(u => u.UbicacionId).HasColumnName("UbicacionID");

            builder.Property(u => u.CodigoUbicacion)
                .HasColumnName("CodigoUbicacion")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(u => u.CodigoUbicacion).IsUnique();

            builder.Property(u => u.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(150);

            builder.Property(u => u.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);
        }
    }
}
