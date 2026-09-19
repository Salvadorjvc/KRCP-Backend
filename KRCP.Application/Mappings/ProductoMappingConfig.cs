using KRCP.Application.DTOs.Producto;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Mappings
{
    public class ProductoMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Producto, ProductoResponseDto>()
                .Map(dest => dest.NombreCategoria,
                src => src.Categoria != null ? src.Categoria.Nombre : string.Empty)

                .Map(dest => dest.CodigoUbicacion,
                src => src.Ubicacion != null ? src.Ubicacion.CodigoUbicacion : null)

                .Map(dest => dest.NombreUsuarioModificacion,
                src => src.UsuarioModificacion != null ? src.UsuarioModificacion.NombreCompleto : null);
        }
    }
}
