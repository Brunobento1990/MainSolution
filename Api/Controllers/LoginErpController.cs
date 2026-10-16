using Application.DTOs;
using Application.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("erp")]
public sealed class LoginErpController : ControllerBase
{
    private readonly ILoginErpService _loginErpService;

    public LoginErpController(ILoginErpService loginErpService)
    {
        _loginErpService = loginErpService;
    }

    [HttpPost("login")]
    [ProducesResponseType<Resultado<bool>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Login(
        [FromBody] LoginErpDTO dto,
        CancellationToken cancellationToken = default)
    {
        var resultado = await _loginErpService.LoginAsync(dto, cancellationToken);

        return resultado.Sucesso
            ? StatusCode(StatusCodes.Status501NotImplemented)
            : BadRequest(resultado);
    }
}
