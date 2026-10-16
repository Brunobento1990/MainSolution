using Application.Repositories;
using Data.Context;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public sealed class LoginRepository : ILoginRepository
{
    private readonly AppDbContext _context;

    public LoginRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<UsuarioErp>> ObterUsuariosErpPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.UsuariosErp
            .AsNoTracking()
            .Include(usuarioErp => usuarioErp.Usuario)
            .Where(usuarioErp => usuarioErp.Usuario.Ativo
                                 && usuarioErp.Usuario.Login == email)
            .ToListAsync(cancellationToken);
    }
}
