using Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal class EmpresaBaseConfiguracao<T> : EntidadeBaseConfiguracao<T>
    where T : EmpresaBase
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(entidade => entidade.EmpresaId)
            .IsRequired();
    }
}
