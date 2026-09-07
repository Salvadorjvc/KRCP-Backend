using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Producto
{
    public class ProductoResponseDto
    {
        public int ProductoId { get; set; }
        public string CodigoParte { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal CostoUnitario { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set;  }

        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; } = string.Empty; // aplanado de la relación

        public int? UbicacionId { get; set; }
        public string? CodigoUbicacion { get; set; } // aplanado, opcional porque UbicacionId es nullable


        // Nombre de la persona que hizo el último cambio(fk Usuario)
        public string? UsuarioModificacion { get; set; }


    }
}
