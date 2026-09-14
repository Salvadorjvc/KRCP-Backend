using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.Producto;
using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Constants;
using KRCP.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductoResponseDto>>> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponseDto>> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            return Ok(producto);
        }

        [HttpGet("stock-bajo")]
        public async Task<ActionResult<IReadOnlyList<ProductoResponseDto>>> GetStockBajo()
        {
            var producto = await _productoService.GetStockBajoAsync();
            return Ok(producto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductoResponseDto>> Create(ProductoCreateRequestDto dto)
        {
            var resultado = await _productoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.ProductoId }, resultado);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update (int id, ProductoUpdateRequestDto dto)
        {
            var usuarioModificacionId = User.GetUsuarioId(); // obtiene el usuario de la sesion(usa mi extensions en de webApi)

            await _productoService.UpdateAsync(id, dto, usuarioModificacionId);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus (int id, ChangeStatusRequestDto dto)
        {
            await _productoService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    }
}
