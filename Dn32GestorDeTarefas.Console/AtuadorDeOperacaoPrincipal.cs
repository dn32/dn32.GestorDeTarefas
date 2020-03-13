using System.Threading.Tasks;

namespace dn32.GestorDeTarefas.ConsoleTeste
{
    public class AtuadorDeOperacaoPrincipal
    {
        public int Cliente { get; internal set; }

        public string Token { get; internal set; }

        public void AtividadeDeConsultaDeComplemento(object obj)
        {
            //Consulta o tratador com respeito ao conteúdo da operação
        }

        public void AtualizarDados(object obj)
        {
            // Atualiza o conteúdo com base nos dados do tratador
        }

        public void Stream(object obj)
        {
            //Processa a subida de dados
        }

        public void AlimineOLixo(object obj)
        {
            // Limpa a memória para finalizar o processo
        }

        public void NotifiqueAConclusao(object obj)
        {
            // Notifica à API que deu tudo certo
        }
        
        public void SalveRegistros(object obj)
        {
            Task.Delay(5000).Wait();
        }

        public void OperacaoSeguinte(object obj)
        {
        }
    }
}
