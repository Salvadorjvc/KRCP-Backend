using CloudinaryDotNet;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Security;
using KRCP.Application.Interfaces.Services;
using KRCP.Infrastructure.Persistence;
using KRCP.Infrastructure.Repositories;
using KRCP.Infrastructure.Security;
using KRCP.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Persistencia (EF Core + SQL Server)
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));

            //repositorios
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IUbicacionRepository, UbicacionRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IOrdenTrabajoRepository, OrdenTrabajoRepository>();
            services.AddScoped<IOtEvidenciaRepository, OtEvidenciaRepository>();
            services.AddScoped<IOtRepuestoConsumidoRepository, OtRepuestoConsumidoRepository>();
            services.AddScoped<IMovimientoKardexRepository, MovimientoKardexRepository>();
            services.AddScoped<IErpIntegracionLogRepository, ErpIntegracionLogRepository>();

            //seguridad
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            //cloudinary
            services.AddScoped<IFileStorageService, CloudinaryFileStorageService>();



            return services;
        }
    }
}
