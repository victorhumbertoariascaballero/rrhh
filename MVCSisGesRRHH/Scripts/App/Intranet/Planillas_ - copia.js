﻿(function ($) {
    var frmBandejaCAS;
    var frmGenerarPlanillaCAS;
    var frmGenerarPlanillaCASAdicional;
    var frmGenerarPlanillaFUNCAdicional;
    var frmBandejaCASAdicional;
    var frmBandejaFUNCAdicional;
    var frmGenerarPlanillaFUNC;
    var frmBandejaFUNC;
    var frmGenerarPlanillaVacTruncasCAS;
    var frmBandejaVacTruncasCAS;
    var frmBandejaPlanilla;
    var dataImportar = [];
    var dataImportarSuspRet4Ta = [];
    var frmModalAgregarPlanilla;
    var frmTrabajadoresSuspRet4Ta;
    var frmBandejaFED;
    var frmGenerarPlanillaFED;
    var frmModica = 0;
    var frmModalPlanillaReporte;
    this.PlanillasJS = function () { };


    ////////////////////////////BANDEJA PLANILLA REGIMEN CAS//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaCAS = function () {
        debugger;
        frmBandejaCAS = $("#frmBandejaCAS").kendoValidator().data("kendoValidator");
        frmGenerarPlanillaCAS = $("#frmGenerarPlanillaCAS").kendoValidator().data("kendoValidator");
        $("#ddlMesCAS_GenPlan").kendoDropDownList({
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
        $("#ddlAnioCAS_GenPlan").kendoDropDownList({
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
        $("#ddlMesCAS_BusqPlan").kendoDropDownList({
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
        $("#ddlAnioCAS_BusqPlan").kendoDropDownList({
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
        $("#optional").kendoListBox({
            autoBind: true,
            connectWith: "selected",
            dropSources: ["selected"],
            draggable: {
                enabled: true,
                placeholder: function (element) {
                    return element.clone().css({
                        "opacity": 0.3,
                        "border": "1px dashed #000000"
                    });
                }
            },
            toolbar: {
                tools: ["transferTo", "transferFrom", "transferAllTo", "transferAllFrom"]
            }
        });

        $("#selected").kendoListBox({
            dropSources: ["optional"],
            //connectWith: "optional",
            draggable: {
                enabled: true,
                placeholder: function (element) {
                    return element.clone().css({
                        "opacity": 0.3,
                        "border": "1px dashed #000000"
                    });
                }
            }
        });
        //var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
        //var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
        //debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //    this.CargarBandejaPrincipalPlanillaCAS(event);
        //}
        //else {
        //    this.CargarBandejaPrincipalPlanillaCASVacio(event);
        //}
        //this.CargarBandejaPrincipalPlanillaCAS(event);
        //controlador.CargarBandejaPrincipalPlanillaCASVacio();        
        $("#divGridBandejaPlanillaCAS").empty();
        $("#divGridBandejaPlanillaCASVacio").empty();
        $("#divGridBandejaPlanillaCASVacio").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "bExoneracionRenta4ta",
                        title: "EXO. 4TA",
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var tipo = '';
                            if (item.bExoneracionRenta4ta == true) tipo = "S";
                            if (item.bExoneracionRenta4ta == false) tipo = "N";

                            return tipo;
                        }
                    },
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasDescansos_Subsidios",
                        title: "DIAS D/S",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "R. BÁSICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.3. 2 8. 1 2)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        divGridBandejaPlanillaCASVacio.wrapper.css("display", "block");
    }    
    
    //this.PlanillasJS.prototype.ConsultarEjecucionPlanilla = function (item) {
    //    //e.preventDefault();
    //    //item.preventDefault();
    //    debugger;
    //    var items = new Object();
    //    if (item != 0) {
    //        //var _item = item.split(',');
    //        items.iCodPlanilla = item[0];
    //        items.iMes = item[1];
    //        items.iAnio = item[2];
    //        items.iCodTipoPlanilla = item[3];
    //    }
    //    var data_param = new FormData();
    //    debugger;
    //    data_param.append('iCodPlanilla', items.iCodPlanilla);
    //    data_param.append('iMes', items.iMes);
    //    data_param.append('iAnio', items.iAnio);
    //    data_param.append('iCodTipoPlanilla', items.iCodTipoPlanilla);

    //    $.ajax({
    //        url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
    //        type: 'POST',
    //        dataType: 'json',
    //        contentType: false,
    //        processData: false,
    //        data: data_param,
    //        success: function (res) {               
    //            debugger;
    //            //return res;
    //            if (res.length > 0) {
    //                debugger;
    //                //controlador.CargarBandejaPrincipalPlanillaCAS();
    //            }
    //            //else {

    //            //}
    //        },
    //        error: function (res) {
    //            //alert(res);
    //            debugger;
    //        }
    //    });
    //    debugger;
    //}

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaCAS.validate()) {
            var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
            var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
            var metasTemp = "";
            var list = $("#selected").data("kendoListBox");
            if (list.dataSource.data().length > 0) {
                for (var i = 0; i < list.dataSource.data().length; i++) {
                    debugger;
                    if (i < list.dataSource.data().length - 1) {
                        metasTemp += list.dataSource._data[i].value + ",";
                    }
                    else {
                        metasTemp += list.dataSource._data[i].value;
                    }
                }
            }
            $("#hdidCodFuenteFinanciamiento").val(0);
            $("#hdvMetasTemp").val(metasTemp);
            $("#hdiMesBusqPlan4ta").val(iMesBusqPlan4ta);
            $("#hdiAnioBusqPlan4ta").val(iAnioBusqPlan4ta);

            var iCodPlanilla = '1';
            var iCodTipoPlanilla = '1';           
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            data_param.append('idCodFuenteFinanciamiento', 0);
            data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlan4ta);
            data_param.append('iAnio', iAnioBusqPlan4ta);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaCAS").empty();
                                    $("#divGridBandejaPlanillaCASVacio").empty();
                                    controlador.CargarBandejaPrincipalPlanillaCAS();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
                                    divGridBandejaPlanillaCAS.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaCASVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
                                    GridBandejaPlanillaCASVacio.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaCAS").empty();
                                    $("#divGridBandejaPlanillaCASVacio").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotal").html(res.length);
                                    $("#divGridBandejaPlanillaCASVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaCAS").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "bExoneracionRenta4ta",
                                                title: "EXO. 4TA",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" },
                                                template: function (item) {
                                                    var tipo = '';
                                                    if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                    if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                    return tipo;
                                                }
                                            },
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasDescansos_Subsidios",
                                                title: "DIAS D/S",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "R. BÁSICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.3. 2 8. 1 2)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
                                    divGridBandejaPlanillaCAS.wrapper.css("display", "none");
                                    var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
                                    GridBandejaPlanillaCASVacio.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCAS = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
        var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                            data_param.idCodFuenteFinanciamiento = 0;
                            var list = $("#selected").data("kendoListBox");
                            if (list.dataSource.data().length > 0) {
                                var metasTemp = "";
                                for (var i = 0; i < list.dataSource.data().length; i++) {
                                    debugger;
                                    if (i < list.dataSource.data().length - 1) {
                                        metasTemp += list.dataSource._data[i].value + ",";
                                    }
                                    else {
                                        metasTemp += list.dataSource._data[i].value;
                                    }
                                }
                            }
                            //alert(metasTemp);

                            data_param.sMetas = metasTemp;
                            data_param.iMes = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
                            //data_param.iMes = '9';
                            //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                            data_param.iAnio = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
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
                    total: function (response) {
                        //debugger;
                        //var TotalDeRegistros = 0;
                        //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "iCodTrabajador",
                        fields: {
                            iCodTrabajador: { type: "number" },
                            sNombreTipoDocumento: { type: "string" },
                            sNroDocumento: { type: "string" },
                            sNombreCompleto: { type: "string" },
                            sCargo: { type: "string" },
                            sRegimenPensionario: { type: "string" },
                            bExoneracionRenta4ta: { type: "bool" },
                            iDiasLaborados: { type: "number" },
                            iDiasVacaciones: { type: "number" },
                            iDiasLicencias: { type: "number" },
                            iDiasDescansos_Subsidios: { type: "number" },
                            dcMontoRemuneracionBasica: { type: "number" },
                            dcMontoTotalIngresos: { type: "number" },
                            dcMontoTotalDescuentos: { type: "number" },
                            dcMontoTotalNeto: { type: "number" },
                            dcMontoAporteEsSalud: { type: "number" },
                            iMes: { type: "number" },
                            iAnio: { type: "number" }                        
                        }
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
            this.$grid = $("#divGridBandejaPlanillaCAS").kendoGrid({
                toolbar: ["excel", ],
                excel: {
                    fileName: "Planilla CAS.xlsx",
                    filterable: false,
                    proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaCAS"
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
                dataBound: function () {
                    //debugger;
                    //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                    this.expandRow(this.tbody.find("tr.k-master-row").first());
                },
                columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "bExoneracionRenta4ta",
                        title: "EXO. 4TA",
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var tipo = '';
                            if (item.bExoneracionRenta4ta == true) tipo = "S";
                            if (item.bExoneracionRenta4ta == false) tipo = "N";

                            return tipo;
                        }
                    },
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasDescansos_Subsidios",
                        title: "DIAS D/S",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "R. BÁSICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD <br /> (2.3. 2 8. 1 2)",
                        width: "50px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Modificar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var items = [item.iCodTrabajador, item.iMes, item.iAnio, "1", "1",item.sNombreCompleto];
                            var controles = "";
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();


            //debugger;
            //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
            //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
            //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
            //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
            // }
        //}
        $('#filter').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaCAS').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
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

        detailRow.find(".divGridIngresos").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosCAS',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },                    
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;                            
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                
                {
                    field: "dcMontoContraprestacion",
                    title: "CONTRAPRESTACION (2.3. 2 8. 1 1)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoContraprescionVacaional",
                    title: "CONTRAPRESTACION VACACIONAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoCopagoSubsidio",
                    title: "COPAGO SUBSIDIO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.3. 2 8. 1 4)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
                
            ]
        }); //.data()
        detailRow.find(".divGridDescuentos").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosCAS',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoTardanzas",
                    title: "TARDANZAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoInasistencias",
                    title: "INASISTENCIAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoSalidasAnticipadas",
                    title: "SAL. ANTIC.",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta4ta",
                    title: "IMP. RTA 4TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEsSaludMasVida",
                    title: "EsSALUD MAS VIDA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASVacio = function (e) {
        //e.preventDefault();
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.idCodFuenteFinanciamiento = 0;
                        var list = $("#selected").data("kendoListBox");
                        if (list.dataSource.data().length > 0) {
                            var metasTemp = "";
                            for (var i = 0; i < list.dataSource.data().length; i++) {
                                debugger;
                                if (i < list.dataSource.data().length - 1) {
                                    metasTemp += list.dataSource._data[i].value + ",";
                                }
                                else {
                                    metasTemp += list.dataSource._data[i].value;
                                }
                            }
                        }
                        //alert(metasTemp);

                        data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
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
                total: function (response) {                    
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador"                    
                }
            },            
        });
        debugger;
        this.$grid = $("#divGridBandejaPlanillaCASVacio").kendoGrid({
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
            //detailTemplate: kendo.template($("#template").html()),
            //detailInit: detailInit,
            dataType: 'json',
            //dataBound: function () {
            //    //debugger;
            //    //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasDescansos_Subsidios",
                    title: "DIAS D/S",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD (2.3. 2 8. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                }
            ]
        }).data();

    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaCAS = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamiento").val();
        var iMesBusqPlan4ta = $("#hdiMesBusqPlan4ta").val();
        var iAnioBusqPlan4ta = $("#hdiAnioBusqPlan4ta").val();
        var metasTemp = $("#hdvMetasTemp").val();
        var cantReg = $("#lblTotal").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaCAS').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaCAS',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlan4ta;
                                data_param.iAnio = iAnioBusqPlan4ta;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                bExoneracionRenta4ta: { type: "bool" },
                                iDiasLaborados: { type: "number" },
                                iDiasVacaciones: { type: "number" },
                                iDiasLicencias: { type: "number" },
                                iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoTotalIngresos: { type: "number" },

                                dcMontoTardanzas: { type: "number" },
                                dcMontoPermisos: { type: "number" },
                                dcMontoSalidasAnticipadas: { type: "number" },
                                dcMontoImpuestoRenta4ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" },
                                sNombreFuenteFinanciamiento: { type: "string" },
                                sNombreMeta: { type: "string" },
                                sBanco: { type: "string" },
                                sCuenta: { type: "string" },
                                sCCI: { type: "string" }

                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 11,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 6,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 14,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 4,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 3,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Ex. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Laborados",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Vacaciones",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Licencias",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Descansos y/o Subsidios",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Contraprestacion (2.3. 2 8. 1 1)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Contraprescion Vacacional",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. CopagoSubsidio",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aguinaldos (2.3. 2 8. 1 4)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Tardanzas",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Sal. Antic.",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EsSaludMasVida",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.3. 2 8. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "Fuente Financiamiento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Meta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Cta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "CCI",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              { value: data[i].bExoneracionRenta4ta },
                              { value: data[i].iDiasLaborados },
                              { value: data[i].iDiasVacaciones },
                              { value: data[i].iDiasLicencias },
                              { value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" },
                              { value: data[i].sNombreFuenteFinanciamiento },
                              { value: data[i].sNombreMeta },
                              { value: data[i].sBanco },
                              { value: data[i].sCuenta },
                              { value: data[i].sCCI }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla CAS",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla General Completa CAS.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }
        
    }

    this.PlanillasJS.prototype.GenerarPlanillaCAS = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaCAS';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
       
        if (frmGenerarPlanillaCAS.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            var iMes = $('#ddlMesCAS_GenPlan').data("kendoDropDownList").value();
            var iAnio = $('#ddlAnioCAS_GenPlan').data("kendoDropDownList").value();
            var iCodPlanilla = '1';
            var iCodTipoPlanilla = '1';
            data_param.append('iMes', iMes);
            data_param.append('iAnio', iAnio);
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
            debugger;
            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
            //var existe = controlador.ConsultarEjecucionPlanilla(items);
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res>0) {
                        if (res == 2) {
                            controladorApp.abrirMensajeDeConfirmacion(
                            '¿Está seguro de generar Planilla CAS?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                            controladorApp.notificarMensajeSatisfactorio("La planilla CAS se ha generado correctamente");
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
                            controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                        }
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                    }
                    
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
            debugger;     
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        

    }

    this.PlanillasJS.prototype.ListarConceptosPagosTrabajador = function (item) {
        debugger;
        var items = null;
        if (item != 0) {
            items = new Object();
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];
            //items.strNombreCompleto = _item[1] + " " + _item[2];
            items.iMes = _item[1];
            items.iAnio = _item[2];
            items.iCodPlanilla = _item[3];
            items.iCodTipoPlanilla = _item[4];
        }
        debugger;
        if (items != null) {
            debugger;
            //if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarConceptosPagosTrabajador',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    update: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ActualizarConceptoPagosTrabajador",
                        type: 'POST',
                        dataType: 'json',
                        cache: false                        
                    },
                    destroy: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/EliminarConceptoPagosTrabajador",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        //if ($operation === "read") {
                        //    data_param.Grilla = {};
                        //    data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                        //    data_param.Grilla.PaginaActual = $options.page
                        //    if ($options !== undefined && $options.sort !== undefined) {
                        //        data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                        //        data_param.Grilla.OrdenarPor = $options.sort[0].field;
                        //    }

                        //    data_param.iCodTrabajador = items.iCodTrabajador;                            
                        //    data_param.iMes = items.iMes;
                        //    data_param.iAnio = items.iAnio;
                        //    data_param.iCodPlanilla = items.iCodPlanilla;
                        //    data_param.iCodTipoPlanilla = items.iCodTipoPlanilla;
                        //}
                        switch ($operation) {
                            case "read":
                                data_param.iCodTrabajador = items.iCodTrabajador;                            
                                data_param.iMes = items.iMes;
                                data_param.iAnio = items.iAnio;
                                data_param.iCodPlanilla = items.iCodPlanilla;
                                data_param.iCodTipoPlanilla = items.iCodTipoPlanilla;
                                //data_param.Estado = 0;
                                data_param.Grilla = {};
                                data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                                data_param.Grilla.PaginaActual = $options.page
                                if ($options !== undefined && $options.sort !== undefined) {
                                    data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                                    data_param.Grilla.OrdenarPor = $options.sort[0].field;
                                }
                                
                                break;
                            case "create":
                                //if (frmCompromiso.validate()) {   
                                data_param.iCodTrabajador = $options.iCodTrabajador;
                                data_param.iMes = $options.iMes;
                                data_param.iAnio = $options.iAnio;
                                data_param.iCodPlanilla = $options.iCodPlanilla;
                                data_param.iCodTipoPlanilla = $options.iCodTipoPlanilla;                                
                                data_param.dcMonto = $options.dcMonto;
                                //}
                                break;
                            case "update":
                                data_param.iCodTrabajador = $options.iCodTrabajador;
                                data_param.iMes = $options.iMes;
                                data_param.iAnio = $options.iAnio;
                                data_param.iCodPlanilla = $options.iCodPlanilla;
                                data_param.iCodTipoPlanilla = $options.iCodTipoPlanilla;
                                data_param.iCodTipoConcepto = $options.iCodTipoConcepto;
                                data_param.iCodSubTipoConcepto = $options.iCodSubTipoConcepto;
                                data_param.iCodConcepto = $options.iCodConcepto;
                                data_param.dcMonto = $options.dcMonto;
                                frmModica = 1;
                                break;
                            case "destroy":
                                data_param.iCodTrabajador = $options.iCodTrabajador;
                                data_param.iMes = $options.iMes;
                                data_param.iAnio = $options.iAnio;
                                data_param.iCodPlanilla = $options.iCodPlanilla;
                                data_param.iCodTipoPlanilla = $options.iCodTipoPlanilla;
                                data_param.iCodTipoConcepto = $options.iCodTipoConcepto;
                                data_param.iCodSubTipoConcepto = $options.iCodSubTipoConcepto;
                                data_param.iCodConcepto = $options.iCodConcepto;
                                break;
                        }
                        return $.toDictionary(data_param);
                    }
                },
                requestEnd: function (e) {
                    switch (e.type) {
                        case "create": case "update": case "destroy":
                            var grilla = $('#divConceptosPagosTrabajador').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);

                            break;
                    }
                },
                //change: function (e) {
                //    $("#lblTotal").html(this.total());
                //    debugger;
                //},
                schema: {
                    model: {
                        id: "iCodTrabajador",
                        fields: {
                            iCodTrabajador: { editable: false, nullable: true },
                            sNombreTipoConcepto: { editable: false, nullable: true },
                            sNombreSubTipoConcepto: { editable: false, nullable: true },
                            sNombreConcepto: { editable: false, nullable: true },
                            dcMonto: {
                                validation:
                                    {
                                        required: true,
                                        dcmontovalidation: function (input) {
                                            if (input.is("[name='dcMonto']") && input.val() == "") {
                                                input.attr("data-dcmontovalidation-msg", "El Monto es requerido");
                                                return false;
                                            }

                                            return true;
                                        }
                                    }
                            },
                        }
                    }
                }
            });
            debugger;
            this.$grid = $("#divConceptosPagosTrabajador").kendoGrid({
                //toolbar: [, ],
                //excel: {
                //    fileName: "Listado de Bases de Convocatoria.xlsx",
                //    filterable: false
                //},
                dataSource: this.$dataSource,
                autoBind: true,
                //edit: function (e) {
                //    debugger;
                //    frmModica = 1;
                //},
                selectable: true,
                scrollable: false,
                sortable: false,
                pageable: false,
                groupable: false,
                dataType: 'json',
                columns: [
                    {
                        field: "iCodTrabajador",
                        title: "iCodTrabajador",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "iCodPlanilla",
                        title: "iCodPlanilla",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "iCodTipoPlanilla",
                        title: "iCodTipoPlanilla",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "iMes",
                        title: "iMes",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "iAnio",
                        title: "iAnio",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "iCodTipoConcepto",
                        title: "iCodTipoConcepto",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },

                    {
                        field: "sNombreTipoConcepto",
                        title: "Tipo Concepto",
                        width: "100px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "iCodSubTipoConcepto",
                        title: "iCodSubTipoConcepto",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },

                    {
                        field: "sNombreSubTipoConcepto",
                        title: "Sub Tipo Concepto",
                        width: "150px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "iCodConcepto",
                        title: "iCodConcepto",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreConcepto",
                        title: "Concepto",
                        width: "200px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "dcMonto",
                        title: "Monto",
                        width: "100px",
                        attributes: { style: "text-align:center;" },
                        format: "{0:c}"
                    },
                    { title: "ACCIONES", command: ["edit", "destroy"], width: "100px" },
                    //{
                    //    //INGRESAR DETALLE DE LA EVALUACION
                    //    title: "Modificar",
                    //    attributes: { style: "text-align:center;" },
                    //    template: function (item) {
                    //        var items = [item.iCodTrabajador, item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio, item.iCodTipoConcepto, item.iCodSubTipoConcepto, item.iCodConcepto, item.dcMonto];
                    //        var controles = "";
                    //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + items + '\')">';
                    //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                    //        controles += '</button>';
                    //        return controles;
                    //    },
                    //    width: '30px'
                    //}
                ],
                editable: "inline"
            }).data();
            //}

            debugger;
        } 
    }

    this.PlanillasJS.prototype.abrirModalConceptosPagosTrabajador = function (item) {
        //e.preventDefault();
        var items = null;
        if (item != 0) {
            items = new Object();
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];
            //items.strNombreCompleto = _item[1] + " " + _item[2];
            items.iMes = _item[1];
            items.iAnio = _item[2];
            items.iCodPlanilla = _item[3];
            items.iCodTipoPlanilla = _item[4];
            items.strNombreCompleto = _item[5];
        }
        debugger;
        var modal = $('#ModalConceptosPagosTrabajador').data('kendoWindow');

        modal.title("Conceptos Pago Trabajadores");

        $("#lblTrabajador").val(items.strNombreCompleto);
        controlador.ListarConceptosPagosTrabajador(item);
        //frmModica = items;
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalConceptosPagosTrabajador = function () {
        debugger;
        var modal = $('#ModalConceptosPagosTrabajador').data('kendoWindow');
        modal.close();
    }
    $('#ModalConceptosPagosTrabajador').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Conceptos Pago Trabajadores',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            
            debugger;
            if (frmModica == 1){
                location.reload();
            }
        }
    }).data("kendoWindow");
    ////////////////////////////BANDEJA PLANILLA REGIMEN CAS - FED//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaFED = function () {
        debugger;
        frmBandejaFED = $("#frmBandejaFED").kendoValidator().data("kendoValidator");
        frmGenerarPlanillaFED = $("#frmGenerarPlanillaFED").kendoValidator().data("kendoValidator");
        $("#ddlMesFED_GenPlan").kendoDropDownList({
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
        $("#ddlAnioFED_GenPlan").kendoDropDownList({
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
        $("#ddlMesFED_BusqPlan").kendoDropDownList({
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
        $("#ddlAnioFED_BusqPlan").kendoDropDownList({
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
        $("#optional").kendoListBox({
            autoBind: true,
            connectWith: "selected",
            dropSources: ["selected"],
            draggable: {
                enabled: true,
                placeholder: function (element) {
                    return element.clone().css({
                        "opacity": 0.3,
                        "border": "1px dashed #000000"
                    });
                }
            },
            toolbar: {
                tools: ["transferTo", "transferFrom", "transferAllTo", "transferAllFrom"]
            }
        });

        $("#selected").kendoListBox({
            dropSources: ["optional"],
            //connectWith: "optional",
            draggable: {
                enabled: true,
                placeholder: function (element) {
                    return element.clone().css({
                        "opacity": 0.3,
                        "border": "1px dashed #000000"
                    });
                }
            }
        });
        //var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
        //var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
        //debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //    this.CargarBandejaPrincipalPlanillaCAS(event);
        //}
        //else {
        //    this.CargarBandejaPrincipalPlanillaCASVacio(event);
        //}
        //this.CargarBandejaPrincipalPlanillaCAS(event);
        //controlador.CargarBandejaPrincipalPlanillaCASVacio();        
        $("#divGridBandejaPlanillaFED").empty();
        $("#divGridBandejaPlanillaFEDVacio").empty();
        $("#divGridBandejaPlanillaFEDVacio").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "bExoneracionRenta4ta",
                        title: "EXO. 4TA",
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var tipo = '';
                            if (item.bExoneracionRenta4ta == true) tipo = "S";
                            if (item.bExoneracionRenta4ta == false) tipo = "N";

                            return tipo;
                        }
                    },
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasDescansos_Subsidios",
                        title: "DIAS D/S",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "R. BÁSICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.3. 2 8. 1 2)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaFEDVacio = $("#divGridBandejaPlanillaFEDVacio").data("kendoGrid");
        divGridBandejaPlanillaFEDVacio.wrapper.css("display", "block");
    }
       
    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFEDValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaFED.validate()) {
            var iMesBusqPlan4ta = $('#ddlMesFED_BusqPlan').data("kendoDropDownList").value();
            var iAnioBusqPlan4ta = $('#ddlAnioFED_BusqPlan').data("kendoDropDownList").value();
            var metasTemp = "";
            var list = $("#selected").data("kendoListBox");
            if (list.dataSource.data().length > 0) {
                for (var i = 0; i < list.dataSource.data().length; i++) {
                    debugger;
                    if (i < list.dataSource.data().length - 1) {
                        metasTemp += list.dataSource._data[i].value + ",";
                    }
                    else {
                        metasTemp += list.dataSource._data[i].value;
                    }
                }
            }
            $("#hdidCodFuenteFinanciamientoFED").val(0);
            $("#hdvMetasTempFED").val(metasTemp);
            $("#hdiMesBusqPlan4taFED").val(iMesBusqPlan4ta);
            $("#hdiAnioBusqPlan4taFED").val(iAnioBusqPlan4ta);

            var iCodPlanilla = '1';
            var iCodTipoPlanilla = '1';
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            data_param.append('idCodFuenteFinanciamiento', 0);
            data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlan4ta);
            data_param.append('iAnio', iAnioBusqPlan4ta);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFED',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaFED").empty();
                                    $("#divGridBandejaPlanillaFEDVacio").empty();
                                    controlador.CargarBandejaPrincipalPlanillaFED();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaFED").data("kendoGrid");
                                    divGridBandejaPlanillaCAS.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaFEDVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaFEDVacio").data("kendoGrid");
                                    GridBandejaPlanillaCASVacio.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaFED").empty();
                                    $("#divGridBandejaPlanillaFEDVacio").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotalFED").html(res.length);
                                    $("#divGridBandejaPlanillaFEDVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaFED").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "bExoneracionRenta4ta",
                                                title: "EXO. 4TA",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" },
                                                template: function (item) {
                                                    var tipo = '';
                                                    if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                    if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                    return tipo;
                                                }
                                            },
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasDescansos_Subsidios",
                                                title: "DIAS D/S",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "R. BÁSICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.3. 2 8. 1 2)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaFED = $("#divGridBandejaPlanillaFED").data("kendoGrid");
                                    divGridBandejaPlanillaFED.wrapper.css("display", "none");
                                    var GridBandejaPlanillaFEDVacio = $("#divGridBandejaPlanillaFEDVacio").data("kendoGrid");
                                    GridBandejaPlanillaFEDVacio.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFED = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan4ta = $('#ddlMesFED_BusqPlan').data("kendoDropDownList").value();
        var iAnioBusqPlan4ta = $('#ddlAnioFED_BusqPlan').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFED',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.idCodFuenteFinanciamiento = 0;
                        var list = $("#selected").data("kendoListBox");
                        if (list.dataSource.data().length > 0) {
                            var metasTemp = "";
                            for (var i = 0; i < list.dataSource.data().length; i++) {
                                debugger;
                                if (i < list.dataSource.data().length - 1) {
                                    metasTemp += list.dataSource._data[i].value + ",";
                                }
                                else {
                                    metasTemp += list.dataSource._data[i].value;
                                }
                            }
                        }
                        //alert(metasTemp);

                        data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesFED_BusqPlan').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioFED_BusqPlan').data("kendoDropDownList").value();
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
                $("#lblTotalFED").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {
                        iCodTrabajador: { type: "number" },
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" },
                        sRegimenPensionario: { type: "string" },
                        bExoneracionRenta4ta: { type: "bool" },
                        iDiasLaborados: { type: "number" },
                        iDiasVacaciones: { type: "number" },
                        iDiasLicencias: { type: "number" },
                        iDiasDescansos_Subsidios: { type: "number" },
                        dcMontoRemuneracionBasica: { type: "number" },
                        dcMontoTotalIngresos: { type: "number" },
                        dcMontoTotalDescuentos: { type: "number" },
                        dcMontoTotalNeto: { type: "number" },
                        dcMontoAporteEsSalud: { type: "number" },
                        iMes: { type: "number" },
                        iAnio: { type: "number" }
                    }
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
        this.$grid = $("#divGridBandejaPlanillaFED").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Planilla FED.xlsx",
                filterable: false,
                proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaFED"
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitFED,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasDescansos_Subsidios",
                    title: "DIAS D/S",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD <br />(2.3. 2 8. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Modificar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio, "2", "1", item.sNombreCompleto];
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();


        //debugger;
        //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
        //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
        //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
        // }
        //}
        $('#filter').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaFED').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
        debugger;
    };
    function detailInitFED(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridIngresosFED").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosFED',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoContraprestacion",
                    title: "CONTRAPRESTACION (2.3. 2 8. 1 1)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoContraprescionVacaional",
                    title: "CONTRAPRESTACION VACACIONAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoCopagoSubsidio",
                    title: "COPAGO SUBSIDIO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.3. 2 8. 1 4)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }

            ]
        }); //.data()
        detailRow.find(".divGridDescuentosFED").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosFED',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoTardanzas",
                    title: "TARDANZAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoInasistencias",
                    title: "INASISTENCIAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoSalidasAnticipadas",
                    title: "SAL. ANTIC.",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta4ta",
                    title: "IMP. RTA 4TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEsSaludMasVida",
                    title: "EsSALUD MAS VIDA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFEDVacio = function (e) {
        //e.preventDefault();
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFED',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.idCodFuenteFinanciamiento = 0;
                        var list = $("#selected").data("kendoListBox");
                        if (list.dataSource.data().length > 0) {
                            var metasTemp = "";
                            for (var i = 0; i < list.dataSource.data().length; i++) {
                                debugger;
                                if (i < list.dataSource.data().length - 1) {
                                    metasTemp += list.dataSource._data[i].value + ",";
                                }
                                else {
                                    metasTemp += list.dataSource._data[i].value;
                                }
                            }
                        }
                        //alert(metasTemp);

                        data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesFED_BusqPlan').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioFED_BusqPlan').data("kendoDropDownList").value();
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
                $("#lblTotalFED").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador"
                }
            },
        });
        debugger;
        this.$grid = $("#divGridBandejaPlanillaFEDVacio").kendoGrid({
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
            //detailTemplate: kendo.template($("#template").html()),
            //detailInit: detailInit,
            dataType: 'json',
            //dataBound: function () {
            //    //debugger;
            //    //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasDescansos_Subsidios",
                    title: "DIAS D/S",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD (2.3. 2 8. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                }
            ]
        }).data();

    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaFED = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamientoFED").val();
        var iMesBusqPlan4ta = $("#hdiMesBusqPlan4taFED").val();
        var iAnioBusqPlan4ta = $("#hdiAnioBusqPlan4taFED").val();
        var metasTemp = $("#hdvMetasTempFED").val();
        var cantReg = $("#lblTotalFED").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaFED').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaFED',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlan4ta;
                                data_param.iAnio = iAnioBusqPlan4ta;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                bExoneracionRenta4ta: { type: "bool" },
                                iDiasLaborados: { type: "number" },
                                iDiasVacaciones: { type: "number" },
                                iDiasLicencias: { type: "number" },
                                iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoTotalIngresos: { type: "number" },

                                dcMontoTardanzas: { type: "number" },
                                dcMontoPermisos: { type: "number" },
                                dcMontoSalidasAnticipadas: { type: "number" },
                                dcMontoImpuestoRenta4ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" },
                                sNombreFuenteFinanciamiento: { type: "string" },
                                sNombreMeta: { type: "string" },
                                sBanco: { type: "string" },
                                sCuenta: { type: "string" },
                                sCCI: { type: "string" }
                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 38
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 11,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 6,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 14,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 4,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 3,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Ex. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Laborados",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Vacaciones",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Licencias",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Descansos y/o Subsidios",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Contraprestacion (2.3. 2 8. 1 1)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Contraprescion Vacacional",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. CopagoSubsidio",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aguinaldos (2.3. 2 8. 1 4)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Tardanzas",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Sal. Antic.",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EsSaludMasVida",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.3. 2 8. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "Fuente Financiamiento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Meta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Cta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "CCI",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              { value: data[i].bExoneracionRenta4ta },
                              { value: data[i].iDiasLaborados },
                              { value: data[i].iDiasVacaciones },
                              { value: data[i].iDiasLicencias },
                              { value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" },
                              { value: data[i].sNombreFuenteFinanciamiento },
                              { value: data[i].sNombreMeta },
                              { value: data[i].sBanco },
                              { value: data[i].sCuenta },
                              { value: data[i].sCCI }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla FED",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla General Completa FED.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }

    this.PlanillasJS.prototype.GenerarPlanillaFED = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaFED';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;

        if (frmGenerarPlanillaFED.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            var iMes = $('#ddlMesFED_GenPlan').data("kendoDropDownList").value();
            var iAnio = $('#ddlAnioFED_GenPlan').data("kendoDropDownList").value();
            var iCodPlanilla = '2';
            var iCodTipoPlanilla = '1';
            data_param.append('iMes', iMes);
            data_param.append('iAnio', iAnio);
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
            debugger;
            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
            //var existe = controlador.ConsultarEjecucionPlanilla(items);
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res > 0) {
                        if (res == 2) {
                            controladorApp.abrirMensajeDeConfirmacion(
                            '¿Está seguro de generar Planilla FED?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                            controladorApp.notificarMensajeSatisfactorio("La planilla FED se ha generado correctamente");
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
                            controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                        }
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                    }

                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
            debugger;
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }


    }

    ////////////////////////////BANDEJA PLANILLA ADICIONAL REGIMEN CAS//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaCASAdicional = function () {
        debugger;
        frmGenerarPlanillaCASAdicional = $("#frmGenerarPlanillaCASAdicional").kendoValidator().data("kendoValidator");
        frmBandejaCASAdicional = $("#frmBandejaCASAdicional").kendoValidator().data("kendoValidator");
        $("#ddlMesCAS_GenPlanAdicional").kendoDropDownList({
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
        $("#ddlAnioCAS_GenPlanAdicional").kendoDropDownList({
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
        $("#ddlMesCAS_BusqPlanAdicional").kendoDropDownList({
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
        $("#ddlAnioCAS_BusqPlanAdicional").kendoDropDownList({
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
            
        $("#divGridBandejaPlanillaCASAdicional").empty();
        $("#divGridBandejaPlanillaCASVacioAdicional").empty();
        $("#divGridBandejaPlanillaCASVacioAdicional").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "bExoneracionRenta4ta",
                        title: "EXO. 4TA",
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var tipo = '';
                            if (item.bExoneracionRenta4ta == true) tipo = "S";
                            if (item.bExoneracionRenta4ta == false) tipo = "N";

                            return tipo;
                        }
                    },
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasDescansos_Subsidios",
                        title: "DIAS D/S",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "R. BÁSICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.3. 2 8. 1 2)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaCASVacioAdicional = $("#divGridBandejaPlanillaCASVacioAdicional").data("kendoGrid");
        divGridBandejaPlanillaCASVacioAdicional.wrapper.css("display", "block");

        //controlador.ListarEmpleadosPlanilla(event);
        controlador.CargarEmpleadosPlanillaAdicioalTemporal(event);
    }

    this.PlanillasJS.prototype.ListarEmpleadosPlanilla = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarEmpleadosPlanilla',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation === "read") {
                        data_param.strCodTipoCondicionTrabajador = '1,2,3,6';
                                                
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
            pageSize: 10,
            //change: function (e) {
            //    //$("#lblTotal").html(this.total());
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
                    id: "IdEmpleado",
                    fields: {
                        IdEmpleado: { type: "number" },
                        TipoDocumento: { type: "string" },
                        NroDocumento: { type: "string" },
                        NombreCompleto: { type: "string" },
                        NombreOficina: { type: "string" }
                    }
                }
            }
        });
        debugger;
        this.$grid = $("#divAgregarTrabajador").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: true,            
            //groupable: true,
            //filterable: true,
            dataType: 'json',
            columns: [
                {
                    field: "IdEmpleado",
                    title: "IdEmpleado",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "TipoDocumento",
                    title: "Tipo Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NroDocumento",
                    title: "Nro Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreCompleto",
                    title: "Apellidos y Nombres",
                    width: "150px"
                },
                {
                    field: "NombreOficina",
                    title: "Dependencia",
                    width: "300px"
                },                              
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.IdEmpleado, item.NombreCompleto];

                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.AgregarTrabajadorPlanillaAdicional(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-plus" aria-hidden="true" data-uib-tooltip="Ver" title="Agregar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
        $('#filterAdicional').on('input', function (e) {
            var grid = $('#divAgregarTrabajador').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
    };
    

    this.PlanillasJS.prototype.abrirModalAgregarTrabajadorCASAdicional = function (e) {
        e.preventDefault();
        debugger;
        var modal = $('#ModalAgregarTrabajador').data('kendoWindow');

        modal.title("Buscar Trabajadores");
        controlador.ListarEmpleadosPlanilla(event);
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarTrabajadorCASAdicional = function () {
        debugger;
        var modal = $('#ModalAgregarTrabajador').data('kendoWindow');
        modal.close();
    }
    $('#ModalAgregarTrabajador').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Buscar Trabajadores',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {            
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.CargarEmpleadosPlanillaAdicioalTemporal = function (e) {
        //e.preventDefault();

        debugger;
        //if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/CargarEmpleadosPlanillaAdicioalTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {                            
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresAdicional").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";


                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        //}

        debugger;
    };
    this.PlanillasJS.prototype.AgregarTrabajadorPlanillaAdicional = function (item) {
        //e.preventDefault();

        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];
            items.strNombreCompleto = _item[1] + " " + _item[2];
            
        }
        debugger;
        var gridTra = $("#divGridTrabajadoresAdicional").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        if (gridTra.length > 0) {            
            for (var i = 0; i < gridTra.length; i++) {
                debugger;
                if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
                    controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
                    return;
                }
                
            }
        }
        if (items.iCodTrabajador!="") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/AgregarEmpleadosPlanillaAdicioalTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = items.iCodTrabajador;
                            data_param.strNombreCompleto = items.strNombreCompleto; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresAdicional").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                        
                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";
                            

                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }
        
        debugger;
    };

    this.PlanillasJS.prototype.QuitarEmpleadosPlanillaAdicioalTemporal = function (id) {
        //e.preventDefault();

        debugger;
        if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/QuitarEmpleadosPlanillaAdicioalTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = id;                                                
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresAdicional").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";
                            

                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }

        debugger;
    };

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASAdicionalValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaCASAdicional.validate()) {
            var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlanAdicional').data("kendoDropDownList").value();
            var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlanAdicional').data("kendoDropDownList").value();
            //var metasTemp = "";
            //var list = $("#selected").data("kendoListBox");
            //if (list.dataSource.data().length > 0) {
            //    for (var i = 0; i < list.dataSource.data().length; i++) {
            //        debugger;
            //        if (i < list.dataSource.data().length - 1) {
            //            metasTemp += list.dataSource._data[i].value + ",";
            //        }
            //        else {
            //            metasTemp += list.dataSource._data[i].value;
            //        }
            //    }
            //}
            //$("#hdidCodFuenteFinanciamiento").val(0);
            //$("#hdvMetasTemp").val(metasTemp);
            $("#hdiMesBusqPlan4taAdicional").val(iMesBusqPlan4ta);
            $("#hdiAnioBusqPlan4taAdicional").val(iAnioBusqPlan4ta);

            var iCodPlanilla = '1';
            var iCodTipoPlanilla = '2';
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            //data_param.append('idCodFuenteFinanciamiento', 0);
            //data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlan4ta);
            data_param.append('iAnio', iAnioBusqPlan4ta);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCASAdicional',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaCASAdicional").empty();
                                    $("#divGridBandejaPlanillaCASVacioAdicional").empty();
                                    controlador.CargarBandejaPrincipalPlanillaCASAdicional();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaCASAdicional = $("#divGridBandejaPlanillaCASAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaCASAdicional.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaCASVacioAdicional").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var divGridBandejaPlanillaCASVacioAdicional = $("#divGridBandejaPlanillaCASVacioAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacioAdicional.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaCASAdicional").empty();
                                    $("#divGridBandejaPlanillaCASVacioAdicional").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotal").html(res.length);
                                    $("#divGridBandejaPlanillaCASVacioAdicional").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasDescansos_Subsidios",
                                                    title: "DIAS D/S",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaCASAdicional").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "bExoneracionRenta4ta",
                                                title: "EXO. 4TA",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" },
                                                template: function (item) {
                                                    var tipo = '';
                                                    if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                    if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                    return tipo;
                                                }
                                            },
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasDescansos_Subsidios",
                                                title: "DIAS D/S",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "R. BÁSICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.3. 2 8. 1 2)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaCASAdicional = $("#divGridBandejaPlanillaCASAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaCASAdicional.wrapper.css("display", "none");
                                    var divGridBandejaPlanillaCASVacioAdicional = $("#divGridBandejaPlanillaCASVacioAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacioAdicional.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASAdicional = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlanAdicional').data("kendoDropDownList").value();
        var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlanAdicional').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCASAdicional',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        //data_param.idCodFuenteFinanciamiento = 0;
                        //var list = $("#selected").data("kendoListBox");
                        //if (list.dataSource.data().length > 0) {
                        //    var metasTemp = "";
                        //    for (var i = 0; i < list.dataSource.data().length; i++) {
                        //        debugger;
                        //        if (i < list.dataSource.data().length - 1) {
                        //            metasTemp += list.dataSource._data[i].value + ",";
                        //        }
                        //        else {
                        //            metasTemp += list.dataSource._data[i].value;
                        //        }
                        //    }
                        //}
                        //alert(metasTemp);

                        //data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesCAS_BusqPlanAdicional').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioCAS_BusqPlanAdicional').data("kendoDropDownList").value();
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
                $("#lblTotalBusqAdicional").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {
                        iCodTrabajador: { type: "number" },
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" },
                        sRegimenPensionario: { type: "string" },
                        bExoneracionRenta4ta: { type: "bool" },
                        iDiasLaborados: { type: "number" },
                        iDiasVacaciones: { type: "number" },
                        iDiasLicencias: { type: "number" },
                        iDiasDescansos_Subsidios: { type: "number" },
                        dcMontoRemuneracionBasica: { type: "number" },
                        dcMontoTotalIngresos: { type: "number" },
                        dcMontoTotalDescuentos: { type: "number" },
                        dcMontoTotalNeto: { type: "number" },
                        dcMontoAporteEsSalud: { type: "number" },
                        iMes: { type: "number" },
                        iAnio: { type: "number" }
                    }
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
        this.$grid = $("#divGridBandejaPlanillaCASAdicional").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Planilla CAS.xlsx",
                filterable: false,
                proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaCASAdicional"
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit1,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasDescansos_Subsidios",
                    title: "DIAS D/S",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD <br />(2.3. 2 8. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Modificar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio, "1", "2", item.sNombreCompleto];
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();


        //debugger;
        //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
        //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
        //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
        // }
        //}
        $('#filterBusqAdicional').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaCASAdicional').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
        debugger;
    };
    function detailInit1(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridIngresosAdicional").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosCASAdicional',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoContraprestacion",
                    title: "CONTRAPRESTACION (2.3. 2 8. 1 1)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoContraprescionVacaional",
                    title: "CONTRAPRESTACION VACACIONAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoCopagoSubsidio",
                    title: "COPAGO SUBSIDIO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.3. 2 8. 1 4)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }

            ]
        }); //.data()
        detailRow.find(".divGridDescuentosAdicional").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosCASAdicional',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoTardanzas",
                    title: "TARDANZAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoInasistencias",
                    title: "INASISTENCIAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoSalidasAnticipadas",
                    title: "SAL. ANTIC.",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta4ta",
                    title: "IMP. RTA 4TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEsSaludMasVida",
                    title: "EsSALUD MAS VIDA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaCASAdicional = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        //var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamientoAdicional").val();
        var iMesBusqPlan4ta = $("#hdiMesBusqPlan4taAdicional").val();
        var iAnioBusqPlan4ta = $("#hdiAnioBusqPlan4taAdicional").val();
        //var metasTemp = $("#hdvMetasTemp").val();
        var cantReg = $("#lblTotalBusqAdicional").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaCASAdicional').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaCASAdicional',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                //data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                //data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlan4ta;
                                data_param.iAnio = iAnioBusqPlan4ta;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                bExoneracionRenta4ta: { type: "bool" },
                                iDiasLaborados: { type: "number" },
                                iDiasVacaciones: { type: "number" },
                                iDiasLicencias: { type: "number" },
                                iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoTotalIngresos: { type: "number" },

                                dcMontoTardanzas: { type: "number" },
                                dcMontoPermisos: { type: "number" },
                                dcMontoSalidasAnticipadas: { type: "number" },
                                dcMontoImpuestoRenta4ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" }
                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 33
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 32,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 32
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 11,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 6,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 14,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 2,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Ex. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Laborados",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Vacaciones",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Licencias",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Descansos y/o Subsidios",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Contraprestacion (2.3. 2 8. 1 1)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Contraprescion Vacacional",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. CopagoSubsidio",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aguinaldos (2.3. 2 8. 1 4)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Tardanzas",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Sal. Antic.",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EsSaludMasVida",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.3. 2 8. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              { value: data[i].bExoneracionRenta4ta },
                              { value: data[i].iDiasLaborados },
                              { value: data[i].iDiasVacaciones },
                              { value: data[i].iDiasLicencias },
                              { value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoSalidasAnticipadas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla CAS Adicional",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla Adicional CAS.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }

    this.PlanillasJS.prototype.GenerarPlanillaCASAdicional = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaCASAdicional';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        //var gridTra = $("#divGridTrabajadoresAdicional").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        //if (gridTra.length > 0) {
            if (frmGenerarPlanillaCASAdicional.validate()) {
                var esValido = true;
                var mensajeValidacion = '';
                var data_param = new FormData();
                var iMes = $('#ddlMesCAS_GenPlanAdicional').data("kendoDropDownList").value();
                var iAnio = $('#ddlAnioCAS_GenPlanAdicional').data("kendoDropDownList").value();
                var iCodPlanilla = '1';
                var iCodTipoPlanilla = '2';
                //if (gridTra.length > 0) {
                //    var idsTrabajadoresTemp = "";
                //    for (var i = 0; i < gridTra.length; i++) {
                //        debugger;
                //        if (i < gridTra.length - 1) {
                //            idsTrabajadoresTemp += gridTra[i].IdEmpleado + ",";
                //        }
                //        else {
                //            idsTrabajadoresTemp += gridTra[i].IdEmpleado;
                //        }
                //    }                    
                //}
                debugger;
                data_param.append('iMes', iMes);
                data_param.append('iAnio', iAnio);
                //data_param.append('iAnio', '2020');
                data_param.append('iCodPlanilla', iCodPlanilla);
                data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
                //data_param.append('strCodTrabajadores', idsTrabajadoresTemp);


                //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
                //var existe = controlador.ConsultarEjecucionPlanilla(items);
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data_param,
                    success: function (res) {
                        debugger;
                        //return res;
                        if (res > 0) {
                            if (res == 2) {
                                controladorApp.abrirMensajeDeConfirmacion(
                                '¿Está seguro de generar Planilla CAS Adicional?', 'SI', 'NO'
                                , function (arg) {
                                    $.ajax({
                                        url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                                $("#divGridTrabajadoresAdicional").empty();
                                                controladorApp.notificarMensajeSatisfactorio("La planilla CAS se ha generado correctamente");
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
                                controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                            }
                        }
                        else {
                            controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                        }

                    },
                    error: function (res) {
                        //alert(res);
                        debugger;
                    }
                });
                debugger;
            }
            else {
                controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
            }
        //}
        //else {
        //    controladorApp.notificarMensajeDeAlerta('Debe agregrar trabajadores para procesar la planilla adicional');
        //}



    }
    //this.PlanillasJS.prototype.GenerarPlanillaCASAdicional = function (e) {
    //    debugger;

    //    var metodo = 'GenerarPlanillaCASAdicional';
    //    //controladorApp.notificarMensajeDeAlerta('hola');
    //    //if (frmPerfilesValidador.validate()) {
    //    var iCodBasePerfil = 0;
    //    var iCodPerfil = 0;
    //    var gridTra = $("#divGridTrabajadoresAdicional").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
    //    if (gridTra.length > 0) {
    //        if (frmGenerarPlanillaCASAdicional.validate()) {
    //            var esValido = true;
    //            var mensajeValidacion = '';
    //            var data_param = new FormData();
    //            var iMes = $('#ddlMesCAS_GenPlanAdicional').data("kendoDropDownList").value();
    //            var iAnio = $('#ddlAnioCAS_GenPlanAdicional').data("kendoDropDownList").value();
    //            var iCodPlanilla = '1';
    //            var iCodTipoPlanilla = '2';
    //            if (gridTra.length > 0) {
    //                var idsTrabajadoresTemp = "";
    //                for (var i = 0; i < gridTra.length; i++) {
    //                    debugger;
    //                    if (i < gridTra.length - 1) {
    //                        idsTrabajadoresTemp += gridTra[i].IdEmpleado + ",";
    //                    }
    //                    else {
    //                        idsTrabajadoresTemp += gridTra[i].IdEmpleado;
    //                    }
    //                }
    //                //for (var i = 0; i < gridTra.length; i++) {
    //                //    debugger;
    //                //    if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
    //                //        controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
    //                //        return;
    //                //    }

    //                //}
    //            }
    //            debugger;
    //            data_param.append('iMes', iMes);
    //            data_param.append('iAnio', iAnio);
    //            //data_param.append('iAnio', '2020');
    //            data_param.append('iCodPlanilla', iCodPlanilla);
    //            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
    //            data_param.append('strCodTrabajadores', idsTrabajadoresTemp);
                
                
    //            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
    //            //var existe = controlador.ConsultarEjecucionPlanilla(items);
    //            $.ajax({
    //                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
    //                type: 'POST',
    //                dataType: 'json',
    //                contentType: false,
    //                processData: false,
    //                data: data_param,
    //                success: function (res) {
    //                    debugger;
    //                    //return res;
    //                    if (res > 0) {
    //                        if (res == 2) {
    //                            controladorApp.abrirMensajeDeConfirmacion(
    //                            '¿Está seguro de generar Planilla CAS Adicional?', 'SI', 'NO'
    //                            , function (arg) {
    //                                $.ajax({
    //                                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
    //                                    type: 'POST',
    //                                    dataType: 'json',
    //                                    contentType: false,
    //                                    processData: false,
    //                                    data: arg,
    //                                    success: function (res) {
    //                                        debugger;
    //                                        if (res.success == 'False') {
    //                                            controladorApp.notificarMensajeDeAlerta(res.responseText);
    //                                        }
    //                                        else {
    //                                            //controlador.GenerarPDFConvocatorias(item);
    //                                            $("#divGridTrabajadoresAdicional").empty();
    //                                            controladorApp.notificarMensajeSatisfactorio("La planilla CAS se ha generado correctamente");
    //                                            //controlador.inicializarBandejaUser();                                
    //                                            //controlador.CargarBandejaPrincipalPlanillaCAS(event);
    //                                            // REFRESCAR INFORMACION DEL TRABAJADOR

    //                                        }
    //                                    },
    //                                    error: function (res) {
    //                                        //alert(res);
    //                                    }
    //                                });
    //                            }, data_param);

    //                        }
    //                        else {
    //                            controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
    //                        }
    //                    }
    //                    else {
    //                        controladorApp.notificarMensajeDeAlerta('La planilla no existe');
    //                    }

    //                },
    //                error: function (res) {
    //                    //alert(res);
    //                    debugger;
    //                }
    //            });
    //            debugger;
    //        }
    //        else {
    //            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
    //        }
    //    }
    //    else {
    //        controladorApp.notificarMensajeDeAlerta('Debe agregrar trabajadores para procesar la planilla adicional');
    //    }
        


    //}

    ////////////////////////////BANDEJA PLANILLA REGIMEN FUNCIONARIOS//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaFUNC = function () {
        debugger;
        frmBandejaFUNC = $("#frmBandejaFUNC").kendoValidator().data("kendoValidator");
        frmGenerarPlanillaFUNC = $("#frmGenerarPlanillaFUNC").kendoValidator().data("kendoValidator");
        $("#ddlMesFUNC_GenPlan").kendoDropDownList({
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
        $("#ddlAnioFUNC_GenPlan").kendoDropDownList({
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
        $("#ddlMesFUNC_BusqPlan").kendoDropDownList({
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
        $("#ddlAnioFUNC_BusqPlan").kendoDropDownList({
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
        
        $("#divGridBandejaPlanillaFUNC").empty();
        $("#divGridBandejaPlanillaFUNCVacio").empty();
        $("#divGridBandejaPlanillaFUNCVacio").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    //{
                    //    field: "bExoneracionRenta4ta",
                    //    title: "EXO. 4TA",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" },
                    //    template: function (item) {
                    //        var tipo = '';
                    //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                    //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                    //        return tipo;
                    //    }
                    //},
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    ////{
                    ////    field: "iDiasDescansos_Subsidios",
                    ////    title: "DIAS D/S",
                    ////    width: "30px",
                    ////    attributes: { style: "text-align:center;" }
                    ////},
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "COMP. ECONÓMICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.1. 3 1. 1 5)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaFUNCVacio = $("#divGridBandejaPlanillaFUNCVacio").data("kendoGrid");
        divGridBandejaPlanillaFUNCVacio.wrapper.css("display", "block");
    }
    
    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFUNCValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaFUNC.validate()) {
            var iMesBusqPlanFunc = $('#ddlMesFUNC_BusqPlan').data("kendoDropDownList").value();
            var iAnioBusqPlanFunc = $('#ddlAnioFUNC_BusqPlan').data("kendoDropDownList").value();
            
            //$("#hdidCodFuenteFinanciamiento").val(0);
            //$("#hdvMetasTemp").val(metasTemp);
            $("#hdiMesBusqPlanFUNC").val(iMesBusqPlanFunc);
            $("#hdiAnioBusqPlanFUNC").val(iAnioBusqPlanFunc);

            var iCodPlanilla = '5';
            var iCodTipoPlanilla = '1';
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            //data_param.append('idCodFuenteFinanciamiento', 0);
            //data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlanFunc);
            data_param.append('iAnio', iAnioBusqPlanFunc);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFUNC',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaFUNC").empty();
                                    $("#divGridBandejaPlanillaFUNCVacio").empty();                                    
                                    controlador.CargarBandejaPrincipalPlanillaFUNC();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaFUNC = $("#divGridBandejaPlanillaFUNC").data("kendoGrid");
                                    divGridBandejaPlanillaFUNC.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaFUNCVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "bExoneracionRenta4ta",
                                                //    title: "EXO. 4TA",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" },
                                                //    template: function (item) {
                                                //        var tipo = '';
                                                //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                //        return tipo;
                                                //    }
                                                //},
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "iDiasDescansos_Subsidios",
                                                //    title: "DIAS D/S",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "COMP. ECONÓMICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.1. 3 1. 1 5)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var divGridBandejaPlanillaFUNCVacio = $("#divGridBandejaPlanillaFUNCVacio").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCVacio.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaFUNC").empty();
                                    $("#divGridBandejaPlanillaFUNCVacio").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotalFunc").html(res.length);
                                    $("#divGridBandejaPlanillaFUNCVacio").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "bExoneracionRenta4ta",
                                                //    title: "EXO. 4TA",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" },
                                                //    template: function (item) {
                                                //        var tipo = '';
                                                //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                //        return tipo;
                                                //    }
                                                //},
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "iDiasDescansos_Subsidios",
                                                //    title: "DIAS D/S",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "COMP. ECONÓMICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.1. 3 1. 1 5)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaFUNC").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            //{
                                            //    field: "bExoneracionRenta4ta",
                                            //    title: "EXO. 4TA",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" },
                                            //    template: function (item) {
                                            //        var tipo = '';
                                            //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                            //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                            //        return tipo;
                                            //    }
                                            //},
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            //{
                                            //    field: "iDiasDescansos_Subsidios",
                                            //    title: "DIAS D/S",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "COMP. ECONÓMICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.1. 3 1. 1 5)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaFUNC = $("#divGridBandejaPlanillaFUNC").data("kendoGrid");
                                    divGridBandejaPlanillaFUNC.wrapper.css("display", "none");
                                    var divGridBandejaPlanillaFUNCVacio = $("#divGridBandejaPlanillaFUNCVacio").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCVacio.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFUNC = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlanFunc = $('#ddlMesFUNC_BusqPlan').data("kendoDropDownList").value();
        var iAnioBusqPlanFunc = $('#ddlAnioFUNC_BusqPlan').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFUNC',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        //data_param.idCodFuenteFinanciamiento = 0;
                        //var list = $("#selected").data("kendoListBox");
                        //if (list.dataSource.data().length > 0) {
                        //    var metasTemp = "";
                        //    for (var i = 0; i < list.dataSource.data().length; i++) {
                        //        debugger;
                        //        if (i < list.dataSource.data().length - 1) {
                        //            metasTemp += list.dataSource._data[i].value + ",";
                        //        }
                        //        else {
                        //            metasTemp += list.dataSource._data[i].value;
                        //        }
                        //    }
                        //}
                        //alert(metasTemp);

                        //data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesFUNC_BusqPlan').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioFUNC_BusqPlan').data("kendoDropDownList").value();
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
                $("#lblTotalFunc").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {
                        iCodTrabajador: { type: "number" },
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" },
                        sRegimenPensionario: { type: "string" },
                        //bExoneracionRenta4ta: { type: "bool" },
                        iDiasLaborados: { type: "number" },
                        iDiasVacaciones: { type: "number" },
                        iDiasLicencias: { type: "number" },
                        //iDiasDescansos_Subsidios: { type: "number" },
                        dcMontoRemuneracionBasica: { type: "number" },
                        dcMontoTotalIngresos: { type: "number" },
                        dcMontoTotalDescuentos: { type: "number" },
                        dcMontoTotalNeto: { type: "number" },
                        dcMontoAporteEsSalud: { type: "number" },
                        iMes: { type: "number" },
                        iAnio: { type: "number" }
                    }
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
        this.$grid = $("#divGridBandejaPlanillaFUNC").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Planilla FUNCIONARIOS.xlsx",
                filterable: false,
                proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaFUNC"
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitFunc,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bExoneracionRenta4ta",
                //    title: "EXO. 4TA",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var tipo = '';
                //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                //        return tipo;
                //    }
                //},
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "iDiasDescansos_Subsidios",
                //    title: "DIAS D/S",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "COMP. ECONÓMICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD <br />(2.1. 3 1. 1 5)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Modificar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio, "5", "1", item.sNombreCompleto];
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();


        //debugger;
        //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
        //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
        //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
        // }
        //}
        $('#filterFUNC').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaFUNC').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
        debugger;
    };
    function detailInitFunc(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridIngresosFUNC").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosFUNC',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoContraprestacion",
                    title: "COMPENSACIÓN ECONÓMICA (2.1. 1 1. 1 7)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoContraprescionVacaional",
                    title: "COMPENSACIÓN ECONÓMICA VACACIONAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoCopagoSubsidio",
                //    title: "COPAGO SUBSIDIO",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.1. 1 9. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoCTS",
                    title: "CTS (2.1. 1 9. 2 1)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }

            ]
        }); //.data()
        detailRow.find(".divGridDescuentosFUNC").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosFUNC',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                //{
                //    field: "dcMontoTardanzas",
                //    title: "TARDANZAS",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                //{
                //    field: "dcMontoInasistencias",
                //    title: "INASISTENCIAS",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta5ta",
                    title: "IMP. RTA 5TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoEsSaludMasVida",
                //    title: "EsSALUD MAS VIDA",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFUNCVacio = function (e) {
        //e.preventDefault();
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.idCodFuenteFinanciamiento = 0;
                        var list = $("#selected").data("kendoListBox");
                        if (list.dataSource.data().length > 0) {
                            var metasTemp = "";
                            for (var i = 0; i < list.dataSource.data().length; i++) {
                                debugger;
                                if (i < list.dataSource.data().length - 1) {
                                    metasTemp += list.dataSource._data[i].value + ",";
                                }
                                else {
                                    metasTemp += list.dataSource._data[i].value;
                                }
                            }
                        }
                        //alert(metasTemp);

                        data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesCAS_BusqPlan').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioCAS_BusqPlan').data("kendoDropDownList").value();
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
                $("#lblTotalFunc").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador"
                }
            },
        });
        debugger;
        this.$grid = $("#divGridBandejaPlanillaFUNCVacio").kendoGrid({
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
            //detailTemplate: kendo.template($("#template").html()),
            //detailInit: detailInit,
            dataType: 'json',
            //dataBound: function () {
            //    //debugger;
            //    //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasDescansos_Subsidios",
                    title: "DIAS D/S",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD (2.1. 3 1. 1 5)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                }
            ]
        }).data();

    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaFUNC = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        //var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamiento").val();
        var iMesBusqPlanFUNC = $("#hdiMesBusqPlanFUNC").val();
        var iAnioBusqPlanFUNC = $("#hdiAnioBusqPlanFUNC").val();
        //var metasTemp = $("#hdvMetasTemp").val();
        var cantReg = $("#lblTotalFunc").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlanFUNC != "" && iAnioBusqPlanFUNC != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaFUNC').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaFUNC',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                //data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                //data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlanFUNC;
                                data_param.iAnio = iAnioBusqPlanFUNC;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                //bExoneracionRenta4ta: { type: "bool" },
                                iDiasLaborados: { type: "number" },
                                iDiasVacaciones: { type: "number" },
                                iDiasLicencias: { type: "number" },
                                //iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                //dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoCTS: { type: "number" },                                
                                dcMontoTotalIngresos: { type: "number" },

                                //dcMontoTardanzas: { type: "number" },
                                //dcMontoPermisos: { type: "number" },
                                dcMontoImpuestoRenta5ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                //dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" },
                                sNombreFuenteFinanciamiento: { type: "string" },
                                sNombreMeta: { type: "string" },
                                sBanco: { type: "string" },
                                sCuenta: { type: "string" },
                                sCCI: { type: "string" }
                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 33
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 33,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 33
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 9,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 6,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 11,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 4,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 3,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "Ex. Renta 4ta",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "D. Laborados",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Vacaciones",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Licencias",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "D. Descansos y/o Subsidios",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Compensación Economica (2.1. 1 1. 1 7)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Compensación Economica Vacaional",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. CopagoSubsidio",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Aguinaldos (2.1. 1 9. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                    {
                        value: "M. CTS (2.1. 1 9. 2 1)",
                        bold: true,
                        background: "#217DAD",
                        vAlign: "center",
                        hAlign: "center",
                        color: "#FFFFFF"
                    },
                    {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      //{
                      //    value: "M. Tardanzas",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 5ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. EsSaludMasVida",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.1. 3 1. 1 5)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "Fuente Financiamiento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Meta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Banco",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Cta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "CCI",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              //{ value: data[i].bExoneracionRenta4ta },
                              { value: data[i].iDiasLaborados },
                              { value: data[i].iDiasVacaciones },
                              { value: data[i].iDiasLicencias },
                              //{ value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCTS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta5ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" },
                              { value: data[i].sNombreFuenteFinanciamiento },
                              { value: data[i].sNombreMeta },
                              { value: data[i].sBanco },
                              { value: data[i].sCuenta },
                              { value: data[i].sCCI }

                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla FUNC",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla General Completa FUNCIONARIOS.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }

    this.PlanillasJS.prototype.GenerarPlanillaFUNC = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaFUNC';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;

        if (frmGenerarPlanillaFUNC.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            var iMes = $('#ddlMesFUNC_GenPlan').data("kendoDropDownList").value();
            var iAnio = $('#ddlAnioFUNC_GenPlan').data("kendoDropDownList").value();
            var iCodPlanilla = '5';
            var iCodTipoPlanilla = '1';
            data_param.append('iMes', iMes);
            data_param.append('iAnio', iAnio);
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
            debugger;
            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
            //var existe = controlador.ConsultarEjecucionPlanilla(items);
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res > 0) {
                        if (res == 2) {
                            controladorApp.abrirMensajeDeConfirmacion(
                            '¿Está seguro de generar Planilla CAS?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                            controladorApp.notificarMensajeSatisfactorio("La planilla FUNCIONARIOS se ha generado correctamente");
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
                            controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                        }
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                    }

                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
            debugger;
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }


    }
    
    ////////////////////////////BANDEJA PLANILLA ADICIONAL REGIMEN FUNCIONARIOS//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaFUNCAdicional = function () {
        debugger;
        frmGenerarPlanillaFUNCAdicional = $("#frmGenerarPlanillaFUNCAdicional").kendoValidator().data("kendoValidator");
        frmBandejaFUNCAdicional = $("#frmBandejaFUNCAdicional").kendoValidator().data("kendoValidator");
        $("#ddlMesFUNC_GenPlanAdicional").kendoDropDownList({
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
        $("#ddlAnioFUNC_GenPlanAdicional").kendoDropDownList({
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
        $("#ddlMesFUNC_BusqPlanAdicional").kendoDropDownList({
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
        $("#ddlAnioFUNC_BusqPlanAdicional").kendoDropDownList({
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

        $("#divGridBandejaPlanillaFUNCAdicional").empty();
        $("#divGridBandejaPlanillaFUNCVacioAdicional").empty();
        $("#divGridBandejaPlanillaFUNCVacioAdicional").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    //{
                    //    field: "bExoneracionRenta4ta",
                    //    title: "EXO. 4TA",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" },
                    //    template: function (item) {
                    //        var tipo = '';
                    //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                    //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                    //        return tipo;
                    //    }
                    //},
                    {
                        field: "iDiasLaborados",
                        title: "DIAS LAB.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasVacaciones",
                        title: "DÍAS VAC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "iDiasLicencias",
                        title: "DÍAS LIC.",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    ////{
                    ////    field: "iDiasDescansos_Subsidios",
                    ////    title: "DIAS D/S",
                    ////    width: "30px",
                    ////    attributes: { style: "text-align:center;" }
                    ////},
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "COMP. ECONÓMICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.1. 3 1. 1 5)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaFUNCVacioAdicional = $("#divGridBandejaPlanillaFUNCVacioAdicional").data("kendoGrid");
        divGridBandejaPlanillaFUNCVacioAdicional.wrapper.css("display", "block");

        //controlador.ListarEmpleadosPlanilla(event);
        controlador.CargarEmpleadosPlanillaAdicioalFUNCTemporal(event);
    }

    this.PlanillasJS.prototype.ListarEmpleadosPlanillaFUNC = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarEmpleadosPlanillaFUNC',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation === "read") {
                        data_param.strCodTipoCondicionTrabajador = '4';

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
            pageSize: 10,
            //change: function (e) {
            //    //$("#lblTotal").html(this.total());
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
                    id: "IdEmpleado",
                    fields: {
                        IdEmpleado: { type: "number" },
                        TipoDocumento: { type: "string" },
                        NroDocumento: { type: "string" },
                        NombreCompleto: { type: "string" },
                        NombreOficina: { type: "string" }
                    }
                }
            }
        });
        debugger;
        this.$grid = $("#divAgregarTrabajadorFUNC").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: true,
            //groupable: true,
            //filterable: true,
            dataType: 'json',
            columns: [
                {
                    field: "IdEmpleado",
                    title: "IdEmpleado",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "TipoDocumento",
                    title: "Tipo Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NroDocumento",
                    title: "Nro Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreCompleto",
                    title: "Apellidos y Nombres",
                    width: "150px"
                },
                {
                    field: "NombreOficina",
                    title: "Dependencia",
                    width: "300px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.IdEmpleado, item.NombreCompleto];

                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.AgregarTrabajadorPlanillaAdicionalFUNC(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-plus" aria-hidden="true" data-uib-tooltip="Ver" title="Agregar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
        $('#filterFUNCAdicional').on('input', function (e) {
            var grid = $('#divAgregarTrabajadorFUNC').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
    };
    
    this.PlanillasJS.prototype.abrirModalAgregarTrabajadorFUNCAdicional = function (e) {
        e.preventDefault();
        debugger;
        var modal = $('#ModalAgregarTrabajadorFUNC').data('kendoWindow');

        modal.title("Buscar Trabajadores");
        controlador.ListarEmpleadosPlanillaFUNC(event);
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarTrabajadorFUNCAdicional = function () {
        debugger;
        var modal = $('#ModalAgregarTrabajadorFUNC').data('kendoWindow');
        modal.close();
    }
    $('#ModalAgregarTrabajadorFUNC').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Buscar Trabajadores',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.CargarEmpleadosPlanillaAdicioalFUNCTemporal = function (e) {
        //e.preventDefault();

        debugger;
        //if (id != "") {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/CargarEmpleadosPlanillaFUNCAdicioalTemporal',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
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
                model: {
                    id: "IdEmpleado"
                }
            }
        });
        debugger;
        this.$grid = $("#divGridTrabajadoresFUNCAdicional").kendoGrid({
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
                    field: "IdEmpleado",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "Nombre",
                    title: "TRABAJADOR",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Quitar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";


                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicionalTemporalFUNC(\'' + item.IdEmpleado + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        //}

        debugger;
    };
    this.PlanillasJS.prototype.AgregarTrabajadorPlanillaAdicionalFUNC = function (item) {
        //e.preventDefault();

        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];
            items.strNombreCompleto = _item[1] + " " + _item[2];

        }
        debugger;
        var gridTra = $("#divGridTrabajadoresFUNCAdicional").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        if (gridTra.length > 0) {
            for (var i = 0; i < gridTra.length; i++) {
                debugger;
                if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
                    controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
                    return;
                }

            }
        }
        if (items.iCodTrabajador != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/AgregarEmpleadosPlanillaFUNCAdicioalTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = items.iCodTrabajador;
                            data_param.strNombreCompleto = items.strNombreCompleto; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresFUNCAdicional").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";


                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicionalTemporalFUNC(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }

        debugger;
    };

    this.PlanillasJS.prototype.QuitarEmpleadosPlanillaAdicionalTemporalFUNC = function (id) {
        //e.preventDefault();

        debugger;
        if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/QuitarEmpleadosPlanillaFUNCAdicionalTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = id;
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresFUNCAdicional").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";


                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }

        debugger;
    };

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFUNCAdicionalValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaFUNCAdicional.validate()) {
            var iMesBusqPlan4ta = $('#ddlMesFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
            var iAnioBusqPlan4ta = $('#ddlAnioFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
            //var metasTemp = "";
            //var list = $("#selected").data("kendoListBox");
            //if (list.dataSource.data().length > 0) {
            //    for (var i = 0; i < list.dataSource.data().length; i++) {
            //        debugger;
            //        if (i < list.dataSource.data().length - 1) {
            //            metasTemp += list.dataSource._data[i].value + ",";
            //        }
            //        else {
            //            metasTemp += list.dataSource._data[i].value;
            //        }
            //    }
            //}
            //$("#hdidCodFuenteFinanciamiento").val(0);
            //$("#hdvMetasTemp").val(metasTemp);
            $("#hdiMesBusqPlanFUNCAdicional").val(iMesBusqPlan4ta);
            $("#hdiAnioBusqPlanFUNCAdicional").val(iAnioBusqPlan4ta);

            var iCodPlanilla = '5';
            var iCodTipoPlanilla = '2';
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            //data_param.append('idCodFuenteFinanciamiento', 0);
            //data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlan4ta);
            data_param.append('iAnio', iAnioBusqPlan4ta);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFUNCAdicional',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaFUNCAdicional").empty();
                                    $("#divGridBandejaPlanillaFUNCVacioAdicional").empty();
                                    controlador.CargarBandejaPrincipalPlanillaFUNCAdicional();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaFUNCAdicional = $("#divGridBandejaPlanillaFUNCAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCAdicional.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaFUNCVacioAdicional").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "bExoneracionRenta4ta",
                                                //    title: "EXO. 4TA",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" },
                                                //    template: function (item) {
                                                //        var tipo = '';
                                                //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                //        return tipo;
                                                //    }
                                                //},
                                                {
                                                    field: "iDiasLaborados",
                                                    title: "DIAS LAB.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasVacaciones",
                                                    title: "DÍAS VAC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "iDiasLicencias",
                                                    title: "DÍAS LIC.",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                //{
                                                //    field: "iDiasDescansos_Subsidios",
                                                //    title: "DIAS D/S",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "COMP. ECONÓMICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.1. 3 1. 1 5)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var divGridBandejaPlanillaFUNCVacioAdicional = $("#divGridBandejaPlanillaFUNCVacioAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCVacioAdicional.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaFUNCAdicional").empty();
                                    $("#divGridBandejaPlanillaFUNCVacioAdicional").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotalBusqFUNCAdicional").html(res.length);
                                    $("#divGridBandejaPlanillaFUNCVacioAdicional").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            //{
                                            //    field: "bExoneracionRenta4ta",
                                            //    title: "EXO. 4TA",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" },
                                            //    template: function (item) {
                                            //        var tipo = '';
                                            //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                            //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                            //        return tipo;
                                            //    }
                                            //},
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            ////{
                                            ////    field: "iDiasDescansos_Subsidios",
                                            ////    title: "DIAS D/S",
                                            ////    width: "30px",
                                            ////    attributes: { style: "text-align:center;" }
                                            ////},
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "COMP. ECONÓMICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.1. 3 1. 1 5)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaFUNCAdicional").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            //{
                                            //    field: "bExoneracionRenta4ta",
                                            //    title: "EXO. 4TA",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" },
                                            //    template: function (item) {
                                            //        var tipo = '';
                                            //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                            //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                            //        return tipo;
                                            //    }
                                            //},
                                            {
                                                field: "iDiasLaborados",
                                                title: "DIAS LAB.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasVacaciones",
                                                title: "DÍAS VAC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "iDiasLicencias",
                                                title: "DÍAS LIC.",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            //{
                                            //    field: "iDiasDescansos_Subsidios",
                                            //    title: "DIAS D/S",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "COMP. ECONÓMICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.1. 3 1. 1 5)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaFUNCAdicional = $("#divGridBandejaPlanillaFUNCAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCAdicional.wrapper.css("display", "none");
                                    var divGridBandejaPlanillaFUNCVacioAdicional = $("#divGridBandejaPlanillaFUNCVacioAdicional").data("kendoGrid");
                                    divGridBandejaPlanillaFUNCVacioAdicional.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaFUNCAdicional = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan4ta = $('#ddlMesFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
        var iAnioBusqPlan4ta = $('#ddlAnioFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralFUNCAdicional',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        //data_param.idCodFuenteFinanciamiento = 0;
                        //var list = $("#selected").data("kendoListBox");
                        //if (list.dataSource.data().length > 0) {
                        //    var metasTemp = "";
                        //    for (var i = 0; i < list.dataSource.data().length; i++) {
                        //        debugger;
                        //        if (i < list.dataSource.data().length - 1) {
                        //            metasTemp += list.dataSource._data[i].value + ",";
                        //        }
                        //        else {
                        //            metasTemp += list.dataSource._data[i].value;
                        //        }
                        //    }
                        //}
                        //alert(metasTemp);

                        //data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioFUNC_BusqPlanAdicional').data("kendoDropDownList").value();
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
                $("#lblTotalBusqFUNCAdicional").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {
                        iCodTrabajador: { type: "number" },
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" },
                        sRegimenPensionario: { type: "string" },
                        //bExoneracionRenta4ta: { type: "bool" },
                        iDiasLaborados: { type: "number" },
                        iDiasVacaciones: { type: "number" },
                        iDiasLicencias: { type: "number" },
                        //iDiasDescansos_Subsidios: { type: "number" },
                        dcMontoRemuneracionBasica: { type: "number" },
                        dcMontoTotalIngresos: { type: "number" },
                        dcMontoTotalDescuentos: { type: "number" },
                        dcMontoTotalNeto: { type: "number" },
                        dcMontoAporteEsSalud: { type: "number" },
                        iMes: { type: "number" },
                        iAnio: { type: "number" }
                    }
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
        this.$grid = $("#divGridBandejaPlanillaFUNCAdicional").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Planilla Adicional Funcionarios .xlsx",
                filterable: false,
                proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaFUNCAdicional"
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitFuncAdicional,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bExoneracionRenta4ta",
                //    title: "EXO. 4TA",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var tipo = '';
                //        if (item.bExoneracionRenta4ta == true) tipo = "S";
                //        if (item.bExoneracionRenta4ta == false) tipo = "N";

                //        return tipo;
                //    }
                //},
                {
                    field: "iDiasLaborados",
                    title: "DIAS LAB.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasVacaciones",
                    title: "DÍAS VAC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iDiasLicencias",
                    title: "DÍAS LIC.",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "iDiasDescansos_Subsidios",
                //    title: "DIAS D/S",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "COMP. ECONÓMICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD <br />(2.1. 3 1. 1 5)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Modificar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio, "5", "2", item.sNombreCompleto];
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();


        //debugger;
        //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
        //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
        //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
        // }
        //}
        $('#filterBusqFUNCAdicional').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaFUNCAdicional').data('kendoGrid'); 
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
        debugger;
    };
    function detailInitFuncAdicional(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridIngresosFUNCAdicional").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosFUNCAdicional',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoContraprestacion",
                    title: "COMPENSACIÓN ECONÓMICA (2.1. 1 1. 1 7)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoContraprescionVacaional",
                    title: "COMPENSACIÓN ECONÓMICA VACACIONAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoCopagoSubsidio",
                //    title: "COPAGO SUBSIDIO",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.1. 1 9. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoCTS",
                    title: "CTS (2.1. 1 9. 2 1)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }

            ]
        }); //.data()
        detailRow.find(".divGridDescuentosFUNCAdicional").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosFUNCAdicional',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                //{
                //    field: "dcMontoTardanzas",
                //    title: "TARDANZAS",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                //{
                //    field: "dcMontoInasistencias",
                //    title: "INASISTENCIAS",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta5ta",
                    title: "IMP. RTA 5TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoEsSaludMasVida",
                //    title: "EsSALUD MAS VIDA",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaFUNCAdicional = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        //var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamientoAdicional").val();
        var iMesBusqPlan4ta = $("#hdiMesBusqPlanFUNCAdicional").val();
        var iAnioBusqPlan4ta = $("#hdiAnioBusqPlanFUNCAdicional").val();
        //var metasTemp = $("#hdvMetasTemp").val();
        var cantReg = $("#lblTotalBusqFUNCAdicional").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaFUNCAdicional').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaFUNCAdicional',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                //data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                //data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlan4ta;
                                data_param.iAnio = iAnioBusqPlan4ta;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                //bExoneracionRenta4ta: { type: "bool" },
                                iDiasLaborados: { type: "number" },
                                iDiasVacaciones: { type: "number" },
                                iDiasLicencias: { type: "number" },
                                //iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                //dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoCTS: { type: "number" },                                
                                dcMontoTotalIngresos: { type: "number" },

                                //dcMontoTardanzas: { type: "number" },
                                //dcMontoPermisos: { type: "number" },
                                dcMontoImpuestoRenta5ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                //dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" }
                            }
                        }
                    }
                });
                debugger;
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 28
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 28,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 28
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 9,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 6,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 11,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 2,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "Ex. Renta 4ta",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "D. Laborados",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Vacaciones",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "D. Licencias",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "D. Descansos y/o Subsidios",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Compensación Economica (2.1. 1 1. 1 7)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Compensación Economica Vacaional",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. CopagoSubsidio",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Aguinaldos (2.1. 1 9. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. CTS (2.1. 1 9. 2 1)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },                      
                      {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      //{
                      //    value: "M. Tardanzas",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 5ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. EsSaludMasVida",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.1. 3 1. 1 5)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              //{ value: data[i].bExoneracionRenta4ta },
                              { value: data[i].iDiasLaborados },
                              { value: data[i].iDiasVacaciones },
                              { value: data[i].iDiasLicencias },
                              //{ value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCTS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta5ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla Adicional FUNC",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla Adicional Completa FUNCIONARIOS.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }

    this.PlanillasJS.prototype.GenerarPlanillaFUNCAdicional = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaFUNCAdicional';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        var gridTra = $("#divGridTrabajadoresFUNCAdicional").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        if (gridTra.length > 0) {
            if (frmGenerarPlanillaFUNCAdicional.validate()) {
                var esValido = true;
                var mensajeValidacion = '';
                var data_param = new FormData();
                var iMes = $('#ddlMesFUNC_GenPlanAdicional').data("kendoDropDownList").value();
                var iAnio = $('#ddlAnioFUNC_GenPlanAdicional').data("kendoDropDownList").value();
                var iCodPlanilla = '5';
                var iCodTipoPlanilla = '2';
                if (gridTra.length > 0) {
                    var idsTrabajadoresTemp = "";
                    for (var i = 0; i < gridTra.length; i++) {
                        debugger;
                        if (i < gridTra.length - 1) {
                            idsTrabajadoresTemp += gridTra[i].IdEmpleado + ",";
                        }
                        else {
                            idsTrabajadoresTemp += gridTra[i].IdEmpleado;
                        }
                    }
                    //for (var i = 0; i < gridTra.length; i++) {
                    //    debugger;
                    //    if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
                    //        controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
                    //        return;
                    //    }

                    //}
                }
                debugger;
                data_param.append('iMes', iMes);
                data_param.append('iAnio', iAnio);
                //data_param.append('iAnio', '2020');
                data_param.append('iCodPlanilla', iCodPlanilla);
                data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
                data_param.append('strCodTrabajadores', idsTrabajadoresTemp);


                //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
                //var existe = controlador.ConsultarEjecucionPlanilla(items);
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data_param,
                    success: function (res) {
                        debugger;
                        //return res;
                        if (res > 0) {
                            if (res == 2) {
                                controladorApp.abrirMensajeDeConfirmacion(
                                '¿Está seguro de generar Planilla Adicional Funcionarios?', 'SI', 'NO'
                                , function (arg) {
                                    $.ajax({
                                        url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                                $("#divGridTrabajadoresFUNCAdicional").empty();
                                                controladorApp.notificarMensajeSatisfactorio("La planilla Adicional Funcionarios se ha generado correctamente");
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
                                controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                            }
                        }
                        else {
                            controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                        }

                    },
                    error: function (res) {
                        //alert(res);
                        debugger;
                    }
                });
                debugger;
            }
            else {
                controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Debe agregrar trabajadores para procesar la planilla adicional');
        }



    }

    ////////////////////////////BANDEJA PLANILLA VACACIONES TRUNCAS CAS//////////////////////////////////
    this.PlanillasJS.prototype.inicializarVerBandejaPlanillaVacTruncasCAS = function () {
        debugger;
        frmGenerarPlanillaVacTruncasCAS = $("#frmGenerarPlanillaVacTruncasCAS").kendoValidator().data("kendoValidator");
        frmBandejaVacTruncasCAS = $("#frmBandejaVacTruncasCAS").kendoValidator().data("kendoValidator");
        //$("#txtCantDiasDescFisNoGozado").attr('min', '0');
        //$("#txtCantDiasDescFisNoGozado").attr('max', '30');
        //$("#txtCantMesesDescFisNoGozado").attr('min', '0');
        //$("#txtCantMesesDescFisNoGozado").attr('max', '12');
        //$("#txtCantDiasVacTruncas").attr('min', '0');
        //$("#txtCantDiasVacTruncas").attr('max', '30');
        //$("#txtCantMesesVacTruncas").attr('min', '0');
        //$("#txtCantMesesVacTruncas").attr('max', '12');
        $("#ddlMesCAS_GenPlanVacTruncas").kendoDropDownList({
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
        $("#ddlAnioCAS_GenPlanVacTruncas").kendoDropDownList({
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
        $("#ddlMesCAS_BusqPlanVacTruncas").kendoDropDownList({
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
        $("#ddlAnioCAS_BusqPlanVacTruncas").kendoDropDownList({
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

        $("#divGridBandejaPlanillaCASVacTruncas").empty();
        $("#divGridBandejaPlanillaCASVacioVacTruncas").empty();
        $("#divGridBandejaPlanillaCASVacioVacTruncas").kendoGrid({
            columns: [
                    {
                        field: "iCodTrabajador",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "sNombreTipoDocumento",
                        title: "TIPO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNroDocumento",
                        title: "NRO DOCUMENTO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "sNombreCompleto",
                        title: "APELLIDOS Y NOMBRES",
                        width: "200px"
                    },
                    {
                        field: "sCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "sRegimenPensionario",
                        title: "REG. PENSIONARIO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }
                    },
                    {
                        field: "bExoneracionRenta4ta",
                        title: "EXO. 4TA",
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var tipo = '';
                            if (item.bExoneracionRenta4ta == true) tipo = "S";
                            if (item.bExoneracionRenta4ta == false) tipo = "N";

                            return tipo;
                        }
                    },
                    //{
                    //    field: "iDiasLaborados",
                    //    title: "DIAS LAB.",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" }
                    //},
                    //{
                    //    field: "iDiasVacaciones",
                    //    title: "DÍAS VAC.",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" }
                    //},
                    //{
                    //    field: "iDiasLicencias",
                    //    title: "DÍAS LIC.",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" }
                    //},
                    //{
                    //    field: "iDiasDescansos_Subsidios",
                    //    title: "DIAS D/S",
                    //    width: "30px",
                    //    attributes: { style: "text-align:center;" }
                    //},
                    {
                        field: "dcMontoRemuneracionBasica",
                        title: "R. BÁSICA",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalIngresos",
                        title: "T. ING.",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalDescuentos",
                        title: "T. DSCTO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoTotalNeto",
                        title: "M. NETO",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
                    },
                    {
                        field: "dcMontoAporteEsSalud",
                        title: "ESSALUD (2.3. 2 8. 1 2)",
                        width: "100px",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}"
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
                        width: "300px",
                        hidden: true
                    }
            ]
        });
        var divGridBandejaPlanillaCASVacioVacTruncas = $("#divGridBandejaPlanillaCASVacioVacTruncas").data("kendoGrid");
        divGridBandejaPlanillaCASVacioVacTruncas.wrapper.css("display", "block");

        //controlador.ListarEmpleadosPlanilla(event);
        controlador.CargarEmpleadosPlanillaVacTruncasTemporal(event);
    }

    this.PlanillasJS.prototype.ListarEmpleadosPlanillaVacTruncas = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarEmpleadosPlanillaVacTruncas',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation === "read") {
                        data_param.strCodTipoCondicionTrabajador = '1,2,3,6';

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
            pageSize: 10,
            //change: function (e) {
            //    //$("#lblTotal").html(this.total());
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
                    id: "IdEmpleado",
                    fields: {
                        IdEmpleado: { type: "number" },
                        TipoDocumento: { type: "string" },
                        NroDocumento: { type: "string" },
                        NombreCompleto: { type: "string" },
                        NombreOficina: { type: "string" }
                    }
                }
            }
        });
        debugger;
        this.$grid = $("#divAgregarTrabajadorVacTruncas").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: true,
            //groupable: true,
            //filterable: true,
            dataType: 'json',
            columns: [
                {
                    field: "IdEmpleado",
                    title: "IdEmpleado",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "TipoDocumento",
                    title: "Tipo Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NroDocumento",
                    title: "Nro Documento",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreCompleto",
                    title: "Apellidos y Nombres",
                    width: "150px"
                },
                {
                    field: "NombreOficina",
                    title: "Dependencia",
                    width: "300px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.IdEmpleado, item.NombreCompleto];

                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.CargarTrabajadorVacTruncas(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-plus" aria-hidden="true" data-uib-tooltip="Ver" title="Agregar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
        $('#filterVacTruncas').on('input', function (e) {
            var grid = $('#divAgregarTrabajadorVacTruncas').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
    };

    this.PlanillasJS.prototype.CargarTrabajadorPlanillaCASVacTruncas = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        var dni = $("#txtDniTrabajador").val();
        debugger;
        if (dni!='') {
            
            data_param.append('strCodTipoCondicionTrabajador', '1,2,3,6');
            data_param.append('strDNI', dni);
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/CargarTrabajadorPlanillaCASVacTruncas',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    if (res.IdEmpleado>0) {
                        $("#hdIdEmpleado").val(res.IdEmpleado);
                        $("#txtTrabajador").val(res.NombreCompleto);
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('El trabajador no se está registrado en la nómina del personal');
                        $("#hdIdEmpleado").val('');
                        $("#txtDniTrabajador").val('');
                        $("#txtTrabajador").val('');
                    }
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }
    this.PlanillasJS.prototype.CargarTrabajadorVacTruncas = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.IdEmpleado = _item[0];
            items.NombreCompleto = _item[1] + " " + _item[2];
        }
        if (items.IdEmpleado != "") {
            $("#hdIdEmpleado").val(items.IdEmpleado);
            $("#txtTrabajador").val(items.NombreCompleto);
            controlador.cerrarModalAgregarTrabajadorCASVacTruncas();
        }

        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {


    };
    

    this.PlanillasJS.prototype.abrirModalAgregarTrabajadorCASVacTruncas = function (e) {
        e.preventDefault();
        debugger;
        var modal = $('#ModalAgregarTrabajadorVacTruncas').data('kendoWindow');

        modal.title("Buscar Trabajadores");
        controlador.ListarEmpleadosPlanillaVacTruncas(event);
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarTrabajadorCASVacTruncas = function () {
        debugger;
        var modal = $('#ModalAgregarTrabajadorVacTruncas').data('kendoWindow');
        modal.close();
    }
    $('#ModalAgregarTrabajadorVacTruncas').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Buscar Trabajadores',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.abrirModalAgregarTrabajadorCASVacTruncas2 = function (e) {
        e.preventDefault();
        debugger;
        $("#txtCantDiasDescFisNoGozado").attr('min', '0');
        $("#txtCantDiasDescFisNoGozado").attr('max', '30');
        $("#txtCantMesesDescFisNoGozado").attr('min', '0');
        $("#txtCantMesesDescFisNoGozado").attr('max', '12');
        $("#txtCantDiasVacTruncas").attr('min', '0');
        $("#txtCantDiasVacTruncas").attr('max', '30');
        $("#txtCantMesesVacTruncas").attr('min', '0');
        $("#txtCantMesesVacTruncas").attr('max', '12');
        var modal = $('#ModalAgregarTrabajadorVacTruncas2').data('kendoWindow');

        modal.title("Agregar Trabajador");
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarTrabajadorCASVacTruncas2 = function () {
        debugger;
        var modal = $('#ModalAgregarTrabajadorVacTruncas2').data('kendoWindow');
        $("#hdIdEmpleado").val('');
        $("#txtDniTrabajador").val('');
        $("#txtTrabajador").val('');
        $("#txtCantDiasDescFisNoGozado").val(0);
        $("#txtCantMesesDescFisNoGozado").val(0);        
        $("#txtCantDiasVacTruncas").val(0);        
        $("#txtCantMesesVacTruncas").val(0);        
        modal.close();
    }
    $('#ModalAgregarTrabajadorVacTruncas2').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Agregar Trabajador',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.CargarEmpleadosPlanillaVacTruncasTemporal = function (e) {
        //e.preventDefault();

        debugger;
        //if (id != "") {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/CargarEmpleadosPlanillaVacTruncasTemporal',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
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
                model: {
                    id: "IdEmpleado"
                }
            }
        });
        debugger;
        this.$grid = $("#divGridTrabajadoresVacTruncas").kendoGrid({
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
                    field: "IdEmpleado",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "Nombre",
                    title: "TRABAJADOR",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "DiasDescansoFisico",
                    title: "DIAS DESC. FISICO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "MesesDescansoFisico",
                    title: "MESES DESC. FISICO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "DiasVacacionesTruncas",
                    title: "DIAS VAC. TRUNCAS",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "MesesVacacionesTruncas",
                    title: "MESES VAC. TRUNCAS",
                    width: "30px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "QUITAR",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";


                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaVacTruncasTemporal(\'' + item.IdEmpleado + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        //}

        debugger;
    };

    this.PlanillasJS.prototype.AgregarTrabajadorPlanillaVacTruncasTemporal = function (e) {
        debugger;
        e.preventDefault();
        var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');
        
        items.iCodTrabajador = $("#hdIdEmpleado").val();
        items.strNombreCompleto = $("#txtTrabajador").val();
        items.iDiasDescansoFisico = $("#txtCantDiasDescFisNoGozado").val();
        items.iMesesDescansoFisico = $("#txtCantMesesDescFisNoGozado").val();
        items.iDiasVacTruncas = $("#txtCantDiasVacTruncas").val();
        items.iMesesVacTruncas = $("#txtCantMesesVacTruncas").val();
            

        //}
        debugger;
        var gridTra = $("#divGridTrabajadoresVacTruncas").data().kendoGrid.dataSource.view();
        if (gridTra.length > 0) {
            for (var i = 0; i < gridTra.length; i++) {
                debugger;
                if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
                    controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
                    return;
                }

            }
        }
        if (items.iCodTrabajador != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/AgregarTrabajadorPlanillaVacTruncasTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = items.iCodTrabajador;
                            data_param.strNombreCompleto = items.strNombreCompleto; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                            data_param.iDiasDescansoFisico = items.iDiasDescansoFisico;
                            data_param.iMesesDescansoFisico = items.iMesesDescansoFisico;
                            data_param.iDiasVacTruncas = items.iDiasVacTruncas;
                            data_param.iMesesVacTruncas = items.iMesesVacTruncas;
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresVacTruncas").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "DiasDescansoFisico",
                        title: "DIAS DESC. FISICO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "MesesDescansoFisico",
                        title: "MESES DESC. FISICO",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "DiasVacacionesTruncas",
                        title: "DIAS VAC. TRUNCAS",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        field: "MesesVacacionesTruncas",
                        title: "MESES VAC. TRUNCAS",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";


                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaVacTruncasTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
            controlador.cerrarModalAgregarTrabajadorCASVacTruncas2();
        }

        debugger;
    };

    this.PlanillasJS.prototype.QuitarEmpleadosPlanillaVacTruncasTemporal = function (id) {
        //e.preventDefault();

        debugger;
        if (id != "") {
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/QuitarEmpleadosPlanillaVacTruncasTemporal',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {

                            data_param.iCodTrabajador = id;
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
                    model: {
                        id: "IdEmpleado"
                    }
                }
            });
            debugger;
            this.$grid = $("#divGridTrabajadoresVacTruncas").kendoGrid({
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
                        field: "IdEmpleado",
                        title: "ID",
                        attributes: { style: "text-align:right;" },
                        width: "30px",
                        hidden: true
                    },
                    {
                        field: "Nombre",
                        title: "TRABAJADOR",
                        width: "30px",
                        attributes: { style: "text-align:center;" }

                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: "Quitar",
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";


                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosPlanillaAdicioalTemporal(\'' + item.IdEmpleado + '\')">';
                            controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                            controles += '</button>';
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }

        debugger;
    };

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASVacTruncasValidar = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        if (frmBandejaCASAdicional.validate()) {
            var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
            var iAnioBusqPlan4ta = $('#ddlAnioCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
            //var metasTemp = "";
            //var list = $("#selected").data("kendoListBox");
            //if (list.dataSource.data().length > 0) {
            //    for (var i = 0; i < list.dataSource.data().length; i++) {
            //        debugger;
            //        if (i < list.dataSource.data().length - 1) {
            //            metasTemp += list.dataSource._data[i].value + ",";
            //        }
            //        else {
            //            metasTemp += list.dataSource._data[i].value;
            //        }
            //    }
            //}
            //$("#hdidCodFuenteFinanciamiento").val(0);
            //$("#hdvMetasTemp").val(metasTemp);
            $("#hdiMesBusqPlan4taVacTruncas").val(iMesBusqPlan4ta);
            $("#hdiAnioBusqPlan4taVacTruncas").val(iAnioBusqPlan4ta);

            var iCodPlanilla = '3';
            var iCodTipoPlanilla = '1';
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);


            //data_param.append('idCodFuenteFinanciamiento', 0);
            //data_param.append('sMetas', metasTemp);
            data_param.append('iMes', iMesBusqPlan4ta);
            data_param.append('iAnio', iAnioBusqPlan4ta);




            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarGeneracionPlanilla',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    //return res;
                    if (res == true) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCASVacTruncas',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res.length > 0) {
                                    $("#divGridBandejaPlanillaCASVacTruncas").empty();
                                    $("#divGridBandejaPlanillaCASVacioVacTruncas").empty();
                                    controlador.CargarBandejaPrincipalPlanillaCASAdicional();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'block');                                                
                                    var divGridBandejaPlanillaCASVacTruncas = $("#divGridBandejaPlanillaCASVacTruncas").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacTruncas.wrapper.css("display", "block");
                                    $("#divGridBandejaPlanillaCASVacioVacTruncas").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                //{
                                                //    field: "iDiasLaborados",
                                                //    title: "DIAS LAB.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasVacaciones",
                                                //    title: "DÍAS VAC.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasLicencias",
                                                //    title: "DÍAS LIC.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasDescansos_Subsidios",
                                                //    title: "DIAS D/S",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    var divGridBandejaPlanillaCASVacioVacTruncas = $("#divGridBandejaPlanillaCASVacioVacTruncas").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacioVacTruncas.wrapper.css("display", "none");

                                }
                                else {
                                    $("#divGridBandejaPlanillaCASVacTruncas").empty();
                                    $("#divGridBandejaPlanillaCASVacioVacTruncas").empty();
                                    //controlador.CargarBandejaPrincipalPlanillaCASVacio();
                                    debugger;
                                    //$('.divGridBandejaPlanillaCAS').css('display', 'none');
                                    //$('.divGridBandejaPlanillaCASVacio').css('display', 'block');
                                    $("#lblTotalBusqCASVacTruncas").html(res.length);
                                    $("#divGridBandejaPlanillaCASVacioVacTruncas").kendoGrid({
                                        columns: [
                                                {
                                                    field: "iCodTrabajador",
                                                    title: "ID",
                                                    attributes: { style: "text-align:right;" },
                                                    width: "30px",
                                                    hidden: true
                                                },
                                                {
                                                    field: "sNombreTipoDocumento",
                                                    title: "TIPO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNroDocumento",
                                                    title: "NRO DOCUMENTO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "sNombreCompleto",
                                                    title: "APELLIDOS Y NOMBRES",
                                                    width: "200px"
                                                },
                                                {
                                                    field: "sCargo",
                                                    title: "CARGO",
                                                    width: "300px"
                                                },
                                                {
                                                    field: "sRegimenPensionario",
                                                    title: "REG. PENSIONARIO",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" }
                                                },
                                                {
                                                    field: "bExoneracionRenta4ta",
                                                    title: "EXO. 4TA",
                                                    width: "30px",
                                                    attributes: { style: "text-align:center;" },
                                                    template: function (item) {
                                                        var tipo = '';
                                                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                        return tipo;
                                                    }
                                                },
                                                //{
                                                //    field: "iDiasLaborados",
                                                //    title: "DIAS LAB.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasVacaciones",
                                                //    title: "DÍAS VAC.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasLicencias",
                                                //    title: "DÍAS LIC.",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                //{
                                                //    field: "iDiasDescansos_Subsidios",
                                                //    title: "DIAS D/S",
                                                //    width: "30px",
                                                //    attributes: { style: "text-align:center;" }
                                                //},
                                                {
                                                    field: "dcMontoRemuneracionBasica",
                                                    title: "R. BÁSICA",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalIngresos",
                                                    title: "T. ING.",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalDescuentos",
                                                    title: "T. DSCTO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoTotalNeto",
                                                    title: "M. NETO",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
                                                },
                                                {
                                                    field: "dcMontoAporteEsSalud",
                                                    title: "ESSALUD (2.3. 2 8. 1 2)",
                                                    width: "100px",
                                                    attributes: { style: "text-align:right;" },
                                                    format: "{0:c}"
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
                                                    width: "300px",
                                                    hidden: true
                                                }
                                        ]
                                    });
                                    $("#divGridBandejaPlanillaCASVacTruncas").kendoGrid({
                                        columns: [
                                            {
                                                field: "iCodTrabajador",
                                                title: "ID",
                                                attributes: { style: "text-align:right;" },
                                                width: "30px",
                                                hidden: true
                                            },
                                            {
                                                field: "sNombreTipoDocumento",
                                                title: "TIPO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNroDocumento",
                                                title: "NRO DOCUMENTO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "sNombreCompleto",
                                                title: "APELLIDOS Y NOMBRES",
                                                width: "200px"
                                            },
                                            {
                                                field: "sCargo",
                                                title: "CARGO",
                                                width: "300px"
                                            },
                                            {
                                                field: "sRegimenPensionario",
                                                title: "REG. PENSIONARIO",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" }
                                            },
                                            {
                                                field: "bExoneracionRenta4ta",
                                                title: "EXO. 4TA",
                                                width: "30px",
                                                attributes: { style: "text-align:center;" },
                                                template: function (item) {
                                                    var tipo = '';
                                                    if (item.bExoneracionRenta4ta == true) tipo = "S";
                                                    if (item.bExoneracionRenta4ta == false) tipo = "N";

                                                    return tipo;
                                                }
                                            },
                                            //{
                                            //    field: "iDiasLaborados",
                                            //    title: "DIAS LAB.",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            //{
                                            //    field: "iDiasVacaciones",
                                            //    title: "DÍAS VAC.",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            //{
                                            //    field: "iDiasLicencias",
                                            //    title: "DÍAS LIC.",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            //{
                                            //    field: "iDiasDescansos_Subsidios",
                                            //    title: "DIAS D/S",
                                            //    width: "30px",
                                            //    attributes: { style: "text-align:center;" }
                                            //},
                                            {
                                                field: "dcMontoRemuneracionBasica",
                                                title: "R. BÁSICA",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalIngresos",
                                                title: "T. ING.",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalDescuentos",
                                                title: "T. DSCTO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoTotalNeto",
                                                title: "M. NETO",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
                                            },
                                            {
                                                field: "dcMontoAporteEsSalud",
                                                title: "ESSALUD (2.3. 2 8. 1 2)",
                                                width: "100px",
                                                attributes: { style: "text-align:right;" },
                                                format: "{0:c}"
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
                                                width: "300px",
                                                hidden: true
                                            }
                                        ]
                                    });
                                    var divGridBandejaPlanillaCASVacTruncas = $("#divGridBandejaPlanillaCASVacTruncas").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacTruncas.wrapper.css("display", "none");
                                    var divGridBandejaPlanillaCASVacioVacTruncas = $("#divGridBandejaPlanillaCASVacioVacTruncas").data("kendoGrid");
                                    divGridBandejaPlanillaCASVacioVacTruncas.wrapper.css("display", "block");
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });

                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('La planilla no ha sido generada');
                    }
                },
                error: function (res) {
                    //alert(res);
                    debugger;
                }
            });
        }
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaCASVacTruncas = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan4ta = $('#ddlMesCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
        var iAnioBusqPlan4ta = $('#ddlMesCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCASVacTruncas',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        //data_param.idCodFuenteFinanciamiento = 0;
                        //var list = $("#selected").data("kendoListBox");
                        //if (list.dataSource.data().length > 0) {
                        //    var metasTemp = "";
                        //    for (var i = 0; i < list.dataSource.data().length; i++) {
                        //        debugger;
                        //        if (i < list.dataSource.data().length - 1) {
                        //            metasTemp += list.dataSource._data[i].value + ",";
                        //        }
                        //        else {
                        //            metasTemp += list.dataSource._data[i].value;
                        //        }
                        //    }
                        //}
                        //alert(metasTemp);

                        //data_param.sMetas = metasTemp;
                        data_param.iMes = $('#ddlMesCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
                        //data_param.iMes = '9';
                        //data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
                        data_param.iAnio = $('#ddlAnioCAS_BusqPlanVacTruncas').data("kendoDropDownList").value();
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
                $("#lblTotalBusqCASVacTruncas").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {
                        iCodTrabajador: { type: "number" },
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" },
                        sRegimenPensionario: { type: "string" },
                        bExoneracionRenta4ta: { type: "bool" },
                        //iDiasLaborados: { type: "number" },
                        //iDiasVacaciones: { type: "number" },
                        //iDiasLicencias: { type: "number" },
                        //iDiasDescansos_Subsidios: { type: "number" },
                        dcMontoRemuneracionBasica: { type: "number" },
                        dcMontoTotalIngresos: { type: "number" },
                        dcMontoTotalDescuentos: { type: "number" },
                        dcMontoTotalNeto: { type: "number" },
                        dcMontoAporteEsSalud: { type: "number" },
                        iMes: { type: "number" },
                        iAnio: { type: "number" }
                    }
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
        this.$grid = $("#divGridBandejaPlanillaCASVacTruncas").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Planilla CAS.xlsx",
                filterable: false,
                proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaCASAdicional"
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitVacTruncas,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "sNombreTipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "sNombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px"
                },
                {
                    field: "sCargo",
                    title: "CARGO",
                    width: "300px"
                },
                {
                    field: "sRegimenPensionario",
                    title: "REG. PENSIONARIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bExoneracionRenta4ta",
                    title: "EXO. 4TA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = '';
                        if (item.bExoneracionRenta4ta == true) tipo = "S";
                        if (item.bExoneracionRenta4ta == false) tipo = "N";

                        return tipo;
                    }
                },
                //{
                //    field: "iDiasLaborados",
                //    title: "DIAS LAB.",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "iDiasVacaciones",
                //    title: "DÍAS VAC.",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "iDiasLicencias",
                //    title: "DÍAS LIC.",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "iDiasDescansos_Subsidios",
                //    title: "DIAS D/S",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "dcMontoRemuneracionBasica",
                    title: "R. BÁSICA",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "T. ING.",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuentos",
                    title: "T. DSCTO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalNeto",
                    title: "M. NETO",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteEsSalud",
                    title: "ESSALUD <br />(2.3. 2 8. 1 2)",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
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
                    width: "300px",
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Modificar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio, "3", "1", item.sNombreCompleto];
                        var controles = "";
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalConceptosPagosTrabajador(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Ver" title="Modificar Concepto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();


        //debugger;
        //var divGridBandejaPlanillaCAS = $("#divGridBandejaPlanillaCAS").data("kendoGrid");
        //divGridBandejaPlanillaCAS.wrapper.css("display", "block");
        //var GridBandejaPlanillaCASVacio = $("#divGridBandejaPlanillaCASVacio").data("kendoGrid");
        //GridBandejaPlanillaCASVacio.wrapper.css("display", "none");
        // }
        //}
        $('#filterBusqCASVacTruncas').on('input', function (e) {
            var grid = $('#divGridBandejaPlanillaCASAdicional').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
        debugger;
    };
    function detailInitVacTruncas(e) {
        //e.preventDefault();
        debugger;
        var detailRow = e.detailRow;


        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridIngresosCASVacTruncas").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaIngresosCASVacTruncas',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            debugger;
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoContraprestacion",
                    title: "CONTRAPRESTACION (2.3. 2 8. 1 5)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoContraprescionVacaional",
                //    title: "CONTRAPRESTACION VACACIONAL",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoReintegros",
                    title: "REINTEGROS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                //{
                //    field: "dcMontoCopagoSubsidio",
                //    title: "COPAGO SUBSIDIO",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    format: "{0:c}"
                //},
                {
                    field: "dcMontoAguinaldos",
                    title: "AGUINALDOS (2.3. 2 8. 1 4)",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalIngresos",
                    title: "TOTAL INGRESOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }

            ]
        }); //.data()
        detailRow.find(".divGridDescuentosCASVacTruncas").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaDescuentosCASVacTruncas',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.iCodTrabajador = e.data.iCodTrabajador;
                            data_param.iMes = e.data.iMes;
                            data_param.iAnio = e.data.iAnio;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                //schema: {
                //    //total: function (response) {
                //    //    return response.length; // TotalDeRegistros;
                //    //},
                //    model: {
                //        id: "IdPostulacion"
                //    }
                //}
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [

                {
                    field: "dcMontoTardanzas",
                    title: "TARDANZAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoInasistencias",
                    title: "INASISTENCIAS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPermisos",
                    title: "PERMISOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoImpuestoRenta4ta",
                    title: "IMP. RTA 4TA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoONP",
                    title: "ONP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoAporteOblAFP",
                    title: "APORTE AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoComisionAFP",
                    title: "COMISION AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoPrimaSegAFP",
                    title: "PRIMA SEG. AFP",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDescuentoJudicial",
                    title: "DSCTO. JUDICIAL",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEsSaludMasVida",
                    title: "EsSALUD MAS VIDA",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoRimacSeguro",
                    title: "RIMAC SEGURO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoDsctoPagoExceso",
                    title: "DSCTO P. EXCESO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoEPS",
                    title: "EPS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                },
                {
                    field: "dcMontoTotalDescuento",
                    title: "TOTAL DESCUENTOS",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    format: "{0:c}"
                }
            ]
        }); //.data()
    }

    this.PlanillasJS.prototype.GenerarExcelPlanillaCASVacTruncas = function (e) {
        e.preventDefault();
        var data_param = new FormData();
        debugger;
        //var idCodFuenteFinanciamiento = $("#hdidCodFuenteFinanciamientoAdicional").val();
        var iMesBusqPlan4ta = $("#ddlMesCAS_BusqPlanVacTruncas").val();
        var iAnioBusqPlan4ta = $("#ddlAnioCAS_BusqPlanVacTruncas").val();
        //var metasTemp = $("#hdvMetasTemp").val();
        var cantReg = $("#lblTotalBusqCASVacTruncas").html();
        if (cantReg != "" && cantReg != "0") {
            if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
                //$("#hdidCodFuenteFinanciamiento").val();
                //$("#hdvMetasTemp").val();
                //$("#hdiMesBusqPlan4ta").val();
                //$("#hdiAnioBusqPlan4ta").val();

                var vTituloReporte = $('#txtNombreReportePlanillaCASVacTruncas').val().toUpperCase();

                //var list = $("#selected").data("kendoListBox");
                //if (list.dataSource.data().length > 0) {
                //    for (var i = 0; i < list.dataSource.data().length; i++) {
                //        debugger;
                //        if (i < list.dataSource.data().length - 1) {
                //            metasTemp += list.dataSource._data[i].value + ",";
                //        }
                //        else {
                //            metasTemp += list.dataSource._data[i].value;
                //        }
                //    }
                //}
                //data_param.append('idCodFuenteFinanciamiento', 0);
                //data_param.append('sMetas', metasTemp);
                //data_param.append('iMes', iMesBusqPlan4ta);
                //data_param.append('iAnio', iAnioBusqPlan4ta);

                //$.ajax({
                //    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCAS',
                //    type: 'POST',
                //    dataType: 'json',
                //    contentType: false,
                //    processData: false,
                //    data: data_param,
                //    success: function (res) {
                //        debugger;
                //        if (res.length > 0) {


                //        }

                //    },
                //    error: function (res) {
                //        //alert(res);
                //    }
                //});

                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCalculadaGeneralCompletaCASVacTruncas',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                //data_param.idCodFuenteFinanciamiento = idCodFuenteFinanciamiento;
                                //data_param.sMetas = metasTemp;
                                data_param.iMes = iMesBusqPlan4ta;
                                data_param.iAnio = iAnioBusqPlan4ta;
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
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {
                                //OrderID: { type: "number" },
                                //Freight: { type: "number" },
                                //ShipName: { type: "string" },
                                //OrderDate: { type: "date" },
                                //ShipCity: { type: "string" }
                                //iCodTrabajador: { type: "number" },
                                sNombreTipoDocumento: { type: "string" },
                                sNroDocumento: { type: "string" },
                                sNombreCompleto: { type: "string" },
                                sCargo: { type: "string" },
                                sRegimenPensionario: { type: "string" },
                                bExoneracionRenta4ta: { type: "bool" },
                                //iDiasLaborados: { type: "number" },
                                //iDiasVacaciones: { type: "number" },
                                //iDiasLicencias: { type: "number" },
                                //iDiasDescansos_Subsidios: { type: "number" },
                                dcMontoRemuneracionBasica: { type: "number" },
                                dcMontoContraprestacion: { type: "number" },
                                //dcMontoContraprescionVacaional: { type: "number" },
                                dcMontoReintegros: { type: "number" },
                                //dcMontoCopagoSubsidio: { type: "number" },
                                dcMontoAguinaldos: { type: "number" },
                                dcMontoTotalIngresos: { type: "number" },

                                dcMontoTardanzas: { type: "number" },
                                dcMontoPermisos: { type: "number" },
                                dcMontoImpuestoRenta4ta: { type: "number" },
                                dcMontoONP: { type: "number" },
                                dcMontoAporteOblAFP: { type: "number" },
                                dcMontoComisionAFP: { type: "number" },
                                dcMontoPrimaSegAFP: { type: "number" },
                                dcMontoDescuentoJudicial: { type: "number" },
                                dcMontoEsSaludMasVida: { type: "number" },
                                dcMontoRimacSeguro: { type: "number" },
                                dcMontoDsctoPagoExceso: { type: "number" },
                                dcMontoEPS: { type: "number" },
                                dcMontoTotalDescuentos: { type: "number" },
                                dcMontoTotalNeto: { type: "number" },
                                dcMontoAporteEsSalud: { type: "number" },
                                iMes: { type: "number" },
                                iAnio: { type: "number" }
                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 26
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: vTituloReporte,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 26,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 26
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Datos Generales",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 7,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 4,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 13,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 2,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Tipo Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Nro Documento",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Apellidos y Nombres",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Cargo",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "R. Pensionario",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "Ex. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "D. Laborados",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      //{
                      //    value: "D. Vacaciones",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      //{
                      //    value: "D. Licencias",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      //{
                      //    value: "D. Descansos y/o Subsidios",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Remuneracion",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Contraprestacion (2.3. 2 8. 1 5)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. Contraprescion Vacaional",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Reintegros",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      //{
                      //    value: "M. CopagoSubsidio",
                      //    bold: true,
                      //    background: "#217DAD",
                      //    vAlign: "center",
                      //    hAlign: "center",
                      //    color: "#FFFFFF"
                      //},
                      {
                          value: "M. Aguinaldos (2.3. 2 8. 1 4)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Tardanzas",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Permisos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Imp. Renta 4ta",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. ONP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte Obl AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Comision AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Prima Seg AFP",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Judicial",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EsSaludMasVida",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Rimac Seguro",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Dscto Pago Exceso",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. EPS",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Total Dsctos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      },
                      {
                          value: "M. Total Neto",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF"
                      },
                      {
                          value: "M. Aporte EsSalud (2.3. 2 8. 1 2)",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          },
                      }
                    ]
                });
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: data[i].sNombreTipoDocumento },
                              { value: data[i].sNroDocumento },
                              { value: data[i].sNombreCompleto },
                              { value: data[i].sCargo },
                              { value: data[i].sRegimenPensionario },
                              { value: data[i].bExoneracionRenta4ta },
                              //{ value: data[i].iDiasLaborados },
                              //{ value: data[i].iDiasVacaciones },
                              //{ value: data[i].iDiasLicencias },
                              //{ value: data[i].iDiasDescansos_Subsidios },
                              { value: data[i].dcMontoRemuneracionBasica, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprestacion, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoContraprescionVacaional, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoReintegros, format: "#,##0.00" },
                              //{ value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoCopagoSubsidio, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaIngresos_Request.dcMontoAguinaldos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalIngresos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoTardanzas, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPermisos, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoImpuestoRenta4ta, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoONP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoAporteOblAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoComisionAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoPrimaSegAFP, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDescuentoJudicial, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEsSaludMasVida, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoRimacSeguro, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoDsctoPagoExceso, format: "#,##0.00" },
                              { value: data[i].objPlanillaCalculadaDescuentos_Request.dcMontoEPS, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalDescuentos, format: "#,##0.00" },
                              { value: data[i].dcMontoTotalNeto, format: "#,##0.00" },
                              { value: data[i].dcMontoAporteEsSalud, format: "#,##0.00" }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                //{ autoWidth: true },
                                //{ autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                              ],
                              // The title of the sheet.
                              title: "Planilla CAS Vacaciones Truncas",
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Planilla CAS Vacaciones Truncas.xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Planilla exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }

    this.PlanillasJS.prototype.GenerarPlanillaCASVacTruncas = function (e) {
        debugger;

        var metodo = 'GenerarPlanillaCASVacTruncas';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        var gridTra = $("#divGridTrabajadoresVacTruncas").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        if (gridTra.length > 0) {
            if (frmGenerarPlanillaVacTruncasCAS.validate()) {
                var esValido = true;
                var mensajeValidacion = '';
                var data_param = new FormData();
                var iMes = $('#ddlMesCAS_GenPlanVacTruncas').data("kendoDropDownList").value();
                var iAnio = $('#ddlAnioCAS_GenPlanVacTruncas').data("kendoDropDownList").value();
                var iCodPlanilla = '3';
                var iCodTipoPlanilla = '1';
                if (gridTra.length > 0) {
                    var idsTrabajadoresTemp = "";
                    for (var i = 0; i < gridTra.length; i++) {
                        debugger;
                        if (i < gridTra.length - 1) {
                            idsTrabajadoresTemp += gridTra[i].IdEmpleado + ",";
                        }
                        else {
                            idsTrabajadoresTemp += gridTra[i].IdEmpleado;
                        }
                    }
                    //for (var i = 0; i < gridTra.length; i++) {
                    //    debugger;
                    //    if (gridTra[i].IdEmpleado == items.iCodTrabajador) {
                    //        controladorApp.notificarMensajeDeAlerta("El trabajador ya ha sido agregado");
                    //        return;
                    //    }

                    //}
                }
                debugger;
                data_param.append('iMes', iMes);
                data_param.append('iAnio', iAnio);
                //data_param.append('iAnio', '2020');
                data_param.append('iCodPlanilla', iCodPlanilla);
                data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
                data_param.append('strCodTrabajadores', idsTrabajadoresTemp);


                //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
                //var existe = controlador.ConsultarEjecucionPlanilla(items);
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ConsultarEjecucionPlanilla',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data_param,
                    success: function (res) {
                        debugger;
                        //return res;
                        if (res > 0) {
                            if (res == 2) {
                                controladorApp.abrirMensajeDeConfirmacion(
                                '¿Está seguro de generar Planilla CAS Vacaciones Truncas?', 'SI', 'NO'
                                , function (arg) {
                                    $.ajax({
                                        url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                                $("#divGridTrabajadoresVacTruncas").empty();
                                                controladorApp.notificarMensajeSatisfactorio("La planilla CAS Vacaciones Truncas se ha generado correctamente");
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
                                controladorApp.notificarMensajeDeAlerta('La planilla está cerrada');
                            }
                        }
                        else {
                            controladorApp.notificarMensajeDeAlerta('La planilla no existe');
                        }

                    },
                    error: function (res) {
                        //alert(res);
                        debugger;
                    }
                });
                debugger;
            }
            else {
                controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Debe agregrar trabajadores para procesar la planilla CAS Vacaciones Truncas');
        }



    }

    ////////////////////////////BANDEJA ADMINISTRACION DE PLANILLAS//////////////////////////////////

    this.PlanillasJS.prototype.inicializarVerBandejaPlanilla = function () {
        debugger;
        var tt = new Date();
        var mm = tt.getMonth() + 1;
        var year = tt.getFullYear();
        frmGenerarPlanillaVacTruncasCAS = $("#frmGenerarPlanillaVacTruncasCAS").kendoValidator().data("kendoValidator");
        $("#ddlMesPlan").kendoDropDownList({
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
        $("#ddlAnioPlan").kendoDropDownList({
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
        $("#ddlMesPlan").data("kendoDropDownList").value(mm);
        $("#ddlAnioPlan").data("kendoDropDownList").value(year);
        //controlador.ListarEmpleadosPlanilla(event);
        controlador.CargarBandejaPrincipalPlanillaEjecucion(event);
    }
    this.PlanillasJS.prototype.CargarBandejaPrincipalPlanillaEjecucion = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        var iMesBusqPlan = $('#ddlMesPlan').data("kendoDropDownList").value();
        var iAnioBusqPlan = $('#ddlAnioPlan').data("kendoDropDownList").value();
        debugger;
        //if (iMesBusqPlan4ta != "" && iAnioBusqPlan4ta != "") {
        //if (frmBandejaCAS.validate()) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillasCreadas',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        
                        data_param.iMes = iMesBusqPlan;                                            
                        data_param.iAnio = iAnioBusqPlan;
                        //data_param.iMes = '';
                        //data_param.iAnio = '';
                        data_param.iCodPlanilla = '';
                        data_param.iCodTipoPlanilla = '';

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
                $("#lblTotalPlanillas").html(this.total());
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodPlanilla"
                    //fields: {
                    //    iCodTrabajador: { type: "number" },
                    //    sNombreTipoDocumento: { type: "string" },
                    //    sNroDocumento: { type: "string" },
                    //    sNombreCompleto: { type: "string" },
                    //    sCargo: { type: "string" },
                    //    sRegimenPensionario: { type: "string" },
                    //    //bExoneracionRenta4ta: { type: "bool" },
                    //    iDiasLaborados: { type: "number" },
                    //    iDiasVacaciones: { type: "number" },
                    //    iDiasLicencias: { type: "number" },
                    //    //iDiasDescansos_Subsidios: { type: "number" },
                    //    dcMontoRemuneracionBasica: { type: "number" },
                    //    dcMontoTotalIngresos: { type: "number" },
                    //    dcMontoTotalDescuentos: { type: "number" },
                    //    dcMontoTotalNeto: { type: "number" },
                    //    dcMontoAporteEsSalud: { type: "number" },
                    //    iMes: { type: "number" },
                    //    iAnio: { type: "number" }
                    //}
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
        this.$grid = $("#divGridBandejaPlanilla").kendoGrid({
            toolbar: [],
            //excel: {
            //    fileName: "Planilla FUNCIONARIOS.xlsx",
            //    filterable: false,
            //    proxyURL: "Planilla/ListarPlanillaCalculadaGeneralCompletaFUNC"
            //},
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            //detailTemplate: kendo.template($("#template").html()),
            //detailInit: detailInitFunc,
            dataType: 'json',
            //dataBound: function () {
            //    //debugger;
            //    //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            columns: [
                {
                    field: "iCodPlanilla",
                    title: "COD. PLANILLA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "sNombrePlanilla",
                    title: "PLANILLA",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodTipoPlanilla",
                    title: "COD. TIPO PLANILLA",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "sNombreTipoPlanilla",
                    title: "TIPO PLANILLA",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iMes",
                    title: "MES",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iAnio",
                    title: "AÑO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "sFechaReg",
                //    title: "FECHA CREACIÓN",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    title: "GENERACIÓN DE PLANILLA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "sEstadoEjecutado",
                            title: "¿GENERADA?",
                            width: "30px",
                            attributes: { style: "text-align:center;" }
                        },
                        {
                            field: "sFechaGeneracion",
                            title: "FECHA",
                            width: "50px",
                            attributes: { style: "text-align:center;" }
                        },
                    ]
                },
                {
                    title: "CIERRE DE PLANILLA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "sEstadoCierre",
                            title: "¿CERRADA?",
                            width: "30px",
                            attributes: { style: "text-align:center;" }
                        },
                        {
                            field: "sFechaCierre",
                            title: "FECHA",
                            width: "50px",
                            attributes: { style: "text-align:center;" }
                        },
                        {
                            //INGRESAR DETALLE DE LA EVALUACION
                            title: "CERRAR",
                            attributes: { style: "text-align:center;" },
                            template: function (item) {
                                var items = [item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio];
                                var controles = "";
                                if (item.sEstadoEjecutado == "SI") {
                                    if (item.sEstadoCierre == "NO") {
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.CerrarPlanilla(\'' + items + '\')">';
                                        controles += '<span class="glyphicon glyphicon-lock" aria-hidden="true" data-uib-tooltip="Planilla" title="Cerrar Planilla"></span>';
                                        controles += '</button>';
                                    }

                                }
                                
                                return controles;
                            },
                            width: '30px'
                        }
                    ]
                },

                {
                    field: "dIngresos",
                    title: "INGRESOS",
                    width: "50px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dDescuentos",
                    title: "DESCUENTOS",
                    width: "50px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "dAportes",
                    title: "APORTES",
                    width: "50px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "REPORTE BCO",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio];
                        var controles = "";
                        if (item.sEstadoCierre == "SI") {
                            //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarReporteBCO(\'' + items + '\')">';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarReporteExcelBCO(\'' + items + '\')">';                            
                            controles += '<span class="glyphicon glyphicon-export" aria-hidden="true" data-uib-tooltip="Planilla" title="Reporte BCO"></span>';
                            controles += '</button>';
                        }
                        
                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "REPORTE RESUMEN",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var items = [item.iCodPlanilla, item.iCodTipoPlanilla, item.iMes, item.iAnio];
                        var controles = "";
                        if (item.sEstadoCierre == "SI") {
                            //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarReporteBCO(\'' + items + '\')">';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalPlanillaReporte(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-export" aria-hidden="true" data-uib-tooltip="Planilla" title="Reporte Resumen"></span>';
                            controles += '</button>';
                        }

                        return controles;
                    },
                    width: '30px'
                }

            ]
        }).data();
                
        debugger;
    };
    
    this.PlanillasJS.prototype.InsertarPlanilla = function (e) {
        e.preventDefault();
        debugger;
        var metodo = 'InsertarPlanilla';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        var gridTra = $("#divGridBandejaPlanilla").data().kendoGrid.dataSource.view();//("#selected").data("kendoListBox");
        
        if (frmModalAgregarPlanilla.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            var iMes = $('#ddlMesPlanilla').data("kendoDropDownList").value();
            var iAnio = $('#ddlAnioPlanilla').data("kendoDropDownList").value();
            var iCodPlanilla = $('#ddlPlanilla').data("kendoDropDownList").value();
            var iCodTipoPlanilla = $('#ddlTipoPlanilla').data("kendoDropDownList").value();
            if (gridTra.length > 0) {
                //var idsTrabajadoresTemp = "";
                //for (var i = 0; i < gridTra.length; i++) {
                //    debugger;
                //    if (i < gridTra.length - 1) {
                //        idsTrabajadoresTemp += gridTra[i].IdEmpleado + ",";
                //    }
                //    else {
                //        idsTrabajadoresTemp += gridTra[i].IdEmpleado;
                //    }
                //}
                for (var i = 0; i < gridTra.length; i++) {
                    debugger;
                    if (gridTra[i].iMes == iMes && gridTra[i].iAnio == iAnio && gridTra[i].iCodPlanilla == iCodPlanilla && gridTra[i].iCodTipoPlanilla == iCodTipoPlanilla) {
                        controladorApp.notificarMensajeDeAlerta("La planilla ya ha sido creada");
                        return;
                    }

                }
            }
            debugger;
            data_param.append('iMes', iMes);
            data_param.append('iAnio', iAnio);
            //data_param.append('iAnio', '2020');
            data_param.append('iCodPlanilla', iCodPlanilla);
            data_param.append('iCodTipoPlanilla', iCodTipoPlanilla);
                


            //var items = [iCodPlanilla, iMes, iAnio, iCodTipoPlanilla];
            //var existe = controlador.ConsultarEjecucionPlanilla(items);
               
            controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de crear la Planilla?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                
                            controladorApp.notificarMensajeSatisfactorio("La planilla se ha creado correctamente");
                            controlador.inicializarVerBandejaPlanilla();
                            controlador.cerrarModalAgregarPlanilla();
                            // REFRESCAR INFORMACION DEL TRABAJADOR

                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);

                            
            debugger;
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        
    };

    this.PlanillasJS.prototype.CerrarPlanilla = function (item) {
        //e.preventDefault();
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPlanilla = _item[0];
            items.iCodTipoPlanilla = _item[1];
            items.iMes = _item[2];
            items.iAnio = _item[3];
        }
        debugger;
        var data_param = new FormData();
            debugger;
            data_param.append('iMes', items.iMes);
            data_param.append('iAnio', items.iAnio);
            data_param.append('iCodPlanilla', items.iCodPlanilla);
            data_param.append('iCodTipoPlanilla', items.iCodTipoPlanilla);            

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Planilla/CerrarPlanilla',
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
                                controladorApp.notificarMensajeSatisfactorio("Operación realizada correctamente");                               
                                controlador.CargarBandejaPrincipalPlanillaEjecucion(event);                               
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            

        debugger;
    };

    this.PlanillasJS.prototype.GenerarReporteBCO = function (item) {
        //e.preventDefault();

        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPlanilla = _item[0];
            items.iCodTipoPlanilla = _item[1];
            items.iMes = _item[2];
            items.iAnio = _item[3];
        }
        debugger;
        var data_param = new FormData();
        //debugger;
        //data_param.append('iCodPlanilla', items.iCodPlanilla);
        //data_param.append('iCodTipoPlanilla', items.iCodTipoPlanilla);
        //data_param.append('iMes', items.iMes);
        //data_param.append('iAnio', items.iAnio);

        //controladorApp.abrirMensajeDeConfirmacion(
        //    '¿Está seguro de realizar la operación?', 'SI', 'NO'
        //    , controlador.GenerarReporteExcelBCO(item), data_param);
        controladorApp.abrirMensajeDeConfirmacion(
                            '¿Está seguro de generar Planilla CAS?', 'SI', 'NO'
                            , function (arg) {
                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Planilla/' + metodo,
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
                                            controladorApp.notificarMensajeSatisfactorio("La planilla CAS se ha generado correctamente");
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

        debugger;
    };

    this.PlanillasJS.prototype.GenerarReporteExcelBCO = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPlanilla = _item[0];
            items.iCodTipoPlanilla = _item[1];
            items.iMes = _item[2];
            items.iAnio = _item[3];
        }
        debugger;
        //var data_param = new FormData();
        //debugger;
        //data_param.append('iMes', items.iMes);
        //data_param.append('iAnio', items.iAnio);
        //data_param.append('iCodPlanilla', items.iCodPlanilla);
        //data_param.append('iCodTipoPlanilla', items.iCodTipoPlanilla);
        
        var ds = new kendo.data.DataSource({
            //type: "odata",
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarPlanillaCerrada',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        debugger;                               
                        data_param.iMes = items.iMes;
                        data_param.iAnio = items.iAnio;
                        data_param.iCodPlanilla = items.iCodPlanilla;
                        data_param.iCodTipoPlanilla = items.iCodTipoPlanilla;
                    }

                    return $.toDictionary(data_param);
                }
            },
            schema: {
                data: function (data) {
                    debugger;
                    return data;
                },
                total: function (data) {
                    debugger;
                    return data['odata.count'];
                },
                errors: function (data) {
                    debugger;
                },
                model: {
                    id: "iCodTrabajador",
                    fields: {                               
                        sNombreTipoDocumento: { type: "string" },
                        sNroDocumento: { type: "string" },
                        sNombreCompleto: { type: "string" },
                        sCargo: { type: "string" }
                    }
                }
            }
        });
        debugger;                
                
        var rows = [{
            cells: [
                // The first cell.
                //{ value: "iCodTrabajador" },                   
                {
                    value: "Tipo Documento",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },
                {
                    value: "Nro Documento",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },                
                {
                    value: "Apellidos y Nombres",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },
                {
                    value: "Banco",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },
                {
                    value: "Nro Cuenta a Abonar",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },
                {
                    value: "Nro CCI",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                },
                {
                    value: "Importe Abonar",
                    bold: true,
                    background: "#217DAD",
                    vAlign: "center",
                    hAlign: "center",
                    color: "#FFFFFF"
                }
                      
            ]
        }];
        debugger;
        // Use fetch so that you can process the data when the request is successfully completed.
        ds.fetch(function () {
            var data = this.data();
            debugger;
            for (var i = 0; i < data.length; i++) {
                // Push single row for every record.
                debugger;
                rows.push({
                    cells: [
                        //{ value: data[i].iCodTrabajador },
                        { value: data[i].TipoDocumento },
                        { value: data[i].NroDocumento },
                        { value: data[i].NombreCompleto },
                        { value: data[i].Banco },
                        { value: data[i].NroCuenta },
                        { value: data[i].NroCCI },
                        { value: data[i].ImporteAbonar, format: "#,##0.00" }
                    ]
                })
            }
            var workbook = new kendo.ooxml.Workbook({
                sheets: [
                    {
                        columns: [
                        // Column settings (width).
                        //{ autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true },
                        { autoWidth: true }                                
                        ],
                        // The title of the sheet.
                        title: "Reporte Planilla BCO",
                        // The rows of the sheet.
                        rows: rows
                    }
                ]
            });
            // Save the file as an Excel file with the xlsx extension.
            kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: "Reporte Planilla BCO.xlsx" });
        });
        controladorApp.notificarMensajeSatisfactorio("Reporte exportado con éxito");
            

    }

    this.PlanillasJS.prototype.abrirModalAgregarPlanilla = function (e) {
        e.preventDefault();
        debugger;
        frmModalAgregarPlanilla = $("#frmModalAgregarPlanilla").kendoValidator().data("kendoValidator");
        var modal = $('#ModalAgregarPlanilla').data('kendoWindow');

        modal.title("Agregar Planilla");
        $("#ddlPlanilla").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "vNombrePlanilla",
            dataValueField: "iCodPlanilla",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillasBase",
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
        $("#ddlTipoPlanilla").kendoDropDownList({
            autoBind: true,
            optionLabel: "SELECCIONE",
            dataTextField: "vNombre",
            dataValueField: "iCodTipoPlanilla",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarTipoPlanillasBase",
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
        $("#ddlMesPlanilla").kendoDropDownList({
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
        $("#ddlAnioPlanilla").kendoDropDownList({
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

        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarPlanilla = function () {
        debugger;
        var modal = $('#ModalAgregarPlanilla').data('kendoWindow');
        
        modal.close();
    }
    $('#ModalAgregarPlanilla').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Planila',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.abrirModalPlanillaReporte = function (item) {
        //e.preventDefault();
        //debugger;
        debugger;
        //var items = new Object();
        if (item != 0) {
            var _item = item.split(',');
            $("#hdiCodPlanillaRpte").val(_item[0]);
            $("#hdiCodTipoPlanillaRpte").val(_item[1]);
            $("#hdiMesRpte").val(_item[2]);
            $("#hdiAnioRpte").val(_item[3]);            
        }
        frmModalPlanillaReporte = $("#frmModalAgregarPlanilla").kendoValidator().data("kendoValidator");
        var modal = $('#ModalPlanillaReporte').data('kendoWindow');

        modal.title("Reportes");        
        $("#ddlReportes").kendoDropDownList({
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
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarReportes",
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

        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalPlanillaReporte = function () {
        debugger;
        var modal = $('#ModalPlanillaReporte').data('kendoWindow');
        
        modal.close();
    }
    $('#ModalPlanillaReporte').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Reportes',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");
        
    this.PlanillasJS.prototype.GenerarReporteResumenGeneral = function () {
        //e.preventDefault();
        var items = new Object();
        debugger;
        var Codigo = $("#ddlReportes").data("kendoDropDownList").value();
        var Nombre = $("#ddlReportes").data("kendoDropDownList").text();
        if (Codigo!="") {
            items.Codigo = Codigo;
            items.Nombre = Nombre;
        }
        
        var data_param = new FormData();
        debugger;
        var iMes = $("#hdiMesRpte").val();
        var iAnio = $("#hdiAnioRpte").val();
        var iCodPlanilla = $("#hdiCodPlanillaRpte").val();
        var iCodTipoPlanilla = $("#hdiCodTipoPlanillaRpte").val();
        var sMes = "";
        switch (iMes) {
            case "1":
                {
                    sMes = "Enero";
                    break;
                }
            case "2":
                {
                    sMes = "Febrero";
                    break;
                }
            case "3":
                {
                    sMes = "Marzo";
                    break;
                }
            case "4":
                {
                    sMes = "Abril";
                    break;
                }
            case "5":
                {
                    sMes = "Mayo";
                    break;
                }
            case "6":
                {
                    sMes = "Junio";
                    break;
                }
            case "7":
                {
                    sMes = "Julio";
                    break;
                }
            case "8":
                {
                    sMes = "Agosto";
                    break;
                }
            case "9":
                {
                    sMes = "Septiembre";
                    break;
                }
            case "10":
                {
                    sMes = "Octubre";
                    break;
                }
            case "11":
                {
                    sMes = "Noviembre";
                    break;
                }
            case "12":
                {
                    sMes = "Diciembre";
                    break;
                }
        }

        var cantReg = $("#lblTotal").html();
        if (cantReg != "" && cantReg != "0") {
            if (iCodPlanilla != "" && iCodTipoPlanilla != "") {
                var ds = new kendo.data.DataSource({
                    //type: "odata",
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Planilla/ReporteResumenGeneral',
                            type: 'POST',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                debugger;
                                data_param.iMes = iMes;
                                data_param.iAnio = iAnio;
                                data_param.iCodPlanilla = iCodPlanilla;
                                data_param.iCodTipoPlanilla = iCodTipoPlanilla;                                
                            }

                            return $.toDictionary(data_param);
                        }
                    },
                    schema: {
                        data: function (data) {
                            debugger;
                            return data;
                        },
                        total: function (data) {
                            debugger;
                            return data['odata.count'];
                        },
                        errors: function (data) {
                            debugger;
                        },
                        model: {
                            id: "iCodTrabajador",
                            fields: {                                
                                sTipoConcepto: { type: "string" },
                                sClasificadorGastoIng: { type: "string" },
                                sConceptoIng: { type: "string" },
                                dcMontoIng: { type: "number" },
                                iCantIng: { type: "number" },
                                sClasificadorGastoDscto: { type: "string" },
                                sConceptoDscto: { type: "string" },
                                dcMontoDscto: { type: "number" },
                                iCantDscto: { type: "number" },
                                sClasificadorGastoAporte: { type: "string" },
                                sConceptoAporte: { type: "string" },
                                dcMontoAporte: { type: "number" },
                                iCantAporte: { type: "number" }

                            }
                        }
                    }
                });
                debugger;
                var rows = [{
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "",
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 11
                      }
                    ]
                }];
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: items.Nombre,
                          bold: true,
                          fontSize: 16,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 11,
                          color: "#000000"
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Contraprestación Normal del Mes de " + sMes + " del " + iAnio,
                          bold: true,
                          vAlign: "center",
                          hAlign: "center",
                          colSpan: 11
                      }
                    ]
                });
                rows.push({
                    cells: [
                       // The first cell.
                      //{ value: "iCodTrabajador" },                   
                      {
                          value: "Ingresos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 5,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 1,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      },
                      {
                          value: "Descuentos",
                          bold: true,
                          background: "#217DAD",
                          vAlign: "center",
                          hAlign: "center",
                          color: "#FFFFFF",
                          colSpan: 5,
                          borderTop: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderBottom: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderLeft: {
                              color: "#FFFFFF",
                              size: 5
                          },
                          borderRight: {
                              color: "#FFFFFF",
                              size: 5
                          }
                      }                      
                    ]
                });
                if (items.Codigo =="2") {
                    rows.push({
                        cells: [
                           // The first cell.
                          //{ value: "iCodTrabajador" },                   
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          }

                        ]
                    });
                }
                if (items.Codigo == "3") {
                    rows.push({
                        cells: [
                           // The first cell.
                          //{ value: "iCodTrabajador" },                   
                          {
                              value: "Partida",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Partida",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          }

                        ]
                    });
                }
                if (items.Codigo == "1") {
                    rows.push({
                        cells: [
                           // The first cell.
                          //{ value: "iCodTrabajador" },                   
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Concepto",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Nro Per",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Parcial",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          },
                          {
                              value: "Total",
                              bold: true,
                              background: "#217DAD",
                              vAlign: "center",
                              hAlign: "center",
                              color: "#FFFFFF"
                          }

                        ]
                    });
                }
                debugger;
                // Use fetch so that you can process the data when the request is successfully completed.
                ds.fetch(function () {
                    var data = this.data();
                    debugger;
                    for (var i = 0; i < data.length; i++) {
                        // Push single row for every record.
                        debugger;
                        if (items.Codigo == "2") {
                            if (i < (data.length - 1)) {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: "" },
                                      { value: data[i].sConceptoIng },
                                      { value: data[i].iCantIng },
                                      { value: data[i].dcMontoIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].sConceptoDscto },
                                      { value: data[i].iCantDscto },
                                      { value: data[i].dcMontoDscto, format: "#,##0.00" },
                                      { value: "" }
                                    ]
                                })
                            }
                            else {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalDscto, format: "#,##0.00" }
                                    ]
                                })
                            }
                        }
                        if (items.Codigo == "3") {
                            if (i < (data.length - 1)) {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: data[i].sClasificadorGastoIng },
                                      { value: data[i].sConceptoIng },
                                      { value: data[i].iCantIng },
                                      { value: data[i].dcMontoIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].sClasificadorGastoDscto },
                                      { value: data[i].sConceptoDscto },
                                      { value: data[i].iCantDscto },
                                      { value: data[i].dcMontoDscto, format: "#,##0.00" },
                                      { value: "" }
                                    ]
                                })
                            }
                            else {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalDscto, format: "#,##0.00" }
                                    ]
                                })
                            }
                        }
                        if (items.Codigo == "1") {
                            if (i < (data.length - 1)) {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: "" },
                                      { value: data[i].sConceptoIng },
                                      { value: data[i].iCantIng },
                                      { value: data[i].dcMontoIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].sConceptoDscto },
                                      { value: data[i].iCantDscto },
                                      { value: data[i].dcMontoDscto, format: "#,##0.00" },
                                      { value: "" }
                                    ]
                                })
                            }
                            else {
                                rows.push({
                                    cells: [
                                      //{ value: data[i].iCodTrabajador },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalIng, format: "#,##0.00" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: "" },
                                      { value: data[i].dcTotalDscto, format: "#,##0.00" }
                                    ]
                                })
                            }
                        }
                        
                    }                    
                    debugger;
                    if (items.Codigo == "2") {
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },                              
                              {
                                  value: "Cuotas Patronales",
                                  colSpan: 2,
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },                              
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: data[data.length - 2].sConceptoAporte },
                              { value: data[data.length - 2].iCantAporte },
                              { value: data[data.length - 2].dcMontoAporte, format: "#,##0.00" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: data[data.length - 1].dcTotalAportes, format: "#,##0.00" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Gastos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalIng, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Descuentos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalDscto, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Liquido",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalLiquido, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              }
                            ]
                        })                        
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "Total Cuotas Patronales",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: data[data.length - 1].dcTotalAportes, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              }
                            ]
                        })
                    }
                    if (items.Codigo == "3") {
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        debugger;
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              {
                                  value: "Cuotas Patronales",
                                  colSpan: 2,
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              { value: "" },
                              { value: "" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: data[data.length - 2].sClasificadorGastoAporte },
                              { value: data[data.length - 2].sConceptoAporte },
                              { value: data[data.length - 2].iCantAporte },
                              { value: data[data.length - 2].dcMontoAporte, format: "#,##0.00" },
                              { value: "" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: "" },
                              { value: data[data.length - 1].dcTotalAportes, format: "#,##0.00" }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Gastos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalIng, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Descuentos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalDscto, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Liquido",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalLiquido, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "Total Cuotas Patronales",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: data[data.length - 1].dcTotalAportes, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              }
                            ]
                        })
                    }
                    if (items.Codigo == "1") {
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Gastos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalIng, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: "Total Descuentos",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              },
                              {
                                  value: data[data.length - 1].dcTotalDscto, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF"
                              }
                            ]
                        })
                        rows.push({
                            cells: [
                              //{ value: data[i].iCodTrabajador },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "Total Liquido",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: data[data.length - 1].dcTotalLiquido, format: "#,##0.00",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              },
                              {
                                  value: "",
                                  bold: true,
                                  background: "#217DAD",
                                  vAlign: "center",
                                  hAlign: "center",
                                  color: "#FFFFFF",
                                  borderTop: {
                                      color: "#FFFFFF",
                                      size: 5
                                  },
                              }
                            ]
                        })
                    }
                    var workbook = new kendo.ooxml.Workbook({
                        sheets: [
                          {
                              columns: [
                                // Column settings (width).
                                //{ autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true },
                                { autoWidth: true }
                                
                              ],
                              // The title of the sheet.
                              title: items.Nombre,
                              // The rows of the sheet.
                              rows: rows
                          }
                        ]
                    });
                    // Save the file as an Excel file with the xlsx extension.
                    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: items.Nombre+".xlsx" });
                });
                controladorApp.notificarMensajeSatisfactorio("Reporte exportada con éxito");
            }
            else {
                controladorApp.notificarMensajeDeAlerta("Para exportar a excel primero debe realizar una búsqueda");
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta("No hay registros para exportar");
        }

    }
    //////////////////////////////SUSPENCION RETENCION DE 4TA CATEGORIA//////////////////////////////////
    this.PlanillasJS.prototype.inicializarSuspencionRetencion4taCat = function () {
        debugger;
        var frmAgregarTrabajadorSuspRet4Ta;
        var tt = new Date();
        var mm = tt.getMonth() + 1;        
        frmTrabajadoresSuspRet4Ta = $("#frmTrabajadoresSuspRet4Ta").kendoValidator().data("kendoValidator");
        //$('#btnAgregarTrabajadorSuspRet4Ta').kendoButton();
        //var botonAgregar = $('#btnAgregarTrabajadorSuspRet4Ta').data("kendoButton");
        //botonAgregar.enable(false);
        //frmBandejaCASAdicional = $("#frmBandejaCASAdicional").kendoValidator().data("kendoValidator");        
        $("#ddlMesTrabajadoresSuspRet4Ta").kendoDropDownList({
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
            }//,
            //change: function (e) {
            //    var value = this.value();

            //    if (value == mm) {                    
            //        botonAgregar.enable(true);
            //    }
            //    else {
            //        botonAgregar.enable(false);
            //    }
            //}
        });
        $("#ddlAnioTrabajadoresSuspRet4Ta").kendoDropDownList({
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
        
        controlador.CargarBandejaPrincipalSuspRet4Ta(event);
    }

    this.PlanillasJS.prototype.CargarBandejaPrincipalSuspRet4Ta = function (e) {
        debugger;
        e.preventDefault();
        var iMes = $('#ddlMesTrabajadoresSuspRet4Ta').data("kendoDropDownList").value();
        var iAnio = $('#ddlAnioTrabajadoresSuspRet4Ta').data("kendoDropDownList").value();
        if (iMes>0 && iAnio>0) {        
            this.$dataSource = [];
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarTrabajadoresSuspRet4Ta',
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {                        
                        
                            data_param.iMes = iMes;
                            data_param.iAnio = iAnio;
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
                    $("#lblTotalTrabajadorSuspRet4Ta").html(this.total());
                    debugger;
                },
                schema: {
                    total: function (response) {
                        //debugger;
                        //var TotalDeRegistros = 0;
                        //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "iCodTrabajador",
                        fields: {
                            iCodTrabajador: { type: "number" },
                            TipoDocumento: { type: "string" },
                            NroDocumento: { type: "string" },
                            NombreCompleto: { type: "string" },
                            iMes: { type: "number" },
                            iAnio: { type: "number" }
                        }
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
        }
        debugger;
        this.$grid = $("#divGridTrabajadoresSuspRet4Ta").kendoGrid({
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
                    field: "iCodTrabajador",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "TipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "NroDocumento",
                    title: "NRO DOCUMENTO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "NombreCompleto",
                    title: "APELLIDOS Y NOMBRES",
                    width: "200px",                    
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
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: "Quitar",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodTrabajador, item.iMes, item.iAnio];

                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.QuitarEmpleadosSuspRetencion4ta(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-trash" aria-hidden="true" data-uib-tooltip="Ver" title="Quitar Trabajador"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
                
            ]
        }).data();
        debugger;
        $('#filterBusqTrabajadorSuspRet4Ta').on('input', function (e) {
            var grid = $('#divGridTrabajadoresSuspRet4Ta').data('kendoGrid');
            var columns = grid.columns;
            debugger;
            var filter = { logic: 'or', filters: [] };
            columns.forEach(function (x) {
                if (x.field) {
                    var type = grid.dataSource.options.schema.model.fields[x.field].type;
                    if (type == 'string') {
                        filter.filters.push({
                            field: x.field,
                            operator: 'contains',
                            value: e.target.value
                        })
                    }
                    else if (type == 'number') {
                        if (isNumeric(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: e.target.value
                            });
                        }

                    } else if (type == 'date') {
                        var data = grid.dataSource.data();
                        for (var i = 0; i < data.length ; i++) {
                            var dateStr = kendo.format(x.format, data[i][x.field]);
                            // change to includes() if you wish to filter that way https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes
                            if (dateStr.startsWith(e.target.value)) {
                                filter.filters.push({
                                    field: x.field,
                                    operator: 'eq',
                                    value: data[i][x.field]
                                })
                            }
                        }
                    } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                        var bool = getBoolean(e.target.value);
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: bool
                        });
                    }
                }
            });
            grid.dataSource.filter(filter);
        });
    };

    this.PlanillasJS.prototype.QuitarEmpleadosSuspRetencion4ta = function (item) {
        //e.preventDefault();

        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodTrabajador = _item[0];            
            items.iMes = _item[1];
            items.iAnio = _item[2];
        }
        debugger;
        var data_param = new FormData();
        debugger;
        data_param.append('iMes', items.iMes);
        data_param.append('iAnio', items.iAnio);
        data_param.append('iCodTrabajador', items.iCodTrabajador);        

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de realizar la operación?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/QuitarTrabajadoresSuspRet4Ta',
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
                            controladorApp.notificarMensajeSatisfactorio("Operación realizada correctamente");
                            controlador.CargarBandejaPrincipalSuspRet4Ta(event);
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);


        debugger;
    };

    this.PlanillasJS.prototype.abrirModalAgregarTrabajadorSuspRet4Ta = function (e) {
        e.preventDefault();
        debugger;        
        frmAgregarTrabajadorSuspRet4Ta = $("#frmAgregarTrabajadorSuspRet4Ta").kendoValidator().data("kendoValidator");
        var modal = $('#ModalAgregarTrabajadorSuspRet4Ta').data('kendoWindow');

        modal.title("Agregar Trabajadores");       
        $("#ddlMesAgregarAgregarTrabajadorSuspRet4Ta").kendoDropDownList({
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
        $("#ddlAnioAgregarAgregarTrabajadorSuspRet4Ta").kendoDropDownList({
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

        $("#divAgregarTrabajadorSuspRet4Ta").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Formato para importar Trabajadores.xlsx",
                filterable: false
            },
            // los nombres de columnas deberian emparejar con el Excel
            columns: [
                { field: "DNI" },
                { field: "Nombre" },
                { field: "NroAutorizacionExoneracion" }                
            ],
            dataSource: [
                { DNI: "XXX", Nombre: "Prueba", NroAutorizacionExoneracion: "000000" }
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
                        var grid = $("#divAgregarTrabajadorSuspRet4Ta").data("kendoGrid");
                        var rows = value.split('\n');

                        dataImportarSuspRet4Ta = [];
                        for (var i = 0; i < rows.length; i++) {
                            var cells = rows[i].split('\t');
                            dataImportarSuspRet4Ta.push({
                                DNI: cells[0],
                                Nombre: cells[1],
                                NroAutorizacionExoneracion: cells[2]
                            });
                        };

                        grid.dataSource.data(dataImportarSuspRet4Ta);
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

        //this.inicializarGridControlTrabajadorSuspRet4Ta();
        var tt = new Date();
        var mm = tt.getMonth() + 1;
        //if (today.getMonth() > 1) var month = month - 1;
        var year = tt.getFullYear();

        $("#ddlAnioAgregarAgregarTrabajadorSuspRet4Ta").data("kendoDropDownList").value(year);
        $("#ddlMesAgregarAgregarTrabajadorSuspRet4Ta").data("kendoDropDownList").value(mm);
        modal.open().center();
    }
    this.PlanillasJS.prototype.cerrarModalAgregarTrabajadorSuspRet4Ta = function () {
        debugger;
        var modal = $('#ModalAgregarTrabajadorSuspRet4Ta').data('kendoWindow');

        modal.close();
    }
    $('#ModalAgregarTrabajadorSuspRet4Ta').kendoWindow({
        draggable: true,
        modal: true,
        pinned: false,
        resizable: false,
        width: '60%',
        height: 'auto',
        title: 'Agregar Trabajador',
        visible: false,
        position: { top: '25%', left: "10%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PlanillasJS.prototype.ejecutarRegistroSuspRet4Ta = function () {
        var modal = $('#ModalAgregarTrabajadorSuspRet4Ta').data('kendoWindow');

        var data_param = new FormData();
        var file = '';
        for (i = 0; i < dataImportarSuspRet4Ta.length; i++) {
            fila = dataImportarSuspRet4Ta[i].DNI + '|' + dataImportarSuspRet4Ta[i].NroAutorizacionExoneracion;
            data_param.append('formatos[' + i + ']', fila);
        }

        //var idPlanilla = $('#ddlPlanillaAsistPerm').data("kendoDropDownList").value();
        //if (idPlanilla == '') {
        //    controladorApp.notificarMensajeDeAlerta('Seleccione una planilla para realizar la importación');
        //    return;
        //}

        //var item = idPlanilla.split('|');   
        var iMes = $('#ddlMesAgregarAgregarTrabajadorSuspRet4Ta').data("kendoDropDownList").value();
        var iAnio = $('#ddlAnioAgregarAgregarTrabajadorSuspRet4Ta').data("kendoDropDownList").value();
        var gridTra = $("#divAgregarTrabajadorSuspRet4Ta").data().kendoGrid.dataSource.view();
        

        if (iMes > 0 && iAnio > 0) {
            if (gridTra.length > 0) {
                data_param.append('iAnio', iAnio);
                data_param.append('iMes', iMes);
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Planilla/InsertarTrabajadoresSuspRet4Ta',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data_param,
                    success: function (res) {
                        controladorApp.notificarMensajeDeAlerta('Operación satisfactoria');

                        modal.close();
                        //this.inicializarGridControlTrabajador();
                        controlador.CargarBandejaPrincipalSuspRet4Ta(event);
                    },
                    error: function (res) {
                        debugger;
                        controladorApp.notificarMensajeDeAlerta(res); //[0].responseText);
                    }
                });
            }
            else {
                controladorApp.notificarMensajeDeAlerta('Debe agregar trabajadores');
                return;
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Seleccione Año y Mes para continuar');
            return;
        }
        
    }
    
    //////////////////////////////BANDEJA REGIMEN PENSIONARIO//////////////////////////////////
    //this.PlanillasJS.prototype.inicializarVerBandejaRegimenPensionario = function () {
    //    debugger;
    //    this.CargarBandejaPrincipalRegimenPensionario(event);
    //}

    //this.PlanillasJS.prototype.CargarBandejaPrincipalRegimenPensionario = function (e) {
    //    e.preventDefault();
    //    this.$dataSource = [];
    //    this.$dataSource = new kendo.data.DataSource({
    //        serverPaging: true,
    //        serverSorting: true,
    //        batch: false,
    //        transport: {
    //            read: {
    //                url: controladorApp.obtenerRutaBase() + 'Planilla/ListarRegistroRegimenPensionario',
    //                type: 'POST',
    //                dataType: 'json',
    //                cache: false
    //            },
    //            parameterMap: function ($options, $operation) {
    //                var data_param = {};

    //                if ($operation === "read") {                        
                        
    //                    data_param.iMes = '9';
    //                    data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
    //                    data_param.Grilla = {};
    //                    data_param.Grilla.RegistrosPorPagina = $options.pageSize;
    //                    data_param.Grilla.PaginaActual = $options.page
    //                    if ($options !== undefined && $options.sort !== undefined) {
    //                        data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
    //                        data_param.Grilla.OrdenarPor = $options.sort[0].field;
    //                    }
    //                }

    //                return $.toDictionary(data_param);
    //            }
    //        },
    //        //change: function (e) {
    //        //    $("#lblTotal").html(this.total());
    //        //    debugger;
    //        //},
    //        schema: {
    //            //total: function (response) {
    //            //    //debugger;
    //            //    //var TotalDeRegistros = 0;
    //            //    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
    //            //    return response.length; // TotalDeRegistros;
    //            //},
    //            model: {
    //                id: "iCodRegistroRegimenPen"
    //            }
    //        },
    //        //group: {
    //        //    field: "strOrgano", aggregates: [
    //        //       { field: "strOrgano", aggregate: "count" }
    //        //    ]
    //        //},
    //        //aggregate: [
    //        //        { field: "strOrgano", aggregate: "count" },
    //        //        { field: "strOrgano", aggregate: "count" }
    //        //]
    //    });
    //    debugger;
    //    this.$grid = $("#divGridPensiones").kendoGrid({
    //        //toolbar: ["excel", ],
    //        //excel: {
    //        //    fileName: "Listado de Bases de Convocatoria.xlsx",
    //        //    filterable: false
    //        //},
    //        dataSource: this.$dataSource,
    //        autoBind: true,
    //        selectable: true,
    //        scrollable: false,
    //        sortable: false,
    //        pageable: false,
    //        groupable: false,            
    //        dataType: 'json',            
    //        columns: [
    //            {
    //                field: "iCodRegistroRegimenPen",
    //                title: "ID",
    //                attributes: { style: "text-align:right;" },
    //                width: "30px",
    //                hidden: true
    //            },
    //            {
    //                field: "iCodRegimenPen",
    //                title: "TIPO DOCUMENTO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" },
    //                hidden: true
    //            },
    //            {
    //                field: "iCodTipoRegimenPen",
    //                title: "NRO DOCUMENTO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" },
    //                hidden: true
    //            },
    //            {
    //                field: "iCodAPF",
    //                title: "APELLIDOS Y NOMBRES",
    //                width: "200px",
    //                hidden: true
    //            },
    //            {
    //                field: "iMes",
    //                title: "CARGO",
    //                width: "300px",
    //                hidden: true
    //            },
    //            {
    //                field: "iAnio",
    //                title: "REG. PENSIONARIO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" },
    //                hidden: true
    //            },
    //            {
    //                field: "sNombre",
    //                title: "REGIMEN PENSIONARIO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "sTipo",
    //                title: "TIPO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "dcComision",
    //                title: "COMISION FLUJO/MIXTA",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "dcPrimaSeguro",
    //                title: "PRIMA SEGURO",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "dcAporte",
    //                title: "APORTE",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "dcTope",
    //                title: "TOPE",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" },
    //                format: "{0:c}"
    //            },
    //            {
    //                field: "sEtado",
    //                title: "T. ING.",
    //                width: "100px",
    //                attributes: { style: "text-align:right;" },
    //                hidden: true
    //            }
    //        ]
    //    }).data();
    //    debugger;
    //};

    //////////////////////////////BANDEJA CONCEPTOS//////////////////////////////////
    //this.PlanillasJS.prototype.inicializarVerBandejaConceptos = function () {
    //    debugger;
    //    this.CargarBandejaPrincipalConceptos(event);
    //}

    //this.PlanillasJS.prototype.CargarBandejaPrincipalConceptos = function (e) {
    //    e.preventDefault();
    //    this.$dataSource = [];
    //    this.$dataSource = new kendo.data.DataSource({
    //        serverPaging: true,
    //        serverSorting: true,
    //        batch: false,
    //        transport: {
    //            read: {
    //                url: controladorApp.obtenerRutaBase() + 'Planilla/ListarConceptos',
    //                type: 'POST',
    //                dataType: 'json',
    //                cache: false
    //            },
    //            parameterMap: function ($options, $operation) {
    //                var data_param = {};

    //                if ($operation === "read") {

    //                    data_param.iMes = '9';
    //                    data_param.iAnio = '2020'; //$("#txtFechaInicio").data("kendoDatePicker").value();                        
    //                    data_param.Grilla = {};
    //                    data_param.Grilla.RegistrosPorPagina = $options.pageSize;
    //                    data_param.Grilla.PaginaActual = $options.page
    //                    if ($options !== undefined && $options.sort !== undefined) {
    //                        data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
    //                        data_param.Grilla.OrdenarPor = $options.sort[0].field;
    //                    }
    //                }

    //                return $.toDictionary(data_param);
    //            }
    //        },
    //        //change: function (e) {
    //        //    $("#lblTotal").html(this.total());
    //        //    debugger;
    //        //},
    //        schema: {
    //            //total: function (response) {
    //            //    //debugger;
    //            //    //var TotalDeRegistros = 0;
    //            //    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
    //            //    return response.length; // TotalDeRegistros;
    //            //},
    //            model: {
    //                id: "vConcepto"
    //            }
    //        },
    //        //group: {
    //        //    field: "strOrgano", aggregates: [
    //        //       { field: "strOrgano", aggregate: "count" }
    //        //    ]
    //        //},
    //        //aggregate: [
    //        //        { field: "strOrgano", aggregate: "count" },
    //        //        { field: "strOrgano", aggregate: "count" }
    //        //]
    //    });
    //    debugger;
    //    this.$grid = $("#divGridConceptos").kendoGrid({
    //        //toolbar: ["excel", ],
    //        //excel: {
    //        //    fileName: "Listado de Bases de Convocatoria.xlsx",
    //        //    filterable: false
    //        //},
    //        dataSource: this.$dataSource,
    //        autoBind: true,
    //        selectable: true,
    //        scrollable: false,
    //        sortable: false,
    //        pageable: false,
    //        groupable: false,
    //        dataType: 'json',
    //        columns: [
    //            {
    //                field: "vConcepto",
    //                title: "CONCEPTO",
    //                attributes: { style: "text-align:center;" },
    //                width: "100px"
    //            },
    //            {
    //                field: "vTipoConcepto",
    //                title: "TIPO CONCEPTO",
    //                width: "100px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "vSubTipoConcepto",
    //                title: "SUB TIPO CONCEPTO",
    //                width: "100px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "vRegCAS",
    //                title: "REG. CAS",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "vRegFunc",
    //                title: "REG. FUNC.",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            },
    //            {
    //                field: "vRegSeci",
    //                title: "REG. SECIGRISTA",
    //                width: "30px",
    //                attributes: { style: "text-align:center;" }
    //            }
    //        ]
    //    }).data();
    //    debugger;
    //};

    ////////////////////////////BANDEJA REGIMEN PENSIONARIO//////////////////////////////////
    this.PlanillasJS.prototype.inicializarImportarAsistencia = function () {
        debugger;
        $("#ddlAnioAsistPerm").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMesAsistPerm").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPlanillaAsistPerm").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "IdNombrePlanilla",
            dataValueField: "IdRegistroPlanilla",
            cascadeFrom: "ddlMesAsistPerm",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPlanilla = 0;
                            data_param.IdTipoPlanilla = 0;
                            data_param.IdMes = $('#ddlMesAsistPerm').data("kendoDropDownList").value();
                            data_param.IdAnio = $('#ddlAnioAsistPerm').data("kendoDropDownList").value();
                            
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

        $("#ddlAnioAsistPerm_busqueda").kendoDropDownList({
            autoBind: true,
            //optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMesAsistPerm_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPlanillaAsistPerm_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "IdNombrePlanilla",
            dataValueField: "IdRegistroPlanilla",
            cascadeFrom: "ddlMesAsistPerm_busqueda",
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Planilla/ListarPlanillas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPlanilla = 0;
                            data_param.IdTipoPlanilla = 0;
                            data_param.IdMes = $('#ddlMesAsistPerm_busqueda').data("kendoDropDownList").value();
                            data_param.IdAnio = $('#ddlAnioAsistPerm_busqueda').data("kendoDropDownList").value();

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

        $('#divModalImportar').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '80%',
            height: 'auto',
            title: 'Importar asistencia',
            visible: false,
            position: { top: '10%', left: "10%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $("#divGridImportar").kendoGrid({
            toolbar: ["excel",],
            excel: {
                fileName: "Formato para importar asistencia.xlsx",
                filterable: false
            },
            // los nombres de columnas deberian emparejar con el Excel
            columns: [
                { field: "DNI" },
                { field: "Nombre" },
                { field: "Vacaciones" },
                { field: "Faltas" },
                { field: "Licencia" },
                { field: "Tardanza" },
                { field: "Permisos" },
                { field: "Salidas" }
            ],
            dataSource: [
                { DNI: "XXX", Nombre: "Prueba", Vacaciones: 0, Faltas: 0 }
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
                        var grid = $("#divGridImportar").data("kendoGrid");
                        var rows = value.split('\n');

                        dataImportar = [];
                        for (var i = 0; i < rows.length; i++) {
                            var cells = rows[i].split('\t');
                            dataImportar.push({
                                DNI: cells[0],
                                Nombre: cells[1],
                                Vacaciones: cells[2],
                                Faltas: cells[3],
                                Licencia: cells[4],
                                Tardanza: cells[5],
                                Permisos: cells[6],
                                Salidas: cells[7]
                            });
                        };

                        grid.dataSource.data(dataImportar);
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

        //this.inicializarGridControlTrabajador();
        controlador.buscar(event);
        //this.CargarBandejaPrincipalRegimenPensionario(event);
    }

    this.PlanillasJS.prototype.abrirModalImportarAsistencia = function () {
        var modal = $('#divModalImportar').data('kendoWindow');

        modal.open().center();
    }
    this.PlanillasJS.prototype.ejecutarImportarAsistencia = function () {
        var modal = $('#divModalImportar').data('kendoWindow');

        var data_param = new FormData();
        var file = '';
        for (i = 0; i < dataImportar.length; i++) {
            fila = dataImportar[i].DNI + '|' + dataImportar[i].Vacaciones + '|' + dataImportar[i].Faltas + '|' + dataImportar[i].Licencia + '|' + dataImportar[i].Tardanza + '|' + dataImportar[i].Permisos + '|' + dataImportar[i].Salidas;
            data_param.append('formatos[' + i + ']', fila);
        }

        var idPlanilla = $('#ddlPlanillaAsistPerm').data("kendoDropDownList").value();
        if (idPlanilla == '') {
            controladorApp.notificarMensajeDeAlerta('Seleccione una planilla para realizar la importación');
            return;
        }

        var item = idPlanilla.split('|');
        data_param.append('IdPlanilla', item[0]);
        data_param.append('IdTipoPlanilla', item[1]);
        data_param.append('IdAnio', item[2]);
        data_param.append('IdMes', item[3]);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Planilla/EjecutarImportarAsistencia',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                controladorApp.notificarMensajeDeAlerta('La importación se realizó con éxito');

                modal.close();
                //this.inicializarGridControlTrabajador();
            },
            error: function (res) {
                debugger;
                controladorApp.notificarMensajeDeAlerta(res); //[0].responseText);
            }
        });
    }
    this.PlanillasJS.prototype.inicializarGridControlTrabajador = function () {
        var iMes = $('#ddlMesAsistPerm_busqueda').data("kendoDropDownList").value();
        var iAnio = $('#ddlAnioAsistPerm_busqueda').data("kendoDropDownList").value();
        var arrayIdPlanilla = $('#ddlPlanillaAsistPerm').data("kendoDropDownList").value();
        var item = arrayIdPlanilla.split('|');
        var IdPlanilla = item[0];
        var IdTipoPlanilla = item[1];

        if (iMes > 0 && iAnio > 0) {

        }
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: false,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Planilla/ListarControlTrabajador',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {                        
                        data_param.IdPlanilla = IdPlanilla;
                        data_param.IdTipoPlanilla = IdTipoPlanilla;
                        data_param.IdMes = iMes;
                        data_param.IdAnio = iAnio;

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
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdPlanilla"
                }
            }//,
            //group: {
            //    field: "NombreOficina", aggregates: [
            //       { field: "NombreOficina", aggregate: "count" }
            //    ]
            //},
            //aggregate: [
            //    { field: "NombreCompleto", aggregate: "count" },
            //    { field: "NombreOficina", aggregate: "count" }
            //]
        });

        this.$grid = $("#divGridBandejaAsistPerm").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Listado de Asistencia.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "IdNombrePlanilla",
                    title: "PLANILLA",
                    //attributes: { style: "text-align:center;" },
                    width: "100px"
                },
                {
                    field: "NombreTrabajador",
                    title: "EMPLEADO",
                    width: "150px"//,
                    //aggregates: ["count"],
                    //footerTemplate: "Total: #= count#"
                },
                {
                    field: "NombreCondicion",
                    title: "CONDICIÓN",
                    width: "50px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreDependencia",
                    title: "DEPENDENCIA",
                    width: "200px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "DiasLaborados",
                    title: "DIAS<br>LABORADOS",
                    attributes: { style: "text-align:center" },
                    width: "30px"
                },
                {
                    field: "Vacaciones",
                    title: "VACACIONES",
                    attributes: { style: "text-align:center" },
                    width: "30px"
                },
                {
                    title: "FALTAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Faltas",
                            title: "DÍAS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteFaltas",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "LICENCIAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Licencia",
                            title: "DÍAS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteLicencias",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "TARDANZAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Tardanza",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteTardanzas",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "PERMISOS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Permisos",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImportePermisos",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "SALIDA ANTICIPADA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Permisos",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImportePermisos",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },

                {
                    field: "ImporteDescuento",
                    title: "IMPORTE",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    width: "50px"
                }//,
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.Estado == 0 && item.IdTieneArchivo < 1) {
                //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdContrato + '\')">'; //GenerarFormatoContrato
                //            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar contrato"></span>';
                //            controles += '</button>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.Estado == 1 && item.IdTieneArchivo == 1) {
                //            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Contrato/DescargarArchivo/?id=' + item.IdContrato + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span></a>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //}
            ]
        }).data();
    };
    //this.PlanillasJS.prototype.inicializarGridControlTrabajador = function () {
        
    //    this.$dataSource = [];
    //    this.$dataSource = new kendo.data.DataSource({
    //        serverPaging: false,
    //        serverSorting: true,
    //        batch: false,
    //        transport: {
    //            read: {
    //                url: controladorApp.obtenerRutaBase() + 'Planilla/ListarControlTrabajador',
    //                type: 'GET',
    //                dataType: 'json',
    //                cache: false
    //            },
    //            parameterMap: function ($options, $operation) {
    //                var data_param = {};

    //                if ($operation === "read") {
    //                    data_param.IdPlanilla = 0;
    //                    data_param.IdTipoPlanilla = 0;                        
    //                    data_param.IdMes = $('#ddlMesAsistPerm_busqueda').data("kendoDropDownList").value();;
    //                    data_param.IdAnio = $('#ddlAnioAsistPerm_busqueda').data("kendoDropDownList").value();;

    //                    data_param.Grilla = {};
    //                    data_param.Grilla.RegistrosPorPagina = $options.pageSize;
    //                    data_param.Grilla.PaginaActual = $options.page
    //                    if ($options !== undefined && $options.sort !== undefined) {
    //                        data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
    //                        data_param.Grilla.OrdenarPor = $options.sort[0].field;
    //                    }
    //                }

    //                return $.toDictionary(data_param);
    //            }
    //        },
    //        change: function (e) {
    //            $("#lblTotal").html(this.total());
    //        },
    //        schema: {
    //            total: function (response) {
    //                //debugger;
    //                //var TotalDeRegistros = 0;
    //                //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
    //                return response.length; // TotalDeRegistros;
    //            },
    //            model: {
    //                id: "IdPlanilla"
    //            }
    //        }//,
    //        //group: {
    //        //    field: "NombreOficina", aggregates: [
    //        //       { field: "NombreOficina", aggregate: "count" }
    //        //    ]
    //        //},
    //        //aggregate: [
    //        //    { field: "NombreCompleto", aggregate: "count" },
    //        //    { field: "NombreOficina", aggregate: "count" }
    //        //]
    //    });

    //    this.$grid = $("#divGridBandejaAsistPerm").kendoGrid({
    //        toolbar: ["excel",],
    //        excel: {
    //            fileName: "Listado de Asistencia.xlsx",
    //            filterable: false
    //        },
    //        dataSource: this.$dataSource,
    //        autoBind: true,
    //        selectable: true,
    //        scrollable: false,
    //        sortable: false,
    //        pageable: false,
    //        groupable: true,

    //        dataType: 'json',
    //        columns: [
    //            {
    //                field: "IdNombrePlanilla",
    //                title: "PLANILLA",
    //                //attributes: { style: "text-align:center;" },
    //                width: "100px"
    //            },
    //            {
    //                field: "NombreTrabajador",
    //                title: "EMPLEADO",
    //                width: "150px"//,
    //                //aggregates: ["count"],
    //                //footerTemplate: "Total: #= count#"
    //            },
    //            {
    //                field: "NombreCondicion",
    //                title: "CONDICIÓN",
    //                width: "50px"//,
    //                //aggregates: ["count"],
    //                //groupHeaderTemplate: "#= value # (Total: #= count#)"
    //            },
    //            {
    //                field: "NombreDependencia",
    //                title: "DEPENDENCIA",
    //                width: "200px"//,
    //                //aggregates: ["count"],
    //                //groupHeaderTemplate: "#= value # (Total: #= count#)"
    //            },
    //            {
    //                field: "DiasLaborados",
    //                title: "DIAS<br>LABORADOS",
    //                attributes: { style: "text-align:center" },
    //                width: "30px"
    //            },
    //            {
    //                field: "Vacaciones",
    //                title: "VACACIONES",
    //                attributes: { style: "text-align:center" },
    //                width: "30px"
    //            },
    //            {
    //                title: "FALTAS",
    //                attributes: { style: "text-align:center" },
    //                columns: [
    //                    {
    //                        field: "Faltas",
    //                        title: "DÍAS",
    //                        attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
    //                        width: "50px"
    //                    },
    //                    {
    //                        field: "ImporteFaltas",
    //                        title: "IMPORTE",
    //                        attributes: { style: "text-align:right;" },
    //                        format: "{0:c}",
    //                        width: "50px"
    //                    }
    //                ]
    //            },
    //            {
    //                title: "LICENCIAS",
    //                attributes: { style: "text-align:center" },
    //                columns: [
    //                    {
    //                        field: "Licencia",
    //                        title: "DÍAS",
    //                        attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
    //                        width: "50px"
    //                    },
    //                    {
    //                        field: "ImporteLicencias",
    //                        title: "IMPORTE",
    //                        attributes: { style: "text-align:right;" },
    //                        format: "{0:c}",
    //                        width: "50px"
    //                    }
    //                ]
    //            },
    //            {
    //                title: "TARDANZAS",
    //                attributes: { style: "text-align:center" },
    //                columns: [
    //                    {
    //                        field: "Tardanza",
    //                        title: "MINUTOS",
    //                        attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
    //                        width: "50px"
    //                    },
    //                    {
    //                        field: "ImporteTardanzas",
    //                        title: "IMPORTE",
    //                        attributes: { style: "text-align:right;" },
    //                        format: "{0:c}",
    //                        width: "50px"
    //                    }
    //                ]
    //            },
    //            {
    //                title: "PERMISOS",
    //                attributes: { style: "text-align:center" },
    //                columns: [
    //                    {
    //                        field: "Permisos",
    //                        title: "MINUTOS",
    //                        attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
    //                        width: "50px"
    //                    },
    //                    {
    //                        field: "ImportePermisos",
    //                        title: "IMPORTE",
    //                        attributes: { style: "text-align:right;" },
    //                        format: "{0:c}",
    //                        width: "50px"
    //                    }
    //                ]
    //            },
    //            {
    //                title: "SALIDA ANTICIPADA",
    //                attributes: { style: "text-align:center" },
    //                columns: [
    //                    {
    //                        field: "Permisos",
    //                        title: "MINUTOS",
    //                        attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
    //                        width: "50px"
    //                    },
    //                    {
    //                        field: "ImportePermisos",
    //                        title: "IMPORTE",
    //                        attributes: { style: "text-align:right;" },
    //                        format: "{0:c}",
    //                        width: "50px"
    //                    }
    //                ]
    //            },

    //            {
    //                field: "ImporteDescuento",
    //                title: "IMPORTE",
    //                attributes: { style: "text-align:right;" },
    //                format: "{0:c}",
    //                width: "50px"
    //            }//,
    //            //{
    //            //    //INGRESAR DETALLE DE LA EVALUACION
    //            //    title: '',
    //            //    attributes: { style: "text-align:center;" },
    //            //    template: function (item) {
    //            //        var controles = "";
    //            //        if (item.Estado == 0 && item.IdTieneArchivo < 1) {
    //            //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdContrato + '\')">'; //GenerarFormatoContrato
    //            //            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar contrato"></span>';
    //            //            controles += '</button>';
    //            //        }

    //            //        return controles;
    //            //    },
    //            //    width: '30px'
    //            //},
    //            //{
    //            //    //INGRESAR DETALLE DE LA EVALUACION
    //            //    title: '',
    //            //    attributes: { style: "text-align:center;" },
    //            //    template: function (item) {
    //            //        var controles = "";
    //            //        if (item.Estado == 1 && item.IdTieneArchivo == 1) {
    //            //            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Contrato/DescargarArchivo/?id=' + item.IdContrato + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span></a>';
    //            //        }

    //            //        return controles;
    //            //    },
    //            //    width: '30px'
    //            //}
    //        ]
    //    }).data();
    //};

    //this.PlanillasJS.prototype.buscar = function (e) {
    //    e.preventDefault();

    //    var grilla = $('#divGridBandejaAsistPerm').data("kendoGrid");
    //    grilla.dataSource._sort = undefined;
    //    grilla.dataSource.page(1);

    //    //$("#lblTotal").html(grilla.dataSource.total());
    //};
    this.PlanillasJS.prototype.buscar = function (e) {
        e.preventDefault();
        debugger;
        //var grilla = $('#divGridBandejaAsistPerm').data("kendoGrid");
        //grilla.dataSource._sort = undefined;
        //grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());

        var iMes = $('#ddlMesAsistPerm_busqueda').data("kendoDropDownList").value();
        var iAnio = $('#ddlAnioAsistPerm_busqueda').data("kendoDropDownList").value();
        var arrayIdPlanilla = $('#ddlPlanillaAsistPerm_busqueda').data("kendoDropDownList").value();
        var item = arrayIdPlanilla.split('|');
        var IdPlanilla = item[0];
        var IdTipoPlanilla = item[1];
        this.$dataSource = [];
        if (iMes!="" && iAnio>0 && item!="") {        
            this.$dataSource = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Planilla/ListarControlTrabajador',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.IdPlanilla = IdPlanilla;
                            data_param.IdTipoPlanilla = IdTipoPlanilla;
                            data_param.IdMes = iMes;
                            data_param.IdAnio = iAnio;

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
                },
                schema: {
                    total: function (response) {
                        //debugger;
                        //var TotalDeRegistros = 0;
                        //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPlanilla"
                    }
                }//,
                //group: {
                //    field: "NombreOficina", aggregates: [
                //       { field: "NombreOficina", aggregate: "count" }
                //    ]
                //},
                //aggregate: [
                //    { field: "NombreCompleto", aggregate: "count" },
                //    { field: "NombreOficina", aggregate: "count" }
                //]
            });
        }
        this.$grid = $("#divGridBandejaAsistPerm").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Listado de Asistencia.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "IdNombrePlanilla",
                    title: "PLANILLA",
                    //attributes: { style: "text-align:center;" },
                    width: "100px"
                },
                {
                    field: "NombreTrabajador",
                    title: "EMPLEADO",
                    width: "150px"//,
                    //aggregates: ["count"],
                    //footerTemplate: "Total: #= count#"
                },
                {
                    field: "NombreCondicion",
                    title: "CONDICIÓN",
                    width: "50px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreDependencia",
                    title: "DEPENDENCIA",
                    width: "200px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "DiasLaborados",
                    title: "DIAS<br>LABORADOS",
                    attributes: { style: "text-align:center" },
                    width: "30px"
                },
                {
                    field: "Vacaciones",
                    title: "VACACIONES",
                    attributes: { style: "text-align:center" },
                    width: "30px"
                },
                {
                    title: "FALTAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Faltas",
                            title: "DÍAS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteFaltas",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "LICENCIAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Licencia",
                            title: "DÍAS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteLicencias",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "TARDANZAS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Tardanza",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImporteTardanzas",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "PERMISOS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Permisos",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImportePermisos",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "SALIDA ANTICIPADA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "Permisos",
                            title: "MINUTOS",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px"
                        },
                        {
                            field: "ImportePermisos",
                            title: "IMPORTE",
                            attributes: { style: "text-align:right;" },
                            format: "{0:c}",
                            width: "50px"
                        }
                    ]
                },

                {
                    field: "ImporteDescuento",
                    title: "IMPORTE",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    width: "50px"
                }//,
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.Estado == 0 && item.IdTieneArchivo < 1) {
                //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdContrato + '\')">'; //GenerarFormatoContrato
                //            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar contrato"></span>';
                //            controles += '</button>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.Estado == 1 && item.IdTieneArchivo == 1) {
                //            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Contrato/DescargarArchivo/?id=' + item.IdContrato + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span></a>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //}
            ]
        }).data();
    };
}(jQuery));