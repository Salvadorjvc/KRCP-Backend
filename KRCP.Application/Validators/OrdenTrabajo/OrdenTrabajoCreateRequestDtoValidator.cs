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
                .NotEmpty().WithMessage("El numero de serie es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.UsuarioPlanificadorId)
                .GreaterThan(0).WithMessage("Debe seleccionar un usuario planificador valido");

            RuleFor(x => x.FechaEstimadaEntrega)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.FechaEstimadaEntrega.HasValue)
                .WithMessage("La fecha estimada de entrega debe ser futura.");
        } 

    }
}
