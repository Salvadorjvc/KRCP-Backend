using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class ErpIntegracionLog
    {
        public int LogId { get; set; }
        public required string EntidadAfectada { get; set; }
        public int EntidadId { get; set; }
        public required string Accion { get; set; }

        // Cadena JSON con la estructura completa de datos enviada o recibida del ERP (SAP/Oracle)
        public string? PayloadJson { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
