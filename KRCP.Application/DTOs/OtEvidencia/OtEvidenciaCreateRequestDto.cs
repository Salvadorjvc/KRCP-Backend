using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OtEvidencia
{
    public class OtEvidenciaCreateRequestDto
    {
        public required int OtId { get; set; }
        public required TipoEvidenciaOT TipoEvidencia { get; set; }
        public required string UrlArchivo { get; set; }
        public string? Descripcion { get; set; }

    }
}
