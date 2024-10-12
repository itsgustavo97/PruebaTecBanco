using Application.Contracts.Infrastructure;
using Application.Features.FeatureUsuario.Dto;
using MediatR;

namespace Application.Features.FeatureUsuario.Queries.ObtenerUsuarioPorId
{
    public class ObtenerUsuarioPorIdQueryHandler : IRequestHandler<ObtenerUsuarioPorIdQuery, UsuarioDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObtenerUsuarioPorIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioDto> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.RepositoryUsuario.GetByIdAsync(request.Id);
            return usuario;
        }
    }
}
