using ComplaintsManagement.Infrastructure.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Validators
{
    public class ClaimsOptionsValidator : AbstractValidator<ClaimsOptionsDto>
    {
        public ClaimsOptionsValidator()
        {
            RuleFor(e => e.Name).NotNull().WithMessage("El campo 'Nombre' es requerido").MinimumLength(5).MaximumLength(500);


        }
    }
}