using Coto.VentasAutomoviles.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Coto.VentasAutomoviles.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            RegisterValidatorsFromAssembly(services, Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        private static void RegisterValidatorsFromAssembly(IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);
            var validators = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validators)
            {
                var interfaceType = validator.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorType);
                services.AddTransient(interfaceType, validator);
            }
        }
    }
}
