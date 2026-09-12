using KRCP.Application.DTOs.ErpIntegracionLog;
using KRCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErpIntegracionLogController : ControllerBase
    {
        private readonly IErpIntegracionLogService _logService;

        public ErpIntegracionLogController(IErpIntegracionLogService logService)
        {
            _logService = logService;
        }

        [HttpGet("por-entidad")]
        public async Task<ActionResult<IReadOnlyList<ErpIntegracionLogResponseDto>>> GetByEntidad(string entidadAfectada, int entidadId)
        {
            var logs = await _logService.GetByEntidadAsync(entidadAfectada, entidadId);
            return Ok(logs);
        }
    }
}
