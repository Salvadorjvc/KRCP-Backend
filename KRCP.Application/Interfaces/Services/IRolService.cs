using KRCP.Application.DTOs.Rol;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IRolService
    {
        Task<RolResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<RolResponseDto>> GetAllAsync();
        Task<RolResponseDto> CreateAsync(RolCreateRequestDto dto);
        Task UpdateAsync(int id,RolUpdateRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);//cambiar estado
    }
}
