using Application.Dtos;
using MediatR;

namespace Application.Features.FeatureUsuario.Commands.EliminarUsuario
{
    public class EliminarUsuarioCommand : IRequest<UserDto>
    {
        public string Id { get; set; }

        public EliminarUsuarioCommand(string id)
        {
            Id = id;
        }
    }
}
