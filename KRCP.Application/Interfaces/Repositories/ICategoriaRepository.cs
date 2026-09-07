using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    /*
   1: Método para verificar si una categoria con el mismo nombre ya existe
   */
    public interface ICategoriaRepository: IGenericRepository<Categoria>
    {
        Task<bool> ExistsByNameAsync(string NombreCategoria);
    }
}
