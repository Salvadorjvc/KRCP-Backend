using FluentValidation;
using KRCP.Application.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Cliente
{
    public class ClienteUpdateRequestDtoValidator : AbstractValidator<ClienteUpdateRequestDto>
    {
        public ClienteUpdateRequestDtoValidator() 
        {
            RuleFor(x => x.RazonSocial)
                 .NotEmpty().WithMessage("La razón social es obligatoria.")
                 .MaximumLength(150);

            RuleFor(x => x.ContactoEmail)
                .EmailAddress().WithMessage("El email de contacto no es válido.")
                .When(x => !string.IsNullOrEmpty(x.ContactoEmail));

            RuleFor(x => x.Telefono)
                .MaximumLength(20);
        }
    }
}
