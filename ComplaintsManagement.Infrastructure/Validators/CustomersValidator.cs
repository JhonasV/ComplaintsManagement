using ComplaintsManagement.Domain.DTOs;
using FluentValidation;

namespace ComplaintsManagement.Infrastructure.Validators
{
    public class CustomersValidator: AbstractValidator<UsersDto>
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