
$(document).ready(function () {


    $("#btnCancelar").on("click", function () {

        window.location = $("#UrlPaginaPorDefecto").val();

    })


    $("#btnActualizarContrasena").on("click", function () {

        if ($("#vPassword").val().length==0) {
            $("#lblMensaje").css("color", "#ff0000").html("Ingrese una nueva contraseña por favor");
            return;
        }
        if ($("#vPassword").val() != $("#vRepetirPassword").val()) {

            $("#lblMensaje").css("color", "#ff0000").html("Las contraseñas ingresadas no coinciden");
            return;
        }


        if (!isStrongPwd1($("#vPassword").val())) {
            $("#lblMensaje").css("color", "#ff0000").html("Su contraseña debe tener al menos 8 caracteres, combinado de mayúsculas, minúsculas y números");
            return;
        }

        var dataToSend = { vToken: $("#vToken").val(), vPassword: $("#vPassword").val() }

        dataToSend = JSON.stringify(dataToSend);
        $.ajax({
            url: $("#UrlChangePasswordConfirm").val(),
            data: dataToSend,
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                $("#lblMensaje").css("color", "#4373bf").html("Espere por favor...");
                $("#btnActualizarContrasena").attr("disabled", "disabled");
            },
            success: function (resultado) {
                $("#btnActualizarContrasena").removeAttr("disabled");
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

    });
});

function isStrongPwd1(password) {

    var regExp = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/;

    var validPassword = regExp.test(password);

    return validPassword;

}