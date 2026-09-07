using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class ErpIntegracionLogRepository : GenericRepository<ErpIntegracionLog>, IErpIntegracionLogRepository
    {
        public ErpIntegracionLogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<ErpIntegracionLog>> GetByEntidadAsync(string entidadAfectada, int entidadId)
        {
            return await _context.ErpIntegracionLogs
                .Where(l => l.EntidadAfectada == entidadAfectada && l.EntidadId == entidadId)
                .ToListAsync();
        }
    }
}
