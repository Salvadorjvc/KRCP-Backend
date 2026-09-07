using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    public class OtRepuestoConsumido
    {
        public int DetalleId { get; set; }
        public int OtId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitarioHistorico { get; set; }
        public int UsuarioAlmacenId { get; set; }
        public DateTime FechaDespacho { get; set; } = DateTime.UtcNow;

        //foreign keys
        public OrdenTrabajo OrdenTrabajo { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
        public Usuario UsuarioAlmacen { get; set; } = null!;
    }
}
