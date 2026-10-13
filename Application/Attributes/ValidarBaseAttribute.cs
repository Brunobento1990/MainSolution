namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public abstract class ValidarBaseAttribute : Attribute
{
    public abstract string? Validar(object? valor);
}
