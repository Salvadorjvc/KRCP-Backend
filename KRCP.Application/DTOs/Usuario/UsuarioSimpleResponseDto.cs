using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Usuario
{
    public class UsuarioSimpleResponseDto
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
