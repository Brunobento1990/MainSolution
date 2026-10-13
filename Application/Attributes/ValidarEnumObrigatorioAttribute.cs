namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarEnumObrigatorioAttribute : ValidarBaseAttribute
{
    private readonly Type _enumType;
    private readonly string _erroInvalido;

    public ValidarEnumObrigatorioAttribute(Type enumType, string erroInvalido)
    {
        _enumType = enumType;
        _erroInvalido = erroInvalido;
    }

    public override string? Validar(object? valor)
    {
        return valor != null && Enum.IsDefined(_enumType, valor)
            ? null
            : _erroInvalido;
    }
}
