using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public required string RUC { get; set; }
        public required string RazonSocial { get; set; }
        public string? ContactoEmail { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; } = true;
        // UTCNow es el tiempo universal en sí, no toma la hora local del servidor/PC
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }

        // Relaciones de 1 a muchos con OrdenTrabajo
        public ICollection<OrdenTrabajo> OrdenesTrabajo { get; set; } = new List<OrdenTrabajo>();

    }
}
