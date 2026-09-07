using FluentValidation;
using KRCP.Application.DTOs.OrdenTrabajo;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OrdenTrabajo
{
    public class AsignarTecnicoRequestDtoValidator : AbstractValidator<AsignarTecnicoRequestDto>
    {
        public AsignarTecnicoRequestDtoValidator()
        {
            RuleFor(x => x.TecnicoId)
                .GreaterThan(0).WithMessage("Debe seleccionar un técnico válido.");
        }
    }
}
