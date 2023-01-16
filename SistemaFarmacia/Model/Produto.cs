namespace SistemaFarmacia.Model
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        public Produto(int codigo, string descricao, decimal valor) {
            Codigo = codigo;
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
        }

        public override string ToString()
        {
            return $"({string.Format("{0:000000.}", Codigo)}) {Descricao} - {string.Format("R$ {0:#0.00}", Valor)}" + (Ativo ? "" : " DESATIVADO");
        }
    }
}