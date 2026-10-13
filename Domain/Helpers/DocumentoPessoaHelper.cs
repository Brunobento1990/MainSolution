using Domain.Enuns;
using Domain.EntidadeConfiguracoes;

namespace Domain.Helpers;

public static class DocumentoPessoaHelper
{
    public static string? Validar(TipoDocumentoPessoaEnum tipoDocumento, string documento)
    {
        return tipoDocumento switch
        {
            TipoDocumentoPessoaEnum.Fisica when CpfCnpjHelper.EhCpfValido(documento) => null,
            TipoDocumentoPessoaEnum.Fisica => PessoaConfiguracoes.ErroCpfInvalido,
            TipoDocumentoPessoaEnum.Juridica when CpfCnpjHelper.EhCnpjValido(documento) => null,
            TipoDocumentoPessoaEnum.Juridica => PessoaConfiguracoes.ErroCnpjInvalido,
            _ => null
        };
    }
}
