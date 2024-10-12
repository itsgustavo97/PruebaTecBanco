using Application.Contracts.Infrastructure;
using Application.Contracts.Infrastructure.IRepositories;
using Domain.ModelBase;
using Infrastructure.Persistencia;
using Infrastructure.Services.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext db;
        private Hashtable repositories;
        private SqlConnection conn;
        private readonly IConfiguration config;
        private IRepositoryTransaccion repositoryTransaccion;
        private IRepositoryUsuario repositoryUsuario;

        public UnitOfWork(ApplicationDBContext db, IConfiguration config)
        {
            this.db = db;
            this.config = config;
        }

        public IRepositoryTransaccion RepositoryTransaccion => repositoryTransaccion ??= new RepositoryTransaccion(GetConnectionSql());
        public IRepositoryUsuario RepositoryUsuario => repositoryUsuario ??= new RepositoryUsuario(GetConnectionSql());

        public void Dispose() => db.Dispose();
        public async Task<IDbContextTransaction> BeginTransactionAsync() =>
            await db.Database.BeginTransactionAsync();
        public async Task<int> SaveChangesAsync() => await db.SaveChangesAsync();

        public IRepositoryGeneric<T> genericRepository<T>() where T : BaseModel
        {
            if (repositories == null)
                repositories = new Hashtable();

            var tipo = typeof(T).Name;
            if (!repositories.ContainsKey(tipo))
            {
                var repositoryType = typeof(RepositoryGeneric<>);
                var instanceRepo = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), db);
                repositories.Add(tipo, instanceRepo);
            }
            return (IRepositoryGeneric<T>)repositories[tipo];
        }

        private SqlConnection GetConnectionSql()
        {
            if (conn == null)
                conn = new SqlConnection(config.GetConnectionString("default"));

            return conn;
        }
    }
}
