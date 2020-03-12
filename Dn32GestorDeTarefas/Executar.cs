using System;

namespace dn32.GestorDeTarefas
{
    public static class Executar
    {
        public static void Iniciar(this Dn32Executor executor)
        {
            if (executor.DisposeExecutado) return;

            try
            {
                executor.ExecutarProcessos();
                executor.AguardarAConclusaoDeTodasAsTarefas();
            }
            catch (TimeoutException timeoutException)
            {
                executor.Dispose();
                executor.TimeOutInterno(timeoutException);
            }
        }
    }
}
