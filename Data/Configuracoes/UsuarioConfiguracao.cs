using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioConfiguracao : EntidadeBaseConfiguracao<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.Property(usuario => usuario.Ativo)
            .IsRequired();

        builder.Property(usuario => usuario.Senha)
            .HasMaxLength(UsuarioConfiguracoes.SenhaMaxLength)
            .IsRequired(false);

        builder.Property(usuario => usuario.Login)
            .HasMaxLength(UsuarioConfiguracoes.LoginMaxLength)
            .IsRequired();

        builder.HasIndex(usuario => usuario.Login);
        builder.HasIndex(usuario => new { usuario.Login, usuario.Ativo });

        builder.HasOne(usuario => usuario.Pessoa)
            .WithMany()
            .HasForeignKey(usuario => usuario.PessoaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}