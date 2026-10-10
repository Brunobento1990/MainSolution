using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class MenuConfiguracao : EntidadeBaseConfiguracao<Menu>
{
    public override void Configure(EntityTypeBuilder<Menu> builder)
    {
        base.Configure(builder);

        builder.Property(menu => menu.Nome)
            .HasMaxLength(MenuConfiguracoes.NomeMaxLength)
            .IsRequired();

        builder.Property(menu => menu.Caminho)
            .HasMaxLength(MenuConfiguracoes.CaminhoMaxLength)
            .IsRequired(false);

        builder.Property(menu => menu.Icone)
            .HasMaxLength(MenuConfiguracoes.IconeMaxLength)
            .IsRequired(false);

        builder.Property(menu => menu.Ordem)
            .IsRequired();

        builder.Property(menu => menu.Ativo)
            .IsRequired();

        builder.Ignore(menu => menu.EhDashboard);
    }
}
