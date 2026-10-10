using Domain.EntidadeConfiguracoes;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class EmpresaConfiguracao : EntidadeBaseConfiguracao<Empresa>
{
    public override void Configure(EntityTypeBuilder<Empresa> builder)
    {
        base.Configure(builder);

        builder.Property(empresa => empresa.RazaoSocial)
            .HasMaxLength(EmpresaConfiguracoes.RazaoSocialMaxLength)
            .IsRequired();

        builder.Property(empresa => empresa.NomeFantasia)
            .HasMaxLength(EmpresaConfiguracoes.NomeFantasiaMaxLength)
            .IsRequired();

        builder.Property(empresa => empresa.CpfCnpj)
            .HasMaxLength(EmpresaConfiguracoes.CpfCnpjMaxLength)
            .IsRequired();

        builder.HasIndex(empresa => empresa.CpfCnpj)
            .IsUnique();
    }
}
