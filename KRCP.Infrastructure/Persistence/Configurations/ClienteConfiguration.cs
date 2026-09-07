using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration: IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.ClienteId);
            builder.Property(c => c.ClienteId).HasColumnName("ClienteID");

            builder.Property(c => c.RUC)
                .HasColumnName("RUC")
                .HasMaxLength(11)
                .IsRequired();
            builder.HasIndex(c => c.RUC).IsUnique();

            builder.Property(c => c.RazonSocial)
                .HasColumnName("RazonSocial")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.ContactoEmail)
                .HasColumnName("ContactoEmail")
                .HasMaxLength(100);

            builder.Property(c => c.Telefono)
                .HasColumnName("Telefono")
                .HasMaxLength(20);

            builder.Property(c => c.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);

            builder.Property(c => c.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.FechaModificacion)
                .HasColumnName("FechaModificacion")
                .HasColumnType("datetime");
            
        }
    }
}
