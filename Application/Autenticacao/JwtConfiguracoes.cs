namespace Application.Autenticacao;

internal static class JwtConfiguracoes
{
    public const string EmissorPadrao = "MainSolution.Api";
    public const string AudienciaPadrao = "MainSolution.Clientes";
    public const string ChavePadrao = "chave-local-de-desenvolvimento-com-32-caracteres";
    public const int ExpiracaoPadraoEmMinutos = 60;
    public const bool ValidateIssuerPadrao = true;
    public const bool ValidateAudiencePadrao = true;

    public const string Secao = "Jwt";
    public const string Emissor = Secao + ":Emissor";
    public const string Audiencia = Secao + ":Audiencia";
    public const string Chave = Secao + ":Chave";
    public const string ExpiracaoEmMinutos = Secao + ":ExpiracaoEmMinutos";
    public const string ValidateIssuer = Secao + ":ValidateIssuer";
    public const string ValidateAudience = Secao + ":ValidateAudience";
}
