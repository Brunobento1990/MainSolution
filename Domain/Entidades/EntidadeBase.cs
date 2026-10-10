namespace Domain.Entidades;

public abstract class EntidadeBase
{
    public EntidadeBase(long id, DateTime dataHoraCadastro, DateTime? dataHoraAtualizacao)
    {
        Id = id;
        DataHoraCadastro = dataHoraCadastro;
        DataHoraAtualizacao = dataHoraAtualizacao;
    }

    public long Id { get; protected set; }
    public DateTime DataHoraCadastro { get; protected set; }
    public DateTime? DataHoraAtualizacao { get; protected set; }
}