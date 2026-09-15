using KRCP.Application.DTOs.MovimientoKardex;
using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.DTOs.OtRepuestoConsumido;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IReporteService
    {
        Task<OrdenTrabajoResponseDto> GetDataLiquidacionOtAsync(int otId);
        Task<IReadOnlyList<MovimientoKardexResponseDto>> GetDataKardexMensualAsync(int mes, int anio);
        Task<IReadOnlyList<OtRepuestoConsumidoResponseDto>> GetDataComponentesEntregadosAsync(int otId);
    }
}
