using System;
using System.Linq;

namespace dn32.GestorDeTarefas
{
    public static class Dn32OperadorExtensao
    {
        public static RelatorioDeExecucao[] Iniciar(this Dn32Operador executor)
        {
            if (executor.DisposeExecutado) return default;

            try
            {
                executor.ExecutarProcessosAsync();
                executor.AguardarAConclusaoDeTodasAsTarefas();
            }
            catch (TimeoutException timeoutException)
            {
                executor.TimeOutInterno(timeoutException);
            }

            var relatorio = executor.Relatorio.Reverse().ToArray();
            executor.Dispose();
            return relatorio;
        }
    }
}
