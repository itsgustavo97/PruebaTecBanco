using Application.Features.FeatureTransaccion.Dto;
using Application.Features.FeatureTransaccion.Vm;
using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerTransaccionesPorTarjeta
{
    public class ObtenerTransaccionesPorTarjetaQuery : IRequest<List<TransaccionVM>>
    {
        public long IdTarjeta { get; set; }

        public ObtenerTransaccionesPorTarjetaQuery(long idTarjeta)
        {
            IdTarjeta = idTarjeta;
        }
    }
}
