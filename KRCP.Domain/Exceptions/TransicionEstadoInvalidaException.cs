using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class TransicionEstadoInvalidaException : Exception
    {
        public TransicionEstadoInvalidaException(string estadoActual)
            : base($"No se puede cambiar el estado de una OT que ya está en '{estadoActual}'.")
        {
        }
    }
}
