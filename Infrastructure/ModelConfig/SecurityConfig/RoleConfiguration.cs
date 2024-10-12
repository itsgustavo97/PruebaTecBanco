using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfig.SecurityConfig
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            builder.HasData(new IdentityRole()
            {
                Id = "02f40ed9-7b62-4c05-a89b-3c26794daae2",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = "3a606748-2258-4152-adf6-7619ee40b89a"
            });
        }
    }
}
