using FluentValidation;

namespace Application.Features.FeatureTransaccion.Commands.NuevaCompra
{
    public class NuevaCompraCommandValidator : AbstractValidator<NuevaCompraCommand>
    {
        public NuevaCompraCommandValidator()
        {
            RuleFor(p => p.IdTarjeta).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Fecha).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Descripcion).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
            RuleFor(p => p.Monto).NotNull().NotEmpty().WithMessage("{PropertyName} es requerido");
        }
    }
}
