using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioErpPerfilMenuConfiguracao : IEntityTypeConfiguration<UsuarioErpPerfilMenu>
{
    public void Configure(EntityTypeBuilder<UsuarioErpPerfilMenu> builder)
    {
        builder.HasKey(perfilMenu => new
        {
            perfilMenu.UsuarioErpPerfilId,
            perfilMenu.MenuId
        });

        builder.HasOne(perfilMenu => perfilMenu.UsuarioErpPerfil)
            .WithMany(perfil => perfil.Menus)
            .HasForeignKey(perfilMenu => perfilMenu.UsuarioErpPerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(perfilMenu => perfilMenu.Menu)
            .WithMany()
            .HasForeignKey(perfilMenu => perfilMenu.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
