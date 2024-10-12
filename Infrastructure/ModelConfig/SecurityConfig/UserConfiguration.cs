using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfig.SecurityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(p => p.Nombres).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.Apellidos).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.FechaNacimiento).IsRequired();
            builder.Property(p => p.Direccion).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(p => p.PhoneNumber).IsRequired().HasColumnType("varchar").HasMaxLength(12);
            builder.Property(p => p.Email).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(p => p.Estado).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.FechaModificacion).IsRequired(false);
            var hasher = new PasswordHasher<Usuario>();
            builder.HasData(
                    new Usuario
                    {
                        Id = "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                        Email = "gtfofo97@gmail.com",
                        NormalizedEmail = "GTFOFO97@GMAIL.COM",
                        Nombres = "Gustavo",
                        Apellidos = "Pineda",
                        PhoneNumber = "72465626",
                        Direccion = "Sonsonate centro",
                        FechaNacimiento = new DateTime(1997, 3, 10),
                        FechaCreacion = DateTime.Now,
                        UserName = "gtfofo97",
                        NormalizedUserName = "GTFOFO97",
                        PasswordHash = hasher.HashPassword(null, "ABCDabcd1234$"),
                        EmailConfirmed = true,
                        Estado = true,
                    }

                );
        }
    }
}
