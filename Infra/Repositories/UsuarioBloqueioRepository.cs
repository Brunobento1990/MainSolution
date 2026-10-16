using Application.Repositories;
using Data.Context;
using Domain.Entidades;
using Domain.Enuns;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public sealed class UsuarioBloqueioRepository
    : Repository<UsuarioBloqueio>, IUsuarioBloqueioRepository
{
    public UsuarioBloqueioRepository(AppDbContext context)
        : base(context)
    {
    }

    public Task<UsuarioBloqueio?> ObterBloqueioAtivoAsync(
        long usuarioId,
        bool considerarBloqueioPorTentativasSenha,
        CancellationToken cancellationToken = default)
    {
        var dataHoraAtual = DateTime.UtcNow;

        return DbSet
            .AsNoTracking()
            .Include(usuarioBloqueio => usuarioBloqueio.Bloqueio)
            .FirstOrDefaultAsync(
                usuarioBloqueio => usuarioBloqueio.UsuarioId == usuarioId
                                   && usuarioBloqueio.Bloqueio.Ativo
                                   && (!usuarioBloqueio.Bloqueio.DataHoraExpiracao.HasValue
                                       || usuarioBloqueio.Bloqueio.DataHoraExpiracao > dataHoraAtual)
                                   && (considerarBloqueioPorTentativasSenha
                                       || usuarioBloqueio.TipoBloqueio
                                       != TipoBloqueioUsuarioEnum.TentativasLoginErroSenha),
                cancellationToken);
    }
}
