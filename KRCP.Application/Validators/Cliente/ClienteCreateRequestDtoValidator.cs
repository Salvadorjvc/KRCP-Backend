using FluentValidation;
using KRCP.Application.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Cliente
{
    public class ClienteCreateRequestDtoValidator: AbstractValidator<ClienteCreateRequestDto>
    {
        public ClienteCreateRequestDtoValidator()
        {
            RuleFor(x => x.RUC)
                .NotEmpty().WithMessage("El RUC es obligatorio.")
                .Length(11).WithMessage("El RUC debe tener exactamente 11 dígitos.")
                .Matches(@"^\d+$").WithMessage("El RUC solo debe contener números.");

            RuleFor(x => x.RazonSocial)
                .NotEmpty().WithMessage("La razón social es obligatoria.")
                .MaximumLength(150);

            RuleFor(x => x.ContactoEmail)
                .EmailAddress().WithMessage("El email de contacto no es válido.")
                .When(x => !string.IsNullOrEmpty(x.ContactoEmail)); // solo valida formato si mandaron algo, ya que es opcional

            RuleFor(x => x.Telefono)
                .MaximumLength(20);
        }
    }
}
