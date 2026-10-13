using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("self-service")]
public sealed class SelfServiceController : ControllerBase
{
    private readonly ISelfServiceService _selfServiceService;

    public SelfServiceController(ISelfServiceService selfServiceService)
    {
        _selfServiceService = selfServiceService;
    }

    [HttpPost("empresas")]
    [ProducesResponseType<CadastrarEmpresaSelfServiceResultado>(StatusCodes.Status201Created)]
    [ProducesResponseType<CadastrarEmpresaSelfServiceResultado>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarEmpresa(
        [FromBody] CadastrarEmpresaSelfServiceDTO dto,
        CancellationToken cancellationToken)
    {
        var resultado = await _selfServiceService.CadastrarEmpresaAsync(dto, cancellationToken);

        return resultado.Sucesso
            ? StatusCode(StatusCodes.Status201Created, resultado)
            : BadRequest(resultado);
    }
}
