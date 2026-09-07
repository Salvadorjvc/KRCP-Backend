using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(p => p.ProductoId);
            builder.Property(p => p.ProductoId).HasColumnName("ProductoID");

            builder.Property(p => p.CodigoParte)
                .HasColumnName("CodigoParte")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(p => p.CodigoParte).IsUnique();

            builder.Property(p => p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(150)
                .IsRequired();

            //fk
            builder.Property(p => p.CategoriaId).HasColumnName("CategoriaID").IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.UbicacionId).HasColumnName("UbicacionID");

            builder.HasOne(p => p.Ubicacion)
                .WithMany(u => u.Productos) //una ubicacion si puede tener varios productos debido a que es por sectores supongo
                .HasForeignKey(p => p.UbicacionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(p => p.StockActual)
                .HasColumnName("StockActual")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.StockMinimo)
                .HasColumnName("StockMinimo")
                .IsRequired()
                .HasDefaultValue(5);

            builder.Property(p => p.CostoUnitario)
                .HasColumnName("CostoUnitario")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00m);

            builder.Property(p => p.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);

            builder.Property(p => p.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.FechaModificacion)
                .HasColumnName("FechaModificacion")
                .HasColumnType("datetime");

            //fk
            builder.Property(p => p.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(p => p.UsuarioModificacion)
                .WithMany()
                .HasForeignKey(p => p.UsuarioModificacionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
