using System.Text;

namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarSenhaAttribute : ValidarBaseAttribute
{
    private readonly string _erroObrigatorio;
    private readonly string _erroMaxLength;
    private readonly int _maxBytes;

    public ValidarSenhaAttribute(string erroObrigatorio, string erroMaxLength, int maxBytes)
    {
        _erroObrigatorio = erroObrigatorio;
        _erroMaxLength = erroMaxLength;
        _maxBytes = maxBytes;
    }

    public override string? Validar(object? valor)
    {
        var senha = valor?.ToString();

        if (string.IsNullOrWhiteSpace(senha))
        {
            return _erroObrigatorio;
        }

        return Encoding.UTF8.GetByteCount(senha) > _maxBytes
            ? _erroMaxLength
            : null;
    }
}
