using ClosedXML.Excel;
using KRCP.Application.DTOs.MovimientoKardex;
using KRCP.Application.DTOs.OtRepuestoConsumido;
using KRCP.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Reports
{
    public class ClosedXmlExcelGenerator: IExcelGenerator
    {
        public byte[] GenerarKardexMensual(IReadOnlyList<MovimientoKardexResponseDto> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Kardex Mensual");

            worksheet.Cell(1, 1).Value = "Producto";
            worksheet.Cell(1, 2).Value = "Tipo Movimiento";
            worksheet.Cell(1, 3).Value = "Cantidad";
            worksheet.Cell(1, 4).Value = "Stock Anterior";
            worksheet.Cell(1, 5).Value = "Stock Nuevo";
            worksheet.Cell(1, 6).Value = "Fecha del movimiento";
            worksheet.Row(1).Style.Font.Bold = true;

            var fila = 2;
            foreach(var item in data)
            {
                worksheet.Cell(fila, 1).Value = item.NombreProducto;
                worksheet.Cell(fila, 2).Value = item.TipoMovimiento;
                worksheet.Cell(fila, 3).Value = item.Cantidad;
                worksheet.Cell(fila, 4).Value = item.StockAnterior;
                worksheet.Cell(fila, 5).Value = item.StockNuevo;
                worksheet.Cell(fila, 6).Value = item.FechaMovimiento;
                fila++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GenerarComponentesEntregados(IReadOnlyList<OtRepuestoConsumidoResponseDto> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Componentes Entregados");

            worksheet.Cell(1, 1).Value = "Código Parte";
            worksheet.Cell(1, 2).Value = "Producto";
            worksheet.Cell(1, 3).Value = "Cantidad";
            worksheet.Cell(1, 4).Value = "Precio Unitario";
            worksheet.Cell(1, 5).Value = "Subtotal";
            worksheet.Row(1).Style.Font.Bold = true;

            var fila = 2;
            foreach(var item in data)
            {
                worksheet.Cell(fila, 1).Value = item.CodigoParte;
                worksheet.Cell(fila, 2).Value = item.NombreProducto;
                worksheet.Cell(fila, 3).Value = item.Cantidad;
                worksheet.Cell(fila, 4).Value = item.PrecioUnitarioHistorico;
                worksheet.Cell(fila, 5).Value = item.Subtotal;
                fila++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
