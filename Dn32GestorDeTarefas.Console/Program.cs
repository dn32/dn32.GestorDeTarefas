using System;

namespace dn32.GestorDeTarefas.ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var ativador = new MinhasOperacoes
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
