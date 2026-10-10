using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class BloqueioConfiguracao : EntidadeBaseConfiguracao<Bloqueio>
{
    public override void Configure(EntityTypeBuilder<Bloqueio> builder)
    {
        base.Configure(builder);

        builder.Property(bloqueio => bloqueio.Motivo)
            .HasMaxLength(BloqueioConfiguracoes.MotivoMaxLength)
            .IsRequired();

        builder.Property(bloqueio => bloqueio.Ativo)
            .IsRequired();
    }
}
