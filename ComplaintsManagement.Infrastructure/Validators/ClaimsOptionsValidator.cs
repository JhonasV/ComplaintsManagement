using ComplaintsManagement.Domain.DTOs;
using FluentValidation;

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