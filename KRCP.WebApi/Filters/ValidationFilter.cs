using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KRCP.WebApi.Filters
{
    // IAsyncActionFilter: Filtro que intercepta la petición antes de que llegue al controlador
    public class ValidationFilter: IAsyncActionFilter
    {
        //IServiceProvider: Contenedor que permite buscar dinámicamente el validador correcto
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Recorre cada argumento que recibe el controlador (ej. los DTOs)
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null) continue;

                // Busca dinámicamente si existe un validador de FluentValidation para este DTO
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                if (_serviceProvider.GetService(validatorType) is not IValidator validator) continue;

                // Ejecuta las reglas de validación de forma asíncrona
                var result = await validator.ValidateAsync(new ValidationContext<object>(argument));
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }
            await next();
        }
    }
}
