using FluentValidation;
using KRCP.Application.DTOs.OtEvidencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Validators.OtEvidenciaService
{
    public class OtEvidenciaCreateRequestDtoValidator: AbstractValidator<OtEvidenciaCreateRequestDto>
    {
        public OtEvidenciaCreateRequestDtoValidator()
        {
            RuleFor(x => x.OtId)
                .NotEmpty().WithMessage("La orden de trabajo es obligatoria");

            RuleFor(x => x.UrlArchivo)
                .NotEmpty().WithMessage("La URL del archivo es obligatoria.")
                .MaximumLength(500)
                .Must(url => url.StartsWith("http://") || url.StartsWith("https://"))
                    .WithMessage("La URL debe ser una dirección web válida.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255);

            RuleFor(x => x.TipoEvidencia)
                .IsInEnum().WithMessage("El tipo de evidencia no es válido.");
        }
    }
}
