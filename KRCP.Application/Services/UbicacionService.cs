using KRCP.Application.DTOs.Ubicacion;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Entities;
using KRCP.Domain.Exceptions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class UbicacionService: IUbicacionService
    {
        private readonly IUbicacionRepository _ubicacionRepository;

        public UbicacionService(IUbicacionRepository ubicacionRepository)
        {
            _ubicacionRepository = ubicacionRepository;
        }

        public async Task<UbicacionResponseDto> GetByIdAsync(int id)
        {
            var ubicacion = await _ubicacionRepository.GetByIdAsync(id);

            if(ubicacion is null)
            {
                throw new EntityNotFoundException("Ubiacion", id);
            }

            return ubicacion.Adapt<UbicacionResponseDto>();
        }

        public async Task<IReadOnlyList<UbicacionResponseDto>> GetAllAsync()
        {
            var ubicaciones = await _ubicacionRepository.GetAllAsync();
            return ubicaciones.Adapt<List<UbicacionResponseDto>>();
        }

        public async Task<UbicacionResponseDto> CreateAsync(UbicacionCreateRequestDto dto)
        {
            await ValidarCodigoUnicoAsync(dto.CodigoUbicacion);

            var ubicacion = dto.Adapt<Ubicacion>();

            await _ubicacionRepository.AddAsync(ubicacion);

            return ubicacion.Adapt<UbicacionResponseDto>();

        }

        public async Task UpdateAsync(int id, UbicacionUpdateRequestDto dto)
        {
            var ubicacion = await _ubicacionRepository.GetByIdAsync(id);
            if(ubicacion is null)
            {
                throw new EntityNotFoundException("Ubiacion", id);
            }

            // Solo valida si el código cambió, para evitar falsos positivos al editar el mismo registro
            if (!ubicacion.CodigoUbicacion.Equals(dto.CodigoUbicacion, StringComparison.OrdinalIgnoreCase))
            {
                await ValidarCodigoUnicoAsync(dto.CodigoUbicacion);
            }

            dto.Adapt(ubicacion);

            await _ubicacionRepository.UpdateAsync(ubicacion);

        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var ubicacion = await _ubicacionRepository.GetByIdAsync(id);

            if(ubicacion is null)
            {
                throw new EntityNotFoundException("Ubiacion", id);
            }

            ubicacion.Activo = activo;

            await _ubicacionRepository.UpdateAsync(ubicacion);
        }

        //validar si codigo de ubicacion(nombre) unico
        private async Task ValidarCodigoUnicoAsync(string codigoUbicacion)
        {
            var existeCodigo = await _ubicacionRepository.ExistsByCodigoAsync(codigoUbicacion);
            if (existeCodigo)
                throw new DuplicateEntityException("una ubicacion", "el codigo", codigoUbicacion);
        }

    }
}
