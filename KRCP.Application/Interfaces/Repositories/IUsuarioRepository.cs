using System;
using System.Collections.Generic;
using System.Text;
using KRCP.Domain.Entities;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository: IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
