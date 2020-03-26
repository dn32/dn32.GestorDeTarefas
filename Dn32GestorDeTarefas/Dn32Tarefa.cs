using System;
using System.Threading.Tasks;

namespace dn32.GestorDeTarefas
{
    internal class Dn32Tarefa
    {
        public string Descricao { get; set; }

        public Guid Id { get; set; }

        public Func<object, Task> Acao { get; set; }

        public TimeSpan? TimeOut { get; set; }

        public bool TimeOutDisparado { get; set; }

        public Action<Dn32Tarefa> TarefaIniciadaCallBack { get; set; }

        public Action<Dn32Tarefa> TarefaFinalizadaCallBack { get; set; }

        public Action<Dn32Tarefa, Exception> TarefaErroCallBack { get; set; }

        public void ExecutarAsync(object obj) => ExecutarInterno(false, obj);

        public void Executar(object obj) => ExecutarInterno(true, obj);

        public Dn32Tarefa(Func<object, Task> acao, string descricao, TimeSpan? timeout)
        {
            Id = Guid.NewGuid();
            Acao = acao;
            TimeOut = timeout;
            Descricao = descricao;
        }

        private void ExecutarInterno(bool ehAguardar, object obj)
        {
            var aguardar = GerarATask(obj);
            if (ehAguardar)
            {
                aguardar.Wait();
                if (TimeOutDisparado)
                    throw new TimeoutException($"O método '{Descricao}' sofreu timeout por ter demorado mais do que {TimeOut.Value.TotalMilliseconds}ms para responder.");
            }
        }

        private void Ex(Exception ex)
        {
            Console.WriteLine(ex.Message);
            TarefaErroCallBack?.Invoke(this, ex);
        }

        private Task GerarATask(object obj)
        {
            return Task.Run(() =>
            {
                TarefaIniciadaCallBack?.Invoke(this);

                if (TimeOut == null)
                {
                    try
                    {
                        Acao(obj).Wait();
                    }
                    catch (Exception ex)
                    {
                        Ex(ex);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        if (!Task.Run(() => Acao(obj)).Wait(TimeOut.Value))
                        {
                            TimeOutDisparado = true;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Ex(ex);
                        return;
                    }
                }

                TarefaFinalizadaCallBack?.Invoke(this);
            });
        }
    }
}
