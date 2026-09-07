using FluentValidation;
using KRCP.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Usuario
{
    public class UsuarioUpdateRequestDtoValidator : AbstractValidator<UsuarioUpdateRequestDto>
    {
        public UsuarioUpdateRequestDtoValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(100);

            RuleFor(x => x.RolId)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        }
    }
}
