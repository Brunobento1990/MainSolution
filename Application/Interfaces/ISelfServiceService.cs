using Application.DTOs;
using Application.Models;

namespace Application.Interfaces;

public interface ISelfServiceService
{
    Task<CadastrarEmpresaSelfServiceResultado> CadastrarEmpresaAsync(
        CadastrarEmpresaSelfServiceDTO dto,
        CancellationToken cancellationToken = default);
}
