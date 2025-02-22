namespace Coto.VentasAutomoviles.Domain.ValueObjects;

public class TipoVehiculo
{
    public string Nombre { get; }
    public decimal PrecioBase { get; }
    public decimal Impuesto { get; }

    private static readonly Dictionary<string, (decimal Precio, decimal Impuesto)> Precios = new()
    {
        { "Sedan", (8000m, 0m) },
        { "Suv", (9500m, 0m) },
        { "Offroad", (12500m, 0m) },
        { "Sport", (18200m, 0.07m) } // 7% extra de impuesto
    };

    public TipoVehiculo(string nombre)
    {
        if (!Precios.ContainsKey(nombre))
            throw new ArgumentException("Tipo de vehículo no válido", nameof(nombre));

        Nombre = nombre;
        (PrecioBase, Impuesto) = Precios[nombre];
    }

    public (decimal PrecioBase, decimal PrecioFinal) CalcularPrecio()
    {
        decimal precioFinal = PrecioBase * (1 + Impuesto);
        return (PrecioBase, precioFinal);
    }

    public override bool Equals(object obj)
    {
        return obj is TipoVehiculo other && Nombre == other.Nombre;
    }

    public override int GetHashCode() => Nombre.GetHashCode();

    public override string ToString() => $"{Nombre} - {PrecioBase:C}";
}
