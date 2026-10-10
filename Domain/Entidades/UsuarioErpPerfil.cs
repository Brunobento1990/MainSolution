using Domain.EntidadeConfiguracoes;
using Domain.Extensions;

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

    public static UsuarioErpPerfil Novo(long empresaId, string nome)
    {
        return new UsuarioErpPerfil(
            0,
            DateTime.UtcNow,
            null,
            empresaId,
            nome.Limitar(UsuarioErpPerfilConfiguracoes.NomeMaxLength),
            true);
    }
}
