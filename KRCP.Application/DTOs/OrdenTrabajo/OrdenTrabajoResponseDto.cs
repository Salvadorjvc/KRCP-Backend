using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OrdenTrabajo
{
    public class OrdenTrabajoResponseDto
    {
        public int OtId { get; set; }
        public string CodigoOt { get; set; } = string.Empty;

        //fk cliente
        public int ClienteId { get; set; }
        public string RazonSocialCliente { get; set; } = string.Empty; // aplanado

        public string EquipoComponente { get; set;  } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;

        //fk usuario
        public int UsuarioPlanificadorId { get; set; }
        public string NombrePlanificador { get; set; } = string.Empty; // aplanado

        public int? TecnicoAsignadoId { get; set; }
        public string? NombreTecnico { get; set; } = string.Empty; //aplando, opcional

        public string Estado { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEstimadaEntrega{ get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal CostoManoObra { get; set; }
        public decimal CostoRepuestos { get; set; }
        public decimal CostoTotal { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int? UsuarioModificacionId { get; set; }
        public string? NombreUsuarioModificacion { get; set; } //aplanado, opcional

    }
}
