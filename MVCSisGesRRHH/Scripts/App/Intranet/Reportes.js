(function ($) {
    var frmResumenGeneral;
    var frmResumenGeneralAnual;
    this.ReportesJS = function () { };

    this.ReportesJS.prototype.inicializarReporteResumenGeneral = function () {
        debugger;
        $("#d_Conceptos").hide();
        frmResumenGeneral = $("#frmResumenGeneral").kendoValidator().data("kendoValidator");

        $("#ddlMes_RptGeneral").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        $("#ddlAnio_RptGeneral").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "Anio",
            dataValueField: "Anio",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        $("#ddlPlanilla_RptGeneral").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "IdNombrePlanilla",
            dataValueField: "IdRegistroPlanilla",
            cascadeFrom: "ddlAnio_RptGeneral",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        //url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillasAdicinalesCASFED",
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            //data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            data_param.IdPlanilla = 0;
                        data_param.IdTipoPlanilla = 0;
                        data_param.IdMes = $('#ddlMes_RptGeneral').data("kendoDropDownList").value();
                        data_param.IdAnio = $('#ddlAnio_RptGeneral').data("kendoDropDownList").value();
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlReportes_RptGeneral").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarReportes",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            },
            change: function (e) {
                var value = this.value();
                if (value == 16) {
                    $("#ddlReportes_RptTipoConcepto").data("kendoDropDownList").value("");
                    $("#ddlReportes_RptConcepto").data("kendoDropDownList").value("");
                    $("#d_Conceptos").show();
                }
                else {
                    $("#d_Conceptos").hide();
                }
            }
        });

        $("#ddlReportes_RptTipoConcepto").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarTiposConceptos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.Nombre = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlReportes_RptConcepto").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "vConcepto",
            dataValueField: "iCodConcepto",
            cascadeFrom: "ddlReportes_RptTipoConcepto",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        //url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillasAdicinalesCASFED",
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarConceptoPorTipo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)                        
                        data_param.iCodTipoConcepto = $('#ddlReportes_RptTipoConcepto').data("kendoDropDownList").value();
                        var lstPlanilla = $("#ddlPlanilla_RptGeneral").data("kendoDropDownList").value();
                        var vplanilla = lstPlanilla.split('|');
                        var iCodPlanilla = vplanilla[0];
                        debugger;
                        if (iCodPlanilla == 1 || iCodPlanilla == 2 || iCodPlanilla == 3 || iCodPlanilla == 4) {
                            data_param.bRegCAS = true;
                            data_param.bRegFunc = false;
                            data_param.bRegSeci = false;
                        }
                        else if (iCodPlanilla == 5 || iCodPlanilla == 7 || iCodPlanilla == 10) {
                            data_param.bRegCAS = false;
                            data_param.bRegFunc = true;
                            data_param.bRegSeci = false;
                        }
                        else if (iCodPlanilla == 12 || iCodPlanilla == 13) {
                            data_param.bRegCAS = false;
                            data_param.bRegFunc = false;
                            data_param.bRegSeci = true;
                        }
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
    }

    this.ReportesJS.prototype.GenerarReporteResumenGeneral = function (e) {
        e.preventDefault();
        //var data_param = new FormData();
        debugger;
        
        var data_param = new FormData();
        var iMes = $("#ddlMes_RptGeneral").data("kendoDropDownList").value();
        var sMes = $("#ddlMes_RptGeneral").data("kendoDropDownList").text();
        var iAnio = $("#ddlAnio_RptGeneral").data("kendoDropDownList").value();
        var iReporte = $("#ddlReportes_RptGeneral").data("kendoDropDownList").value();
        var lstPlanilla = $("#ddlPlanilla_RptGeneral").data("kendoDropDownList").value();
        var sPlanilla = $("#ddlPlanilla_RptGeneral").data("kendoDropDownList").text();
        var sNombreReporte = $("#ddlReportes_RptGeneral").data("kendoDropDownList").text();
        var vplanilla = lstPlanilla.split('|');
        var iCodPlanilla = vplanilla[0];
        var iCodTipoPlanilla = vplanilla[1];
        var iCodDetPlanilla = vplanilla[4];

        var iCodTipoConcepto = $("#ddlReportes_RptTipoConcepto").data("kendoDropDownList").value();
        var iCodConcepto = $("#ddlReportes_RptConcepto").data("kendoDropDownList").value();

        data_param.append('iMes', iMes);
        data_param.append('sMes', sMes);
        data_param.append('iAnio', iAnio);
        //data_param.append('iAnio', '2020');
        data_param.append('iCodPlanilla', iCodPlanilla);
        data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
        data_param.append('iCodDetPlanilla', iCodDetPlanilla);
        data_param.append('sPlanilla', sPlanilla);
        data_param.append('sNombreReporte', sNombreReporte);
        data_param.append('iCodTipoConcepto', iCodTipoConcepto);
        data_param.append('iCodConcepto', iCodConcepto);

        var sReporte = '';
        switch (iReporte) {
            case "1": {
                sReporte = 'ReporteResumenGeneral';
                break;
            }
            case "2": {
                sReporte = "ReporteResumenGeneralPorMeta";
                break;
            }
            case "3": {
                sReporte = "ReporteResumenGeneralPorMetaEsSalud";
                break;
            }
            case "4": {
                sReporte = "ReporteDetalladoEsSalud";
                break;
            }
            case "5": {
                sReporte = "ReporteResumenRetencionesFteFto";
                break;
            }
            case "6": {
                sReporte = "ReporteResumenIngresosMetaPartidaConcepto";
                break;
            }
            case "7": {
                sReporte = "ReporteResumenEgresosMetaPartidaConcepto";
                break;
            }
            case "8": {
                sReporte = "ReporteResumenAportacionesMetaPartidaConcepto";
                break;
            }
            case "9": {
                sReporte = "ReporteResumenDsctosJudicialesMetaPartidaConcepto";
                break;
            }
            case "10": {
                sReporte = "ReporteResumenGralAFPconNroAfiliados";
                break;
            }
            case "11": {
                sReporte = "ReporteResumenGralAFPconAFPDepMetaClasGasto";
                break;
            }
            case "12": {
                sReporte = "ReporteResumenGralBcosPorInstCantTotIngTotEgre";
                break;
            }
             case "13": {
                 sReporte = "ReporteResumenGralBcosFteFtoMeta";
                break;
            }
            case "14": {
                 sReporte = "ReporteResumenDetalladoBcos";
                break;
            }
            case "15": {
                sReporte = "ReportePlanillaUnicaPagos";
                break;
            }
            case "16": {
                sReporte = "ReporteResumenIngresosMetaPartidaPorConcepto";
                break;
            }
        }
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Reportes/'+sReporte,
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;                
                controladorApp.notificarMensajeSatisfactorio('El reporte se generó correctamente');
                //$("#ifrRptGeneral").data
                document.getElementById('ifrRptGeneral').src += '';
            },
            error: function (res) {
                alert("El reporte se generó correctamente");
                debugger;
            }
        });
        
    }

    this.ReportesJS.prototype.inicializarReporteResumenAnual = function () {
        debugger;
        
        $("#d_DNI").hide(); 
        $("#d_Planilla").hide();
        frmResumenGeneralAnual = $("#frmResumenGeneralAnual").kendoValidator().data("kendoValidator");
                
        $("#ddlMes_RptAnual").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlAnio_RptAnual").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "Anio",
            dataValueField: "Anio",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });        

        $("#ddlReportes_RptAnual").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarReportes2",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            },            
            change: function (e) {
            var value = this.value();            
            if (value == 7) {
                //$("#ddlReportes_RptAnual").data("kendoDropDownList").value('');
                $("#txtDNI").val('');
                $("#d_DNI").show();
            }
            else {
                $("#d_DNI").hide();
            }
            if (value == 1) {                
                $("#ddlPlanillaRpteAnual").data("kendoDropDownList").value('');
                $("#d_Planilla").show();
            }
            else {
                $("#d_Planilla").hide();
            }
        }
        });

        $("#ddlPlanillaRpteAnual").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "IdNombrePlanilla",
            dataValueField: "IdRegistroPlanilla",
            cascadeFrom: "ddlAnio_RptAnual",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        //url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillasAdicinalesCASFED",
                        //url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillas",
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillasTodas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            //data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            data_param.IdPlanilla = 0;
                        data_param.IdTipoPlanilla = 0;
                        data_param.IdMes = $('#ddlMes_RptAnual').data("kendoDropDownList").value();
                        data_param.IdAnio = $('#ddlAnio_RptAnual').data("kendoDropDownList").value();
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
    }
    this.ReportesJS.prototype.inicializarReporteAltas = function () {
        debugger;

        frmResumenGeneralAnual = $("#frmResumenGeneralAnual").kendoValidator().data("kendoValidator");

        $("#ddlMes_RptAnual").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Reportes/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        $("#ddlAnio_RptAnual").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "Anio",
            dataValueField: "Anio",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
    }

    this.ReportesJS.prototype.GenerarReporteResumenAnual = function (e) {
        e.preventDefault();
        //var data_param = new FormData();
        debugger;

        var data_param = new FormData();
        var iMes = $("#ddlMes_RptAnual").data("kendoDropDownList").value();
        var sMes = $("#ddlMes_RptAnual").data("kendoDropDownList").text();
        var iAnio = $("#ddlAnio_RptAnual").data("kendoDropDownList").value();
        var iReporte = $("#ddlReportes_RptAnual").data("kendoDropDownList").value();        
        var sNombreReporte = $("#ddlReportes_RptAnual").data("kendoDropDownList").text();
        var comboPlanilla = $("#ddlPlanillaRpteAnual").data("kendoDropDownList").value();
        
        var codPlanilla = comboPlanilla.split('|');

        var iCodPlanilla = codPlanilla[0];
        var iCodTipoPlanilla = codPlanilla[1];        
        var iCodDetPlanilla = codPlanilla[4];
        var sDNI = $("#txtDNI").val();
        //var vplanilla = lstPlanilla.split('|');
        //var iCodPlanilla = vplanilla[0];
        //var iCodTipoPlanilla = vplanilla[1];
        //var iCodDetPlanilla = vplanilla[4];

        data_param.append('iMes', iMes);
        data_param.append('sMes', sMes);
        data_param.append('iAnio', iAnio);
        data_param.append('sDNI', sDNI);
        data_param.append('iCodPlanilla', iCodPlanilla);
        data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
        data_param.append('iCodDetPlanilla', iCodDetPlanilla);
        //data_param.append('iAnio', '2020');
        //data_param.append('iCodPlanilla', iCodPlanilla);
        //data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
        //data_param.append('iCodDetPlanilla', iCodDetPlanilla);
        //data_param.append('sPlanilla', sPlanilla);
        var existeDNI = false;
        data_param.append('sNombreReporte', sNombreReporte);
        if (iMes == "" && iReporte <= 3) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el mes');
            return;
        }
        else {
            if (iReporte == 7 && sDNI == "") {
                controladorApp.notificarMensajeDeAlerta('Debe ingresar DNI');
                return;
            }            
        }
        var sReporte = '';
        switch (iReporte) {
            case "1": {
                sReporte = 'ReporteEstructuraInfoAFPNET';
                existeDNI = true;
                break;
            }
            case "2": {
                sReporte = 'ReporteTrabajadoresAfiliadosONPE';
                existeDNI = true;
                break;
            }
            case "3": {
                sReporte = "ReporteInfoPortalTranspEstandar";
                existeDNI = true;
                break;
            }
            case "4": {
                sReporte = "ReporteResumenPlanillaMensIngEgreEsSalud";
                existeDNI = true;
                data_param.append('iCodTipoConcepto', 1);                
                break;
            }
            case "5": {
                sReporte = "ReporteResumenPlanillaMensIngEgreEsSalud";
                existeDNI = true;
                data_param.append('iCodTipoConcepto', 2);                
                break;
            }
            case "6": {
                sReporte = "ReporteResumenPlanillaMensIngEgreEsSalud";
                existeDNI = true;
                data_param.append('iCodTipoConcepto', 3);                
                break;
            }
            case "7": {
                sReporte = "ReporteResumenAnuaPorTrab";
                if (sDNI!="") {
                    existeDNI = true;
                }
                break;
            }
            case "8": {
                sReporte = "ReporteResumenIngRent4ta";
                existeDNI = true;
                break;
            }
            case "9": {
                sReporte = "ReporteEjecucionMensualMetaEspGastoCAS";
                existeDNI = true;
                break;
            }
            case "10": {
                sReporte = "ReporteEjecucionMensualMetaEspGastoFUNC";
                existeDNI = true;
                break;
            }
        }
        if (existeDNI) {
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Reportes/' + sReporte,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    controladorApp.notificarMensajeSatisfactorio('El reporte se generó correctamente');
                    //$("#ifrRptGeneral").data
                    document.getElementById('ifrRptGeneral').src += '';
                },
                error: function (res) {
                    alert("El reporte no se generó correctamente");
                    debugger;
                }
            });
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Debe Ingresar nro de DNI');
            return;
        }
            
    }
        
        

    
}(jQuery));