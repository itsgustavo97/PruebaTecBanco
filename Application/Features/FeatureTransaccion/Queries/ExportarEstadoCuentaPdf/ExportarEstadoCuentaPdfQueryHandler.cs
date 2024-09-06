using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Features.FeatureTransaccion.Dto;
using Application.Features.FeatureTransaccion.Vm;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeatureTransaccion.Queries.ExportarEstadoCuentaPdf
{
    public class ExportarEstadoCuentaPdfQueryHandler : IRequestHandler<ExportarEstadoCuentaPdfQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ExportarEstadoCuentaPdfQueryHandler(IUnitOfWork unitOfWork, IReportService reportService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(ExportarEstadoCuentaPdfQuery request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.genericRepository<Tarjeta>().GetByIdAsync(request.IdTarjeta);
            if (tarjeta == null)
                throw new NotFoundException(nameof(Tarjeta), request.IdTarjeta);
            var fechaActual = DateTime.Now;
            var primerDiaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            var ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            var compras = await _unitOfWork.genericRepository<Transaccion>().GetQuery()
                .Where(x => x.IdTarjeta == request.IdTarjeta && x.Fecha >= primerDiaDelMes && x.Fecha <= ultimoDiaDelMes && x.Tipo == "Compra").ToListAsync();
            var transaccionesVM = _mapper.Map<List<TransaccionDto>>(compras);
            decimal totalComprasMes = 0m;
            transaccionesVM.ForEach(c => totalComprasMes += c.Monto);
            var estadoCuenta = new EstadoCuentaDto();
            #region ViewModel
            estadoCuenta.IdTarjeta = tarjeta.Id;
            estadoCuenta.Titular = tarjeta.Titular;
            estadoCuenta.Numero = tarjeta.Numero;
            estadoCuenta.FechaVencimiento = tarjeta.FechaVencimiento;
            estadoCuenta.Limite = tarjeta.Limite;
            estadoCuenta.SaldoActual = tarjeta.SaldoActual;
            estadoCuenta.SaldoDisponible = tarjeta.SaldoDisponible;
            estadoCuenta.PorcentajePagoMinimo = tarjeta.PorcentajePagoMinimo;
            estadoCuenta.PorcentajeInteres = tarjeta.PorcentajeInteres;
            estadoCuenta.ListaComprasMes = transaccionesVM;
            estadoCuenta.SaldoTotalComprasMes = totalComprasMes;
            estadoCuenta.InteresBonificable = totalComprasMes * (decimal)(tarjeta.PorcentajeInteres / 100);
            estadoCuenta.CuotaMinima = totalComprasMes * (decimal)(tarjeta.PorcentajePagoMinimo / 100);
            estadoCuenta.TotalContadoAPagar = totalComprasMes;
            estadoCuenta.TotalContadoMasInteresBonificable = totalComprasMes + estadoCuenta.InteresBonificable;
            #endregion
            var report = _reportService.GenerarEstadoDeCuentaPDF(estadoCuenta);
            return report;
        }
    }
}
