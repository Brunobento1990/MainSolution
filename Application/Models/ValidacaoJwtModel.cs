using Domain.Model;

namespace Application.Models;

public class ValidacaoJwtModel
{
    public bool Expirado { get; set; }
    public IUsuarioLogado? UsuarioLogado { get; set; }
}