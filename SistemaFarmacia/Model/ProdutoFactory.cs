namespace SistemaFarmacia.Model
{
    public class ProdutoFactory
    {
        public static Produto CriaProduto(int codigo, string descricao, decimal valor) {
            return new Produto(codigo, descricao, valor);
        }

        public static ProdutoComQuantidade CriaProduto(int codigo, string descricao, decimal valor, int quantidade) {
            return new ProdutoComQuantidade(CriaProduto(codigo, descricao, valor), quantidade);
        }

        public static ProdutoComQuantidade CriaProduto(Produto produto, int quantidade) {
            return new ProdutoComQuantidade(produto, quantidade);
        }
    }
}