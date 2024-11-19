(function ($) {
    var frmConceptoValidador;
    var strMensajes = '';

    this.ConceptoJS = function () { };

    ////////////////////////////BANDEJA CONCEPTOS//////////////////////////////////
    this.ConceptoJS.prototype.inicializarVerBandejaConceptos = function () {
        debugger;
        //
        $("#ddlTipoConcepto_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "-- TODOS --",
            dataTextField: "vNombre",
            dataValueField: "iCodTipoConcepto",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Concepto/ListarTipoConcepto",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        //
        $("#ddlTipoConcepto").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodTipoConcepto",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Concepto/ListarTipoConcepto",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlSubTipoConcepto").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodSubTipoConcepto",
            cascadeFrom: "ddlTipoConcepto",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Concepto/ListarSubTipoConcepto",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodTipoConcepto = $('#ddlTipoConcepto').data("kendoDropDownList").value();
                            
                            //data_param.Grilla = {};
                            //data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                            //data_param.Grilla.PaginaActual = $options.page
                            //if ($options !== undefined && $options.sort !== undefined) {
                            //    data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                            //    data_param.Grilla.OrdenarPor = $options.sort[0].field;
                            //}
                        }

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        //
        $('#divModalConcepto').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Agregar Concepto',
            visible: false,
            position: { top: '10%', left: "15%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmConceptoValidador.hideMessages();

                $("#hdIdConcepto").val('0');    //Nuevo Registro

                $('#ddlTipoConcepto').data("kendoDropDownList").value('');
                $('#ddlSubTipoConcepto').data("kendoDropDownList").value('');
                //                
                $("#txtNombreConcepto").val('');
                $("#txtAbreviatura").val('');
                $("#txtCodigoExterno").val('');
                $("#txtCodigoMcpp").val('');
                $("#txtCodigoMEF").val('');
                $("#chkRegimenCAS").prop("checked", false);
                $("#chkRegimenFunc").prop("checked", false);
                $("#chkRegimenSeci").prop("checked", false);
                $("#chkConceptoBaseImp").prop("checked", false);
                $("#chkCalculoAutomatico").prop("checked", false);
            }
        }).data("kendoWindow");
        //
        /* CONSULTA */
        $('#divModalEliminacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '30%',
            height: 'auto',
            title: 'Confirmar eliminación',
            visible: false,
            position: { top: '10%', left: "20%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        //
        frmConceptoValidador = $("#frmConcepto").kendoValidator().data("kendoValidator");

        this.CargarBandejaPrincipal(event);


    }

    this.ConceptoJS.prototype.CargarBandejaPrincipal= function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Concepto/ListarConceptos',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {

                        data_param.iCodTipoConcepto = $("#ddlTipoConcepto_busqueda").data("kendoDropDownList").value();
                        //data_param.iCodTipoConcepto = '1';
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
                    id: "vConcepto"
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
        this.$grid = $("#divGridConceptos").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Bases de Convocatoria.xlsx",
            //    filterable: false
            //},
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
                    field: "vConcepto",
                    title: "CONCEPTO",
                    attributes: { style: "text-align:center;" },
                    width: "150px"
                },
                {
                    field: "vTipoConcepto",
                    title: "TIPO CONCEPTO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vSubTipoConcepto",
                    title: "SUB TIPO CONCEPTO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vRegCAS",
                    title: "REGIMEN CAS",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vRegFunc",
                    title: "REGIMEN FUNCIONARIO",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vRegSeci",
                    title: "REGIMEN SECIGRISTA",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vRegConceptoBaseImponible",
                    title: "CONCEPTO BASE IMPONIBLE",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vRegCalculoAutomatico",
                    title: "CONCEPTO CALCULO AUTO.",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vCodigoExterno",
                    title: "CODIGO PLAME",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vCodigoMCPP",
                    title: "CODIGO MCPP",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },                
                {
                    field: "vCodigoMEF",
                    title: "CODIGO MEF",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vClasificadorGasto",
                    title: "CLASIF. GASTO",
                    width: "25px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    //ACTUALIZAR VALORES DE CONCEPTOS
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalConcepto(\'' + item.iCodConcepto + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar Concepto"></span>';
                        controles += '</button>';

                        return controles;
                    },
                    width: '30px'
                },
                {
                        //INGRESAR DETALLE DE LA EVALUACION
                        //title: 'Eliminar',
                        title: '',
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalEliminacion(\'' + item.iCodConcepto + '\')">';
                            //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.eliminar(\'' + item.iCodConcepto + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar Concepto"></span>';
                            controles += '</button>';
                            //controles += '</a>';

                            return controles;
                        },
                        width: '30px'
                },
            ]
        }).data();
        debugger;
    }

    function LimpiarModalConcepto() {

        $("#hdIdConcepto").val(0);
        $("#ddlTipoConcepto").data("kendoDropDownList").value('');
        $("#txtNombreConcepto").val('');
        $("#txtAbreviatura").val('');
        $("#txtCodigoExterno").val('');
        $("#txtCodigoMcpp").val('');
        $("#txtCodigoMEF").val('');
        $('#txtNombreConcepto').prop('readonly', false);
        $('#txtAbreviatura').prop('readonly', false);
        $('#txtCodigoExterno').prop('readonly', false);
        $('#txtCodigoMcpp').prop('readonly', false);
        $('#txtCodigoMEF').prop('readonly', false);
        $('#txtClasificadorGasto').prop('readonly', false);
        //$("#chkRegimenCAS").val('0');
        //$("#chkRegimenFunc").val('0');
        //$("#chkRegimenSeci").val('0');
    }

    this.ConceptoJS.prototype.agregarConcepto = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        var mensaje = '¿Está seguro de agregar el Concepto?';
        var resultado = 'Concepto registrado correctamente';

        debugger;

        if (frmConceptoValidador.validate()) {
            var modal = $('#divModalConcepto').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';
            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();

            if ($("#hdIdConcepto").val() != 0) {
                data_param.append('iCodConcepto', $("#hdIdConcepto").val());
                metodo = 'Guardar';
                mensaje = '¿Está seguro de actualizar el Concepto?';
                resultado = 'Concepto actualizado correctamente';

            }

            debugger;
            data_param.append('iCodTipoConcepto', $("#ddlTipoConcepto").data("kendoDropDownList").value());
            data_param.append('iCodSubTipoConcepto', $("#ddlSubTipoConcepto").data("kendoDropDownList").value());
            data_param.append('vConcepto', $("#txtNombreConcepto").val());
            data_param.append('vAbreviatura', $("#txtAbreviatura").val());
            if ($("#txtCodigoExterno").val()!="") {
                data_param.append('vCodigoExterno', $("#txtCodigoExterno").val());
            }
            if ($("#txtCodigoMcpp").val() != "") {
                data_param.append('vCodigoMCPP', $("#txtCodigoMcpp").val());
            }
            if ($("#txtCodigoMEF").val() != "") {
                data_param.append('vCodigoMEF', $("#txtCodigoMEF").val());
            }
            if ($("#txtClasificadorGasto").val() != "") {
                data_param.append('vClasificadorGasto', $("#txtClasificadorGasto").val());
            }
            
            if ($("#chkRegimenCAS").prop("checked")) data_param.append('bRegCAS', true);
            else data_param.append('bRegCAS', false);
            if ($("#chkRegimenFunc").prop("checked")) data_param.append('bRegFunc', true);
            else data_param.append('bRegFunc', false);
            if ($("#chkRegimenSeci").prop("checked")) data_param.append('bRegSeci', true);
            else data_param.append('bRegSeci', false);
            if ($("#chkConceptoBaseImp").prop("checked")) data_param.append('bRegConceptoBaseImponible', true);
            else data_param.append('bRegConceptoBaseImponible', false);
            if ($("#chkCalculoAutomatico").prop("checked")) data_param.append('bRegCalculoAutomatico', true);
            else data_param.append('bRegCalculoAutomatico', false);

            //data_param.append('bRegCAS', $("#chkRegimenCAS").val());
            //data_param.append('bRegFunc', $("#chkRegimenFunc").val());
            //data_param.append('bRegSeci', $("#chkRegimenSeci").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Concepto/' + metodo,
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

                                // REFRESCAR INFORMACION DEL CONCEPTO
                                $("#hdIdConcepto").val(res.responseText);
                                controlador.CargarFormularioConcepto(res.responseText);
                                modal.title("Actualizar Concepto");
                                modal.close();

                               // $("#btnBuscarConcepto").click();
                                $('#divGridConceptos').data("kendoGrid").dataSource.page(1);

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

    this.ConceptoJS.prototype.abrirModalConcepto = function (id) {
        var modal = $('#divModalConcepto').data('kendoWindow');

        LimpiarModalConcepto()

        if (id == 0) {
            debugger;
            $('#ddlTipoConcepto').data("kendoDropDownList").enable(true);
            $('#ddlSubTipoConcepto').data("kendoDropDownList").enable(true);
            $('#txtNombreConcepto').prop('readonly', false);
            $('#txtAbreviatura').prop('readonly', false);
            $('#txtCodigoExterno').prop('readonly', false);
            $('#txtCodigoMcpp').prop('readonly', false);
            $('#txtCodigoMEF').prop('readonly', false); 
            $('#txtClasificadorGasto').prop('readonly', false);

            $('#chkRegimenCAS').prop('readonly', false);
            $('#chkRegimenFunc').prop('readonly', false);
            $('#chkRegimenSeci').prop('readonly', false);
            $('#chkConceptoBaseImp').prop('readonly', false);
            $('#chkCalculoAutomatico').prop('readonly', false);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");

            modal.title("Agregar Concepto");
            modal.open().center();
        }
        else {

            $("#hdIdConcepto").val(id);

            debugger;
            controlador.CargarFormularioConcepto(id);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");

            modal.title("Actualizar Concepto");
            modal.open().center();
        }
    }

    this.ConceptoJS.prototype.CargarFormularioConcepto = function (id) {
        debugger;
        var data_param = new FormData();
        data_param.append('iCodConcepto', id);
        //$('#btnBuscarP').prop('disabled', false);
 
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Concepto/ObtenerConceptoParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#ddlAnio_Regimen").data("kendoDropDownList").value(res.iAnio);
                //$("#ddlMes_Regimen").data("kendoDropDownList").value(res.iMes);

                $("#ddlTipoConcepto").data("kendoDropDownList").value(res.iCodTipoConcepto);
                $("#ddlSubTipoConcepto").data("kendoDropDownList").value(res.iCodSubTipoConcepto);
                $("#txtNombreConcepto").val(res.vConcepto);
                $("#txtAbreviatura").val(res.vAbreviatura);
                $("#txtCodigoExterno").val(res.vCodigoExterno);
                $("#txtCodigoMcpp").val(res.vCodigoMCPP);
                $("#txtCodigoMEF").val(res.vCodigoMEF);
                $('#txtClasificadorGasto').val(res.vClasificadorGasto);

                if (res.bRegCAS == true)
                    $("#chkRegimenCAS").prop("checked", true);
                else
                    $("#chkRegimenCAS").prop("checked", false);

                if (res.bRegFunc == true)
                    $("#chkRegimenFunc").prop("checked", true);
                else
                    $("#chkRegimenFunc").prop("checked", false);

                if (res.bRegSeci == true)
                    $("#chkRegimenSeci").prop("checked", true);
                else
                    $("#chkRegimenSeci").prop("checked", false);

                if (res.bRegConceptoBaseImponible == true)
                    $("#chkConceptoBaseImp").prop("checked", true);
                else
                    $("#chkConceptoBaseImp").prop("checked", false);
                if (res.bRegCalculoAutomatico == true)
                    $("#chkCalculoAutomatico").prop("checked", true);
                else
                    $("#chkCalculoAutomatico").prop("checked", false);

                //if (res.iCodTipoConcepto == '1') {

                //}
                //else {

                //}
               
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.ConceptoJS.prototype.cerrarModalConcepto = function () {
        var modal = $('#divModalConcepto').data('kendoWindow');
        modal.close();
    }

    this.ConceptoJS.prototype.abrirModalEliminacion = function (uid) {
        
        $('#hdnUid').val(uid);
        var modal = $('#divModalEliminacion').data('kendoWindow');
        //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        //var grilla = $('#divGrid').data("kendoGrid");
        //var dr = grilla.dataSource.getByUid($('#hdnUid').val());

        modal.title("Confirmar eliminación");
        modal.open().center();
    }

    this.ConceptoJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.ConceptoJS.prototype.eliminar = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');

        debugger;
        //$('#divGrid').data("kendoGrid").dataSource.page(1);

        var grilla = $('#divGridConceptos').data("kendoGrid");
        var idReg = $('#hdnUid').val();
        //var dr = grilla.dataSource.getByUid($('#hdnUid').val());
        var dr = grilla.dataSource.getByUid();

        debugger;

        var data_param = new FormData();
        data_param.append('iCodConcepto',  idReg);
        //data_param.append('iCodConcepto', dr.iCodConcepto);


        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Concepto/Eliminar',
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

                    controladorApp.notificarMensajeSatisfactorio("Registro eliminado correctamente");
                    modal.close();
                    // REFRESCAR INFORMACION DEL CONCEPTO
                    $("#btnBuscarConcepto").click();
                    $('#divGridConceptos').data("kendoGrid").dataSource.page(1);
 
                }

            },
            error: function (res) {

            }
        });
    }
}(jQuery));

