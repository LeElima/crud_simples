using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Cadastro
{
    public class DefaultAlerts
    {
        enum ToastTipo { Success, Error, Warning, Info, Question }

        //https://github.com/dolce/iziToast/archive/master.zip
        //  Download do iziToast para as
        //  notificações padrão do sistema.
        public static void ShowSuccessAlert(Page pagina, string msg, string titulo = "Sucesso!", string tema = "light", string posicao = "bottomRight", bool wrap = false)
        {
            var js = MontarJScriptToast(ToastTipo.Success, msg, titulo, tema, posicao);
            RegistrarScript(pagina, js, wrap);
        }
        public static void ShowWarningAlert(Page pagina, string msg, string titulo = "Aviso!", string tema = "light", string posicao = "bottomRight", bool wrap = false)
        {
            var js = MontarJScriptToast(ToastTipo.Warning, msg, titulo, tema, posicao);
            RegistrarScript(pagina, js, wrap);
        }

        public static void ShowErrorAlert(Page pagina, string msg, string titulo = "Erro!", string tema = "light", string posicao = "bottomRight", bool wrap = false)
        {
            var js = MontarJScriptToast(ToastTipo.Error, msg, titulo, tema, posicao);
            RegistrarScript(pagina, js, wrap);
        }

        public static void ShowInfoAlert(Page pagina, string msg, string titulo = "Informação!", string tema = "light", string posicao = "bottomRight", bool wrap = false)
        {
            var js = MontarJScriptToast(ToastTipo.Info, msg, titulo, tema, posicao);
            RegistrarScript(pagina, js, wrap);
        }

        private static StringBuilder MontarJScriptToast(ToastTipo tipo, string msg, string titulo, string tema, string posicao)
        {
            var js = new StringBuilder();
            js.AppendLine($"iziToast.{tipo.ToString().ToLower()}({{");
            js.AppendLine($"    title: '{titulo}',");
            js.AppendLine($"    message: '{msg.Replace("'", "\\'")}',");
            js.AppendLine($"    progressBar: false,");
            js.AppendLine($"    theme: '{tema}',");
            js.AppendLine($"    position: '{posicao}'");
            js.AppendLine("});");
            return js;
        }

        /// <summary>
        /// Regista o javascript na página asp
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="js"></param>
        /// <param name="wrapJScript">Se true: embala o script fornecido em uma função que será executada no load da página</param>
        private static void RegistrarScript(Page pagina, StringBuilder js, bool wrapJScript)
        {
            if (wrapJScript)
                js = WrapPluginToast(js);

            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), js.ToString(), true);
        }

        /// <summary>
        /// Embala o javascript fornecido em uma função que será executa uma única vez no load da página. 
        /// Isso evita que no refresh da página a mensagem/toast apareça novamente
        /// </summary>
        private static StringBuilder WrapPluginToast(StringBuilder javascript)
        {
            var js = new StringBuilder();
            js.AppendLine("(function() {");
            js.AppendLine("    var showToast = function (){");
            js.Append(javascript);
            js.AppendLine("        Sys.Application.remove_load(showToast);");
            js.AppendLine("    }");
            js.AppendLine("    Sys.Application.add_load(showToast);");
            js.AppendLine("})(this);");
            return js;
        }

        public static void ShowConfirmAlert(Page pagina, string methodName, string msg, string titulo, string tema = "light", string posicao = "center")
        {
            var sb = new StringBuilder();
            //sb.AppendLine(@"function retornoAjax(retorno) {");
            //sb.AppendLine(@"");
            //sb.AppendLine(@"                    $.ajax({");
            //sb.AppendLine(@"                        type: ""POST"",");
            //sb.AppendLine(@"                        url: '" + pagina.AppRelativeVirtualPath.Split('/').Last() + "/" + methodName + "', ");
            //sb.AppendLine(@"                        data: '',");
            //sb.AppendLine(@"                        contentType: 'application/json; charset=utf-8',");
            //sb.AppendLine(@"                        dataType: 'text',");
            //sb.AppendLine(@"                        error: function (XMLHttpRequest, textStatus, errorThrown) {");
            //sb.AppendLine(@"                            alert(""Request: "" + XMLHttpRequest.toString() + ""\n\nStatus: "" + textStatus + ""\n\nError: "" + errorThrown);");
            //sb.AppendLine(@"                        }");
            //sb.AppendLine(@"                    });");
            //sb.AppendLine(@"");
            //sb.AppendLine(@"                }");
            sb.AppendLine(@" ");
            sb.AppendLine(@"iziToast.question({");
            sb.AppendLine(@"    timeout: 20000,");
            sb.AppendLine(@"    close: false,");
            sb.AppendLine(@"    overlay: true,");
            sb.AppendLine(@"    toastOnce: true,");
            sb.AppendLine(@"    id: 'question',");
            sb.AppendLine(@"    progressBar: false,");
            sb.AppendLine(@"    zindex: 999,");
            sb.AppendLine(@"    title: '" + titulo + "',");
            sb.AppendLine(@"    theme: '" + tema + "',");
            sb.AppendLine(@"    message: '" + msg + "',");
            sb.AppendLine(@"    position: '" + posicao + "',");
            sb.AppendLine(@"    buttons: [");
            sb.AppendLine(@"        ['<button onclick=\'retornoAjax(1)\'><b>Sim</b></button>', function (instance, toast) {");
            sb.AppendLine(@" ");
            sb.AppendLine(@"            instance.hide({ transitionOut: 'fadeOut' }, toast, true);");
            sb.AppendLine(@" ");
            sb.AppendLine(@"        }, true],");
            sb.AppendLine(@"        ['<button  onclick=\'retornoAjax(0)\'>Não</button>', function (instance, toast) {");
            sb.AppendLine(@" ");
            sb.AppendLine(@"            instance.hide({ transitionOut: 'fadeOut' }, toast, false);");
            sb.AppendLine(@" ");
            sb.AppendLine(@"        }],");
            sb.AppendLine(@"    ]");
            sb.AppendLine(@"});");

            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), sb.ToString(), true);
        }
    }
}