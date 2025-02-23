using Coto.VentasAutomoviles.Domain.Enums;
using Coto.VentasAutomoviles.Domain.Interfaces;
using Coto.VentasAutomoviles.Domain.Strategies;
using Coto.VentasAutomoviles.Domain.ValueObjects;
using Coto.VentasAutomoviles.Infrastructure.Data;
using Coto.VentasAutomoviles.Infrastructure.Data.Repositories;
using Coto.VentasAutomoviles.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coto.VentasAutomoviles.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VentasAutomovilesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DesaConnection")));
        

        services.AddScoped<IVentaRepository, VentaRepository>();
        //Services
        services.AddTransient<IVentaService, VentaService>();
        // Registrar strategy
        services.AddTransient<Func<TipoAutomovilEnum, IPreciosStrategy>>(serviceProvider => tipoAutomovil =>
        {
            return new PreciosAutosStrategy(tipoAutomovil);
        });
        return services;
    }
}
