using Domain.Enuns;
using Domain.Extensions;

namespace Domain.Entidades;

public sealed class Pessoa : EmpresaBase
{
    public Pessoa(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao,
        long empresaId, string nome, DateOnly? dataDeNascimento, string? documento,
        TipoDocumentoPessoaEnum tipoDocumento, string? telefone, string? email)
        : base(id, dataHoraCadastro, dataHoraAtualizacao, empresaId)
    {
        Nome = nome;
        DataDeNascimento = dataDeNascimento;
        Documento = documento;
        TipoDocumento = tipoDocumento;
        Telefone = telefone;
        Email = email;
    }

    public string Nome { get; private set; }
    public DateOnly? DataDeNascimento { get; private set; }
    public int? Idade => DataDeNascimento.Idade();
    public string? Documento { get; private set; }
    public TipoDocumentoPessoaEnum TipoDocumento { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
}