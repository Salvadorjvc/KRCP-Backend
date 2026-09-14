using KRCP.Application.DTOs.Dashboard;
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
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("cards")]
        public async Task <ActionResult<DashboardCardsResponseDto>> GetCards()
        {
            var cards = await _dashboardService.GetCardsAsync();
            return Ok(cards);
        }

        [HttpGet("barras")]
        public async Task <ActionResult<IReadOnlyList<DashboardBarrasResponseDto>>> GetBarras()
        {
            var barras = await _dashboardService.GetBarrasAsync();
            return Ok(barras);
        }

        [HttpGet("pie")]
        public async Task <ActionResult<IReadOnlyList<DashboardPieResponseDto>>> GetPie()
        {
            var pie = await _dashboardService.GetPieAsync();
            return Ok(pie);
        }
    }
}
