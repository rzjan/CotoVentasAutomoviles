using Coto.VentasAutomoviles.Api.Configurations;
using Coto.VentasAutomoviles.Api.Middlewares;
using Microsoft.OpenApi.Models;

namespace Coto.VentasAutomoviles.Api.Extensions;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static void UseApiMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();        
    }
}
