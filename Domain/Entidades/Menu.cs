using Domain.Enuns;

namespace Domain.Entidades;

public sealed class Menu : EntidadeBase
{
    public Menu(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, long? paiId, string nome, string? caminho, string? icone, int ordem,
        Guid? empresaId, TipoModuloEnum? tipoModulo, bool ativo, TipoMenuEnum? tipoMenu)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        PaiId = paiId;
        Nome = nome;
        Caminho = caminho;
        Icone = icone;
        Ordem = ordem;
        EmpresaId = empresaId;
        TipoModulo = tipoModulo;
        Ativo = ativo;
        TipoMenu = tipoMenu;
    }

    public long? PaiId { get; private set; }
    public string Nome { get; private set; }
    public string? Caminho { get; private set; }
    public string? Icone { get; private set; }
    public int Ordem { get; private set; }
    public Guid? EmpresaId { get; private set; }
    public TipoModuloEnum? TipoModulo { get; private set; }
    public bool Ativo { get; private set; }
    public TipoMenuEnum? TipoMenu { get; private set; }

    public bool EhDashboard
    {
        get => TipoMenu == TipoMenuEnum.Dashboard;
    }
}