using Coto.VentasAutomoviles.Domain.Enums;

namespace Coto.VentasAutomoviles.Application.Request;

public class GenerarVentaRequest
{
    public TipoAutomovilEnum TipoAutomovil { get; set; }
    public int CentroDistribucion { get; set; }
    public int Cantidad { get; set; }    
    public DateTime FechaVenta { get; set; }
    public int ClienteId { get; set; }
}
