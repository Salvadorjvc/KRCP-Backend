using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Usuario usuario);
        DateTime GetExpirationTime();
    }
}
