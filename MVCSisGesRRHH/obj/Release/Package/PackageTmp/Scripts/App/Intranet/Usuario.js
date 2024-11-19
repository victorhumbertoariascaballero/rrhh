(function ($) {
    var frmNuevoValidador;
    var frmEdicionValidador;

    this.UsuarioJS = function () { };

    this.UsuarioJS.prototype.inicializar = function () {

        /* BUSQUEDA */
        $("#ddlIdPersona_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombres",
            dataValueField: "IdPersona",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Usuario/ListarIdPersona",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        
        
        
        
        $("#txtFechaCaducidadEntreDesde_busqueda").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaCaducidadEntreHasta_busqueda").kendoDatePicker({ format: "dd/MM/yyyy" });
        
        
        
        /* REGISTRO */
        $("#ddlIdPersona_nuevo").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombres",
            dataValueField: "IdPersona",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Usuario/ListarIdPersona",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        
        
        
        
        $("#txtFechaCaducidad_nuevo").kendoDatePicker({ format: "dd/MM/yyyy" });
        
        
        

        $('#divModalNuevo').kendoWindow({
            draggable: false,
            modal: true,
            pinned: false,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Nuevo Usuario',
            visible: false,
            position: { top: '5%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmNuevoValidador.hideMessages();
                $("#ddlIdPersona_nuevo").data("kendoDropDownList").value('');
                $("#txtCuentaUsuario_nuevo").val('');
                $("#txtCorreoUsuario_nuevo").val('');
                $("#chkCorreoUsuarioValido_nuevo").prop("checked", false);
                $("#txtCuentaRed_nuevo").val('');
                $("#txtFechaCaducidad_nuevo").data("kendoDatePicker").value('');
                $("#chkEsPrimeravez_nuevo").prop("checked", false);
                $("#txtContrasenia_nuevo").val('');
                $("#txtContraseniaSalt_nuevo").val('');
              
            }
        }).data("kendoWindow");

        frmNuevoValidador = $("#frmNuevo").kendoValidator().data("kendoValidator");
        
        /* EDITAR*/
        $("#ddlIdPersona_edicion").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombres",
            dataValueField: "IdPersona",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Usuario/ListarIdPersona",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        
        
        
        
        $("#txtFechaCaducidad_edicion").kendoDatePicker({ format: "dd/MM/yyyy" });
        
        
        
        $('#divModalEdicion').kendoWindow({
            draggable: false,
            modal: true,
            pinned: false,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Edición de Usuario',
            visible: false,
            position: { top: '5%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmEdicionValidador.hideMessages();
                $("#ddlIdPersona_edicion").data("kendoDropDownList").value('');
                $("#txtCuentaUsuario_edicion").val('');
                $("#txtCorreoUsuario_edicion").val('');
                $("#chkCorreoUsuarioValido_edicion").prop("checked", false);
                $("#txtCuentaRed_edicion").val('');
                $("#txtFechaCaducidad_edicion").data("kendoDatePicker").value('');
                $("#chkEsPrimeravez_edicion").prop("checked", false);
                $("#txtContrasenia_edicion").val('');
                $("#txtContraseniaSalt_edicion").val('');
            }
        }).data("kendoWindow");

        frmEdicionValidador = $("#frmEdicion").kendoValidator().data("kendoValidator");

        /* CONSULTA */
        $('#divModalDetalle').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: '',
            visible: false,
            position: { top: '5%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $('#divModalEliminacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '30%',
            height: 'auto',
            title: 'Confirmar eliminación',
            visible: false,
            position: { top: '5%', left: "35%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

		this.inicializarGrid();

    };

    this.UsuarioJS.prototype.inicializarGrid = function () {
        this.$dataSource = [];

        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            pageSize: 10,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Usuario/Listar',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.IdPersona = $("#ddlIdPersona_busqueda").data("kendoDropDownList").value();
                        data_param.CuentaUsuario = $("#txtCuentaUsuario_busqueda").val();
                        data_param.CorreoUsuario = $("#txtCorreoUsuario_busqueda").val();
                        data_param.CorreoUsuarioValido = $("#ddlCorreoUsuarioValido_busqueda").val();
                        data_param.CuentaRed = $("#txtCuentaRed_busqueda").val();
                        data_param.FechaCaducidadEntreDesde = $("#txtFechaCaducidadEntreDesde_busqueda").data("kendoDatePicker").value();
                        data_param.FechaCaducidadEntreHasta = $("#txtFechaCaducidadEntreHasta_busqueda").data("kendoDatePicker").value();
                        data_param.EsPrimeravez = $("#ddlEsPrimeravez_busqueda").val();
                        data_param.Contrasenia = $("#txtContrasenia_busqueda").val();
                        data_param.ContraseniaSalt = $("#txtContraseniaSalt_busqueda").val();
	                    data_param.RegistroEstaActivo = $("#ddlRegistroEstaActivo_busqueda").val();
                        data_param.Grilla = {};
                        data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                        data_param.Grilla.PaginaActual = $options.page
                        if ($options !== undefined && $options.sort !== undefined) {
                            data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                            data_param.Grilla.OrdenarPor = $options.sort[0].field;
                        }
                    }

                    return $.toDictionary(data_param);
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdUsuario"
                }
            },
            change: function (e) {
                if (!e.items.length > 0) {
                    $("#btnReporte").prop('disabled', true);
                    controladorApp.notificarMensajeDeInformacion('No se encontraron registros');
                }
                else
                    $("#btnReporte").prop('disabled', true);
            }
        });

        this.$grid = $("#divGrid").kendoGrid({
            dataSource: this.$dataSource,
            autoBind: true,
            height: 360,
            selectable: "row",
            sortable: {
                mode: "single",
                allowUnsort: false
            },
            pageable: {
                refresh: false,
                pageSizes: [10, 20, 50],
                previousNext: true,
                numeric: true,
                buttonCount: 5

            },
            dataType: 'json',
            columns: [
                {
                    field: "Persona.Nombres",
                    title: "PERSONA",
                    width: "50px"
                },
                {
                    field: "CuentaUsuario",
                    title: "CUENTA USUARIO",
                    width: "50px"
                },
                {
                    field: "CorreoUsuario",
                    title: "CORREO USUARIO",
                    width: "50px"
                },
                {
                    field: "CorreoUsuarioValido",
                    title: "CORREO USUARIO VALIDO",
                    attributes: { style: "text-align:right;" },
                    template: function (item) {
                        var str = '';
                        if(item.CorreoUsuarioValido != null) str = item.CorreoUsuarioValido ? 'SI' : 'NO';
                        return str;
                    },
                    width: "50px"
                },
                {
                    field: "CuentaRed",
                    title: "CUENTA RED",
                    width: "50px"
                },
                {
                    field: "FechaCaducidad",
                    title: "FECHA CADUCIDAD",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        return controladorApp.mostrarFechaEnGrid(item.FechaCaducidad);
                    },
                    width: "50px"
                },
                {
                    field: "EsPrimeravez",
                    title: "ES PRIMERAVEZ",
                    attributes: { style: "text-align:right;" },
                    template: function (item) {
                        var str = '';
                        if(item.EsPrimeravez != null) str = item.EsPrimeravez ? 'SI' : 'NO';
                        return str;
                    },
                    width: "50px"
                },
                {
                    field: "Contrasenia",
                    title: "CONTRASEÑA",
                    width: "50px"
                },
                {
                    field: "ContraseniaSalt",
                    title: "CONTRASEÑA SALT",
                    width: "50px"
                },
                {
                    //HABILITAR INHABILITAR
                    title: 'ACTIVO',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";

                        if (item.RegistroEstaActivo) {
                            controles += '<button type="button" class="btn btn-default btn-xs" data-toggle="modal" data-target="#myModal" onclick="controlador.inactivar(\'' + item.uid + '\')">';
                            controles += '<span class="glyphicon glyphicon glyphicon-check" aria-hidden="true" data-uib-tooltip="Inhabilitar registro activo"></span>';
                            controles += '</button>';
                        }
                        else {
                            controles += '<button type="button" class="btn btn-default btn-xs" data-toggle="modal" data-target="#myModal" onclick="controlador.activar(\'' + item.uid + '\')">';
                            controles += '<span class="glyphicon glyphicon glyphicon-unchecked" aria-hidden="true" data-uib-tooltip="Habilitar registro inactivo"></span>';
                            controles += '</button>';
                        }
                        return controles;
                    },
                    width: '60px'
                    }, {
                   //DETALLE
                   title: '',
                   attributes: { style: "text-align:center;" },
                   template: function (item) {
                       var controles = "";

                       controles += '<button type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#myModal" onclick="controlador.abrirModalDetalle(\'' + item.uid + '\')">';
                       controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Detalle"></span>';
                       controles += '</button>';

                       return controles;
                   },
                   width: '40px'
               }, {
                   //EDITAR
                   title: '',
                   attributes: { style: "text-align:center;" },
                   template: function (item) {
                       var controles = "";

                       controles += '<button type="button" class="btn btn-primary btn-xs" onclick="controlador.abrirModalEdicion(\'' + item.uid + '\')">';
                       controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar"></span>';
                       controles += '</button>';

                       return controles;
                   },
                   width: '40px'
               }, {
                   //ELIMINAR
                   title: '',
                   attributes: { style: "text-align:center;" },
                   template: function (item) {
                       var controles = "";

                       controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.abrirModalEliminacion(\'' + item.uid + '\')">';
                       controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar"></span>';
                       controles += '</button>';

                       return controles;
                   },
                   width: '40px'
               }
            ]
        }).data();
    };

    this.UsuarioJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
    };

    this.UsuarioJS.prototype.abrirModalNuevo = function () {
        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.title("Nuevo Usuario");
        modal.open();
    }

    this.UsuarioJS.prototype.cerrarModalNuevo = function () {
        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.close();
    }

    this.UsuarioJS.prototype.registrar = function (e) {
        e.preventDefault();

        if (frmNuevoValidador.validate()) {
            
            var modal = $('#divModalNuevo').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();
            data_param.append('IdPersona', $("#ddlIdPersona_nuevo").data("kendoDropDownList").value());
            data_param.append('CuentaUsuario', $("#txtCuentaUsuario_nuevo").val());
            data_param.append('CorreoUsuario', $("#txtCorreoUsuario_nuevo").val());
            data_param.append('CorreoUsuarioValido', $("#chkCorreoUsuarioValido_nuevo").is(':checked'));
            data_param.append('CuentaRed', $("#txtCuentaRed_nuevo").val());
            data_param.append('FechaCaducidad', $("#txtFechaCaducidad_nuevo").data("kendoDatePicker").value());
            data_param.append('EsPrimeravez', $("#chkEsPrimeravez_nuevo").is(':checked'));
            data_param.append('Contrasenia', $("#txtContrasenia_nuevo").val());
            data_param.append('ContraseniaSalt', $("#txtContraseniaSalt_nuevo").val());
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Usuario/Validar',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {

                    if (res.length > 0) {
                        esValido = false;
                        for (var i = 0; i < res.length; i++) mensajeValidacion = (mensajeValidacion == '' ? '' : mensajeValidacion + '<br />') + res[i];
                    }

                    if (!esValido)
                        controladorApp.notificarMensajeDeAlerta(mensajeValidacion);
                    else {
                        controladorApp.abrirMensajeDeConfirmacion(
                            '¿Desea guardar los datos?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Usuario/Registrar',
                                    type: 'POST',
                                    dataType: 'json',
                                    contentType: false,
                                    processData: false,
                                    data: arg,
                                    success: function (res) {
                                        controladorApp.notificarMensajeSatisfactorio("Usuario registrado correctamente");
                                        modal.close();
                                        $('#divGrid').data("kendoGrid").dataSource.page(1);
                                    },
                                    error: function (res) {

                                    }
                                });
                            }, data_param);
                    }
                },
                error: function (res) {
                    debugger;
                }
            });
        }
    }

    this.UsuarioJS.prototype.abrirModalEdicion = function (uid) {
        var modal = $('#divModalEdicion').data('kendoWindow');
        var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        $('#hdnUid').val(uid);
        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Usuario/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
            if(res.IdPersona != null) $("#ddlIdPersona_edicion").data("kendoDropDownList").value(res.IdPersona);
            if(res.CuentaUsuario != null) $("#txtCuentaUsuario_edicion").val(res.CuentaUsuario);
            if(res.CorreoUsuario != null) $("#txtCorreoUsuario_edicion").val(res.CorreoUsuario);
            if(res.CorreoUsuarioValido != null){
                    if(res.CorreoUsuarioValido)
                        $("#chkCorreoUsuarioValido_edicion").prop("checked", true);
                    else
                        $("#chkCorreoUsuarioValido_edicion").prop("checked", false);
                    } 
            if(res.CuentaRed != null) $("#txtCuentaRed_edicion").val(res.CuentaRed);
            if(res.FechaCaducidad != null) $("#txtFechaCaducidad_edicion").data("kendoDatePicker").value(res.FechaCaducidad);
            if(res.EsPrimeravez != null){
                    if(res.EsPrimeravez)
                        $("#chkEsPrimeravez_edicion").prop("checked", true);
                    else
                        $("#chkEsPrimeravez_edicion").prop("checked", false);
                    } 
            if(res.Contrasenia != null) $("#txtContrasenia_edicion").val(res.Contrasenia);
            if(res.ContraseniaSalt != null) $("#txtContraseniaSalt_edicion").val(res.ContraseniaSalt);
                
                modal.open();
            },
            error: function (res) {
                debugger;
            }
        });


        
    }

    this.UsuarioJS.prototype.cerrarModalEdicion = function () {
        $('#divModalEdicion').data('kendoWindow').close();
    }

    this.UsuarioJS.prototype.guardar = function (e) {
        e.preventDefault();

        if (frmEdicionValidador.validate()) {

            var modal = $('#divModalEdicion').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());

            var data_param = new FormData();
            data_param.append('IdUsuario', dr.IdUsuario);
            data_param.append('IdPersona', $("#ddlIdPersona_edicion").data("kendoDropDownList").value());
            data_param.append('CuentaUsuario', $("#txtCuentaUsuario_edicion").val());
            data_param.append('CorreoUsuario', $("#txtCorreoUsuario_edicion").val());
            data_param.append('CorreoUsuarioValido', $("#chkCorreoUsuarioValido_edicion").is(':checked'));
            data_param.append('CuentaRed', $("#txtCuentaRed_edicion").val());
            data_param.append('FechaCaducidad', $("#txtFechaCaducidad_edicion").data("kendoDatePicker").value());
            data_param.append('EsPrimeravez', $("#chkEsPrimeravez_edicion").is(':checked'));
            data_param.append('Contrasenia', $("#txtContrasenia_edicion").val());
            data_param.append('ContraseniaSalt', $("#txtContraseniaSalt_edicion").val());

            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Usuario/Validar',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {

                    if (res.length > 0) {
                        esValido = false;
                        for (var i = 0; i < res.length; i++) mensajeValidacion = (mensajeValidacion == '' ? '' : mensajeValidacion + '<br />') + res[i];
                    }

                    if (!esValido)
                        controladorApp.notificarMensajeDeAlerta(mensajeValidacion);
                    else {
                        controladorApp.abrirMensajeDeConfirmacion(
                            '¿Desea guardar los datos?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Usuario/Guardar',
                                    type: 'POST',
                                    dataType: 'json',
                                    contentType: false,
                                    processData: false,
                                    data: arg,
                                    success: function (res) {
                                        controladorApp.notificarMensajeSatisfactorio("Usuario guardado correctamente");
                                        modal.close();
                                        $('#divGrid').data("kendoGrid").dataSource.page(1);
                                    },
                                    error: function (res) {

                                    }
                                });
                            }, data_param);
                    }
                },
                error: function (res) {
                    debugger;
                }
            });
        }
    }

    this.UsuarioJS.prototype.abrirModalDetalle = function (uid) {
        var modal = $('#divModalDetalle').data('kendoWindow');
        var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        $('#hdnUid').val(uid);
        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Usuario/ObtenerParaMostrar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
            $("#spnIdPersona_detalle").text(res.Persona.Nombres);
            $("#spnCuentaUsuario_detalle").text(res.CuentaUsuario);
            $("#spnCorreoUsuario_detalle").text(res.CorreoUsuario);
            if(res.CorreoUsuarioValido != null){
                        if(res.CorreoUsuarioValido)
                            $("#spnCorreoUsuarioValido_detalle").text('SI');
                        else
                            $("#spnCorreoUsuarioValido_detalle").text('NO');
                    } else $("#spnCorreoUsuarioValido_detalle").text('')
            $("#spnCuentaRed_detalle").text(res.CuentaRed);
            $("#spnFechaCaducidad_detalle").text(kendo.toString(kendo.parseDate(res.FechaCaducidad), 'dd/MM/yyyy'));
            if(res.EsPrimeravez != null){
                        if(res.EsPrimeravez)
                            $("#spnEsPrimeravez_detalle").text('SI');
                        else
                            $("#spnEsPrimeravez_detalle").text('NO');
                    } else $("#spnEsPrimeravez_detalle").text('')
            $("#spnContrasenia_detalle").text(res.Contrasenia);
            $("#spnContraseniaSalt_detalle").text(res.ContraseniaSalt);
                
                modal.open();
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.UsuarioJS.prototype.cerrarModalDetalle = function () {
        var modal = $('#divModalDetalle').data('kendoWindow');
        modal.close();
    }

    this.UsuarioJS.prototype.inactivar = function (uid) {
        var grilla = $('#divGrid').data("kendoGrid");
        var dr = grilla.dataSource.getByUid(uid);

        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);
        data_param.append('RegistroEstaActivo', false);

        controladorApp.abrirMensajeDeConfirmacion(
                '¿Esta seguro de desactivar el Usuario?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Usuario/GuardarEstaActivo',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            controladorApp.notificarMensajeSatisfactorio("Usuario desactivado correctamente");
                            grilla.dataSource.read();
                        }
                    });
                }
                , data_param);
    };

    this.UsuarioJS.prototype.activar = function (uid) {
        var grilla = $('#divGrid').data("kendoGrid");
        var dr = grilla.dataSource.getByUid(uid);

        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);
        data_param.append('RegistroEstaActivo', true);

        controladorApp.abrirMensajeDeConfirmacion(
                '¿Esta seguro de activar el Usuario?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Usuario/GuardarEstaActivo',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            controladorApp.notificarMensajeSatisfactorio("Usuario activado correctamente");
                            grilla.dataSource.read();
                        }
                    });
                }
                , data_param);
    };

    this.UsuarioJS.prototype.abrirModalEliminacion = function (uid) {
        $('#hdnUid').val(uid);
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        modal.title("Confirmar eliminación del Usuario " + dr.CuentaUsuario);

        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Usuario/ConfirmarEliminacion',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                if (res.permite)
                    $('#hEliminacion').html('¿Está seguro de eliminar el Usuario?')
                else
                    $('#hEliminacion').html('El Usuario no puede ser eliminado')

                $("#pMensaje").html(res.listaMensaje[0]);
                $("#btnEliminar").prop('disabled', !res.permite);
                modal.open();
            },
            error: function (res) {

            }
        });
    }

    this.UsuarioJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.UsuarioJS.prototype.eliminar = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var grilla = $('#divGrid').data("kendoGrid");
        var dr = grilla.dataSource.getByUid($('#hdnUid').val());

        var data_param = new FormData();
        data_param.append('IdUsuario', dr.IdUsuario);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Usuario/Eliminar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                controladorApp.notificarMensajeSatisfactorio("Usuario eliminado correctamente");
                grilla.dataSource.page(1);
                modal.close();
            },
            error: function (res) {

            }
        });
    }

}(jQuery));