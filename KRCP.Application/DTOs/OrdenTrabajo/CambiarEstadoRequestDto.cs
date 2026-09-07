using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OrdenTrabajo
{
    public class CambiarEstadoRequestDto
    {
        public required EstadoOrdenTrabajo NuevoEstado { get; set; }
    }
}
