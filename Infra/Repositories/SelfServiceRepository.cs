using Application.Repositories;
using Data.Configuracoes;
using Data.Context;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infra.Repositories;

public sealed class SelfServiceRepository : ISelfServiceRepository
{
    private readonly AppDbContext _context;

    public SelfServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExisteEmpresaAsync(
        string cpfCnpj,
        CancellationToken cancellationToken = default)
    {
        return _context.Empresas.AnyAsync(
            empresa => empresa.CpfCnpj == cpfCnpj,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<long>> ObterMenusAtivosIdsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Menus
            .AsNoTracking()
            .Where(menu => menu.Ativo)
            .Select(menu => menu.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AdicionarEmpresaAsync(
        Empresa empresa,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Empresas.AddAsync(empresa, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is PostgresException
                  {
                      SqlState: PostgresErrorCodes.UniqueViolation,
                      ConstraintName: EmpresaPersistenciaConfiguracoes.CpfCnpjIndiceUnicoNome
                  })
        {
            _context.Entry(empresa).State = EntityState.Detached;
            return false;
        }
    }

    public async Task AdicionarUsuarioErpAsync(
        UsuarioErp usuarioErp,
        CancellationToken cancellationToken = default)
    {
        await _context.UsuariosErp.AddAsync(usuarioErp, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverEmpresaAsync(
        Empresa empresa,
        CancellationToken cancellationToken = default)
    {
        _context.ChangeTracker.Clear();
        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
