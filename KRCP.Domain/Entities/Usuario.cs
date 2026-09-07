using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required string NombreCompleto { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }

        //objeto para la relación con Rol (fk)
        public Rol Rol { get; set; } = null!;

    }
}
