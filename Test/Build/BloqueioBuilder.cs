using Domain.Entidades;

namespace Test.Build;

public sealed class BloqueioBuilder
{
    private long _id = 1;
    private string _motivo = "Usuário bloqueado.";
    private bool _ativo = true;

    public static BloqueioBuilder Init() => new();

    public BloqueioBuilder ComMotivo(string motivo)
    {
        _motivo = motivo;
        return this;
    }

    public Bloqueio Build()
    {
        return new Bloqueio(
            _id,
            DateTime.UtcNow,
            null,
            _motivo,
            _ativo,
            null);
    }
}
