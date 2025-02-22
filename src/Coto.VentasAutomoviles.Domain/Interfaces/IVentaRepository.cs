using Coto.VentasAutomoviles.Domain.Entities;

namespace Coto.VentasAutomoviles.Domain.Interfaces;

public interface IVentaRepository : IGenericRepository<Venta>
{    
    public Task<List<Venta>> ObtenerVentasPorCentroDistribucion(int centroDistribucion);    
    Task<Dictionary<string, object>> ObtenerVolumenTotalVentas();
    Task<Dictionary<string, object>> ObtenerVolumenVentasPorCentro(int centroDistribucion);
}
