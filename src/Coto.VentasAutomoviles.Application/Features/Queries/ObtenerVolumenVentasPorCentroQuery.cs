using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.Queries;

public class ObtenerVolumenVentasPorCentroQuery : IRequest<Result<VolumenVentasPorCentroResponse>>
{
    public int CentroDistribucion { get; set; }

    public ObtenerVolumenVentasPorCentroQuery(int centroDistribucion)
    {
        CentroDistribucion = centroDistribucion;
    }
}
