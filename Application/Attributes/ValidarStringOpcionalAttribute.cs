namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarStringOpcionalAttribute : ValidarBaseAttribute
{
    private readonly string _erroMaxLength;
    private readonly int _maxLength;

    public ValidarStringOpcionalAttribute(string erroMaxLength, int maxLength)
    {
        _erroMaxLength = erroMaxLength;
        _maxLength = maxLength;
    }

    public override string? Validar(object? valor)
    {
        var texto = valor?.ToString();

        return !string.IsNullOrWhiteSpace(texto) && texto.Length > _maxLength
            ? _erroMaxLength
            : null;
    }
}
