using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class OrdenTrabajoRepository: GenericRepository<OrdenTrabajo>, IOrdenTrabajoRepository
    {
        public OrdenTrabajoRepository(ApplicationDbContext context) : base(context)
        { }

        // una query no inicializada para no repetir los includes en cada metodo
        private IQueryable<OrdenTrabajo> QueryConRelaciones()
        {
            return _context.OrdenesTrabajo
                .Include(o => o.Cliente)
                .Include(o => o.UsuarioPlanificador)
                .Include(o => o.TecnicoAsignado)
                .Include(o => o.UsuarioModificacion);
        }

        public override async Task<OrdenTrabajo?> GetByIdAsync(int id)
        {
            return await QueryConRelaciones()
                .FirstOrDefaultAsync(o => o.OtId == id);
        }

        public override async Task<IReadOnlyList<OrdenTrabajo>> GetAllAsync()
        {
            return await QueryConRelaciones()
                .ToListAsync();
        }

        public async Task<bool> ExistsByCodigoOTAsync(string codigoOT)
        {
            return await _context.OrdenesTrabajo.AnyAsync(o => o.CodigoOT == codigoOT);
        }

        public async Task<int> GetCountByYearAsync(int year)
        {
            return await _context.OrdenesTrabajo.CountAsync(o => o.FechaIngreso.Year == year);
        }

        public async Task<IReadOnlyList<OrdenTrabajo>> GetByTecnicoAsignadoAsync(int tecnicoId)
        {
            return await QueryConRelaciones()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<OrdenTrabajo>> GetByEstadoAsync(EstadoOrdenTrabajo estado)
        {
            return await QueryConRelaciones()
                .ToListAsync();
        }
    }
}
