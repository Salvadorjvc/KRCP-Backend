using KRCP.Application.DTOs.OtEvidencia;
using KRCP.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Mappings
{
    public class OtEvidenciaMappingConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OtEvidencia, OtEvidenciaResponseDto>()
                .Map(dest => dest.CodigoOT,
                src => src.OrdenTrabajo != null ? src.OrdenTrabajo.CodigoOT : string.Empty)

                .Map(dest => dest.UsuarioCarga,
                src => src.UsuarioCarga != null ? src.UsuarioCarga.NombreCompleto : string.Empty);
        }
    }
}
