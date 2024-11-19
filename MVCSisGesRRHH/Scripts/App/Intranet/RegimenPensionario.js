(function ($) {
    var frmRegimenPensionarioValidador;
    var strMensajes = '';

    this.RegimenPensionarioJS = function () { };

    ////////////////////////////BANDEJA REGIMEN PENSIONARIO//////////////////////////////////
    this.RegimenPensionarioJS.prototype.inicializarVerBandejaRegimenPensionario = function () {
        debugger;
        $("#ddlAnio_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "- Seleccione -",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMes_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "- Seleccione -",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        //VENTANA MODAL PARA ADICIONAR Y EDITAR
        $("#ddlRegimenPensionario").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodRegimenPen",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarRegimenPensionario",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlAfps").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodAFP",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarAfps",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlTipoRegimenPen").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodTipoRegimenPen",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarTipoRegimenPensionario",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        $("#ddlAnio_Regimen").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        $("#ddlMes_Regimen").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "RegimenPensionario/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        //$("#ddlAnioCAS_GenPlan").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "SELECCIONE",
        //    dataTextField: "Anio",
        //    dataValueField: "Anio",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        $('#divModalRegistroRegimenPensionario').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '75%',
            height: 'auto',
            title: 'Agregar Registro Regimen Pensionario',
            visible: false,
            position: { top: '5%', left: "5%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmRegimenPensionarioValidador.hideMessages();

                $("#hdIdRegistroRegimenPen").val('0');    //Nuevo Registro
                $('#ddlAnio_Regimen').data("kendoDropDownList").value('');
                $('#ddlMes_Regimen').data("kendoDropDownList").value('');
                $('#ddlRegimenPensionario').data("kendoDropDownList").value('');
                $('#ddlAfps').data("kendoDropDownList").value('');;
                $('#ddlTipoRegimenPen').data("kendoDropDownList").value('');;
                $("#txtRegimenAporte").val('');
                $("#txtRegimenPrimaSeguro").val('');
                $("#txtRegimenComision").val('');
                $("#txtRegimenTope").val('');

            }
        }).data("kendoWindow");

        $('#divModalCopiaRegimen').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '40%',
            height: 'auto',
            title: 'Confirmar Copia de Regimen Pensionario',
            visible: false,
            position: { top: '10%', left: "20%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        frmRegimenPensionarioValidador = $("#frmRegistroRegimenPensionario").kendoValidator().data("kendoValidator");

        var tt = new Date();
        var mm = tt.getMonth() + 1;
        //if (today.getMonth() > 1) var month = month - 1;
        var year = tt.getFullYear();

        $("#ddlAnio_busqueda").data("kendoDropDownList").value(year);
        $("#ddlMes_busqueda").data("kendoDropDownList").value(mm);

        //this.inicializarGrid();
        this.CargarBandejaPrincipal(event);

    }

    this.RegimenPensionarioJS.prototype.CargarBandejaPrincipal = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'RegimenPensionario/ListarRegistroRegimenPensionario',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    debugger;
                    if ($operation === "read") {
                        //data_param.iMes = '10';
                        //data_param.iAnio = '2020';
                        data_param.iMes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
                        data_param.iAnio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
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
            //change: function (e) {
            //    $("#lblTotal").html(this.total());
            //    debugger;
            //},
            schema: {
                //total: function (response) {
                //    //debugger;
                //    //var TotalDeRegistros = 0;
                //    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return response.length; // TotalDeRegistros;
                //},
                model: {
                    id: "iCodRegistroRegimenPen"
                }
            },
            //group: {
            //    field: "strOrgano", aggregates: [
            //       { field: "strOrgano", aggregate: "count" }
            //    ]
            //},
            //aggregate: [
            //        { field: "strOrgano", aggregate: "count" },
            //        { field: "strOrgano", aggregate: "count" }
            //]

        });
        debugger;
        this.$grid = $("#divGridPensiones").kendoGrid({
            toolbar: ["excel"],
            excel: {
                fileName: "Listado de Regimen Pensionario.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "iCodRegistroRegimenPen",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iCodRegimenPen",
                    title: "COD. REGIMEN",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },

                {
                    field: "iCodTipoRegimenPen",
                    title: "COD.TIPO REGIMEN",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodAPF",
                    title: "COD. AFP",
                    width: "200px",
                    hidden: true
                },
                {
                    field: "iMes",
                    title: "MES",
                    width: "300px",
                    hidden: true
                },
                {
                    field: "iAnio",
                    title: "AÑO",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "sNombreRegimenPen",
                    title: "REGIMEN PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                },
                {
                    field: "sNombre",
                    title: "NOMBRE AFP",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                },
                {
                    field: "sTipo",
                    title: "TIPO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcComision",
                    title: "COMISION FLUJO/MIXTA",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcPrimaSeguro",
                    title: "PRIMA SEGURO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcAporte",
                    title: "APORTE",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcTope",
                    title: "TOPE",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "sEstado",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    hidden: true
                },
                {
                    //ACTUALIZAR VALORES DE COMISION PARA REGISTRO DE REGIMEN PENSIONARIO SELECCIONADO
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalRegistroRegimenPensionario(\'' + item.iCodRegistroRegimenPen + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar Registro de Régimen Pensionario según periodo (aaaa/mm)"></span>';
                        controles += '</button>';

                        return controles;
                    },
                    width: '30px'
                }

            ]
        }).data();
        debugger;
    };

    function LimpiarModalRegistroRegimenPensionario() {
        //var today = new Date();
        //var month = today.getMonth()+1;
        ////if (today.getMonth() > 1) var month = month - 1;
        //var year = today.getFullYear();

        $("#hdIdRegistroRegimenPen").val(0);
        //$("#ddlAnio_busqueda").data("kendoDropDownList").value(year);
        //$("#ddlMes_busqueda").data("kendoDropDownList").value(month);
        //$("#ddlRegimenPensionario").data("kendoDropDownList").value('');
        //$("#ddlTipoRegimem").data("kendoDropDownList").value('');
        $("#txtRegimenAporte").val('0');
        $("#txtRegimenPrimaSeguro").val('0');
        $("#txtRegimenComision").val('0');
        $("#txtRegimenTope").val('0');
        $("#txtRegimenVigenciaTope").val('0');
    }

    this.RegimenPensionarioJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.RegimenPensionarioJS.prototype.agregarRegistroRegimenPensionario = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        var mensaje = '¿Está seguro de agregar el Regimen Pensionario?';
        var resultado = 'Regimen Pensionario registrado correctamente';

        debugger;

        if (frmRegimenPensionarioValidador.validate()) {
            var modal = $('#divModalRegistroRegimenPensionario').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();

            if ($("#hdIdRegistroRegimenPen").val() != 0) {
                data_param.append('iCodRegistroRegimenPen', $("#hdIdRegistroRegimenPen").val());
                metodo = 'Guardar';
                mensaje = '¿Está seguro de actualizar el Regimen Pensionario?';
                resultado = 'Regimen Pensionario actualizado correctamente';

            }

            debugger;
            data_param.append('iAnio', $("#ddlAnio_Regimen").data("kendoDropDownList").value());
            data_param.append('iMes', $("#ddlMes_Regimen").data("kendoDropDownList").value());
            data_param.append('iCodRegimenPen', $("#ddlRegimenPensionario").data("kendoDropDownList").value());
            data_param.append('iCodAFP', $("#ddlAfps").data("kendoDropDownList").value());
            data_param.append('iCodTipoRegimenPen', $("#ddlTipoRegimenPen").data("kendoDropDownList").value());
            //.append('IdGenero', $("#ddlPersonaSexo").data("kendoDropDownList").value());
            data_param.append('dcAporte', $("#txtRegimenAporte").val());
            data_param.append('dcComision', $("#txtRegimenComision").val());
            data_param.append('dcPrimaSeguro', $("#txtRegimenPrimaSeguro").val());
            data_param.append('dcTope', $("#txtRegimenTope").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'RegimenPensionario/' + metodo,
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            debugger;
                            if (res.success == 'False') {
                                controladorApp.notificarMensajeDeAlerta(res.responseText);
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio(resultado);

                                // REFRESCAR INFORMACION DEL REGIMEN PENSIONARIO
                                $("#hdIdRegistroRegimenPen").val(res.responseText);

                                controlador.CargarFormularioRegistroRegimenPen(res.responseText);

                                modal.title("Actualizar Regimen Pensionario");
                                modal.close();
                                $("#btnBuscarRegimen").click();
                                $('#divGrid').data("kendoGrid").dataSource.page(1);

                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
    };

    this.RegimenPensionarioJS.prototype.abrirModalRegistroRegimenPensionario = function (id) {
        var modal = $('#divModalRegistroRegimenPensionario').data('kendoWindow');

        LimpiarModalRegistroRegimenPensionario();

        if (id == 0) {
            //$("#btnGuardarContacto").hide();
            //if ($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) {
            //}
            //else {
            //}

            var vHoy = new Date();
            var mes = vHoy.getMonth() + 1;
            //if (vHoy.getMonth() > 1) var mes = mes - 1;
            var year = vHoy.getFullYear();
            debugger;

            $("#ddlAnio_Regimen").data("kendoDropDownList").value(year);
            $("#ddlMes_Regimen").data("kendoDropDownList").value(mes);
            $("#ddlAnio_Regimen").data("kendoDropDownList").enable(false);
            $("#ddlMes_Regimen").data("kendoDropDownList").enable(false);
            $('#ddlRegimenPensionario').data("kendoDropDownList").enable(true);
            $('#ddlAfps').data("kendoDropDownList").enable(true);
            $('#ddlTipoRegimenPen').data("kendoDropDownList").enable(true);
            $('#txtRegimenAporte').prop('readonly', false);
            $('#txtRegimenComision').prop('readonly', false);
            $('#txtRegimenPrimaSeguro').prop('readonly', false);
            $('#txtRegimenTope').prop('readonly', false);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");

            modal.title("Agregar Registro Regimen Pensionario");
            modal.open().center();
        }
        else {

            $("#hdIdRegistroRegimenPen").val(id);

            debugger;
            controlador.CargarFormularioRegistroRegimenPen(id);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");

            modal.title("Actualizar Registro Regimen Pensionario");
            modal.open().center();
        }
    }

    this.RegimenPensionarioJS.prototype.CargarFormularioRegistroRegimenPen = function (id) {
        debugger;
        var data_param = new FormData();
        data_param.append('iCodRegistroRegimenPen', id);
        //$('#btnBuscarP').prop('disabled', false);
        // $('#hdIdRegistroRegimenPen').val('');

        //Combos de Seleccion
        $('#ddlAnio_Regimen').data("kendoDropDownList").enable(false);
        $('#ddlMes_Regimen').data("kendoDropDownList").enable(false);
        $('#ddlRegimenPensionario').data("kendoDropDownList").enable(true);
        $('#ddlAfps').data("kendoDropDownList").enable(true);
        $('#ddlTipoRegimenPen').data("kendoDropDownList").enable(true);
        //Casillero de Texto
        $('#txtRegimenAporte').prop('readonly', true);
        $('#txtRegimenComision').prop('readonly', true);
        $('#txtRegimenPrimaSeguro').prop('readonly', true);
        $('#txtRegimenTope').prop('readonly', true);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'RegimenPensionario/ObtenerRegimenParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#ddlAnio_Regimen").data("kendoDropDownList").value(res.iAnio);
                $("#ddlMes_Regimen").data("kendoDropDownList").value(res.iMes);

                $("#ddlRegimenPensionario").data("kendoDropDownList").value(res.iCodRegimenPen);
                $("#ddlAfps").data("kendoDropDownList").value(res.iCodAFP);
                $("#ddlTipoRegimenPen").data("kendoDropDownList").value(res.iCodTipoRegimenPen);

                $("#txtRegimenAporte").val(res.dcAporte);
                $("#txtRegimenComision").val(res.dcComision);
                $("#txtRegimenPrimaSeguro").val(res.dcPrimaSeguro);
                $("#txtRegimenTope").val(res.dcTope);

                if (res.iCodRegimenPen == '1') {
                    $('#ddlAfps').data("kendoDropDownList").enable(true);
                    $('#ddlTipoRegimenPen').data("kendoDropDownList").enable(true);

                    $('#txtRegimenAporte').prop('readonly', false);
                    $('#txtRegimenComision').prop('readonly', false);
                    $('#txtRegimenPrimaSeguro').prop('readonly', false);
                    $('#txtRegimenTope').prop('readonly', false);

                }
                else {
                    $('#ddlAfps').data("kendoDropDownList").enable(false);
                    $('#ddlTipoRegimenPen').data("kendoDropDownList").enable(false);
                    $('#txtRegimenAporte').prop('readonly', false);
                }
                //if (res.IdCondicion == '5') {
                //    $("#divOrdenes").show();
                //    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
                //    $("#txtPersonaCargo").removeAttr("required");
                //    $("#txtRUC").attr("required", true);
                //}
                //else {
                //    $("#divOrdenes").hide();
                //    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
                //    $("#txtPersonaCargo").attr("required", true);
                //    $("#txtRUC").removeAttr("required");
                //}
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.RegimenPensionarioJS.prototype.cerrarModalRegistroRegimenPensionario = function () {
        var modal = $('#divModalRegistroRegimenPensionario').data('kendoWindow');
        modal.close();
    }

    this.RegimenPensionarioJS.prototype.abrirModalCopiarRegimen = function (e) {
        e.preventDefault();

        var modal= $('#divModalCopiaRegimen').data('kendoWindow');
        var vHoy = new Date();
        var mes = vHoy.getMonth() + 1;
            //if (vHoy.getMonth() > 1) var mes = mes - 1;
        var year = vHoy.getFullYear();
        debugger;

        //$("#ddlAnio_CopiaRegimen").data("kendoDropDownList").value(year);
        //$("#ddlMes_CopiaRegimen").data("kendoDropDownList").value(mes);

        modal.title("Copiar Regimen Pensionario Mes Anterior");
        modal.open().center();
     
    }

    this.RegimenPensionarioJS.prototype.cerrarModalCopiarRegimen = function () {
        var modal = $('#divModalCopiaRegimen').data('kendoWindow');
        modal.close();
    }

    this.RegimenPensionarioJS.prototype.copiaRegimen = function () {
        var modal = $('#divModalCopiaRegimen').data('kendoWindow');

        //$('#divGrid').data("kendoGrid").dataSource.page(1);

        var grilla = $('#divGridPensiones').data("kendoGrid");
        var idReg = $('#hdnUid').val();
        //var dr = grilla.dataSource.getByUid($('#hdnUid').val());
        var dr = grilla.dataSource.getByUid();

        debugger;
        var iAnio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
        var iMes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
        if (iMes== 1) {
            iMes = 13;
            iAnio = iAnio - 1;
        }
        var data_param = new FormData();
        data_param.append('iAnio', iAnio);
        data_param.append('iMes', iMes-1);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'RegimenPensionario/CopiarRegimen',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                if (res.success == 'False') {
                    controladorApp.notificarMensajeDeAlerta(res.responseText);
                }
                else {

                    controladorApp.notificarMensajeSatisfactorio("Copia masiva realizada correctamente");
                    modal.close();
                    // REFRESCAR INFORMACION DEL CONCEPTO
                    $("#btnBuscarConcepto").click();
                    $('#divGridPensiones').data("kendoGrid").dataSource.page(1);

                }

            },
            error: function (res) {

            }
        });
    }
}(jQuery));



