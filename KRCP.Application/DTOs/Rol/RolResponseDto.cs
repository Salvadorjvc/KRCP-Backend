using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Rol
{
    /// DTO de lectura utilizado para retornar la información de un Rol en las consultas HTTP GET.
    public class RolResponseDto
    {  
        public int RolId { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
