using KRCP.Application.DTOs.Ubicacion;
using KRCP.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IUbicacionService
    {
        Task<UbicacionResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<UbicacionResponseDto>> GetAllAsync();
        Task<UbicacionResponseDto> CreateAsync(UbicacionCreateRequestDto dto);
        Task UpdateAsync(int id, UbicacionUpdateRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);
    }
}
