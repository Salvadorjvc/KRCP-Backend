using KRCP.Application.DTOs.ErpIntegracionLog;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class ErpIntegracionLogService : IErpIntegracionLogService
    {
        private readonly IErpIntegracionLogRepository _logRepository;

        public ErpIntegracionLogService(IErpIntegracionLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IReadOnlyList<ErpIntegracionLogResponseDto>> GetByEntidadAsync(string entidadAfectada, int entidadId)
        {
            var logs = await _logRepository.GetByEntidadAsync(entidadAfectada, entidadId);
            return logs.Adapt<List<ErpIntegracionLogResponseDto>>();
        }

        public async Task RegistrarAsync(string entidadAfectada, int entidadId, string accion, string? payloadJson)
        {
            var log = new ErpIntegracionLog
            {
                EntidadAfectada = entidadAfectada,
                EntidadId = entidadId,
                Accion = accion,
                PayloadJson = payloadJson
            };

            await _logRepository.AddAsync(log);
        }
    }
}
