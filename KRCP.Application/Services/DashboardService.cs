using KRCP.Application.DTOs.Dashboard;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class DashboardService: IDashboardService
    {
        private readonly IOrdenTrabajoRepository _otRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IOtRepuestoConsumidoRepository _detalleRepository;

        public DashboardService(
            IOrdenTrabajoRepository otRepository,
            IProductoRepository productoRepository,
            IOtRepuestoConsumidoRepository detalleRepository)
        {
            _otRepository = otRepository;
            _productoRepository = productoRepository;
            _detalleRepository = detalleRepository;
        }
       
        public async Task<DashboardCardsResponseDto> GetCardsAsync()
        {
            //para los cards de ordenes activas
            var otsEnProceso = await _otRepository.GetByEstadoAsync(EstadoOrdenTrabajo.EnProceso);
            var otsRegistradas = await _otRepository.GetByEstadoAsync(EstadoOrdenTrabajo.Registrado);
            var otsQa = await _otRepository.GetByEstadoAsync(EstadoOrdenTrabajo.QaPruebas);

            //para la cantidad de repuestos con stock bajo y el valor total del inventario
            var stockBajo = await _productoRepository.GetStockBajoAsync();
            var productos = await _productoRepository.GetAllAsync();

            return new DashboardCardsResponseDto
            {
                TotalOtsActivas = otsEnProceso.Count + otsRegistradas.Count + otsQa.Count,
                TotalProductosStockBajo = stockBajo.Count,
                ValorTotalInventario = productos.Sum(p => p.CostoUnitario * p.StockActual)
            };
        }

        public async Task<IReadOnlyList<DashboardBarrasResponseDto>> GetBarrasAsync()
        {
            // para el grafico de barras de ordenes por estado
            var resultado = new List<DashboardBarrasResponseDto>();

            foreach(EstadoOrdenTrabajo estado in Enum.GetValues<EstadoOrdenTrabajo>())
            {
                var ots = await _otRepository.GetByEstadoAsync(estado);
                resultado.Add(new DashboardBarrasResponseDto
                {
                    Estado = estado.ToString(),
                    Cantidad = ots.Count
                });
            }

            return resultado;
        }

        public async Task<IReadOnlyList<DashboardPieResponseDto>> GetPieAsync()
        {
            return await _detalleRepository.GetTopConsumidosAsync(5);
        }
    }
}
