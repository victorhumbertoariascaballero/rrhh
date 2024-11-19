(function ($) {
    var rutaBase = '';
    var notificacion = null;

    this.IntranetAnonimoAppJS = function (parametro) {
        rutaBase = parametro.rutaBase;
    };

    this.IntranetAnonimoAppJS.prototype.inicializar = function () {

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
                    ,baseZ: 1000000
                });
            })
            .ajaxComplete(function () {
                $.unblockUI();
            })
            .ajaxError(function (e, xhr, opts) {
                debugger;
                switch (xhr.status) {
                    case 0: //401
                        controladorApp.notificarMensajeDeError("La sesión ha terminado");
                        //setTimeout(function () { window.location = rutaBase + 'Login/'; }, 1000);
                        break;
                    case 401: //401
                        controladorApp.notificarMensajeDeError("La sesión ha terminado");
                        //setTimeout(function () { window.location = rutaBase + 'Login/'; }, 1000);
                        break;
                    case 403: //403
                        controladorApp.notificarMensajeDeError("Usted no esta autorizado para realizar esta acción");
                        break;
                    default:
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            //controladorApp.notificarMensajeDeError("La sesión ha terminado");
                        //    setTimeout(function () { window.location = rutaBase + 'Login/'; }, 1000);
                        //}
                        //else {
                            controladorApp.notificarMensajeDeError("Ha ocurrido un error en la aplicación : " + opts.url);
                        }
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
            autoHideAfter: 5000,
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

        // KMM
        //$.ajax({
        //    url: rutaBase + 'Cuenta2/Inicializar',
        //    type: 'POST',
        //    dataType: 'json',
        //    contentType: false,
        //    processData: false,
        //    //data: data_param,
        //    success: function (res) {

        //    },
        //    error: function (res) {

        //    }
        //});
        /**/


    };

    this.IntranetAnonimoAppJS.prototype.obtenerRutaBase = function () {
        return rutaBase;
    };

    this.IntranetAnonimoAppJS.prototype.abrirMensajeDeConfirmacion = function (mensaje, textoSi, textoNo, objFuncion, argFuncion) {
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

    this.IntranetAnonimoAppJS.prototype.abrirMensajeDeConfirmacion2 = function (mensaje, textoSi, textoNo, objFuncionOk, objFuncionCancel) {
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
                objFuncionOk();
            }).fail(function () {
                objFuncionCancel();
            });
    };

    this.IntranetAnonimoAppJS.prototype.notificarMensajeDeInformacion = function (mensaje) {
        notification.show({
            title: "INFORMACIÓN",
            message: mensaje
        }, "informacion");
    };

    this.IntranetAnonimoAppJS.prototype.notificarMensajeDeError = function (mensaje) {
        notification.show({
            title: "ERROR",
            message: mensaje
        }, "error");
    };

    this.IntranetAnonimoAppJS.prototype.notificarMensajeDeAlerta = function (mensaje) {
        notification.show({
            title: "ALERTA",
            message: mensaje
        }, "alerta");
    };

    this.IntranetAnonimoAppJS.prototype.notificarMensajeSatisfactorio = function (mensaje) {
        notification.show({
            title: "ALERTA",
            message: mensaje
        }, "satisfactorio");
    };

    this.IntranetAnonimoAppJS.prototype.mostrarFechaEnGrid = function (valor) {
        var fecha = '';

        if (valor != null)
            fecha = '<i class="fa fa-calendar"></i> &nbsp;&nbsp;' + kendo.toString(kendo.parseDate(valor), 'dd/MM/yyyy');

        return fecha;

    };

    this.IntranetAnonimoAppJS.prototype.mostrarFechaHoraEnGrid = function (valor) {
        var fecha = '';

        if (valor != null)
            fecha = '<i class="fa fa-calendar"></i> &nbsp;&nbsp;' + kendo.toString(kendo.parseDate(valor), 'dd/MM/yyyy hh:mm tt');

        return fecha;

    };

    this.IntranetAnonimoAppJS.prototype.cerrarSesion = function () {
        $.ajax({
            url: rutaBase + 'Login/CerrarSesion',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            //data: data_param,
            success: function (res) {
                if (!res.EstaAutenticado)
                    window.location = rutaBase + 'Login';
            },
            error: function (res) {

            }
        });
    }

    this.IntranetAnonimoAppJS.prototype.cambiarRol = function () {
        
        $.ajax({
            url: rutaBase + 'Cuenta/CambiarRol',
            type: 'POST',
            dataType: 'text',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({nombreCorto: $("#ddlRol").val()}),
            success: function (res) {
                if (res) window.location = rutaBase + '';
            },
            error: function (res) {

            }
        });
    }

    this.IntranetAnonimoAppJS.prototype.htmlDecode = function (value) {
        return value.replace(/&lt;/g, "<").replace(/&gt;/g, ">");
    }
}(jQuery));