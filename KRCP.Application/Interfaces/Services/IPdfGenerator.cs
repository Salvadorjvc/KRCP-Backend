using KRCP.Application.DTOs.OrdenTrabajo;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IPdfGenerator
    {
        byte[] GenerarLiquidacionOt(OrdenTrabajoResponseDto data);
    }
}
