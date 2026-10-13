using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Migrations.Migrations;

/// <inheritdoc />
public partial class _20261013233058_InicialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Bloqueios",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Motivo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Ativo = table.Column<bool>(type: "boolean", nullable: false),
                DataHoraDesbloqueio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                DataHoraExpiracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Bloqueios", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Empresas",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RazaoSocial = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                NomeFantasia = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                CpfCnpj = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Empresas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Menus",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                PaiId = table.Column<long>(type: "bigint", nullable: true),
                Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Caminho = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                Icone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                Ordem = table.Column<int>(type: "integer", nullable: false),
                TipoModulo = table.Column<int>(type: "integer", nullable: true),
                Ativo = table.Column<bool>(type: "boolean", nullable: false),
                TipoMenu = table.Column<int>(type: "integer", nullable: true),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Menus", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pessoas",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                DataDeNascimento = table.Column<DateOnly>(type: "date", nullable: true),
                Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                TipoDocumento = table.Column<int>(type: "integer", nullable: false),
                Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmpresaId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pessoas", x => x.Id);
                table.UniqueConstraint("AK_Pessoas_Id_EmpresaId", x => new { x.Id, x.EmpresaId });
                table.ForeignKey(
                    name: "FK_Pessoas_Empresas_EmpresaId",
                    column: x => x.EmpresaId,
                    principalTable: "Empresas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UsuarioErpPerfis",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Ativo = table.Column<bool>(type: "boolean", nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmpresaId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuarioErpPerfis", x => x.Id);
                table.UniqueConstraint("AK_UsuarioErpPerfis_Id_EmpresaId", x => new { x.Id, x.EmpresaId });
                table.ForeignKey(
                    name: "FK_UsuarioErpPerfis_Empresas_EmpresaId",
                    column: x => x.EmpresaId,
                    principalTable: "Empresas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                PessoaId = table.Column<long>(type: "bigint", nullable: false),
                Ativo = table.Column<bool>(type: "boolean", nullable: false),
                Senha = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmpresaId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuarios", x => x.Id);
                table.UniqueConstraint("AK_Usuarios_Id_EmpresaId", x => new { x.Id, x.EmpresaId });
                table.ForeignKey(
                    name: "FK_Usuarios_Pessoas_PessoaId_EmpresaId",
                    columns: x => new { x.PessoaId, x.EmpresaId },
                    principalTable: "Pessoas",
                    principalColumns: new[] { "Id", "EmpresaId" },
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UsuarioErpPerfilMenus",
            columns: table => new
            {
                UsuarioErpPerfilId = table.Column<long>(type: "bigint", nullable: false),
                MenuId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuarioErpPerfilMenus", x => new { x.UsuarioErpPerfilId, x.MenuId });
                table.ForeignKey(
                    name: "FK_UsuarioErpPerfilMenus_Menus_MenuId",
                    column: x => x.MenuId,
                    principalTable: "Menus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UsuarioErpPerfilMenus_UsuarioErpPerfis_UsuarioErpPerfilId",
                    column: x => x.UsuarioErpPerfilId,
                    principalTable: "UsuarioErpPerfis",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UsuarioBloqueios",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                TipoBloqueio = table.Column<int>(type: "integer", nullable: false),
                BloqueioId = table.Column<long>(type: "bigint", nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuarioBloqueios", x => x.Id);
                table.ForeignKey(
                    name: "FK_UsuarioBloqueios_Bloqueios_BloqueioId",
                    column: x => x.BloqueioId,
                    principalTable: "Bloqueios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UsuarioBloqueios_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UsuarioRecuperacoesDeSenha",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                Tipo = table.Column<int>(type: "integer", nullable: false),
                Token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Destino = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                ExpiraEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UtilizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                RevogadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Tentativas = table.Column<int>(type: "integer", nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuarioRecuperacoesDeSenha", x => x.Id);
                table.ForeignKey(
                    name: "FK_UsuarioRecuperacoesDeSenha_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UsuariosErp",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                UsuarioErpPerfilId = table.Column<long>(type: "bigint", nullable: false),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmpresaId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuariosErp", x => x.Id);
                table.ForeignKey(
                    name: "FK_UsuariosErp_UsuarioErpPerfis_UsuarioErpPerfilId_EmpresaId",
                    columns: x => new { x.UsuarioErpPerfilId, x.EmpresaId },
                    principalTable: "UsuarioErpPerfis",
                    principalColumns: new[] { "Id", "EmpresaId" },
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UsuariosErp_Usuarios_UsuarioId_EmpresaId",
                    columns: x => new { x.UsuarioId, x.EmpresaId },
                    principalTable: "Usuarios",
                    principalColumns: new[] { "Id", "EmpresaId" },
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UsuarioSessoes",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                UltimaAtividadeEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ExpiraEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                RevogadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EnderecoIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                UserAgent = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                SistemaOperacional = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                Navegador = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                Dispositivo = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                DataHoraCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataHoraAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuarioSessoes", x => x.Id);
                table.ForeignKey(
                    name: "FK_UsuarioSessoes_Usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "Usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Menus",
            columns: new[] { "Id", "Ativo", "Caminho", "DataHoraAtualizacao", "DataHoraCadastro", "Icone", "Nome", "Ordem", "PaiId", "TipoMenu", "TipoModulo" },
            values: new object[,]
            {
                { 1L, true, "/dashboard", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "clarity:home-line", "Home", 1, null, 1, 1 },
                { 2L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "icon-park-outline:shopping", "E-commerce", 2, null, 2, 1 },
                { 3L, true, "/banners", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "material-symbols-light:planner-banner-ad-pt-outline", "Banners", 1, 2L, 3, 1 },
                { 4L, true, "/lojas-parceiras", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "mdi:store-check-outline", "Lojas parceiras", 2, 2L, 4, 1 },
                { 5L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "healthicons:stock-out-outline", "Estoque", 3, null, 5, 1 },
                { 6L, true, "/estoque/categoria", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "mdi:category-outline", "Categoria", 1, 5L, 6, 1 },
                { 7L, true, "/cadastros/produtos", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "fluent-mdl2:product-variant", "Produto", 2, 5L, 7, 1 },
                { 8L, true, "/estoque/peso", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "game-icons:weight", "Peso", 3, 5L, 8, 1 },
                { 9L, true, "/estoque/tamanho", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "bx:font-size", "Tamanho", 4, 5L, 9, 1 },
                { 10L, true, "/estoque/movimentacao-produto", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "arcticons:stockswidget", "Movimento produto", 5, 5L, 10, 1 },
                { 11L, true, "/estoque/posicao-estoque", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "icon-park-outline:database-position", "Posição estoque", 6, 5L, 11, 1 },
                { 12L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "mdi:report-finance", "Relatórios", 7, 5L, 12, 1 },
                { 13L, true, "/estoque/relatorio-posicao-estoque", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "hugeicons:package-out-of-stock", "Posição de estoque", 1, 12L, 13, 1 },
                { 14L, true, "/estoque/relatorio-estoque", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "tabler:report-search", "Relatório de estoque", 2, 12L, 14, 1 },
                { 15L, true, "/estoque/relatorio-producao", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "simple-icons:soundcharts", "Relatório de produção", 3, 12L, 15, 1 },
                { 16L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "carbon:sales-ops", "Vendas", 4, null, 16, 1 },
                { 17L, true, "/pedidos", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "material-symbols:order-approve-outline", "Pedidos", 1, 16L, 17, 1 },
                { 18L, true, "/vendas/tabela-de-preco", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:tag-price-bold", "Tabela de preço", 2, 16L, 18, 1 },
                { 19L, true, "/vendas/preco-por-peso", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "material-symbols-light:price-change-outline-rounded", "Preço por peso", 3, 16L, 19, 1 },
                { 20L, true, "/vendas/preco-por-tamanho", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:tag-price-outline", "Preço por tamanho", 4, 16L, 20, 1 },
                { 21L, true, "/vendas/relatorio-por-periodo", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "tabler:report-search", "Relatório por período", 5, 16L, 21, 1 },
                { 22L, true, "/vendas/relatorio-venda-produto", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "fluent-mdl2:product-variant", "Relatório venda produto", 6, 16L, 22, 1 },
                { 23L, true, "/vendas/cliente", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:user-id-linear", "Clientes", 7, 16L, 23, 1 },
                { 24L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "iwwa:settings", "Configurações", 5, null, 24, 1 },
                { 25L, true, "/configuracao/minha-empresa", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "mdi:company", "Minha empresa", 1, 24L, 25, 1 },
                { 26L, true, "/configuracao/pedido", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "material-symbols:order-approve-outline", "Pedidos", 2, 24L, 26, 1 },
                { 27L, true, "/configuracao/mercado-pago", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "simple-icons:mercadopago", "Mercado Pago", 3, 24L, 27, 1 },
                { 28L, true, "/configuracao/frete", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "fluent:vehicle-truck-cube-24-regular", "Frete", 4, 24L, 28, 1 },
                { 29L, true, null, null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "rivet-icons:money", "Financeiro", 6, null, 29, 1 },
                { 30L, true, "/financeiro/contas-a-receber", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "game-icons:receive-money", "Contas a receber", 1, 29L, 30, 1 },
                { 31L, true, "/financeiro/contas-a-pagar", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "game-icons:pay-money", "Contas a pagar", 2, 29L, 31, 1 },
                { 32L, true, "/financeiro/bonificados", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "mdi:gift-outline", "Bonificados", 3, 29L, 32, 1 },
                { 33L, true, "/financeiro/transacoes", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "grommet-icons:transaction", "Transações", 4, 29L, 33, 1 },
                { 34L, true, "/financeiro/mensalidades", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "hugeicons:payment-02", "Mensalidades", 5, 29L, 34, 1 },
                { 42L, true, "/link-da-bio/configuracao", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:link-circle-linear", "Link da Bio", 3, 2L, 42, 1 },
                { 43L, true, "/link-da-bio/eventos", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:chart-2-linear", "Eventos do Link da Bio", 4, 2L, 43, 1 },
                { 44L, true, "/cadastros/representantes", null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "solar:user-speak-linear", "Representantes", 8, 16L, 44, 1 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Empresas_CpfCnpj",
            table: "Empresas",
            column: "CpfCnpj",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Pessoas_EmpresaId_Documento",
            table: "Pessoas",
            columns: new[] { "EmpresaId", "Documento" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioBloqueios_BloqueioId",
            table: "UsuarioBloqueios",
            column: "BloqueioId");

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioBloqueios_UsuarioId",
            table: "UsuarioBloqueios",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioErpPerfilMenus_MenuId",
            table: "UsuarioErpPerfilMenus",
            column: "MenuId");

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioErpPerfis_EmpresaId_Nome",
            table: "UsuarioErpPerfis",
            columns: new[] { "EmpresaId", "Nome" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioRecuperacoesDeSenha_Token",
            table: "UsuarioRecuperacoesDeSenha",
            column: "Token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioRecuperacoesDeSenha_UsuarioId",
            table: "UsuarioRecuperacoesDeSenha",
            column: "UsuarioId");

        migrationBuilder.CreateIndex(
            name: "IX_Usuarios_Login",
            table: "Usuarios",
            column: "Login");

        migrationBuilder.CreateIndex(
            name: "IX_Usuarios_Login_Ativo",
            table: "Usuarios",
            columns: new[] { "Login", "Ativo" });

        migrationBuilder.CreateIndex(
            name: "IX_Usuarios_PessoaId_EmpresaId",
            table: "Usuarios",
            columns: new[] { "PessoaId", "EmpresaId" });

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosErp_EmpresaId_UsuarioId",
            table: "UsuariosErp",
            columns: new[] { "EmpresaId", "UsuarioId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosErp_UsuarioErpPerfilId_EmpresaId",
            table: "UsuariosErp",
            columns: new[] { "UsuarioErpPerfilId", "EmpresaId" });

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosErp_UsuarioId_EmpresaId",
            table: "UsuariosErp",
            columns: new[] { "UsuarioId", "EmpresaId" });

        migrationBuilder.CreateIndex(
            name: "IX_UsuarioSessoes_UsuarioId",
            table: "UsuarioSessoes",
            column: "UsuarioId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UsuarioBloqueios");

        migrationBuilder.DropTable(
            name: "UsuarioErpPerfilMenus");

        migrationBuilder.DropTable(
            name: "UsuarioRecuperacoesDeSenha");

        migrationBuilder.DropTable(
            name: "UsuariosErp");

        migrationBuilder.DropTable(
            name: "UsuarioSessoes");

        migrationBuilder.DropTable(
            name: "Bloqueios");

        migrationBuilder.DropTable(
            name: "Menus");

        migrationBuilder.DropTable(
            name: "UsuarioErpPerfis");

        migrationBuilder.DropTable(
            name: "Usuarios");

        migrationBuilder.DropTable(
            name: "Pessoas");

        migrationBuilder.DropTable(
            name: "Empresas");
    }
}
