using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Rol
{
    /// DTO de creación utilizado para crear un nuevo Rol a través de las consultas HTTP POST.
    public class RolCreateRequestDto
    {
        public required string NombreRol { get; set; }
    }
}
