using FluentValidation;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerComprasDelMesPorTarjeta
{
    public class ObtenerComprasDelMesPorTarjetaQueryValidator : AbstractValidator<ObtenerComprasDelMesPorTarjetaQuery>
    {
        public ObtenerComprasDelMesPorTarjetaQueryValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
