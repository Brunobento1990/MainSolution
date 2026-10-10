using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioRecuperacaoDeSenhaConfiguracao
    : EntidadeBaseConfiguracao<UsuarioRecuperacaoDeSenha>
{
    public override void Configure(EntityTypeBuilder<UsuarioRecuperacaoDeSenha> builder)
    {
        base.Configure(builder);

        builder.Property(recuperacao => recuperacao.Tipo)
            .IsRequired();

        builder.Property(recuperacao => recuperacao.Token)
            .HasMaxLength(UsuarioRecuperacaoDeSenhaConfiguracoes.TokenMaxLength)
            .IsRequired();

        builder.Property(recuperacao => recuperacao.Destino)
            .HasMaxLength(UsuarioRecuperacaoDeSenhaConfiguracoes.DestinoMaxLength)
            .IsRequired();

        builder.Property(recuperacao => recuperacao.ExpiraEm)
            .IsRequired();

        builder.Property(recuperacao => recuperacao.Tentativas)
            .IsRequired();

        builder.HasIndex(recuperacao => recuperacao.Token)
            .IsUnique();

        builder.HasOne(recuperacao => recuperacao.Usuario)
            .WithMany()
            .HasForeignKey(recuperacao => recuperacao.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
