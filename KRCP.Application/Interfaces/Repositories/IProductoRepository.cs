using KRCP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Repositories
{
    public interface IProductoRepository: IGenericRepository<Producto>
    {
        Task<bool> ExistsByCodigoParteAsync(string codigoParte);
        Task<IReadOnlyList<Producto>> GetStockBajoAsync();// alerta si StockActual <= StockMinimo
    }
}
