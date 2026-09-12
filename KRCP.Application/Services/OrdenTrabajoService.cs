using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.Interfaces.Repositories;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Exceptions;
using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using KRCP.Domain.Enums;

namespace KRCP.Application.Services
{
    public class OrdenTrabajoService : IOrdenTrabajoService
    {
        private readonly IOrdenTrabajoRepository _otRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IErpIntegracionLogService _erpLogService;

        public OrdenTrabajoService(
            IOrdenTrabajoRepository otRepository,
            IClienteRepository clienteRepository,
            IUsuarioRepository usuarioRepository,
            IErpIntegracionLogService erpLogService
            )
        {
            _otRepository = otRepository;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
            _erpLogService = erpLogService;

        }

        public async Task<OrdenTrabajoResponseDto> GetByIdAsync(int id)
        {
            var ot = await _otRepository.GetByIdAsync(id);

            if (ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", id);
            }

            return ot.Adapt<OrdenTrabajoResponseDto>();
        }

        public async Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetAllAsync()
        {
            var ots = await _otRepository.GetAllAsync();

            return ots.Adapt<List<OrdenTrabajoResponseDto>>();
        }

        public async Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetByTecnicoAsync(int tecnicoId)
        {
            var ots = await _otRepository.GetByTecnicoAsignadoAsync(tecnicoId);
            return ots.Adapt<List<OrdenTrabajoResponseDto>>();
        }


        public async Task<IReadOnlyList<OrdenTrabajoResponseDto>> GetByEstadoAsync(EstadoOrdenTrabajo estado)
        {
            var ots = await _otRepository.GetByEstadoAsync(estado);

            return ots.Adapt<List<OrdenTrabajoResponseDto>>();
        }

        public async Task<OrdenTrabajoResponseDto> CreateAsync(OrdenTrabajoCreateRequestDto dto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
            if (cliente is null)
            {
                throw new EntityNotFoundException("Cliente", dto.ClienteId);
            }

            if (!cliente.Activo)
            {
                throw new EntityNotActiveException("El cliente",cliente.RazonSocial);
            }

            var planificador = await _usuarioRepository.GetByIdAsync(dto.UsuarioPlanificadorId);
            if (planificador is null)
            {
                throw new EntityNotFoundException("Usuario", dto.UsuarioPlanificadorId);
            }

            if (!planificador.Activo)
            {
                throw new EntityNotActiveException("El planificador", planificador.NombreCompleto);
            }


            var ordenTrabajo = dto.Adapt<OrdenTrabajo>();
            ordenTrabajo.CodigoOT = await GenerarCodigoOtAsync();
            ordenTrabajo.Estado = EstadoOrdenTrabajo.Registrado;

            await _otRepository.AddAsync(ordenTrabajo);
            return ordenTrabajo.Adapt<OrdenTrabajoResponseDto>();
                
        }

        public async Task UpdateAsync(int id, OrdenTrabajoUpdateRequestDto dto, int usuarioModificacionId)
        {
            var ot = await _otRepository.GetByIdAsync(id);
            if ( ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", id);
            }

            dto.Adapt(ot);
            ot.CalcularCostoTotal(); // por si CostManoObraCambio
            ot.FechaModificacion = DateTime.UtcNow;
            ot.UsuarioModificacionId = usuarioModificacionId;

            await _otRepository.UpdateAsync(ot);
        }

        public async Task AsignarTecnicoAsync(int otId, AsignarTecnicoRequestDto dto)
        {
            var ot = await _otRepository.GetByIdAsync(otId);
            if(ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", otId);
            }

            var tecnico = await _usuarioRepository.GetByIdAsync(dto.TecnicoId);
            if(tecnico is null)
            {
                throw new EntityNotFoundException("Tecnico", dto.TecnicoId);
            }

            if (!tecnico.Activo)
            {
                throw new EntityNotActiveException("El tecnico", tecnico.NombreCompleto);
            }

            ot.TecnicoAsignadoId = dto.TecnicoId;
            ot.FechaModificacion = DateTime.UtcNow;

            await _otRepository.UpdateAsync(ot);
        }

        public async Task CambiarEstadoAsync(int otId, CambiarEstadoRequestDto dto)
        {
            var ot = await _otRepository.GetByIdAsync(otId);
            if (ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", otId);
            }

            ValidarTransicionEstado(ot.Estado, dto.NuevoEstado);

            ot.Estado = dto.NuevoEstado;
            ot.FechaModificacion = DateTime.UtcNow;

            if(dto.NuevoEstado == EstadoOrdenTrabajo.Completado)
            {
                ot.FechaCierre = DateTime.UtcNow;

                // Simulación de sincronización con el ERP central(luego averiguo mas sobre Oracle y SAP)
                var payload = System.Text.Json.JsonSerializer.Serialize(new
                {
                    codigoOT = ot.CodigoOT,
                    clienteId = ot.ClienteId,
                    montoTotal = ot.CostoTotal,
                    status = ot.Estado.ToString()
                });

                await _erpLogService.RegistrarAsync("ORDEN_TRABAJO", ot.OtId, "SYNC_OUT", payload);
            }

            await _otRepository.UpdateAsync(ot);
        }

        public async Task ChangeStatusAsync(int id, bool Activo)
        {
            var ot = await _otRepository.GetByIdAsync(id);
            if(ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", id);
            }

            ot.Activo = Activo;
            await _otRepository.UpdateAsync(ot);
        }
        //privates
        private async Task<string> GenerarCodigoOtAsync()
        {
            var anioActual = DateTime.UtcNow.Year;
            var cantidadDelAnio = await _otRepository.GetCountByYearAsync(anioActual);
            var correlativo = (cantidadDelAnio + 1).ToString("D3"); //001, 002,...

            return $"OT-{anioActual}-{correlativo}";
        }

        private static void ValidarTransicionEstado(EstadoOrdenTrabajo estadoActual, EstadoOrdenTrabajo nuevoEstado)
        {
            if (estadoActual == EstadoOrdenTrabajo.Completado || estadoActual == EstadoOrdenTrabajo.Cancelado)
                throw new TransicionEstadoInvalidaException(estadoActual.ToString());
        }
    }
}
