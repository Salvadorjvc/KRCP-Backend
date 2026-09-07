using KRCP.Application.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<ClienteResponseDto>> GetAllAsync();
        Task<ClienteResponseDto> CreateAsync(ClienteCreateRequestDto dto);
        Task UpdateAsync(int id, ClienteUpdateRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);//cambiar estado
    }
}
