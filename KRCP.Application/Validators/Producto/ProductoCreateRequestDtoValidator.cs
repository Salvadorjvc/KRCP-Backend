using FluentValidation;
using KRCP.Application.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.Producto
{
    public class ProductoCreateRequestDtoValidator: AbstractValidator<ProductoCreateRequestDto>
    {
        public ProductoCreateRequestDtoValidator()
        {
            RuleFor(x => x.CodigoParte)
                .NotEmpty().WithMessage("El codigo de parte del producto es obligatorio")
                .MaximumLength(50);

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto no puede estar vacio")
                .MaximumLength(150);

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida.");

            RuleFor(x => x.StockMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("El stock mínimo no puede ser negativo.");

            RuleFor(x => x.CostoUnitario)
                .GreaterThan(0).WithMessage("El costo unitario debe ser mayor a 0.");
        }
    }
}
