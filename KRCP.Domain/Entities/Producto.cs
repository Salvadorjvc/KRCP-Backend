using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public required string CodigoParte { get; set; }
        public required string Nombre { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal CostoUnitario { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }


        //foreign keys
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public int? UbicacionId { get; set; }
        public Ubicacion? Ubicacion { get; set; }

        public int? UsuarioModificacionId { get; set; }
        public Usuario? UsuarioModificacion { get; set; }
        

    }
}
