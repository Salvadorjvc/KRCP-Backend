using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using KRCP.Application.DTOs.Usuario;

namespace KRCP.Application.Validators.Usuario
{
    public class CambiarPasswordRequestDtoValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public CambiarPasswordRequestDtoValidator()
        {
            RuleFor(x => x.PasswordActual)
                .NotEmpty().WithMessage("Debe ingresar su contraseña actual.");

            RuleFor(x => x.PasswordNueva)
                .NotEmpty().WithMessage("La nueva contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La nueva contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.");
        }
    }
}
