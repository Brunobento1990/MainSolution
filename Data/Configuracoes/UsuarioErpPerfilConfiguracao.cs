using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioErpPerfilConfiguracao : EmpresaBaseConfiguracao<UsuarioErpPerfil>
{
    public override void Configure(EntityTypeBuilder<UsuarioErpPerfil> builder)
    {
        base.Configure(builder);

        builder.Property(perfil => perfil.Nome)
            .HasMaxLength(UsuarioErpPerfilConfiguracoes.NomeMaxLength)
            .IsRequired();

        builder.Property(perfil => perfil.Ativo)
            .IsRequired();

        builder.HasIndex(perfil => new { perfil.EmpresaId, perfil.Nome })
            .IsUnique();
    }
}
