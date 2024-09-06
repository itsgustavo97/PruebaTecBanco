using Application.Features.FeatureTransaccion.Vm;
using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.GenerarEstadoDeCuentaPorTarjeta
{
    public class GenerarEstadoDeCuentaPorTarjetaQuery : IRequest<EstadoCuentaVM>
    {
        public long IdTarjeta { get; set; }

        public GenerarEstadoDeCuentaPorTarjetaQuery(long idTarjeta)
        {
            IdTarjeta = idTarjeta;
        }
    }
}
