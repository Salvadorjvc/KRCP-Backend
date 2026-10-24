using FluentValidation;
using KRCP.Application.DTOs.OrdenTrabajo;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OrdenTrabajo
{
    public class OrdenTrabajoCreateRequestDtoValidator: AbstractValidator<OrdenTrabajoCreateRequestDto>
    {
        public  OrdenTrabajoCreateRequestDtoValidator()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("Debe seleccionar un cliente valido");

            RuleFor(x => x.EquipoComponente)
                .NotEmpty().WithMessage("El equipo/componente es obligatorio")
                .MaximumLength(150);

            RuleFor(x => x.NumeroSerie)
                .Cascade(CascadeMode.Stop) // Si una regla falla, frena y no evalúa las siguientes de esta propiedad
                .NotEmpty().WithMessage("El número de serie es obligatorio")
                .MinimumLength(9).WithMessage("El número de serie debe tener al menos 9 caracteres")
                .MaximumLength(100).WithMessage("El número de serie no puede superar los 100 caracteres");

            RuleFor(x => x.FechaEstimadaEntrega)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .When(x => x.FechaEstimadaEntrega.HasValue)
                .WithMessage("La fecha estimada de entrega debe ser futura.");

            RuleFor(x => x.CostoManoObra)
               .GreaterThanOrEqualTo(0).WithMessage("El costo de mano de obra no puede ser negativo");
        } 

    }
}
