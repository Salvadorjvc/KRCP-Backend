using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class UbicacionRepository: GenericRepository<Ubicacion>, IUbicacionRepository
    {
        public UbicacionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByCodigoAsync( string codigoUbicacion)
        {
            return await _context.Ubicaciones.AnyAsync(u => u.CodigoUbicacion == codigoUbicacion);
        }
    }
}
