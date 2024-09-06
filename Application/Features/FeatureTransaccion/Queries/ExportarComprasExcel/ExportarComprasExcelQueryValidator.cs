using FluentValidation;

namespace Application.Features.FeatureTransaccion.Queries.ExportarComprasExcel
{
    public class ExportarComprasExcelQueryValidator : AbstractValidator<ExportarComprasExcelQuery>
    {
        public ExportarComprasExcelQueryValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
