using Application.Dtos;
using MediatR;

namespace Application.Features.FeatureUsuario.Commands.Login
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public string Telefono { get; set; }
        public string Password { get; set; }
    }
}
