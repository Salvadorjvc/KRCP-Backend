using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Ubicacion
{
    public class UbicacionResponseDto
    {
        public int UbicacionId { get; set; }
        public string CodigoUbicacion { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
