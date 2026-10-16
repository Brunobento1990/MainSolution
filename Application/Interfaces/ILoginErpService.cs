using Application.DTOs;
using Domain.Model;

namespace Application.Interfaces;

public interface ILoginErpService
{
    Task<Resultado<bool>> LoginAsync(
        LoginErpDTO dto,
        CancellationToken cancellationToken = default);
}
