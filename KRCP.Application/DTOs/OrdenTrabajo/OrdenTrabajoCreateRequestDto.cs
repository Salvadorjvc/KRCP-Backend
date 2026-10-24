using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OrdenTrabajo
{
    public class OrdenTrabajoCreateRequestDto
    {
        public required int ClienteId { get; set; }
        public required string EquipoComponente { get; set; }
        public required string NumeroSerie { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public decimal CostoManoObra { get; set; } = 0; //arranca con 0 de default
        public string? Observaciones { get; set; }

    }
}
