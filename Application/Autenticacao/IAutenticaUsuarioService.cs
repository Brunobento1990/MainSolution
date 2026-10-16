using Domain.Entidades;
using Domain.Model;

namespace Application.Autenticacao;

public interface IAutenticaUsuarioService
{
    Task<Resultado<bool>> ValidarAcessoAsync(
        Usuario usuario,
        string senha,
        CancellationToken cancellationToken = default);
}
