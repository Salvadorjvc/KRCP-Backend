using KRCP.Application.DTOs.MovimientoKardex;
using KRCP.Application.DTOs.OtRepuestoConsumido;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IExcelGenerator
    {
        byte[] GenerarKardexMensual(IReadOnlyList<MovimientoKardexResponseDto> data);
        byte[] GenerarComponentesEntregados(IReadOnlyList<OtRepuestoConsumidoResponseDto> data);
    }
}
