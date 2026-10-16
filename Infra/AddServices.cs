using Application.Repositories;
using Data.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class AddServices
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly("Migrations")));

        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<ISelfServiceRepository, SelfServiceRepository>();
        services.AddScoped<IUsuarioBloqueioRepository, UsuarioBloqueioRepository>();
        services.AddScoped<IUsuarioTentativaLoginRepository, UsuarioTentativaLoginRepository>();

        return services;
    }
}
