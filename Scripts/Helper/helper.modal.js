
var modalHelper = function () {
    var defaults = {
        titulo: '',
        mensagem: '',
        actionOk: {
            id: 'okfunction',
            class: 'btn btn-primary',
            icon: '',//'fa fa-check',
            texto: '',
            callback: null,
            botaoReferencia: null
        },
        actionCancelar: {
            id: 'cancelfunction',
            class: 'btn btn-default',
            icon: '',//'fa fa-ban',
            texto: '',
            callback: null,
            botaoReferencia: null
        },
        show: true
    };


    function montarModal(settings) {
        var $modal = $('#modalGenerico'),
            $modalTitle = $("#modalGenerico .modal-title"),
            $modalBody = $("#modalGenerico .modal-body"),
            $modalFooter = $("#modalGenerico .modal-footer").empty();

        $modalTitle.html(!settings.titulo ? "<br />" : settings.titulo);
        $modalBody.html(!settings.mensagem ? "" : "<p>" + settings.mensagem + "</p>");

        montarBotao(settings.actionOk, $modal, $modalFooter);
        montarBotao(settings.actionCancelar, $modal, $modalFooter);

        if (settings.show)
            $modal.modal('show');

        $modal.unbind('hidden.bs.modal', handlerClose)
            .bind('hidden.bs.modal', handlerClose);

        return $modal;
    }
    function montarBotao(actionBtn, $modal, $footer) {
        if (actionBtn && actionBtn.texto) {
            var funcFecharModal = function () { $modal.modal('hide'); },
                $btn,
                btnHtml;

            btnHtml = '<a id="' + actionBtn.id + '" class="' + actionBtn.class + '">';

            if (actionBtn.icon)
                btnHtml += '<i class="' + actionBtn.icon + '" aria-hidden="true"></i>&nbsp;';

            btnHtml += actionBtn.texto + '</a>';

            $btn = $(btnHtml);


            $footer.append($btn);

            if (actionBtn.botaoReferencia && typeof (actionBtn.botaoReferencia) === 'object') {
                $btn.attr('href', $(actionBtn.botaoReferencia).attr("href"));
            }

            $btn.unbind("click")
                .bind('click', function () {
                    funcFecharModal();
                    actionBtn.callback && actionBtn.callback();
                    return true;
                });
        }
    }
    var handlerClose = function () {
        $('.modal:visible').length && $(document.body).addClass('modal-open');
        fixBugsModal();
    };
    function fixBugsModal() {
        var modalBackdrops = obterBackdropDeModaisAbertos();
        var backdropsRemover = obterBackdropRemover(modalBackdrops);

        removerElementos(backdropsRemover);

        if (!modalBackdrops.length)
            removerStyleModalBootstrap();
    }
    function obterBackdropDeModaisAbertos() {
        return $('.modal')
            .map(function (i, e) {
                var data = $(e).data('bs.modal');
                return data && data.$backdrop;
            })
            .toArray();
    }
    function obterBackdropRemover(backdropsNaoExcluir) {
        var backdrops = $('.modal-backdrop').toArray();

        if (backdropsNaoExcluir && backdropsNaoExcluir.length) {
            backdrops = backdrops.filter(function (elem) {
                return backdropsNaoExcluir.some(function ($backdrop) { return !$(elem).is($backdrop); });
            });
        }

        return backdrops;
    }
    function removerElementos(elementos) {
        if (elementos && elementos.length)
            elementos.forEach(function (e) { e.remove(); });
    }
    function removerStyleModalBootstrap() {
        var $body = $("body");
        $body = $("body");
        $body.css('padding-right', '0px');
        $body.removeClass('modal-open');
    }

    var modal = {
        empty: function (options) {
            var settings = $.extend(true, {}, defaults, options);
            return montarModal(settings);
        },
        tituloEMensagem: function (titulo, mensagem) {
            var options = {
                titulo: titulo,
                mensagem: mensagem,
                actionOk: { texto: 'Ok' }
            };

            var settings = $.extend(true, {}, defaults, options);
            return montarModal(settings);
        },
        confirmacao: function (options) {
            var defaultsConfirmacao = {
                titulo: 'Atenção',
                actionOk: { texto: 'Ok' },
                actionCancelar: { texto: 'Cancelar' }
            };

            var settings = $.extend(true, {}, defaults, defaultsConfirmacao, options);
            return montarModal(settings);
        },
        confirmacaoExclusao: function (options) {
            var defaultsConfirmacao = {
                titulo: 'Confirmar',
                mensagem: 'Deseja realmente excluir?',
                actionOk: {
                    texto: 'Sim',
                    class: 'btn btn-danger',
                    icon: '',//icon: 'fa fa-times',
                    botaoReferencia: (options instanceof HTMLElement || options instanceof jQuery) && options
                },
                actionCancelar: {
                    texto: 'Não',
                    class: 'btn btn-default',
                    icon: ''//icon: 'fa fa-ban'
                }
            };

            var settings = $.extend(true, {}, defaults, defaultsConfirmacao, options);
            montarModal(settings);

            return false;
        },
        bootstrapFechar: function (idModal) {
            $('#' + idModal).modal('hide');
            return false;
        },
        fixBugsModal: fixBugsModal
    };

    return modal;
}();

$.modalHelper = modalHelper;


//encapsula jquery
(function ($) {
    var executarOnLoad = function () {
        $(function () {
            modalHelper.fixBugsModal();

            //fecha o modal quando o botão é clicado
            $('[data-btn-modal=close]').click(function () {
                $(this).closest('.modal').modal('hide');
            });

            $('.modal[data-modal-abrir=auto]').each(function () {
                var $that = $(this);
                var aberto = $that.find('[id*=hfAberto]').val();

                if (aberto && Boolean.parse(aberto))
                    $that.modal({ backdrop: 'static', keybord: false });

                $that.on('hidden.bs.modal', function (e) {
                    $that.find('[id*=hfAberto]').val("False");
                });
            });
        });
    };

    // executar função no load da página ou postback do asp
    if (Sys && Sys.Application)
        Sys.Application.add_load(executarOnLoad);
    else
        executarOnLoad();

})(jQuery);