using FluentValidation;

namespace Application.Features.FeatureUsuario.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.Telefono).NotEmpty().WithMessage("El telefono no puede ir vacio")
                                 .NotNull().WithMessage("El telefono no puede ir nulo");
            RuleFor(p => p.Password).NotEmpty().WithMessage("La contraseña no puede ir vacia")
                                 .NotNull().WithMessage("La contraseña no puede ir nula");
        }
    }
}
