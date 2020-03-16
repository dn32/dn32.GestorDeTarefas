using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Dn32GestorDeTarefas
{
    public abstract class Dn32Executor : IDisposable
    {
        internal bool DisposeExecutado { get; set; }

#if DEBUG
        public bool MostrarLogsNoConsole { get; set; } = true;

#else
        public bool MostrarLogsNoConsole { get; set; };
#endif


        private ConcurrentDictionary<Guid, Dn32Tarefa> TaferasEmExecucao { get; set; } = new ConcurrentDictionary<Guid, Dn32Tarefa>();

        protected virtual void TimeOut(TimeoutException timeoutEx)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(timeoutEx.Message);
            Console.ResetColor();
        }

        protected virtual void Log(string mensagem) { if (MostrarLogsNoConsole) Console.WriteLine(mensagem); }

        protected internal abstract void ExecutarProcessos();

        internal void TimeOutInterno(TimeoutException timeoutException)
        {
            TimeOut(timeoutException);
            Dispose();
        }

        public void AguardarAConclusaoDeTodasAsTarefas()
        {
            while (!DisposeExecutado && TaferasEmExecucao.Count > 0) Task.Delay(1);
        }

        protected void Executar(Action acao, string descricao, TimeSpan? timeout = null)
        {
            if (DisposeExecutado) return;
            AguardarAConclusaoDeTodasAsTarefas();
            var tarefa = AdicionarTarefa(acao, descricao, timeout);
            tarefa.Executar();
        }

        protected void ExecutarAsync(Action acao, string descricao, TimeSpan? timeout = null)
        {
            if (DisposeExecutado) return;
            var tarefa = AdicionarTarefa(acao, descricao, timeout);
            tarefa.ExecutarAsync();
        }

        private Dn32Tarefa AdicionarTarefa(Action acao, string descricao, TimeSpan? timeout)
        {
            var tarefa = new Dn32Tarefa(acao, descricao, timeout)
            {
                TarefaIniciadaCallBack = (Dn32Tarefa tarefaLocal) =>
                {
                    Log($"Tarefa iniciada: {descricao}");
                },

                TarefaFinalizadaCallBack = (Dn32Tarefa tarefaLocal) =>
                {
                    TaferasEmExecucao.TryRemove(tarefaLocal.Id, out _);
                    Log($"Tarefa finalizada: {descricao}");
                }
            };

            TaferasEmExecucao.TryAdd(tarefa.Id, tarefa);
            return tarefa;
        }

        public void Dispose()
        {
            if (DisposeExecutado) return;
            DisposeExecutado = true;

            TaferasEmExecucao?.Clear();
        }
    }
}
