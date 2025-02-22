namespace Coto.VentasAutomoviles.Domain.ValueObjects;

//Los Value Objects representan conceptos inmutables, como Centro de Distribución.
//public class CentroDistribucion
//{
//    public string Nombre { get; }

//    public CentroDistribucion(string nombre)
//    {
//        if (string.IsNullOrWhiteSpace(nombre))
//        {
//            throw new ArgumentException("El nombre del centro de distribución no puede ser nulo o vacío", nameof(nombre));
//        }
//        Nombre = nombre;
//    }
//    public override string ToString() => Nombre;    
//}
public class CentroDistribucion
{
    public int Id { get; }
    public string Nombre { get; }

    // Lista de centros válidos
    private static readonly HashSet<string> CentrosValidos = new()
    {
        "Centro Norte",
        "Centro Sur",
        "Centro Este",
        "Centro Oeste"
    };

    // Constructor privado para evitar instanciación sin validación
    private CentroDistribucion(int id, string nombre)
    {
        if (id <= 0)
            throw new ArgumentException($"El ID del centro de distribución debe ser mayor que cero: {id}");

        if (!CentrosValidos.Contains(nombre))
            throw new ArgumentException($"Centro de distribución inválido: {nombre}");

        Id = id;
        Nombre = nombre;
    }   

    public override bool Equals(object obj)
    {
        return obj is CentroDistribucion other && Id == other.Id && Nombre == other.Nombre;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Nombre);

    public override string ToString() => $"{Nombre} (ID: {Id})";
}
