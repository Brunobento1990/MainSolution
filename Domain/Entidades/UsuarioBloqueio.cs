using Domain.Enuns;

namespace Domain.Entidades;

public sealed class UsuarioBloqueio : EntidadeBase
{
    public UsuarioBloqueio(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, long usuarioId, TipoBloqueioUsuarioEnum tipoBloqueio, long bloqueioId)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        UsuarioId = usuarioId;
        TipoBloqueio = tipoBloqueio;
        BloqueioId = bloqueioId;
    }

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public TipoBloqueioUsuarioEnum TipoBloqueio { get; private set; }
    public long BloqueioId { get; private set; }
    public Bloqueio Bloqueio { get; set; } = null!;
}