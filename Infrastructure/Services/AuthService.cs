using Application.Contracts.Infrastructure;
using Application.Models.Security;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings jwtSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor,
            TokenValidationParameters tokenValidationParameters, IUnitOfWork unitOfWork)
        {
            this.jwtSettings = jwtSettings.Value;
            this.httpContextAccessor = httpContextAccessor;
            _tokenValidationParameters = tokenValidationParameters;
            this.unitOfWork = unitOfWork;
        }

        public Tuple<string, RefreshToken> CreateToken(Usuario user, IList<string>? roles)
        {
            var symmetrictSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.UtcNow.Add(jwtSettings.ExpireTime),
                SigningCredentials = new SigningCredentials(symmetrictSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", user.Id),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("userId", user.Id)
                }.Union(roleClaims))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = tokenHandler.WriteToken(token);
            var resfreshToken = new RefreshToken
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                FechaCreado = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(1),
                Token = $"{GenerateRandomTokenCharacters(35)}{Guid.NewGuid()}"
            };

            return new Tuple<string, RefreshToken>(jwtToken, resfreshToken);
        }

        private string GenerateRandomTokenCharacters(int length)
        {
            var random = new Random();
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }
    }
}
