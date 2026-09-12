using KRCP.Application.DTOs.Dashboard;
using KRCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dasboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dasboardService = dashboardService;
        }

        [HttpGet("cards")]
        public async Task <ActionResult<DashboardCardsResponseDto>> GetCards()
        {
            var cards = await _dasboardService.GetCardsAsync();
            return Ok(cards);
        }

        [HttpGet("barras")]
        public async Task <ActionResult<IReadOnlyList<DashboardBarrasResponseDto>>> GetBarras()
        {
            var barras = await _dasboardService.GetBarrasAsync();
            return Ok(barras);
        }

        [HttpGet("pie")]
        public async Task <ActionResult<IReadOnlyList<DashboardPieResponseDto>>> GetPie()
        {
            var pie = await _dasboardService.GetPieAsync();
            return Ok(pie);
        }
    }
}
