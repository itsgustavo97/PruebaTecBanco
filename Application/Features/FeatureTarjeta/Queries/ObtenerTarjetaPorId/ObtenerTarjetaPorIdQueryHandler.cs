using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Features.FeatureTarjeta.Dto;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.FeatureTarjeta.Queries.ObtenerTarjetaPorId
{
    public class ObtenerTarjetaPorIdQueryHandler : IRequestHandler<ObtenerTarjetaPorIdQuery, TarjetaDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerTarjetaPorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TarjetaDto> Handle(ObtenerTarjetaPorIdQuery request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.genericRepository<Tarjeta>().GetByIdAsync(request.Id);
            if (tarjeta == null)
                throw new NotFoundException(nameof(Tarjeta), request.Id);
            return _mapper.Map<TarjetaDto>(tarjeta);
        }
    }
}
