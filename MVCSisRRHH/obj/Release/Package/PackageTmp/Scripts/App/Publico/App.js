(function ($) {
    var rutaBase = '';
    var notificacion = null;

    this.PublicoAppJS = function (parametro) {
        rutaBase = parametro.rutaBase;
    };

    this.PublicoAppJS.prototype.inicializar = function () {
        kendo.culture("es-PE");

        $.ajaxSetup({ cache: false });

        $(document)
            .ajaxStart(function () {

            })
            .ajaxSend(function (event, jqxhr, settings) {
                $.blockUI({
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000000',
                        //'-webkit-border-radius': '10px',
                        //'-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    },
                    message: "Procesando..."
                });
            })
            .ajaxComplete(function () {
                $.unblockUI();
            })
            .ajaxError(function (e, xhr, opts) {
                switch (xhr.status) {
                    case 0: //401
                        //notificarMensajeDeError("La sesión ha terminado");
                        setTimeout(function () { window.location = rutaBase + 'Cuenta/CerrarSesion'; }, 1000);
                        break;
                    case 200: //403
                        //notificarMensajeDeError("Usted no esta autorizado para realizar esta acción");
                        break;
                    default:
                        //notificarMensajeDeError("Ha ocurrido un error en la aplicación con código: " + xhr.status.toString());
                        break;
                }
            })
            .ajaxSuccess(function (e, xhr, opts) {

            });

        notification = $("#notification").kendoNotification({
            position: {
                pinned: true,
                top: 30,
                right: 30
            },
            autoHideAfter: 3000,
            stacking: "down",
            templates: [{
                type: "informacion",
                template: $("#informacionTemplate").html()
            }, {
                type: "error",
                template: $("#errorTemplate").html()
            }, {
                type: "alerta",
                template: $("#alertaTemplate").html()
            }, {
                type: "satisfactorio",
                template: $("#satisfactorioTemplate").html()
            }],
            show: function (e) {
                if (e.sender.getNotifications().length == 1) {
                    var element = e.element.parent(),
                        eWidth = element.width(),
                        eHeight = element.height(),
                        wWidth = $(window).width(),
                        wHeight = $(window).height(),
                        newTop, newLeft;

                    newLeft = Math.floor(wWidth / 2 - eWidth / 2);
                    newTop = Math.floor(wHeight / 2 - eHeight / 2);

                    e.element.parent().css({ top: 60, left: newLeft });
                }
            }
        }).data("kendoNotification");
    };

    this.PublicoAppJS.prototype.obtenerRutaBase = function () {
        return rutaBase;
    };

    this.PublicoAppJS.prototype.abrirMensajeDeConfirmacion = function (mensaje, textoSi, textoNo, objFuncion, argFuncion) {
        var divConfirm = document.createElement('div');
        divConfirm.id = 'divConfirm';

        var kendoConfirm = $(divConfirm).kendoConfirm({
            width: "300px",
            messages: {
                okText: textoSi,
                cancel: textoNo
            },
            title: "CONFIRMACIÓN",
            content: mensaje,
        }).data("kendoConfirm").open()
            .result.done(function () {
                //ejecutar funcion
                objFuncion(argFuncion);
            }).fail(function () {
                
            });
    };

    this.PublicoAppJS.prototype.notificarMensajeDeInformacion = function (mensaje) {
        notification.show({
            title: "MENSAJE INFORMATIVO",
            message: mensaje
        }, "informacion");
    };

    this.PublicoAppJS.prototype.notificarMensajeDeError = function (mensaje) {
        notification.show({
            title: "MENSAJE DE ERROR",
            message: mensaje
        }, "error");
    };

    this.PublicoAppJS.prototype.notificarMensajeDeAlerta = function (mensaje) {
        notification.show({
            title: "MENSAJE DE ALERTA",
            message: mensaje
        }, "alerta");
    };

    this.PublicoAppJS.prototype.notificarMensajeSatisfactorio = function (mensaje) {
        notification.show({
            title: "MENSAJE DE ALERTA",
            message: mensaje
        }, "satisfactorio");
    };

    this.PublicoAppJS.prototype.mostrarFechaEnGrid = function evalDateToGrid(valor) {
        var fecha = '';

        if (valor != null)
            fecha = '<i class="fa fa-calendar"></i> &nbsp;&nbsp;' + kendo.toString(kendo.parseDate(valor), 'dd/MM/yyyy');

        return fecha;

    };

}(jQuery));