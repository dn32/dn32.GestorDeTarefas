using System;
using System.Linq;

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

            var relatorioCompleto = ativador.Iniciar();

            var status = relatorioCompleto.Select(x => $" {x.Inicio.ToString("HH:mm:ss fff")} - {x.Fim?.ToString("HH:mm:ss fff")?? "**:**:** ***"} : {x.Descricao}").ToArray();
            Console.WriteLine(string.Join("\n", status));
        }
    }
}
