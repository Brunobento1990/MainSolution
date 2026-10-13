using System.Net.Mail;

namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarEmailAttribute : ValidarBaseAttribute
{
    private readonly int _maxLength;
    private readonly string _erroObrigatorio;
    private readonly string _erroInvalido;
    private readonly string _erroMaxLength;

    public ValidarEmailAttribute(
        string erroObrigatorio,
        string erroInvalido,
        string erroMaxLength,
        int maxLength)
    {
        _erroObrigatorio = erroObrigatorio;
        _erroInvalido = erroInvalido;
        _erroMaxLength = erroMaxLength;
        _maxLength = maxLength;
    }

    public override string? Validar(object? valor)
    {
        var email = valor?.ToString();

        if (string.IsNullOrWhiteSpace(email))
        {
            return _erroObrigatorio;
        }

        if (email.Length > _maxLength)
        {
            return _erroMaxLength;
        }

        return MailAddress.TryCreate(email, out var endereco) && endereco.Address == email
            ? null
            : _erroInvalido;
    }
}
