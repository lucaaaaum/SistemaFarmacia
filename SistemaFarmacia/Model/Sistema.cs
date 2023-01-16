namespace SistemaFarmacia.Model
{
    public class Sistema
    {
        public Farmacia Farmacia { get; set; }
        public int ComprimentoCaixaDeTexto { get; set; }
        public char CaracterCaixaDeTexto { get; set; }
        private bool EmExecucao { get; set; }
        
        public Sistema(Farmacia farmacia, int comprimentoCaixaDeTexto, char caracterCaixaDeTexto) {
            Farmacia = farmacia;
            ComprimentoCaixaDeTexto = comprimentoCaixaDeTexto;
            CaracterCaixaDeTexto = caracterCaixaDeTexto;
            EmExecucao = true;
        }

        public Sistema(int comprimentoCaixaDeTexto, char caracterCaixaDeTexto) {
            ComprimentoCaixaDeTexto = comprimentoCaixaDeTexto;
            CaracterCaixaDeTexto = caracterCaixaDeTexto;
            EmExecucao = true;
            Farmacia = CriaFarmacia();
        }

        private Farmacia CriaFarmacia() {
            Menu menu = MenuFactory.CriaMenu(
                "Nome da Farmácia",
                "Como é a sua primeira vez utilizando o sistema, precisamos que você digite o nome da farmácia no campo abaixo.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );

            string nome = menu.FazPergunta();
            return new Farmacia(nome);
        }

        public void IniciarSistema() {
            while (EmExecucao) {
                MenuComOpcoes menu = MenuFactory.CriaMenu(
                    Farmacia.Nome,
                    "O que deseja fazer agora?",
                    ComprimentoCaixaDeTexto,
                    CaracterCaixaDeTexto,
                    new List<string> {"Sair do sistema","Acessar o catálogo", "Acessar o estoque", "Acessar as vendas", "Acessar os clientes"},
                    new List<Action> {EncerrarPrograma, ModuloCatalogo, ModuloEstoque, ModuloVendas, ModuloClientes}
                );
                menu.FazPergunta();
            }
        }

        private void ModuloCatalogo() {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Catálogo",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Listar produtos", "Consultar produto", "Adicionar novo produto"},
                new List<Action> {Retornar, () => ModuloListaProdutos(Farmacia.Catalogo.MontaLista()), ModuloConsultaCatalogo, ModuloAdicionarProdutoCatalogo}
            );
            menu.FazPergunta();
        }

        private void ModuloListaProdutos(List<string> lista) {
            Menu menu = MenuFactory.CriaMenu("Lista Produtos",
            "Abaixo, lista dos produtos.",
            "Pressione Enter para continuar.",
            ComprimentoCaixaDeTexto,
            CaracterCaixaDeTexto);
            menu.FazPergunta(lista);
        }

        private void ModuloConsultaCatalogo() {
            Menu menu = MenuFactory.CriaMenu(
                "Consulta Catálogo",
                "Digite a descrição do produto ou seu código, o sistema retornará as informações dele e permitirá algumas alterações.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );

            string resposta = menu.FazPergunta();
            Produto produto;
            if (int.TryParse(resposta, out int codigo))
                produto = Farmacia.Catalogo.ConsultaProduto(codigo);
            else
                produto = Farmacia.Catalogo.ConsultaProduto(resposta);
            
            if (produto != null)
                ModuloAlterarProdutoCatalogo(produto);
            else {
                menu.Texto = "Produto não encontrado.";
                menu.TermoResposta = "Pressione Enter para retornar.";
                menu.FazPergunta();
            }
        }

        private void ModuloAlterarProdutoCatalogo(Produto produto) {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Alterar Produto Catálogo",
                produto.ToString(),
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", produto.Ativo ? "Desativar produto" : "Reativar produto", "Alterar descrição", "Alterar valor"},
                new List<Action> {Retornar, produto.Ativo ? () => ModuloDesativarProduto(produto) : () => ModuloReativarProduto(produto), () => ModuloAlterarDescricao(produto), () => ModuloAlterarValor(produto)}
            );
            menu.FazPergunta();
            Farmacia.Estoque.ConsultaProduto(produto.Codigo).Produto = produto;
        }

        private void ModuloDesativarProduto(Produto produto) {
            produto.Ativo = false;
        }

        private void ModuloReativarProduto(Produto produto) {
            produto.Ativo = true;
        }

        private void ModuloAlterarDescricao(Produto produto) {
            Menu menu = MenuFactory.CriaMenu(
                "Alterar Descrição",
                "Digite a descrição desejada.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            
            string descricaoAnterior = produto.Descricao;
            produto.Descricao = menu.FazPergunta();
            
            menu.Texto = $"O produto foi alterado de '{descricaoAnterior}' para '{produto.Descricao}'";
        }

        private void ModuloAlterarValor(Produto produto) {
            Menu menu = MenuFactory.CriaMenu(
                "Alterar Valor",
                "Digite o novo valor.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            decimal valorAnterior = produto.Valor;
            produto.Valor = Convert.ToDecimal(menu.FazPergunta('D'));
            menu.Texto = $"O valor foi alterado de {string.Format("R$ {0:#0.00}", valorAnterior)} para {string.Format("R$ {0:#0.00}", produto.Valor)}";
            menu.TermoResposta = "Pressione Enter para continuar.";
            menu.FazPergunta();
        }

        private void ModuloAdicionarProdutoCatalogo() {
            Menu menu = MenuFactory.CriaMenu(
                "Adicionar Produto",
                "Por favor, digite a descrição do produto.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            string descricao = menu.FazPergunta();

            menu.Texto = "Por favor, digite o valor do produto.";
            decimal valor = Convert.ToDecimal(menu.FazPergunta('D'));

            int codigo = Farmacia.Catalogo.InsereProduto(ProdutoFactory.CriaProduto(Farmacia.Catalogo.Produtos.Count, descricao, valor));
            Farmacia.Estoque.InsereProduto(ProdutoFactory.CriaProduto(codigo, descricao, valor, 0));
            menu.Texto = $"Produto {descricao} inserido sob código {codigo}";
            menu.TermoResposta = "Pressione Enter para continuar.";
            menu.FazPergunta();
        }

        private void ModuloEstoque() {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Estoque",
                "Para aumentar ou diminuir a quantidade de itens no estoque, por favor consulte o produto.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Listar produtos", "Consultar produto"},
                new List<Action> {Retornar, () => ModuloListaProdutos(Farmacia.Estoque.MontaLista()), ModuloConsultaEstoque}
            );
            menu.FazPergunta();
        }

        private void ModuloConsultaEstoque() {
            Menu menu = MenuFactory.CriaMenu(
                "Consulta de Estoque",
                "Digite a descrição do produto ou seu código, o sistema retornará as informações dele e permitirá algumas alterações.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            string resposta = menu.FazPergunta();
            ProdutoComQuantidade produtoComQuantidade = Farmacia.Estoque.ConsultaProduto(resposta);
            
            if (produtoComQuantidade != null)
                ModuloAlterarProdutoEstoque(produtoComQuantidade);
            else {
                menu.Texto = "Produto não encontrado.";
                menu.TermoResposta = "Pressione Enter para retornar.";
                menu.FazPergunta();
            }
        }

        private void ModuloAlterarProdutoEstoque(ProdutoComQuantidade produtoComQuantidade) {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Alterar Produto Estoque",
                produtoComQuantidade.ToString(),
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Aumentar quantidade", "Diminuir quantidade"},
                new List<Action> {Retornar, () => ModuloAumentarQuantidade(produtoComQuantidade), () => ModuloDiminuirQuantidade(produtoComQuantidade)}
            );
            menu.FazPergunta();
        }

        private void ModuloAumentarQuantidade(ProdutoComQuantidade produtoComQuantidade) {
            Menu menu = MenuFactory.CriaMenu(
                "Aumentar Quantidade",
                "Digite a quantidade a ser aumentada.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            
            int quantidade = menu.FazPergunta(1, int.MaxValue);
            int quantidadeAnterior = produtoComQuantidade.Quantidade;
            produtoComQuantidade.Quantidade = quantidade;
            
            menu.Texto = $"Quantidade de {produtoComQuantidade.Produto.Descricao} foi alterada de {quantidadeAnterior} para {produtoComQuantidade.Quantidade}.";
            menu.TermoResposta = "Pressione Enter para continuar.";
            menu.FazPergunta();
        }

        private void ModuloDiminuirQuantidade(ProdutoComQuantidade produtoComQuantidade) {
            Menu menu = MenuFactory.CriaMenu(
                "Diminuir Quantidade",
                "Digite a quantidade a ser diminuída.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            
            int quantidade = menu.FazPergunta(1, produtoComQuantidade.Quantidade);
            int quantidadeAnterior = produtoComQuantidade.Quantidade;
            produtoComQuantidade.Quantidade -= quantidade;
            
            menu.Texto = $"Quantidade de {produtoComQuantidade.Produto.Descricao} foi alterada de {quantidadeAnterior} para {produtoComQuantidade.Quantidade}.";
            menu.TermoResposta = "Pressione Enter para continuar.";
            menu.FazPergunta();
        }

        private void ModuloVendas() {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Vendas",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Listar vendas", "Consultar venda", "Cadastrar venda"},
                new List<Action> {Retornar, ModuloListaVendas, ModuloConsultaVenda, ModuloCadastraVenda}
            );
            menu.FazPergunta();
        }

        private void ModuloListaVendas() {
            Menu menu = MenuFactory.CriaMenu(
                "Lista Vendas",
                "Abaixo, lista de vendas realizadas.",
                "Pressione Enter para continuar.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            menu.FazPergunta(Farmacia.MontaListaVendas());
        }

        private void ModuloConsultaVenda() {
            Menu menu = MenuFactory.CriaMenu(
                "Consulta Venda",
                "Por favor, digite o código da venda.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            int codigo = Convert.ToInt32(menu.FazPergunta('I'));
            Venda venda = Farmacia.ConsultaVenda(codigo);
            if (venda == null) {
                menu.Texto = "Venda não encontrada.";
                menu.TermoResposta = "Pressione Enter pra continuar.";
                menu.FazPergunta();
                return;
            }
            else
                ModuloAlterarVenda(venda);
        }

        private void ModuloAlterarVenda(Venda venda) {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Altera Venda",
                "Abaixo, dados do pedido.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Estornar venda"},
                new List<Action> {Retornar, () => CancelaPedido(venda)}
            );
            menu.FazPergunta(venda.MontaListaVenda());
        }

        private void ModuloCadastraVenda() {
            Menu menuConsultaCliente = MenuFactory.CriaMenu(
                "Cadastro de Venda",
                "Por favor, digite o Código / Nome / CPF do cliente.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );

            if (Farmacia.Clientes.Count == 0) {
                menuConsultaCliente.Texto = "Não há clientes cadastrados para fazer uma venda.";
                menuConsultaCliente.TermoResposta = "Pressione Enter para continuar.";
                menuConsultaCliente.FazPergunta();
                return;
            }
            
            Cliente cliente = null;
            while (cliente == null)
                cliente = Farmacia.ConsultaCliente(menuConsultaCliente.FazPergunta());
 
            Venda venda = new Venda(Farmacia.Vendas.Count, DateTime.Now, new List<ProdutoComQuantidade>(), cliente);

            MenuComOpcoes menuVenda = MenuFactory.CriaMenu(
                "Produtos da Venda",
                $"Cliente: {venda.Cliente.Nome} Total: {string.Format("R$ {0:#0.00}", venda.Total)}",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Cancelar operação", "Listar estoque", "Consultar estoque", "Inserir produto", "Listar pedido", "Encerrar pedido"},
                new List<Action> {() => CancelaPedido(venda), () => ModuloListaProdutos(Farmacia.Estoque.MontaLista()), ModuloConsultaEstoque, () => ModuloAdicionarProdutoVenda(venda), () => ModuloImprimeVenda(venda), () => ModuloEncerraPedido(venda)}
            );

            while (venda.PedidoAberto) {
                menuVenda.Menu.Texto = $"Cliente: {venda.Cliente.Nome} Total: {string.Format("R$ {0:#0.00}", venda.Total)}";
                menuVenda.FazPergunta();
            }
        }

        private void CancelaPedido(Venda venda) {
            foreach(ProdutoComQuantidade produtoVenda in venda.Produtos)
                foreach(ProdutoComQuantidade produtoEstoque in Farmacia.Estoque.ProdutosComQuantidade)
                    if (produtoVenda.Produto.Codigo == produtoEstoque.Produto.Codigo)
                        produtoEstoque.Quantidade += produtoVenda.Quantidade;
            venda.PedidoCancelado = true;                        
            venda.PedidoAberto = false;
        }

        private void ModuloAdicionarProdutoVenda(Venda venda) {
            Menu menu = MenuFactory.CriaMenu(
                "Adicionar Produto",
                "Digite a descrição do produto ou seu código.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );

            ProdutoComQuantidade produtoComQuantidade = Farmacia.Estoque.ConsultaProduto(menu.FazPergunta());
            if (produtoComQuantidade == null) {
                menu.Texto = "Produto não encontrado.";
                menu.TermoResposta = "Pressione Enter para continuar.";
                menu.FazPergunta();
                return;
            }

            if (produtoComQuantidade.Quantidade <= 0) {
                menu.Texto = "Produto não tem estoque.";
                menu.TermoResposta = "Pressione Enter para continuar.";
                menu.FazPergunta();
                return;
            }

            menu.Texto = "Digite a quantidade desejada.";
            int quantidade = menu.FazPergunta(1, produtoComQuantidade.Quantidade+1);
            produtoComQuantidade.Quantidade -= quantidade;

            menu.Texto = $"Foram vendidas {quantidade} unidades do produto {produtoComQuantidade.Produto.Descricao}.";
            menu.TermoResposta = "Pressione Enter para continuar.";
            menu.FazPergunta();

            venda.Produtos.Add(ProdutoFactory.CriaProduto(produtoComQuantidade.Produto, quantidade));
        }

        private void ModuloImprimeVenda(Venda venda) {
            Menu menu = MenuFactory.CriaMenu(
                "Venda",
                "Abaixo, lista de vendas.",
                "Pressione Enter para continuar.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            menu.FazPergunta(venda.MontaListaVenda());
        }

        private void ModuloEncerraPedido(Venda venda) {
            Menu menu = MenuFactory.CriaMenu(
                "Encerramento do Pedido",
                "Abaixo, dados do pedido.",
                "Pressione Enter para continuar.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );

            if (venda.Produtos.Count == 0) {
                menu.Texto = "A venda não pode ser concluída sem nenhum produto.";
                menu.FazPergunta();
                return;
            }

            venda.PedidoAberto = false;
            Farmacia.Vendas.Add(venda);
            menu.FazPergunta(venda.MontaListaVenda());
        }

        private void ModuloClientes() {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Clientes",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Listar clientes", "Consultar cliente", "Adicionar cliente"},
                new List<Action> {Retornar, ModuloListaClientes, ModuloConsultaCliente, ModuloAdicionarCliente}
            );
            menu.FazPergunta();
        }

        private void ModuloListaClientes() {
            Menu menu = MenuFactory.CriaMenu("Lista Clientes",
            "Abaixo, lista dos clientes.",
            "Pressione Enter para continuar.",
            ComprimentoCaixaDeTexto,
            CaracterCaixaDeTexto);
            menu.FazPergunta(Farmacia.MontaListaClientes());
        }

        private void ModuloAdicionarCliente() {
            Menu menu = MenuFactory.CriaMenu(
                "Adicionar Cliente",
                "Qual o nome do cliente?",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            string nome = menu.FazPergunta();
            menu.Texto = "Qual o CPF do cliente?";
            string CPF = menu.FazPergunta(11);
            Farmacia.Clientes.Add(new Cliente(Farmacia.Clientes.Count, nome, CPF));

        }

        private void ModuloConsultaCliente() {
            Menu menu = MenuFactory.CriaMenu(
                "Consulta Cliente",
                "Digite o nome do cliente, seu CPF ou seu código. Sistema retornará suas informações e permitirá algumas alterações.",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            Cliente cliente = Farmacia.ConsultaCliente(menu.FazPergunta());
            if (cliente != null)
                ModuloAlterarCliente(cliente);
            else {
                menu.Texto = "Cliente não encontrado.";
                menu.TermoResposta = "Pressione Enter para retornar.";
                menu.FazPergunta();
            }

        }

        private void ModuloAlterarCliente(Cliente cliente) {
            MenuComOpcoes menu = MenuFactory.CriaMenu(
                "Alterar Cliente",
                cliente.ToString(),
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto,
                new List<string> {"Retornar", "Alterar nome", "Alterar CPF"},
                new List<Action> {Retornar, () => ModuloAlterarNomeCliente(cliente), () => ModuloAlterarCPFCliente(cliente)}
            );
            menu.FazPergunta();
        }

        private void ModuloAlterarNomeCliente(Cliente cliente) {
            Menu menu = MenuFactory.CriaMenu(
                "Alterar Nome",
                "Qual o novo nome?",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            string nomeAnterior = cliente.Nome;
            cliente.Nome = menu.FazPergunta();
            menu.Texto = $"Nome alterado de {nomeAnterior} para {cliente.Nome}";
            menu.TermoResposta = "Pressione Enter para continuar";
            menu.FazPergunta();
        }

        private void ModuloAlterarCPFCliente(Cliente cliente) {
            Menu menu = MenuFactory.CriaMenu(
                "Alterar Nome",
                "Qual o novo CPF?",
                ComprimentoCaixaDeTexto,
                CaracterCaixaDeTexto
            );
            string CPFAnterior = cliente.CPF;
            cliente.Nome = menu.FazPergunta(11);
            menu.Texto = $"CPF alterado de {CPFAnterior} para {cliente.CPF}";
            menu.TermoResposta = "Pressione Enter para continuar";
            menu.FazPergunta();
        }

        private void Retornar() { }
   
        private void EncerrarPrograma() {
            EmExecucao = false;
        }
    }
}