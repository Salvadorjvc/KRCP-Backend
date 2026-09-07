using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Usuario
{
    public class UsuarioUpdateRequestDto
    {
        public required string NombreCompleto { get; set; }
        public required string Email { get; set; }
        public required int RolId { get; set; }
        public bool Activo { get; set; }
    }
}
