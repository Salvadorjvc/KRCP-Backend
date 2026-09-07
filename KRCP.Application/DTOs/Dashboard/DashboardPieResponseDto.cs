using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Dashboard
{
    public class DashboardPieResponseDto
    {
        public int ProductoId { get; set; } //por evaluar si es necesario el id
        public string CodigoParte { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int CantidadTotalConsumida { get; set; }
    }
}
