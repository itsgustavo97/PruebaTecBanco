using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ModelConfig
{
    public class ExecuteDomainConfigurations
    {
        public ExecuteDomainConfigurations(ModelBuilder builder)
        {
            new TransaccionConfig(builder.Entity<Transaccion>());
            new TarjetaConfig(builder.Entity<Tarjeta>());
        }
    }
}
