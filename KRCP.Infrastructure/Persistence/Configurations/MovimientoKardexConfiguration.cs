using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class MovimientoKardexConfiguration: IEntityTypeConfiguration<MovimientoKardex>
    {
        public void Configure(EntityTypeBuilder<MovimientoKardex> builder)
        {
            builder.ToTable("MovimientosKardex");

            builder.HasKey(k => k.KardexId);
            builder.Property(k => k.KardexId).HasColumnName("KardexID");

            //fk
            builder.Property(k => k.ProductoId).HasColumnName("ProductoID").IsRequired();

            builder.HasOne(k => k.Producto)
                .WithMany()
                .HasForeignKey(k => k.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(k => k.UsuarioId).HasColumnName("UsuarioID").IsRequired();

            builder.HasOne(k => k.Usuario)
                .WithMany()
                .HasForeignKey(k => k.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(k => k.OtId).HasColumnName("OTID");

            builder.HasOne(k => k.OrdenTrabajo)
                .WithMany()
                .HasForeignKey(k => k.OtId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(k => k.TipoMovimiento)
                .HasColumnName("TipoMovimiento")
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(k => k.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            builder.Property(k => k.StockAnterior)
                .HasColumnName("StockAnterior")
                .IsRequired();

            builder.Property(k => k.StockNuevo)
                .HasColumnName("StockNuevo")
                .IsRequired();

            builder.Property(k => k.Motivo)
                .HasColumnName("Motivo")
                .HasMaxLength(255);

            builder.Property(k => k.FechaMovimiento)
                .HasColumnName("FechaMovimiento")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
