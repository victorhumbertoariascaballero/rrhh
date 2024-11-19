function Eliminar_Aplicacion(Id) {
    jqMensaje("Confirmar", "¿Está seguro de eliminar la Aplicación?", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "Aplicacion/EliminarAplicacion", { Id_aplicacion: Id }, function (data) {
                //alert(data.rpta);
                //alert(data.split("|")[1]);
                if (data.rpta) {
                    $("form:last").submit();
                }
                else {
                    alert(data.errores); //split("|")[1]);
                }
            });
        }
    });
}