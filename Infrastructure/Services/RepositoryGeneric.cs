using Application.Contracts.Infrastructure;
using Domain.ModelBase;
using Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : BaseModel
    {
        private ApplicationDBContext DBContext;

        public RepositoryGeneric(ApplicationDBContext dBContext)
        {
            DBContext = dBContext;
        }

        public void Insert(T entity)
        {
            DBContext.Set<T>().Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await DBContext.Set<T>().AddAsync(entity);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await DBContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(long id) =>
            await DBContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public void Update(T entity)
        {
            DBContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => DBContext.Entry(entity).State = EntityState.Modified);
        }

        public async Task DeleteByIdAsync(long id)
        {
            DBContext.Set<T>().Remove(await GetByIdAsync(id));
        }

        public IQueryable<T> GetQuery() =>
            DBContext.Set<T>().AsQueryable();

    }
}
