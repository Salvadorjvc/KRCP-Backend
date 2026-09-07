using FluentValidation;
using KRCP.Application.DTOs.OrdenTrabajo;
using KRCP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OrdenTrabajo
{
    public class CambiarEstadoRequestDtoValidator : AbstractValidator<CambiarEstadoRequestDto>
    {
        public CambiarEstadoRequestDtoValidator()
        {
            RuleFor(x => x.NuevoEstado)
                .IsInEnum().WithMessage("El estado indicado no es válido.");
        }
    }
}
