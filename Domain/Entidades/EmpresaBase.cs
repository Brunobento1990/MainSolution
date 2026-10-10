namespace Domain.Entidades;

public abstract class EmpresaBase : EntidadeBase
{
    protected EmpresaBase(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, 
        long empresaId)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        EmpresaId = empresaId;
    }

    public long EmpresaId { get; protected set; }
}