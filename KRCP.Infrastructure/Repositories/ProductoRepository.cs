using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class ProductoRepository: GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(ApplicationDbContext context) : base(context)
        { }

        private IQueryable<Producto> QueryConRelaciones()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Ubicacion)
                .Include(p => p.UsuarioModificacion);
        }

        public override async Task<Producto?> GetByIdAsync(int id)
        {
            return await QueryConRelaciones()
                .FirstOrDefaultAsync(p => p.ProductoId == id);
        }

        public override async Task<IReadOnlyList<Producto>> GetAllAsync()
        {
            return await QueryConRelaciones()
                .ToListAsync();
        }

        public async Task<bool> ExistsByCodigoParteAsync(string codigoParte)
        {
            return await _context.Productos.AnyAsync(p => p.CodigoParte == codigoParte);
        }

        //analizar el uso de .AsNoTracking() para mejorar el rendimiento en consultas de solo lectura
        public async Task<IReadOnlyList<Producto>> GetStockBajoAsync()
        {
            return await QueryConRelaciones()
                .Where(p => p.StockActual <= p.StockMinimo && p.Activo)
                .ToListAsync();
        }
    }
}
