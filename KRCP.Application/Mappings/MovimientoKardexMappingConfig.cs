using KRCP.Application.DTOs.MovimientoKardex;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Mappings
{
    public class MovimientoKardexMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MovimientoKardex, MovimientoKardexResponseDto>()
                .Map(dest => dest.CodigoParte,
                src => src.Producto != null ? src.Producto.CodigoParte : string.Empty)

                .Map(dest => dest.NombreProducto,
                src => src.Producto != null ? src.Producto.Nombre : string.Empty)

                .Map(dest => dest.NombreUsuario,
                src => src.Usuario != null ? src.Usuario.NombreCompleto : string.Empty)

                .Map(dest => dest.CodigoOT,
                src => src.OrdenTrabajo != null ? src.OrdenTrabajo.CodigoOT : null);
        }
    }
}
