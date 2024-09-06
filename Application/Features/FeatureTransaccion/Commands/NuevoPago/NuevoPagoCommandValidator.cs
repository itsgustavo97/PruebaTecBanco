using FluentValidation;

namespace Application.Features.FeatureTransaccion.Commands.NuevoPago
{
    public class NuevoPagoCommandValidator : AbstractValidator<NuevoPagoCommand>
    {
        public NuevoPagoCommandValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Tipo).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Fecha).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Monto).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
