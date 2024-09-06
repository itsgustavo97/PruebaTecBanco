using Application.Features.FeatureTarjeta.Commands.CrearTarjeta;
using Application.Features.FeatureTarjeta.Commands.ModificarTarjeta;
using Application.Features.FeatureTarjeta.Dto;
using Application.Features.FeatureTarjeta.Queries.ObtenerTarjetaPorId;
using Application.Features.FeatureTarjeta.Queries.ObtenerTarjetas;
using Application.Features.FeatureTransaccion.Queries.ObtenerTransaccionesPorTarjeta;
using Application.Features.FeatureTransaccion.Vm;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private IMediator mediator;

        public TarjetaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CrearNuevaTarjetaAsync")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> CrearNuevaTarjetaAsync([FromBody] CrearTarjetaCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        //[HttpPut("ModificarTarjetaAsync")]
        //[ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Unit>> ModificarTarjetaAsync([FromBody] ModificarTarjetaCommand request)
        //{
        //    return Ok(await mediator.Send(request));
        //}

        [HttpGet("ObtenerTarjetasAsync")]
        [ProducesResponseType(typeof(List<TarjetaDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TarjetaDto>>> ObtenerTarjetasAsync()
        {
            return Ok(await mediator.Send(new ObtenerTarjetasQuery()));
        }

        [HttpGet("ObtenerTarjetaPorIdAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(TarjetaDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TarjetaDto>> ObtenerTarjetaPorIdAsync(long IdTarjeta)
        {
            return Ok(await mediator.Send(new ObtenerTarjetaPorIdQuery(IdTarjeta)));
        }
    }
}
