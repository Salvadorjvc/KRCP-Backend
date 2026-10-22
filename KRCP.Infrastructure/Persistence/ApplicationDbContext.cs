using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<OrdenTrabajo> OrdenesTrabajo => Set<OrdenTrabajo>();
        public DbSet<OtEvidencia> OtEvidencias => Set<OtEvidencia>();
        public DbSet<OtRepuestoConsumido> OtRepuestosConsumidos => Set<OtRepuestoConsumido>();
        public DbSet<MovimientoKardex> MovimientosKardex => Set<MovimientoKardex>();
        public DbSet<ErpIntegracionLog> ErpIntegracionLogs => Set<ErpIntegracionLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        //método para aplicar el conversor UTC de forma global
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Fuerza a que cualquier propiedad de tipo DateTime use el conversor UTC automáticamente
            configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
        }
    }
}
