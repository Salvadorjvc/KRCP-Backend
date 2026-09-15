using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace KRCP.Infrastructure.Reports
{
    public class QuestPdfGenerator: IPdfGenerator
    {
        public QuestPdfGenerator()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerarLiquidacionOt(OrdenTrabajoResponseDto data)
        {
            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header()
                        .Text($"Hoja de liquidación - {data.CodigoOt}")
                        .SemiBold().FontSize(18);

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().Text($"Cliente: {data.RazonSocialCliente}");
                        column.Item().Text($"Equipo: {data.EquipoComponente}");
                        column.Item().Text($"Número de Serie: {data.NumeroSerie}");
                        column.Item().Text($"Estado: {data.Estado}");
                        column.Item().Text($"Costo Mano de Obra: S/ {data.CostoManoObra:N2}");
                        column.Item().Text($"Costo Repuestos: S/ {data.CostoRepuestos:N2}");
                        column.Item().Text($"Costo Total: S/ {data.CostoTotal:N2}").Bold();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            var zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); // UTC-5, Perú
                            var fechaLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaPeru); // Ajuste de UTC a hora local

                            x.Span("Generado el ");
                            x.Span(fechaLocal.ToString("dd/MM/yyyy HH:mm"));
                        });
                });
            });

            return documento.GeneratePdf();
        }
    }
}
