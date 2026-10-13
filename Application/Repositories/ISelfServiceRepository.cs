using Domain.Entidades;

namespace Application.Repositories;

public interface ISelfServiceRepository
{
    Task<bool> ExisteEmpresaAsync(string cpfCnpj, CancellationToken cancellationToken = default);
    Task<bool> ExisteUsuarioErpAsync(string login, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<long>> ObterMenusAtivosIdsAsync(CancellationToken cancellationToken = default);
    Task<bool> AdicionarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken = default);
    Task AdicionarUsuarioErpAsync(UsuarioErp usuarioErp, CancellationToken cancellationToken = default);
    Task RemoverEmpresaAsync(Empresa empresa, CancellationToken cancellationToken = default);
}
