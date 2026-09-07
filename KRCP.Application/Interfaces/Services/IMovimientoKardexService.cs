using KRCP.Application.DTOs.MovimientoKardex;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IMovimientoKardexService
    {
        Task<MovimientoKardexResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<MovimientoKardexResponseDto>> GetByProductoIdAsync(int productoId);
        Task<IReadOnlyList<MovimientoKardexResponseDto>> GetByOtIdAsync(int otId);
        Task<MovimientoKardexResponseDto> RegistrarMovimientoKardexAsync(MovimientoKardexCreateRequestDto dto, int usuarioId);
    }
}
