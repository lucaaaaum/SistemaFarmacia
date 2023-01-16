namespace SistemaFarmacia.Model
{
    public class Menu
    {
        public string Nome { get; set; }
        public string Texto { get; set; }
        public string TermoResposta { get; set; }
        public int ComprimentoCaixaDeTexto { get; set; }
        public char CaracterCaixaDeTexto { get; set; }

        public Menu(string nome, string texto, string termoResposta, int comprimentoCaixaDeTexto, char caracterCaixaDeTexto) {
            Nome = nome;
            Texto = texto;
            TermoResposta = termoResposta;
            ComprimentoCaixaDeTexto = comprimentoCaixaDeTexto;
            CaracterCaixaDeTexto = caracterCaixaDeTexto;
        }

        public void ImprimeMenu() {
            Console.Clear();
            ImprimeCabecalho();
            ImprimeCorpo();
            ImprimeRodape();
        }

        public void ImprimeMenu(List<string> linhas) {
            Console.Clear();
            ImprimeCabecalho();
            ImprimeCorpo(linhas);
            ImprimeRodape();
        }

        public void ImprimeMenu(List<List<string>> conjuntos) {
            Console.Clear();
            ImprimeCabecalho();
            ImprimeCorpo(conjuntos);
            ImprimeRodape();
        }

        private void ImprimeCabecalho() {
            int comprimentoInicial = (ComprimentoCaixaDeTexto - Nome.Length)/2;
            int comprimentoFinal = ComprimentoCaixaDeTexto - comprimentoInicial - (Nome.Length + 2);

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < comprimentoInicial; i++)
                Console.Write(CaracterCaixaDeTexto);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {Nome.ToUpper()} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < comprimentoFinal; i++)
                Console.Write(CaracterCaixaDeTexto);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        private void ImprimeCorpo() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{CaracterCaixaDeTexto} ");
            Console.ForegroundColor = ConsoleColor.White;
            
            int caracteresNaLinha = 0;
            for (int i = 0; i < Texto.Length; i++) {
                Console.Write(Texto[i]);
                caracteresNaLinha++;
                if (caracteresNaLinha == ComprimentoCaixaDeTexto - 4) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($" {CaracterCaixaDeTexto}");
                    Console.Write($"{CaracterCaixaDeTexto} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    caracteresNaLinha = 0;
                }
            }
            
            for (int i = 0; i < (ComprimentoCaixaDeTexto - caracteresNaLinha - 4); i++)
                Console.Write(' ');

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {CaracterCaixaDeTexto}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ImprimeCorpo(List<string> linhas) {
            ImprimeCorpo();
            ImprimeRodape();

            foreach (string linha in linhas) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{CaracterCaixaDeTexto} ");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.Write(linha);
                for (int i = 0; i < (ComprimentoCaixaDeTexto - linha.Length - 4); i++)
                    Console.Write(' ');

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" {CaracterCaixaDeTexto}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ImprimeCorpo(List<List<string>> conjuntos) {
            foreach(List<string> linhas in conjuntos)
                ImprimeCorpo(linhas);
        }

        private void ImprimeRodape() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < ComprimentoCaixaDeTexto; i++)
                Console.Write(CaracterCaixaDeTexto);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public string FazPergunta() {
            ImprimeMenu();
            return PerguntaFactory.FazPergunta(TermoResposta);
        }

        public object FazPergunta(char tipo) {
            ImprimeMenu();
            return PerguntaFactory.FazPergunta(TermoResposta, tipo);
        }

        public string FazPergunta(List<string> linhas) {
            ImprimeMenu(linhas);
            return PerguntaFactory.FazPergunta(TermoResposta);
        }

        public string FazPergunta(List<List<string>> conjuntos) {
            ImprimeMenu(conjuntos);
            return PerguntaFactory.FazPergunta(TermoResposta);
        }

        public object FazPergunta(List<string> linhas, char tipo) {
            ImprimeMenu(linhas);
            return PerguntaFactory.FazPergunta(TermoResposta, tipo);
        }
    
        public int FazPergunta(List<string> linhas, int inicial, int final) {
            ImprimeMenu(linhas);
            return PerguntaFactory.FazPergunta(TermoResposta, inicial, final);
        }

        public int FazPergunta(List<List<string>> conjuntos, int inicial, int final) {
            foreach(List<string> linhas in conjuntos)
                ImprimeMenu(linhas);
            return PerguntaFactory.FazPergunta(TermoResposta, inicial, final);
        }
        
        public int FazPergunta(int inicial, int final) {
            ImprimeMenu();
            return PerguntaFactory.FazPergunta(TermoResposta, inicial, final);
        }

        public string FazPergunta(int quantidadeCaracteres) {
            ImprimeMenu();
            return PerguntaFactory.FazPergunta(TermoResposta, quantidadeCaracteres);
        }
    }
}