using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace dn32.GestorDeTarefas
{
    public abstract class Dn32Operador : IDisposable
    {
        internal bool DisposeExecutado { get; set; }

#if DEBUG
        public bool MostrarLogsNoConsole { get; set; } = true;

#else
        public bool MostrarLogsNoConsole { get; set; };
#endif

        private ConcurrentDictionary<Guid, Dn32Tarefa> TaferasEmExecucao { get; set; } = new ConcurrentDictionary<Guid, Dn32Tarefa>();

        public ConcurrentBag<RelatorioDeExecucao> Relatorio { get; set; } = new ConcurrentBag<RelatorioDeExecucao>();

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
        }

        public void AguardarAConclusaoDeTodasAsTarefas()
        {
            while (!DisposeExecutado && TaferasEmExecucao.Count > 0) Task.Delay(1);
        }

        protected void Executar(Func<object, Task> acao, string descricao, TimeSpan? timeout = null, object obj = null)
        {

        }

        protected void Executar(Action<object> acao, string descricao, TimeSpan? timeout = null, object obj = null)
        {
            Func<object, Task> acaoFunc = (a) => Task.Run(() => acao(a));

            if (DisposeExecutado) return;
            AguardarAConclusaoDeTodasAsTarefas();
            var tarefa = AdicionarTarefa(acaoFunc, descricao, timeout);
            tarefa.Executar(obj);
        }

        protected void ExecutarAsync(Action<object> acao, string descricao, TimeSpan? timeout = null, object obj = null)
        {
            Func<object, Task> acaoFunc = (a) => Task.Run(() => acao(a));
          
            if (DisposeExecutado) return;
            var tarefa = AdicionarTarefa(acaoFunc, descricao, timeout);
            tarefa.ExecutarAsync(obj);
        }

        private Dn32Tarefa AdicionarTarefa(Func<object, Task> acao, string descricao, TimeSpan? timeout)
        {
            var relatorioDaTarefa = new RelatorioDeExecucao { Descricao = descricao };
            Relatorio.Add(relatorioDaTarefa);

            var tarefa = new Dn32Tarefa(acao, descricao, timeout)
            {
                TarefaIniciadaCallBack = (Dn32Tarefa tarefaLocal) =>
                {
                    relatorioDaTarefa.Inicio = DateTime.Now;
                    Log($"Tarefa iniciada: {descricao}");
                },

                TarefaFinalizadaCallBack = (Dn32Tarefa tarefaLocal) =>
                {
                    relatorioDaTarefa.Fim = DateTime.Now;
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
            Relatorio?.Clear();
        }
    }
}
