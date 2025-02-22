using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coto.VentasAutomoviles.Infrastructure.Data.Repositories;

public class VentaRepository : GenericRepository<Venta>, IVentaRepository
{
    private readonly VentasAutomovilesDbContext _context;

    public VentaRepository(VentasAutomovilesDbContext context) : base(context)
    {
        _context = context;
    }

    //public async Task<List<Venta>> ObtenerVentasPorCentroDistribucion(int centroDistribucionId)
    //{
    //    return await _context.Ventas
    //        .Where(v => v.CentroDistribucionId == centroDistribucionId)
    //        .ToListAsync();
    //}

    public async Task<List<Venta>> ObtenerVentasPorCentroDistribucion(int centroDistribucionId)
    {
        return await _context.Ventas
            .Where(v => v.CentroDistribucionId == centroDistribucionId)
            .Include(v => v.Vehiculo)  // Si tienes propiedades de navegación
            .ToListAsync();
    }

    public async Task<Dictionary<string, object>> ObtenerVolumenTotalVentas()
    {
        var cantidadTotalVendida = await _context.Ventas.SumAsync(c => c.Cantidad);
        var montoTotalVendido = await _context.Ventas.SumAsync(v => v.PrecioDeVenta);

        var result = new Dictionary<string, object>
        {
            { "CantidadTotalVendida", cantidadTotalVendida },
            { "MontoTotalVendido", montoTotalVendido }
        };

        return result;
    }

    public async Task<Dictionary<string, object>> ObtenerVolumenVentasPorCentro(int centroDistribucionId)
    {

        var cantidadTotalVendida = await _context.Ventas
                                   .Where(v => v.CentroDistribucionId == centroDistribucionId)
                                   .SumAsync(c => c.Cantidad);
        var montoTotalVendido = await _context.Ventas
                                   .Where(v => v.CentroDistribucionId == centroDistribucionId)
                                   .SumAsync(v => v.PrecioDeVenta);

        var result = new Dictionary<string, object>
        {
            { "CantidadTotalVendida", cantidadTotalVendida },
            { "MontoTotalVendido", montoTotalVendido }
        };

        return result;
    }

}
