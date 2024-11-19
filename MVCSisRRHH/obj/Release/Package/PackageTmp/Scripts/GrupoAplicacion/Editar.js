$(document).ready(function () {
    $("#btnGrabarModal").click(function () {

            jqMensaje("Confirmar", "Está seguro de grabar los cambios?",
            function (data) {
                if (data) {
                    var app = {
                        Id_grupo: $("#Id_grupo").val(),
                        Nro_orden: $("#Nro_orden").val(),
                        Nombre_grupo: $("#Nombre_grupo").val(),
                        Id_sector: $("#Id_sector").val(),
                        Ip_Ingreso: $("#Ip_Ingreso").val(),
                        Usu_ingresa: $("#Usu_ingresa").val(),
                        Usu_modifica: $("#Usu_modifica").val(),
                        Ip_ingreso: $("#Ip_ingreso").val(),
                        Ip_modifica: $("#Ip_modifica").val(),
                        Fec_ingreso: $("#Fec_ingreso").val(),
                        Fec_modifica: $("#Fec_modifica").val()
                    }

                    $.getJSON(globalRutaServidor + "GrupoAplicacion/GrabarGrupoAplicacion",
                        app,
                        function (data) {
                            if (!data.rpta) {
                                $("#divError").removeAttr("style");
                                $("#ulListaError").empty();
                                $.each(data.errores, function (key, value) {

                                    if (value != null) {
                                        if (key == "*") {
                                            $("#ulListaError").append("<li>" + value + "</li>");
                                        }
                                        else {
                                            $("#" + key).removeClass("valError").addClass("valError");
                                            //$("#ulListaError").append("<li>" + key + " - " + value[value.length - 1].ErrorMessage + "</li>");
                                            $("#ulListaError").append("<li>" + value[value.length - 1].ErrorMessage + "</li>");
                                        }
                                    }
                                });
                            }
                            else {
                                $('#divModalSenace').modal('hide');
                                $("form:last").submit();
                            }


                        });
                }
            });
        })
    //$('#btnCerrarModal').click(function (e) {
    //    $(".seccionModal").hide();
    //    window.location = globalRutaServidor + "EntidadLocal";
    //});
})
/*********************************************************************/
//function fRefrescar(data) {
//    if (typeof ($(data).find("#SeccionDetalle").html()) == "undefined") {
//        $(".seccionModal").hide();
//        window.location = globalRutaServidor + "GrupoAplicacion";
//    }
//}