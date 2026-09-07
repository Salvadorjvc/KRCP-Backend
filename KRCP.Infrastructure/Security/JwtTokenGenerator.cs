using KRCP.Application.Interfaces.Security;
using KRCP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KRCP.Infrastructure.Security
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            var KeyString = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("La clave secreta 'Jwt:Key' no esta configurada");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.NombreRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: GetExpirationTime(),
                signingCredentials: credentials
            );



            return new JwtSecurityTokenHandler().WriteToken(token);
        
        }

        public DateTime GetExpirationTime()
        {
            var expirationHours = int.TryParse(_configuration["Jwt:ExpirationInHours"], out var hours) ? hours : 8;
            return DateTime.UtcNow.AddHours(expirationHours);
        }

    }
}
