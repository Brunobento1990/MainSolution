using Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Bloqueio> Bloqueios => Set<Bloqueio>();
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UsuarioBloqueio> UsuarioBloqueios => Set<UsuarioBloqueio>();
    public DbSet<UsuarioErp> UsuariosErp => Set<UsuarioErp>();
    public DbSet<UsuarioErpPerfil> UsuarioErpPerfis => Set<UsuarioErpPerfil>();
    public DbSet<UsuarioErpPerfilMenu> UsuarioErpPerfilMenus => Set<UsuarioErpPerfilMenu>();
    public DbSet<UsuarioRecuperacaoDeSenha> UsuarioRecuperacoesDeSenha => Set<UsuarioRecuperacaoDeSenha>();
    public DbSet<UsuarioSessao> UsuarioSessoes => Set<UsuarioSessao>();
    public DbSet<UsuarioTentativaLogin> UsuarioTentativasLogin => Set<UsuarioTentativaLogin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
