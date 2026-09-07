using FluentValidation;
using KRCP.Application.DTOs.Rol;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Rol
{
    public class RolCreateRequestDtoValidator : AbstractValidator<RolCreateRequestDto>
    {
        public RolCreateRequestDtoValidator()
        {
            RuleFor(x => x.NombreRol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del rol no puede superar los 50 caracteres.");
        }
    }
}
