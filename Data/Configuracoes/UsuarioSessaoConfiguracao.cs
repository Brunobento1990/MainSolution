using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class UsuarioSessaoConfiguracao : EntidadeBaseConfiguracao<UsuarioSessao>
{
    public override void Configure(EntityTypeBuilder<UsuarioSessao> builder)
    {
        base.Configure(builder);

        builder.Property(sessao => sessao.EnderecoIp)
            .HasMaxLength(UsuarioSessaoConfiguracoes.EnderecoIpMaxLength)
            .IsRequired(false);

        builder.Property(sessao => sessao.UserAgent)
            .HasMaxLength(UsuarioSessaoConfiguracoes.UserAgentMaxLength)
            .IsRequired(false);

        builder.Property(sessao => sessao.SistemaOperacional)
            .HasMaxLength(UsuarioSessaoConfiguracoes.SistemaOperacionalMaxLength)
            .IsRequired(false);

        builder.Property(sessao => sessao.Navegador)
            .HasMaxLength(UsuarioSessaoConfiguracoes.NavegadorMaxLength)
            .IsRequired(false);

        builder.Property(sessao => sessao.Dispositivo)
            .HasMaxLength(UsuarioSessaoConfiguracoes.DispositivoMaxLength)
            .IsRequired(false);

        builder.Property(sessao => sessao.ExpiraEm)
            .IsRequired();

        builder.HasOne(sessao => sessao.Usuario)
            .WithMany()
            .HasForeignKey(sessao => sessao.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
