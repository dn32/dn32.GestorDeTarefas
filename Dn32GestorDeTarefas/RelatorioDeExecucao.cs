using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dn32.GestorDeTarefas
{
    [NotMapped, Serializable]
    public class RelatorioDeExecucao
    {
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public string Descricao { get; set; }
        public bool Sucesso { get; set; }
        public bool Concluido => Fim != null;
        public string Erro { get; internal set; }
    }
}
