using KRCP.Application.DTOs.Usuario;
using KRCP.Domain.Entities;
using Mapster;

namespace KRCP.Application.Mappings
{
    public class UsuarioMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Usuario, UsuarioResponseDto>()
                .Map(dest => dest.NombreRol, src => src.Rol != null ? src.Rol.NombreRol : string.Empty );
        }
    }
}
