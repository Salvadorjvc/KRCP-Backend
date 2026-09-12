using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<OrdenTrabajoResponseDto>> Create(OrdenTrabajoCreateRequestDto dto)
        {
            var resultado = await _otService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.OtId }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrdenTrabajoUpdateRequestDto dto)
        {
            var usuarioModificacionId = 1; //TEMPORAL

            await _otService.UpdateAsync(id, dto, usuarioModificacionId);
            return NoContent();
        }

        [HttpPatch("{id}/asignar-tecnico")]
        public async Task<IActionResult> AsignarTecnico(int id, AsignarTecnicoRequestDto dto)
        {
            await _otService.AsignarTecnicoAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstado(int id, CambiarEstadoRequestDto dto)
        {
            await _otService.CambiarEstadoAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _otService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }


    }
}
