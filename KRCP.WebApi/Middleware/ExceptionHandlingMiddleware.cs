using KRCP.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace KRCP.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        } 

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //este logError registra el error en los logs del servidor(util para debugging)
            _logger.LogError(exception, "Ocurrio un error no controlado");

            var statusCode = exception switch
            {
                EntityNotFoundException => HttpStatusCode.NotFound,      // 404
                DuplicateEntityException => HttpStatusCode.Conflict,     //409
                EntityNotActiveException => HttpStatusCode.BadRequest,   //400
                StockInsuficienteException => HttpStatusCode.BadRequest,   //400
                OrdenTrabajoCerradaException => HttpStatusCode.BadRequest,  //400
                TransicionEstadoInvalidaException => HttpStatusCode.Conflict, //409
                InvalidCredentialsException => HttpStatusCode.Unauthorized, //401
                _ => HttpStatusCode.InternalServerError             //500 (otros errores no esperados)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;


            var response = new
            {
                statusCode = (int)statusCode,
                message = exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);

        }

    }
}
