using KRCP.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardCardsResponseDto> GetCardsAsync();
        Task<IReadOnlyList<DashboardBarrasResponseDto>> GetBarrasAsync();
        Task<IReadOnlyList<DashboardPieResponseDto>> GetPieAsync();
    }
}
