using System.Threading.Tasks;

namespace dn32.GestorDeTarefas.ConsoleTeste
{
    public class AtuadorDeOperacaoPrincipal
    {
        public int Cliente { get; internal set; }

        public string Token { get; internal set; }

        public void AtividadeDeConsultaDeComplemento()
        {
            //Consulta o tratador com respeito ao conteúdo da operação
        }

        public void AtualizarDados()
        {
            // Atualiza o conteúdo com base nos dados do tratador
        }

        public void Stream()
        {
            //Processa a subida de dados
        }

        public void AlimineOLixo()
        {
            // Limpa a memória para finalizar o processo
        }

        public void NotifiqueAConclusao()
        {
            // Notifica à API que deu tudo certo
        }
        
        public void SalveRegistros()
        {
            Task.Delay(5000).Wait();
        }

        public void OperacaoSeguinte()
        {
        }
    }
}
