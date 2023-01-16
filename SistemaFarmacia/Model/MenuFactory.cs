namespace SistemaFarmacia.Model
{
    public class MenuFactory
    {
        public static Menu CriaMenu(string nome, string texto, string termoResposta, int comprimentoCaixaDeTexto, char caracterContorno) {
            return new Menu(nome, texto, termoResposta, comprimentoCaixaDeTexto, caracterContorno);
        }

        public static Menu CriaMenu(string nome, string texto, int comprimentoCaixaDeTexto, char caracterContorno) {
            return new Menu(nome, texto, "Resposta:", comprimentoCaixaDeTexto, caracterContorno);
        }

        public static Menu CriaMenu(string nome, int comprimentoCaixaDeTexto, char caracterContorno) {
            return new Menu(nome, "O que deseja fazer agora?", "Resposta:", comprimentoCaixaDeTexto, caracterContorno);
        }

        public static MenuComOpcoes CriaMenu(string nome, string texto, string termoResposta, int comprimentoCaixaDeTexto, char caracterContorno, List<string> opcoes, List<Action> acoes) {
            return new MenuComOpcoes(CriaMenu(nome, texto, termoResposta, comprimentoCaixaDeTexto, caracterContorno), opcoes, acoes);
        }

        public static MenuComOpcoes CriaMenu(string nome, string texto, int comprimentoCaixaDeTexto, char caracterContorno, List<string> opcoes, List<Action> acoes) {
            return new MenuComOpcoes(CriaMenu(nome, texto, comprimentoCaixaDeTexto, caracterContorno), opcoes, acoes);
        }

        public static MenuComOpcoes CriaMenu(string nome, int comprimentoCaixaDeTexto, char caracterContorno, List<string> opcoes, List<Action> acoes) {
            return new MenuComOpcoes(CriaMenu(nome, comprimentoCaixaDeTexto, caracterContorno), opcoes, acoes);
        }
    }
}