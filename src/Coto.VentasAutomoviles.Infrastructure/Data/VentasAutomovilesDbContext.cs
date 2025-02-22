using Coto.VentasAutomoviles.Domain.Entities;
using Coto.VentasAutomoviles.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Coto.VentasAutomoviles.Infrastructure.Data;

public class VentasAutomovilesDbContext : DbContext
{
    public VentasAutomovilesDbContext(DbContextOptions<VentasAutomovilesDbContext> options) : base(options)
    {
    }
    public DbSet<Venta> Ventas { get; set; }    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VentaConfiguration());
    }
}
