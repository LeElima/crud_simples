using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Cadastro
{
    public class WebNotificacao
    {
        private Page _page;

        public const string MSG_CONTATAR_ADMINISTRADOR = "Se o problema persistir entre em contato com o administrador do sistema.";

        public WebNotificacao(Page page)
        {
            _page = page;
        }

        public void ToastSucesso(string mensagem) => DefaultAlerts.ShowSuccessAlert(_page, mensagem, "", wrap: true);

        public void ToastDadosSalvos() => ToastSucesso("Dados cadastrados com sucesso!");

        public void ToastSucessoExclusao() => ToastSucesso("Dados excluidos com sucesso!");

        public void ToastAtencao(string mensagem) => DefaultAlerts.ShowWarningAlert(_page, mensagem, "Atenção!", wrap: true);

        public void ToastErro(string mensagem) => DefaultAlerts.ShowErrorAlert(_page, mensagem, "Erro!", wrap: true);

        public void ToastInfo(string mensagem) => DefaultAlerts.ShowInfoAlert(_page, mensagem, "Informação!", wrap: true);

        public void Modal(string titulo, string mensagem, bool incluirMsgContatarAdm)
        {
            if (incluirMsgContatarAdm)
                mensagem = $"{mensagem}<br><br>{MSG_CONTATAR_ADMINISTRADOR}";

            Modal(titulo, mensagem);
        }

        public void Modal(string titulo, string mensagem)
        {
            mensagem = mensagem.Replace("\n", "<br>").Replace("\r", "");

            // registra o script no load da página e logo em seguida remove-o 
            // para evitar a mesma exibição do modal em outros postbacks.
            var js = new StringBuilder();
            js.AppendLine("<script>");
            js.AppendLine("(function() {");
            js.AppendLine("    var showModal = function (){");
            js.AppendLine($"       modalHelper.tituloEMensagem('{titulo}', '{mensagem}');");
            js.AppendLine("        Sys.Application.remove_load(showModal);");
            js.AppendLine("    }");
            js.AppendLine("    Sys.Application.add_load(showModal);");
            js.AppendLine("})(this);");
            js.AppendLine("</script>");

            ScriptManager.RegisterStartupScript(_page, _page.GetType(), Guid.NewGuid().ToString(), js.ToString(), false);
        }

        public void ModalAtencao(StringBuilder mensagem) => ModalAtencao(mensagem.ToString());

        public void ModalAtencao(string mensagem) => ModalAtencao(mensagem, false);

        public void ModalAtencao(StringBuilder mensagem, bool incluirMsgContatarAdm) => ModalAtencao(mensagem.ToString(), incluirMsgContatarAdm);

        public void ModalAtencao(string mensagem, bool incluirMsgContatarAdm)
        {
            Modal("Atenção", mensagem, incluirMsgContatarAdm);
        }
    }
}