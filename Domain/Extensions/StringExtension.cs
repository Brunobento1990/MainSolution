namespace Domain.Extensions;

public static class StringExtension
{
    public static string? LimitarOuNull(this string? valor, int length)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return null;
        }

        return valor.Limitar(length);
    }

    public static string Limitar(this string valor, int length)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return string.Empty;
        }

        if (valor.Length > length)
        {
            return valor[..length];
        }

        return valor;
    }
}