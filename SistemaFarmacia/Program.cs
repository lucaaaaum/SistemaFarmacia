using SistemaFarmacia.Model;

namespace SistemaFarmacia
{
    public class Program
    {
        public static void Main() {
            Sistema sistema = new Sistema(50, '#');
            sistema.IniciarSistema();
        }
    }
}