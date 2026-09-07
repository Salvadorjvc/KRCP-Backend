using KRCP.Application.DTOs.Usuario;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Security;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Entities;
using KRCP.Domain.Exceptions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPasswordHasher _passwordHasher;


        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioResponseDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", id);
            }

            return usuario.Adapt<UsuarioResponseDto>();
        }

        public async Task<IReadOnlyList<UsuarioResponseDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Adapt<List<UsuarioResponseDto>>();
        }

        public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateRequestDto dto)
        {
            await ValidarEmailUnicoAsync(dto.Email);
            await ValidarRolExisteAsync(dto.RolId);

            var usuario = dto.Adapt<Usuario>();
            usuario.PasswordHash = _passwordHasher.Hash(dto.Password);

            await _usuarioRepository.AddAsync(usuario);
            return usuario.Adapt<UsuarioResponseDto>();
        }

        public async Task UpdateAsync(int id, UsuarioUpdateRequestDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", id);
            }

            if (!usuario.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                await ValidarEmailUnicoAsync(dto.Email);
            }

            if (usuario.RolId != dto.RolId)
            {
                await ValidarRolExisteAsync(dto.RolId);
            }

            dto.Adapt(usuario);
            usuario.FechaModificacion = DateTime.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
        }
        public async Task ChangePasswordAsync(int id, ChangePasswordRequestDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", id);
            }

            // Verifica que la contraseña actual sea correcta antes de permitir el cambio
            var passwordValida = _passwordHasher.Verify(dto.PasswordActual, usuario.PasswordHash);
            if (!passwordValida)
            {
                throw new InvalidCredentialsException();
            }

            usuario.PasswordHash = _passwordHasher.Hash(dto.PasswordNueva);
            usuario.FechaModificacion = DateTime.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", id);
            }

            usuario.Activo = activo;
            usuario.FechaModificacion = DateTime.UtcNow;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        private async Task ValidarEmailUnicoAsync(string email)
        {
            var existeEmail = await _usuarioRepository.ExistsByEmailAsync(email);
            if (existeEmail)
            {
                throw new DuplicateEntityException("un Usuario", "el email", email);
            }
        }

        private async Task ValidarRolExisteAsync(int rolId)
        {
            var rol = await _rolRepository.GetByIdAsync(rolId);
            if(rol is null)
            {
                throw new EntityNotFoundException("Rol", rolId);
            }
        }
    }
}
