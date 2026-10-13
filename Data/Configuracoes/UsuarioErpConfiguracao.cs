using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioErpConfiguracao : EmpresaBaseConfiguracao<UsuarioErp>
{
    public override void Configure(EntityTypeBuilder<UsuarioErp> builder)
    {
        base.Configure(builder);

        builder.HasIndex(usuarioErp => new { usuarioErp.EmpresaId, usuarioErp.UsuarioId })
            .IsUnique();

        builder.HasOne(usuarioErp => usuarioErp.Usuario)
            .WithMany()
            .HasForeignKey(usuarioErp => new { usuarioErp.UsuarioId, usuarioErp.EmpresaId })
            .HasPrincipalKey(usuario => new { usuario.Id, usuario.EmpresaId })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(usuarioErp => usuarioErp.UsuarioErpPerfil)
            .WithMany()
            .HasForeignKey(usuarioErp => new { usuarioErp.UsuarioErpPerfilId, usuarioErp.EmpresaId })
            .HasPrincipalKey(perfil => new { perfil.Id, perfil.EmpresaId })
            .OnDelete(DeleteBehavior.Restrict);
    }
}
