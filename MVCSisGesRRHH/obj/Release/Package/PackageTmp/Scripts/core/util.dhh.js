$(function ($) {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
});

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

function castFechaCorta(fechaJson) {

    if (fechaJson == null) {
        return "";

    }

    var fullDate = new Date(parseInt(fechaJson.substr(6)));

    var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? '0' + (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);

    var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();

    return currentDate;

};

