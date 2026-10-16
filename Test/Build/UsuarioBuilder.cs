using Domain.Entidades;

namespace Test.Build;

public sealed class UsuarioBuilder
{
    private long _id = 1;
    private long _empresaId = 1;
    private long _pessoaId = 1;
    private bool _ativo = true;
    private string? _senha = "senha";
    private string _login = "usuario@empresa.com";

    public static UsuarioBuilder Init() => new();

    public UsuarioBuilder ComId(long id)
    {
        _id = id;
        return this;
    }

    public UsuarioBuilder ComSenha(string senha)
    {
        _senha = senha;
        return this;
    }

    public Usuario Build()
    {
        return new Usuario(
            _id,
            DateTime.UtcNow,
            null,
            _empresaId,
            _pessoaId,
            _ativo,
            _senha,
            _login);
    }
}
