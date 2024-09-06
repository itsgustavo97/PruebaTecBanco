using Application.Features.FeatureTransaccion.Commands.NuevaCompra;
using Application.Features.FeatureTransaccion.Commands.NuevoPago;
using Application.Features.FeatureTransaccion.Queries.ExportarComprasExcel;
using Application.Features.FeatureTransaccion.Queries.ExportarEstadoCuentaPdf;
using Application.Features.FeatureTransaccion.Queries.GenerarEstadoDeCuentaPorTarjeta;
using Application.Features.FeatureTransaccion.Queries.ObtenerComprasDelMesPorTarjeta;
using Application.Features.FeatureTransaccion.Queries.ObtenerTransaccionesPorTarjeta;
using Application.Features.FeatureTransaccion.Vm;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private IMediator mediator;

        public MovimientoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CrearNuevaCompraAsync")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> CrearNuevaCompraAsync([FromBody] NuevaCompraCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPost("CrearNuevoPagoAsync")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> CrearNuevoPagoAsync([FromBody] NuevoPagoCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpGet("ObtenerHistorialTransaccionesPorTarjetaAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(List<TransaccionVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TransaccionVM>>> ObtenerHistorialTransaccionesPorTarjetaAsync(long IdTarjeta)
        {
            return Ok(await mediator.Send(new ObtenerTransaccionesPorTarjetaQuery(IdTarjeta)));
        }

        [HttpGet("ObtenerComprasDelMesPorTarjetaAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(List<TransaccionVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TransaccionVM>>> ObtenerComprasDelMesPorTarjetaAsync(long IdTarjeta)
        {
            return Ok(await mediator.Send(new ObtenerComprasDelMesPorTarjetaQuery(IdTarjeta)));
        }

        [HttpGet("GenerarEstadoDeCuentaPorTarjetaAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(List<TransaccionVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TransaccionVM>>> GenerarEstadoDeCuentaPorTarjetaAsync(long IdTarjeta)
        {
            return Ok(await mediator.Send(new GenerarEstadoDeCuentaPorTarjetaQuery(IdTarjeta)));
        }

        [HttpGet("ExportarPdfEstadoDeCuentaPorTarjetaAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(byte[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<byte[]>> ExportarPdfEstadoDeCuentaPorTarjetaAsync(long IdTarjeta)
        {
            var file = await mediator.Send(new ExportarEstadoCuentaPdfQuery(IdTarjeta));
            var bs64File = Convert.ToBase64String(file);
            return Ok(new { File = bs64File });
        }

        [HttpGet("ExportarExcelComprasDelMesPorTarjetaAsync/{IdTarjeta}")]
        [ProducesResponseType(typeof(byte[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<byte[]>> ExportarExcelComprasDelMesPorTarjetaAsync(long IdTarjeta)
        {
            var reportBytes = await mediator.Send(new ExportarComprasExcelQuery(IdTarjeta));
            Response.Headers.Add("Content-Disposition", reportBytes.ToString());
            Response.Headers.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return File(reportBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @$"ReporteReclamos_{DateTime.Now.ToShortDateString()}.xlsx");
        }
    }
}
