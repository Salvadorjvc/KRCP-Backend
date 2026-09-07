using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class OtEvidenciaConfiguration: IEntityTypeConfiguration<OtEvidencia>
    {
        public void Configure(EntityTypeBuilder<OtEvidencia> builder)
        {
            builder.ToTable("OT_Evidencias");

            builder.HasKey(e => e.EvidenciaId);
            builder.Property(e => e.EvidenciaId).HasColumnName("EvidenciaID");

            //fk
            builder.Property(e => e.OtId).HasColumnName("OTID").IsRequired();

            builder.HasOne(e => e.OrdenTrabajo)
                .WithMany()
                .HasForeignKey(e => e.OtId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.TipoEvidencia)
                .HasColumnName("TipoEvidencia")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.UrlArchivo)
                .HasColumnName("UrlArchivo")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(255);

            //fk
            builder.Property(e => e.UsuarioCargaId).HasColumnName("UsuarioCargaID").IsRequired();

            builder.HasOne(e => e.UsuarioCarga)
                .WithMany()
                .HasForeignKey(e => e.UsuarioCargaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.FechaCarga)
                .HasColumnName("FechaCarga")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
