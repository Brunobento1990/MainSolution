using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class InjecaoDeDependencia
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISelfServiceService, SelfServiceService>();

        return services;
    }
}
