using Application.Features.FeatureUsuario.Dto;
using MediatR;

namespace Application.Features.FeatureUsuario.Queries.ObtenerUsuarios
{
    public class ObtenerUsuariosQuery : IRequest<List<UsuarioDto>>
    {
    }
}
