using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioTentativaLoginConfiguracao
    : EntidadeBaseConfiguracao<UsuarioTentativaLogin>
{
    public override void Configure(EntityTypeBuilder<UsuarioTentativaLogin> builder)
    {
        base.Configure(builder);

        builder.Property(tentativa => tentativa.QuantidadeErrosSenha)
            .IsRequired();

        builder.HasIndex(tentativa => tentativa.UsuarioId)
            .IsUnique();

        builder.HasOne(tentativa => tentativa.Usuario)
            .WithMany()
            .HasForeignKey(tentativa => tentativa.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
