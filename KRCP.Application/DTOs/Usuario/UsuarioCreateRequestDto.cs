using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Usuario
{
    /// <summary>
    /// DTO de creacion utilizado para crear un nuevo Usuario a través de las consultas HTTP POST.
    /// </summary>
    public class UsuarioCreateRequestDto
    {
        public required string NombreCompleto { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int RolId { get; set; }
    }
}
