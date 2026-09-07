using KRCP.Application.DTOs.Dashboard;
using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IOtRepuestoConsumidoRepository: IGenericRepository<OtRepuestoConsumido>
    {
        Task<IReadOnlyList<OtRepuestoConsumido>> GetByOtIdAsync(int otId);
        Task<decimal> SumarCostoTotalByOtIdAsync(int otId); // para recalcular CostoRepuestos de la OT
        Task<IReadOnlyList<DashboardPieResponseDto>> GetTopConsumidosAsync(int top, DateTime? desde = null, DateTime? hasta = null);
    }
}
