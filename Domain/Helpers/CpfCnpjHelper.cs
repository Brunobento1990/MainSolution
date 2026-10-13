namespace Domain.Helpers;

public static class CpfCnpjHelper
{
    public static bool EhCpfValido(string valor)
    {
        if (valor.Length != 11 || !valor.All(EhDigitoAscii) || TodosCaracteresIguais(valor))
        {
            return false;
        }

        return CalcularDigitoCpf(valor[..9], [10, 9, 8, 7, 6, 5, 4, 3, 2]) == valor[9] - '0'
               && CalcularDigitoCpf(valor[..10], [11, 10, 9, 8, 7, 6, 5, 4, 3, 2]) == valor[10] - '0';
    }

    public static bool EhCnpjValido(string valor)
    {
        if (valor.Length != 14
            || !valor[..12].All(EhCaractereCnpjValido)
            || !valor[12..].All(EhDigitoAscii)
            || TodosCaracteresIguais(valor))
        {
            return false;
        }

        return CalcularDigitoCnpj(valor[..12], [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]) == valor[12] - '0'
               && CalcularDigitoCnpj(valor[..13], [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]) == valor[13] - '0';
    }

    private static bool EhCaractereCnpjValido(char caractere)
    {
        return caractere is >= '0' and <= '9' or >= 'A' and <= 'Z';
    }

    private static bool EhDigitoAscii(char caractere)
    {
        return caractere is >= '0' and <= '9';
    }

    private static int CalcularDigitoCpf(string caracteres, int[] pesos)
    {
        var soma = caracteres
            .Select((numero, indice) => (numero - '0') * pesos[indice])
            .Sum();
        var resto = soma % 11;

        return resto < 2 ? 0 : 11 - resto;
    }

    private static int CalcularDigitoCnpj(string caracteres, int[] pesos)
    {
        var soma = caracteres
            .Select((caractere, indice) => (caractere - 48) * pesos[indice])
            .Sum();
        var resto = soma % 11;

        return resto < 2 ? 0 : 11 - resto;
    }

    private static bool TodosCaracteresIguais(string valor)
    {
        return valor.All(digito => digito == valor[0]);
    }
}
