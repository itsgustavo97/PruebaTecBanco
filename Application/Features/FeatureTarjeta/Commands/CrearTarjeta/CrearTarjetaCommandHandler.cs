using Application.Contracts.Infrastructure;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.FeatureTarjeta.Commands.CrearTarjeta
{
    public class CrearTarjetaCommandHandler : IRequestHandler<CrearTarjetaCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrearTarjetaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CrearTarjetaCommand request, CancellationToken cancellationToken)
        {
            var tarjeta = _mapper.Map<Tarjeta>(request);
            tarjeta.SaldoDisponible = tarjeta.Limite;
            await _unitOfWork.genericRepository<Tarjeta>().InsertAsync(tarjeta);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new BadRequestException("No se pudo guardar");
            return Unit.Value;
        }
    }
}
