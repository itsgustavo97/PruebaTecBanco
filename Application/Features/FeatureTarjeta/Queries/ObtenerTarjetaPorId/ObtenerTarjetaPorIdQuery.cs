using Application.Features.FeatureTarjeta.Dto;
using MediatR;

namespace Application.Features.FeatureTarjeta.Queries.ObtenerTarjetaPorId
{
    public class ObtenerTarjetaPorIdQuery : IRequest<TarjetaDto>
    {
        public long Id { get; set; }

        public ObtenerTarjetaPorIdQuery(long id)
        {
            Id = id;
        }
    }
}
