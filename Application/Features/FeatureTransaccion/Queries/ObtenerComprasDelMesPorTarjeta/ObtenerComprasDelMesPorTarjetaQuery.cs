using Application.Features.FeatureTransaccion.Vm;
using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerComprasDelMesPorTarjeta
{
    public class ObtenerComprasDelMesPorTarjetaQuery : IRequest<List<TransaccionVM>>
    {
        public long IdTarjeta { get; set; }

        public ObtenerComprasDelMesPorTarjetaQuery(long idTarjeta)
        {
            IdTarjeta = idTarjeta;
        }
    }
}
