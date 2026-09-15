using KRCP.Application.DTOs.Producto;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Entities;
using KRCP.Domain.Enums;
using KRCP.Domain.Exceptions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class ProductoService: IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUbicacionRepository _ubicacionRepository;

        public ProductoService(
            IProductoRepository productoRepository,
            ICategoriaRepository categoriaRepository,
            IUbicacionRepository ubicacionRepository)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
            _ubicacionRepository = ubicacionRepository;
        }

        public async Task<ProductoResponseDto> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);

            if(producto is null)
            {
                throw new EntityNotFoundException("Producto", id);
            }

            return producto.Adapt<ProductoResponseDto>();
        }

        public async Task<IReadOnlyList<ProductoResponseDto>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();

            return productos.Adapt<List<ProductoResponseDto>>();
        }

        public async Task<IReadOnlyList<ProductoResponseDto>> GetStockBajoAsync()
        {
            var productos = await _productoRepository.GetStockBajoAsync();

            return productos.Adapt<List<ProductoResponseDto>>();
        }

        public async Task<ProductoResponseDto> CreateAsync(ProductoCreateRequestDto dto)
        {
            await ValidarCodigoParteUnicoAsync(dto.CodigoParte);
            await ValidarCategoriaExisteAsync(dto.CategoriaId);

            if(dto.UbicacionId.HasValue)
            {
                await ValidarUbicacionExisteAsync(dto.UbicacionId.Value);
            }

            var producto = dto.Adapt<Producto>();
            producto.StockActual = 0; // todo producto nuevo empieza sin stock

            await _productoRepository.AddAsync(producto);

            return producto.Adapt<ProductoResponseDto>();

        }

        public async Task UpdateAsync(int id, ProductoUpdateRequestDto dto,int usuarioModificacionId)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if(producto is null)
            {
                throw new EntityNotFoundException("Producto", id);
            }

            if (producto.CategoriaId != dto.CategoriaId)
            {
                await ValidarCategoriaExisteAsync(dto.CategoriaId);
            }

            if(dto.UbicacionId.HasValue && producto.UbicacionId != dto.UbicacionId)
            {
                await ValidarUbicacionExisteAsync(dto.UbicacionId.Value);
            }

            dto.Adapt(producto);
            producto.FechaModificacion = DateTime.UtcNow;
            producto.UsuarioModificacionId = usuarioModificacionId;

            await _productoRepository.UpdateAsync(producto);
        }

        public async Task ChangeStatusAsync(int id, bool activo)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto is null)
            {
                throw new EntityNotFoundException("Producto", id);
            }

            producto.Activo = activo;
            await _productoRepository.UpdateAsync(producto);
        }

        public async Task AjustarStockAsync(int productoId, int cantidad, TipoMovimientoKardex tipoMovimiento)
        {
            var producto = await _productoRepository.GetByIdAsync(productoId);

            if (producto is null)
            {
                throw new EntityNotFoundException("Producto", productoId);
            }

            var nuevoStock = tipoMovimiento switch
            {
                TipoMovimientoKardex.Entrada => producto.StockActual + cantidad,
                TipoMovimientoKardex.Devolucion => producto.StockActual + cantidad,
                TipoMovimientoKardex.Salida => producto.StockActual - cantidad,
                TipoMovimientoKardex.Ajuste => cantidad,
                _ => throw new InvalidOperationException("Tipo de movimiento no reconocido")
            };

            if (nuevoStock < 0)
            {
                throw new StockInsuficienteException(producto.CodigoParte, cantidad, producto.StockActual);
            }

            producto.StockActual = nuevoStock;
            await _productoRepository.UpdateAsync(producto);
        }

        //metodos private 
        private async Task ValidarCodigoParteUnicoAsync(string codigoParte)
        {
            var existe = await _productoRepository.ExistsByCodigoParteAsync(codigoParte);
            if (existe)
            {
                throw new DuplicateEntityException("un Producto", "el código de parte", codigoParte);
            }
        }

        private async Task ValidarCategoriaExisteAsync ( int categoriaId)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);
            if(categoria is null)
            {
                throw new EntityNotFoundException("Categoria", categoriaId);
            }
        }

        private async Task ValidarUbicacionExisteAsync(int ubicacionId)
        {
            var ubicacion = await _ubicacionRepository.GetByIdAsync(ubicacionId);
            if (ubicacion is null)
            {
                throw new EntityNotFoundException("Ubicacion", ubicacionId);
            }
        }
 
    }
}
