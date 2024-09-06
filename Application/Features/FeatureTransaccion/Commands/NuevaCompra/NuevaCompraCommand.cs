using MediatR;

namespace Application.Features.FeatureTransaccion.Commands.NuevaCompra
{
    public class NuevaCompraCommand : IRequest
    {
        public string Tipo { get; set; } = "Compra"; //Compra o pago
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public long IdTarjeta { get; set; }

    }
}
