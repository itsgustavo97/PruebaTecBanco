using Application.Contracts.Infrastructure;
using Application.Features.FeatureTransaccion.Vm;
using AutoMapper;
using MediatR;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerTransaccionesPorTarjeta
{
    public class ObtenerTransaccionesPorTarjetaQueryHandler : IRequestHandler<ObtenerTransaccionesPorTarjetaQuery, List<TransaccionVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerTransaccionesPorTarjetaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TransaccionVM>> Handle(ObtenerTransaccionesPorTarjetaQuery request, CancellationToken cancellationToken)
        {
            var transacciones = await _unitOfWork.RepositoryTransaccion.ObtenerHistorialTransaccionesPorTarjeta(request.IdTarjeta);
            return transacciones;
        }
    }
}
