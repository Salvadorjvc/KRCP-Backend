using FluentValidation;
using KRCP.Application.DTOs.OrdenTrabajo;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OrdenTrabajo
{
    public class OrdenTrabajoUpdateRequestDtoValidator: AbstractValidator<OrdenTrabajoUpdateRequestDto>
    {
        public OrdenTrabajoUpdateRequestDtoValidator()
        {
            RuleFor(x => x.EquipoComponente)
                .NotEmpty().WithMessage("El equipo/componente es obligatorio")
                .MaximumLength(150);

            RuleFor(x => x.NumeroSerie)
                .NotEmpty().WithMessage("El numero de serie es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.FechaEstimadaEntrega)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.FechaEstimadaEntrega.HasValue)
                .WithMessage("La fecha estimada de entrega debe ser futura.");

            RuleFor(x => x.CostoManoObra)
                .GreaterThanOrEqualTo(0).WithMessage("El costo de mano de obra no puede ser negativo.");
        }
    }
}
