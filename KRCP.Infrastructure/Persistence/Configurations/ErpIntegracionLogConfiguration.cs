using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class ErpIntegracionLogConfiguration: IEntityTypeConfiguration<ErpIntegracionLog>
    {
        public void Configure(EntityTypeBuilder<ErpIntegracionLog> builder)
        {
            builder.ToTable("ERP_IntegracionLog");

            builder.HasKey(e => e.LogId);
            builder.Property(e => e.LogId).HasColumnName("LogID");

            builder.Property(e => e.EntidadAfectada)
                .HasColumnName("EntidadAfectada")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.EntidadId)
                .HasColumnName("EntidadID")
                .IsRequired();

            builder.Property(e => e.Accion)
                .HasColumnName("Accion")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.PayloadJson)
                .HasColumnName("PayloadJSON")
                .HasColumnType("varchar(MAX)");

            builder.Property(e => e.FechaRegistro)
                .HasColumnName("FechaRegistro")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
