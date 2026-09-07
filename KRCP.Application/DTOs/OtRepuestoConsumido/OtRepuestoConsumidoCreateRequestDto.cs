using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OtRepuestoConsumido
{
    public class OtRepuestoConsumidoCreateRequestDto
    {
        public required int OtId { get; set; }
        public required int ProductoId { get; set; }
        public required int Cantidad { get; set;  }
    }
}
