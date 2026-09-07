using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Cliente
{
    public class ClienteResponseDto
    {
        public int ClienteId { get; set; }
        public string RUC { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string? ContactoEmail { get; set; }
        public string? Telefono { get; set; } 
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
