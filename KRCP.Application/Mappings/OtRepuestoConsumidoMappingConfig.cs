using KRCP.Application.DTOs.OtRepuestoConsumido;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Mappings
{
    public class OtRepuestoConsumidoMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OtRepuestoConsumido, OtRepuestoConsumidoResponseDto>()
                .Map(dest => dest.CodigoOt,
                src => src.OrdenTrabajo != null ? src.OrdenTrabajo.CodigoOT : string.Empty)

                .Map(dest => dest.CodigoParte,
                src => src.Producto != null ? src.Producto.CodigoParte : string.Empty)

                .Map(dest => dest.NombreProducto,
                src => src.Producto != null ? src.Producto.Nombre : string.Empty)

                .Map(dest => dest.NombreUsuarioAlmacen,
                src => src.UsuarioAlmacen != null ? src.UsuarioAlmacen.NombreCompleto : string.Empty);
        }
    }
}
