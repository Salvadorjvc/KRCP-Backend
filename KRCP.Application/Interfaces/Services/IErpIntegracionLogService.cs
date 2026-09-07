using KRCP.Application.DTOs.ErpIntegracionLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IErpIntegracionLogService
    {
        Task<IReadOnlyList<ErpIntegracionLogResponseDto>> GetByEntidadAsync(string entidadAfectada, int entidadId);
        Task RegistrarAsync(string entidadAfectada, int entidadId, string accion, string? payloadJson);
    }
}
