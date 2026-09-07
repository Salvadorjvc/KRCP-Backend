using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Rol
    {
        public int RolId { get; set; }
        public required string NombreRol { get; set; }
        public bool Activo { get; set; } = true;

        // ICollection:Propiedad de navegación para la relación uno a muchos con Usuario
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
