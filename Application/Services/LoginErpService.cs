using Application.Autenticacao;
using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using Domain.Model;

namespace Application.Services;

internal sealed class LoginErpService : ILoginErpService
{
    private const string ErroCredenciaisInvalidas = "Credenciais inválidas.";
    private readonly IAutenticaUsuarioService _autenticaUsuarioService;
    private readonly ILoginRepository _loginRepository;

    public LoginErpService(
        IAutenticaUsuarioService autenticaUsuarioService,
        ILoginRepository loginRepository)
    {
        _autenticaUsuarioService = autenticaUsuarioService;
        _loginRepository = loginRepository;
    }

    public async Task<Resultado<bool>> LoginAsync(
        LoginErpDTO dto,
        CancellationToken cancellationToken = default)
    {
        var erros = dto.Validar();

        if (erros.Count > 0)
        {
            return Resultado<bool>.ComErros(erros);
        }

        var usuariosErp = await _loginRepository.ObterUsuariosErpPorEmailAsync(
            dto.Email,
            cancellationToken);

        // TODO: validar login com multi acesso.
        var usuarioErp = usuariosErp.FirstOrDefault();

        if (usuarioErp is null)
        {
            return Resultado<bool>.ComErro(ErroCredenciaisInvalidas);
        }

        return await _autenticaUsuarioService.ValidarAcessoAsync(
            usuarioErp.Usuario,
            dto.Senha,
            cancellationToken);
    }
}
