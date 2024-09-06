using Domain.ModelBase;

namespace Application.Contracts.Infrastructure
{
    public interface IRepositoryGeneric<T> where T : BaseModel
    {
        IQueryable<T> GetQuery();
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        void Update(T entity);
        void Insert(T entity);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);

    }
}
