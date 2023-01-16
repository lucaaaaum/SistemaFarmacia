namespace SistemaFarmacia.Model
{
    public class PerguntaFactory
    {
        public static string FazPergunta(string termoResposta) {
            ImprimePergunta(termoResposta);
            return Console.ReadLine();
        }

        public static object FazPergunta (string termoResposta, char tipo) {
            tipo = char.ToUpper(tipo);

            switch (tipo) {
                case 'I':
                    return perguntaInt(termoResposta);
                case 'B':
                    return perguntaBool(termoResposta);
                case 'D':
                    return perguntaDecimal(termoResposta);
                default:
                    return FazPergunta(termoResposta);
            }
        }

        public static int FazPergunta (string termoResposta, int inicial, int final) {
            while (true) {
                ImprimePergunta(termoResposta);
                string resposta = Console.ReadLine();
                if (int.TryParse(resposta, out int respostaInt) && respostaInt >= inicial && respostaInt < final)
                    return respostaInt;
                else
                    Console.WriteLine("Resposta inválida!");
            }
        }

        public static string FazPergunta (string termoResposta, int quantidadeCaracteres) {
            while (true) {
                ImprimePergunta(termoResposta);
                string resposta = Console.ReadLine();
                if (resposta != null && resposta.Length == quantidadeCaracteres)
                    return resposta;
                else
                    Console.WriteLine("Resposta inválida!");
            }
        }

        private static void ImprimePergunta(string termoResposta) {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write(termoResposta);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }

        private static int perguntaInt(string termoResposta) {
            while (true) {
                ImprimePergunta(termoResposta);
                string resposta = Console.ReadLine();
                if (int.TryParse(resposta, out int respostaInt))
                    return respostaInt;
                else
                    Console.WriteLine("Resposta inválida!");
            }
        }

        private static bool perguntaBool(string termoResposta) {
            while (true) {
                ImprimePergunta(termoResposta);
                string resposta = (Console.ReadLine()).ToUpper();
                if (resposta.Equals('S'))
                    return true;
                else if (resposta.Equals('N'))
                    return false;
                else
                    Console.WriteLine("Resposta inválida!");
            }
        }

        private static decimal perguntaDecimal(string termoResposta) {
            while (true) {
                ImprimePergunta(termoResposta);
                string resposta = Console.ReadLine();
                if (decimal.TryParse(resposta, out decimal respostaDecimal))
                    return respostaDecimal;
                else
                    Console.WriteLine("Resposta inválida!");
            }
        }
    }
}