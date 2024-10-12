using Application.Contracts.Infrastructure;
using Application.Features.FeatureUsuario.Dto;
using MediatR;

namespace Application.Features.FeatureUsuario.Queries.ObtenerUsuarios
{
    public class ObtenerUsuariosQueryHandler : IRequestHandler<ObtenerUsuariosQuery, List<UsuarioDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObtenerUsuariosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UsuarioDto>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _unitOfWork.RepositoryUsuario.GetAllAsync();
            return usuarios;
        }
    }
}
