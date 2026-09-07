using FluentValidation;
using KRCP.Application.DTOs.Ubicacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Ubicacion
{
    public class UbicacionCreateRequestDtoValidator: AbstractValidator<UbicacionCreateRequestDto>
    {
        public UbicacionCreateRequestDtoValidator()
        {
            RuleFor(x => x.CodigoUbicacion)
                .NotEmpty().WithMessage("El codigo de ubicación es obligatorio")
                .MaximumLength(50).WithMessage("El código no puede superar los 50 caracteres");

            RuleFor(x => x.Descripcion)
                .MaximumLength(150).WithMessage("La descripción no puede superar los 150 caracteres");
        }
    }
}
