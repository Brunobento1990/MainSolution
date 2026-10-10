using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class PessoaConfiguracao : EmpresaBaseConfiguracao<Pessoa>
{
    public override void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        base.Configure(builder);

        builder.Property(pessoa => pessoa.Nome)
            .HasMaxLength(PessoaConfiguracoes.NomeMaxLength)
            .IsRequired();

        builder.Property(pessoa => pessoa.Documento)
            .HasMaxLength(PessoaConfiguracoes.DocumentoMaxLength)
            .IsRequired(false);

        builder.Property(pessoa => pessoa.TipoDocumento)
            .IsRequired();

        builder.Property(pessoa => pessoa.Telefone)
            .HasMaxLength(PessoaConfiguracoes.TelefoneMaxLength)
            .IsRequired(false);

        builder.Property(pessoa => pessoa.Email)
            .HasMaxLength(PessoaConfiguracoes.EmailMaxLength)
            .IsRequired(false);

        builder.HasIndex(pessoa => new { pessoa.EmpresaId, pessoa.Documento })
            .IsUnique();

        builder.Ignore(pessoa => pessoa.Idade);
    }
}
