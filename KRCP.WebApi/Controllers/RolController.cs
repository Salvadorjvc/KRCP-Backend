using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.Rol;
using KRCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {

        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        //getters pa
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RolResponseDto>>> GetAll()
        {
            var roles = await _rolService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolResponseDto>> GetById(int id)
        {
            var rol = await _rolService.GetByIdAsync(id);

            return Ok(rol);
        }

        //post
        [HttpPost]
        public async Task<ActionResult<RolResponseDto>> Create(RolCreateRequestDto dto)
        {
            var resultado = await _rolService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.RolId }, resultado);
        }

        //put
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RolUpdateRequestDto dto)
        {
            await _rolService.UpdateAsync(id, dto);
            return NoContent();
        }

        //patch
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _rolService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    
    }
}
