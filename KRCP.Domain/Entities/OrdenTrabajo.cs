using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class OrdenTrabajo
    {
        public int OtId { get; set; }
        public required string CodigoOT { get; set; }
        public int ClienteId { get; set; }
        public required string EquipoComponente { get; set; }
        public required string NumeroSerie { get; set; }
        public int UsuarioPlanificadorId { get; set; }
        public int? TecnicoAsignadoId { get; set; }
        public EstadoOrdenTrabajo Estado { get; set; } = EstadoOrdenTrabajo.Registrado;

        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
        public DateTime? FechaEstimadaEntrega { get; set; }
        public DateTime? FechaCierre { get; set; }

        public decimal CostoManoObra { get; set; }
        public decimal CostoRepuestos { get; set; }
        public decimal CostoTotal { get; set; }

        public string? Observaciones { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacionId { get; set; }

        //relaciones fk
        public Cliente Cliente { get; set; } = null!;
        public Usuario UsuarioPlanificador { get; set; } = null!;
        public Usuario? TecnicoAsignado { get; set; }
        public Usuario? UsuarioModificacion { get; set; }

        //metodo para calcular el costo total
        public void CalcularCostoTotal()
        {
            CostoTotal = CostoManoObra + CostoRepuestos;
        }
    }
}
