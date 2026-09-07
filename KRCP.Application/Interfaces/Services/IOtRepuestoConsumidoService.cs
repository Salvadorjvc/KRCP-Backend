using KRCP.Application.DTOs.OtRepuestoConsumido;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IOtRepuestoConsumidoService
    {
        Task<OtRepuestoConsumidoResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<OtRepuestoConsumidoResponseDto>> GetByOtIdAsync(int otId);
        Task<OtRepuestoConsumidoResponseDto> DespacharAsync(OtRepuestoConsumidoCreateRequestDto dto, int usuarioAlmacenId);
    }
}
