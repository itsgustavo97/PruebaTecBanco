using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Features.FeatureTransaccion.Vm;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeatureTransaccion.Queries.GenerarEstadoDeCuentaPorTarjeta
{
    public class GenerarEstadoDeCuentaPorTarjetaQueryHandler : IRequestHandler<GenerarEstadoDeCuentaPorTarjetaQuery, EstadoCuentaVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerarEstadoDeCuentaPorTarjetaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EstadoCuentaVM> Handle(GenerarEstadoDeCuentaPorTarjetaQuery request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.genericRepository<Tarjeta>().GetByIdAsync(request.IdTarjeta);
            if (tarjeta == null)
                throw new NotFoundException(nameof(Tarjeta), request.IdTarjeta);
            var fechaActual = DateTime.Now;
            var primerDiaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            var ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            var compras = await _unitOfWork.genericRepository<Transaccion>().GetQuery()
                .Where(x => x.IdTarjeta == request.IdTarjeta && x.Fecha >= primerDiaDelMes && x.Fecha <= ultimoDiaDelMes && x.Tipo == "Compra").ToListAsync();
            var transaccionesVM = _mapper.Map<List<TransaccionVM>>(compras);
            decimal totalComprasMes = 0m;
            transaccionesVM.ForEach(c => totalComprasMes += c.Monto);
            #region ViewModel
            var estadoCuenta = new EstadoCuentaVM();
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
            return estadoCuenta;
        }
    }
}
