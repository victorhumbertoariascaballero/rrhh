$("#btnGrabarModal").click(function () {
    jqMensaje("Confirmar", "Está seguro de grabar los cambios?",
       function (data) {
           if (data) {
               var app = {
                   Id_opcion: $("#Id_opcion").val(),
                   Id_aplicacion: $("#Id_aplicacion").val(),
                   Nombre_opcion: $("#Nombre_opcion").val(),
                   Url: $("#Url").val(),
                   Titulo_opcion: $("#Titulo_opcion").val(),
                   Direccion_opcion: $("#Direccion_opcion").val(),
                   Id_grupo_opcion: $("#Id_grupo_opcion").val(),
                   Nroorden: $("#Nroorden").val(),

                   Ip_ingreso: $("#Ip_ingreso").val(),
                   Id_Usuario_Ingresa: $("#Id_Usuario_Ingresa").val(),
                   Usu_ingresa: $("#Usu_ingresa").val(),
                   Fec_ingreso: $("#Fec_ingreso").val(),
                   Ip_Modifica: $("#Ip_Modifica").val(),
                   Id_Usuario_Modifica: $("#Id_Usuario_Modifica").val(),
                   Usu_Modifica: $("#Usu_Modifica").val(),
                   Fec_Modifica: $("#Fec_Modifica").val(),
                   Flg_estado: $("#Flg_estado").val(),
                   Flg_eliminado: $("#Flg_eliminado").val()
               }

               $.getJSON(globalRutaServidor + "Opcion/GrabarOpcion",
            app,
            function (data) {
                if (!data.rpta) {
                    $("#divErrorModal").removeAttr("style");
                    $("#ulListaErrorModal").empty();
                    $.each(data.errores, function (key, value) {

                        if (value != null) {
                            if (key == "*") {
                                $("#ulListaErrorModal").append("<li>" + value + "</li>");
                            }
                            else {
                                $("#" + key).removeClass("valError").addClass("valError");
                                $("#ulListaErrorModal").append("<li>" + key + " - " + value[value.length - 1].ErrorMessage + "</li>");
                            }
                        }
                    });
                }
                else {
                    $('#divModalSenace').modal('hide');
                    parent.RefrescarTreeView();
                }

            });
           }
       });
});