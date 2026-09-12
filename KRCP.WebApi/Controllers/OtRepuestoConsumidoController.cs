using KRCP.Application.DTOs.OtRepuestoConsumido;
using KRCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtRepuestoConsumidoController : ControllerBase
    {
        private readonly IOtRepuestoConsumidoService _otRepuestoConsumidoService;

        public OtRepuestoConsumidoController(IOtRepuestoConsumidoService otRepuestoConsumidoService)
        {
            _otRepuestoConsumidoService = otRepuestoConsumidoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OtRepuestoConsumidoResponseDto>> GetById(int id)
        {
            var repuesto = await _otRepuestoConsumidoService.GetByIdAsync(id);
            return Ok(repuesto);
        }

        [HttpGet("por-ordentrabajo")]
        public async Task<ActionResult<IReadOnlyList<OtRepuestoConsumidoResponseDto>>> GetByOtId(int id)
        {
            var repuestos = await _otRepuestoConsumidoService.GetByOtIdAsync(id);
            return Ok(repuestos);
        }

        [HttpPost]
        public async Task<ActionResult<OtRepuestoConsumidoResponseDto>> Despachar(OtRepuestoConsumidoCreateRequestDto dto)
        {
            var usuarioAlmacenId = 1; // TEMPORAL, hasta conectar el usuario autenticado del JWT

            var resultado = await _otRepuestoConsumidoService.DespacharAsync(dto, usuarioAlmacenId);
            return CreatedAtAction(nameof(GetById), new {id = resultado.DetalleId}, resultado);
        }

    }
}
