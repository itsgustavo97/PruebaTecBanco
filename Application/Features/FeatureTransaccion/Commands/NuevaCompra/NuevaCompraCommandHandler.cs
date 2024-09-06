using Application.Contracts.Infrastructure;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.FeatureTransaccion.Commands.NuevaCompra
{
    public class NuevaCompraCommandHandler : IRequestHandler<NuevaCompraCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NuevaCompraCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(NuevaCompraCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.genericRepository<Tarjeta>().GetByIdAsync(request.IdTarjeta);
            if(tarjeta.SaldoDisponible <= request.Monto)
                throw new BadRequestException("El saldo disponible es insuficiente");
            var fechaActual = DateTime.Now;
            request.Fecha = request.Fecha.AddHours(fechaActual.Hour);
            request.Fecha = request.Fecha.AddMinutes(fechaActual.Minute);
            await _unitOfWork.genericRepository<Transaccion>().InsertAsync(_mapper.Map<Transaccion>(request));
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new BadRequestException("No se pudo guardar");
            tarjeta.SaldoActual = tarjeta.SaldoActual + request.Monto;
            tarjeta.SaldoDisponible = tarjeta.SaldoDisponible - request.Monto;
            await _unitOfWork.genericRepository<Tarjeta>().UpdateAsync(tarjeta);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new BadRequestException("No se pudo guardar");
            return Unit.Value;
        }
    }
}
