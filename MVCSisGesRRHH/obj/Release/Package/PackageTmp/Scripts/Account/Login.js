
$(document).ready(function () {
    $("#btnLogin").on("click", function () {        
        if ($("#vUsuario").val() != "" && $("#vPassword").val() != "") {
            var dataToSend = { vUsuario: $("#vUsuario").val(), vPassword: $("#vPassword").val(), vUrlRedireccion : $("#hdnUrlRedireccion").val() }

            dataToSend = JSON.stringify(dataToSend);
            $.ajax({
                url: $("#UrlLogin").val(),
                data: dataToSend,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                success: function (resultado) {
                    if (resultado.MessageCode == "0000") {
                        
                        if (resultado.vRutaInicio.toUpperCase().indexOf("DESCARGARLIBREARCHIVOIMPUTADO")!=-1) {
                            window.open(resultado.vRutaInicio);
                            window.location = "/";
                            //window.top.close()                           
                        }
                        else {
                            window.location = resultado.vRutaInicio;
                        }
                    } else {
                        alert(resultado.Message);
                    }
                },
                error: function (xhr, status, error) {
                    alert("Se produjo un error al guardar el registro");
                }
            });
        }
        else {

            alert("Ingrese su usuario y contraseña");
        }


    });
});