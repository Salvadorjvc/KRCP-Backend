using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using KRCP.Application.DTOs.Usuario;

namespace KRCP.Application.Validators.Usuario
{
    public class UsuarioCreateRequestDtoValidator: AbstractValidator<UsuarioCreateRequestDto>
    {
        public UsuarioCreateRequestDtoValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El emaill no es valido") // este emailadress comprueba que tenga el formato email
                .MaximumLength(100);


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.");

            RuleFor(x => x.RolId)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido");
        }
        
    }
}
