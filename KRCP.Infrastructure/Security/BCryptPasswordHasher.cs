using BCrypt.Net;
using KRCP.Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Security
{
    //notita anashex: el enhanced hash es más seguro que el hash normal, ya que utiliza un algoritmo de hashing más fuerte
    //y agrega un salt aleatorio a cada contraseña antes de hashearla, lo que hace que sea más difícil
    //para los atacantes descifrar las contraseñas.
    public class BCryptPasswordHasher: IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
