using KRCP.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace KRCP.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
               HttpContext httpContext,
               Exception exception,
               CancellationToken cancellationToken)
        {
            var (statusCode, clientMessage) = exception switch
            {
                EntityNotFoundException => (HttpStatusCode.NotFound, exception.Message),          // 404
                DuplicateEntityException => (HttpStatusCode.Conflict, exception.Message),         // 409
                EntityNotActiveException => (HttpStatusCode.BadRequest, exception.Message),       // 400
                StockInsuficienteException => (HttpStatusCode.BadRequest, exception.Message),     // 400
                OrdenTrabajoCerradaException => (HttpStatusCode.BadRequest, exception.Message),   // 400
                TransicionEstadoInvalidaException => (HttpStatusCode.Conflict, exception.Message), // 409
                InvalidCredentialsException => (HttpStatusCode.Unauthorized, exception.Message),  // 401
                _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno en el servidor.") // 500
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, "Ocurrió un error no controlado en la API");
            }
            else
            {
                _logger.LogWarning("Excepción de negocio ({StatusCode}): {Message}", (int)statusCode, exception.Message);
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = (int)statusCode,
                message = clientMessage
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);

            return true;
        }
    }
}
