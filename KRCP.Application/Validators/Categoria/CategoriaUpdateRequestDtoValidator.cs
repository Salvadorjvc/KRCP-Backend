using FluentValidation;
using KRCP.Application.DTOs.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Categoria
{
    public class CategoriaUpdateRequestDtoValidator: AbstractValidator<CategoriaUpdateRequestDto>
    {
        public CategoriaUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255).WithMessage("La descripción no puede superar los 255 caracteres.");
        }
    }
}
