using KRCP.Application.Interfaces.Services;
using KRCP.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KRCP.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.Gerencia}")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IExcelGenerator _excelGenerator;

        public ReporteController(
            IReporteService reporteService,
            IPdfGenerator pdfGenerator,
            IExcelGenerator excelGenerator)
        {
            _reporteService = reporteService;
            _pdfGenerator = pdfGenerator;
            _excelGenerator = excelGenerator;
        }

        [HttpGet("ot/{id}/liquidacion-pdf")]
        public async Task<IActionResult> LiquidacionOtPdf(int id)
        {
            var data = await _reporteService.GetDataLiquidacionOtAsync(id);
            var pdfBytes = _pdfGenerator.GenerarLiquidacionOt(data);
            return File(pdfBytes, "application/pdf", $"Liquidacion-{data.CodigoOt}.pdf");
        }

        [HttpGet("Kardex-mensual-excel")]
        public async Task<IActionResult> KardexMensualExcel(int mes, int anio)
        {
            var data = await _reporteService.GetDataKardexMensualAsync(mes, anio);
            var excelBytes = _excelGenerator.GenerarKardexMensual(data);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Kardex-{mes}-{anio}.xlsx");
        }

        [HttpGet("ot/{id}/componentes-excel")]
        public async Task<IActionResult> ComponentesEntregadosExcel(int id)
        {
            var data = await _reporteService.GetDataComponentesEntregadosAsync(id);
            var excelBytes = _excelGenerator.GenerarComponentesEntregados(data);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Componentes-OT-{id}.xlsx");
        }
    }
}
