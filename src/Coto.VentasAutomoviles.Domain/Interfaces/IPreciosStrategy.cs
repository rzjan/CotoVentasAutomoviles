using Coto.VentasAutomoviles.Domain.Enums;

namespace Coto.VentasAutomoviles.Domain.Interfaces;

public interface IPreciosStrategy
{
    //decimal CalcularPrecio();    
    (decimal PrecioBase, decimal PrecioFinal) CalcularPrecio(TipoAutomovilEnum tipoAutomovil);

}
