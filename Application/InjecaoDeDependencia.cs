using Application.Autenticacao;
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class InjecaoDeDependencia
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGeradorTokenAcesso, GeradorTokenAcessoJwt>();
        services.AddScoped<ISelfServiceService, SelfServiceService>();
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();

        return services;
    }
}
