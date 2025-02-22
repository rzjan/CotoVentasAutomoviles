using Coto.VentasAutomoviles.Domain.Enums;

namespace Coto.VentasAutomoviles.Domain.Interfaces;

// Capa Domain: Interfaz que define el método para crear una estrategia
public interface IPrecioStrategyFactory
{
    IPreciosStrategy CrearEstrategia(TipoAutomovilEnum tipoAuto);
}
