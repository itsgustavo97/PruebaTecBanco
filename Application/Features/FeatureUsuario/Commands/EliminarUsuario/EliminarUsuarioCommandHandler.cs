using Application.Dtos;
using Application.Exceptions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.FeatureUsuario.Commands.EliminarUsuario
{
    public class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, UserDto>
    {
        private readonly UserManager<Usuario> userManager;

        public EliminarUsuarioCommandHandler(UserManager<Usuario> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserDto> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user is null)
                throw new NotFoundException(nameof(Usuario), request.Id);
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException("No se pudo actualizar el usuario");
            return new UserDto()
            {
                Id = user.Id,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                FechaNacimiento = user.FechaNacimiento,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Direccion = user.Direccion,
            };
        }
    }
}
