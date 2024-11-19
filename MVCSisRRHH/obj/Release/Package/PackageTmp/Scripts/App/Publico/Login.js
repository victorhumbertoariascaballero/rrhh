(function ($) {
    var frmLoginValidador;

    this.LoginJS = function () {
    };

    this.LoginJS.prototype.inicializar = function () {
        frmLoginValidador = $("#frmLogin").kendoValidator().data("kendoValidator");
    };

    this.LoginJS.prototype.iniciarSesion = function (e) {
        e.preventDefault();

        var data_param = new FormData();
        data_param.append('CuentaUsuario', $('#txtCuentaUsuario').val());
        data_param.append('Contrasenia', $('#txtContrasenia').val());
        data_param.append('CaptchaTexto', '1234');

        if (!frmLoginValidador.validate())
            alert('FORMULARIO NO VÁLIDO');
        else {
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Login/IniciarSesion',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    if (res.EstaAutenticado)
                        window.location = controladorApp.obtenerRutaBase();
                    else {
                        controladorApp.notificarMensajeDeAlerta("No se ha podido autenticar el usuario y la contraseña indicada");
                    }
                }
            });
        }

        return false;
    };


}(jQuery));