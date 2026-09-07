using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    /*
    1: Método para verificar si un rol con el mismo nombre ya existe
    */
    public interface IRolRepository : IGenericRepository<Rol>
    {
      Task<bool> ExistsByNameAsync(string NombreRol); 
    }
}
