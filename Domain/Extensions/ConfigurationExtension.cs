using Microsoft.Extensions.Configuration;

namespace Domain.Extensions;

public static class ConfigurationExtension
{
    public static T ObterObrigatoria<T>(this IConfiguration configuration, string caminho)
    {
        var secao = configuration.GetSection(caminho);
        var valor = secao.Get<T>();

        if (!secao.Exists() || valor is null || valor is string texto && string.IsNullOrWhiteSpace(texto))
            throw new InvalidOperationException($"A configuração {caminho} deve ser informada.");

        return valor;
    }

    public static T ObterOuPadrao<T>(
        this IConfiguration configuration,
        string caminho,
        T valorPadrao)
    {
        var secao = configuration.GetSection(caminho);
        var valor = secao.Get<T>();

        return !secao.Exists() || valor is null || valor is string texto && string.IsNullOrWhiteSpace(texto)
            ? valorPadrao
            : valor;
    }
}
