using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IErpIntegracionLogRepository : IGenericRepository<ErpIntegracionLog>
    {
        Task<IReadOnlyList<ErpIntegracionLog>> GetByEntidadAsync(string entidadAfectada, int entidadId);
    }
}
