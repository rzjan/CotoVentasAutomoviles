using Coto.VentasAutomoviles.Application.Features.Queries;
using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.QueriesHandler;

public class ObtenerVolumenVentasPorCentroHandler : IRequestHandler<ObtenerVolumenVentasPorCentroQuery, Result<VolumenVentasPorCentroResponse>>
{
    private readonly IVentaService _ventaService;

    public ObtenerVolumenVentasPorCentroHandler(IVentaService ventaService)
    {
        _ventaService = ventaService;
    }

    public async Task<Result<VolumenVentasPorCentroResponse>> Handle(ObtenerVolumenVentasPorCentroQuery request, CancellationToken cancellationToken)
    {
        var response = await _ventaService.ObtenerVolumenVentasPorCentro(request.CentroDistribucion);

        if (!response.IsSuccess)
        {
            return Result<VolumenVentasPorCentroResponse>.Failure(response.Error);
        }

        var volumenVentasPorCentroResponse = new VolumenVentasPorCentroResponse
        {
            CentroDistribucion = request.CentroDistribucion,
            VolumenVentas = new VolumenTotalVentasResponse
            {
                CantidadTotalVendida = (int)response.Value["CantidadTotalVendida"],
                MontoTotalVendido = (decimal)response.Value["MontoTotalVendido"]
            }
        };

        return Result<VolumenVentasPorCentroResponse>.Success(volumenVentasPorCentroResponse);
    }
}
