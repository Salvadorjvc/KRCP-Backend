using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Categoria
{
    public class CategoriaUpdateRequestDto
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
