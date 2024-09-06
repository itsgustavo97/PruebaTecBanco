using Application.Contracts.Infrastructure.IRepositories;
using Application.Features.FeatureTransaccion.Vm;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Services.Repositories
{
    public class RepositoryTransaccion : IRepositoryTransaccion
    {
        private readonly SqlConnection conn;

        public RepositoryTransaccion(SqlConnection conn)
        {
            this.conn = conn;
        }

        public async Task<List<TransaccionVM>> ObtenerHistorialTransaccionesPorTarjeta(long IdTarjeta)
        {
            var historial = await conn.QueryAsync<TransaccionVM>(@"exec PruebaTec.dbo.usp_ObtenerHistorialTransaccionesPorTarjeta @IdTarjeta", new { IdTarjeta });
            return historial.ToList();
        }
    }
}
