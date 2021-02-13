using ComplaintsManagement.Infrastructure.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Validators
{
    public class CustomersValidator: AbstractValidator<CostumersDto>
    {
        public CustomersValidator()
        {
            RuleFor(e => e.LastName).NotNull().WithMessage("El campo 'Apellido' es requerido");
            RuleFor(e => e.Name).NotNull().WithMessage("El campo 'Nombre' es requerido");
            RuleFor(e => e.DocumentNumber).NotNull().WithMessage("El campo 'Cédula' es requerido");
            RuleFor(e => e.PhoneNumber).NotNull().WithMessage("El campo 'Teléfono' es requerido");
            RuleFor(e => e.Email).NotNull().EmailAddress().WithMessage("El campo 'Teléfono' es requerido");
        }
    }
}