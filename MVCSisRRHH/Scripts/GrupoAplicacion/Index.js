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
function Eliminar_GrupoAplicacion(Id) {
    jqMensaje("Confirmar", "¿Esta seguro de eliminar el Grupo Aplicacion?", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "GrupoAplicacion/EliminarGrupoAplicacion", { Id_GrupoAplicacion: Id }, function (data) {
                $("form:last").submit();
                //if (data == "1") {
                //    $("form:last").submit();
                //}
                //else {
                //    alert(data.split("|")[1]);
                //}
            });
        }
    });
}