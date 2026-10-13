namespace Domain.EntidadeConfiguracoes;

public static class PessoaConfiguracoes
{
    public const int NomeMaxLength = 255;
    public const int DocumentoMaxLength = 20;
    public const int TelefoneMaxLength = 20;
    public const int EmailMaxLength = 255;
    public const string ErroNomeObrigatorio = "Nome é obrigatório.";
    public const string ErroNomeMaxLength = "Nome excede o tamanho máximo permitido.";
    public const string ErroDocumentoObrigatorio = "Documento é obrigatório.";
    public const string ErroDocumentoMaxLength = "Documento excede o tamanho máximo permitido.";
    public const string ErroCpfInvalido = "CPF inválido.";
    public const string ErroCnpjInvalido = "CNPJ inválido.";
    public const string ErroTipoDocumentoInvalido = "Tipo de documento é inválido.";
    public const string ErroTelefoneMaxLength = "Telefone excede o tamanho máximo permitido.";
    public const string ErroEmailObrigatorio = "E-mail é obrigatório.";
    public const string ErroEmailMaxLength = "E-mail excede o tamanho máximo permitido.";
    public const string ErroEmailInvalido = "E-mail inválido.";
}
