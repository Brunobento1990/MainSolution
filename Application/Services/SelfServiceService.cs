using Application.Adapters;
using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Application.Repositories;
using Domain.Entidades;
using Domain.EntidadeConfiguracoes;

namespace Application.Services;

internal sealed class SelfServiceService : ISelfServiceService
{
    private const string NomePerfilMaster = "Master";
    private readonly ISelfServiceRepository _repository;

    public SelfServiceService(ISelfServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<CadastrarEmpresaSelfServiceResultado> CadastrarEmpresaAsync(
        CadastrarEmpresaSelfServiceDTO dto,
        CancellationToken cancellationToken = default)
    {
        var erros = dto.Validar();

        if (erros.Count > 0)
        {
            return new CadastrarEmpresaSelfServiceResultado { Erros = erros };
        }

        if (await _repository.ExisteEmpresaAsync(dto.CpfCnpj, cancellationToken))
        {
            erros.Add(EmpresaConfiguracoes.ErroCpfCnpjDuplicado);
        }

        if (await _repository.ExisteUsuarioErpAsync(dto.Email, cancellationToken))
        {
            erros.Add(UsuarioErpConfiguracoes.ErroLoginDuplicado);
        }

        if (erros.Count > 0)
        {
            return new CadastrarEmpresaSelfServiceResultado { Erros = erros };
        }

        var empresa = Empresa.Novo(dto.RazaoSocial, dto.NomeFantasia, dto.CpfCnpj);
        if (!await _repository.AdicionarEmpresaAsync(empresa, cancellationToken))
        {
            return new CadastrarEmpresaSelfServiceResultado
            {
                Erros = [EmpresaConfiguracoes.ErroCpfCnpjDuplicado]
            };
        }

        try
        {
            var perfilMaster = UsuarioErpPerfil.Novo(empresa.Id, NomePerfilMaster);
            var menusIds = await _repository.ObterMenusAtivosIdsAsync(cancellationToken);
            perfilMaster.AdicionarMenus(menusIds);

            var pessoa = Pessoa.Novo(
                empresa.Id,
                dto.Nome,
                dto.DataDeNascimento,
                dto.Documento,
                dto.TipoDocumento,
                dto.Telefone,
                dto.Email);

            var usuario = Usuario.Novo(
                pessoa,
                PasswordAdapter.GenerateHash(dto.Senha),
                dto.Email);

            var usuarioErp = UsuarioErp.Novo(empresa.Id, perfilMaster, usuario);
            await _repository.AdicionarUsuarioErpAsync(usuarioErp, cancellationToken);

            return new CadastrarEmpresaSelfServiceResultado
            {
                EmpresaId = empresa.Id,
                UsuarioId = usuario.Id
            };
        }
        catch (Exception erroCadastro)
        {
            try
            {
                await _repository.RemoverEmpresaAsync(empresa, CancellationToken.None);
            }
            catch (Exception erroRemocao)
            {
                throw new AggregateException(erroCadastro, erroRemocao);
            }

            throw;
        }
    }
}
