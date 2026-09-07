using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Ubicacion
{
    public class UbicacionUpdateRequestDto
    {
        public required string CodigoUbicacion { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
