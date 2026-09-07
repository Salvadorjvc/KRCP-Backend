using KRCP.Application.DTOs.OtRepuestoConsumido;
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
    public class OtRepuestoConsumidoService: IOtRepuestoConsumidoService
    {
        private readonly IOtRepuestoConsumidoRepository _detalleRepository;
        private readonly IOrdenTrabajoRepository _otRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoService _productoService;
        private readonly IMovimientoKardexRepository _kardexRepository;

        public OtRepuestoConsumidoService(
            IOtRepuestoConsumidoRepository detalleRepository,
            IOrdenTrabajoRepository otRepository,
            IProductoRepository productoRepository,
            IUsuarioRepository usuarioRepository,
            IProductoService productoService,
            IMovimientoKardexRepository kardexRepository)
        {
            _detalleRepository = detalleRepository;
            _otRepository = otRepository;
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
            _productoService = productoService;
            _kardexRepository = kardexRepository;
        }

        public async Task<OtRepuestoConsumidoResponseDto> GetByIdAsync(int id)
        {
            var detalle = await _detalleRepository.GetByIdAsync(id);
            if (detalle is null)
            {
                throw new EntityNotFoundException("OtRepuestoConsumido", id);
            }

            return detalle.Adapt<OtRepuestoConsumidoResponseDto>();
        }

        public async Task<IReadOnlyList<OtRepuestoConsumidoResponseDto>> GetByOtIdAsync(int otId)
        {
            var detalles = await _detalleRepository.GetByOtIdAsync(otId);
            return detalles.Adapt<List<OtRepuestoConsumidoResponseDto>>();
        }

        public async Task<OtRepuestoConsumidoResponseDto> DespacharAsync(OtRepuestoConsumidoCreateRequestDto dto, int usuarioAlmacenId)
        {
     
            var ot = await _otRepository.GetByIdAsync(dto.OtId);
            if (ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", dto.OtId);
            }

            if (ot.Estado == EstadoOrdenTrabajo.Completado || ot.Estado == EstadoOrdenTrabajo.Cancelado)
            {
                throw new OrdenTrabajoCerradaException(ot.CodigoOT, ot.Estado.ToString());
            }

           
            var producto = await _productoRepository.GetByIdAsync(dto.ProductoId);
            if (producto is null)
            {
                throw new EntityNotFoundException("Producto", dto.ProductoId);
            }

            if (!producto.Activo)
            {
                throw new EntityNotActiveException("El producto", producto.CodigoParte);
            }

          
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioAlmacenId);
            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", usuarioAlmacenId);
            }

            if (!usuario.Activo)
            {
                throw new EntityNotActiveException("El usuario", usuario.NombreCompleto);
            }

            var stockAnterior = producto.StockActual; // se guarda antes de ajustar

            var detalle = dto.Adapt<OtRepuestoConsumido>();

            detalle.PrecioUnitarioHistorico = producto.CostoUnitario;
            detalle.UsuarioAlmacenId = usuarioAlmacenId;

            await _detalleRepository.AddAsync(detalle);

            // Descontar stock (esto ya valida internamente stock insuficiente)
            await _productoService.AjustarStockAsync(dto.ProductoId, dto.Cantidad, TipoMovimientoKardex.Salida);

            // registrar el movimiento de Kardex correspondiente
            var productoActualizado = await _productoRepository.GetByIdAsync(dto.ProductoId);

            var movimientoKardex = new MovimientoKardex
            {
                ProductoId = dto.ProductoId,
                UsuarioId = usuarioAlmacenId,
                OtId = dto.OtId,
                TipoMovimiento = TipoMovimientoKardex.Salida,
                Cantidad = dto.Cantidad,
                StockAnterior = stockAnterior,
                StockNuevo = productoActualizado!.StockActual,
                Motivo = $"Despacho a OT {ot.CodigoOT}"
            };
            await _kardexRepository.AddAsync(movimientoKardex);

            //Recalcular CostoRepuestos y CostoTotal de la OT
            var totalRepuestos = await _detalleRepository.SumarCostoTotalByOtIdAsync(dto.OtId);
            ot.CostoRepuestos = totalRepuestos;
            ot.CalcularCostoTotal();
            await _otRepository.UpdateAsync(ot);

            return detalle.Adapt<OtRepuestoConsumidoResponseDto>();
        }

    }
}
