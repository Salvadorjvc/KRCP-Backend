using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Cliente
{
    /* el RUC no se pone en el update pq es el identificador legal/tributario de la empresa cliente,
     * y normalmente no debería poder cambiarse
    */
    public class ClienteUpdateRequestDto
    {
        public required string RazonSocial { get; set; }
        public string? ContactoEmail { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }

    }
}
