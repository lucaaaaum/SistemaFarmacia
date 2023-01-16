namespace SistemaFarmacia.Model
{
    public class Venda
    {
        public int Codigo { get; set; }
        public  DateTime DataHora { get; set; }
        public List<ProdutoComQuantidade> Produtos { get; set; }
        public Cliente Cliente { get; set; }
        public bool PedidoAberto { get; set; }
        public bool PedidoCancelado { get; set; }
        public decimal Total {
            get {
                decimal total = 0;
                foreach (ProdutoComQuantidade p in Produtos) {
                    total += p.Produto.Valor * p.Quantidade;
                }
                return total;
            }
        }

        public Venda(int codigo, DateTime dataHora, List<ProdutoComQuantidade> produtos, Cliente cliente) {
            Codigo = codigo;
            DataHora = dataHora;
            Produtos = produtos;
            Cliente = cliente;
            PedidoAberto = true;
            PedidoCancelado = false;
        }

        public override string ToString()
        {
            string retorno = $"{DataHora}\n {Cliente.ToString()}";
            decimal total = 0;
            foreach (ProdutoComQuantidade p in Produtos) {
                total += p.Produto.Valor * p.Quantidade;
                retorno += $"  {p.ToString()} \n";
            }
            
            retorno += $"\n  Total: {total}";
            return retorno;
        }

        public List<string> MontaListaVenda() {
            List<string> retorno = new List<string>();
            retorno.Add($"({string.Format("{0:000000.}",Codigo)}) - {DataHora}");
            retorno.Add($"Cliente: {Cliente.Nome} - {Cliente.StringCPF()}");
            foreach(ProdutoComQuantidade p in Produtos)
                retorno.Add(p.ToString());
            retorno.Add($"Total: {string.Format("R$ {0:#0.00}",Total)}");
            return retorno;
        }
    }
}