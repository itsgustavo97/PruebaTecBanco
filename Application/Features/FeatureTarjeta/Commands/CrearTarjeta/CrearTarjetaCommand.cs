using MediatR;

namespace Application.Features.FeatureTarjeta.Commands.CrearTarjeta
{
    public class CrearTarjetaCommand : IRequest
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Limite { get; set; }
        public double PorcentajePagoMinimo { get; set; }
        public double PorcentajeInteres { get; set; }
    }
}
