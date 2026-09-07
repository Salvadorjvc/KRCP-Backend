using KRCP.Application.DTOs.OtEvidencia;
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
    public class OtEvidenciaService: IOtEvidenciaService
    {
        private readonly IOtEvidenciaRepository _otEvidenciaRepository;
        private readonly IOrdenTrabajoRepository _otRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public OtEvidenciaService(
            IOtEvidenciaRepository otEvidenciaRepository,
            IOrdenTrabajoRepository otRepository,
            IUsuarioRepository usuarioRepository)
        {
            _otEvidenciaRepository = otEvidenciaRepository;
            _otRepository = otRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<OtEvidenciaResponseDto> GetByIdAsync(int id)
        {
            var otEvidencia = await _otEvidenciaRepository.GetByIdAsync(id);
            
            if(otEvidencia is null)
            {
                throw new EntityNotFoundException("OtEvidencia", id);
            }

            return otEvidencia.Adapt<OtEvidenciaResponseDto>();
        }

        public async Task<IReadOnlyList<OtEvidenciaResponseDto>> GetByOtIdAsync(int otId)
        {
            var otEvidencias = await _otEvidenciaRepository.GetByOtIdAsync(otId);

            return otEvidencias.Adapt<List<OtEvidenciaResponseDto>>();
        }

        public async Task<OtEvidenciaResponseDto> CreateAsync (OtEvidenciaCreateRequestDto dto, int usuarioCargaId)
        {
            var ot = await _otRepository.GetByIdAsync(dto.OtId);
            if (ot is null)
            {
                throw new EntityNotFoundException("OrdenTrabajo", dto.OtId);
            }

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioCargaId);
            if (usuario is null)
            {
                throw new EntityNotFoundException("Usuario", usuarioCargaId);
            }

            if (!usuario.Activo)
            {
                throw new EntityNotActiveException("El usuario", usuario.NombreCompleto);
            }

            if (ot.Estado == EstadoOrdenTrabajo.Completado || ot.Estado == EstadoOrdenTrabajo.Cancelado)
            {
                throw new OrdenTrabajoCerradaException(ot.CodigoOT, ot.Estado.ToString());
            }

            var otEvidencia = dto.Adapt<OtEvidencia>();
            otEvidencia.UsuarioCargaId = usuarioCargaId;


            await _otEvidenciaRepository.AddAsync(otEvidencia);

            return otEvidencia.Adapt<OtEvidenciaResponseDto>();
        }

        //privates
    }
}
