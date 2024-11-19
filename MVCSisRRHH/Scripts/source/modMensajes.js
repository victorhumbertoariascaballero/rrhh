/// <reference path="../jquery-1.10.2.min.js" />
/// <reference path="../bootbox.js" />


/*Enumerados*/
var enumClaseTipoAlertas = {
    alertaSuccess: 1,
    alertaInfo: 2,
    alertaWarning: 3,
    alertaDanger: 4
};

function RecuperarClaseAlerta(valor) {
    var clase = ''
    switch (valor) {
        case 1: clase = 'alert alert-success alert-dismissible'; break;
        case 2: clase = 'alert alert-info alert-dismissible'; break;
        case 3: clase = 'alert alert-warning alert-dismissible'; break;
        case 4: clase = 'alert alert-danger alert-dismissible'; break;
    }
    return clase;
}

var jqMensaje = function (titulo, mensaje, callback) {
    var opciones = {
        message: mensaje,
        title: titulo
    };

    opciones.buttons = {
        confirm: {
            label: 'Si',
            className: 'btn-info',
            callback: function (result) {
                callback(true);
            }
        },
        cancel: {
            label: 'No',
            className: 'btn-default',
            callback: function (result) {
                callback(false);
            }
        }
    };
    bootbox.dialog(opciones);
}

var jqAlertas = function (titulo, mensaje, tipoClase) {

    var clase = RecuperarClaseAlerta(tipoClase);

    var divMensaje = "<div class='" + clase + "' role='alert'>"
    divMensaje += " <button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
    divMensaje += "  <span aria-hidden='true'>&times;</span>"
    divMensaje += " </button>"
    divMensaje += " <strong>" + titulo + "</strong><br/>" + mensaje
    divMensaje += " </div>"
    return divMensaje;
}


