using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioConfiguracao : EmpresaBaseConfiguracao<Usuario>
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

        builder.HasAlternateKey(usuario => new { usuario.Id, usuario.EmpresaId });

        builder.HasOne(usuario => usuario.Pessoa)
            .WithMany()
            .HasForeignKey(usuario => new { usuario.PessoaId, usuario.EmpresaId })
            .HasPrincipalKey(pessoa => new { pessoa.Id, pessoa.EmpresaId })
            .OnDelete(DeleteBehavior.Restrict);
    }
}
