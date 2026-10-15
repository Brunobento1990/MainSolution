using Application.Models;
using Domain.Entidades;
using Domain.Model;

namespace Application.Autenticacao;

public interface IGeradorTokenAcesso
{
    string Gerar(UsuarioSessao usuarioSessao);
    Resultado<ValidacaoJwtModel> Validar(string token, bool validarTempoDeVida = true);
}
