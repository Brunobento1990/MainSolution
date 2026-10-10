using Domain.EntidadeConfiguracoes;
using Domain.Extensions;

namespace Domain.Entidades;

public sealed class Usuario : EntidadeBase
{
    public Usuario(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, long pessoaId, bool ativo, string? senha, string login)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        PessoaId = pessoaId;
        Ativo = ativo;
        Senha = senha;
        Login = login;
    }

    public long PessoaId { get; private set; }
    public Pessoa Pessoa { get; set; } = null!;
    public bool Ativo { get; private set; }
    public string? Senha { get; private set; }
    public string Login { get; private set; }

    public static Usuario Novo(long pessoaId, string? senha, string login)
    {
        return new Usuario(
            0,
            DateTime.UtcNow,
            null,
            pessoaId,
            true,
            senha.LimitarOuNull(UsuarioConfiguracoes.SenhaMaxLength),
            login.Limitar(UsuarioConfiguracoes.LoginMaxLength));
    }
}
