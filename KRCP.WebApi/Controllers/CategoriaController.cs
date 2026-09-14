using KRCP.Application.DTOs.Categoria;
using KRCP.Application.DTOs.Common;
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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoriaResponseDto>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDto>> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            return Ok(categoria);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<CategoriaResponseDto>> Create(CategoriaCreateRequestDto dto)
        {
            var resultado = await _categoriaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.CategoriaId }, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, CategoriaUpdateRequestDto dto)
        {
           await _categoriaService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusRequestDto dto)
        {
            await _categoriaService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    }
}
