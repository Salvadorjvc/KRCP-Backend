using KRCP.Application.Interfaces.Repositories;
using KRCP.Domain.Entities;
using KRCP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Repositories
{
    public class UsuarioRepository: GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base (context)
        {
        }

        public override async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                        .Include(u => u.Rol)
                        .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public override async Task<IReadOnlyList<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync (string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }
    }
}
