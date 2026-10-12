using Domain.Entidades;
using Domain.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuracoes;

internal sealed class MenuSeedConfiguracao : IEntityTypeConfiguration<Menu>
{
    private static readonly DateTime DataHoraCadastro =
        new(2026, 10, 11, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasData(
            Criar(1, null, "Home", "/dashboard", "clarity:home-line", 1, TipoMenuEnum.Dashboard),
            Criar(2, null, "E-commerce", null, "icon-park-outline:shopping", 2, TipoMenuEnum.Ecommerce),
            Criar(3, 2, "Banners", "/banners", "material-symbols-light:planner-banner-ad-pt-outline", 1, TipoMenuEnum.EcommerceBanner),
            Criar(4, 2, "Lojas parceiras", "/lojas-parceiras", "mdi:store-check-outline", 2, TipoMenuEnum.EcommerceLojaParceira),
            Criar(42, 2, "Link da Bio", "/link-da-bio/configuracao", "solar:link-circle-linear", 3, TipoMenuEnum.EcommerceLinkBio),
            Criar(43, 2, "Eventos do Link da Bio", "/link-da-bio/eventos", "solar:chart-2-linear", 4, TipoMenuEnum.EcommerceLinkBioEventos),

            Criar(5, null, "Estoque", null, "healthicons:stock-out-outline", 3, TipoMenuEnum.Estoque),
            Criar(6, 5, "Categoria", "/estoque/categoria", "mdi:category-outline", 1, TipoMenuEnum.EstoqueCategoria),
            Criar(7, 5, "Produto", "/cadastros/produtos", "fluent-mdl2:product-variant", 2, TipoMenuEnum.EstoqueProduto),
            Criar(8, 5, "Peso", "/estoque/peso", "game-icons:weight", 3, TipoMenuEnum.EstoquePeso),
            Criar(9, 5, "Tamanho", "/estoque/tamanho", "bx:font-size", 4, TipoMenuEnum.EstoqueTamanho),
            Criar(10, 5, "Movimento produto", "/estoque/movimentacao-produto", "arcticons:stockswidget", 5, TipoMenuEnum.EstoqueMovimentoProduto),
            Criar(11, 5, "Posição estoque", "/estoque/posicao-estoque", "icon-park-outline:database-position", 6, TipoMenuEnum.EstoquePosicao),
            Criar(12, 5, "Relatórios", null, "mdi:report-finance", 7, TipoMenuEnum.EstoqueRelatorios),
            Criar(13, 12, "Posição de estoque", "/estoque/relatorio-posicao-estoque", "hugeicons:package-out-of-stock", 1, TipoMenuEnum.EstoqueRelatorioPosicao),
            Criar(14, 12, "Relatório de estoque", "/estoque/relatorio-estoque", "tabler:report-search", 2, TipoMenuEnum.EstoqueRelatorioEstoque),
            Criar(15, 12, "Relatório de produção", "/estoque/relatorio-producao", "simple-icons:soundcharts", 3, TipoMenuEnum.EstoqueRelatorioProducao),

            Criar(16, null, "Vendas", null, "carbon:sales-ops", 4, TipoMenuEnum.Vendas),
            Criar(17, 16, "Pedidos", "/pedidos", "material-symbols:order-approve-outline", 1, TipoMenuEnum.VendasPedido),
            Criar(18, 16, "Tabela de preço", "/vendas/tabela-de-preco", "solar:tag-price-bold", 2, TipoMenuEnum.VendasTabelaDePreco),
            Criar(19, 16, "Preço por peso", "/vendas/preco-por-peso", "material-symbols-light:price-change-outline-rounded", 3, TipoMenuEnum.VendasPrecoPorPeso),
            Criar(20, 16, "Preço por tamanho", "/vendas/preco-por-tamanho", "solar:tag-price-outline", 4, TipoMenuEnum.VendasPrecoPorTamanho),
            Criar(21, 16, "Relatório por período", "/vendas/relatorio-por-periodo", "tabler:report-search", 5, TipoMenuEnum.VendasRelatorioPedidoPeriodo),
            Criar(22, 16, "Relatório venda produto", "/vendas/relatorio-venda-produto", "fluent-mdl2:product-variant", 6, TipoMenuEnum.VendasRelatorioProduto),
            Criar(23, 16, "Clientes", "/vendas/cliente", "solar:user-id-linear", 7, TipoMenuEnum.VendasCliente),
            Criar(44, 16, "Representantes", "/cadastros/representantes", "solar:user-speak-linear", 8, TipoMenuEnum.VendasRepresentante),

            Criar(24, null, "Configurações", null, "iwwa:settings", 5, TipoMenuEnum.Configuracoes),
            Criar(25, 24, "Minha empresa", "/configuracao/minha-empresa", "mdi:company", 1, TipoMenuEnum.ConfiguracoesMinhaEmpresa),
            Criar(26, 24, "Pedidos", "/configuracao/pedido", "material-symbols:order-approve-outline", 2, TipoMenuEnum.ConfiguracoesPedido),
            Criar(27, 24, "Mercado Pago", "/configuracao/mercado-pago", "simple-icons:mercadopago", 3, TipoMenuEnum.ConfiguracoesMercadoPago),
            Criar(28, 24, "Frete", "/configuracao/frete", "fluent:vehicle-truck-cube-24-regular", 4, TipoMenuEnum.ConfiguracoesFrete),

            Criar(29, null, "Financeiro", null, "rivet-icons:money", 6, TipoMenuEnum.Financeiro),
            Criar(30, 29, "Contas a receber", "/financeiro/contas-a-receber", "game-icons:receive-money", 1, TipoMenuEnum.FinanceiroContaAReceber),
            Criar(31, 29, "Contas a pagar", "/financeiro/contas-a-pagar", "game-icons:pay-money", 2, TipoMenuEnum.FinanceiroContaAPagar),
            Criar(32, 29, "Bonificados", "/financeiro/bonificados", "mdi:gift-outline", 3, TipoMenuEnum.FinanceiroBonificados),
            Criar(33, 29, "Transações", "/financeiro/transacoes", "grommet-icons:transaction", 4, TipoMenuEnum.FinanceiroTransacao),
            Criar(34, 29, "Mensalidades", "/financeiro/mensalidades", "hugeicons:payment-02", 5, TipoMenuEnum.FinanceiroMensalidade));
    }

    private static Menu Criar(
        long id,
        long? paiId,
        string nome,
        string? caminho,
        string icone,
        int ordem,
        TipoMenuEnum tipoMenu)
    {
        return new Menu(
            id,
            DataHoraCadastro,
            null,
            paiId,
            nome,
            caminho,
            icone,
            ordem,
            TipoModuloEnum.Geral,
            true,
            tipoMenu);
    }
}
