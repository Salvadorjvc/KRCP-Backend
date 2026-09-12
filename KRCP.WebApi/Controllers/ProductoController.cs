using KRCP.Application.DTOs.Common;
using KRCP.Application.DTOs.Producto;
using KRCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<ProductoResponseDto>> Create(ProductoCreateRequestDto dto)
        {
            var resultado = await _productoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.ProductoId }, resultado);
        }

        //PENDIENTE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, ProductoUpdateRequestDto dto)
        {
            var usuarioModificacionId = 1; //provisional (TEMPORAL) hasta tener el authorize del usuario jwt

            await _productoService.UpdateAsync(id, dto, usuarioModificacionId);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ChangeStatus (int id, ChangeStatusRequestDto dto)
        {
            await _productoService.ChangeStatusAsync(id, dto.Activo);
            return NoContent();
        }
    }
}
