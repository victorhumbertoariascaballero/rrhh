var textoPlaceholder = "Dato obligatorio";
//
$(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

//$.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' });
$(document).ready(function ()
{
    //$.blockUI({ message: null });
    $.ajaxSetup({ cache: false });
    
    bootbox.setDefaults({
        /**
         * @optional String
         * @default: en
         * which locale settings to use to translate the three
         * standard button labels: OK, CONFIRM, CANCEL
         */
        locale: "es",

        /**
         * @optional Boolean
         * @default: true
         * animate the dialog in and out (not supported in < IE 10)
         */
        animate: true,


    });

    $('body').on("click", ".modalSENACE", function () {
        var url = $(this).attr('data-url');
        //$("").app
        $.get(url, function (data) {
            $('.contenedorSenace').html(data);
            $('#divModalSenace').modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header",
                cursor: "move"
            });
        });
    });

    $('body').on("click", ".modalSENACEUrl", function () {
        var url = $(this).attr('data-url');
        var cont = $(this).attr('data-contenedor');
        var div = $(this).attr('data-div');
        

        $.get(url, function (data) {
            $('#' + cont).html(data);
            $('#' + div).modal('show');
            $(".modal-content").draggable({
                handle: ".modal-header",
                cursor: "move"
            });
        });
    });

    //$.blockUI({ css: { backgroundColor: '#f00', color: '#fff' } });

    /**
     * Controla la tacla enter para pasar el foco al siguiente cuadro de texto
     */
    $("input.next:text").on('keypress', function (e) {

        if (e.keyCode == 13) {
            var nextIndex = $('input:not([readonly="readonly"]):text').index(this) + 1;
            $('input:not([readonly="readonly"]):text')[nextIndex].focus();
        }
    });

    /**
 * Controlar el ingreso de texto en un cuadro de texto y lo convierte a mayuscula
 */
    $("input.toupper:text").on('change', function (e) {
        //if (e.which == 36) {
        //    alert("demos");
        //    return;
        //}

        //this.value = this.value.toUpperCase();
        //if (e.which >= 97 && e.which <= 122) {
        //    var newKey = e.which - 32;
        //    e.keyCode = newKey;
        //    e.charCode = newKey;
        //}
        $(this).val(($(this).val()).toUpperCase());
    });

  
});


/**
* Funcion que genera una ventana popup
* @param {Direccion url obligatorio para levantar la pagina} url 
* @param {Parametro que define el titulo de la ventana} name 
* @param {Establece el ancho de la ventana} width 
* @param {Establece el alto de la ventana} height 
* @returns {Ejecuta el popup} 
*/
function fventanaPopup(url, name, width, height) {
    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var settings = "toolbar=no,location=no,directories=no," +
        "status=no,menubar=no,scrollbars=no," +
        "resizable=yes,width=" + width + ",height=" + height + ",top=" + top + ",left=" + left;
    var miVentana = window.open(url, name, settings);
    //miVentana.print();
    ///miVentana.close();
};


/**
 * Funcion que formatea una fecha, en formato corto: dd/mm/yyyy
 * @param {Parametro que es recibio por el cliente} date 
 * @returns {returno un formato de fecha en formato dd/mm/yyyy} 
 */
function FormatearFecha(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;
    return day + '/' + month + '/' + year;
}

//function popupDHH(idp, idl, w, z) {
//    var ww = $(window).width();
//    var wh = $(window).height();
//    var h = $('#' + idp).height();
//    var ml = ((ww / 2) - (w / 2)) - 17;
//    var mt = ((wh / 2) - (h / 2)) - 17;
//    $('#' + idl).css('display', 'block');
//    $('#' + idp).css({
//        'display': 'block',
//        'width': w + 'px',
//        'height': h + 'px',
//        'margin-left': ml + 'px',
//        'margin-top': mt + 'px',
//        'z-index': z
//    });
//}
function SoloNumeros(e) {
    var val = (document.all);
    var key = val ? e.keyCode : e.which;
    if (key > 31 && (key < 48 || key > 57) && key != 45) {
        if (val)
            window.event.keyCode = 0;
        else {
            e.stopPropagation();
            e.preventDefault();
        }
    }


}