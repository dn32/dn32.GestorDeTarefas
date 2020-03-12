using System;
using System.Threading.Tasks;

namespace dn32.GestorDeTarefas
{
    internal class Dn32Tarefa
    {
        public string Descricao { get; set; }

        public Guid Id { get; set; }

        public Action Acao { get; set; }

        public TimeSpan? TimeOut { get; set; }

        public Action<Dn32Tarefa> TarefaIniciadaCallBack { get; set; }

        public Action<Dn32Tarefa> TarefaFinalizadaCallBack { get; set; }

        public void ExecutarAsync() => ExecutarInterno(false);

        public void Executar()
        {
            ExecutarInterno(true);
        }

        public Dn32Tarefa(Action acao, string descricao, TimeSpan? timeout)
        {
            Id = Guid.NewGuid();
            Acao = acao;
            TimeOut = timeout;
            Descricao = descricao;
        }

        private void ExecutarInterno(bool ehAguardar)
        {
            var aguardar = GerarATask();
            if (ehAguardar)
            {
                aguardar.Wait();
                if (TimeOutDisparado)
                {
                    throw new TimeoutException($"O método '{Descricao}' sofreu timeout por ter demorado mais do que {TimeOut.Value.TotalMilliseconds}ms para responder.");
                }
            }
        }

        public bool TimeOutDisparado { get; set; }

        private Task GerarATask()
        {
            return Task.Run(() =>
            {
                TarefaIniciadaCallBack?.Invoke(this);

                if (TimeOut == null)
                {
                    Acao();
                }
                else
                {
                    if (!Task.Run(Acao).Wait(TimeOut.Value))
                    {
                        TimeOutDisparado = true;
                        return;
                    }
                }

                TarefaFinalizadaCallBack?.Invoke(this);
            });
        }
    }
}
