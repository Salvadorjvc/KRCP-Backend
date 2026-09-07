using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Categoria
{
    public class CategoriaCreateRequestDto
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
