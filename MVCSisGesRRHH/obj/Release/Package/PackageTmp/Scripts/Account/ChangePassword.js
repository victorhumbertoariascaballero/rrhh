
$(document).ready(function () {


    $("#btnCancelar").on("click", function () {

        window.location = $("#UrlPaginaPorDefecto").val();

    })


    $("#btnEnviarCorreo").on("click", function () {

        if ($("#vUsuario").val() != "" && $("#cCodDocPersona").val() !="") {
            var dataToSend = { vUsuario: $("#vUsuario").val(), oPersona: { cCodDocPersona: $("#cCodDocPersona").val() } }

            dataToSend = JSON.stringify(dataToSend);
            $.ajax({
                url: $("#UrlChangePassword").val(),
                data: dataToSend,
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    $("#lblMensaje").css("color", "#4373bf").html("Espere por favor...");
                    $("#btnEnviarCorreo").attr("disabled", "disabled");
                },
                success: function (resultado) {
                    $("#btnEnviarCorreo").removeAttr("disabled");
                    if (resultado.MessageCode == "0000") {
                        $("#lblMensaje").css("color", "#38a32c").html(resultado.Message);
                        //window.location = resultado.vRutaInicio;
                    } else {
                        $("#lblMensaje").css("color", "#ff0000").html(resultado.Message);
                    }
                },
                error: function (xhr, status, error) {
                    $("#lblMensaje").css("color", "#ff0000").html("Ocurrió un error");
                }
            });
        }
        else {
            $("#lblMensaje").css("color", "#ff0000").html("Verifique su email y/o DNI   ");

        }

    });
});