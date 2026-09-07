using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.MovimientoKardex
{
    public class MovimientoKardexCreateRequestDto
    {
        public required int ProductoId { get; set; }
        public required TipoMovimientoKardex TipoMovimiento { get; set; }
        public required int Cantidad { get; set; }
        public string? Motivo { get; set; }
    }
}
