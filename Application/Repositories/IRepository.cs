using Domain.Entidades;

namespace Application.Repositories;

public interface IRepository<T>
    where T : EntidadeBase
{
    Task AddAsync(T entidade, CancellationToken cancellationToken = default);
    void Update(T entidade);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
