using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.OtRepuestoConsumido
{
    public class OtRepuestoConsumidoResponseDto
    {
        public int DetalleId { get; set; }

        public int OtId { get; set; }
        public string CodigoOt { get; set; } = string.Empty; //aplanado

        public int ProductoId { get; set; }
        public string CodigoParte { get; set; } = string.Empty; //aplanado
        public string NombreProducto { get; set; } = string.Empty; //aplanado

        public int Cantidad { get; set; } 
        public decimal PrecioUnitarioHistorico { get; set; }
        public decimal Subtotal { get; set; } // calculado, no viene de la BD directamente

        public int UsuarioAlmacenId { get; set; }
        public string NombreUsuarioAlmacen { get; set; } = string.Empty;

        public DateTime FechaDespacho { get; set; }

    }
}
