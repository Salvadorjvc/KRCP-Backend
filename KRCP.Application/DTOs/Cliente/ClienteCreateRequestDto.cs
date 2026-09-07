using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Cliente
{
    public class ClienteCreateRequestDto
    {
        public required string RUC { get; set; } 
        public required string RazonSocial { get; set; }
        public string? ContactoEmail { get; set; } 
        public string? Telefono { get; set; } 
    }
}
