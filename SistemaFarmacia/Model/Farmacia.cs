using System.Collections.Generic;

namespace SistemaFarmacia.Model
{
    public class Farmacia
    {
        public string Nome { get; set; }
        public Catalogo Catalogo { get; set; }
        public Estoque Estoque { get; set; }
        public List<Venda> Vendas { get; set; }
        public List<Cliente> Clientes { get; set; }

        public Farmacia(string nome, Catalogo catalogo, Estoque estoque, List<Venda> vendas, List<Cliente> clientes) {
            Nome = nome;
            Catalogo = catalogo;
            Estoque = estoque;
            Vendas = vendas;
            Clientes = clientes;
        }

        public Farmacia(string nome) {
            Nome = nome;
            Catalogo = new Catalogo();
            Estoque = new Estoque();
            Vendas = new List<Venda>();
            Clientes = new List<Cliente>();
        }

        public void Vender(Cliente cliente, List<ProdutoComQuantidade> produtosVendidos) {
            foreach (ProdutoComQuantidade p in produtosVendidos) {
                Estoque.RetiraProduto(p);
            }
        }

        public Cliente ConsultaCliente(string resposta) {
            resposta = StringFormatadaFactory.CriaStringFormatada(resposta);
            foreach (Cliente c in Clientes)
                if (StringFormatadaFactory.CriaStringFormatada(c.Nome) == resposta || StringFormatadaFactory.CriaStringFormatada(c.CPF) == resposta)
                    return c;
                else if (int.TryParse(resposta, out int respostaInt) && c.Codigo == respostaInt)
                    return c;
            return null;
        }

        public Venda ConsultaVenda(int codigo) {
            foreach (Venda v in Vendas)
                if (v.Codigo == codigo)
                    return v;
            return null;
        }

        public List<List<string>> MontaListaVendas() {
            List<List<string>> conjuntos = new List<List<string>>();
            List<string> lista = new List<string>();

            if (Vendas.Count == 0) {
                lista.Add("Não há vendas cadastradas.");
                conjuntos.Add(lista);
            }
            else
                foreach (Venda v in Vendas)
                    if (!v.PedidoCancelado)
                        conjuntos.Add(v.MontaListaVenda());

            return conjuntos;
        }

        public List<string> MontaListaClientes() {
            List<string> lista = new List<string>();
            foreach (Cliente c in Clientes)
                lista.Add(c.ToString());
            return lista;
        }
    }
}