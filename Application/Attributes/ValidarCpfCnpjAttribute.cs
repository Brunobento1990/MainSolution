using Domain.EntidadeConfiguracoes;
using Domain.Helpers;

namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidarCpfCnpjAttribute : ValidarBaseAttribute
{
    private readonly string _erroObrigatorio;
    private readonly string _erroMaxLength;
    private readonly string _erroInvalido;
    private readonly int _maxLength;

    public ValidarCpfCnpjAttribute(
        string erroObrigatorio,
        string erroMaxLength,
        string erroInvalido,
        int maxLength)
    {
        _erroObrigatorio = erroObrigatorio;
        _erroMaxLength = erroMaxLength;
        _erroInvalido = erroInvalido;
        _maxLength = maxLength;
    }

    public override string? Validar(object? valor)
    {
        var cpfCnpj = valor?.ToString();

        if (string.IsNullOrWhiteSpace(cpfCnpj))
        {
            return _erroObrigatorio;
        }

        if (cpfCnpj.Length > _maxLength)
        {
            return _erroMaxLength;
        }

        return CpfCnpjHelper.EhCpfValido(cpfCnpj)
               || CpfCnpjHelper.EhCnpjValido(cpfCnpj)
            ? null
            : _erroInvalido;
    }
}
