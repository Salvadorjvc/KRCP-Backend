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
        public required int UsuarioPlanificadorId { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public string? Observaciones { get; set; }

    }
}
