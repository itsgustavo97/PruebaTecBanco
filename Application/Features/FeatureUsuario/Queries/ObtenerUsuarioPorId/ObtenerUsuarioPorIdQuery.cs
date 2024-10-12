using Application.Features.FeatureUsuario.Dto;
using MediatR;

namespace Application.Features.FeatureUsuario.Queries.ObtenerUsuarioPorId
{
    public class ObtenerUsuarioPorIdQuery : IRequest<UsuarioDto>
    {
        public string Id { get; set; }

        public ObtenerUsuarioPorIdQuery(string id)
        {
            Id = id;
        }
    }
}
