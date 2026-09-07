using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Rol
{
    /// DTO de actualización utilizado para actualizar un Rol existente a través de las consultas HTTP PUT o PATCH.
    public class RolUpdateRequestDto
    {
        public required string NombreRol { get; set; }
        public bool Activo { get; set; }
    }
}
