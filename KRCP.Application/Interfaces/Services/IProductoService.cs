using KRCP.Application.DTOs.Producto;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IProductoService
    {
        Task<ProductoResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductoResponseDto>> GetAllAsync();
        Task<IReadOnlyList<ProductoResponseDto>> GetStockBajoAsync();
        Task<ProductoResponseDto> CreateAsync(ProductoCreateRequestDto dto);
        Task UpdateAsync(int id, ProductoUpdateRequestDto dto, int usuarioModificacionId);
        Task ChangeStatusAsync(int id, bool activo);
        Task AjustarStockAsync(int productoId, int cantidad, TipoMovimientoKardex tipoMovimiento);
    }
}
