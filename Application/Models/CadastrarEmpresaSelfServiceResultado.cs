namespace Application.Models;

public sealed class CadastrarEmpresaSelfServiceResultado
{
    public long? EmpresaId { get; init; }
    public long? UsuarioId { get; init; }
    public ICollection<string> Erros { get; init; } = [];
    public bool Sucesso => Erros.Count == 0;
}
