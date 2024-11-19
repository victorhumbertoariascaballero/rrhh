(function ($) {
    var frmConceptoTrabajadorValidador;
    var frmConceptoMasivoTrabajadorValidador;
    var strMensajes = '';
    var dataImportarConceptoMasivo = [];
    var dataImportarConceptoMasivoSubsidio = [];

    this.ConceptoFijoVariableJS = function () { };

    this.ConceptoFijoVariableJS.prototype.inicializarBandejaConceptoVariable = function () {
        var btnCerrar = $("#btnCerrarPlanilla").kendoButton().data("kendoButton").enable(false);
        var btnAgregarConcepto = $("#btnAgregarConcepto").kendoButton().data("kendoButton").enable(false);
        
        $("#ddlAnio_busqueda").kendoDropDownList({
            autoBind: true,
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
        $("#ddlPlanillaAperturada_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "-- Seleccione --",
            dataTextField: "vNombreCompletoPlanilla",
            dataValueField: "vCodigo",            
            cascadeFrom: "ddlMes_busqueda",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "ConceptoFijoVariable/ListarPlanillaEjecucionAperturada",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodPlanilla = 0;
                            data_param.iCodTipoPlanilla = 0;
                            data_param.iMes = $('#ddlMes_busqueda').data("kendoDropDownList").value();
                            data_param.iAnio = $('#ddlAnio_busqueda').data("kendoDropDownList").value();

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

                }
            },
            change: function (e) {
                var value = this.value();
                debugger;
                var btnCerrar = $("#btnCerrarPlanilla").kendoButton().data("kendoButton"); 
                var btnAgregarConcepto = $("#btnAgregarConcepto").kendoButton().data("kendoButton");
                if (value != "") {
                    var item = value.split('-');
                    var items = new Object();
                    items.IdPlanilla = item[0];
                    items.IdTipoPlanilla = item[1];
                    items.IdAnio = item[2];
                    items.IdMes = item[3];
                    items.IdDetPlanilla = item[4];
                    items.bEstadoRegAsistencia = item[5];
                    items.bEstadoDsctoFijoVariable = item[6];
                    if (items.bEstadoRegAsistencia == 1 && items.bEstadoDsctoFijoVariable == 0) {
                        btnCerrar.enable(true);
                        btnAgregarConcepto.enable(true);
                    }
                    else {
                        btnCerrar.enable(false);
                        btnAgregarConcepto.enable(false);
                        if (items.bEstadoRegAsistencia == 0) {
                            controladorApp.notificarMensajeDeAlerta("La fase de Asistencia y Permisos no se ha cerrado ");
                        }
                        if (items.bEstadoDsctoFijoVariable == 1) {
                            controladorApp.notificarMensajeDeAlerta("La fase de Dsctos Fijos y Variables se ha cerrado ");
                        }
                    }
                }
                else {
                    btnCerrar.enable(false);
                    btnAgregarConcepto.enable(false);
                }
            }
        });
        /* Carga de combos para la Ventana MODAL */
        $("#ddlAnio_planilla").kendoDropDownList({
            autoBind: true,
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
        $("#ddlMes_planilla").kendoDropDownList({
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
        $("#ddlPlanillaAperturada").kendoDropDownList({
            autoBind: false,
            optionLabel: "-- Seleccione --",
            dataTextField: "vNombreCompletoPlanilla",
            dataValueField: "vCodigo",
            cascadeFrom: "ddlMes_planilla",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "ConceptoFijoVariable/ListarPlanillaEjecucionAperturada",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodPlanilla = 0;
                            data_param.iCodTipoPlanilla = 0;
                            data_param.iMes = $('#ddlMes_planilla').data("kendoDropDownList").value();
                            data_param.iAnio = $('#ddlAnio_planilla').data("kendoDropDownList").value();

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

                }
            }
        });

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
            optionLabel: "--Seleccione--",
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
        
        $("#ddlConcepto").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "vConcepto",
            dataValueField: "iCodConcepto",
            cascadeFrom: "ddlSubTipoConcepto",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Concepto/ListarConceptoPorTipo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodTipoConcepto = $('#ddlTipoConcepto').data("kendoDropDownList").value();
                            data_param.iCodSubTipoConcepto = $('#ddlSubTipoConcepto').data("kendoDropDownList").value();

                        }

                        return $.toDictionary(data_param);
                    }

                }
            },
            change: function (e) {
                var value = this.value();
                
                if (value == 4) {
                    $("#divSubsidio").show();
                }
                else {
                    $("#divSubsidio").hide();
                }
            }
        });
       
        

        frmConceptoTrabajadorValidador = $("#frmRegistroConceptoTrabajador").kendoValidator().data("kendoValidator");
        

        var tt = new Date();
        var mm = tt.getMonth() + 1;
        //if (today.getMonth() > 1) var month = month - 1;
        var year = tt.getFullYear();

        $("#ddlAnio_busqueda").data("kendoDropDownList").value(year);
        $("#ddlMes_busqueda").data("kendoDropDownList").value(mm);
        //$("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value('1');
        $("#divSubsidio").hide();

        this.CargarGrillaConceptoTrabajador(event);
    };

    /* VENTAN MODAL AGREGAR CONCEPTO POR TRABAJADOR*/
    $('#divModalConceptoTrabajador').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Conceptos Fijo y Variables por Trabajador',
        visible: false,
        position: { top: '10%', left: "15%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmConceptoTrabajadorValidador.hideMessages();

            //$("#hdIdConcepto").val('0');    //Nuevo Registro
            //$("#txtDNIConcepto").val('');
            //$("#txtNombreTrabajador").val('');
            //$("#txtMontoConcepto").val('0');
            //$('#ddlConcepto').data("kendoDropDownList").value('');
            //$('#ddlTipoConcepto').data("kendoDropDownList").value('');
            //$('#ddlSubTipoConcepto').data("kendoDropDownList").value('');
            //                
        }
    }).data("kendoWindow");

    $('#divModalConceptoMasivoTrabajador').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Conceptos Fijo y Variables por Trabajador',
        visible: false,
        position: { top: '10%', left: "15%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmConceptoTrabajadorValidador.hideMessages();

            //$("#hdIdConcepto").val('0');    //Nuevo Registro
            //$("#txtDNIConcepto").val('');
            //$("#txtNombreTrabajador").val('');
            //$("#txtMontoConcepto").val('0');
            //$('#ddlConcepto').data("kendoDropDownList").value('');
            //$('#ddlTipoConcepto').data("kendoDropDownList").value('');
            //$('#ddlSubTipoConcepto').data("kendoDropDownList").value('');
            //                
        }
    }).data("kendoWindow");

    this.ConceptoFijoVariableJS.prototype.CargarGrillaConceptoTrabajador = function (e) {
        e.preventDefault();
        var itemsParams = new Object();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/ListarConceptoVariable',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodTipoConcepto = $("#ddlTipoConcepto_busqueda").data("kendoDropDownList").value();
                        debugger;

                        var comboPlanilla = $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value();
                        var codPlanilla = comboPlanilla.split('-');
                        
                        itemsParams.IdPlanilla = codPlanilla[0];
                        itemsParams.IdTipoPlanilla = codPlanilla[1];
                        itemsParams.IdAnio = codPlanilla[2];
                        itemsParams.IdMes = codPlanilla[3];
                        itemsParams.IdDetPlanilla = codPlanilla[4];
                        itemsParams.bEstadoRegAsistencia = codPlanilla[5];
                        itemsParams.bEstadoDsctoFijoVariable = codPlanilla[6];


                        data_param.iAnio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
                        data_param.iMes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
                        data_param.iCodPlanilla = itemsParams.IdPlanilla;
                        data_param.iCodTipoPlanilla = itemsParams.IdTipoPlanilla;
                        data_param.iCodDetPlanilla = itemsParams.IdDetPlanilla;
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
            change: function (e) {
                $("#lblTotal").html(this.total());
                debugger;
            },
            schema: {
                //total: function (response) {
                //    //debugger;
                //    //var TotalDeRegistros = 0;
                //    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return response.length; // TotalDeRegistros;
                //},
                model: {
                    id: "vTrabajador"
                }
            },
            //group: {
            //    field: "vTrabajador", aggregates: [
            //       { field: "vTrabajador", aggregate: "count" }
            //    ]
            //},
            //aggregate: [
            //        { field: "vTrabajador", aggregate: "count" },
            //        { field: "vTrabajador", aggregate: "count" }
            //]
        });
        debugger;
        this.$grid = $("#divGridConceptosVariable").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Exportar Concepto Fijos y Variables.xlsx",
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
            filterable: true,
            columns: [
                {
                    field: "iNro",
                    title: "N°",
                    attributes: { style: "text-align:center;" },
                    width: "20px",
                    filterable: false
                },
                {
                    field: "iAnio",
                    title: "AÑO",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "iMes",
                    title: "MES",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "iCodTrabajador",
                    title: "COD. TRABAJADOR",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true 
                },
                {
                    field: "vTrabajador",
                    title: "TRABAJADOR",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },
                {
                    field: "vNroDocumento",
                    title: "N° DOCUMENTO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodPlanilla",
                    title: "COD.PLANILLA",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vNombrePlanilla",
                    title: "PLANILLA",
                    width: "60px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "iCodTipoPlanilla",
                    title: "COD.TIPO PLANILLA",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodDetPlanilla",
                    title: "COD.DET PLANILLA",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vTipoPlanilla",
                    title: "TIPO PLANILLA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "iCodConcepto",
                    title: "COD.CONCEPTO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vConcepto",
                    title: "CONCEPTO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    filterable: true
                },
                {
                    field: "iCodTipoConcepto",
                    title: "COD. TIPO CONCEPTO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vTipoConcepto",
                    title: "TIPO CONCEPTO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "iCodSubTipoConcepto",
                    title: "COD. SUB TIPO CONCEPTO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vSubTipoConcepto",
                    title: "SUB TIPO CONCEPTO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    filterable: false
                },
                {
                    field: "dMonto",
                    title: "MONTO",
                    width: "50px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" },
                    filterable: false
                },
                {
                    //ACTUALIZAR VALORES DE CONCEPTOS
                    title: 'EDITAR',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodTrabajador, item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio, item.iCodConcepto, item.iCodTipoConcepto, item.iCodSubTipoConcepto, item.iCodDetPlanilla];
                        debugger;
                        if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalConceptoTrabajador(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar Concepto Variable"></span>';
                            controles += '</button>';
                        }
                        

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //title: 'Eliminar',
                    title: 'ELIMINAR',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodTrabajador, item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio, item.iCodConcepto, item.iCodTipoConcepto, item.iCodSubTipoConcepto, item.iCodDetPlanilla];
                        if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.EliminarConceptoFijoVariable(\'' + items + '\')">';
                            //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.eliminar(\'' + item.iCodConcepto + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar Concepto"></span>';
                            controles += '</button>';
                            //controles += '</a>';
                        }
                        return controles;
                    },
                    width: '30px'
                },

            ]
        }).data();


        debugger;

    }

    

    //this.ConceptoFijoVariableJS.prototype.buscar = function (e) {
    //    e.preventDefault();

    //    var grilla = $('#divGridConceptosVariable').data("kendoGrid");
    //    grilla.dataSource._sort = undefined;
    //    grilla.dataSource.page(1);

    //    //$("#lblTotal").html(grilla.dataSource.total());
    //};

    this.ConceptoFijoVariableJS.prototype.abrirModalConceptoTrabajador = function (item) {
        var modal = $('#divModalConceptoTrabajador').data('kendoWindow');
        
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodTrabajador= _item[0];
            items.iCodPlanilla = _item[1];
            items.iCodTipoPlanilla = _item[2];
            items.iCodDetPlanilla = _item[8];
        }
        else {
            items.iCodTrabajador = item;
            items.iCodPlanilla = 0;
        }

        //LimpiarModalConcepto()
        debugger;

        if (items.iCodTrabajador == 0) {
            
            var tfecha = new Date();
            var mes = tfecha.getMonth() + 1;
            //if (today.getMonth() > 1) var month = month - 1;
            var aa = tfecha.getFullYear();

            $("#ddlAnio_planilla").data("kendoDropDownList").value(aa);
            $("#ddlMes_planilla").data("kendoDropDownList").value(mes);

            $('#ddlAnio_planilla').data("kendoDropDownList").enable(false);
            //$('#ddlMes_planilla').data("kendoDropDownList").enable(false);

            $("#txtNumeroDocumento").val('');
            $("#txtNombreTrabajador").val('');
            $("#txtMontoConcepto").val('0');
            $("#txtNroDiaSubsidio").val('0');
            //
            $("#hdIdConceptoTrabajador").val('0');

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");

            modal.title("Agregar Conceptos Fijo y Variables por Trabajador");
            modal.open().center();

        }
        else {

            //$("#hdIdConceptoTrabajador").val(id);

            $("#hdIdTrabajador").val(items.iCodTrabajador);
            $("#hdIdPlanilla").val(items.iCodPlanilla);
            $("#hdIdTipoPlanilla").val(items.iCodTipoPlanilla);
            $("#hdiCodDetPlanilla").val(items.iCodDetPlanilla);
            $("#hdIdConceptoTrabajador").val('1');
            debugger;
            controlador.CargarFormularioConcepto(item);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");

            modal.title("Actualizar Conceptos Fijo y Variables por Trabajador");
            modal.open().center();
        }
    }

 
    this.ConceptoFijoVariableJS.prototype.CargarFormularioConcepto = function (item) {
        debugger;
        var data_param = new FormData();

        var _item = item.split(',');
        
        data_param.append('iCodTrabajador', _item[0]);
        data_param.append('iCodPlanilla', _item[1]);
        data_param.append('iCodTipoPlanilla',_item[2]);
        data_param.append('iMes', _item[3]);
        data_param.append('iAnio', _item[4]);
        data_param.append('iCodConcepto', _item[5]);
        data_param.append('iCodTipoConcepto', _item[6]);
        data_param.append('iCodSubTipoConcepto', _item[7]);
        data_param.append('iCodDetPlanilla', _item[8]);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/ObtenerConceptoTrabajadorParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;

                if (res.iCodConcepto == 4) {
                    $("#divSubsidio").show();
                }
                else {
                    $("#divSubsidio").hide();
                }

                $("#ddlAnio_planilla").data("kendoDropDownList").value(res.iAnio);
                $("#ddlMes_planilla").data("kendoDropDownList").value(res.iMes);
                $("#ddlPlanillaAperturada").data("kendoDropDownList").value(res.vCodigoPlanil);
                $("#hdIdTrabajador").val(res.iCodTrabajador);
                $("#hdIdPlanilla").val(res.iCodPlanilla);
                $("#hdIdTipoPlanilla").val(res.iCodTipoPlanilla);
                $("#txtNumeroDocumento").val(res.vNroDocumento);
                $("#txtNombreTrabajador").val(res.vTrabajador);
                $("#ddlConcepto").data("kendoDropDownList").value(res.iCodConcepto);
                $("#ddlTipoConcepto").data("kendoDropDownList").value(res.iCodTipoConcepto);
                $("#ddlSubTipoConcepto").data("kendoDropDownList").value(res.iCodSubTipoConcepto);
                $("#txtMontoConcepto").val(res.dMonto);
                $("#txtNroDiaSubsidio").val(res.iDiaSubsidio);
                $("#hdIdConceptoTrabajador").val('1');
                $("#hdiCodDetPlanilla").val(res.iCodDetPlanilla);
               
            },
            error: function (res) {
                debugger;
            }
        });
    }


    this.ConceptoFijoVariableJS.prototype.agregarConceptoTrabajador = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        var mensaje = '¿Está seguro de agregar Concepto al Trabajador ?';
        var resultado = 'Concepto registrado correctamente';

        debugger;

        if (frmConceptoTrabajadorValidador.validate()) {
            var modal = $('#divModalConceptoTrabajador').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';
            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());

            var data_param = new FormData();

            if ($("#hdIdConceptoTrabajador").val() != 0) {
                data_param.append('iCodTrabajador', $("#hdIdTrabajador").val());

                metodo = 'Guardar';
                mensaje = '¿Está seguro de actualizar el Concepto?';
                resultado = 'Concepto actualizado correctamente';

            }

            debugger;

            var listaPlanilla = $("#ddlPlanillaAperturada").data("kendoDropDownList").value();
            var codPlanilla = listaPlanilla.split('-');
            $("#hdIdPlanilla").val(codPlanilla[0]);
            $("#hdIdTipoPlanilla").val(codPlanilla[1]);
            $("#hdiCodDetPlanilla").val(codPlanilla[4]);
            //
            //var items = [$("#hdIdTrabajador").val(), codPlanilla[0], codPlanilla[1], $("#ddlMes_planilla").val(), $("#ddlAnio_planilla").val(), $("#ddlConcepto").data("kendoDropDownList").value(), $("#ddlTipoConcepto").data("kendoDropDownList").value(), $("#ddlSubTipoConcepto").data("kendoDropDownList").value()];
            //  
                       
            if ($("#txtNombreTrabajador").val() == "") {
                controladorApp.notificarMensajeDeAlerta("Falta buscar Nombre del Trabajador");
                return false;
            }

            if ($("#ddlConcepto").data("kendoDropDownList").value() == "4") {
                if ($("#txtNroDiaSubsidio").val() == "") {
                    controladorApp.notificarMensajeDeAlerta("Ingresar dias de Subsidio");
                    return false;
                }

                var nSubsidio = parseInt($("#txtNroDiaSubsidio").val());

                if (nSubsidio > 30) {
                    controladorApp.notificarMensajeDeAlerta("Ingresar menos de 30 dias");
                    return false;
                }

            }

            if ($("#hdIdTrabajador").val() == "0") {
            }

            data_param.append('iAnio', $("#ddlAnio_planilla").val());
            data_param.append('iMes', $("#ddlMes_planilla").val());
            data_param.append('iCodPlanilla', $("#hdIdPlanilla").val());
            data_param.append('iCodTipoPlanilla', $("#hdIdTipoPlanilla").val());
            data_param.append('iCodDetPlanilla', $("#hdiCodDetPlanilla").val());
            //
            data_param.append('iCodTrabajador', $("#hdIdTrabajador").val());
            data_param.append('iCodConcepto', $("#ddlConcepto").data("kendoDropDownList").value());
            data_param.append('iCodTipoConcepto', $("#ddlTipoConcepto").data("kendoDropDownList").value());
            data_param.append('iCodSubTipoConcepto', $("#ddlSubTipoConcepto").data("kendoDropDownList").value());
            data_param.append('dMonto', $("#txtMontoConcepto").val());
            data_param.append('iDiaSubsidio', $("#txtNroDiaSubsidio").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/' + metodo,
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

                                // REFRESCAR INFORMACION DEL CONCEPTO TRABAJADOR POR LA PLANILLA APERTURADA
                                //$("#hdIdConcepto").val(res.responseText);

                                //controlador.CargarFormularioConcepto(res.responseText);

                                modal.title("Actualizar Concepto del Trabajador");
                                modal.close();
                                $("#hdIdConceptoTrabajador").val('');
                                $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value(listaPlanilla);
                                $("#btnBuscarPlanillaAperturada").click();
                                $('#divGridConceptosVariable').data("kendoGrid").dataSource.page(1);

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

    this.ConceptoFijoVariableJS.prototype.cerrarModalConceptoTrabajador = function () {
        var modal = $('#divModalConceptoTrabajador').data('kendoWindow');
        frmConceptoTrabajadorValidador.hideMessages();

        $("#hdIdConceptoTrabajador").val('0');  
        $("#txtNumeroDocumento").val('');
        $("#txtNombreTrabajador").val('');
        $("#txtMontoConcepto").val('0');
        $("#txtNroDiaSubsidio").val('');
        
        $('#ddlConcepto').data("kendoDropDownList").value('');
        $('#ddlTipoConcepto').data("kendoDropDownList").value('');
        $('#ddlSubTipoConcepto').data("kendoDropDownList").value('');
        $('#ddlPlanillaAperturada').data("kendoDropDownList").value('');

        modal.close();
    }

    this.ConceptoFijoVariableJS.prototype.abrirModalConceptoMasivoTrabajador = function () {
        var modal = $('#divModalConceptoMasivoTrabajador').data('kendoWindow');
        frmConceptoMasivoTrabajadorValidador = $("#frmRegistroConceptoMasivoTrabajador").kendoValidator().data("kendoValidator");
        ///////////////////////////MASIVO
        $("#ddlAnio_planillaMasivo").kendoDropDownList({
            autoBind: true,
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
        $("#ddlMes_planillaMasivo").kendoDropDownList({
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
        $("#ddlPlanillaAperturadaMasivo").kendoDropDownList({
            autoBind: false,
            optionLabel: "-- Seleccione --",
            dataTextField: "vNombreCompletoPlanilla",
            dataValueField: "vCodigo",
            cascadeFrom: "ddlMes_planillaMasivo",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "ConceptoFijoVariable/ListarPlanillaEjecucionAperturada",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodPlanilla = 0;
                            data_param.iCodTipoPlanilla = 0;
                            data_param.iMes = $('#ddlMes_planillaMasivo').data("kendoDropDownList").value();
                            data_param.iAnio = $('#ddlAnio_planillaMasivo').data("kendoDropDownList").value();

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

                }
            },
            change: function (e) {
                var value = this.value();
                debugger;
                var btnGuardarAdminMasivo = $("#btnGuardarAdminMasivo").kendoButton().data("kendoButton");
                if (value != "") {
                    var item = value.split('-');
                    var items = new Object();
                    items.IdPlanilla = item[0];
                    items.IdTipoPlanilla = item[1];
                    items.IdAnio = item[2];
                    items.IdMes = item[3];
                    items.IdDetPlanilla = item[4];
                    items.bEstadoRegAsistencia = item[5];
                    items.bEstadoDsctoFijoVariable = item[6];
                    if (items.bEstadoRegAsistencia == 1 && items.bEstadoDsctoFijoVariable == 0) {                    
                        btnGuardarAdminMasivo.enable(true);
                    }
                    else {
                        btnGuardarAdminMasivo.enable(false);
                        if (items.bEstadoRegAsistencia == 0) {
                            controladorApp.notificarMensajeDeAlerta("La fase de Asistencia y Permisos no se ha cerrado ");
                        }
                        if (items.bEstadoDsctoFijoVariable == 1) {
                            controladorApp.notificarMensajeDeAlerta("La fase de Dsctos Fijos y Variables se ha cerrado ");
                        }                        
                    }

                }
                else {
                    btnCerrar.enable(false);
                }
            }
        });

        $("#ddlTipoConceptoMasivo").kendoDropDownList({
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
        $("#ddlSubTipoConceptoMasivo").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "vNombre",
            dataValueField: "iCodSubTipoConcepto",
            cascadeFrom: "ddlTipoConceptoMasivo",
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
                            data_param.iCodTipoConcepto = $('#ddlTipoConceptoMasivo').data("kendoDropDownList").value();

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

        $("#ddlConceptoMasivo").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "vConcepto",
            dataValueField: "iCodConcepto",
            cascadeFrom: "ddlSubTipoConceptoMasivo",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Concepto/ListarConceptoPorTipo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.iCodTipoConcepto = $('#ddlTipoConceptoMasivo').data("kendoDropDownList").value();
                            data_param.iCodSubTipoConcepto = $('#ddlSubTipoConceptoMasivo').data("kendoDropDownList").value();
                            var listaPlanilla = $("#ddlPlanillaAperturadaMasivo").data("kendoDropDownList").value();
                            var codPlanilla = listaPlanilla.split('-');
                            debugger;
                            if (codPlanilla[0]==1||codPlanilla[0]==2||codPlanilla[0]==3||codPlanilla[0]==4) {
                                data_param.bRegCAS = true;
                                data_param.bRegFunc = false;
                                data_param.bRegSeci = false;
                                data_param.bRegPracticantes = false;
                            }
                            if (codPlanilla[0] == 5) {
                                data_param.bRegCAS = false
                                data_param.bRegFunc = true;
                                data_param.bRegSeci = false;
                                data_param.bRegPracticantes = false;
                            }
                            if (codPlanilla[0] == 12) {
                                data_param.bRegCAS = false
                                data_param.bRegFunc = false;
                                data_param.bRegSeci = true;
                                data_param.bRegPracticantes = false;
                            }
                            if (codPlanilla[0] == 13) {
                                data_param.bRegCAS = false
                                data_param.bRegFunc = false;
                                data_param.bRegSeci = false;
                                data_param.bRegPracticantes = true;
                            }
                        }

                        return $.toDictionary(data_param);
                    }

                }
            },
            change: function (e) {
                var value = this.value();

                if (value == 4) {
                    $("#panelImportarSubsidio").show();
                    $("#panelImportarNormal").hide();
                }
                else {
                    $("#panelImportarSubsidio").hide();
                    $("#panelImportarNormal").show();
                }
            }
        });
        frmConceptoMasivoTrabajadorValidador = $("#frmRegistroConceptoMasivoTrabajador").kendoValidator().data("kendoValidator");
        //var items = new Object();
        //var iCodPlanilla = 
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodTrabajador = _item[0];
        //    items.iCodPlanilla = _item[1];
        //    items.iCodTipoPlanilla = _item[2];

        //}
        //else {
        //    items.iCodTrabajador = item;
        //    items.iCodPlanilla = 0;
        //}

        ////LimpiarModalConcepto()
        //debugger;

        //if (items.iCodTrabajador == 0) {
        $("#divAgregarConceptoMasivo").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Formato para importar Conceptos por Trabajador.xlsx",
                filterable: false
            },
            // los nombres de columnas deberian emparejar con el Excel
            columns: [
                { field: "DNI" },
                { field: "Nombre" },
                { field: "Monto" }
            ],
            dataSource: [
                { DNI: "XXX", Nombre: "Prueba", Monto: "0.00" }
            ]
        }).on('focusin', function (e) {
            // Get the position of the Grid.
            var offset = $(this).offset();
            // Create a textarea element which will act as a clipboard.
            var textarea = $("<textarea>");
            // Position the textarea on top of the Grid and make it transparent.
            textarea.css({
                position: 'absolute',
                opacity: 0,
                top: offset.top,
                left: offset.left,
                border: 'none',
                width: $(this).width(),
                height: $(this).height()
            })
                .appendTo('body')
                .on('paste', function () {
                    // Handle the paste event.
                    setTimeout(function () {
                        var value = $.trim(textarea.val());
                        var grid = $("#divAgregarConceptoMasivo").data("kendoGrid");
                        var rows = value.split('\n');

                        dataImportarConceptoMasivo = [];
                        for (var i = 0; i < rows.length; i++) {
                            var cells = rows[i].split('\t');
                            dataImportarConceptoMasivo.push({
                                DNI: cells[0],
                                Nombre: cells[1],
                                Monto: cells[2]
                            });
                        };

                        grid.dataSource.data(dataImportarConceptoMasivo);
                        //aler(data);
                    });
                }).on('focusout', function () {
                    // Remove the textarea when it loses focus.
                    $(this).remove();
                });
            // Focus the textarea.
            setTimeout(function () {
                textarea.focus();
            });
        });

        $("#divAgregarConceptoMasivoSubsidio").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Formato para importar Conceptos por Trabajador.xlsx",
                filterable: false
            },
            // los nombres de columnas deberian emparejar con el Excel
            columns: [
                { field: "DNI" },
                { field: "Nombre" },
                { field: "Monto" },
                { field: "DiasSubsidio" }
            ],
            dataSource: [
                { DNI: "XXX", Nombre: "Prueba", Monto: "0.00", DiasSubsidio: "0" }
            ]
        }).on('focusin', function (e) {
            // Get the position of the Grid.
            var offset = $(this).offset();
            // Create a textarea element which will act as a clipboard.
            var textarea = $("<textarea>");
            // Position the textarea on top of the Grid and make it transparent.
            textarea.css({
                position: 'absolute',
                opacity: 0,
                top: offset.top,
                left: offset.left,
                border: 'none',
                width: $(this).width(),
                height: $(this).height()
            })
                .appendTo('body')
                .on('paste', function () {
                    // Handle the paste event.
                    setTimeout(function () {
                        var value = $.trim(textarea.val());
                        var grid = $("#divAgregarConceptoMasivoSubsidio").data("kendoGrid");
                        var rows = value.split('\n');

                        dataImportarConceptoMasivoSubsidio = [];
                        for (var i = 0; i < rows.length; i++) {
                            var cells = rows[i].split('\t');
                            dataImportarConceptoMasivoSubsidio.push({
                                DNI: cells[0],
                                Nombre: cells[1],
                                Monto: cells[2],
                                DiasSubsidio: cells[3]
                            });
                        };

                        grid.dataSource.data(dataImportarConceptoMasivoSubsidio);
                        //aler(data);
                    });
                }).on('focusout', function () {
                    // Remove the textarea when it loses focus.
                    $(this).remove();
                });
            // Focus the textarea.
            setTimeout(function () {
                textarea.focus();
            });
        });

        var tfecha = new Date();
        var mes = tfecha.getMonth() + 1;
        //if (today.getMonth() > 1) var month = month - 1;
        var aa = tfecha.getFullYear();

        $("#ddlAnio_planillaMasivo").data("kendoDropDownList").value(aa);
        $("#ddlMes_planillaMasivo").data("kendoDropDownList").value(mes);

        //$('#ddlAnio_planillaMasivo').data("kendoDropDownList").enable(false);
        //$('#ddlMes_planilla').data("kendoDropDownList").enable(false);

        $("#txtNumeroDocumentoMasivo").val('');
        $("#txtNombreTrabajadorMasivo").val('');
        //$("#txtMontoConceptoMasivo").val('0');
        //$("#txtNroDiaSubsidioMasivo").val('0');
        //
        $("#hdIdConceptoTrabajadorMasivo").val('0');

        if ($("#btnGuardarAdminMasivo") != null) $("#btnGuardarAdminMasivo").text("Guardar");


       
        modal.title("Agregar Conceptos Fijo y Variables por Trabajador");
        modal.open().center();

        //}
        //else {

        //    //$("#hdIdConceptoTrabajador").val(id);

        //    $("#hdIdTrabajadorMasivo").val(items.iCodTrabajador);
        //    $("#hdIdPlanillaMasivo").val(items.iCodPlanilla);
        //    $("#hdIdTipoPlanillaMasivo").val(items.iCodTipoPlanilla);
        //    $("#hdIdConceptoTrabajadorMasivo").val('1');
        //    debugger;
        //    controlador.CargarFormularioConcepto(item);

        //    if ($("#btnGuardarAdminMasivo") != null) $("#btnGuardarAdminMasivo").text("Actualizar");

        //    modal.title("Actualizar Conceptos Fijo y Variables por Trabajador");
        //    modal.open().center();
        //}
    }

    this.ConceptoFijoVariableJS.prototype.cerrarModalConceptoMasivoTrabajador = function () {
        var modal = $('#divModalConceptoMasivoTrabajador').data('kendoWindow');
        frmConceptoMasivoTrabajadorValidador.hideMessages();

        $("#hdIdConceptoTrabajadorMasivo").val('0');
        $("#txtNumeroDocumentoMasivo").val('');
        $("#txtNombreTrabajadorMasivo").val('');
        //$("#txtMontoConceptoMasivo").val('0');
        //$("#txtNroDiaSubsidioMasivo").val('');

        $('#ddlConceptoMasivo').data("kendoDropDownList").value('');
        $('#ddlTipoConceptoMasivo').data("kendoDropDownList").value('');
        $('#ddlSubTipoConceptoMasivo').data("kendoDropDownList").value('');
        $('#ddlPlanillaAperturadaMasivo').data("kendoDropDownList").value('');

        modal.close();
    }

    this.ConceptoFijoVariableJS.prototype.agregarConceptoMasivoTrabajador = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        var mensaje = '¿Está seguro de agregar Concepto al Trabajador ?';
        var resultado = 'Concepto registrado correctamente';

        debugger;

        if (frmConceptoMasivoTrabajadorValidador.validate()) {
            var modal = $('#divModalConceptoMasivoTrabajador').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';
            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());

            var data_param = new FormData();

            //if ($("#hdIdConceptoMasivoTrabajador").val() != 0) {
            //    //data_param.append('iCodTrabajador', $("#hdIdTrabajador").val());

            //    metodo = 'Guardar';
            //    mensaje = '¿Está seguro de actualizar el Concepto?';
            //    resultado = 'Concepto actualizado correctamente';

            //}

            debugger;

            var listaPlanilla = $("#ddlPlanillaAperturadaMasivo").data("kendoDropDownList").value();
            var codPlanilla = listaPlanilla.split('-');
            $("#hdIdPlanillaMasivo").val(codPlanilla[0]);
            $("#hdIdTipoPlanillaMasivo").val(codPlanilla[1]);
            $("#hdiCodDetPlanillaMasivo").val(codPlanilla[4]);
            
            //
            //var items = [$("#hdIdTrabajador").val(), codPlanilla[0], codPlanilla[1], $("#ddlMes_planilla").val(), $("#ddlAnio_planilla").val(), $("#ddlConcepto").data("kendoDropDownList").value(), $("#ddlTipoConcepto").data("kendoDropDownList").value(), $("#ddlSubTipoConcepto").data("kendoDropDownList").value()];
            //  

            //if ($("#txtNombreTrabajador").val() == "") {
            //    controladorApp.notificarMensajeDeAlerta("Falta buscar Nombre del Trabajador");
            //    return false;
            //}

            if ($("#ddlConceptoMasivo").data("kendoDropDownList").value() == "4") {

                
                var gridTra = $("#divAgregarConceptoMasivoSubsidio").data().kendoGrid.dataSource.view();

                //if ($("#txtNroDiaSubsidio").val() == "") {
                //    controladorApp.notificarMensajeDeAlerta("Ingresar dias de Subsidio");
                //    return false;
                //}

                //var nSubsidio = parseInt($("#txtNroDiaSubsidio").val());

                //if (nSubsidio > 30) {
                //    controladorApp.notificarMensajeDeAlerta("Ingresar menos de 30 dias");
                //    return false;
                //}
                var file = '';
                for (i = 0; i < dataImportarConceptoMasivoSubsidio.length; i++) {
                    if (dataImportarConceptoMasivoSubsidio[i].DNI.length==8) {
                        if (dataImportarConceptoMasivoSubsidio[i].DiasSubsidio < 0 || dataImportarConceptoMasivoSubsidio[i].DiasSubsidio > 30) {
                            controladorApp.notificarMensajeDeAlerta("El usuario " + dataImportarConceptoMasivoSubsidio[i].DNI + ' - ' + dataImportarConceptoMasivoSubsidio[i].Nombre + "Debe tener al menos 1 día o máximo 30 dias de subsudio");
                            return false;
                        }
                        else {
                            if (parseFloat(dataImportarConceptoMasivoSubsidio[i].Monto) < 0) {
                                controladorApp.notificarMensajeDeAlerta("El usuario " + dataImportarConceptoMasivoSubsidio[i].DNI + ' - ' + dataImportarConceptoMasivoSubsidio[i].Nombre + "Debe ingresar un monto mayor o igual a 0");
                                return false;
                            }
                            else {
                                fila = dataImportarConceptoMasivoSubsidio[i].DNI + '|' + dataImportarConceptoMasivoSubsidio[i].Monto + '|' + dataImportarConceptoMasivoSubsidio[i].DiasSubsidio;
                                data_param.append('formatos[' + i + ']', fila);
                            }

                        }
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta("El usuario " + dataImportarConceptoMasivoSubsidio[i].DNI + ' - ' + dataImportarConceptoMasivoSubsidio[i].Nombre + "Es incorrecto");
                        return false;
                    }                    
                }
            }
            else {
                var file = '';
                for (i = 0; i < dataImportarConceptoMasivo.length; i++) {
                    if (dataImportarConceptoMasivo[i].DNI.length == 8) {
                        if (parseFloat(dataImportarConceptoMasivo[i].Monto) < 0) {
                            controladorApp.notificarMensajeDeAlerta("El usuario " + dataImportarConceptoMasivo[i].DNI + ' - ' + dataImportarConceptoMasivo[i].Nombre + "Debe ingresar un monto mayor o igual a 0");
                            return false;
                        }
                        else {
                            fila = dataImportarConceptoMasivo[i].DNI + '|' + dataImportarConceptoMasivo[i].Monto;
                            data_param.append('formatos[' + i + ']', fila);
                        }
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta("El usuario " + dataImportarConceptoMasivo[i].DNI + ' - ' + dataImportarConceptoMasivo[i].Nombre + "Es incorrecto");
                        return false;
                    }
                }
            }

            if ($("#hdIdTrabajador").val() == "0") {
            }

            data_param.append('iAnio', $("#ddlAnio_planillaMasivo").val());
            data_param.append('iMes', $("#ddlMes_planillaMasivo").val());
            data_param.append('iCodPlanilla', $("#hdIdPlanillaMasivo").val());
            data_param.append('iCodTipoPlanilla', $("#hdIdTipoPlanillaMasivo").val());
            data_param.append('iCodDetPlanilla', $("#hdiCodDetPlanillaMasivo").val());
            //
            //data_param.append('iCodTrabajador', $("#hdIdTrabajador").val());
            data_param.append('iCodConcepto', $("#ddlConceptoMasivo").data("kendoDropDownList").value());
            data_param.append('iCodTipoConcepto', $("#ddlTipoConceptoMasivo").data("kendoDropDownList").value());
            data_param.append('iCodSubTipoConcepto', $("#ddlSubTipoConceptoMasivo").data("kendoDropDownList").value());
            //data_param.append('dMonto', $("#txtMontoConcepto").val());
            //data_param.append('iDiaSubsidio', $("#txtNroDiaSubsidio").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/' + metodo,
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
                                if (res.success == 'True') {
                                    controladorApp.notificarMensajeSatisfactorio(resultado);
                                }
                                else {
                                    //controladorApp.notificarMensajeDeAlerta("Se ha realizado los registros, pero los siguientes nro(s) de DNI no han sido cargados porque no existen: " + res);
                                    var mensaje2 = "Se ha realizado los registros, pero los siguientes nro(s) de DNI no han sido cargados porque no existen, Desea descargar el listado?";                                    
                                    controladorApp.abrirMensajeDeConfirmacion(
                                                        mensaje2, 'SI', 'NO'
                                                        , function (arg) {
                                                            //var data = $(this).data('href');
                                                            //NProgress.start();
                                                            //$.ajax({
                                                            //    //url: '<a href="' + controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/DescargarExportableTXT/?file=' + res,
                                                            //    url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/DescargarExportableTXT',
                                                            //    type: 'POST',
                                                            //    dataType: 'json',
                                                            //    contentType: false,
                                                            //    processData: false,
                                                            //    data: arg,
                                                            //    async: true,
                                                            //    cache: false,
                                                            //    success: function (response) {
                                                            //        debugger;
                                                            //        controladorApp.notificarMensajeSatisfactorio("Se ha descargado correctamente");
                                                            //        //location.reload();
                                                            //    },
                                                            //    error: function (res) {
                                                            //        //alert(res);
                                                            //    }
                                                            //});
                                                            //controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Bases/DescargarArchivo/?id=' + item.iCodBasePerfil + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-download-alt" title="Descargar bases de convocatoria"></span></a>';
                                                            var url = controladorApp.obtenerRutaBase() + "ConceptoFijoVariable/DescargarExportableTXT/?file=" + res;
                                                            window.location = url;
                                                            controladorApp.notificarMensajeSatisfactorio("Se ha descargado correctamente");
                                                        });
                                }
                                

                                // REFRESCAR INFORMACION DEL CONCEPTO TRABAJADOR POR LA PLANILLA APERTURADA
                                //$("#hdIdConcepto").val(res.responseText);

                                //controlador.CargarFormularioConcepto(res.responseText);

                                modal.title("Actualizar Concepto del Trabajador");
                                modal.close();
                                //$("#hdIdConceptoTrabajador").val('');
                                $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value(listaPlanilla);
                                $("#btnBuscarPlanillaAperturada").click();
                                $('#divGridConceptosVariable').data("kendoGrid").dataSource.page(1);

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

    this.ConceptoFijoVariableJS.prototype.EliminarConceptoFijoVariable = function (item) {
        debugger;


        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];
            items.iCodPlanilla = _item[1];
            items.iCodTipoPlanilla = _item[2];
            items.iMes = _item[3];
            items.iAnio = _item[4];
            items.iCodConcepto = _item[5];
            items.iCodDetPlanilla = _item[8];
        }        

        var data_param = new FormData();

        data_param.append('iCodTrabajador', items.iCodTrabajador);
        data_param.append('iMes', items.iMes);
        data_param.append('iAnio', items.iAnio);
        data_param.append('iCodPlanilla', items.iCodPlanilla);
        data_param.append('iCodTipoPlanilla', items.iCodTipoPlanilla);
        data_param.append('iCodConcepto', items.iCodConcepto);
        data_param.append('iCodDetPlanilla', items.iCodDetPlanilla);

        debugger;
        
        controladorApp.abrirMensajeDeConfirmacion(
        '¿Está seguro de eliminar el registro seleccionado?', 'SI', 'NO'
        , function (arg) {
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'ConceptoFijoVariable/Eliminar',
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
                        controladorApp.notificarMensajeSatisfactorio("El registro se ha eliminado correctamente");
                    }
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }, data_param);
        debugger;
    };
    this.ConceptoFijoVariableJS.prototype.CerrarFase = function (fase) {
        //e.preventDefault();

        debugger;
        var data_param = new FormData();
        var iMes = 0;
        var iAnio = 0;
        var arrayIdPlanilla = $('#ddlPlanillaAperturada_busqueda').data("kendoDropDownList").value();
        var item = arrayIdPlanilla.split('-');
        var iCodPlanilla = item[0];
        var iCodTipoPlanilla = item[1];
        var iAnio = item[2];
        var iMes = item[3];
        var iCodDetPlanilla = item[4];
        var bEstadoRegAsistencia = item[5];
        var bEstadoDsctoFijoVariable = item[6];
        var texto = "";
        if (fase == 2) {
            texto = '¿Está seguro de cerrar esta fase de Asistencias y Permisos?'
        }
        else {
            texto = '¿Está seguro de cerrar esta fase Conceptos Fijos y Variables?'
        }

        data_param.append('iCodPlanilla', iCodPlanilla);
        data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
        data_param.append('iAnio', iAnio);
        data_param.append('iMes', iMes);
        data_param.append('iCodDetPlanilla', iCodDetPlanilla);
        //data_param.append('bEstadoRegAsistencia', bEstadoRegAsistencia);
        //data_param.append('bEstadoDsctoFijoVariable', bEstadoDsctoFijoVariable);
        data_param.append('iCodFase', fase);
        controladorApp.abrirMensajeDeConfirmacion(
                            texto, 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Planilla/CerrarFase',
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
                                            controladorApp.notificarMensajeSatisfactorio("La fase de Asistencia y Permisos se ha cerrado correctamente");
                                            controlador.inicializarBandejaConceptoVariable();
                                        }
                                    },
                                    error: function (res) {
                                        //alert(res);
                                    }
                                });
                            }, data_param);


        debugger;
    };
}(jQuery));

