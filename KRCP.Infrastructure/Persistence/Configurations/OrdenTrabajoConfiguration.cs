using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class OrdenTrabajoConfiguration: IEntityTypeConfiguration<OrdenTrabajo>
    {
        public void Configure(EntityTypeBuilder<OrdenTrabajo> builder)
        {
            builder.ToTable("OrdenesTrabajo");

            builder.HasKey(ot => ot.OtId);
            builder.Property(ot => ot.OtId).HasColumnName("OTID");

            builder.Property(ot => ot.CodigoOT)
                .HasColumnName("CodigoOT")
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(ot => ot.CodigoOT).IsUnique();

            //fk
            builder.Property(ot => ot.ClienteId).HasColumnName("ClienteID").IsRequired();

            builder.HasOne(ot => ot.Cliente)
                .WithMany(c => c.OrdenesTrabajo)
                .HasForeignKey(ot => ot.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ot => ot.EquipoComponente)
                .HasColumnName("EquipoComponente")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(ot => ot.NumeroSerie)
                .HasColumnName("NumeroSerie")
                .HasMaxLength(100)
                .IsRequired();

            //fk
            builder.Property(ot => ot.UsuarioPlanificadorId).HasColumnName("UsuarioPlanificadorID").IsRequired();

            builder.HasOne(ot => ot.UsuarioPlanificador)
                .WithMany()
                .HasForeignKey(ot => ot.UsuarioPlanificadorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(ot => ot.TecnicoAsignadoId).HasColumnName("TecnicoAsignadoID");

            builder.HasOne(ot => ot.TecnicoAsignado)
                .WithMany()
                .HasForeignKey(ot => ot.TecnicoAsignadoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(ot => ot.Estado)
                .HasColumnName("Estado")
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired()
                .HasDefaultValue(EstadoOrdenTrabajo.Registrado);

            builder.Property(ot => ot.FechaIngreso)
                .HasColumnName("FechaIngreso")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ot => ot.FechaEstimadaEntrega)
                .HasColumnName("FechaEstimadaEntrega")
                .HasColumnType("datetime");

            builder.Property(ot => ot.FechaCierre)
                .HasColumnName("FechaCierre")
                .HasColumnType("datetime");

            builder.Property(ot => ot.CostoManoObra)
                .HasColumnName("CostoManoObra")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00m);

            builder.Property(ot => ot.CostoRepuestos)
                .HasColumnName("CostoRepuestos")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00m);

            builder.Property(ot => ot.CostoTotal)
                .HasColumnName("CostoTotal")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00m);

            builder.Property(ot => ot.Observaciones)
                .HasColumnName("Observaciones")
                .HasColumnType("varchar(MAX)");

            builder.Property(ot => ot.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);

            builder.Property(ot => ot.FechaModificacion)
                .HasColumnName("FechaModificacion")
                .HasColumnType("datetime");

            //fk
            builder.Property(ot => ot.UsuarioModificacionId).HasColumnName("UsuarioModificacionID");

            builder.HasOne(ot => ot.UsuarioModificacion)
                .WithMany()
                .HasForeignKey(ot => ot.UsuarioModificacionId)
                .OnDelete(DeleteBehavior.Restrict);    
        }
    }
}
