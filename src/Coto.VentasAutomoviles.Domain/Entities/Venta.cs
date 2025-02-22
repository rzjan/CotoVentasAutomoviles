using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.ValueObjects;

namespace Coto.VentasAutomoviles.Domain.Entities;

public class Venta
{    
    public Guid Id { get; set; }
    public int CentroDistribucionId { get; set; }    
    public int ModeloVehiculoId { get; set; }    
    public int Cantidad { get; set; }    
    public DateTime FechaDeVenta { get; set; }    
    public decimal PrecioDeVenta { get; set; }    
    public decimal PrecioBase { get; set; }    
    public int ClienteId { get; set; }   
    public TipoAutomovilEnum Vehiculo { get; private set; }    
    public Venta() { }
    
    public Venta(TipoAutomovilEnum vehiculo, int centroDistribucionId, DateTime fechaDeVenta)
    {
        if (centroDistribucionId <= 0)
        {
            throw new ArgumentException("El ID del centro de distribución debe ser positivo.", nameof(centroDistribucionId));
        }

        if (fechaDeVenta > DateTime.Now)
        {
            throw new ArgumentException("La fecha de venta no puede ser en el futuro.", nameof(fechaDeVenta));
        }

        Vehiculo = vehiculo;
        CentroDistribucionId = centroDistribucionId;
        FechaDeVenta = fechaDeVenta;
        CalcularPrecios(vehiculo);
    }
    
    private void CalcularPrecios(TipoAutomovilEnum vehiculo)
    {
        var precios = TipoVehiculoPrecios.Precios[vehiculo];
        PrecioBase = precios.Precio;
        PrecioDeVenta = PrecioBase + (PrecioBase * precios.Impuesto);
    }
}

public static class TipoVehiculoPrecios
{    
    public static readonly Dictionary<TipoAutomovilEnum, (decimal Precio, decimal Impuesto)> Precios = new()
        {
            { TipoAutomovilEnum.Sedan, (8000m, 0m) },
            { TipoAutomovilEnum.Suv, (9500m, 0m) },
            { TipoAutomovilEnum.Offroad, (12500m, 0m) },
            { TipoAutomovilEnum.Sport, (18200m, 0.07m) } // 7% extra de impuesto
        };
}
