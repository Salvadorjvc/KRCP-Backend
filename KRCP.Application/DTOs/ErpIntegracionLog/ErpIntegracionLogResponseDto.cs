using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.ErpIntegracionLog
{
    public class ErpIntegracionLogResponseDto
    {
        public int LogId { get; set; }
        public string EntidadAfectada { get; set; } = string.Empty;
        public int EntidadId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? PayloadJson { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
