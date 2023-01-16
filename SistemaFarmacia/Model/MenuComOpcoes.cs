namespace SistemaFarmacia.Model
{
    public class MenuComOpcoes
    {
        public Menu Menu { get; set; }
        public List<string> Opcoes { get; set; }
        public List<Action> Acoes { get; set; }

        public MenuComOpcoes(Menu menu, List<string> opcoes, List<Action> acoes) {
            Menu = menu;
            Opcoes = opcoes;
            Acoes = acoes;
        }

        public void ImprimeMenu() {
            Menu.ImprimeMenu(montaOpcoes());
        }

        public void FazPergunta() {
            List<string> opcoesLista = montaOpcoes();
            int resposta = Menu.FazPergunta(opcoesLista, 0, opcoesLista.Count);
            Acoes[resposta]();
        }

        public void FazPergunta(List<string> listaExtra) {
            List<string> opcoesLista = montaOpcoes();
            int resposta = Menu.FazPergunta(new List<List<string>> {listaExtra, opcoesLista}, 0, opcoesLista.Count);
            Acoes[resposta]();
        }

        private List<string> montaOpcoes() {
            List<string> opcoesMontadas = new List<string>();
            for (int i = 0; i < Opcoes.Count; i++){
                opcoesMontadas.Add($" {i} - {Opcoes[i]}");
            }
            return opcoesMontadas;
        }
    }
}