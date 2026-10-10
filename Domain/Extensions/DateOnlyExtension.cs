namespace Domain.Extensions;

public static class DateOnlyExtension
{
    public static int? Idade(this DateOnly? dataDeNascimento)
    {
        if (!dataDeNascimento.HasValue)
            return null;

        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var nascimento = dataDeNascimento.Value;

        var idade = hoje.Year - nascimento.Year;

        if (hoje < nascimento.AddYears(idade))
            idade--;

        return idade;
    }
}