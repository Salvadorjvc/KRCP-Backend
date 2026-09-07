using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class OtRepuestoConsumidoConfiguration: IEntityTypeConfiguration<OtRepuestoConsumido>
    {
        public void Configure(EntityTypeBuilder<OtRepuestoConsumido> builder)
        {
            builder.ToTable("OT_RepuestosConsumidos");

            builder.HasKey(rp => rp.DetalleId);
            builder.Property(rp => rp.DetalleId).HasColumnName("DetalleID");

            //fk
            builder.Property(rp => rp.OtId).HasColumnName("OTID").IsRequired();

            builder.HasOne(rp => rp.OrdenTrabajo)
                .WithMany()
                .HasForeignKey(rp => rp.OtId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rp => rp.ProductoId).HasColumnName("ProductoID").IsRequired();

            builder.HasOne(rp => rp.Producto)
                .WithMany()
                .HasForeignKey(rp => rp.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rp => rp.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            builder.Property(rp => rp.PrecioUnitarioHistorico)
                .HasColumnName("PrecioUnitarioHistorico")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00m);

            //fk
            builder.Property(rp => rp.UsuarioAlmacenId).HasColumnName("UsuarioAlmacenID").IsRequired();

            builder.HasOne(rp => rp.UsuarioAlmacen)
                .WithMany()
                .HasForeignKey(rp => rp.UsuarioAlmacenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rp => rp.FechaDespacho)
                .HasColumnName("FechaDespacho")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
