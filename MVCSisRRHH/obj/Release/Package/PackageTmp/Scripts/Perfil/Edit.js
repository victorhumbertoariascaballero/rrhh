$(document).ready(function () {
    $("#btnGrabarModal").click(function () {

        jqMensaje("Confirmar", "Está seguro de grabar los cambios?",
        function (data) {
            if (data) {
                var app = {
                    Id_perfil: $("#Id_perfil").val(),
                    Nombre_perfil: $("#Nombre_perfil").val(),
                    Id_aplicacion: $("#Id_aplicacion").val(),
                    Ip_Ingreso: $("#Ip_Ingreso").val(),
                    Usu_ingresa: $("#Usu_ingresa").val(),
                    Usu_modifica: $("#Usu_modifica").val(),
                    Ip_ingreso: $("#Ip_ingreso").val(),
                    Ip_modifica: $("#Ip_modifica").val(),
                    Fec_ingreso: $("#Fec_ingreso").val(),
                    Fec_modifica: $("#Fec_modifica").val(),
                    Flg_eliminado: $("#Flg_eliminado").val(),
                    Id_usuario_ingresa: $("#Id_usuario_ingresa").val(),
                    Id_usuario_modifica: $("#Id_usuario_modifica").val()
                   
                }

                $.getJSON(globalRutaServidor + "Perfil/GrabarAplicacion",
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