using Application.Attributes;
using Domain.EntidadeConfiguracoes;

namespace Application.DTOs;

public sealed class LoginErpDTO : ValidarBaseDTO
{
    [ValidarEmail(
        UsuarioErpConfiguracoes.ErroEmailObrigatorio,
        UsuarioErpConfiguracoes.ErroEmailInvalido,
        UsuarioErpConfiguracoes.ErroEmailMaxLength,
        UsuarioConfiguracoes.LoginMaxLength)]
    public string Email { get; set; } = string.Empty;

    [ValidarSenha(
        UsuarioConfiguracoes.ErroSenhaObrigatoria,
        UsuarioConfiguracoes.ErroSenhaEntradaMaxLength,
        UsuarioConfiguracoes.SenhaEntradaMaxBytes)]
    public string Senha { get; set; } = string.Empty;
}
