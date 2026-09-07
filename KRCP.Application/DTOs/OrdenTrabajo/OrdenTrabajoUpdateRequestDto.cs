using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OrdenTrabajo
{
    public class OrdenTrabajoUpdateRequestDto
    {
        public required string EquipoComponente { get; set; }
        public required string NumeroSerie { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public decimal CostoManoObra { get; set; }
        public string? Observaciones { get; set; }

    }
}
