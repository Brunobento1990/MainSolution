using Application.Repositories;
using Data.Context;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public abstract class Repository<T> : IRepository<T>
    where T : EntidadeBase
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task AddAsync(
        T entidade,
        CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entidade, cancellationToken);
    }

    public void Update(T entidade)
    {
        DbSet.Update(entidade);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }
}
