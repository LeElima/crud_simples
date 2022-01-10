using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Cadastro
{
    public class PageGeneric : Page
    {
        public WebNotificacao Notificacao { get; set; }

        public PageGeneric()
        {

            Notificacao = new WebNotificacao(this);
        }
        protected void AbrirModal(string idModal) => PageHelper.AbrirModal(this, idModal);
        protected void FecharModal(string idModal) => PageHelper.FecharModal(this, idModal);
        protected void ExecutarScript(string script) => PageHelper.ExecutarScript(this, script);
    }
}