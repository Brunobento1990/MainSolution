using Domain.Enuns;

namespace Domain.Entidades;

public class UsuarioRecuperacaoDeSenha : EntidadeBase
{
    public UsuarioRecuperacaoDeSenha(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, long usuarioId, TipoRecuperacaoDeSenhaEnum tipo, string token, string destino, DateTime expiraEm, DateTime? utilizadoEm, DateTime? revogadoEm, int tentativas)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        UsuarioId = usuarioId;
        Tipo = tipo;
        Token = token;
        Destino = destino;
        ExpiraEm = expiraEm;
        UtilizadoEm = utilizadoEm;
        RevogadoEm = revogadoEm;
        Tentativas = tentativas;
    }

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public TipoRecuperacaoDeSenhaEnum Tipo { get; private set; }
    public string Token { get; private set; }
    public string Destino { get; private set; }
    public DateTime ExpiraEm { get; private set; }
    public DateTime? UtilizadoEm { get; private set; }
    public DateTime? RevogadoEm { get; private set; }
    public int Tentativas { get; private set; }
}