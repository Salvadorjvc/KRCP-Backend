using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Ubicacion
{
    public class UbicacionCreateRequestDto
    {
        public required string CodigoUbicacion { get; set; }
        public string? Descripcion { get; set; }
    }
}
