using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Interfaces;

namespace Coto.VentasAutomoviles.Domain.Strategies
{
    /// <summary>
    /// Implementación optimizada del patrón Strategy para calcular el precio de los autos.
    /// Se usa un enum para evitar la repetición de código.
    /// Si en el futuro cada tipo de auto tiene reglas más complejas, se puede modificar 
    /// a una implementación con clases separadas.
    /// </summary>
    public class PreciosAutosStrategy : IPreciosStrategy
    {
        private readonly TipoAutomovilEnum _tipoAuto;

        public PreciosAutosStrategy(TipoAutomovilEnum tipoAuto)
        {
            _tipoAuto = tipoAuto;
        }

        public (decimal PrecioBase, decimal PrecioFinal) CalcularPrecio(TipoAutomovilEnum tipoAutomovil)
        {
            decimal precioBase = tipoAutomovil switch
            {
                TipoAutomovilEnum.Sedan => 8000m,
                TipoAutomovilEnum.Suv => 9500m,
                TipoAutomovilEnum.Offroad => 12500m,
                TipoAutomovilEnum.Sport => 18200m,
                _ => throw new ArgumentException("Modelo no válido")
            };

            // Aplicar impuesto del 7% solo si es "Sport"
            decimal precioFinal = tipoAutomovil == TipoAutomovilEnum.Sport
                ? precioBase * 1.07m
                : precioBase;

            return (precioBase, precioFinal);
        }
    }
}
