using Domain.Entidades;

namespace Application.Repositories;

public interface IUsuarioTentativaLoginRepository : IRepository<UsuarioTentativaLogin>
{
    Task<UsuarioTentativaLogin?> ObterPorUsuarioIdAsync(
        long usuarioId,
        CancellationToken cancellationToken = default);

    Task<int> ZerarErrosSenhaAsync(
        long usuarioId,
        CancellationToken cancellationToken = default);
}
