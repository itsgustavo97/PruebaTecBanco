using Application.Contracts.Infrastructure;
using Application.Dtos;
using Application.Exceptions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Features.FeatureUsuario.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;

        public LoginCommandHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IAuthService authService, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.Where(x => x.PhoneNumber == request.Telefono).FirstOrDefaultAsync();
            if (user == null)
                throw new BadRequestException("No hay coincidencias para este número de telefono");
            if (!user.Estado)
                throw new BadRequestException("Usuario desactivado, contacte a servicio técnico");
            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new BadRequestException("Credenciales incorrectas");
            var tokenGenerado = authService.CreateToken(user, new List<string>() { "Administrador"});
            await unitOfWork.genericRepository<RefreshToken>().InsertAsync(tokenGenerado.Item2);
            await unitOfWork.SaveChangesAsync();
            return new LoginDto(user.Id, user.Nombres, user.Apellidos, user.FechaNacimiento, user.Direccion, user.PhoneNumber, user.Email, user.PasswordHash, tokenGenerado.Item1);
        }
    }
}
