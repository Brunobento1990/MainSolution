using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using Application.Models;
using Domain.Entidades;
using Domain.Extensions;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Autenticacao;

internal sealed class GeradorTokenAcessoJwt : IGeradorTokenAcesso
{
    private const string MensagemTokenInvalido = "Token inválido, efetue o login";
    private readonly IConfiguration _configuration;

    public GeradorTokenAcessoJwt(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Gerar(UsuarioSessao usuarioSessao)
    {
        var configuracao = ObterConfiguracao();

        Claim[] claims =
        [
            new(ClaimsAutenticacao.UsuarioSessaoId, usuarioSessao.Id.ToString(CultureInfo.InvariantCulture))
        ];

        var credenciais = new SigningCredentials(
            CriarChave(configuracao.Chave),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuracao.Emissor,
            audience: configuracao.Audiencia,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(configuracao.ExpiracaoEmMinutos),
            signingCredentials: credenciais);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Resultado<ValidacaoJwtModel> Validar(string token, bool validarTempoDeVida = true)
    {
        if (string.IsNullOrWhiteSpace(token))
            return Resultado<ValidacaoJwtModel>.ComErro(MensagemTokenInvalido);

        try
        {
            var configuracao = ObterConfiguracao();
            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(
                token,
                CriarParametrosValidacao(configuracao, validarTempoDeVida),
                out var tokenValidado);

            if (tokenValidado is not JwtSecurityToken jwtToken)
                return Resultado<ValidacaoJwtModel>.ComErro(MensagemTokenInvalido);

            var sessaoIdClaim = jwtToken.Claims
                .FirstOrDefault(claim => claim.Type == ClaimsAutenticacao.UsuarioSessaoId)
                ?.Value;

            if (!long.TryParse(
                    sessaoIdClaim,
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out var sessaoId))
                return Resultado<ValidacaoJwtModel>.ComErro(MensagemTokenInvalido);

            return Resultado<ValidacaoJwtModel>.RetornaSucesso(new ValidacaoJwtModel
            {
                Expirado = jwtToken.ValidTo <= DateTime.UtcNow,
                UsuarioLogado = new UsuarioLogado { SessaoId = sessaoId }
            });
        }
        catch (SecurityTokenExpiredException)
        {
            return Resultado<ValidacaoJwtModel>.RetornaSucesso(new ValidacaoJwtModel
            {
                Expirado = true
            });
        }
        catch (SecurityTokenException)
        {
            return Resultado<ValidacaoJwtModel>.ComErro(MensagemTokenInvalido);
        }
        catch (ArgumentException)
        {
            return Resultado<ValidacaoJwtModel>.ComErro(MensagemTokenInvalido);
        }
    }

    private ConfiguracaoJwt ObterConfiguracao()
    {
        var expiracaoEmMinutos = _configuration.ObterOuPadrao(
            JwtConfiguracoes.ExpiracaoEmMinutos,
            JwtConfiguracoes.ExpiracaoPadraoEmMinutos);

        if (expiracaoEmMinutos <= 0)
            expiracaoEmMinutos = JwtConfiguracoes.ExpiracaoPadraoEmMinutos;

        return new ConfiguracaoJwt(
            _configuration.ObterOuPadrao(JwtConfiguracoes.Emissor, JwtConfiguracoes.EmissorPadrao),
            _configuration.ObterOuPadrao(JwtConfiguracoes.Audiencia, JwtConfiguracoes.AudienciaPadrao),
            _configuration.ObterOuPadrao(JwtConfiguracoes.Chave, JwtConfiguracoes.ChavePadrao),
            expiracaoEmMinutos,
            _configuration.ObterOuPadrao(
                JwtConfiguracoes.ValidateIssuer,
                JwtConfiguracoes.ValidateIssuerPadrao),
            _configuration.ObterOuPadrao(
                JwtConfiguracoes.ValidateAudience,
                JwtConfiguracoes.ValidateAudiencePadrao));
    }

    private static SymmetricSecurityKey CriarChave(string chave)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
    }

    private static TokenValidationParameters CriarParametrosValidacao(
        ConfiguracaoJwt configuracao,
        bool validarTempoDeVida)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = configuracao.ValidateIssuer,
            ValidateAudience = configuracao.ValidateAudience,
            ValidateLifetime = validarTempoDeVida,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuracao.Emissor,
            ValidAudience = configuracao.Audiencia,
            IssuerSigningKey = CriarChave(configuracao.Chave),
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
        };
    }

    private sealed record ConfiguracaoJwt(
        string Emissor,
        string Audiencia,
        string Chave,
        int ExpiracaoEmMinutos,
        bool ValidateIssuer,
        bool ValidateAudience);
}
