using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    // y esto e basicamente un repository generico para usar en cualquier entidad
    //que tiene metodos basico para no repetir codigo demas
    public interface IGenericRepository<T> where T: class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
