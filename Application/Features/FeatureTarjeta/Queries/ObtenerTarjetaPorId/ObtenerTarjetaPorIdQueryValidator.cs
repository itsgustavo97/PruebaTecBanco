using FluentValidation;

namespace Application.Features.FeatureTarjeta.Queries.ObtenerTarjetaPorId
{
    public class ObtenerTarjetaPorIdQueryValidator : AbstractValidator<ObtenerTarjetaPorIdQuery>
    {
        public ObtenerTarjetaPorIdQueryValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
