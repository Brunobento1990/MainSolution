namespace Domain.EntidadeConfiguracoes;

public static class UsuarioConfiguracoes
{
    public const int SenhaMaxLength = 500;
    public const int SenhaEntradaMaxBytes = 72;
    public const int LoginMaxLength = 255;
    public const string ErroSenhaObrigatoria = "Senha é obrigatória.";
    public const string ErroSenhaEntradaMaxLength = "Senha excede o tamanho máximo permitido.";
}
