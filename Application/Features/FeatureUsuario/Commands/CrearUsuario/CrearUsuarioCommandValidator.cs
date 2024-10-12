using FluentValidation;

namespace Application.Features.FeatureUsuario.Commands.CrearUsuario
{
    public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
    {
        public CrearUsuarioCommandValidator()
        {
            RuleFor(p => p.Nombres).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Apellidos).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.FechaNacimiento).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.PhoneNumber).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Email).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido")
                .EmailAddress().WithMessage("{PropertyName} no tiene el formato adecuado");
            RuleFor(p => p.Password).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Direccion).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
