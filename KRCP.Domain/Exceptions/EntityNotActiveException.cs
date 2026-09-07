using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class EntityNotActiveException: Exception
    {
        public EntityNotActiveException(string entityName, string identificador)
            : base($"{entityName} '{identificador}' está inactivo y no puede utilizarse en esta operación.")
        {
        }
    }
}
