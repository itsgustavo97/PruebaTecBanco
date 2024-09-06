using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.Configurations;
using Security.Identity;

namespace Security.Persistencia
{
    public class SecurityDBContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityDBContext(DbContextOptions<SecurityDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration()); 
            builder.ApplyConfiguration(new UserConfiguration()); 
            builder.ApplyConfiguration(new UserRoleConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
