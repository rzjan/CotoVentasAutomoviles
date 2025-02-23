using Coto.VentasAutomoviles.Application.DTOs;
using Coto.VentasAutomoviles.Application.Features.Commands;
using Coto.VentasAutomoviles.Application.Features.Queries;
using Coto.VentasAutomoviles.Application.Request;
using Coto.VentasAutomoviles.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VentasController> _logger;

        public VentasController(IMediator mediator, ILogger<VentasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Inserta una nueva venta.
        /// </summary>
        /// <param name="command">El comando que contiene los detalles de la venta a generar.</param>
        /// <returns>Un mensaje indicando si la venta fue registrada exitosamente o no.</returns>
        /// <response code="200">Venta registrada exitosamente.</response>
        /// <response code="400">No se pudo generar la venta.</response>
        /// <response code="500">Error interno del servidor.</response>        
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarVenta([FromBody] GenerarVentaRequest request)
        {
            //validar request cantidad tiene que ser > 0
            // Validar request
            if (request.Cantidad <= 0)
            {
                return BadRequest("La cantidad de vehículos debe ser mayor a 0.");
            }                     

            var stopwatch = Stopwatch.StartNew();
            try
            {
                var command = new GenerarVentaCommand(
                    request.TipoAutomovil,
                    request.CentroDistribucion,
                    request.Cantidad,
                    request.FechaVenta,
                    request.ClienteId
                );
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return BadRequest("No se pudo generar la venta.");
                }

                return Ok("Venta registrada exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar la venta");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"GenerarVenta ejecutado en: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        /// <summary>
        /// Obtiene el volumen total de ventas.
        /// </summary>
        /// <returns>El volumen total de ventas.</returns>
        /// <response code="200">Volumen total de ventas obtenido exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerVolumenTotalDeVentas()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var resultado = await _mediator.Send(new ObtenerVolumenTotalVentasQuery());
                if (!resultado.IsSuccess)
                {
                    throw new ArgumentException(resultado.Error);
                }
                return Ok(resultado);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"ObtenerVolumenTotalDeVentas ejecutado en: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        /// <summary>
        /// Obtiene el volumen de ventas por centro de distribución.
        /// </summary>
        /// <param name="request">El DTO que contiene el ID del centro de distribución.</param>
        /// <returns>El volumen de ventas del centro de distribución especificado.</returns>
        /// <response code="200">Volumen de ventas por centro obtenido exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerVolumenDeVentasPorCentro([FromQuery] CentroDistribucionDto request)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var resultado = await _mediator.Send(new ObtenerVolumenVentasPorCentroQuery(request.IdCentro));
                if (!resultado.IsSuccess)
                {
                    throw new ArgumentException(resultado.Error);
                }
                return Ok(resultado);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"ObtenerVolumenDeVentasPorCentro ejecutado en: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        /// <summary>
        /// Obtiene el porcentaje de unidades de cada modelo vendido en cada centro.
        /// </summary>
        /// <returns>El porcentaje de ventas por modelo y centro.</returns>
        /// <response code="200">Porcentaje de ventas por modelo y centro obtenido exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<PorcentajeVentasResponse>> ObtenerPorcentajeDeVentasPorModeloYCentro()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var resultado = await _mediator.Send(new ObtenerPorcentajesVentasQuery());
                if (!resultado.IsSuccess)
                {
                    throw new ArgumentException(resultado.Error);
                }
                return Ok(resultado);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"ObtenerPorcentajeDeVentasPorModeloYCentro ejecutado en: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

    }
}
