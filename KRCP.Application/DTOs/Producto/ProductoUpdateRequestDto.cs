using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Producto
{
    //no se incluye "CodigoParte" debido a que es al que no deberia cambiar una vez ya creado
    // y tampoco se incluye el "stockActual" por la misma razon que en create
    public class ProductoUpdateRequestDto
    {
        public required string Nombre { get; set; }
        public required int CategoriaId { get; set; }
        public int? UbicacionId { get; set; }
        public int StockMinimo { get; set;  }
        public required decimal CostoUnitario { get; set; }
        public bool Activo { get; set; }
    }
}
