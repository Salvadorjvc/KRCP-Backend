using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OtEvidencia
{
    public class OtEvidenciaResponseDto
    {
        public int EvidenciaId { get; set; }
        public int OtId { get; set; }
        public string CodigoOT { get; set; } = string.Empty; // aplanado
        public string TipoEvidencia { get; set; } = string.Empty;
        public string UrlArchivo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public int UsuarioCargaId { get; set; }
        public string UsuarioCarga { get; set; } = string.Empty; //aplanado

        public DateTime FechaCarga { get; set; }
    }
}
