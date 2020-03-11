using System;

namespace Dn32GestorDeTarefas.ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var ativador = new Ativador
            {
                Atuador = new AtuadorDeOperacaoPrincipal
                {
                    Cliente = 32,
                    Token = "ABC"
                }
            };

            ativador.Iniciar();
        }
    }
}
