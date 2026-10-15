using Domain.Model;

namespace Application.Models;

public class UsuarioLogado : IUsuarioLogado
{
    public long SessaoId { get; set; }
}