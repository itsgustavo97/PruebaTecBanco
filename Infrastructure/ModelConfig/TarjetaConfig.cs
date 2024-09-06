using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfig
{
    public class TarjetaConfig
    {
        public TarjetaConfig(EntityTypeBuilder<Tarjeta> entity)
        {
            entity.Property(p => p.Titular).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            entity.Property(p => p.Numero).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            entity.Property(p => p.FechaVencimiento).IsRequired();
            entity.Property(p => p.Limite).IsRequired().HasColumnType("decimal(16,4)").HasDefaultValue(0);
            entity.Property(p => p.SaldoActual).IsRequired().HasColumnType("decimal(16,4)").HasDefaultValue(0);
            entity.Property(p => p.SaldoDisponible).IsRequired().HasColumnType("decimal(16,4)").HasDefaultValue(0);
            entity.Property(p => p.InteresBonificable).IsRequired().HasColumnType("decimal(16,4)").HasDefaultValue(0);
            entity.Property(p => p.PorcentajePagoMinimo).IsRequired().HasDefaultValue(0);
            entity.Property(p => p.PorcentajeInteres).IsRequired().HasDefaultValue(0);
        }
    }
}
