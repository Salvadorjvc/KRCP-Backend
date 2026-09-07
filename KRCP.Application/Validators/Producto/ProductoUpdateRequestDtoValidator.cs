using FluentValidation;
using KRCP.Application.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Producto
{
    public class ProductoUpdateRequestDtoValidator: AbstractValidator<ProductoUpdateRequestDto>
    {
        public ProductoUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacio")
                .MaximumLength(150);

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoria valida");

            RuleFor(x => x.StockMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("El stock minimo no puede ser negativo");

            RuleFor(x => x.CostoUnitario)
                .GreaterThan(0).WithMessage("El costo unitario debe ser mayor a 0.");

        }
    }
}
