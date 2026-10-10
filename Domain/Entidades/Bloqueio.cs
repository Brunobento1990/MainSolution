namespace Domain.Entidades;

public sealed class Bloqueio : EntidadeBase
{
    public Bloqueio(
        long id, 
        DateTime dataHoraCadastro, 
        DateTime? dataHoraAtualizacao, string motivo, bool ativo, DateTime? dataHoraDesbloqueio) 
            : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        Motivo = motivo;
        Ativo = ativo;
        DataHoraDesbloqueio = dataHoraDesbloqueio;
    }

    public string Motivo { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime? DataHoraDesbloqueio { get; private set; }
    public DateTime? DataHoraExpiracao { get; private set; }
}