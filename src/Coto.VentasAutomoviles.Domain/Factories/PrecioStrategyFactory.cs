//using Coto.VentasAutomoviles.Domain.Enums;
//using Coto.VentasAutomoviles.Domain.Interfaces;
//using Coto.VentasAutomoviles.Domain.Strategies;

//namespace Coto.VentasAutomoviles.Domain.Factories;

//// Capa Domain: Implementación concreta de la Factory
//public class PrecioStrategyFactory : IPrecioStrategyFactory
//{
//    public IPreciosStrategy CrearEstrategia(TipoAutomovilEnum tipoAuto)
//    {
//        return tipoAuto switch
//        {
//            TipoAutomovilEnum.Sedan => new PreciosAutosStrategy(TipoAutomovilEnum.Sedan),
//            TipoAutomovilEnum.Suv => new PreciosAutosStrategy(TipoAutomovilEnum.Suv),
//            TipoAutomovilEnum.Offroad => new PreciosAutosStrategy(TipoAutomovilEnum.Offroad),
//            TipoAutomovilEnum.Sport => new PreciosAutosStrategy(TipoAutomovilEnum.Sport),
//            _ => throw new ArgumentException("Tipo de auto no válido")
//        };
//    }
//}
