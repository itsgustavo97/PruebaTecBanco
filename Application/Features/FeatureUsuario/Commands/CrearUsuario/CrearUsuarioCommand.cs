using Application.Dtos;
using MediatR;

namespace Application.Features.FeatureUsuario.Commands.CrearUsuario
{
    public class CrearUsuarioCommand : IRequest<UserDto>
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
