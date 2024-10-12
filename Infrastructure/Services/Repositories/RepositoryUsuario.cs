using Application.Contracts.Infrastructure.IRepositories;
using Application.Features.FeatureUsuario.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Services.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly SqlConnection conn;

        public RepositoryUsuario(SqlConnection conn)
        {
            this.conn = conn;
        }

        public async Task<List<UsuarioDto>> GetAllAsync()
        {
            var result = await conn.QueryAsync<UsuarioDto>(
                @$"SELECT 
	                Id
	                ,Nombres
	                ,Apellidos
	                ,FechaNacimiento
	                ,PhoneNumber
	                ,Email
	                ,Direccion
                    ,Estado
                    ,FechaCreacion
                  FROM [PruebaTec].[dbo].[AspNetUsers]");
            return result.ToList();
        }

        public async Task<UsuarioDto> GetByIdAsync(string Id)
        {
            var result = await conn.QueryAsync<UsuarioDto>(
                @$"SELECT 
	                Id
	                ,Nombres
	                ,Apellidos
	                ,FechaNacimiento
	                ,PhoneNumber
	                ,Email
	                ,Direccion
                  FROM [PruebaTec].[dbo].[AspNetUsers]
                  WHERE Id = '{Id}'");
            return result.FirstOrDefault();
        }

    }
}
