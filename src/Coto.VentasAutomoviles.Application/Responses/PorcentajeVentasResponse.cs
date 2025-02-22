using Coto.VentasAutomoviles.Domain.Enums;

namespace Coto.VentasAutomoviles.Application.Responses;

public class PorcentajeVentasResponse
{
    public Dictionary<int, CentroVentas> Centros { get; set; }

    public class CentroVentas
    {
        public string NombreCentro { get; set; }
        public double PorcentajeTotalCentro { get; set; }
        public int CantiadTotalCentro { get; set; }
        public List<ModeloPorcentaje> Modelos { get; set; }
    }

    public class ModeloPorcentaje
    {
        public TipoAutomovilEnum TipoAutomovil { get; set; }
        public double Porcentaje { get; set; }
        public int Cantidad { get; set; }
    }
}