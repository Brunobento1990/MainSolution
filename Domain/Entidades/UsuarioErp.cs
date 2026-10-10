namespace Domain.Entidades;

public sealed class UsuarioErp : EmpresaBase
{
    public UsuarioErp(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao,
        long empresaId, long usuarioErpPerfilId, long usuarioId)
        : base(id, dataHoraCadastro, dataHoraAtualizacao, empresaId)
    {
        UsuarioErpPerfilId = usuarioErpPerfilId;
        UsuarioId = usuarioId;
    }

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public long UsuarioErpPerfilId { get; private set; }
    public UsuarioErpPerfil UsuarioErpPerfil { get; set; } = null!;
}