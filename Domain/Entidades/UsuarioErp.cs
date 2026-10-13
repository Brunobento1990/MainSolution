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

    public static UsuarioErp Novo(long empresaId, long usuarioErpPerfilId, long usuarioId)
    {
        return new UsuarioErp(
            0,
            DateTime.UtcNow,
            null,
            empresaId,
            usuarioErpPerfilId,
            usuarioId);
    }

    public static UsuarioErp Novo(
        long empresaId,
        UsuarioErpPerfil usuarioErpPerfil,
        Usuario usuario)
    {
        return new UsuarioErp(
            0,
            DateTime.UtcNow,
            null,
            empresaId,
            usuarioErpPerfil.Id,
            usuario.Id)
        {
            UsuarioErpPerfil = usuarioErpPerfil,
            Usuario = usuario
        };
    }
}
