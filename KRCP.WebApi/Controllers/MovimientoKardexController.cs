using KRCP.Application.DTOs.MovimientoKardex;
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
    public class MovimientoKardexController : ControllerBase
    {
        private readonly IMovimientoKardexService _kardexService;

        public MovimientoKardexController(IMovimientoKardexService kardexService)
        {
            _kardexService = kardexService;
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<MovimientoKardexResponseDto>> GetById(int id)
        {
            var kardex = await _kardexService.GetByIdAsync(id);
            return Ok(kardex);
        }

        [HttpGet("por-producto")]
        public async Task <ActionResult<IReadOnlyList<MovimientoKardexResponseDto>>> GetByProductoId(int id)
        {
            var kardexs = await _kardexService.GetByProductoIdAsync(id);
            return Ok(kardexs);
        }

        [HttpGet("por-ordentrabajo")]
        public async Task <ActionResult<IReadOnlyList<MovimientoKardexResponseDto>>> GetByOtId(int id)
        {
            var kardexs = await _kardexService.GetByOtIdAsync(id);
            return Ok(kardexs);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.TecnicoAlmacen}")]
        public async Task <ActionResult<MovimientoKardexResponseDto>> RegistrarMovimientoKardex( MovimientoKardexCreateRequestDto dto)
        {
            var usuarioId = User.GetUsuarioId();

            var resultado = await _kardexService.RegistrarMovimientoKardexAsync(dto, usuarioId);
            return CreatedAtAction(nameof(GetById), new { id = resultado.KardexId }, resultado);
        }


    }
}
