using KRCP.Application.DTOs.MovimientoKardex;
using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.DTOs.OtRepuestoConsumido;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Exceptions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Services
{
    public class ReporteService: IReporteService
    {
        private readonly IOrdenTrabajoRepository _otRepository;
        private readonly IMovimientoKardexRepository _kardexRepository;
        private readonly IOtRepuestoConsumidoRepository _detalleRepository;

        public ReporteService(
            IOrdenTrabajoRepository otRepository,
            IMovimientoKardexRepository kardexRepository,
            IOtRepuestoConsumidoRepository detalleRepository
            )
        {
            _otRepository = otRepository;
            _kardexRepository = kardexRepository;
            _detalleRepository = detalleRepository;
        }

        public async Task<OrdenTrabajoResponseDto> GetDataLiquidacionOtAsync(int otId)
        {
            var ot = await _otRepository.GetByIdAsync(otId);
            if(ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", otId);
            }

            return ot.Adapt<OrdenTrabajoResponseDto>();
        }

        public async Task<IReadOnlyList<MovimientoKardexResponseDto>> GetDataKardexMensualAsync(int mes, int anio)
        {
            var movimientos = await _kardexRepository.GetByMesAnioAsync(mes, anio);

            return movimientos.Adapt<List<MovimientoKardexResponseDto>>();
        }

        public async Task<IReadOnlyList<OtRepuestoConsumidoResponseDto>> GetDataComponentesEntregadosAsync(int otId)
        {
            var detalles = await _detalleRepository.GetByOtIdAsync(otId);
            return detalles.Adapt<List<OtRepuestoConsumidoResponseDto>>();
        }
    }
}
