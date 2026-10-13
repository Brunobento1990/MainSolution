namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarStringAttribute : ValidarBaseAttribute
{
    private readonly string _erroObrigatorio;
    private readonly string _erroMaxLength;
    private readonly int _maxLength;

    public ValidarStringAttribute(string erroObrigatorio, string erroMaxLength, int maxLength)
    {
        _erroObrigatorio = erroObrigatorio;
        _erroMaxLength = erroMaxLength;
        _maxLength = maxLength;
    }

    public override string? Validar(object? valor)
    {
        var texto = valor?.ToString();

        if (string.IsNullOrWhiteSpace(texto))
        {
            return _erroObrigatorio;
        }

        return texto.Length > _maxLength
            ? _erroMaxLength
            : null;
    }
}
