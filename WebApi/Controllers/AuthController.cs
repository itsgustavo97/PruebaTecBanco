using Application.Dtos;
using Application.Features.FeatureUsuario.Commands.CrearUsuario;
using Application.Features.FeatureUsuario.Commands.EliminarUsuario;
using Application.Features.FeatureUsuario.Commands.Login;
using Application.Features.FeatureUsuario.Commands.ModificarUsuario;
using Application.Features.FeatureUsuario.Dto;
using Application.Features.FeatureUsuario.Queries.ObtenerUsuarioPorId;
using Application.Features.FeatureUsuario.Queries.ObtenerUsuarios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpGet("ObtenerUsuariosAsync")]
        [ProducesResponseType(typeof(List<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UsuarioDto>>> ObtenerUsuariosAsync()
        {
            return Ok(await mediator.Send(new ObtenerUsuariosQuery()));
        }

        [HttpGet("ObtenerUsuarioPorIdAsync/{Id}")]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UsuarioDto>> ObtenerUsuarioPorIdAsync(string Id)
        {
            return Ok(await mediator.Send(new ObtenerUsuarioPorIdQuery(Id)));
        }

        [HttpPost("CrearUsuarioAsync")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> CrearUsuarioAsync([FromBody] CrearUsuarioCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("ModificarUsuarioAsync")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> ModificarUsuarioAsync([FromBody] ModificarUsuarioCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> EliminarUsuarioAsync(string Id)
        {
            return Ok(await mediator.Send(new EliminarUsuarioCommand(Id)));
        }
    }
}
