using MediatR;

namespace Application.Features.FeatureTransaccion.Commands.NuevoPago
{
    public class NuevoPagoCommand : IRequest
    {
        public string Tipo { get; set; } = "Pago"; //Compra o pago
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = "Pago / abono";
        public decimal Monto { get; set; }
        public long IdTarjeta { get; set; }
    }
}
