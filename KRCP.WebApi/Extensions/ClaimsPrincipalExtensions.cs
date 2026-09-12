using System.Security.Claims;

namespace KRCP.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // Obtiene el ID del usuario autenticado
        public static int GetUsuarioId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if(claim is null)
            {
                throw new UnauthorizedAccessException("No se pudo obtener el usuario autenticado");
            }

            return int.Parse(claim.Value);
        }
    }
}
