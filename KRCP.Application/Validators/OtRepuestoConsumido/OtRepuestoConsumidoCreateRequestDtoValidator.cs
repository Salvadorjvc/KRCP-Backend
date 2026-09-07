using FluentValidation;
using KRCP.Application.DTOs.OtRepuestoConsumido;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OtRepuestoConsumido
{
    public class OtRepuestoConsumidoCreateRequestDtoValidator : AbstractValidator<OtRepuestoConsumidoCreateRequestDto>
    {
        public OtRepuestoConsumidoCreateRequestDtoValidator()
        {
            RuleFor(x => x.OtId)
                .GreaterThan(0).WithMessage("Debe indicar una orden de trabajo válida.");

            RuleFor(x => x.ProductoId)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto válido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
        }
    }
}
