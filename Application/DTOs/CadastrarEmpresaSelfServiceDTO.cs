using Application.Attributes;
using Domain.EntidadeConfiguracoes;
using Domain.Enuns;
using Domain.Helpers;

namespace Application.DTOs;

public sealed class CadastrarEmpresaSelfServiceDTO : ValidarBaseDTO
{
    [ValidarString(
        EmpresaConfiguracoes.ErroRazaoSocialObrigatoria,
        EmpresaConfiguracoes.ErroRazaoSocialMaxLength,
        EmpresaConfiguracoes.RazaoSocialMaxLength)]
    public string RazaoSocial { get; set; } = string.Empty;

    [ValidarString(
        EmpresaConfiguracoes.ErroNomeFantasiaObrigatorio,
        EmpresaConfiguracoes.ErroNomeFantasiaMaxLength,
        EmpresaConfiguracoes.NomeFantasiaMaxLength)]
    public string NomeFantasia { get; set; } = string.Empty;

    [ValidarCpfCnpj(
        EmpresaConfiguracoes.ErroCpfCnpjObrigatorio,
        EmpresaConfiguracoes.ErroCpfCnpjMaxLength,
        EmpresaConfiguracoes.ErroCpfCnpjInvalido,
        EmpresaConfiguracoes.CpfCnpjMaxLength)]
    public string CpfCnpj { get; set; } = string.Empty;

    [ValidarString(
        PessoaConfiguracoes.ErroNomeObrigatorio,
        PessoaConfiguracoes.ErroNomeMaxLength,
        PessoaConfiguracoes.NomeMaxLength)]
    public string Nome { get; set; } = string.Empty;

    public DateOnly? DataDeNascimento { get; set; }

    [ValidarString(
        PessoaConfiguracoes.ErroDocumentoObrigatorio,
        PessoaConfiguracoes.ErroDocumentoMaxLength,
        PessoaConfiguracoes.DocumentoMaxLength)]
    public string Documento { get; set; } = string.Empty;

    [ValidarEnumObrigatorio(typeof(TipoDocumentoPessoaEnum), PessoaConfiguracoes.ErroTipoDocumentoInvalido)]
    public TipoDocumentoPessoaEnum TipoDocumento { get; set; }

    [ValidarStringOpcional(PessoaConfiguracoes.ErroTelefoneMaxLength, PessoaConfiguracoes.TelefoneMaxLength)]
    public string? Telefone { get; set; }

    [ValidarEmail(
        PessoaConfiguracoes.ErroEmailObrigatorio,
        PessoaConfiguracoes.ErroEmailInvalido,
        PessoaConfiguracoes.ErroEmailMaxLength,
        PessoaConfiguracoes.EmailMaxLength)]
    public string Email { get; set; } = string.Empty;

    [ValidarSenha(
        UsuarioConfiguracoes.ErroSenhaObrigatoria,
        UsuarioConfiguracoes.ErroSenhaEntradaMaxLength,
        UsuarioConfiguracoes.SenhaEntradaMaxBytes)]
    public string Senha { get; set; } = string.Empty;

    public override ICollection<string> Validar()
    {
        var erros = base.Validar();
        var documentoPodeSerValidado = !string.IsNullOrWhiteSpace(Documento)
                                       && Documento.Length <= PessoaConfiguracoes.DocumentoMaxLength
                                       && Enum.IsDefined(TipoDocumento);
        var erroDocumento = documentoPodeSerValidado
            ? DocumentoPessoaHelper.Validar(TipoDocumento, Documento)
            : null;

        if (!string.IsNullOrWhiteSpace(erroDocumento))
        {
            erros.Add(erroDocumento);
        }

        return erros;
    }
}
