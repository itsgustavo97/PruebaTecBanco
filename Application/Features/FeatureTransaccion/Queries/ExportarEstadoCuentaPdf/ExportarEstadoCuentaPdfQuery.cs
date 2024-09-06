using Application.Features.FeatureTransaccion.Vm;
using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.ExportarEstadoCuentaPdf
{
    public class ExportarEstadoCuentaPdfQuery : IRequest<byte[]>
    {
        public long IdTarjeta { get; set; }

        public ExportarEstadoCuentaPdfQuery(long idTarjeta)
        {
            IdTarjeta = idTarjeta;
        }
    }
}
