using FluentValidation;

namespace Application.Features.FeatureTarjeta.Commands.CrearTarjeta
{
    public class CrearTarjetaCommandValidator : AbstractValidator<CrearTarjetaCommand>
    {
        public CrearTarjetaCommandValidator()
        {
            RuleFor(p => p.Titular).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Numero).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido")
                .Matches(@"^\d+$").WithMessage("El valor debe contener solo números.");
            RuleFor(p => p.Limite).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.FechaVencimiento).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido")
                .GreaterThan(DateTime.Now.Date).WithMessage("{PropertyName} debe ser posterior a la fecha actual");
            RuleFor(p => p.PorcentajePagoMinimo).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.PorcentajeInteres).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
