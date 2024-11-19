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


function Eliminar_SectorAplicacion(Id) {
    jqMensaje("Confirmar", "¿Esta seguro de eliminar el Sector Aplicacion?", function (result) {
        if (result) {
            $.getJSON(globalRutaServidor + "SectorAplicacion/EliminarSectorAplicacion", { Id_Sector: Id }, function (data) {
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