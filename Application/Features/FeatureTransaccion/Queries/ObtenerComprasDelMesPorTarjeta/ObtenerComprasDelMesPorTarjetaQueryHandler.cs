using Application.Contracts.Infrastructure;
using Application.Features.FeatureTransaccion.Vm;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeatureTransaccion.Queries.ObtenerComprasDelMesPorTarjeta
{
    public class ObtenerComprasDelMesPorTarjetaQueryHandler : IRequestHandler<ObtenerComprasDelMesPorTarjetaQuery, List<TransaccionVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerComprasDelMesPorTarjetaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TransaccionVM>> Handle(ObtenerComprasDelMesPorTarjetaQuery request, CancellationToken cancellationToken)
        {
            var fechaActual = DateTime.Now;
            var primerDiaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            var ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            var compras = await _unitOfWork.genericRepository<Transaccion>().GetQuery().Where(x => x.IdTarjeta == request.IdTarjeta && x.Fecha >= primerDiaDelMes && x.Fecha <= ultimoDiaDelMes && x.Tipo == "Compra").ToListAsync();
            return _mapper.Map<List<TransaccionVM>>(compras);
        }
    }
}
