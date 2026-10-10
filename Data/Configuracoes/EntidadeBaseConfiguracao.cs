using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal class EntidadeBaseConfiguracao<T> : IEntityTypeConfiguration<T>
    where T : EntidadeBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(entidade => entidade.Id);

        builder.Property(entidade => entidade.Id)
            .ValueGeneratedOnAdd();

        builder.Property(entidade => entidade.DataHoraCadastro)
            .IsRequired();

        builder.Property(entidade => entidade.DataHoraAtualizacao)
            .IsRequired(false);
    }
}
