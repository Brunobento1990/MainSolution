using Domain.Entidades;

namespace Application.Repositories;

public interface ILoginRepository
{
    Task<IReadOnlyCollection<UsuarioErp>> ObterUsuariosErpPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}
