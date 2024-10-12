using Application.Contracts.Infrastructure;
using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeatureUsuario.Commands.CrearUsuario
{
    public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, UserDto>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;

        public CrearUsuarioCommandHandler(UserManager<Usuario> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var existeEmail = await userManager.Users.Where(c => c.Email == request.Email).FirstOrDefaultAsync() is not null;
            if (existeEmail)
                throw new BadRequestException("El correo electrónico ya está en uso");
            var user = mapper.Map<Usuario>(request);
            user.UserName = request.Email;
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException("Ocurrió un problema inesperado, no se guardó el usuario.");
            return new UserDto() 
            { 
                Nombres = user.Nombres, 
                Apellidos = user.Apellidos, 
                FechaNacimiento = user.FechaNacimiento,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = request.Password,
                Direccion = user.Direccion,
            };
        }
    }
}
