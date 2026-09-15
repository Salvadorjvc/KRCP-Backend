using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IMovimientoKardexRepository: IGenericRepository<MovimientoKardex>
    {
        Task<IReadOnlyList<MovimientoKardex>> GetByProductoIdAsync(int productoId);
        Task<IReadOnlyList<MovimientoKardex>> GetByOtIdAsync(int otId);
        Task<IReadOnlyList<MovimientoKardex>> GetByMesAnioAsync(int mes, int anio); // para los reportes
    }
}
