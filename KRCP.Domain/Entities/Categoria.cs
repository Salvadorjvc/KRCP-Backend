using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
