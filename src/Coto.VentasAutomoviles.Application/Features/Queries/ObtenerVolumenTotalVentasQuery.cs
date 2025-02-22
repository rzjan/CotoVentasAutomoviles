using Coto.VentasAutomoviles.Application.Responses;
using Coto.VentasAutomoviles.Domain.Utilities;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.Queries;

public class ObtenerVolumenTotalVentasQuery:IRequest<Result<VolumenTotalVentasResponse>>
{    
}
