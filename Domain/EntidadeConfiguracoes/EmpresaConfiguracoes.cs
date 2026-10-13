namespace Domain.EntidadeConfiguracoes;

public static class EmpresaConfiguracoes
{
    public const int RazaoSocialMaxLength = 255;
    public const int NomeFantasiaMaxLength = 255;
    public const int CpfCnpjMaxLength = 20;
    public const string ErroRazaoSocialObrigatoria = "Razão social é obrigatória.";
    public const string ErroRazaoSocialMaxLength = "Razão social excede o tamanho máximo permitido.";
    public const string ErroNomeFantasiaObrigatorio = "Nome fantasia é obrigatório.";
    public const string ErroNomeFantasiaMaxLength = "Nome fantasia excede o tamanho máximo permitido.";
    public const string ErroCpfCnpjObrigatorio = "CPF/CNPJ é obrigatório.";
    public const string ErroCpfCnpjMaxLength = "CPF/CNPJ excede o tamanho máximo permitido.";
    public const string ErroCpfCnpjInvalido = "CPF/CNPJ inválido.";
    public const string ErroCpfCnpjDuplicado = "Já existe uma empresa cadastrada com o CPF/CNPJ informado.";
}
