using Application.Contracts.Infrastructure;
using Application.Features.FeatureTarjeta.Dto;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.FeatureTarjeta.Queries.ObtenerTarjetas
{
    public class ObtenerTarjetasQueryHandler : IRequestHandler<ObtenerTarjetasQuery, List<TarjetaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerTarjetasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TarjetaDto>> Handle(ObtenerTarjetasQuery request, CancellationToken cancellationToken)
        {
            var tarjetas = await _unitOfWork.genericRepository<Tarjeta>().GetAllAsync();
            return _mapper.Map<List<TarjetaDto>>(tarjetas.ToList());
        }
    }
}
