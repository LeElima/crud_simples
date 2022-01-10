using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cadastro
{
    public class Modal : Panel
    {
        private HiddenField _hfAberto;
        private bool _abertoNoHtml;

        /// <summary>
        /// Atribui a classe css 'fade' ao elemento html. 
        /// A classe css 'fade' aplica o efeito 
        /// </summary>
        public bool FadeEffect
        {
            get { return Convert.ToBoolean(ViewState["FadeEffect"]?.ToString()); }
            set { ViewState["FadeEffect"] = value; }
        }

        /// <summary>
        /// Se o valor de FecharAuto for 'true', o modal é fechado automaticamente a cada requisição ao servidor.
        /// <para>False é o valor padrão</para>
        /// </summary>
        public bool FecharAuto
        {
            get { return Convert.ToBoolean(ViewState["FecharAuto"]?.ToString()); }
            set { ViewState["FecharAuto"] = value; }
        }

        /// <summary>
        /// Oculta o controle caso o mesmo não esteja aberto. 
        /// Atribui o valor false para a propriedade Visible.
        /// <para>True é o valor padrão</para>
        /// </summary>
        public bool OcultarSeFechado
        {
            get { return Convert.ToBoolean(ViewState["OcultarSeNaoVisivel"]?.ToString()); }
            set { ViewState["OcultarSeNaoVisivel"] = value; }
        }

        /// <summary>
        /// Adiciona o atributo data-modal-abrir="auto" ao elemento. 
        /// O atributo é utilizado no lado do cliente para abrir o modal automaticamente quando a página é carregada.
        /// <para>True é o valor padrão</para>
        /// </summary>
        public bool AbrirAuto
        {
            get { return Convert.ToBoolean(ViewState["AbrirAuto"]?.ToString()); }
            set { ViewState["AbrirAuto"] = value; }
        }

        /// <summary>
        /// Identifica quando o modal está aberto ou não. 
        /// O hidden field "hfAberto" é utilizado no lado do cliente para identificar quando o usuário fecha o modal.
        /// <para>True é o valor padrão</para>
        /// </summary>
        public bool Aberto
        {
            get => bool.TryParse(_hfAberto.Value, out bool visible) ? visible : false;
            set
            {
                _hfAberto.Value = value.ToString();

                if (value)
                    Visible = value;
            }
        }

        public string DialogCssClass { get; set; }

        public Modal()
        {
            //TabIndex = -1;
            ClientIDMode = ClientIDMode.Static;
            FadeEffect = true;
            FecharAuto = false;
            OcultarSeFechado = true;
            AbrirAuto = true;

            _hfAberto = new HiddenField
            {
                ClientIDMode = ClientIDMode.Static,
                Value = false.ToString()
            };
        }

        protected override void OnInit(EventArgs e)
        {
            _hfAberto.ID = $"{ID}_hfAberto";
            Controls.Add(_hfAberto);

            if (Aberto)
                _abertoNoHtml = true;

            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!_abertoNoHtml && FecharAuto)
                Aberto = false;

            if (!Aberto && OcultarSeFechado)
                Visible = false;

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            Attributes.Add("role", "dialog");

            if (AbrirAuto)
                Attributes.Add("data-modal-abrir", "auto");

            base.CssClass = BuildCssClass();

            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            var cssClass = $"modal-dialog {DialogCssClass}".Trim();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.AddAttribute("role", "document");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.RenderContents(writer);

            writer.RenderEndTag(); //close tag modal-content
            writer.RenderEndTag(); //close tag modal-dialog
        }

        private string BuildCssClass()
        {
            var cssDefault = new List<string> { "modal" };

            if (!string.IsNullOrWhiteSpace(CssClass))
                cssDefault.AddRange(CssClass.Split(' '));

            if (FadeEffect)
                cssDefault.Add("fade");
            else
                cssDefault.Remove("fade");

            cssDefault = cssDefault.Distinct().ToList();

            return string.Join(" ", cssDefault);
        }

        public void Abrir()
        {
            Aberto = true;
        }

        public void Fechar()
        {
            Aberto = false;

            if (OcultarSeFechado)
                Visible = false;
        }
    }
}
