using Coto.VentasAutomoviles.Domain.Entities;

namespace Coto.VentasAutomoviles.Application.Interfaces;

public interface IVentaRepository
{
    Task<int> CrearVentaAsync(Venta venta);
    Task<List<Venta>> ObtenerVentasAsync();
}
