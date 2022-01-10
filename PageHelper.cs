using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;


namespace Cadastro
{
    public class PageHelper
    {
        public static void AbrirModal(Page pagina, string idModal, bool verificarParentIsModal = false, bool isFadeEffect = true)
        {
            var script = new StringBuilder();
            script.Append(" $('.modal-backdrop').remove();");
            script.Append(" $('body').removeClass('modal-open').css('padding-right','0px');");
            script.AppendLine($"    var $modal = $('#{idModal}');");

            if (verificarParentIsModal)
                script.AppendLine(" $modal.parent().closest('.modal').modal({ backdrop: 'static', keybord: false});");

            if (isFadeEffect)
                script.AppendLine("$modal.addClass('fade');");
            else
                script.AppendLine("$modal.removeClass('fade');");

            script.AppendLine("$modal.modal({ backdrop: 'static', keybord: false});");

            ExecutarScript(pagina, script.ToString());
        }

        public static void FecharModal(Page pagina, string idModal)
        {
            var script = new StringBuilder();
            script.Append($"$('#{idModal}').modal('hide');");
            script.Append("$('body').removeClass('modal-open').css('padding-right','0px');");
            script.Append("$('.modal-backdrop').last().remove();");

            ExecutarScript(pagina, script.ToString());
        }

        public static void ExecutarScript(Page pagina, string script)
        {
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}