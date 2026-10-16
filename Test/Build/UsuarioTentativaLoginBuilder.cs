using Domain.Entidades;

namespace Test.Build;

public sealed class UsuarioTentativaLoginBuilder
{
    private long _id = 1;
    private long _usuarioId = 1;
    private int _quantidadeErrosSenha;

    public static UsuarioTentativaLoginBuilder Init() => new();

    public UsuarioTentativaLoginBuilder ComUsuarioId(long usuarioId)
    {
        _usuarioId = usuarioId;
        return this;
    }

    public UsuarioTentativaLoginBuilder ComQuantidadeErrosSenha(int quantidadeErrosSenha)
    {
        _quantidadeErrosSenha = quantidadeErrosSenha;
        return this;
    }

    public UsuarioTentativaLogin Build()
    {
        return new UsuarioTentativaLogin(
            _id,
            DateTime.UtcNow,
            null,
            _usuarioId,
            _quantidadeErrosSenha);
    }
}
