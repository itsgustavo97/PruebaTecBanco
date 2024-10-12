using FluentValidation;

namespace Application.Features.FeatureUsuario.Commands.ModificarUsuario
{
    public class ModificarUsuarioCommandValidator : AbstractValidator<ModificarUsuarioCommand>
    {
        public ModificarUsuarioCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
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
