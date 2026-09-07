using KRCP.Application.DTOs.Dashboard;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class OtRepuestoConsumidoRepository: GenericRepository<OtRepuestoConsumido>, IOtRepuestoConsumidoRepository
    {
        public OtRepuestoConsumidoRepository(ApplicationDbContext context) : base(context)
        { }

        private IQueryable<OtRepuestoConsumido> QueryConRelaciones()
        {
            return _context.OtRepuestosConsumidos
                .Include(rp => rp.OrdenTrabajo)
                .Include(rp => rp.Producto)
                .Include(rp => rp.UsuarioAlmacen);
        }

        public override async Task<OtRepuestoConsumido?> GetByIdAsync(int id)
        {
            return await QueryConRelaciones()
                .FirstOrDefaultAsync(rp => rp.DetalleId == id);
        }

        public override async Task<IReadOnlyList<OtRepuestoConsumido>> GetAllAsync()
        {
            return await QueryConRelaciones().ToListAsync();
        }

        public async Task<IReadOnlyList<OtRepuestoConsumido>> GetByOtIdAsync(int otId)
        {
            return await QueryConRelaciones()
                .Where(rp => rp.OtId == otId)
                .ToListAsync();
        }

        public async Task<decimal> SumarCostoTotalByOtIdAsync(int otId)
        {
            return await _context.OtRepuestosConsumidos
                .Where(rp => rp.OtId == otId)
                .SumAsync(rp => rp.Cantidad * rp.PrecioUnitarioHistorico);
        }

        //para el grafico pie de los repuestos mas consumidos
        public async Task<IReadOnlyList<DashboardPieResponseDto>> GetTopConsumidosAsync(int top, DateTime? desde = null, DateTime? hasta = null)
        {
            var fechaDesde = desde ?? new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var fechaHasta = hasta ?? DateTime.UtcNow;

            return await _context.OtRepuestosConsumidos
                .Where(rp => rp.FechaDespacho >= fechaDesde && rp.FechaDespacho <= fechaHasta)
                .GroupBy(rp => new { rp.ProductoId, rp.Producto.CodigoParte, rp.Producto.Nombre })
                .Select(g => new DashboardPieResponseDto
                {
                    ProductoId = g.Key.ProductoId,
                    CodigoParte = g.Key.CodigoParte,
                    NombreProducto = g.Key.Nombre,
                    CantidadTotalConsumida = g.Sum(rp => rp.Cantidad)
                })
                .OrderByDescending(r => r.CantidadTotalConsumida)
                .Take(top)
                .ToListAsync();
        }



    }
}
