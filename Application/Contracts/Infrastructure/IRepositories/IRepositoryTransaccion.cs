using Application.Features.FeatureTransaccion.Vm;

namespace Application.Contracts.Infrastructure.IRepositories
{
    public interface IRepositoryTransaccion
    {
        Task<List<TransaccionVM>> ObtenerHistorialTransaccionesPorTarjeta(long IdTarjeta);
    }
}
