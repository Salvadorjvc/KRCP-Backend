using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IOtEvidenciaRepository: IGenericRepository<OtEvidencia>
    {
        Task<IReadOnlyList<OtEvidencia>> GetByOtIdAsync(int otId);
    }
}
