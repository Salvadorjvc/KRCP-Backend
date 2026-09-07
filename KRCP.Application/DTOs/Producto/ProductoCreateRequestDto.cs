using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Producto
{
    // El stock actual no se incluye aquí porque se gestionará a través de movimientos de Kardex (Entrada).
    public class ProductoCreateRequestDto
    {
        public required string CodigoParte { get; set; }
        public required string Nombre { get; set; }
        public required int CategoriaId { get; set; }
        public int? UbicacionId { get; set; }
        public int StockMinimo { get; set; } = 5; // default de negocio, igual que la tabla
        public decimal CostoUnitario { get; set; }

    }
}
