using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public DateTime ExpiraEn { get; set; }
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;
    }
}
