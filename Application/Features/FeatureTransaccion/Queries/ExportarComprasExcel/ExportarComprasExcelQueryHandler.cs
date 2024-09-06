using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Features.FeatureTransaccion.Dto;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeatureTransaccion.Queries.ExportarComprasExcel
{
    public class ExportarComprasExcelQueryHandler : IRequestHandler<ExportarComprasExcelQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ExportarComprasExcelQueryHandler(IUnitOfWork unitOfWork, IReportService reportService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(ExportarComprasExcelQuery request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.genericRepository<Tarjeta>().GetByIdAsync(request.IdTarjeta);
            if (tarjeta == null)
                throw new NotFoundException(nameof(Tarjeta), request.IdTarjeta);
            var fechaActual = DateTime.Now;
            var primerDiaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            var ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            var compras = await _unitOfWork.genericRepository<Transaccion>().GetQuery().Where(x => x.IdTarjeta == request.IdTarjeta && x.Fecha >= primerDiaDelMes && x.Fecha <= ultimoDiaDelMes && x.Tipo == "Compra").ToListAsync();
            var comprasVM = _mapper.Map<List<TransaccionDto>>(compras);
            var report = _reportService.GenerarReporteExcel(comprasVM);
            return report;
        }
    }
}
