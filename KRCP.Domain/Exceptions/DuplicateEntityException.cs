using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string entityName, string fieldName, string value)
          : base($"Ya existe {entityName} con {fieldName} '{value}'.")
        {
        }
    }
}
