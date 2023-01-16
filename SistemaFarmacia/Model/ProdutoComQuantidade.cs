namespace SistemaFarmacia.Model
{
    public class ProdutoComQuantidade
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ProdutoComQuantidade(Produto produto, int quantidade) {
            Produto = produto;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"{Quantidade}x   {Produto.ToString()}";
        }
    }
}