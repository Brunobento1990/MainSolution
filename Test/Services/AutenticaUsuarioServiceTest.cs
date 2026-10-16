using Application.Adapters;
using Application.Autenticacao;
using Application.Repositories;
using Domain.Entidades;
using Microsoft.Extensions.Configuration;
using Moq;
using Test.Build;

namespace Test.Services;

public sealed class AutenticaUsuarioServiceTest
{
    private const string ErroCredenciaisInvalidas = "Credenciais inválidas.";
    private readonly Mock<IConfiguration> _configuration = new();
    private readonly Mock<IUsuarioBloqueioRepository> _usuarioBloqueioRepository = new();
    private readonly Mock<IUsuarioTentativaLoginRepository> _usuarioTentativaLoginRepository = new();
    private readonly AutenticaUsuarioService _autenticaUsuarioService;

    public AutenticaUsuarioServiceTest()
    {
        _autenticaUsuarioService = new(
            configuration: _configuration.Object,
            usuarioBloqueioRepository: _usuarioBloqueioRepository.Object,
            usuarioTentativaLoginRepository: _usuarioTentativaLoginRepository.Object);
    }

    [Fact]
    public async Task DeveRetornarMotivoQuandoUsuarioPossuirBloqueioAtivo()
    {
        const string motivoBloqueio = "Usuário bloqueado manualmente.";
        var usuario = UsuarioBuilder.Init().Build();
        var bloqueio = BloqueioBuilder.Init()
            .ComMotivo(motivoBloqueio)
            .Build();
        var usuarioBloqueio = UsuarioBloqueioBuilder.Init()
            .ComUsuarioId(usuario.Id)
            .ComBloqueio(bloqueio)
            .Build();
        ConfigurarTentativasSenha(true);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuarioBloqueio);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(usuario, "senha");

        Assert.False(resultado.Sucesso);
        Assert.Equal(motivoBloqueio, Assert.Single(resultado.Erros!));
        _usuarioTentativaLoginRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task DeveRetornarSucessoComSenhaValidaQuandoValidacaoTentativasSenhaEstiverDesabilitada()
    {
        const string senha = "senha-valida";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        ConfigurarTentativasSenha(false);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                false,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(usuario, senha);

        Assert.True(resultado.Sucesso);
        Assert.True(resultado.Valor);
        _usuarioTentativaLoginRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task DeveRetornarErroComSenhaInvalidaQuandoValidacaoTentativasSenhaEstiverDesabilitada()
    {
        const string senha = "senha-valida";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        ConfigurarTentativasSenha(false);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                false,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(
            usuario,
            "senha-invalida");

        AssertErro(resultado, ErroCredenciaisInvalidas);
        _usuarioTentativaLoginRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task DeveRegistrarPrimeiroErroDeSenha()
    {
        const string senha = "senha-valida";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        ConfigurarTentativasSenha(true);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);
        _usuarioTentativaLoginRepository
            .Setup(x => x.ObterPorUsuarioIdAsync(
                usuario.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioTentativaLogin?)null);
        _usuarioTentativaLoginRepository
            .Setup(x => x.AddAsync(
                It.IsAny<UsuarioTentativaLogin>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _usuarioTentativaLoginRepository
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(
            usuario,
            "senha-invalida");

        AssertErro(resultado, ErroCredenciaisInvalidas);
        _usuarioTentativaLoginRepository.Verify(
            x => x.AddAsync(
                It.Is<UsuarioTentativaLogin>(tentativa =>
                    tentativa.UsuarioId == usuario.Id &&
                    tentativa.QuantidadeErrosSenha == 1),
                It.IsAny<CancellationToken>()),
            Times.Once);
        _usuarioTentativaLoginRepository.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        _usuarioBloqueioRepository.Verify(
            x => x.AddAsync(
                It.IsAny<UsuarioBloqueio>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DeveIncrementarErroDeSenhaExistenteSemBloquearAntesDoLimite()
    {
        const string senha = "senha-valida";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        var tentativaLogin = UsuarioTentativaLoginBuilder.Init()
            .ComUsuarioId(usuario.Id)
            .ComQuantidadeErrosSenha(1)
            .Build();
        ConfigurarTentativasSenha(true);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);
        _usuarioTentativaLoginRepository
            .Setup(x => x.ObterPorUsuarioIdAsync(
                usuario.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(tentativaLogin);
        _usuarioTentativaLoginRepository
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(
            usuario,
            "senha-invalida");

        AssertErro(resultado, ErroCredenciaisInvalidas);
        Assert.Equal(2, tentativaLogin.QuantidadeErrosSenha);
        _usuarioTentativaLoginRepository.Verify(
            x => x.Update(tentativaLogin),
            Times.Once);
        _usuarioTentativaLoginRepository.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        _usuarioBloqueioRepository.Verify(
            x => x.AddAsync(
                It.IsAny<UsuarioBloqueio>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task DeveBloquearUsuarioQuandoAtingirLimiteDeErrosDeSenha(
        int quantidadeMaximaErrosSenha)
    {
        const string senha = "senha-valida";
        var motivoBloqueio =
            $"Quantidade máxima configurada de erros de senha atingida: {quantidadeMaximaErrosSenha}.";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        var tentativaLogin = UsuarioTentativaLoginBuilder.Init()
            .ComUsuarioId(usuario.Id)
            .ComQuantidadeErrosSenha(quantidadeMaximaErrosSenha - 1)
            .Build();
        ConfigurarTentativasSenha(true, quantidadeMaximaErrosSenha);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);
        _usuarioTentativaLoginRepository
            .Setup(x => x.ObterPorUsuarioIdAsync(
                usuario.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(tentativaLogin);
        _usuarioBloqueioRepository
            .Setup(x => x.AddAsync(
                It.IsAny<UsuarioBloqueio>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _usuarioBloqueioRepository
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(
            usuario,
            "senha-invalida");

        AssertErro(resultado, motivoBloqueio);
        Assert.Equal(quantidadeMaximaErrosSenha, tentativaLogin.QuantidadeErrosSenha);
        _usuarioBloqueioRepository.Verify(
            x => x.AddAsync(
                It.Is<UsuarioBloqueio>(usuarioBloqueio =>
                    usuarioBloqueio.UsuarioId == usuario.Id &&
                    usuarioBloqueio.Bloqueio.Motivo == motivoBloqueio),
                It.IsAny<CancellationToken>()),
            Times.Once);
        _usuarioBloqueioRepository.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        _usuarioTentativaLoginRepository.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DeveUsarLimitePadraoQuandoQuantidadeMaximaDeErrosForInvalida()
    {
        const int quantidadeMaximaErrosSenhaPadrao = 5;
        const string senha = "senha-valida";
        var motivoBloqueio =
            $"Quantidade máxima configurada de erros de senha atingida: {quantidadeMaximaErrosSenhaPadrao}.";
        var usuario = UsuarioBuilder.Init()
            .ComSenha(PasswordAdapter.GenerateHash(senha))
            .Build();
        var tentativaLogin = UsuarioTentativaLoginBuilder.Init()
            .ComUsuarioId(usuario.Id)
            .ComQuantidadeErrosSenha(quantidadeMaximaErrosSenhaPadrao - 1)
            .Build();
        ConfigurarTentativasSenha(true, 0);
        _usuarioBloqueioRepository
            .Setup(x => x.ObterBloqueioAtivoAsync(
                usuario.Id,
                true,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((UsuarioBloqueio?)null);
        _usuarioTentativaLoginRepository
            .Setup(x => x.ObterPorUsuarioIdAsync(
                usuario.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(tentativaLogin);
        _usuarioBloqueioRepository
            .Setup(x => x.AddAsync(
                It.IsAny<UsuarioBloqueio>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _usuarioBloqueioRepository
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var resultado = await _autenticaUsuarioService.ValidarAcessoAsync(
            usuario,
            "senha-invalida");

        AssertErro(resultado, motivoBloqueio);
        _usuarioBloqueioRepository.Verify(
            x => x.AddAsync(
                It.Is<UsuarioBloqueio>(usuarioBloqueio =>
                    usuarioBloqueio.Bloqueio.Motivo == motivoBloqueio),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private void ConfigurarTentativasSenha(
        bool habilitada,
        int quantidadeMaximaErrosSenha = 5)
    {
        _configuration
            .Setup(x => x.GetSection(AutenticacaoConfiguracoes.TentativasSenhaHabilitada))
            .Returns(CriarSecaoConfiguracao(habilitada ? "true" : "false"));
        _configuration
            .Setup(x => x.GetSection(AutenticacaoConfiguracoes.QuantidadeMaximaErrosSenha))
            .Returns(CriarSecaoConfiguracao(quantidadeMaximaErrosSenha.ToString()));
    }

    private static IConfigurationSection CriarSecaoConfiguracao(string valor)
    {
        var secao = new Mock<IConfigurationSection>();
        secao.SetupGet(x => x.Value).Returns(valor);
        return secao.Object;
    }

    private static void AssertErro(Domain.Model.Resultado<bool> resultado, string erro)
    {
        Assert.False(resultado.Sucesso);
        Assert.Equal(erro, Assert.Single(resultado.Erros!));
    }
}
