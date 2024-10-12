using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.FeatureUsuario.Commands.ModificarUsuario
{
    public class ModificarUsuarioCommandHandler : IRequestHandler<ModificarUsuarioCommand, UserDto>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;

        public ModificarUsuarioCommandHandler(UserManager<Usuario> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(ModificarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await userManager.FindByIdAsync(request.Id);
            if (userToUpdate is null)
                throw new NotFoundException(nameof(Usuario), request.Id);
            if (request.Email != userToUpdate.Email)
            {
                var existeCorreo = await userManager.FindByEmailAsync(request.Email) is null ? false : true;
                if (existeCorreo)
                    throw new BadRequestException("El correo electrónico ya está en uso");
            }
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(userToUpdate);
            var changePass = await userManager.ResetPasswordAsync(userToUpdate, resetToken, request.Password);
            mapper.Map(request, userToUpdate, typeof(ModificarUsuarioCommand), typeof(Usuario));
            var result = await userManager.UpdateAsync(userToUpdate);
            if (!result.Succeeded)
                throw new BadRequestException("No se pudo actualizar el usuario");
            return new UserDto()
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                FechaNacimiento = request.FechaNacimiento,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                Direccion = request.Direccion,
            };
        }
    }
}
