using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class OtEvidenciaRepository: GenericRepository<OtEvidencia>, IOtEvidenciaRepository
    {
        public OtEvidenciaRepository(ApplicationDbContext context): base(context)
        { }

        private IQueryable<OtEvidencia> QueryConRelaciones()
        {
            return _context.OtEvidencias
                .Include(e => e.OrdenTrabajo)
                .Include(e => e.UsuarioCarga);
        }

        public override async Task<OtEvidencia?> GetByIdAsync(int id)
        {
            return await QueryConRelaciones()
                .FirstOrDefaultAsync(e => e.EvidenciaId == id);
        }

        public override async Task<IReadOnlyList<OtEvidencia>> GetAllAsync()
        {
            return await QueryConRelaciones().ToListAsync();
        }

        public async Task<IReadOnlyList<OtEvidencia>> GetByOtIdAsync(int otId)
        {
            return await QueryConRelaciones()
                .Where(e => e.OtId == otId)
                .ToListAsync();
        }
    }
}
