using Application.Repositories;
using Data.Context;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public sealed class UsuarioTentativaLoginRepository
    : Repository<UsuarioTentativaLogin>, IUsuarioTentativaLoginRepository
{
    public UsuarioTentativaLoginRepository(AppDbContext context)
        : base(context)
    {
    }

    public Task<UsuarioTentativaLogin?> ObterPorUsuarioIdAsync(
        long usuarioId,
        CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(
            tentativa => tentativa.UsuarioId == usuarioId,
            cancellationToken);
    }

    public Task<int> ZerarErrosSenhaAsync(
        long usuarioId,
        CancellationToken cancellationToken = default)
    {
        return DbSet
            .Where(tentativa => tentativa.UsuarioId == usuarioId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
