var Controladora = "";
var Accion = "";
var Parametros = "";
var Id = "";
var Filtros = "";
var nombre = "";
$(document).ready(function () {

    $("div .paginationDHH").each(function (pos, item) {

        var id = $(this).attr("id");
        $("#" + id).paginate({ 

            //count: Math.ceil($("#hdTotal_" + nombre).val() / $("#hdRegxPag_" + nombre).val()),
            count: Math.ceil($("#hdTotal_" + $(this).attr("id")).val() / $("#hdRegxPag_" + $(this).attr("id")).val()),
            start: $("#hdPag_" + $(this).attr("id")).val(),
            display: 3,
            border: false,
            text_color: "#912836",
            background_color: "#008EB3",//"#FFFFFF",
            text_hover_color: "#912836",
            background_hover_color: "#EEE",
            first_text: "Primero",
            last_text: "Ultimo",
            nombre_paginador: $(this).attr("id"),
            onChange: function (e, nom) {
                $("#hdPag_" + nom).val(e);

                if (Controladora == "") {
                    $("#" + nom).parents("form").submit();
                } else {
                    $("#SeccionDetalleJson").load("/" + Controladora + "/" + Accion, "&filtro=" + Filtros + "&hdPag=" + e + "&btnBuscar=&" + Parametros);
                }
            }
        });

        $("#select" + $(this).attr("id")).val($("#hdRegxPag_" + $(this).attr("id")).val());

        $("#select" + id).on("change", function () {
            
            $("#hdRegxPag_" + id).val($(this).val());
            $("#" + id).parents("form").submit();
        });
    });

});

function RefrescarCambioSelect(valor, nombre) {
    $("#hdPag_" + nombre).val(valor);
    $("#" + nombre).parents("form").submit();
}

