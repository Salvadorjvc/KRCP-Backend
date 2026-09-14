using KRCP.Application.DTOs.OtEvidencia;
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
    public class OtEvidenciaController : ControllerBase
    {
        private readonly IOtEvidenciaService _otEvidenciaService;

        public OtEvidenciaController(IOtEvidenciaService otEvidenciaService)
        {
            _otEvidenciaService = otEvidenciaService;
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<OtEvidenciaResponseDto>> GetById(int id)
        {
            var evidencias = await _otEvidenciaService.GetByIdAsync(id);
            return Ok(evidencias);
        }

        [HttpGet("por-evidencias")]
        public async Task <ActionResult<IReadOnlyList<OtEvidenciaResponseDto>>> GetByOtId(int Otid)
        {
            var Evidencias = await _otEvidenciaService.GetByOtIdAsync(Otid);
            return Ok(Evidencias);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Planificador},{Roles.TecnicoAlmacen}")]
        public async Task <ActionResult<OtEvidenciaResponseDto>> Create (OtEvidenciaCreateRequestDto dto)
        {
            var usuarioCargaId = User.GetUsuarioId();

            var resultado = await _otEvidenciaService.CreateAsync(dto, usuarioCargaId);
            return CreatedAtAction(nameof(GetById), new { id = resultado.OtId }, resultado);
        }
    }
}
