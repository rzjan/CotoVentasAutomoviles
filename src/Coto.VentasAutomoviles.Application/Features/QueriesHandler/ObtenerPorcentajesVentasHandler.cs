using Azure;
using Coto.VentasAutomoviles.Application.Features.Queries;
using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.QueriesHandler
{
    public class ObtenerPorcentajesVentasHandler : IRequestHandler<ObtenerPorcentajesVentasQuery, Result<PorcentajeVentasResponse>>
    {
        private readonly IVentaService _ventaService;

        public ObtenerPorcentajesVentasHandler(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        public async Task<Result<PorcentajeVentasResponse>> Handle(ObtenerPorcentajesVentasQuery request, CancellationToken cancellationToken)
        {
            var response = await _ventaService.ObtenerPorcentajeVentasPorModeloYCentro();

            if (!response.IsSuccess)
            {
                return Result<PorcentajeVentasResponse>.Failure(response.Error);
            }

            var porcentajeVentasResponse = new PorcentajeVentasResponse
            {
                Centros = response.Value.ToDictionary(
                    kv => kv.Key,
                    kv => new PorcentajeVentasResponse.CentroVentas
                    {
                        NombreCentro = kv.Value.NombreCentro,
                        PorcentajeTotalCentro = kv.Value.PorcentajeTotalCentro,
                        CantiadTotalCentro = kv.Value.CantidadTotalCentro,
                        Modelos = kv.Value.Modelos.Select(m => new PorcentajeVentasResponse.ModeloPorcentaje
                        {
                            TipoAutomovil = m.TipoAutomovil,
                            Porcentaje = m.Porcentaje,
                            Cantidad = m.Cantidad
                        }).ToList()
                    }
                )
            };

            return Result<PorcentajeVentasResponse>.Success(porcentajeVentasResponse);
        }
    }
}
