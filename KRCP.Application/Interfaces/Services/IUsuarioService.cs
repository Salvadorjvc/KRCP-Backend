using KRCP.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<UsuarioResponseDto>> GetAllAsync();
        Task<UsuarioResponseDto> CreateAsync(UsuarioCreateRequestDto dto);
        Task UpdateAsync(int id, UsuarioUpdateRequestDto dto);
        Task ChangePasswordAsync(int id, ChangePasswordRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);//cambiar estadicius jr
    }
}
