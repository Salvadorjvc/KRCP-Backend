using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.Usuario;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IReadOnlyList<UsuarioResponseDto>>> GetAll()
        {
            var Usuarios = await _usuarioService.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<UsuarioResponseDto>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            return Ok(usuario);
        }

        [HttpGet("tecnicos")]
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Planificador}")]
        public async Task<ActionResult<IReadOnlyList<UsuarioSimpleResponseDto>>> GetTecnicos()
        {
            var tecnicos = await _usuarioService.GetTecnicosActivosAsync();
            return Ok(tecnicos);
        }


        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<UsuarioResponseDto>> Create(UsuarioCreateRequestDto dto)
        {
            var resultado = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.UsuarioId }, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, UsuarioUpdateRequestDto dto)
        {
            await _usuarioService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus (int id, ChangeStatusRequestDto dto)
        {
            await _usuarioService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }

        //cambiar contraseña
        [HttpPatch("{id}/cambiar-password")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangePassword (int id, ChangePasswordRequestDto dto)
        {
            await _usuarioService.ChangePasswordAsync(id, dto);
            return NoContent();
        }
    }
}
