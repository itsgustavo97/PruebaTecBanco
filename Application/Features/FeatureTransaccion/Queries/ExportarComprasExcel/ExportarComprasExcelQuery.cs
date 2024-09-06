using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.ExportarComprasExcel
{
    public class ExportarComprasExcelQuery : IRequest<byte[]>
    {
        public long IdTarjeta { get; set; }

        public ExportarComprasExcelQuery(long idTarjeta)
        {
            IdTarjeta = idTarjeta;
        }
    }
}
