using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class OtEvidencia
    {
        public int EvidenciaId { get; set; }
        public int OtId { get; set; }
        public required TipoEvidenciaOT TipoEvidencia { get; set; }
        public required string UrlArchivo { get; set; }
        public string? Descripcion { get; set; }
        public int UsuarioCargaId { get; set; }
        public DateTime FechaCarga { get; set; } = DateTime.UtcNow;

        //foreign keys
        public OrdenTrabajo OrdenTrabajo { get; set; } = null!;
        public Usuario UsuarioCarga { get; set; } = null!;

    }
}
