using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IOrdenTrabajoService
    {
        Task<OrdenTrabajoResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetAllAsync();
        Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetByTecnicoAsync(int tecnicoId);
        Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetByEstadoAsync(EstadoOrdenTrabajo estado);
        Task<OrdenTrabajoResponseDto> CreateAsync(OrdenTrabajoCreateRequestDto dto);
        Task UpdateAsync(int id, OrdenTrabajoUpdateRequestDto dto, int usuarioModificacionId);
        Task AsignarTecnicoAsync(int otId, AsignarTecnicoRequestDto dto);
        Task CambiarEstadoAsync(int otId, CambiarEstadoRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);
    }
}
