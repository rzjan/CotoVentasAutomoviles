using Coto.VentasAutomoviles.Application.Features.Queries;
using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.QueriesHandler;

public class ObtenerVolumenTotalVentasHandler : IRequestHandler<ObtenerVolumenTotalVentasQuery, Result<VolumenTotalVentasResponse>>
{    
    //llamar al Servio ventas
    private readonly IVentaService _ventaService;

    public ObtenerVolumenTotalVentasHandler(IVentaService ventaService)
    {
        _ventaService = ventaService;
    }

    public async Task<Result<VolumenTotalVentasResponse>> Handle(ObtenerVolumenTotalVentasQuery request, CancellationToken cancellationToken)
    {
        var result = await _ventaService.ObtenerVolumenTotalVentas();

        if (!result.IsSuccess)
        {
            return Result<VolumenTotalVentasResponse>.Failure(result.Error);
        }

        var response = new VolumenTotalVentasResponse
        {
            CantidadTotalVendida = (int)result.Value["CantidadTotalVendida"],
            MontoTotalVendido = (decimal)result.Value["MontoTotalVendido"]
        };

        return Result<VolumenTotalVentasResponse>.Success(response);
    }
}
