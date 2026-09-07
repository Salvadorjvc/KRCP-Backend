using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class RolRepository: GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string NombreRol)
        {
            return await _context.Roles.AnyAsync(r => r.NombreRol == NombreRol);
        }
    }
}
