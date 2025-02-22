using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Utilities;

namespace Coto.VentasAutomoviles.Domain.Interfaces;

public interface IVentaService
{
    Task<Result<Venta>> GenerarVenta(Venta venta);
    Task<Result<List<Venta>>> ObtenerVentasPorCentroDistribucion(int centroDistribucion);
    Task<Result<Dictionary<string, object>>> ObtenerVolumenTotalVentas();
    Task<Result<Dictionary<string, object>>> ObtenerVolumenVentasPorCentro(int centroDistribucion);
    Task<Result<Dictionary<int, PorcentajeVentasCentroResponse>>> ObtenerPorcentajeVentasPorModeloYCentro();
}
