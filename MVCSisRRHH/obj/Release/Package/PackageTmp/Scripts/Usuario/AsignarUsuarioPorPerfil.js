$(document).ready(function () {
    $("#btnCerrarModal").on("click", function () {
        document.location = globalRutaServidor + "Usuario/Index/";
    });
});

function EliminarUsuarioPerfil(idUsuario, idPerfil)
{
    
    jqMensaje("Confirmar", "¿Esta seguro de eliminar Usuario de Perfil?", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "Usuario/EliminarUsuarioDePerfil", { idUsuario: idUsuario, idPerfil: idPerfil }, function (data) {
                $("form:last").submit();
                //if (data == "0") {
                //    $("form:last").submit();
                // }
                // else {
                //      alert(data.split("|")[1]); 
                // }
            });
        }
    });
}