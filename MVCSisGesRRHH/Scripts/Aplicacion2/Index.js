
$(function () {
    alert("cargando aplicacion");

});

function Eliminar_Aplicacion(Id) {
    jqMensaje("Confirmar", "¿Esta seguro de eliminar la  Aplicacion?", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "Aplicacion2/EliminarAplicacion", { Id_aplicacion: Id }, function (data) {
                if (data.rpta == "1") {
                    $('#divModalSenace').modal('hide');
                    $("form:last").submit();
                }
                else {
                    alert(data.split("|")[1]);
                }
            });
        }
    });
}




