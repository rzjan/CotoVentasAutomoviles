using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Enums;
using MediatR;

namespace Coto.VentasAutomoviles.Application.Features.Commands;

public class GenerarVentaCommand : IRequest<bool>
{
    public TipoAutomovilEnum TipoAutomovil { get; set; }// EnumModeloVehiculo
    public int CentroDistribucion { get; set; }
    public int Cantidad { get; set; }    
    public DateTime FechaVenta { get; set; }
    public int ClienteId { get; set; }

    public GenerarVentaCommand(TipoAutomovilEnum tipoAutomovil, int centroDistribucion, int cantidad, DateTime fechaVenta, int clienteId)
    {
        TipoAutomovil = tipoAutomovil;
        CentroDistribucion = centroDistribucion;
        Cantidad = cantidad;        
        FechaVenta = fechaVenta;
        ClienteId = clienteId;
    }
}
