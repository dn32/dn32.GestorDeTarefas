using System;

namespace Dn32GestorDeTarefas.ConsoleTeste
{
    public class Ativador : Dn32Executor
    {
        public AtuadorDeOperacaoPrincipal Atuador { get; set; }

        protected override void ExecutarProcessos()
        {
            Executar(Atuador.AtividadeDeConsultaDeComplemento, "Consultando API de complementos", TimeSpan.FromSeconds(10));
            Executar(Atuador.AtualizarDados, "Atualizando complementos", TimeSpan.FromSeconds(10));
            Executar(Atuador.Stream, "Processando stream");


            ExecutarAsync(Atuador.AlimineOLixo, "Eliminando o lixo", TimeSpan.FromSeconds(10));
            ExecutarAsync(Atuador.NotifiqueAConclusao, "Notificando a exclusão", TimeSpan.FromSeconds(10));


            Executar(Atuador.SalveRegistros, "Salvar registro", TimeSpan.FromSeconds(2));
            Executar(Atuador.OperacaoSeguinte, "Operacao seguinte");
        }
    }
}
