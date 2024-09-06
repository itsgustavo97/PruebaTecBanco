using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfig
{
    public class TransaccionConfig
    {
        public TransaccionConfig(EntityTypeBuilder<Transaccion> entity)
        {
            entity.Property(p => p.Tipo).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            entity.Property(p => p.Fecha).IsRequired();
            entity.Property(p => p.Descripcion).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            entity.Property(p => p.Monto).IsRequired().HasColumnType("decimal(16,4)").HasDefaultValue(0);
            entity.Property(p => p.IdTarjeta).IsRequired().HasDefaultValue(0);
        }
    }
}
