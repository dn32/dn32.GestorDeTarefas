using System;

namespace dn32.GestorDeTarefas
{
    public class RelatorioDeExecucao
    {
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public string Descricao { get; set; }
        public bool Concluido => Fim != null;
    }
}
