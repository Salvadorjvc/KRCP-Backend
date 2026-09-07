using KRCP.Application.DTOs.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<CategoriaResponseDto>> GetAllAsync();
        Task<CategoriaResponseDto> CreateAsync(CategoriaCreateRequestDto dto);
        Task UpdateAsync(int id, CategoriaUpdateRequestDto dto);
        Task ChangeStatusAsync(int id, bool activo);
    }
}
