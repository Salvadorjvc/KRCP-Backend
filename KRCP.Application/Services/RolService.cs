using KRCP.Application.DTOs.Rol;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Exceptions;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService (IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<RolResponseDto> GetByIdAsync(int id)
        {
            var rol = await _rolRepository.GetByIdAsync(id);

            if(rol is null)
            {
                throw new EntityNotFoundException("Rol", id);
            }

            return rol.Adapt<RolResponseDto>();
        }

        public async Task<IReadOnlyList<RolResponseDto>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            return roles.Adapt<List<RolResponseDto>>();
        }

        public async Task<RolResponseDto> CreateAsync(RolCreateRequestDto dto)
        {
            //Validacion de negocio:Asegura que la entidad cumpla las reglas previas
            await ValidarNombreUnicoAsync(dto.NombreRol);

            //Asignación de Valores:Crea el nuevo objeto Entidad a partir de los datos del DTO
            var rol = dto.Adapt<Rol>();

            //Persistencia: Inserta la nueva fila en la base de datos
            await _rolRepository.AddAsync(rol);

            //Convierte la entidad creada a un DTO de respuesta seguro
            return rol.Adapt<RolResponseDto>();
        }

        public async Task UpdateAsync(int id, RolUpdateRequestDto dto)
        {
            //Busca si la entidad existe basicamente acho
            var rol = await _rolRepository.GetByIdAsync(id);
            if(rol is null)
            {
                throw new EntityNotFoundException("Rol", id);
            }

            //validar si ya existe otro rol con el mismo nombre, ignorando mayúsculas y minúsculas
            if (!rol.NombreRol.Equals(dto.NombreRol, StringComparison.OrdinalIgnoreCase))
            {
                await ValidarNombreUnicoAsync(dto.NombreRol);
            }

            //Asignación de Valores: Copia los cambios del DTO hacia la entidad en memoria RAM
            dto.Adapt(rol);

            //Confirma y guarda los cambios en SQL Server
            await _rolRepository.UpdateAsync(rol);
        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var rol = await _rolRepository.GetByIdAsync(id);

            if (rol is null)
            {
                throw new EntityNotFoundException("Rol", id);
            }

            rol.Activo = activo;

            await _rolRepository.UpdateAsync(rol);
        }

        //validar si nombre unico
        private async Task ValidarNombreUnicoAsync(string nombreRol)
        {
            var existeNombre = await _rolRepository.ExistsByNameAsync(nombreRol);
            if (existeNombre)
                throw new DuplicateEntityException("un Rol", "el nombre", nombreRol);
        }
    }
}
