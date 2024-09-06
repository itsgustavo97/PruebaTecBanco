using Application.Contracts.Security;
using Application.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Identity;
using Security.Persistencia;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Security.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;
        private readonly SecurityDBContext context;
        private readonly TokenValidationParameters tokenParamaters;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, SecurityDBContext context, TokenValidationParameters tokenParamaters)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
            this.context = context;
            this.tokenParamaters = tokenParamaters;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Usuario o contraseña incorrectos");
            var validacionContraseña = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!validacionContraseña.Succeeded)
                throw new Exception("Usuario o contraseña incorrectos");
            var token = await GenerateToken(user);
            var userRol = await userManager.GetRolesAsync(user);
            return new AuthResponse() { Email = user.Email, IdUser = user.Id, Rol = userRol[0], Token = token };
        }

        private async Task<string> GenerateToken(ApplicationUser appUser)
        {
            var userClaims = await userManager.GetClaimsAsync(appUser);
            var roles = await userManager.GetRolesAsync(appUser);
            List<Claim> rolesClaims = new List<Claim>();
            foreach(var rol in roles)
            {
                rolesClaims.Add(new Claim(ClaimTypes.Role, rol));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim("IdUser", appUser.Id)
            }.Union(rolesClaims).Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer:jwtSettings.Issuer, 
                audience: jwtSettings.Audience, 
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signInCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
