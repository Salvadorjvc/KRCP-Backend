using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IUbicacionRepository: IGenericRepository<Ubicacion>
    {
        Task<bool> ExistsByCodigoAsync(string codigoUbicacion);
    }
}
