namespace Domain.Entidades;

public sealed class Empresa : EntidadeBase
{
    public Empresa(
        long id,
        DateTime dataHoraCadastro,
        DateTime? dataHoraAtualizacao, string razaoSocial, string nomeFantasia, string cpfCnpj)
        : base(id, dataHoraCadastro, dataHoraAtualizacao)
    {
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        CpfCnpj = cpfCnpj;
    }

    public string RazaoSocial { get; private set; }
    public string NomeFantasia { get; private set; }
    public string CpfCnpj { get; private set; }
}