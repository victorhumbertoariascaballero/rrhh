(function ($) {
    var frmDescuentoJudicialValidador;
    var frmDescuentoJudicialTrabajadorValidador;
    var strMensajes = '';
    var frmBandejaDescuentoJudicial;

    var listaBeneficiario = [];

    this.DescuentoJudicialJS = function () { };


    this.DescuentoJudicialJS.prototype.inicializarBandejaDescuentoJudicial = function () {

        
        //var btnAgregarConcepto = $("#btnAgregarConcepto").kendoButton().data("kendoButton").enable(false);

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
            //change: function (e) {
            //    var value = this.value();
            //    debugger;
            //    //var btnCerrar = $("#btnCerrarPlanilla").kendoButton().data("kendoButton");
            //    var btnAgregarConcepto = $("#btnAgregarConcepto").kendoButton().data("kendoButton");
            //    if (value != "") {
            //        var item = value.split('-');
            //        var items = new Object();
            //        items.IdPlanilla = item[0];
            //        items.IdTipoPlanilla = item[1];
            //        items.IdAnio = item[2];
            //        items.IdMes = item[3];
            //        items.IdDetPlanilla = item[4];
            //        items.bEstadoRegAsistencia = item[5];
            //        items.bEstadoDsctoFijoVariable = item[6];
            //        items.bEstadoEjecutado = item[7];
            //        if (items.bEstadoRegAsistencia == 1 && items.bEstadoDsctoFijoVariable == 1 && items.bEstadoEjecutado == 1) {
            //            //btnCerrar.enable(true);
            //            btnAgregarConcepto.enable(true);
            //        }
            //        else {
            //            //btnCerrar.enable(false);
            //            btnAgregarConcepto.enable(false);
            //            if (items.bEstadoRegAsistencia == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La fase de Asistencia y Permisos no se ha cerrado ");
            //            }
            //            if (items.bEstadoDsctoFijoVariable == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La fase de Dsctos Fijos y Variables no se ha cerrado ");
            //            }
            //            if (items.bEstadoEjecutado == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La planilla aún no se ha generado ");
            //            }
            //        }
            //    }
            //    else {
            //        //btnCerrar.enable(false);
            //        btnAgregarConcepto.enable(false);
            //    }
            //}
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
            autoBind: true,
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
                            data_param.iCodDetPlanilla = 0;

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
            //change: function (e) {
            //    var value = this.value();
            //    debugger;
            //    //var btnCerrar = $("#btnCerrarPlanilla").kendoButton().data("kendoButton");
            //    var btnGuardarAdmin = $("#btnGuardarAdmin").kendoButton().data("kendoButton");
            //    if (value != "") {
            //        var item = value.split('-');
            //        var items = new Object();
            //        items.IdPlanilla = item[0];
            //        items.IdTipoPlanilla = item[1];
            //        items.IdAnio = item[2];
            //        items.IdMes = item[3];
            //        items.IdDetPlanilla = item[4];
            //        items.bEstadoRegAsistencia = item[5];
            //        items.bEstadoDsctoFijoVariable = item[6];
            //        items.bEstadoEjecutado = item[7];
            //        if (items.bEstadoRegAsistencia == 1 && items.bEstadoDsctoFijoVariable == 1 && items.bEstadoEjecutado == 1) {
            //            //btnCerrar.enable(true);
            //            btnGuardarAdmin.enable(true);
            //        }
            //        else {
            //            //btnCerrar.enable(false);
            //            btnGuardarAdmin.enable(false);
            //            if (items.bEstadoRegAsistencia == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La fase de Asistencia y Permisos no se ha cerrado ");
            //            }
            //            if (items.bEstadoDsctoFijoVariable == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La fase de Dsctos Fijos y Variables no se ha cerrado ");
            //            }
            //            if (items.bEstadoEjecutado == 0) {
            //                controladorApp.notificarMensajeDeAlerta("La planilla aún no se ha generado ");
            //            }
            //        }                   
            //    }
            //    else {
            //        //btnCerrar.enable(false);
            //        btnAgregarConcepto.enable(false);
            //    }
            //}
            //change: function (e) {
            //    var value1 = this.value();
            //    var vplanilla = value1.split('-');

            //    //$('#ddlPlanillaAperturada').data("kendoDropDownList").value(value1);
            //    $('#ddlAnio_planilla').data("kendoDropDownList").value(vplanilla[2]);
            //    $('#ddlMes_planilla').data("kendoDropDownList").value(vplanilla[3]);

            //}
        });
                
        $("#ddlPlanillaAperturada_carga").kendoDropDownList({
            autoBind: false,
            optionLabel: "-- Seleccione --",
            dataTextField: "vNombrePlanilla",
            dataValueField: "vCodigo",
            dataSource: {
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
            }
        });

        $("#ddlBancos").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "IdBanco",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarBancos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            },
            change: function (e) {
                var value = this.value();

                if (value > 1) {
                    $('#txtNumeroCuenta').prop('readonly', false);
                }
                else {
                    $('#txtNumeroCuenta').prop('readonly', true);
                }

            }

        });

        $("#ddlBancosJudicial").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "IdBanco",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarBancos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            },
            change: function (e) {
                var value = this.value();

                if (value > 1) {
                    $('#txtNumeroCuentaJudicial').prop('readonly', false);
                }
                else {
                    $('#txtNumeroCuentaJudicial').prop('readonly', true);
                }

            }

        });

        $("#ddlFormaPago").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "NomFormaPago",
            dataValueField: "CodFormaPago",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarFormaPago",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            }
        });

        $("#ddlFormaPagoJudicial").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "NomFormaPago",
            dataValueField: "CodFormaPago",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarFormaPago",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            }
        });

        $("#ddlTipoRetencion").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "NomTipoRetencion",
            dataValueField: "CodTipoRetencion",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarTipoRetencion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            },
            change: function (e) {
                var value = this.value();

                if (value > 1) {
                    //$('#divPorcentaje').show();
                    //$('#divMonto').hide();
                    $('#txtImporteFijo').val('');
                    $('#txtValorPorcentaje').prop('readonly', false);
                    $('#txtImporteFijo').prop('readonly', true);
                }
                else {
                    //$('#divPorcentaje').hide();
                    //$('#divMonto').show();
                    $('#txtValorPorcentaje').val('');
                    $('#txtImporteFijo').prop('readonly', false);
                    $('#txtValorPorcentaje').prop('readonly', true);
                }
            }
        });

        $("#ddlTipoRetencionJudicial").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "NomTipoRetencion",
            dataValueField: "CodTipoRetencion",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "DescuentoJudicial/ListarTipoRetencion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }

                }
            },
            change: function (e) {
                var value = this.value();

                if (value > 1) {
                    //$('#divPorcentaje').show();
                    //$('#divMonto').hide();
                    $('#txtImporteFijoJudicial').val('');
                    $('#txtValorPorcentajeJudicial').prop('readonly', false);
                    $('#txtImporteFijoJudicial').prop('readonly', true);
                }
                else {
                    //$('#divPorcentaje').hide();
                    //$('#divMonto').show();
                    $('#txtValorPorcentajeJudicial').val('');
                    $('#txtImporteFijoJudicial').prop('readonly', false);
                    $('#txtValorPorcentajeJudicial').prop('readonly', true);
                }
            }
        });

        
        frmDescuentoJudicialValidador = $("#frmRegistroJudicialTrabajador").kendoValidator().data("kendoValidator");
        
        var tt = new Date();
        var mm = tt.getMonth() + 1;
        //if (today.getMonth() > 1) var month = month - 1;
        var year = tt.getFullYear();

        $("#ddlAnio_busqueda").data("kendoDropDownList").value(year);
        $("#ddlMes_busqueda").data("kendoDropDownList").value(mm);
        $("#btnGenerarPlanilla").hide();
        
        //$('#divPorcentaje').hide();
        
        this.CargarGrillaDescuentoJudicial(event);

        frmDescuentoJudicialTrabajadorValidador = $("#frmRegistroJudicial").kendoValidator().data("kendoValidator");

        this.CargarGrillaDescuentoJudicialTrabajador(event);
        
    };

    this.DescuentoJudicialJS.prototype.CargarGrillaDescuentoJudicial = function (e) {
        e.preventDefault();
        var itemsParams = new Object();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ListarDescuentoJudicial',
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
                    id: "vTrabajador"
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
        this.$grid = $("#divGridDescuentoJudicial").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Exportar Descuento Judicial.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            dataType: 'json',
            columns: [
                {
                    field: "iNro",
                    title: "N°",
                    attributes: { style: "text-align:center;" },
                    width: "10px"
                },
                {
                    field: "iCodJudicial",
                    title: "COD. JUDICIAL",
                    width: "30px",
                    attributes: { style: "text-align:left;" },
                    hidden: true
                },
                {
                    field: "iAnio",
                    title: "AÑO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },

                },
                {
                    field: "iMes",
                    title: "MES",
                    width: "20px",
                    attributes: { style: "text-align:center;" },

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
                    width: "250px",
                    attributes: { style: "text-align:left;" }
                },
                {
                    field: "vNroDocumentoTrabajador",
                    title: "NRO.DOCUMENTO",
                    width: "150px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodPlanilla",
                    title: "COD.PLANILLA",
                    width: "10px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vNombrePlanilla",
                    title: "PLANILLA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoPlanilla",
                    title: "COD.TIPO PLANILLA",
                    width: "10px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vTipoPlanilla",
                    title: "TIPO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dMontoRetencionTotal",
                    title: "RETENCION JUDICIAL TOTAL",
                    width: "100px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    //ACTUALIZAR VALORES DE CONCEPTOS
                //    title: 'MODIFICAR',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        //var items = [item.iCodTrabajador, item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio];
                //        if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalDescuentoJudicial(\'' + item.iCodJudicial + '\')">';
                //            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar Descuento Judicial"></span>';
                //            controles += '</button>';
                //        }
                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //title: 'Eliminar',
                //    title: 'ELIMINAR',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodJudicial, item.vTrabajador];
                //        if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                //            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalEliminacion(\'' + items + '\')">';
                //            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar Descuento Judicial"></span>';
                //            controles += '</button>';
                //            //controles += '</a>';
                //        }
                //        return controles;
                //    },
                //    width: '30px'
                //},
            ]
        }).data();
        debugger;

    };
    function detailInit(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridGridDescuentoJudicialBeneficiarios").kendoGrid({
            //toolbar: ["excel"],
            //excel: {
            //    fileName: "Listado de postulantes.xlsx",
            //    filterable: false
            //},
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ListarBeneficiariosPlanilla',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodJudicial = e.data.iCodJudicial;                            
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
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "iCodJudicial",
                    title: "COD. JUDICIAL",
                    width: "30px",
                    attributes: { style: "text-align:left;" },
                    hidden: true
                },
                {
                    field: "vDniBeneficiario",
                    title: "Nª DNI",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNombreBeneficiario",
                    title: "BENEFICIARIO(A)",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },

                {
                    field: "iCodigoBanco",
                    title: "COD.BANCO",
                    width: "10px",
                    hidden: true,
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "vNombreBanco",
                    title: "BANCO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNumeroCuenta",
                    title: "NRO. CUENTA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoRetencion",
                    title: "COD. RETENCION",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreRetencion",
                    title: "TIPO RETENCION",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodFormaPago",
                    title: "COD. FORMA PAGO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreFormaPago",
                    title: "FORMA PAGO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dValorPorcentaje",
                    title: " (%) ",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "dMontoRetencion",
                    title: "MONTO",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },                
            ]
        }); //.data()
        
    }
    this.DescuentoJudicialJS.prototype.CargarGrillaDescuentoJudicialTrabajador = function (e) {
        debugger;
        e.preventDefault();
        var itemsParams = new Object();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ListarDescuentoJudicialTrabajadores',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodTipoConcepto = $("#ddlTipoConcepto_busqueda").data("kendoDropDownList").value();
                        debugger;

                        //var comboPlanilla = $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value();
                        //var codPlanilla = comboPlanilla.split('-');

                        //itemsParams.IdPlanilla = codPlanilla[0];
                        //itemsParams.IdTipoPlanilla = codPlanilla[1];
                        //itemsParams.IdAnio = codPlanilla[2];
                        //itemsParams.IdMes = codPlanilla[3];
                        //itemsParams.IdDetPlanilla = codPlanilla[4];
                        //itemsParams.bEstadoRegAsistencia = codPlanilla[5];
                        //itemsParams.bEstadoDsctoFijoVariable = codPlanilla[6];

                        //data_param.iAnio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
                        //data_param.iMes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
                        //data_param.iCodPlanilla = itemsParams.IdPlanilla;
                        //data_param.iCodTipoPlanilla = itemsParams.IdTipoPlanilla;
                        //data_param.iCodDetPlanilla = itemsParams.IdDetPlanilla;
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
                    id: "vTrabajador"
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
        this.$grid = $("#divGridDescuentoJudicialTrabajadores").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Exportar Descuento Judicial.xlsx",
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
                    field: "iNro",
                    title: "N°",
                    attributes: { style: "text-align:center;" },
                    width: "10px"
                },
                {
                    field: "iCodJudicial",
                    title: "COD. JUDICIAL",
                    width: "30px",
                    attributes: { style: "text-align:left;" },
                    hidden: true
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
                    width: "250px",
                    attributes: { style: "text-align:left;" }
                },
                {
                    field: "vNroDocumentoTrabajador",
                    title: "NRO.DOCUMENTO",
                    width: "150px",
                    attributes: { style: "text-align:center;" }
                },                
                {
                    //ACTUALIZAR VALORES DE CONCEPTOS
                    title: 'MODIFICAR',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //var items = [item.iCodTrabajador, item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio];
                        //if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalDescuentoJudicialTrabajador(\'' + item.iCodJudicial + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar Descuento Judicial"></span>';
                            controles += '</button>';
                        //}
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
                        var items = [item.iCodJudicial, item.vTrabajador];
                        //if (itemsParams.bEstadoRegAsistencia == 1 && itemsParams.bEstadoDsctoFijoVariable == 0) {
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalEliminacion(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar Descuento Judicial"></span>';
                            controles += '</button>';
                            //controles += '</a>';
                        //}
                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        debugger;

    }

    /* VENTAN MODAL AGREGAR DESCUENTO JUDICIAL POR TRABAJADOR*/
    $('#divModalDescuentoJudicial').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Retención Judicial por Trabajador',
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

    $('#divModalDescuentoJudicialTrabajador').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Trabajador',
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

    /* ELIMINACION */
    $('#divModalEliminacion').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '35%',
        height: 'auto',
        title: 'Confirmar eliminación',
        visible: false,
        position: { top: '10%', left: "20%" },
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    $('#divModalGenerarPlanilla').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Cargar Retenciones Judiciales a la Planilla del Mes',
        visible: false,
        position: { top: '10%', left: "20%" },
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.DescuentoJudicialJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGridDescuentoJudicial').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };


    function LimpiarModalDescuentoJudicial() {

        $('#ddlPlanillaAperturada').data("kendoDropDownList").value('');
        $("#ddlAnio_planilla").data("kendoDropDownList").value('');
        $("#ddlMes_planilla").data("kendoDropDownList").value('');
        $("#txtNumeroDocumentoTrabajador").val('');
        $("#txtNombreTrabajador").val('');
        //$("#txtMontoRetencionTotal").val('');
        //
        $("#txtNumeroDocumentoBeneficiario").val('');
        $("#txtNombreBeneficiario").val('');
        $("#ddlBancos").data("kendoDropDownList").value('');
        $("#txtNumeroCuenta").val('');
        $("#ddlTipoRetencion").data("kendoDropDownList").value('');
        $("#txtImporteFijo").val('');
        $("#txtValorPorcentaje").val('');
        $("#ddlFormaPago").data("kendoDropDownList").value('');
        //

    }

    function LimpiarModalDescuentoJudicialTrabajadores() {

        //$('#ddlPlanillaAperturada').data("kendoDropDownList").value('');
        //$("#ddlAnio_planilla").data("kendoDropDownList").value('');
        //$("#ddlMes_planilla").data("kendoDropDownList").value('');
        $("#txtNumeroDocumentoTrabajadorJudicial").val('');
        $("#txtNombreTrabajadorJudicial").val('');
        //$("#txtMontoRetencionTotal").val('');
        //
        $("#txtNumeroDocumentoBeneficiarioJudicial").val('');
        $("#txtNombreBeneficiarioJudicial").val('');
        $("#ddlBancosJudicial").data("kendoDropDownList").value('');
        $("#txtNumeroCuentaJudicial").val('');
        $("#ddlTipoRetencionJudicial").data("kendoDropDownList").value('');
        $("#txtImporteFijoJudicial").val('');
        $("#txtValorPorcentajeJudicial").val('');
        $("#ddlFormaPagoJudicial").data("kendoDropDownList").value('');
        //

    }

    this.DescuentoJudicialJS.prototype.LimpiarBeneficiarios= function() {

        $("#txtNumeroDocumentoBeneficiario").val('');
        $("#txtNombreBeneficiario").val('');
        $("#ddlBancos").data("kendoDropDownList").value('');
        $("#txtNumeroCuenta").val('');
        $("#ddlTipoRetencion").data("kendoDropDownList").value('');
        $("#txtImporteFijo").val('');
        $("#txtValorPorcentaje").val('');
        $("#ddlFormaPago").data("kendoDropDownList").value('');
        //

    }

    this.DescuentoJudicialJS.prototype.LimpiarBeneficiariosTrabajadores = function () {

        $("#txtNumeroDocumentoBeneficiarioJudicial").val('');
        $("#txtNombreBeneficiarioJudicial").val('');
        $("#ddlBancosJudicial").data("kendoDropDownList").value('');
        $("#txtNumeroCuentaJudicial").val('');
        $("#ddlTipoRetencionJudicial").data("kendoDropDownList").value('');
        $("#txtImporteFijoJudicial").val('');
        $("#txtValorPorcentajeJudicial").val('');
        $("#ddlFormaPagoJudicial").data("kendoDropDownList").value('');
        //

    }

    this.DescuentoJudicialJS.prototype.abrirModalDescuentoJudicial = function (id) {
        var modal = $('#divModalDescuentoJudicial').data('kendoWindow');

        LimpiarModalDescuentoJudicial();

        debugger;

        if (id == "0") {

            var tfecha = new Date();
            var mes = tfecha.getMonth() + 1;
            //if (today.getMonth() > 1) var month = month - 1;
            var aa = tfecha.getFullYear();

            //$('#ddlPlanillaAperturada').data("kendoDropDownList").value('');
            $("#ddlAnio_planilla").data("kendoDropDownList").value(aa);
            $("#ddlMes_planilla").data("kendoDropDownList").value(mes);
            /* Deshabilitar */
            $('#ddlAnio_planilla').data("kendoDropDownList").enable(false);
            //$('#ddlMes_planilla').data("kendoDropDownList").enable(false);            
            $('#txtValorPorcentaje').prop('readonly', true);
            $('#txtImporteFijo').prop('readonly', true);

            this.CargarGrillaBeneficiarios(-1);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");

            modal.title("Agregar Descuento Judicial por Trabajador");
            modal.open().center();
          
        }
        else {

            $("#hdIdJudicial").val(id);
            debugger;
            //controlador.CargarFormularioDescuentoJudicial(item);
            controlador.CargarFormularioDescuentoJudicial(id);
            this.CargarGrillaBeneficiarios(id);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");

            modal.title("Actualizar Descuento Judicial por Trabajador");
            modal.open().center();

        }
       
    }

    this.DescuentoJudicialJS.prototype.abrirModalDescuentoJudicialTrabajador = function (id) {
        var modal = $('#divModalDescuentoJudicialTrabajador').data('kendoWindow');

        LimpiarModalDescuentoJudicialTrabajadores();

        debugger;

        if (id == "0") {

            //var tfecha = new Date();
            //var mes = tfecha.getMonth() + 1;
            //if (today.getMonth() > 1) var month = month - 1;
            //var aa = tfecha.getFullYear();

            //$('#ddlPlanillaAperturada').data("kendoDropDownList").value('');
            //$("#ddlAnio_planilla").data("kendoDropDownList").value(aa);
            //$("#ddlMes_planilla").data("kendoDropDownList").value(mes);
            /* Deshabilitar */
            //$('#ddlAnio_planilla').data("kendoDropDownList").enable(false);
            ////$('#ddlMes_planilla').data("kendoDropDownList").enable(false);
            $('#txtValorPorcentajeJudicial').prop('readonly', true);
            $('#txtImporteFijoJudicial').prop('readonly', true);
            //this.CargarGrillaBeneficiarios(-1);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");

            modal.title("Agregar Trabajador");
            modal.open().center();

        }
        else {

            $("#hdIdJudicialTrabajador").val(id);
            debugger;
            //controlador.CargarFormularioDescuentoJudicial(item);
            controlador.CargarFormularioDescuentoJudicialTrabajador(id);
            this.CargarGrillaBeneficiariosTrabajadores(id);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");

            modal.title("Actualizar Descuento Judicial por Trabajador");
            modal.open().center();

        }

    }
    
    /* Editar de la GRILLA CABECERA PRINCIPAL */
    this.DescuentoJudicialJS.prototype.CargarFormularioDescuentoJudicial = function (id) {
        debugger;
        var data_param = new FormData();
        data_param.append('iCodJudicial', id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ObtenerDescuentoJudicialParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;

                $("#ddlAnio_planilla").data("kendoDropDownList").value(res.iAnio);
                $("#ddlMes_planilla").data("kendoDropDownList").value(res.iMes);
                $("#ddlPlanillaAperturada").data("kendoDropDownList").value(res.vCodigoPlanilla);
                $("#hdIdTrabajador").val(res.iCodTrabajador);
                $("#hdIdPlanilla").val(res.iCodPlanilla);
                $("#hdIdTipoPlanilla").val(res.iCodTipoPlanilla);
                $("#txtNumeroDocumentoTrabajador").val(res.vNroDocumentoTrabajador);
                $("#txtNombreTrabajador").val(res.vTrabajador);
                $("#hdTotalRetencion").val(res.dMontoRetencionTotal);
                //$("#hdIdJudicial").val(res.iCodJudicial);

            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.DescuentoJudicialJS.prototype.CargarFormularioDescuentoJudicialTrabajador = function (id) {
        debugger;
        var data_param = new FormData();
        data_param.append('iCodJudicial', id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ObtenerDescuentoJudicialTrabajadorParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;

                //$("#ddlAnio_planilla").data("kendoDropDownList").value(res.iAnio);
                //$("#ddlMes_planilla").data("kendoDropDownList").value(res.iMes);
                //$("#ddlPlanillaAperturada").data("kendoDropDownList").value(res.vCodigoPlanilla);
                //$("#hdIdTrabajador").val(res.iCodTrabajador);
                //$("#hdIdPlanilla").val(res.iCodPlanilla);
                //$("#hdIdTipoPlanilla").val(res.iCodTipoPlanilla);
                $("#txtNumeroDocumentoTrabajadorJudicial").val(res.vNroDocumentoTrabajador);
                $("#txtNombreTrabajadorJudicial").val(res.vTrabajador);
                //$("#hdTotalRetencion").val(res.dMontoRetencionTotal);
                //$("#hdIdJudicial").val(res.iCodJudicial);

            },
            error: function (res) {
                debugger;
            }
        });
    }

    /* Boton Agregar del Formulario Principal */
    this.DescuentoJudicialJS.prototype.agregarDescuentoJudicial = function (e) {
        e.preventDefault();

        var metodo = 'Registrar';
        var mensaje = '¿Está seguro de agregar la Retención Judicial del Trabajador ?';
        var resultado = 'Descuento Judicial registrado correctamente';

        debugger;

        if (frmDescuentoJudicialValidador.validate()) {
            var modal = $('#divModalDescuentoJudicial').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';
            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());

            var data_param = new FormData();
            var id = $("#hdIdJudicial").val();

            if (id != "0") {
                metodo = 'Guardar';
                mensaje = '¿Está seguro de actualizar la Retención Judicial ?';
                resultado = 'Descuento Judicial actualizado correctamente';

                /*Agregando parametro ID existente parfa EDICION*/
                $("#hdCambioDetalle").val('1');
                data_param.append('iCodJudicial', $("#hdIdJudicial").val());
            }

            debugger;

            /* Captura Codigo de la Planilla */
            var listaPlanilla = $("#ddlPlanillaAperturada").data("kendoDropDownList").value();
            var codPlanilla = listaPlanilla.split('-');
            $("#hdIdPlanilla").val(codPlanilla[0]);
            $("#hdIdTipoPlanilla").val(codPlanilla[1]); 
            $("#hdiCodDetPlanilla").val(codPlanilla[4]);
            /* VALIDACION */
            if ($("#txtNombreTrabajador").val() == "") {
                controladorApp.notificarMensajeDeAlerta("Falta buscar Nombre del Trabajador");
                return false;
            }

            data_param.append('iAnio', $("#ddlAnio_planilla").val());
            data_param.append('iMes', $("#ddlMes_planilla").val());
            data_param.append('iCodPlanilla', $("#hdIdPlanilla").val());
            data_param.append('iCodTipoPlanilla', $("#hdIdTipoPlanilla").val());
            data_param.append('iCodTrabajador', $("#hdIdTrabajador").val());
            data_param.append('vNroDocumentoTrabajador', $("#txtNumeroDocumentoTrabajador").val());
            data_param.append('iCodDetPlanilla', $("#hdiCodDetPlanilla").val());
            /* ------------------------*/            
            /*    Almacenar en Memoria */
            /* ------------------------*/
            var gridBenef = $("#divGridBeneficiarios").data().kendoGrid.dataSource.view();
            var sum=0;
            if (gridBenef.length > 0) {

                debugger;
                
                for (var i = 0; i < gridBenef.length; i++) {
                    fila = gridBenef[i].vDniBeneficiario + '|' + gridBenef[i].vNombreBeneficiario + '|' + gridBenef[i].iCodigoBanco + '|' + gridBenef[i].vNumeroCuenta + '|' + gridBenef[i].iCodTipoRetencion + '|' + gridBenef[i].iCodFormaPago + '|' + gridBenef[i].dValorPorcentaje + '|' + gridBenef[i].dMontoRetencion;
                    data_param.append('detBeneficiarios[' + i + ']', fila);
                    sum = sum + gridBenef[i].dMontoRetencion;
                }

                $("#hdTotalRetencion").val(sum);

            }
            else {

                $("#hdTotalRetencion").val('0');
                controladorApp.notificarMensajeDeAlerta("Falta lista de beneficiarios..");
                return false;

            }

            data_param.append('dMontoRetencionTotal', $("#hdTotalRetencion").val());
            data_param.append('iValidarCambio', $("#hdCambioDetalle").val());

            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/' + metodo,
                        
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

                                if (res.responseText == -1) {
                                    controladorApp.notificarMensajeDeAlerta("YA EXISTE REGISTRO DE LA RETENCION JUDICIAL...");
                                }
                                else{
                                    
                                    controladorApp.notificarMensajeSatisfactorio(resultado);

                                    // REFRESCAR INFORMACION DEL CONCEPTO TRABAJADOR POR LA PLANILLA APERTURADA
                                    //$("#hdIdConcepto").val(res.responseText);

                                    controlador.CargarFormularioDescuentoJudicial(res.responseText);

                                    modal.title("Actualizar Retención Judicial del Trabajador");
                                    modal.close();

                                    $("#hdIdJudicial").val('0');
                                    $("#txtNumeroDocumentoTrabajador").val('');
                                    $("#txtNombreTrabajador").val('');
                                    $("#txtMontoRetencionTotal").val('');
                                    $('#ddlPlanillaAperturada').data("kendoDropDownList").value('');

                                    $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value(listaPlanilla);
                                    $("#btnBuscarPlanillaAperturada").click();
                                    //$('#divGridDescuentoJudicial').data("kendoGrid").dataSource.page(1);
                                }

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

    this.DescuentoJudicialJS.prototype.agregarDescuentoJudicialTrabajador = function (e) {
        e.preventDefault();

        var metodo = 'Registrar_Nuevo';
        var mensaje = '¿Está seguro de agregar la Retención Judicial del Trabajador ?';
        var resultado = 'Descuento Judicial registrado correctamente';

        debugger;

        if (frmDescuentoJudicialTrabajadorValidador.validate()) {
            var modal = $('#divModalDescuentoJudicialTrabajador').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';
            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());

            var data_param = new FormData();
            var id = $("#hdIdJudicialTrabajador").val();

            if (id != "0") {
                metodo = 'Guardar_Nuevo';
                mensaje = '¿Está seguro de actualizar la Retención Judicial ?';
                resultado = 'Descuento Judicial actualizado correctamente';

                /*Agregando parametro ID existente parfa EDICION*/
                $("#hdCambioDetalleJudicial").val('1');
                data_param.append('iCodJudicial', $("#hdIdJudicialTrabajador").val());
            }

            debugger;

            /* Captura Codigo de la Planilla */
            //var listaPlanilla = $("#ddlPlanillaAperturada").data("kendoDropDownList").value();
            //var codPlanilla = listaPlanilla.split('-');
            //$("#hdIdPlanilla").val(codPlanilla[0]);
            //$("#hdIdTipoPlanilla").val(codPlanilla[1]);
            //$("#hdiCodDetPlanilla").val(codPlanilla[4]);
            /* VALIDACION */
            if ($("#txtNombreTrabajadorJudicial").val() == "") {
                controladorApp.notificarMensajeDeAlerta("Falta buscar Nombre del Trabajador");
                return false;
            }

            //data_param.append('iAnio', $("#ddlAnio_planilla").val());
            //data_param.append('iMes', $("#ddlMes_planilla").val());
            //data_param.append('iCodPlanilla', $("#hdIdPlanilla").val());
            //data_param.append('iCodTipoPlanilla', $("#hdIdTipoPlanilla").val());
            data_param.append('iCodTrabajador', $("#hdIdTrabajadorJudicial").val());
            data_param.append('vNroDocumentoTrabajador', $("#txtNumeroDocumentoTrabajadorJudicial").val());
            //data_param.append('iCodDetPlanilla', $("#hdiCodDetPlanilla").val());
            /* ------------------------*/
            /*    Almacenar en Memoria */
            /* ------------------------*/
            var gridBenef = $("#divGridBeneficiariosJudicial").data().kendoGrid.dataSource.view();
            var sum = 0;
            if (gridBenef.length > 0) {

                debugger;

                for (var i = 0; i < gridBenef.length; i++) {
                    fila = gridBenef[i].vDniBeneficiario + '|' + gridBenef[i].vNombreBeneficiario + '|' + gridBenef[i].iCodigoBanco + '|' + gridBenef[i].vNumeroCuenta + '|' + gridBenef[i].iCodTipoRetencion + '|' + gridBenef[i].iCodFormaPago + '|' + gridBenef[i].dValorPorcentaje + '|' + gridBenef[i].dMontoRetencion;
                    data_param.append('detBeneficiarios[' + i + ']', fila);
                    sum = sum + gridBenef[i].dMontoRetencion;
                }

                $("#hdTotalRetencionJudicial").val(sum);

            }
            else {

                $("#hdTotalRetencionJudicial").val('0');
                controladorApp.notificarMensajeDeAlerta("Falta lista de beneficiarios..");
                return false;

            }

            data_param.append('dMontoRetencionTotal', $("#hdTotalRetencionJudicial").val());
            data_param.append('iValidarCambio', $("#hdCambioDetalleJudicial").val());

            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/' + metodo,

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

                                if (res.responseText == -1) {
                                    controladorApp.notificarMensajeDeAlerta("YA EXISTE REGISTRO DE LA RETENCION JUDICIAL...");
                                }
                                else {

                                    controladorApp.notificarMensajeSatisfactorio(resultado);

                                    // REFRESCAR INFORMACION DEL CONCEPTO TRABAJADOR POR LA PLANILLA APERTURADA
                                    //$("#hdIdConcepto").val(res.responseText);

                                    controlador.CargarFormularioDescuentoJudicialTrabajador(res.responseText);

                                    modal.title("Actualizar Retención Judicial del Trabajador");
                                    modal.close();

                                    $("#hdIdJudicialTrabajador").val('0');
                                    $("#txtNumeroDocumentoTrabajadorJudicial").val('');
                                    $("#txtNombreTrabajadorJudicial").val('');
                                    //$("#txtMontoRetencionTotalJudicial").val('');
                                    //$('#ddlPlanillaAperturadaJudicial').data("kendoDropDownList").value('');

                                    //$("#ddlPlanillaAperturada_busquedaJudicial").data("kendoDropDownList").value(listaPlanilla);
                                    //$("#btnBuscarPlanillaAperturadaJudicial").click();
                                    //$('#divGridDescuentoJudicial').data("kendoGrid").dataSource.page(1);
                                }

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

    this.DescuentoJudicialJS.prototype.cerrarModalDescuentoJudicial = function () {
        var modal = $('#divModalDescuentoJudicial').data('kendoWindow');
        frmDescuentoJudicialValidador.hideMessages();

        $("#hdIdJudicial").val('0');
        $("#txtNroDocumentoTrabajador").val('');
        $("#txtNombreTrabajador").val('');
        $("#txtMontoRetencionTotal").val('');

        $('#ddlPlanillaAperturada').data("kendoDropDownList").value('');

        modal.close();
    }
    this.DescuentoJudicialJS.prototype.cerrarModalDescuentoJudicialTrabajadores = function () {
        var modal = $('#divModalDescuentoJudicialTrabajador').data('kendoWindow');
        frmDescuentoJudicialTrabajadorValidador.hideMessages();

        $("#hdIdJudicialTrabajador").val('0');
        $("#txtNumeroDocumentoTrabajadorJudicial").val('');
        $("#txtNombreTrabajadorJudicial").val('');
        //$("#txtMontoRetencionTotal").val('');

        //$('#ddlPlanillaAperturada').data("kendoDropDownList").value('');

        modal.close();
    }
    /* ELIMINAR DESCUENTO JUDICIAL */
    this.DescuentoJudicialJS.prototype.abrirModalEliminacion = function (item) {
        //$('#hdnUid').val(uid);
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var _item = item.split(',');

        $('#hdnUid').val(_item[0]);
        $("#lblTrabajador").html(_item[1]);

        modal.title("Confirmar eliminación");
        modal.open().center();
    }

    this.DescuentoJudicialJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.DescuentoJudicialJS.prototype.eliminar = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');

        debugger;
        //$('#divGrid').data("kendoGrid").dataSource.page(1);

        var grilla = $('#divGridDescuentoJudicial').data("kendoGrid");
        var idReg = $('#hdnUid').val();
        //var dr = grilla.dataSource.getByUid($('#hdnUid').val());
        var dr = grilla.dataSource.getByUid();

        debugger;

        var data_param = new FormData();
        data_param.append('iCodJudicial', idReg);
        //data_param.append('iCodConcepto', dr.iCodConcepto);


        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/Eliminar',
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
                    //$("#btnBuscarConcepto").click();
                    $('#divGridDescuentoJudicial').data("kendoGrid").dataSource.page(1);

                }

            },
            error: function (res) {

            }
        });
    }

    /* CARGAR DESCCUENTOS JUDICIAL A LA PLANILLA */
    this.DescuentoJudicialJS.prototype.abrirModalGenerarPlanillaJudicial = function () {
        //e.preventDefault();

        var modal = $('#divModalGenerarPlanilla').data('kendoWindow');

        var vHoy = new Date();
        var mes = vHoy.getMonth() + 1;
        //if (vHoy.getMonth() > 1) var mes = mes - 1;
        var year = vHoy.getFullYear();
        debugger;

        $("#ddlAnio_planilla").data("kendoDropDownList").value(year);
        $("#ddlMes_planilla").data("kendoDropDownList").value(mes);
        $("#ddlPlanillaAperturada").data("kendoDropDownList").value('');

        modal.title("Generar Planilla de Retencion Judicial");
        modal.open().center();


        this.CargarGrillaBeneficiarios(event);

    }

    this.DescuentoJudicialJS.prototype.cerrarModalGenerarPlanillaJudicial = function () {
        var modal = $('#divModalGenerarPlanilla').data('kendoWindow');
        modal.close();
    }

    this.DescuentoJudicialJS.prototype.cargarPlanillaJudicial = function () {
        var modal = $('#divModalGenerarPlanilla').data('kendoWindow');

        debugger;
        //$('#divGrid').data("kendoGrid").dataSource.page(1);
        var grilla = $('#divGridDescuentoJudicial').data("kendoGrid");
        var ds = grilla.dataSource;
        //var idReg = $('#hdnUid').val();
        //var dr = grilla.dataSource.getByUid($('#hdnUid').val());
        //var dr = grilla.dataSource.getByUid();

        /* Calcula si la grilla tiene registros*/
        var nTotReg = ds.total();

        if (nTotReg != 0) {
            debugger;

            var lstPlanilla = $("#ddlPlanillaAperturada_carga").data("kendoDropDownList").value();

            if (lstPlanilla == "" || lstPlanilla == null) {
                controladorApp.notificarMensajeDeAlerta("Seleccione Planilla del Mes");
                return false;
            }

            var vplanilla = lstPlanilla.split('-');
            $("#hdIdPlanilla").val(vplanilla[0]);
            $("#hdIdTipoPlanilla").val(vplanilla[1]);

            var data_param = new FormData();
            data_param.append('iCodPlanilla',  vplanilla[0]);
            data_param.append('iCodTipoPlanilla', vplanilla[1]);
            data_param.append('iAnio', vplanilla[2]);
            data_param.append('iMes', vplanilla[3]);
            data_param.append('iCodDetPlanilla', vplanilla[4]);

            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/CargarRetencionJudicial',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    if (res.success == 'False') {
                        controladorApp.notificarMensajeDeAlerta(res.responseText);
                    }
                    else {

                        if (res.responseText == -1) {
                            controladorApp.notificarMensajeDeAlerta("YA EXISTE CARGA DE RETENCIONES JUDICIALES");
                        }
                        else {

                            controladorApp.notificarMensajeSatisfactorio("Carga de la Retenciones Judiciales realizada correctamente");
                            modal.close();
                            // REFRESCAR INFORMACION DEL CONCEPTO
                            //$("#btnBuscarConcepto").click();
                            $('#divGridDescuentoJudicial').data("kendoGrid").dataSource.page(1);

                        }
                        
                    }

                },
                error: function (res) {

                }


            });
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No puede cargar descuentos a la PLANILLA, no existe registros.");
        }

    }

    /* DETALLE DE LOS BENEFICIARIOS */
    this.DescuentoJudicialJS.prototype.CargarGrillaBeneficiarios= function (id) {
       
        debugger;

        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ListarBeneficiarios',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodTipoConcepto = $("#ddlTipoConcepto_busqueda").data("kendoDropDownList").value();
                        debugger;

                        data_param.iCodJudicial = id;
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
                    id: "vNombreBeneficiario"
                }
            },

        });
        debugger;
        this.$grid = $("#divGridBeneficiarios").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Exportar Beneficiarios.xlsx",
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
                    field: "iCodJudicial",
                    title: "COD. JUDICIAL",
                    width: "30px",
                    attributes: { style: "text-align:left;" },
                    hidden: true
                },
                {
                    field: "vDniBeneficiario",
                    title: "Nª DNI",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNombreBeneficiario",
                    title: "BENEFICIARIO(A)",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },
                
                {
                    field: "iCodigoBanco",
                    title: "COD.BANCO",
                    width: "10px",
                    hidden: true,
                    attributes: { style: "text-align:center;" }
                    
                },
                {
                    field: "vNombreBanco",
                    title: "BANCO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNumeroCuenta",
                    title: "NRO. CUENTA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoRetencion",
                    title: "COD. RETENCION",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                    
                },
                {
                     field: "vNombreRetencion",
                     title: "TIPO RETENCION",
                     width: "50px",
                     attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodFormaPago",
                    title: "COD. FORMA PAGO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreFormaPago",
                    title: "FORMA PAGO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dValorPorcentaje",
                    title: " (%) ",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "dMontoRetencion",
                    title: "MONTO",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    //ACTUALIZAR VALORES DE BENRFICAIRIOS
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var aItems = [item.vDniBeneficiario, item.vNombreBeneficiario, item.iCodigoBanco, item.vNombreBanco, item.vNumeroCuenta, item.iCodTipoRetencion, item.vNombreRetencion, item.dValorPorcentaje, item.dMontoRetencion];
                //        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.EditarBeneficiarioTemporal(\'' + aItems + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar datos del Beneficiario"></span>';
                //        controles += '</button>';
                //        return controles;
                //    },
                //    width: '30px'
                //},
                {
                    //title: 'Eliminar',
                    title: 'Quitar',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarBeneficiarioTemporal(\'' + item.vDniBeneficiario + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Beneficiario"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        debugger;

    }

    this.DescuentoJudicialJS.prototype.CargarGrillaBeneficiariosTrabajadores = function (id) {

        debugger;

        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/ListarBeneficiariosTrabajadores',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodTipoConcepto = $("#ddlTipoConcepto_busqueda").data("kendoDropDownList").value();
                        debugger;

                        data_param.iCodJudicial = id;
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
                $("#lblNroBeneficiarioJudicial").html(this.total());
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
                    id: "vNombreBeneficiario"
                }
            },

        });
        debugger;
        this.$grid = $("#divGridBeneficiariosJudicial").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Exportar Beneficiarios.xlsx",
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
                    field: "iCodJudicial",
                    title: "COD. JUDICIAL",
                    width: "30px",
                    attributes: { style: "text-align:left;" },
                    hidden: true
                },
                {
                    field: "vDniBeneficiario",
                    title: "Nª DNI",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNombreBeneficiario",
                    title: "BENEFICIARIO(A)",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },

                {
                    field: "iCodigoBanco",
                    title: "COD.BANCO",
                    width: "10px",
                    hidden: true,
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "vNombreBanco",
                    title: "BANCO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNumeroCuenta",
                    title: "NRO. CUENTA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoRetencion",
                    title: "COD. RETENCION",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreRetencion",
                    title: "TIPO RETENCION",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodFormaPago",
                    title: "COD. FORMA PAGO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreFormaPago",
                    title: "FORMA PAGO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dValorPorcentaje",
                    title: " (%) ",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "dMontoRetencion",
                    title: "MONTO",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    //ACTUALIZAR VALORES DE BENRFICAIRIOS
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var aItems = [item.vDniBeneficiario, item.vNombreBeneficiario, item.iCodigoBanco, item.vNombreBanco, item.vNumeroCuenta, item.iCodTipoRetencion, item.vNombreRetencion, item.dValorPorcentaje, item.dMontoRetencion];
                //        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.EditarBeneficiarioTemporal(\'' + aItems + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar datos del Beneficiario"></span>';
                //        controles += '</button>';
                //        return controles;
                //    },
                //    width: '30px'
                //},
                {
                    //title: 'Eliminar',
                    title: 'Quitar',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarBeneficiarioJudicialTemporal(\'' + item.vDniBeneficiario + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Beneficiario"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        debugger;

    }
  
    this.DescuentoJudicialJS.prototype.ListarGrillaBeneficiarios = function (lista) {
        debugger;

        this.$grid = $("#divGridBeneficiarios").kendoGrid({
            dataSource: lista,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "vDniBeneficiario",
                    title: "Nª DNI",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNombreBeneficiario",
                    title: "BENEFICIARIO(A)",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },

                {
                    field: "iCodigoBanco",
                    title: "COD.BANCO",
                    width: "10px",
                    hidden: true,
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "vNombreBanco",
                    title: "BANCO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNumeroCuenta",
                    title: "NRO. CUENTA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoRetencion",
                    title: "COD. RETENCION",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreRetencion",
                    title: "TIPO RETENCION",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodFormaPago",
                    title: "COD. FORMA PAGO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreFormaPago",
                    title: "FORMA PAGO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dValorPorcentaje",
                    title: "Porcen.(%)",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "dMontoRetencion",
                    title: "MONTO",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    //ACTUALIZAR VALORES DE BENRFICAIRIOS
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var aItems = [item.vDniBeneficiario, item.vNombreBeneficiario, item.iCodigoBanco, item.vNombreBanco, item.vNumeroCuenta,  item.iCodTipoRetencion, item.vNombreRetencion, item.dValorPorcentaje,item.dMontoRetencion];
                //        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.EditarBeneficiarioTemporal(\'' + aItems + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar datos del Beneficiario"></span>';
                //        controles += '</button>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
                {
                    //ELIMINAR BENEFICIARIO
                    title: "Quitar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarBeneficiarioTemporal(\'' + item.vDniBeneficiario + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Beneficiario"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

    }

    this.DescuentoJudicialJS.prototype.ListarGrillaBeneficiariosTrabajador = function (lista) {
        debugger;

        this.$grid = $("#divGridBeneficiariosJudicial").kendoGrid({
            dataSource: lista,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "vDniBeneficiario",
                    title: "Nª DNI",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNombreBeneficiario",
                    title: "BENEFICIARIO(A)",
                    width: "150px",
                    attributes: { style: "text-align:left;" }
                },

                {
                    field: "iCodigoBanco",
                    title: "COD.BANCO",
                    width: "10px",
                    hidden: true,
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "vNombreBanco",
                    title: "BANCO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "vNumeroCuenta",
                    title: "NRO. CUENTA",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoRetencion",
                    title: "COD. RETENCION",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreRetencion",
                    title: "TIPO RETENCION",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodFormaPago",
                    title: "COD. FORMA PAGO",
                    width: "20px",
                    attributes: { style: "text-align:center;" },
                    hidden: true

                },
                {
                    field: "vNombreFormaPago",
                    title: "FORMA PAGO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dValorPorcentaje",
                    title: "Porcen.(%)",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                {
                    field: "dMontoRetencion",
                    title: "MONTO",
                    width: "40px", format: "{0:n2}",
                    attributes: { style: "text-align:right;" }
                },
                //{
                //    //ACTUALIZAR VALORES DE BENRFICAIRIOS
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var aItems = [item.vDniBeneficiario, item.vNombreBeneficiario, item.iCodigoBanco, item.vNombreBanco, item.vNumeroCuenta,  item.iCodTipoRetencion, item.vNombreRetencion, item.dValorPorcentaje,item.dMontoRetencion];
                //        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.EditarBeneficiarioTemporal(\'' + aItems + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar datos del Beneficiario"></span>';
                //        controles += '</button>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
                {
                    //ELIMINAR BENEFICIARIO
                    title: "Quitar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarBeneficiarioJudicialTemporal(\'' + item.vDniBeneficiario + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Beneficiario"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

    }

    this.DescuentoJudicialJS.prototype.AgregarBeneficiarioTemporal = function (e) {
        e.preventDefault();
        debugger;
        var dniBeneficiario = $("#txtNumeroDocumentoBeneficiario").val();
        var nomBeneficiario = $("#txtNombreBeneficiario").val();
        var codBanco = $("#ddlBancos").data("kendoDropDownList").value();
        var nomBanco = $("#ddlBancos").data("kendoDropDownList").text();
        var nroCuenta = $("#txtNumeroCuenta").val();
        var tipoRetencion = $("#ddlTipoRetencion").data("kendoDropDownList").value();
        var nombRetencion = $("#ddlTipoRetencion").data("kendoDropDownList").text();
        var porcRetencion = $("#txtValorPorcentaje").val();
        var dmontoFijo = $("#txtImporteFijo").val();
        var vObservacion = $("#txtObservacion").val();
        var codFormaPago = $("#ddlFormaPago").data("kendoDropDownList").value();
        var nombFormaPago = $("#ddlFormaPago").data("kendoDropDownList").text();
        //
        if (tipoRetencion == "1") {
            porcRetencion = "0";
            dmontoFijo = $("#txtImporteFijo").val();
        }
        
        if (tipoRetencion == "2") {
            dmontofijo = "0";
            porcRetencion = $("#txtValorPorcentaje").val();
        }

        if (dniBeneficiario == "" || nomBeneficiario == "") {
            alert("Falta Datos del Beneficiario");
            return false;
        }

        if (codBanco == "") {
            alert("Debe seleccionar el Banco");
            return false;
        }

        if (tipoRetencion == "") {
            alert("Debe seleccionar Tipo de Retención");
            return false;
        }
        if (tipoRetencion == "1") {

            if (dmontoFijo == "") {
                alert("Ingresar Monto de Retención");
                return false;
            }

        }
        if (tipoRetencion == "2") {

            if (porcRetencion == "") {
                alert("Ingresar valor del Porcentaje (%)");
                return false;
            }

        }
        if (codFormaPago == "") {
            alert("Debe seleccionar Forma de Pago");
            return false;
        }

        /*****************************************************/
        /*    Enviar para agregar la fila en la GRILLA        */
        /*****************************************************/
        var id = $("#hdIdJudicialDetalle").val();

        if (id == "0") {
            $("#hdCambioDetalle").val('0');

            /* VALIDAR SI EXISTE BENEFICIARIO CON MONTO RETENCIOB */
            var gridBenef = $("#divGridBeneficiarios").data().kendoGrid.dataSource.view();
            if (gridBenef.length > 0) {
                for (var i = 0; i < gridBenef.length; i++) {
                    debugger;
                    if (gridBenef[i].vDniBeneficiario == dniBeneficiario) {
                        controladorApp.notificarMensajeDeAlerta("El Beneficiario ya ha sido agregado en la relación, validar...");
                        return;
                    }

                }
            }

            
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/AgregarBeneficiarioTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            
                            data_param.cDniBeneficiario = dniBeneficiario;
                            data_param.cNomBeneficiario = nomBeneficiario;
                            data_param.iCodBanco = codBanco;
                            data_param.cNomBanco = nomBanco;
                            data_param.cNroCuenta = nroCuenta;
                            data_param.iCodRetencion = tipoRetencion;
                            data_param.cNomRetencion = nombRetencion;
                            data_param.dPorReten = porcRetencion;
                            data_param.dMonReten = dmontoFijo;
                            data_param.iCodFormaPago = codFormaPago;
                            data_param.cNomFormaPago = nombFormaPago;


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
                    $("#lblNroBeneficiario").html(this.total());
                    debugger;
                },
                schema: {
                    model: {
                        id: "vDniBeneficiario"
                    }
                }
            });

            debugger;
            
            this.ListarGrillaBeneficiarios(this.$dataSource);

        }

        //
        $("#txtNumeroDocumentoBeneficiario").val('');
        $("#txtNombreBeneficiario").val('');
        $("#ddlBancos").data("kendoDropDownList").value('');
        $("#txtNumeroCuenta").val('');
        $("#ddlTipoRetencion").data("kendoDropDownList").value('');
        $("#ddlFormaPago").data("kendoDropDownList").value('');
        $("#txtImporteFijo").val('');
        $("#txtValorPorcentaje").val('');
        
    };

    this.DescuentoJudicialJS.prototype.AgregarBeneficiarioJudicialTemporal = function (e) {
        e.preventDefault();
        debugger;
        var dniBeneficiario = $("#txtNumeroDocumentoBeneficiarioJudicial").val();
        var nomBeneficiario = $("#txtNombreBeneficiarioJudicial").val();
        var codBanco = $("#ddlBancosJudicial").data("kendoDropDownList").value();
        var nomBanco = $("#ddlBancosJudicial").data("kendoDropDownList").text();
        var nroCuenta = $("#txtNumeroCuentaJudicial").val();
        var tipoRetencion = $("#ddlTipoRetencionJudicial").data("kendoDropDownList").value();
        var nombRetencion = $("#ddlTipoRetencionJudicial").data("kendoDropDownList").text();
        var porcRetencion = $("#txtValorPorcentajeJudicial").val();
        var dmontoFijo = $("#txtImporteFijoJudicial").val();
        var vObservacion = $("#txtObservacionJudicial").val();
        var codFormaPago = $("#ddlFormaPagoJudicial").data("kendoDropDownList").value();
        var nombFormaPago = $("#ddlFormaPagoJudicial").data("kendoDropDownList").text();
        //
        if (tipoRetencion == "1") {
            porcRetencion = "0";
            dmontoFijo = $("#txtImporteFijoJudicial").val();
        }

        if (tipoRetencion == "2") {
            dmontofijo = "0";
            porcRetencion = $("#txtValorPorcentajeJudicial").val();
        }

        if (dniBeneficiario == "" || nomBeneficiario == "") {
            alert("Falta Datos del Beneficiario");
            return false;
        }

        if (codBanco == "") {
            alert("Debe seleccionar el Banco");
            return false;
        }

        if (tipoRetencion == "") {
            alert("Debe seleccionar Tipo de Retención");
            return false;
        }
        if (tipoRetencion == "1") {

            if (dmontoFijo == "") {
                alert("Ingresar Monto de Retención");
                return false;
            }

        }
        if (tipoRetencion == "2") {

            if (porcRetencion == "") {
                alert("Ingresar valor del Porcentaje (%)");
                return false;
            }

        }
        if (codFormaPago == "") {
            alert("Debe seleccionar Forma de Pago");
            return false;
        }

        /*****************************************************/
        /*    Enviar para agregar la fila en la GRILLA        */
        /*****************************************************/
        var id = $("#hdIdJudicialDetalleJudicial").val();

        if (id == "0") {
            $("#hdCambioDetalleJudicial").val('0');

            /* VALIDAR SI EXISTE BENEFICIARIO CON MONTO RETENCIOB */
            var gridBenef = $("#divGridBeneficiariosJudicial").data().kendoGrid.dataSource.view();
            if (gridBenef.length > 0) {
                for (var i = 0; i < gridBenef.length; i++) {
                    debugger;
                    if (gridBenef[i].vDniBeneficiario == dniBeneficiario) {
                        controladorApp.notificarMensajeDeAlerta("El Beneficiario ya ha sido agregado en la relación, validar...");
                        return;
                    }

                }
            }


            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/AgregarBeneficiarioTrabajadorTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.cDniBeneficiario = dniBeneficiario;
                            data_param.cNomBeneficiario = nomBeneficiario;
                            data_param.iCodBanco = codBanco;
                            data_param.cNomBanco = nomBanco;
                            data_param.cNroCuenta = nroCuenta;
                            data_param.iCodRetencion = tipoRetencion;
                            data_param.cNomRetencion = nombRetencion;
                            data_param.dPorReten = porcRetencion;
                            data_param.dMonReten = dmontoFijo;
                            data_param.iCodFormaPago = codFormaPago;
                            data_param.cNomFormaPago = nombFormaPago;


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
                    $("#lblNroBeneficiarioJudicial").html(this.total());
                    debugger;
                },
                schema: {
                    model: {
                        id: "vDniBeneficiario"
                    }
                }
            });

            debugger;

            this.ListarGrillaBeneficiariosTrabajador(this.$dataSource);

        }

        //
        $("#txtNumeroDocumentoBeneficiarioJudicial").val('');
        $("#txtNombreBeneficiarioJudicial").val('');
        $("#ddlBancosJudicial").data("kendoDropDownList").value('');
        $("#txtNumeroCuentaJudicial").val('');
        $("#ddlTipoRetencionJudicial").data("kendoDropDownList").value('');
        $("#ddlFormaPagoJudicial").data("kendoDropDownList").value('');
        $("#txtImporteFijoJudicial").val('');
        $("#txtValorPorcentajeJudicial").val('');

    };

    this.DescuentoJudicialJS.prototype.EditarBeneficiarioTemporal = function (item) {

        var _item = item.split(',');
        var data_param = new FormData();

        debugger;
        $("#txtNumeroDocumentoBeneficiario").val(_item[0]);
        $("#txtNombreBeneficiario").val(_item[1]);
        $("#ddlBancos").data("kendoDropDownList").value(_item[2]);
        $("#txtNumeroCuenta").val(_item[4]);
        $("#ddlTipoRetencion").data("kendoDropDownList").value(_item[5]);
        $("#txtValorPorcentaje").val(_item[7]);
        $("#txtImporteFijo").val(_item[8]);
        //$("#txtObservacion").val(_item[9]);
        $("#hdIdJudicialDetalle").val('1');  /* Valor 1 cuando es EDICION */

        $("#txtNumeroCuenta").prop('readonly',false);

        if (_item[5]=="1")
        {
            $('#divPorcentaje').hide();
            $('#divMonto').show();
            $('#txtImporteFijo').prop('readonly', false);

        }
        if (_item[5]=="2")
        {
            $('#divPorcentaje').show();
            $('#divMonto').hide();
            $('#txtValorPorcentaje').prop('readonly', false);
        }

    };

    this.DescuentoJudicialJS.prototype.EditarBeneficiarioJudicialTemporal = function (item) {

        var _item = item.split(',');
        var data_param = new FormData();

        debugger;
        $("#txtNumeroDocumentoBeneficiarioJudicial").val(_item[0]);
        $("#txtNombreBeneficiarioJudicial").val(_item[1]);
        $("#ddlBancosJudicial").data("kendoDropDownList").value(_item[2]);
        $("#txtNumeroCuentaJudicial").val(_item[4]);
        $("#ddlTipoRetencionJudicial").data("kendoDropDownList").value(_item[5]);
        $("#txtValorPorcentajeJudicial").val(_item[7]);
        $("#txtImporteFijoJudicial").val(_item[8]);
        //$("#txtObservacion").val(_item[9]);
        $("#hdIdJudicialDetalleJudicial").val('1');  /* Valor 1 cuando es EDICION */

        $("#txtNumeroCuentaJudicial").prop('readonly', false);

        if (_item[5] == "1") {
            $('#divPorcentajeJudicial').hide();
            $('#divMontoJudicial').show();
            $('#txtImporteFijoJudicial').prop('readonly', false);

        }
        if (_item[5] == "2") {
            $('#divPorcentajeJudicial').show();
            $('#divMontoJudicial').hide();
            $('#txtValorPorcentajeJudicial').prop('readonly', false);
        }

    };

    this.DescuentoJudicialJS.prototype.QuitarBeneficiarioTemporal = function (id) {
        //e.preventDefault();
        $("#hdCambioDetalle").val('1');
        
        debugger;
        if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/QuitarBeneficiarioTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.nroDNI = id;
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
                    $("#lblNroBeneficiario").html(this.total());
                    debugger;
                },
                schema: {
                    model: {
                        id: "vDniBeneficiario"
                    }
                }
            });

            debugger;
            this.ListarGrillaBeneficiarios(this.$dataSource);

        }
    };
    
    this.DescuentoJudicialJS.prototype.QuitarBeneficiarioJudicialTemporal = function (id) {
        //e.preventDefault();
        $("#hdCambioDetalleJudicial").val('1');

        debugger;
        if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/QuitarBeneficiarioTrabajadorTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.nroDNI = id;
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
                    $("#lblNroBeneficiarioJudicial").html(this.total());
                    debugger;
                },
                schema: {
                    model: {
                        id: "vDniBeneficiario"
                    }
                }
            });

            debugger;
            this.ListarGrillaBeneficiariosTrabajador(this.$dataSource);

        }
    };

    this.DescuentoJudicialJS.prototype.GenerarPlanillaJudicial = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaJudicial';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        
        frmBandejaDescuentoJudicial = $("#frmBandejaDescuentoJudicial").kendoValidator().data("kendoValidator");
        if (frmBandejaDescuentoJudicial.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            //var iMes = $('#ddlMesCAS_GenPlan').data("kendoDropDownList").value();
            //var iAnio = $('#ddlAnioCAS_GenPlan').data("kendoDropDownList").value();
            //var iCodPlanilla = '1';
            //var iCodTipoPlanilla = '1';
            //var iCodDetPlanilla = '1';
            var comboPlanilla = $("#ddlPlanillaAperturada_busqueda").data("kendoDropDownList").value();
            var codPlanilla = comboPlanilla.split('-');

            var iCodPlanilla = codPlanilla[0];
            var iCodTipoPlanilla = codPlanilla[1];
            var iAnio = codPlanilla[2];
            var iMes = codPlanilla[3];
            var iCodDetPlanilla = codPlanilla[4];
            var bEstadoRegAsistencia = codPlanilla[5];
            var bEstadoDsctoFijoVariable = codPlanilla[6];
            data_param.append('iMes', iMes);
            data_param.append('iAnio', iAnio);
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
            data_param.append('iCodDetPlanilla', iCodDetPlanilla);
            debugger;
            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
            //var existe = controlador.ConsultarEjecucionPlanilla(items);
            
            controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de generar Planilla Judicial?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'DescuentoJudicial/' + metodo,
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
                            //controlador.GenerarPDFConvocatorias(item);
                            controladorApp.notificarMensajeSatisfactorio("La planilla Judicial se ha generado correctamente");
                            //controlador.inicializarBandejaUser();                                
                            //controlador.CargarBandejaPrincipalPlanillaCAS(event);
                            // REFRESCAR INFORMACION DEL TRABAJADOR

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


    }

}(jQuery));
