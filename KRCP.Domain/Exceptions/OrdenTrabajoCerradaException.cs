using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class OrdenTrabajoCerradaException : Exception
    {
        public OrdenTrabajoCerradaException(string codigoOT, string estadoActual)
            : base($"No se puede realizar esta operación porque la OT '{codigoOT}' está en estado '{estadoActual}'.")
        {
        }
    }
}
