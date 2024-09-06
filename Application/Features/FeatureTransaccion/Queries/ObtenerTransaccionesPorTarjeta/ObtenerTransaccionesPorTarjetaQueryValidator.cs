using FluentValidation;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerTransaccionesPorTarjeta
{
    public class ObtenerTransaccionesPorTarjetaQueryValidator : AbstractValidator<ObtenerTransaccionesPorTarjetaQuery>
    {
        public ObtenerTransaccionesPorTarjetaQueryValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
