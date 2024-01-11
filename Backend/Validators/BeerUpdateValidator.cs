﻿using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(e => e.Id).NotNull().WithMessage("El id es obligatorio");
            RuleFor(e => e.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(e => e.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(e => e.BrandID).NotNull().WithMessage("La marga es obligatoria");
            RuleFor(e => e.BrandID).GreaterThan(0).WithMessage("El id de la marca debe ser mayor a cero");
            RuleFor(e => e.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a cero");
        }
    }
}
