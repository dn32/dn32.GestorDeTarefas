//using Microsoft.Extensions.Caching.Memory;
//using System;
//using System.Collections.Concurrent;
//using System.Threading.Tasks;

namespace dn32.GestorDeTarefas
{
//namespace Dn32GestorDeTarefas
//{
//    internal class DnGestorDeTarefas
//    {
//        //private MemoryCache Memoria { get; set; }

//        public ConcurrentDictionary<int, Tarefa> TaferasEmExecucao { get; set; }

//        public bool DisposeExecutado { get; set; }

//        public DnGestorDeTarefas()
//        {
//            //Memoria = new MemoryCache(new MemoryCacheOptions());
//            TaferasEmExecucao = new ConcurrentDictionary<int, Tarefa>();
//        }

//        //public void Adicionar(int timeout, Tarefa tarefa)
//        //{
//        //    if (DisposeExecutado) return;

//        //    var key = tarefa.GetHashCode();
//        //    var excluir5SegundosAposTimeout = TimeSpan.FromSeconds(timeout);
//        //    //var entidade = Memoria.CreateEntry(key);
//        //    entidade.AbsoluteExpirationRelativeToNow = excluir5SegundosAposTimeout;
//        //    entidade.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration { EvictionCallback = Remover });

//        //    entidade.SetValue(tarefa);
//        //    TaferasEmExecucao.TryAdd(key, tarefa);
//        //}

//        internal void RemoverTarefa(Tarefa tarefa)
//        {
//            TaferasEmExecucao.TryRemove(tarefa.GetHashCode(), out _);
//        }

//        internal void AdicionarTarefa(Tarefa tarefa)
//        {
//            TaferasEmExecucao.TryAdd(tarefa.GetHashCode(), tarefa);
//        }

//        private void Remover(object key, object value, EvictionReason reason, object state)
//        {
//            if (DisposeExecutado) return;

//            TaferasEmExecucao.TryRemove((int)key, out _);
//        }

//        public void AguardarAConclusaoDeTodasAsTarefas()
//        {
//            while (!DisposeExecutado && TaferasEmExecucao.Count > 0) Task.Delay(1);
//        }

//        internal void Dispose()
//        {
//            if (DisposeExecutado) return;

//            DisposeExecutado = true;
//            TaferasEmExecucao.Clear();
//            Memoria.Dispose();
//        }
//    }
//}
}