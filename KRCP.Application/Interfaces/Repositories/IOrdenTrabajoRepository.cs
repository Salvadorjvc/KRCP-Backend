using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IOrdenTrabajoRepository: IGenericRepository<OrdenTrabajo>
    {
        Task<bool> ExistsByCodigoOTAsync(string codigoOT);
        Task<int> GetCountByYearAsync(int year); // para generar el correlativo del código
        Task<IReadOnlyList<OrdenTrabajo>> GetByTecnicoAsignadoAsync(int tecnicoId);
        Task<IReadOnlyList<OrdenTrabajo>> GetByEstadoAsync(EstadoOrdenTrabajo estado);
    }
}
