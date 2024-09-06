using Application.Features.FeatureTransaccion.Dto;

namespace Application.Contracts.Infrastructure
{
    public interface IReportService
    {
        byte[] GenerarEstadoDeCuentaPDF(EstadoCuentaDto data);
        byte[] GenerarReporteExcel(List<TransaccionDto> data);
    }
}
