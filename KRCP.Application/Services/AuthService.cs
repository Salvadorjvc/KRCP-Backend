using KRCP.Application.DTOs.Auth;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Security;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (usuario is null)
                throw new InvalidCredentialsException();

            if (!usuario.Activo)
                throw new EntityNotActiveException("El usuario", usuario.NombreCompleto);

            var passwordValida = _passwordHasher.Verify(dto.Password, usuario.PasswordHash);
            if (!passwordValida)
                throw new InvalidCredentialsException();

            var token = _jwtTokenGenerator.GenerateToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                ExpiraEn = _jwtTokenGenerator.GetExpirationTime(),
                UsuarioId = usuario.UsuarioId,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                NombreRol = usuario.Rol.NombreRol
            };
        }
    }
}
