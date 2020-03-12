using System;

namespace dn32.GestorDeTarefas
{
    public static class Dn32OperadorExtensao
    {
        public static void Iniciar(this Dn32Operador executor)
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
