using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object key)
            : base($"No se encontró '{entityName}' con ID '{key}'")
        {
        }
    }
}
