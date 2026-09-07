using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Usuario
{
    public class ChangePasswordRequestDto
    {
        public required string PasswordActual { get; set; }  // verificación de seguridad
        public required string PasswordNueva { get; set; }
    }
}
