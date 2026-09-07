using FluentValidation;
using KRCP.Application.DTOs.MovimientoKardex;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.MovimientoKardex
{
    public class MovimientoKardexCreateRequestDtoValidator : AbstractValidator<MovimientoKardexCreateRequestDto>
    {
        public MovimientoKardexCreateRequestDtoValidator()
        {
            RuleFor(x => x.ProductoId)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto válido.");

            RuleFor(x => x.TipoMovimiento)
                .IsInEnum().WithMessage("El tipo de movimiento no es válido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.Motivo)
                .MaximumLength(255);
        }
    }
}
