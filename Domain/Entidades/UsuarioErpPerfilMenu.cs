namespace Domain.Entidades;

public sealed class UsuarioErpPerfilMenu
{
    public UsuarioErpPerfilMenu(long usuarioErpPerfilId, long menuId)
    {
        UsuarioErpPerfilId = usuarioErpPerfilId;
        MenuId = menuId;
    }

    public long UsuarioErpPerfilId { get; private set; }
    public UsuarioErpPerfil UsuarioErpPerfil { get; set; } = null!;
    public long MenuId { get; private set; }
    public Menu Menu { get; set; } = null!;

    public static UsuarioErpPerfilMenu Novo(long usuarioErpPerfilId, long menuId)
    {
        return new UsuarioErpPerfilMenu(usuarioErpPerfilId, menuId);
    }

    public static UsuarioErpPerfilMenu Novo(UsuarioErpPerfil usuarioErpPerfil, long menuId)
    {
        return new UsuarioErpPerfilMenu(usuarioErpPerfil.Id, menuId)
        {
            UsuarioErpPerfil = usuarioErpPerfil
        };
    }
}
