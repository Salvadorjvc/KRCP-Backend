using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Categoria
{
    public class CategoriaResponseDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
