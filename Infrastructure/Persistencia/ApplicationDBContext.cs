using Domain;
using Infrastructure.ModelConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistencia
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;
        //constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new ExecuteDomainConfigurations(builder);
            base.OnModelCreating(builder);
        }

        public DbSet<Tarjeta> Tarjeta { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }

        
    }
}
