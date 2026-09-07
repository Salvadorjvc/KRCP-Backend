using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    /*
    1: Método para verificar si un rol con el mismo nombre ya existe
    */
    public interface IClienteRepository: IGenericRepository<Cliente>
    {
        Task<bool> ExistsByRucAsync(string ruc);
    }
}
