using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.MovimientoKardex
{
    public class MovimientoKardexResponseDto
    {
        public int KardexId { get; set; }

        public int ProductoId { get; set; }
        public string CodigoParte { get; set; } = string.Empty; // aplanado
        public string NombreProducto { get; set; } = string.Empty; // aplanado

        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty; // aplanado

        public int? OtId { get; set; }
        public string? CodigoOT { get; set; } // aplanado, opcional
        public string TipoMovimiento { get; set; } = string.Empty; // enum
        public int Cantidad { get; set; }
        public int StockAnterior { get; set; }
        public int StockNuevo { get; set; }
        public string? Motivo { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
