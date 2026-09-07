using KRCP.Application.DTOs.Categoria;
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
    public class CategoriaService: ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaResponseDto> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if(categoria is null)
            {
                throw new EntityNotFoundException("Categoria", id);
            }

            return categoria.Adapt<CategoriaResponseDto>();
        }

        public async Task<IReadOnlyList<CategoriaResponseDto>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Adapt<List<CategoriaResponseDto>>();
        }

        public async Task<CategoriaResponseDto> CreateAsync (CategoriaCreateRequestDto dto)
        {
            await ValidarNombreUnicoAsync(dto.Nombre);

            var categoria = dto.Adapt<Categoria>();

            await _categoriaRepository.AddAsync(categoria);

            return categoria.Adapt<CategoriaResponseDto>();
        }

        public async Task UpdateAsync(int id, CategoriaUpdateRequestDto dto)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if(categoria is null)
            {
                throw new EntityNotFoundException("Categoria", id);
            }

            if (!categoria.Nombre.Equals(dto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                await ValidarNombreUnicoAsync(dto.Nombre);
            }

            dto.Adapt(categoria);
            await _categoriaRepository.UpdateAsync(categoria);

        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria is null)
            {
                throw new EntityNotFoundException("Categoria", id);
            }

            categoria.Activo = activo;

            await _categoriaRepository.UpdateAsync(categoria);
        }


        //validar si nombre unico
        private async Task ValidarNombreUnicoAsync(string nombre)
        {
            var existeNombre = await _categoriaRepository.ExistsByNameAsync(nombre);
            if (existeNombre)
                throw new DuplicateEntityException("una Categoria", "el nombre", nombre);
        }
    }
}
