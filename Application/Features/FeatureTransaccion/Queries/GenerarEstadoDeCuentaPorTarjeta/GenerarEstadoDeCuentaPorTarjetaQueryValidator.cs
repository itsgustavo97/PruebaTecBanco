using FluentValidation;

namespace Application.Features.FeatureTransaccion.Queries.GenerarEstadoDeCuentaPorTarjeta
{
    public class GenerarEstadoDeCuentaPorTarjetaQueryValidator : AbstractValidator<GenerarEstadoDeCuentaPorTarjetaQuery>
    {
        public GenerarEstadoDeCuentaPorTarjetaQueryValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
