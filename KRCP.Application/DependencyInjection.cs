using KRCP.Application.Interfaces.Services;
using KRCP.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentValidation;

namespace KRCP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar todos los Validators de FluentValidation automáticamente
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registrar los Services
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IClienteService, ClienteService > ();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUbicacionService, UbicacionService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IOrdenTrabajoService, OrdenTrabajoService>();
            services.AddScoped<IOtEvidenciaService, OtEvidenciaService>();
            services.AddScoped<IOtRepuestoConsumidoService, OtRepuestoConsumidoService>();
            services.AddScoped<IMovimientoKardexService, MovimientoKardexService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IErpIntegracionLogService, ErpIntegracionLogService>();
            services.AddScoped<IDashboardService, DashboardService>();

            return services;
        }
    }
}
