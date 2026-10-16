using Application.Adapters;
using Application.Repositories;
using Domain.Entidades;
using Domain.Enuns;
using Domain.Extensions;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Application.Autenticacao;

internal sealed class AutenticaUsuarioService : IAutenticaUsuarioService
{
    private const string ErroCredenciaisInvalidas = "Credenciais inválidas.";
    private const string MotivoBloqueioErrosSenha =
        "Quantidade máxima configurada de erros de senha atingida";
    private readonly IConfiguration _configuration;
    private readonly IUsuarioBloqueioRepository _usuarioBloqueioRepository;
    private readonly IUsuarioTentativaLoginRepository _usuarioTentativaLoginRepository;

    public AutenticaUsuarioService(
        IConfiguration configuration,
        IUsuarioBloqueioRepository usuarioBloqueioRepository,
        IUsuarioTentativaLoginRepository usuarioTentativaLoginRepository)
    {
        _configuration = configuration;
        _usuarioBloqueioRepository = usuarioBloqueioRepository;
        _usuarioTentativaLoginRepository = usuarioTentativaLoginRepository;
    }

    public async Task<Resultado<bool>> ValidarAcessoAsync(
        Usuario usuario,
        string senha,
        CancellationToken cancellationToken = default)
    {
        var validarTentativasSenha = _configuration.ObterOuPadrao(
            AutenticacaoConfiguracoes.TentativasSenhaHabilitada,
            AutenticacaoConfiguracoes.TentativasSenhaHabilitadaPadrao);

        var usuarioBloqueio = await _usuarioBloqueioRepository.ObterBloqueioAtivoAsync(
            usuario.Id,
            validarTentativasSenha,
            cancellationToken);

        if (usuarioBloqueio is not null)
        {
            return Resultado<bool>.ComErro(usuarioBloqueio.Bloqueio.Motivo);
        }

        var senhaValida = PasswordAdapter.VerifyPassword(senha, usuario.Senha);

        if (!validarTentativasSenha)
        {
            return senhaValida
                ? Resultado<bool>.RetornaSucesso(true)
                : Resultado<bool>.ComErro(ErroCredenciaisInvalidas);
        }

        if (senhaValida)
        {
            await _usuarioTentativaLoginRepository.ZerarErrosSenhaAsync(
                usuario.Id,
                cancellationToken);

            return Resultado<bool>.RetornaSucesso(true);
        }

        // TODO: garantir incremento atômico das tentativas em acessos concorrentes.
        var tentativaLogin = await _usuarioTentativaLoginRepository.ObterPorUsuarioIdAsync(
            usuario.Id,
            cancellationToken);

        return await RegistrarErroSenhaAsync(
            usuario.Id,
            tentativaLogin,
            cancellationToken);
    }

    private async Task<Resultado<bool>> RegistrarErroSenhaAsync(
        long usuarioId,
        UsuarioTentativaLogin? tentativaLogin,
        CancellationToken cancellationToken = default)
    {
        if (tentativaLogin is null)
        {
            tentativaLogin = UsuarioTentativaLogin.Novo(usuarioId);
            await _usuarioTentativaLoginRepository.AddAsync(tentativaLogin, cancellationToken);
        }
        else
        {
            _usuarioTentativaLoginRepository.Update(tentativaLogin);
        }

        tentativaLogin.RegistrarErroSenha();

        var quantidadeMaximaErrosSenha = _configuration.ObterOuPadrao(
            AutenticacaoConfiguracoes.QuantidadeMaximaErrosSenha,
            AutenticacaoConfiguracoes.QuantidadeMaximaErrosSenhaPadrao);

        if (quantidadeMaximaErrosSenha <= 0)
        {
            quantidadeMaximaErrosSenha = AutenticacaoConfiguracoes.QuantidadeMaximaErrosSenhaPadrao;
        }

        if (tentativaLogin.QuantidadeErrosSenha >= quantidadeMaximaErrosSenha)
        {
            var motivoBloqueio = $"{MotivoBloqueioErrosSenha}: {quantidadeMaximaErrosSenha}.";
            var bloqueio = Bloqueio.Novo(
                motivoBloqueio,
                null);
            var usuarioBloqueio = UsuarioBloqueio.Novo(
                usuarioId,
                TipoBloqueioUsuarioEnum.TentativasLoginErroSenha,
                bloqueio);

            await _usuarioBloqueioRepository.AddAsync(usuarioBloqueio, cancellationToken);
            await _usuarioBloqueioRepository.SaveChangesAsync(cancellationToken);
            return Resultado<bool>.ComErro(motivoBloqueio);
        }

        await _usuarioTentativaLoginRepository.SaveChangesAsync(cancellationToken);
        return Resultado<bool>.ComErro(ErroCredenciaisInvalidas);
    }
}
