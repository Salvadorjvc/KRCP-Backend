using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class StockInsuficienteException : Exception
    {
        public StockInsuficienteException(string codigoParte, int solicitado, int disponible)
           : base($"Stock insuficiente para '{codigoParte}'. Solicitado: {solicitado}, disponible: {disponible}.")
        {
        }
    }
}
