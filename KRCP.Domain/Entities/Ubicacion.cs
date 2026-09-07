using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        public required string CodigoUbicacion { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        // Relación 1 a muchos con Productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}

