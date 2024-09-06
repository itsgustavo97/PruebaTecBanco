using Application.Features.FeatureTarjeta.Dto;
using MediatR;

namespace Application.Features.FeatureTarjeta.Queries.ObtenerTarjetas
{
    public class ObtenerTarjetasQuery : IRequest<List<TarjetaDto>>
    {
    }
}
