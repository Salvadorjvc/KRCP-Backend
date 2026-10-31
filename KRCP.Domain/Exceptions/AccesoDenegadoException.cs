using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class AccesoDenegadoException : Exception
    {
        public AccesoDenegadoException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
