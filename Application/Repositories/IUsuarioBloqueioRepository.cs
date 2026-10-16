using Domain.Entidades;

namespace Application.Repositories;

public interface IUsuarioBloqueioRepository : IRepository<UsuarioBloqueio>
{
    Task<UsuarioBloqueio?> ObterBloqueioAtivoAsync(
        long usuarioId,
        bool considerarBloqueioPorTentativasSenha,
        CancellationToken cancellationToken = default);
}
