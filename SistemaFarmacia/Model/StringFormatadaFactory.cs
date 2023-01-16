namespace SistemaFarmacia.Model
{
    public class StringFormatadaFactory
    {
        public static string CriaStringFormatada(string texto) {
            texto = texto.Trim().ToUpper();
            string textoFormatado = "";

            for (int i = 0; i < texto.Length; i++) {
                if (!texto[i].Equals(' ')) {
                    textoFormatado += texto[i];
                }
            }
            return textoFormatado;
        }
    }
}