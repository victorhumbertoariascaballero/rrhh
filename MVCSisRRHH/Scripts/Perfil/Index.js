$(document).ready(function () {
    //   $("#btnNuevo").click(function () {
    //           $(this).attr("data-url", "SectorAplicacion/Editar?Id_Sector=0");
    //       var atributo = $(this).attr("data-url");
    //       var atributonuevo = atributo.replace("IdSector=0", "IdSector=" + id);
    //       $(this).attr("data-url", atributonuevo);
    //
    //  });
});
/*********************************************************************/


function Eliminar_Perfil(Id) {
    jqMensaje("Confirmar", "¿Esta seguro de eliminar el Perfil? ", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "Perfil/EliminarPerfil", { Id_perfil: Id }, function (data) {
                //alert(data.rpta);
                if (data.rpta) {
                    $("form:last").submit();
                }
                else {
                    alert(data.errores);
                }
                
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