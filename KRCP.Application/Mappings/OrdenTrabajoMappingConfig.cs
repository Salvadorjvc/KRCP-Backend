using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Mappings
{
    public class OrdenTrabajoMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrdenTrabajo, OrdenTrabajoResponseDto>()
                .Map(dest => dest.RazonSocialCliente,
                src => src.Cliente != null ? src.Cliente.RazonSocial : string.Empty)

                .Map(dest => dest.NombrePlanificador,
                src => src.UsuarioPlanificador != null ? src.UsuarioPlanificador.NombreCompleto : string.Empty)

                .Map(dest => dest.NombreTecnico,
                src => src.TecnicoAsignado != null ? src.TecnicoAsignado.NombreCompleto : null)

                .Map(dest => dest.NombreUsuarioModificacion,
                src => src.UsuarioModificacion != null ? src.UsuarioModificacion.NombreCompleto : null)

               //esto es mas que todo para la vinculacion en el service al generar un codigo Ot
               .Map(dest => dest.CodigoOt, src => src.CodigoOT);
        }

    }
}
