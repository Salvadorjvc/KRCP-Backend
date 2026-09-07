using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class ClienteRepository: GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByRucAsync (string ruc)
        {
            return await _context.Clientes.AnyAsync(c => c.RUC == ruc);
        }
    }
}
