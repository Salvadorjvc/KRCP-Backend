using KRCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration: IEntityTypeConfiguration<Usuario> 
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.ToTable("Usuarios");

            builder.HasKey(u => u.UsuarioId);
            builder.Property(u => u.UsuarioId).HasColumnName("UsuarioID");

            builder.Property(u => u.NombreCompleto)
                .HasColumnName("NombreCompleto")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();


            builder.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(255)
                .IsRequired();

            //Foreign key
            builder.Property(u => u.RolId).HasColumnName("RolID").IsRequired();

            builder.HasOne(u => u.Rol) // Usuario tiene UN rol
                .WithMany(r => r.Usuarios)                // un rol tiene muchos usuarios
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict); //evita borrado en cascada accidental


            builder.Property(u => u.Activo)
                .HasColumnName("Activo")
                .HasDefaultValue(true);

            builder.Property(u => u.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.FechaModificacion)
                .HasColumnName("FechaModificacion")
                .HasColumnType("datetime");
            
        }
    }
}
