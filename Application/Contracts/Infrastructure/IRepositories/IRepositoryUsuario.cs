using Application.Features.FeatureUsuario.Dto;

namespace Application.Contracts.Infrastructure.IRepositories
{
    public interface IRepositoryUsuario
    {
        Task<List<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(string Id);
    }
}
