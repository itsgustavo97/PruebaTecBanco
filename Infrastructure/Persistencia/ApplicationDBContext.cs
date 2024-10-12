using Domain;
using Infrastructure.ModelConfig;
using Infrastructure.ModelConfig.SecurityConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistencia
{
    public class ApplicationDBContext : IdentityDbContext<Usuario>
    {
        private readonly IHttpContextAccessor _accessor;
        //constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new ExecuteDomainConfigurations(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<Tarjeta> Tarjeta { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


    }
}
