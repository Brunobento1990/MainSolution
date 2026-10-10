namespace Domain.Entidades;

public class UsuarioErpPerfil : EmpresaBase
{
    public UsuarioErpPerfil(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao,
        long empresaId, string nome, bool ativo)
        : base(id, dataHoraCadastro, dataHoraAtualizacao, empresaId)
    {
        Nome = nome;
        Ativo = ativo;
    }

    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public ICollection<UsuarioErpPerfilMenu> Menus { get; set; } = [];
}