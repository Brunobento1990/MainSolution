using Domain.Entidades;
using Domain.Enuns;

namespace Test.Build;

public sealed class UsuarioBloqueioBuilder
{
    private long _id = 1;
    private long _usuarioId = 1;
    private TipoBloqueioUsuarioEnum _tipoBloqueio =
        TipoBloqueioUsuarioEnum.TentativasLoginErroSenha;
    private Bloqueio _bloqueio = BloqueioBuilder.Init().Build();

    public static UsuarioBloqueioBuilder Init() => new();

    public UsuarioBloqueioBuilder ComUsuarioId(long usuarioId)
    {
        _usuarioId = usuarioId;
        return this;
    }

    public UsuarioBloqueioBuilder ComBloqueio(Bloqueio bloqueio)
    {
        _bloqueio = bloqueio;
        return this;
    }

    public UsuarioBloqueio Build()
    {
        return new UsuarioBloqueio(
            _id,
            DateTime.UtcNow,
            null,
            _usuarioId,
            _tipoBloqueio,
            _bloqueio.Id)
        {
            Bloqueio = _bloqueio
        };
    }
}
