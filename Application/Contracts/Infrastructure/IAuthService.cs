using Domain;

namespace Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Tuple<string, RefreshToken> CreateToken(Usuario user, IList<string>? roles);
    }
}
