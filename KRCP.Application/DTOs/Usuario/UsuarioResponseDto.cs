using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Usuario
{
    public class UsuarioResponseDto
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int RolId { get; set; }
        public string NombreRol { get; set; } = string.Empty; // dato "aplanado" de la relación con Rol
        public DateTime FechaCreacion { get; set; }     
        public DateTime? FechaModificacion { get; set; }

    }
}
