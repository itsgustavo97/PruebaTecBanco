using FluentValidation;

namespace Application.Features.FeatureUsuario.Commands.EliminarUsuario
{
    public class EliminarUsuarioCommandValidator : AbstractValidator<EliminarUsuarioCommand>
    {
        public EliminarUsuarioCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
