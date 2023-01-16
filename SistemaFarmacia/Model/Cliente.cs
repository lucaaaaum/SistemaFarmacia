namespace SistemaFarmacia.Model
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public Cliente(int codigo, string nome, string cpf) {
            Codigo = codigo;
            Nome = nome;
            CPF = cpf;
        }

        public override string ToString() {
            return $"({string.Format("{0:000000.}",Codigo)}) {Nome} - {StringCPF()}";
        }

        public string StringCPF() {
            string CPFFormatado = "";
            
            for (int i = 0; i < 3; i++)
                CPFFormatado += CPF[i];
            CPFFormatado += '.';

            for (int i = 3; i < 6; i++)
                CPFFormatado += CPF[i];
            CPFFormatado += '.';
            
            for (int i = 6; i < 9; i++)
                CPFFormatado += CPF[i];
            CPFFormatado += '-';
            
            for (int i = 9; i < CPF.Length; i++)
                CPFFormatado += CPF[i];
            
            return CPFFormatado;
        }
    }
}