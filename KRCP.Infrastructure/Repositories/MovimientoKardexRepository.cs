using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class MovimientoKardexRepository: GenericRepository<MovimientoKardex>, IMovimientoKardexRepository
    {
        public MovimientoKardexRepository(ApplicationDbContext context) : base(context)
        { }

        private IQueryable<MovimientoKardex> QueryConRelaciones()
        {
            return _context.MovimientosKardex
                .Include(k => k.Producto)
                .Include(k => k.Usuario)
                .Include(k => k.OrdenTrabajo);
        }

        public override async Task<MovimientoKardex?> GetByIdAsync(int id)
        {
            return await QueryConRelaciones().FirstOrDefaultAsync(k => k.KardexId == id);
        }

        public override async Task<IReadOnlyList<MovimientoKardex>> GetAllAsync()
        {
            return await QueryConRelaciones().ToListAsync();
        }

        public async Task<IReadOnlyList<MovimientoKardex>> GetByProductoIdAsync(int productoId)
        {
            return await QueryConRelaciones()
                .Where(k => k.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovimientoKardex>> GetByOtIdAsync(int otId)
        {
            return await QueryConRelaciones()
                .Where(k => k.OtId == otId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovimientoKardex>> GetByMesAnioAsync(int mes, int anio)
        {
            return await QueryConRelaciones()
                .Where(k => k.FechaMovimiento.Month == mes && k.FechaMovimiento.Year == anio)
                .ToListAsync();
        }
    }
}
