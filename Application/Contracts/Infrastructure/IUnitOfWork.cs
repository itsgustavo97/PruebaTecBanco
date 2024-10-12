using Application.Contracts.Infrastructure.IRepositories;
using Domain.ModelBase;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Contracts.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryTransaccion RepositoryTransaccion { get; }
        IRepositoryUsuario RepositoryUsuario { get; }
        IRepositoryGeneric<T> genericRepository<T>() where T : BaseModel;
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
