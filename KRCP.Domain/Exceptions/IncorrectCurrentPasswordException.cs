using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Domain.Exceptions
{
    public class IncorrectCurrentPasswordException : Exception
    {
        public IncorrectCurrentPasswordException()
            :base("La contraseña actual ingresada no es correcta")
        {

        }
    }
}
