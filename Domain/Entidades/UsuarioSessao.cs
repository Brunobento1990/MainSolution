namespace Domain.Entidades;

public sealed class UsuarioSessao : EntidadeBase
{
    public UsuarioSessao(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, long usuarioId, DateTime? ultimaAtividadeEm, DateTime expiraEm,
        DateTime? revogadoEm, string? enderecoIp, string? userAgent, string? sistemaOperacional, string? navegador,
        string? dispositivo)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        UsuarioId = usuarioId;
        UltimaAtividadeEm = ultimaAtividadeEm;
        ExpiraEm = expiraEm;
        RevogadoEm = revogadoEm;
        EnderecoIp = enderecoIp;
        UserAgent = userAgent;
        SistemaOperacional = sistemaOperacional;
        Navegador = navegador;
        Dispositivo = dispositivo;
    }

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public DateTime? UltimaAtividadeEm { get; private set; }
    public DateTime ExpiraEm { get; private set; }
    public DateTime? RevogadoEm { get; private set; }
    public string? EnderecoIp { get; private set; }
    public string? UserAgent { get; private set; }
    public string? SistemaOperacional { get; private set; }
    public string? Navegador { get; private set; }
    public string? Dispositivo { get; private set; }
}