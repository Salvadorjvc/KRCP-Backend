using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.Ubicacion;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionService _ubicacionService;

        public UbicacionController(IUbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UbicacionResponseDto>>> GetAll()
        {
            var ubicaciones = await _ubicacionService.GetAllAsync();
            return Ok(ubicaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UbicacionResponseDto>> GetById(int id)
        {
            var ubicacion = await _ubicacionService.GetByIdAsync(id);
            return Ok(ubicacion);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<UbicacionResponseDto>> Create(UbicacionCreateRequestDto dto)
        {
            var resultado = await _ubicacionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.UbicacionId }, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, UbicacionUpdateRequestDto dto)
        {
            await _ubicacionService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _ubicacionService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    }
}
