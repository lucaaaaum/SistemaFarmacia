using System.Collections.Generic;

namespace SistemaFarmacia.Model
{
    public class Catalogo
    {
        public List<Produto> Produtos { get; set; }

        public Catalogo(List<Produto> produtos) {
            Produtos = produtos;
        }

        public Catalogo() {
            Produtos = new List<Produto>();
        }

        public List<string> MontaLista() {
            List<string> lista = new List<string>();
            
            if (Produtos.Count == 0)
                lista.Add("Não há produtos no catálogo.");
            else
                foreach(Produto p in Produtos)
                    lista.Add(p.ToString());
            
            return lista;
        }

        public int InsereProduto(Produto produto) {
            Produtos.Add(produto);
            return produto.Codigo;
        }

        public void DesativaProduto(Produto produto) {
            foreach (Produto p in Produtos)
                if (p.Codigo == produto.Codigo)
                    p.Ativo = false;
        }

        public Produto ConsultaProduto(string resposta) {
            resposta = StringFormatadaFactory.CriaStringFormatada(resposta);
            foreach(Produto p in Produtos) {
                if (StringFormatadaFactory.CriaStringFormatada(p.Descricao).Equals(resposta))
                    return p;
            }
            
            if (int.TryParse(resposta, out int codigo))
                foreach(Produto p in Produtos) {
                    if (p.Codigo == codigo)
                        return p;
                }
            
            return null;
        }

        public Produto ConsultaProduto(int codigo) {
            foreach(Produto p in Produtos) {
                if (p.Codigo == codigo)
                    return p;
            }
            return null;
        }
    }
}