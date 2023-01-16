using System.Collections.Generic;

namespace SistemaFarmacia.Model
{
    public class Estoque
    {
        public List<ProdutoComQuantidade> ProdutosComQuantidade { get; set; }

        public Estoque(List<ProdutoComQuantidade> produtos) {
            ProdutosComQuantidade = produtos;
        }

        public Estoque() {
            ProdutosComQuantidade = new List<ProdutoComQuantidade>();
        }

        public void InsereProduto(ProdutoComQuantidade produtoComQuantidade) {
            foreach(ProdutoComQuantidade p in ProdutosComQuantidade) {
                if (p.Produto.Codigo == produtoComQuantidade.Produto.Codigo) {
                    p.Quantidade += produtoComQuantidade.Quantidade;
                    return;
                }
            }
            ProdutosComQuantidade.Add(produtoComQuantidade);
        }

        public bool RetiraProduto(ProdutoComQuantidade produtoRetirado) {
            foreach(ProdutoComQuantidade p in ProdutosComQuantidade) {
                if (p.Produto.Codigo == produtoRetirado.Produto.Codigo) {
                    if (p.Quantidade == 0) {
                        Console.WriteLine($"O produto {p.Produto.Descricao} está fora de estoque.");
                        return false;
                    }
                    else if (p.Quantidade < produtoRetirado.Quantidade) {
                        Console.WriteLine("Quantidade de estoque inferior à da venda.");
                        Console.WriteLine("Deseja vender o restante do estoque?");
                        Console.WriteLine($"Quantia em estoque: {p.Quantidade} / Quantia vendida: {produtoRetirado.Quantidade}");
                    }
                    else
                        return true;
                }
            }
            Console.WriteLine($"O produto {produtoRetirado.Produto.Descricao} não está cadastrado no estoque.");
            return false;
        }

        public ProdutoComQuantidade ConsultaProduto(string resposta) {
            resposta = StringFormatadaFactory.CriaStringFormatada(resposta);
            foreach(ProdutoComQuantidade p in ProdutosComQuantidade) {
                if (StringFormatadaFactory.CriaStringFormatada(p.Produto.Descricao).Equals(resposta))
                    return p;
            }
            
            if (int.TryParse(resposta, out int codigo))
                foreach(ProdutoComQuantidade p in ProdutosComQuantidade) {
                    if (p.Produto.Codigo == codigo)
                        return p;
                }
            
            return null;
        }

        public ProdutoComQuantidade ConsultaProduto(int codigo) {
            foreach(ProdutoComQuantidade p in ProdutosComQuantidade) {
                if (p.Produto.Codigo == codigo)
                    return p;
            }
            return null;
        }

        public List<string> MontaLista() {
            List<string> lista = new List<string>();
            
            if (ProdutosComQuantidade.Count == 0)
                lista.Add("Não há produtos no catálogo.");
            else
                foreach(ProdutoComQuantidade p in ProdutosComQuantidade)
                    if (p.Produto.Ativo || p.Quantidade > 0)
                        lista.Add(p.ToString());
            
            return lista;
        }
    }
}