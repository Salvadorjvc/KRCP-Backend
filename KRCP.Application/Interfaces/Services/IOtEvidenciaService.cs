using KRCP.Application.DTOs.OtEvidencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IOtEvidenciaService
    {
        Task<OtEvidenciaResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<OtEvidenciaResponseDto>> GetByOtIdAsync(int otId);
        Task<OtEvidenciaResponseDto> CreateAsync(OtEvidenciaCreateRequestDto dto, int usuarioCargaId);
    }
}
