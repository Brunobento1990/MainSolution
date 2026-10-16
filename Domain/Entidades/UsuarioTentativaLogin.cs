namespace Domain.Entidades;

public sealed class UsuarioTentativaLogin : EntidadeBase
{
    public UsuarioTentativaLogin(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao,
        long usuarioId,
        int quantidadeErrosSenha)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        UsuarioId = usuarioId;
        QuantidadeErrosSenha = quantidadeErrosSenha;
    }

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public int QuantidadeErrosSenha { get; private set; }

    public static UsuarioTentativaLogin Novo(long usuarioId)
    {
        return new UsuarioTentativaLogin(
            0,
            DateTime.UtcNow,
            null,
            usuarioId,
            0);
    }

    public void RegistrarErroSenha()
    {
        if (QuantidadeErrosSenha < int.MaxValue)
        {
            QuantidadeErrosSenha++;
        }

        DataHoraAtualizacao = DateTime.UtcNow;
    }
}
