using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioBloqueioConfiguracao : EntidadeBaseConfiguracao<UsuarioBloqueio>
{
    public override void Configure(EntityTypeBuilder<UsuarioBloqueio> builder)
    {
        base.Configure(builder);

        builder.Property(usuarioBloqueio => usuarioBloqueio.TipoBloqueio)
            .IsRequired();

        builder.HasOne(usuarioBloqueio => usuarioBloqueio.Usuario)
            .WithMany()
            .HasForeignKey(usuarioBloqueio => usuarioBloqueio.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(usuarioBloqueio => usuarioBloqueio.Bloqueio)
            .WithMany()
            .HasForeignKey(usuarioBloqueio => usuarioBloqueio.BloqueioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
