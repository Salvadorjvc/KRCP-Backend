using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.DTOs.Dashboard
{
    public class DashboardCardsResponseDto
    {
        public int TotalOtsActivas { get; set; }
        public int TotalProductosStockBajo { get; set; }
        public decimal ValorTotalInventario { get; set; }
    }

}
