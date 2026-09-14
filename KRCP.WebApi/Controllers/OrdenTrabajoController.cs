using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Constants;
using KRCP.Domain.Enums;
using KRCP.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdenTrabajoController : ControllerBase
    {
        private readonly IOrdenTrabajoService _otService;

        public OrdenTrabajoController(IOrdenTrabajoService otService)
        {
            _otService = otService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrdenTrabajoResponseDto>>> GetAll()
        {
            var Ots = await _otService.GetAllAsync();
            return Ok(Ots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenTrabajoResponseDto>> GetById(int id)
        {
            var Ot = await _otService.GetByIdAsync(id);
            return Ok(Ot);
        }

        [HttpGet("por-tecnico")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.TecnicoAlmacen}")]
        public async Task<ActionResult<IReadOnlyList<OrdenTrabajoResponseDto>>> GetByTecnico(int tecnicoId)
        {
            var Ots = await _otService.GetByTecnicoAsync(tecnicoId);
            return Ok(Ots);
        }

        [HttpGet("por-estado")]
        public async Task<ActionResult<IReadOnlyList<OrdenTrabajoResponseDto>>> GetByEstado(EstadoOrdenTrabajo estado)
        {
            var Ots = await _otService.GetByEstadoAsync(estado);
            return Ok(Ots);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador}")]
        public async Task<ActionResult<OrdenTrabajoResponseDto>> Create(OrdenTrabajoCreateRequestDto dto)
        {
            var resultado = await _otService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.OtId }, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador}")]
        public async Task<IActionResult> Update(int id, OrdenTrabajoUpdateRequestDto dto)
        {
            var usuarioModificacionId = User.GetUsuarioId();

            await _otService.UpdateAsync(id, dto, usuarioModificacionId);
            return NoContent();
        }

        [HttpPatch("{id}/asignar-tecnico")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador}")]
        public async Task<IActionResult> AsignarTecnico(int id, AsignarTecnicoRequestDto dto)
        {
            await _otService.AsignarTecnicoAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador},{Roles.TecnicoAlmacen}")]
        public async Task<IActionResult> CambiarEstado(int id, CambiarEstadoRequestDto dto)
        {
            await _otService.CambiarEstadoAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _otService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }


    }
}
