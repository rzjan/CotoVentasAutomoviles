using Coto.VentasAutomoviles.Domain.Enums;

namespace Coto.VentasAutomoviles.Domain.Utilities;

public class PorcentajeVentasCentroResponse
{
    public string NombreCentro { get; set; }
    public double PorcentajeTotalCentro { get; set; }
    public int CantidadTotalCentro { get; set; }
    public List<ModeloPorcentaje> Modelos { get; set; }

    public class ModeloPorcentaje
    {
        public TipoAutomovilEnum TipoAutomovil { get; set; }
        public double Porcentaje { get; set; }
        public int Cantidad { get; set; }
    }
}
