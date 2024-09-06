using FluentValidation;

namespace Application.Features.FeatureTransaccion.Queries.ExportarEstadoCuentaPdf
{
    internal class ExportarEstadoCuentaPdfQueryValidator : AbstractValidator<ExportarEstadoCuentaPdfQuery>
    {
        public ExportarEstadoCuentaPdfQueryValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
