using KRCP.Application.DTOs.MovimientoKardex;
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
    public class MovimientoKardexService: IMovimientoKardexService
    {
        private readonly IMovimientoKardexRepository _kardexRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoService _productoService;

        public MovimientoKardexService(
            IMovimientoKardexRepository kardexRepository,
            IProductoRepository productoRepository,
            IUsuarioRepository usuarioRepository,
            IProductoService productoService)
        {
            _kardexRepository = kardexRepository;
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
            _productoService = productoService;
        }
        public async Task<MovimientoKardexResponseDto> GetByIdAsync(int id)
        {
            var movimiento = await _kardexRepository.GetByIdAsync(id);
            if (movimiento is null)
                throw new EntityNotFoundException("MovimientoKardex", id);

            return movimiento.Adapt<MovimientoKardexResponseDto>();
        }

        public async Task<IReadOnlyList<MovimientoKardexResponseDto>> GetByProductoIdAsync(int productoId)
        {
            var movimientos = await _kardexRepository.GetByProductoIdAsync(productoId);
            return movimientos.Adapt<List<MovimientoKardexResponseDto>>();
        }

        public async Task<IReadOnlyList<MovimientoKardexResponseDto>> GetByOtIdAsync(int otId)
        {
            var movimientos = await _kardexRepository.GetByOtIdAsync(otId);
            return movimientos.Adapt<List<MovimientoKardexResponseDto>>();
        }

        public async Task<MovimientoKardexResponseDto> RegistrarMovimientoKardexAsync(MovimientoKardexCreateRequestDto dto, int usuarioId)
        {
            var producto = await _productoRepository.GetByIdAsync(dto.ProductoId);
            if (producto is null)
            {
                throw new EntityNotFoundException("Producto", dto.ProductoId);
            }

            if (!producto.Activo)
            {
                throw new EntityNotActiveException("El producto", producto.CodigoParte);
            }

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", usuarioId);
            }

            if (!usuario.Activo)
            {
                throw new EntityNotActiveException("El usuario", usuario.NombreCompleto);
            }

            var stockAnterior = producto.StockActual;

            await _productoService.AjustarStockAsync(dto.ProductoId, dto.Cantidad, dto.TipoMovimiento);


            //se vuelve a llamar al producto para obtener su stockActual actualizado
            var productoActualizado = await _productoRepository.GetByIdAsync(dto.ProductoId);


            var movimiento = dto.Adapt<MovimientoKardex>();
            movimiento.UsuarioId = usuarioId;
            movimiento.StockAnterior = stockAnterior;
            movimiento.StockNuevo = productoActualizado!.StockActual;

            await _kardexRepository.AddAsync(movimiento);
            return movimiento.Adapt<MovimientoKardexResponseDto>();
        }
    }
}
