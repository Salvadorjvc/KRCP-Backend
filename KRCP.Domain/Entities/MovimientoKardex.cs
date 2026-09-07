using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Entities
{
    // el kardex en si es un registro de movimientos de inventario,
    // por lo que cada movimiento se registra en esta entidad
    public class MovimientoKardex
    { 
        public int KardexId { get; set; }
        public int ProductoId { get; set; }
        public int UsuarioId { get; set; }
        public int? OtId { get; set; }

        public required TipoMovimientoKardex TipoMovimiento { get; set; }
        public int Cantidad { get; set; }
        public int StockAnterior { get; set; }
        public int StockNuevo { get; set; }

        public string? Motivo { get; set; }
        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;

        //foreign keys
        public Producto Producto { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;

        // Anulable porque una salida/ajuste de Kardex no siempre está ligada a una Orden de Trabajo
        public OrdenTrabajo? OrdenTrabajo { get; set; }


        //metodo para calcular el stock nuevo dependiendo del enum
        public void CalcularStockNuevo()
        {
            StockNuevo = TipoMovimiento switch
            {
                TipoMovimientoKardex.Entrada => StockAnterior + Cantidad,
                TipoMovimientoKardex.Devolucion => StockAnterior + Cantidad,
                TipoMovimientoKardex.Salida => StockAnterior - Cantidad,
                TipoMovimientoKardex.Ajuste => Cantidad, // el ajuste reemplaza el stock directamente
                _ => throw new InvalidOperationException("Tipo de movimiento no reconocido")
            };
        }

    }
}
