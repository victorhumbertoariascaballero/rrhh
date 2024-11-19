//import { Console } from "console";

(function ($) {
    var frmNuevoValidador;
    var frmRegistroValidador;
    var frmValidacionValidador;
    var strMensajes = '';
        
    this.BoletasJS = function () { };
    this.BoletasJS.prototype.inicializar = function () {

        $("#fileFirma").kendoUpload({
            multiple: true,
            async: {
                saveUrl: controladorApp.obtenerRutaBase() + 'Boletas/Validar',
                removeUrl: controladorApp.obtenerRutaBase() + 'Boletas/Remover',
                autoUpload: true
            },
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 102400
            },
            cancel: onCancel,
            complete: onCompleteF,
            error: onError,
            progress: onProgress,
            remove: onRemove,
            select: onSelect,
            success: onSuccess,
            upload: onUpload
        });

        /* BUSQUEDA */
        $("#ddlAnio_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlAnio_busquedaSisper").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        //$("#ddlAnio_busquedaSispla").kendoDropDownList({
        //    autoBind: false,
        //    optionLabel: "--Todos--",
        //    dataTextField: "Anio",
        //    dataValueField: "Anio",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Boletas/ListarAnios",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            }
        //        }
        //    }
        //});
        $("#ddlMes_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMes_busquedaSisper").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        //$("#ddlMes_busquedaSispla").kendoDropDownList({
        //    autoBind: false,
        //    optionLabel: "--Todos--",
        //    dataTextField: "Nombre",
        //    dataValueField: "Codigo",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Boletas/ListarMeses",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            }
        //        }
        //    }
        //});
        $("#ddlDependencia_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "IdDependencia",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Dependencia/ListarDependencias",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.term = $options.filter.filters[0].value;

                        //if (data_param.cod.length = 1) data_param.cod = '0' + data_param.cod;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        
        /* REGISTRO */
        $("#ddlAnio").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMes").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        
        $('#divModalNuevo').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Nueva Carga de Boletas',
            visible: false,
            position: { top: '5%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmNuevoValidador.hideMessages();                
            }
        }).data("kendoWindow");
        frmNuevoValidador = $("#frmNuevo").kendoValidator().data("kendoValidator");
    
        //$("#progressBar").kendoProgressBar({
        //    min: 0,
        //    max: 100,
        //    type: "percent",
        //    change: onChange,
        //    complete: onComplete
        //});
        
        $("#startProgress").click(function () {
            if ($("#divGridPlanillas").data("kendoGrid").select().length <= 0) {
                controladorApp.notificarMensajeDeAlerta("Por favor seleccione al menos 1 planilla para poder realizar la validación");
                return false;
            }
            else {
                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de validar las planillas seleccionadas?', 'SI', 'NO'
                    , function (arg) {
                        debugger;
                        var grid = $("#divGridPlanillas").data("kendoGrid");
                        var seleccionados = [];
                        var totalSeleccionados = 0;
                        grid.select().each(function () {
                            seleccionados.push(grid.dataItem(this).IdPlanilla + grid.dataItem(this).TipoPlanilla);
                            totalSeleccionados = totalSeleccionados + grid.dataItem(this).DiasLaborados;
                        })

                        var data_param = {};
                        data_param.Anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                        data_param.Mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();

                        var exito = true;
                        //$(document).off('ajaxSend');
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSisper',
                            timeout: 0,
                            type: 'GET',
                            dataType: 'json',
                            contentType: false, //'application/json; charset=utf-8',
                            processData: true, //true
                            data: data_param,
                            success: function (res) {
                                debugger;
                                if (res != null && res.length > 0) {
                                    var contador = 0;
                                    console.log(totalSeleccionados);
                                    res.forEach(function (item) {
                                        if (seleccionados.find(elemento => (elemento == item.IdPlanilla + item.TipoPlanilla)) !== undefined) {
                                            console.log('Encontro ' + item.IdPlanilla + item.TipoPlanilla);
                                            
                                            var data_par = {};
                                            data_par.Anio = item.Anio;
                                            data_par.Mes = item.Mes;
                                            data_par.NroDocumento = item.NroDocumento;
                                            data_par.IdPlanilla = item.IdPlanilla;
                                            data_par.TipoPlanilla = item.TipoPlanilla;
                                            data_par.Trabajador = item.Trabajador;

                                            //pb.value(pb.value() + 1);
                                            //$('#bar').css('width', pb.value() + '%');
                                            debugger;
                                            $.ajax({
                                                url: controladorApp.obtenerRutaBase() + 'Boletas/ValidarBoleta',
                                                //timeout: 0,
                                                //async: false,
                                                type: 'GET',
                                                dataType: 'json',
                                                data: data_par,
                                                success: function (result) {
                                                    //alert(result);
                                                    if (result != null) {
                                                        debugger;
                                                        if (result.success == 'True') {
                                                            contador = contador + 1;

                                                            if (result.operacion == 1) {
                                                                var anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                                                                var mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                                                                var data_par = {};
                                                                data_par.Anio = anio;
                                                                data_par.Mes = mes;
                                                                
                                                                $.ajax({
                                                                    url: controladorApp.obtenerRutaBase() + 'Boletas/TotalBoletas',
                                                                    async: false,
                                                                    type: 'GET',
                                                                    dataType: 'json',
                                                                    data: data_par,
                                                                    success: function (result) {
                                                                        if (result != null) {
                                                                            $("#hdBoletas").val(result.Boletas);
                                                                            $("#hdBoletasValidas").val(result.BoletasValidas);
                                                                            $("#divBoletas").html(result.Boletas);
                                                                            $("#divBoletasValidas").html(result.BoletasValidas);

                                                                            $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);

                                                                            console.log('contador ' + contador);
                                                                            if (contador === totalSeleccionados) {
                                                                                controladorApp.notificarMensajeSatisfactorio("Se validaron correctamente las planillas seleccionadas");
                                                                            }
                                                                        }
                                                                    }
                                                                });
                                                            }
                                                        }
                                                        else {
                                                            exito = false;
                                                            $("#divMensajeError").show();
                                                        }
                                                    }
                                                }
                                            });
                                        }
                                    });

                                    //if ($("#startProgress").hasClass("k-state-disabled")) {
                                        //$("#startProgress").text("Validación Completa__");
                                        ////$("#startProgress").removeClass("k-state-disabled");

                                        //var anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                                        //var mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                                        //var data_par = {};
                                        //data_par.Anio = anio;
                                        //data_par.Mes = mes;

                                        //$.ajax({
                                        //    url: controladorApp.obtenerRutaBase() + 'Boletas/TotalBoletas',
                                        //    async: false,
                                        //    type: 'GET',
                                        //    dataType: 'json',
                                        //    data: data_par,
                                        //    success: function (result) {
                                        //        if (result != null) {
                                        //            $("#hdBoletas").val(result.Boletas);
                                        //            $("#hdBoletasValidas").val(result.BoletasValidas);
                                        //            $("#divBoletas").html(result.Boletas);
                                        //            $("#divBoletasValidas").html(result.BoletasValidas);

                                        //            $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);
                                        //            //pb.max = result.Boletas;
                                        //        }
                                        //    }
                                        //});
                                    //}
                                }
                            }
                        });

                        //$(document).on('ajaxSend');
                    }
                );
            }

            return false;
        });

        $('#divModalValidar').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '80%',
            height: 'auto',
            title: 'Validación de Boletas de Pago',
            visible: false,
            position: { top: '10%', left: "10%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                //frmNuevoValidador.hideMessages();
            }
        }).data("kendoWindow");
        
        $('#divModalEnvioMail').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Envío de Boleta de pago',
            visible: false,
            position: { top: '10%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                //frmNuevoValidador.hideMessages();
            }
        }).data("kendoWindow");
    
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
            position: { top: '5%', left: "35%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        debugger;
        var fecha = new Date();
        var anio = fecha.getFullYear();
        var mes = fecha.getMonth() + 1;
        mes = (mes.toString().length == 1 ? "0" : "") + mes.toString();
        $("#ddlAnio_busqueda").data("kendoDropDownList").value(anio);
        $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value(anio);
        //$("#ddlAnio_busquedaSispla").data("kendoDropDownList").value(anio);
        $("#ddlMes_busqueda").data("kendoDropDownList").value(mes);
        $("#ddlMes_busquedaSisper").data("kendoDropDownList").value(mes);
        //$("#ddlMes_busquedaSispla").data("kendoDropDownList").value(mes);

        this.inicializarGrid();
        this.inicializarGridSisper();
        //this.inicializarGridSispla();
    };

    this.BoletasJS.prototype.inicializarDescargaBoleta = function () {
        $("#ddlPersonaTipoDeDocumento").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarTipoDeDocumento",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        frmRegistroValidador = $("#frmRegistro").kendoValidator().data("kendoValidator");
        frmValidacionValidador = $("#frmValidar").kendoValidator().data("kendoValidator");

        if ($("#hdIdEstado").val() == '1') {
            $("#step1").removeAttr("disabled");
            $("#step1").removeClass("btn-default").addClass("btn-info");
            $("#liTab2").hide();
            $("#liTab3").hide();
        }
        if ($("#hdIdEstado").val() == '2') {
            $("#step2").removeAttr("disabled");
            $("#step2").removeClass("btn-default").addClass("btn-info");
            //$("#liTab2").hide();
            $("#liTab3").hide();

            $('#tab3').removeClass('in active');
            $('#tab1').removeClass('in active');
            $('#tab2').addClass('in active');
            $('#liTab3').removeClass('active');
            $('#liTab1').removeClass('active');
            $('#liTab2').addClass('active');
        }
        if ($("#hdIdEstado").val() == '3') {

        }

    }

    function onChange(e) {
        //kendoConsole.log("Change event :: value is " + e.value);
    }

    function onComplete(e) {
        $("#divMensajeExito").show();
        $("#startProgress").text("Validación Completa").addClass("k-state-disabled");
    }

    function progress() {
        var pb = $("#progressBar").data("kendoProgressBar");
        pb.value($("#divBoletasValidas").val());
        pb.enable();

        //kendo.ui.progress($("#progressBar"), true);
        //$.ajaxSetup({ async: false });

        var data_param = {};
        data_param.Anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
        data_param.Mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
        
        var exito = true;
        $(document).off('ajaxSend');
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSisper',
            timeout: 0, 
            type: 'GET',
            dataType: 'json',
            contentType: false, //'application/json; charset=utf-8',
            processData: true, //true
            data: data_param,
            success: function (res) {
                if (res != null && res.length > 0) {
                    res.forEach(function (item) {
                        var data_par = {};
                        data_par.Anio = item.Anio;
                        data_par.Mes = item.Mes;
                        data_par.NroDocumento = item.NroDocumento;
                        data_par.IdPlanilla = item.IdPlanilla;
                        data_par.TipoPlanilla = item.TipoPlanilla;
                        data_par.Trabajador = item.Trabajador;

                        //pb.value(pb.value() + 1);
                        //$('#bar').css('width', pb.value() + '%');
                        debugger;
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Boletas/ValidarBoleta',
                            //timeout: 0,
                            //async: false,
                            type: 'GET',
                            dataType: 'json',
                            data: data_par,
                            success: function (result) {
                                //alert(result);
                                if (result != null) {
                                    debugger;
                                    if (result.success == 'True') {
                                        if (result.operacion == 1) {
                                            //$("#hdBoletasValidas").val(parseInt($("#hdBoletasValidas").val()) + 1);
                                            //$("#divBoletasValidas").html($("#hdBoletasValidas").val());

                                            //pb.value(pb.value() + 1);
                                            //$('#bar').css('width', pb.value() + '%');

                                            var anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                                            var mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                                            var data_par = {};
                                            data_par.Anio = anio;
                                            data_par.Mes = mes;

                                            $.ajax({
                                                url: controladorApp.obtenerRutaBase() + 'Boletas/TotalBoletas',
                                                async: false,
                                                type: 'GET',
                                                dataType: 'json',
                                                data: data_par,
                                                success: function (result) {
                                                    if (result != null) {
                                                        $("#hdBoletas").val(result.Boletas);
                                                        $("#hdBoletasValidas").val(result.BoletasValidas);
                                                        $("#divBoletas").html(result.Boletas);
                                                        $("#divBoletasValidas").html(result.BoletasValidas);

                                                        $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);
                                                        //pb.max = result.Boletas;
                                                    }
                                                }
                                            });

                                        }
                                    }
                                    else {
                                        exito = false;
                                        $("#divMensajeError").show();
                                    }
                                    //pb.value(pb.value() + 1);

                                    
                                }
                            }
                        });
                    });

                    if ($("#startProgress").hasClass("k-state-disabled")) {
                        //$("#startProgress").text("Validación Completa__");
                        ////$("#startProgress").removeClass("k-state-disabled");
                        
                        //var anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                        //var mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                        //var data_par = {};
                        //data_par.Anio = anio;
                        //data_par.Mes = mes;

                        //$.ajax({
                        //    url: controladorApp.obtenerRutaBase() + 'Boletas/TotalBoletas',
                        //    async: false,
                        //    type: 'GET',
                        //    dataType: 'json',
                        //    data: data_par,
                        //    success: function (result) {
                        //        if (result != null) {
                        //            $("#hdBoletas").val(result.Boletas);
                        //            $("#hdBoletasValidas").val(result.BoletasValidas);
                        //            $("#divBoletas").html(result.Boletas);
                        //            $("#divBoletasValidas").html(result.BoletasValidas);

                        //            $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);
                        //            //pb.max = result.Boletas;
                        //        }
                        //    }
                        //});
                    }
                }
            }
        });

        $(document).on('ajaxSend');
    }

    function onSelect(e) {
        $("#console").empty();

        var files = e.files;
        for (var i = 0; i < files.length; i += 1) {
            var file = files[i];
            if (file.validationErrors && file.validationErrors.length > 0) {
                file.error = file.validationErrors[0];
            }
        }
    }

    function onUpload(e) {
        //debugger;
        e.formData = new FormData();
        e.formData.append('Anio', $('#ddlAnio').data("kendoDropDownList").value());
        e.formData.append('Mes', $("#ddlMes").data("kendoDropDownList").value());
        e.formData.append('NroDocumento', e.files[0].name.split('-')[0]);
        e.formData.append('NombreArchivo', e.files[0].name);
    }

    function onSuccess(e) {
        //debugger;
        if (e.response.success == 'False') {
            //console.log(e.response.responseText);
            strMensajes = '<div style="margin-top: 0px; background-color: rgb(255, 255, 255);">' + e.response.responseText + '</div>';
            $("#console").append(strMensajes); //$("#console").innerHTML + strMensajes;
            e.preventDefault();
            return false;
        }   
    }

    function onError(e) {
        //kendoConsole.log("Error (" + e.operation + ") :: " + getFileInfo(e));
    }

    function onCompleteF(e) {
        $("#divConsole").show();
    }

    function onCancel(e) {
        //kendoConsole.log("Cancel :: " + getFileInfo(e));
    }

    function onRemove(e) {
        //kendoConsole.log("Remove :: " + getFileInfo(e));
    }

    function onProgress(e) {
        console.log("Upload progress :: " + e.percentComplete + "% :: " + getFileInfo(e));
    }

    function getFileInfo(e) {
        return $.map(e.files, function (file) {
            var info = file.name;

            // File size is not available in all browsers
            if (file.size > 0) {
                info += " (" + Math.ceil(file.size / 1024) + " KB)";
            }
            return info;
        }).join(", ");
    }


    function LimpiarModalNuevo() {
        var fecha = new Date();
        var anio = fecha.getFullYear();
        var mes = fecha.getMonth() + 1;
        mes = (mes.toString().length == 1 ? "0" : "") + mes.toString();
        $("#ddlAnio").data("kendoDropDownList").value(anio);
        $("#ddlMes").data("kendoDropDownList").value(mes);
        $("#fileFirma").data("kendoUpload").clearAllFiles();
        $("#console").empty();
        $("#divConsole").hide();
    }

    function LimpiarModalValidar() {
        //var pb = $("#progressBar").data("kendoProgressBar");
        //pb.value(0);
        //pb.enable();

        //$('#bar').css('width', pb.value() + '%');
        $('#bar').css('width', 100 + '%');

        $("#hdBoletas").val(0);
        $("#hdBoletasValidas").val(0);

        $("#divMensajeError").hide();
        $("#divMensajeExito").hide();
        $("#startProgress").text("Iniciar Validación").removeClass("k-state-disabled");
    }

    this.BoletasJS.prototype.inicializarGrid = function () {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosBoleta',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Anio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
                        data_param.Mes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
                        data_param.IdDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        data_param.NroDocumento = $("#txtDNI_busqueda").val();
                        data_param.Nombre = $("#txtEmpleado_busqueda").val().toUpperCase();
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
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdEmpleado"
                }
            },
            group: {
                field: "AnioMes", aggregates: [
                   { field: "AnioMes", aggregate: "count" }
                   //{ field: "MontoMaximo", aggregate: "sum" },
                   //{ field: "MontoNoAlcanzado", aggregate: "sum" },
                   //{ field: "MontoAlcanzado", aggregate: "sum" }
                ]
            },
            aggregate: [
                    { field: "AnioMes", aggregate: "count" }
                    //{ field: "MontoMaximo", aggregate: "sum" },
                    //{ field: "MontoNoAlcanzado", aggregate: "sum" },
                    //{ field: "MontoAlcanzado", aggregate: "sum" }
            ]
        });

        
        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado de Boletas Laborales.xlsx",
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
                    field: "AnioMes",
                    title: "AÑO",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        return item.Anio;
                    },
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreMes",
                    title: "MES",
                    width: "30px"
                },
                {
                    field: "NombreOficina",
                    title: "OFICINA",
                    width: "300px"
                },
                {
                    field: "NroDocumento",
                    title: "NRO DOC",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreCompleto",
                    title: "EMPLEADO",
                    width: "200px"
                },
                {
                    field: "EstadoNombre",
                    title: "ESTADO EMPLEADO",
                    width: "30px"
                },
                {
                    field: "CorreoElectronicoLaboral",
                    title: "EMAIL",
                    attributes: { style: "text-align:center;" },
                    width: "50px"
                },
                {
                    field: "Planilla",
                    title: "PLANILLA",
                    width: "30px"
                },
                {
                    field: "TipoPlanilla",
                    title: "TIPO PLANILLA",
                    width: "30px"
                },
                {
                    field: "EstadoEnvioNombre",
                    title: "ESTADO ENVÍO",
                    width: "30px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "FechaEnvio",
                    title: "FECHA ENVÍO",
                    attributes: { style: "text-align:center;" },
                    width: "50px"
                },
                {
                    field: "FechaRecepcion",
                    title: "FECHA RECEPCIÓN",
                    attributes: { style: "text-align:center;" },
                    width: "50px"
                },
                {
                    //ENVIO DE BOLETA AL MAIL PERSONAL
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.Contrasena != '') {
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalEnvioMail(\'' + item.Contrasena + '\')">';
                            controles += '<span class="glyphicon glyphicon-envelope" aria-hidden="true" data-uib-tooltip="Editar"></span>';
                            controles += '</button>';
                        }
                            
                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.Contrasena != '')
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Boletas/DescargarArchivo/?id=' + item.Contrasena + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file"></span></a>';
                        
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        
        debugger;
    };
    this.BoletasJS.prototype.inicializarGridSisper = function () {
        this.$dataSourceS = [];
        this.$dataSourceS = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSisper',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                        data_param.Mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                        //data_param.IdDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        //data_param.IdEmpleado = $("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                $("#lblTotalSisper").html(this.total());
            },
            schema: {
                total: function (response) {
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "planilla"
                }
            },
            group: {
                field: "AnioMes", aggregates: [
                   { field: "AnioMes", aggregate: "count" },
                   { field: "Ingresos", aggregate: "sum" },
                   { field: "Descuentos", aggregate: "sum" },
                   { field: "Aportes", aggregate: "sum" }
                ]
            },
            aggregate: [
                    { field: "AnioMes", aggregate: "count" },
                    { field: "Ingresos", aggregate: "sum" },
                    { field: "Descuentos", aggregate: "sum" },
                    { field: "Aportes", aggregate: "sum" }
            ]
        });

        //$dataSource.fetch(function () {
        //    var total = this.total();
        //    $("#divTotal").append(total);
        //});

        this.$grid = $("#divGridSisper").kendoGrid({
            toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado de Boletas en SISPER.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceS,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,

            dataType: 'json',
            //dataBound: function () {
            //    $('#totalBits').remove();
            //    //add the total count to the pager div.  or whatever div you want... just remember to clear it before you add it.
            //    $('.k-grid-footer').append('<div id="totalBits">' + this.dataSource.total() + '</div>')
            //},
            columns: [
                {
                    field: "AnioMes",
                    title: "AÑO",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        return item.Anio;
                    },
                    aggregates: ["count"],
                    footerTemplate: "TOTAL: ",
                    groupFooterTemplate: "Sub Total: ",
                    //groupHeaderTemplate: "#= value # <button id='btnNuevo' type='button' class='btn btn-info btn-sm' onclick='controlador.ingresarDetalleDistribucion(#= aggregates.IdDistribucion.max #)' style='float:right'><span class='glyphicon glyphicon-new-window' aria-hidden='true'></span> Detalle de la Distribución</button> "
                    groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreMes",
                    title: "MES",
                    width: "30px"
                },
                {
                    field: "NombreDependencia",
                    title: "OFICINA",
                    width: "230px"
                },
                {
                    field: "Trabajador",
                    title: "CODIGO SISPER",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreCompleto",
                    title: "EMPLEADO",
                    width: "200px"
                },
                {
                    field: "NroDocumento",
                    title: "DNI",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "CondicionLaboral",
                    title: "CONDICIÓN LABORAL",
                    //attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombrePlanilla",
                    title: "PLANILLA",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreTipoPlanilla",
                    title: "TIPO PLANILLA",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "Ingresos",
                    title: "INGRESOS",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    field: "Descuentos",
                    title: "DESCUENTOS",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    field: "Aportes",
                    title: "APORTES",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Boletas/DescargarBoleta?anio=' + item.Anio + '&mes=' + item.Mes + '&trabajador=' + item.Trabajador + '&planilla=' + item.IdPlanilla + '&tipoplanilla=' + item.TipoPlanilla + '" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file"></span></a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

        debugger;
    };
    this.BoletasJS.prototype.inicializarGridSispla = function () {
        this.$dataSourceS = [];
        this.$dataSourceS = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSispla',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Anio = $("#ddlAnio_busquedaSispla").data("kendoDropDownList").value();
                        data_param.Mes = $("#ddlMes_busquedaSispla").data("kendoDropDownList").value();
                        //data_param.IdDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        //data_param.IdEmpleado = $("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                $("#lblTotalSisper").html(this.total());
            },
            schema: {
                total: function (response) {
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "planilla"
                }
            },
            group: {
                field: "AnioMes", aggregates: [
                    { field: "AnioMes", aggregate: "count" },
                    { field: "Ingresos", aggregate: "sum" },
                    { field: "Descuentos", aggregate: "sum" },
                    { field: "Aportes", aggregate: "sum" }
                ]
            },
            aggregate: [
                { field: "AnioMes", aggregate: "count" },
                { field: "Ingresos", aggregate: "sum" },
                { field: "Descuentos", aggregate: "sum" },
                { field: "Aportes", aggregate: "sum" }
            ]
        });

        //$dataSource.fetch(function () {
        //    var total = this.total();
        //    $("#divTotal").append(total);
        //});

        this.$grid = $("#divGridSispla").kendoGrid({
            toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado de Boletas en Sistema de Planillas.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceS,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,

            dataType: 'json',
            //dataBound: function () {
            //    $('#totalBits').remove();
            //    //add the total count to the pager div.  or whatever div you want... just remember to clear it before you add it.
            //    $('.k-grid-footer').append('<div id="totalBits">' + this.dataSource.total() + '</div>')
            //},
            columns: [
                {
                    field: "AnioMes",
                    title: "AÑO",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        return item.Anio;
                    },
                    aggregates: ["count"],
                    footerTemplate: "TOTAL: ",
                    groupFooterTemplate: "Sub Total: ",
                    //groupHeaderTemplate: "#= value # <button id='btnNuevo' type='button' class='btn btn-info btn-sm' onclick='controlador.ingresarDetalleDistribucion(#= aggregates.IdDistribucion.max #)' style='float:right'><span class='glyphicon glyphicon-new-window' aria-hidden='true'></span> Detalle de la Distribución</button> "
                    groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreMes",
                    title: "MES",
                    width: "30px"
                },
                {
                    field: "NombreDependencia",
                    title: "OFICINA",
                    width: "230px"
                },
                //{
                //    field: "Trabajador",
                //    title: "CODIGO SISPER",
                //    attributes: { style: "text-align:center;" },
                //    width: "30px"
                //},
                {
                    field: "NombreCompleto",
                    title: "EMPLEADO",
                    width: "200px"
                },
                {
                    field: "NroDocumento",
                    title: "DNI",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "CondicionLaboral",
                    title: "CONDICIÓN LABORAL",
                    //attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombrePlanilla",
                    title: "PLANILLA",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NombreTipoPlanilla",
                    title: "TIPO PLANILLA",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "Ingresos",
                    title: "INGRESOS",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    field: "Descuentos",
                    title: "DESCUENTOS",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    field: "Aportes",
                    title: "APORTES",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    //template: function (item) {
                    //    return "S/.xxx.xx";
                    //},
                    width: "30px",
                    aggregates: ["sum"],
                    footerTemplate: "#= kendo.toString(sum, 'C') #",
                    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Boletas/DescargarBoletaSispla?anio=' + item.Anio + '&mes=' + item.Mes + '&trabajador=' + item.Trabajador + '&planilla=' + item.IdPlanilla + '&tipoplanilla=' + item.TipoPlanilla + '" class="btn btn-info btn-xs descarga"><span class="glyphicon glyphicon-file"></span></a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

        debugger;
    };

    this.BoletasJS.prototype.registrarSolicitud = function () {
        //frmRegistroValidador = $("#frmRegistro").kendoValidator().data("kendoValidator");

        if (frmRegistroValidador.validate()) {
            controladorApp.abrirMensajeDeConfirmacion('¿Está seguro del documento de identidad ingresado?', 'SI', 'NO'
                , function (arg) {
                    var data_param = new FormData();
                    data_param.append('dni', $("#txtPersonaNumeroDeDocumento").val());

                    $.ajax({
                        type: 'POST',
                        url: controladorApp.obtenerRutaBase() + "Boletas/RegistrarSolicitudDescargaBoleta",
                        contentType: false, //'application/json',
                        processData: false,
                        dataType: 'json',
                        data: data_param
                    }).done(function (data) {
                        if (data.success == "True") {
                            //window.location.href = "/Boletas/DescargarBoletaArchivo?file=" + data.fileName;
                            controladorApp.notificarMensajeDeAlerta("Por favor revise su correo electrónico para continuar con la consulta de las boletas de pago");
                            $("#txtPersonaNumeroDeDocumento").val('');
                        }
                        else {
                            controladorApp.notificarMensajeDeAlerta(data.responseText);
                        }
                    });
                });
        }
        //$("#step1").removeAttr("disabled");
        //$("#step1").removeClass("btn-default").addClass("btn-info");
        //$("#liTab2").hide();
        //$("#liTab3").hide();
    }
    this.BoletasJS.prototype.validarSolicitud = function () {
        if (frmValidacionValidador.validate()) {
            controladorApp.abrirMensajeDeConfirmacion('¿Está seguro de la contraseña ingresada?', 'SI', 'NO'
                , function (arg) {
                    var data_param = new FormData();
                    data_param.append('id', $("#hdIdSolicitud").val());
                    data_param.append('clave', $("#txtPersonaClave").val());

                    $.ajax({
                        type: 'POST',
                        url: controladorApp.obtenerRutaBase() + "Boletas/ValidarSolicitudDescargaBoleta",
                        contentType: false, //'application/json',
                        processData: false,
                        dataType: 'json',
                        data: data_param
                    }).done(function (data) {
                        if (data.success == "True") {
                            //window.location.href = "/Boletas/DescargarBoletaArchivo?file=" + data.fileName;
                            //alert("Por favor revise su correo electronico para continuar con la consulta de las boletas de pago")

                            debugger;
                            //alert(data.responseText)
                            if (data.responseText.split('|')[0] == 1) {
                                $("#step2").prop("disabled", true);
                                $("#step2").removeClass("btn-info").addClass("btn-default");
                                $("#step3").removeAttr("disabled");
                                $("#step3").removeClass("btn-default").addClass("btn-info");
                                //$("#liTab2").hide();
                                $("#liTab3").show();

                                $('#tab2').removeClass('in active');
                                $('#tab1').removeClass('in active');
                                $('#tab3').addClass('in active');
                                $('#liTab2').removeClass('active');
                                $('#liTab1').removeClass('active');
                                $('#liTab3').addClass('active');

                                this.$dataSource = [];
                                this.$dataSource = new kendo.data.DataSource({
                                    serverPaging: true,
                                    serverSorting: true,
                                    batch: false,
                                    transport: {
                                        read: {
                                            url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosBoletaAnonimo',
                                            type: 'GET',
                                            dataType: 'json',
                                            cache: false
                                        },
                                        parameterMap: function ($options, $operation) {
                                            var data_param = {};

                                            if ($operation === "read") {
                                                data_param.Anio = "";
                                                data_param.Mes = "";
                                                data_param.IdDependencia = "";
                                                data_param.NroDocumento = data.responseText.split('|')[1];
                                                data_param.Nombre = "";
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
                                            //var TotalDeRegistros = 0;
                                            //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                                            return response.length; // TotalDeRegistros;
                                        },
                                        model: {
                                            id: "IdEmpleado"
                                        }
                                    },
                                    //group: {
                                    //    field: "AnioMes", aggregates: [
                                    //        { field: "AnioMes", aggregate: "count" }
                                    //        //{ field: "MontoMaximo", aggregate: "sum" },
                                    //        //{ field: "MontoNoAlcanzado", aggregate: "sum" },
                                    //        //{ field: "MontoAlcanzado", aggregate: "sum" }
                                    //    ]
                                    //},
                                    //aggregate: [
                                    //    { field: "AnioMes", aggregate: "count" }
                                    //    //{ field: "MontoMaximo", aggregate: "sum" },
                                    //    //{ field: "MontoNoAlcanzado", aggregate: "sum" },
                                    //    //{ field: "MontoAlcanzado", aggregate: "sum" }
                                    //],
                                    sort: [
                                        { field: "Anio", dir: "asc" },
                                        { field: "Mes", dir: "asc" }
                                    ]
                                });

                                this.$grid = $("#divGridDescarga").kendoGrid({
                                    //toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
                                    //excel: {
                                    //    fileName: "Listado de Boletas Laborales.xlsx",
                                    //    filterable: false
                                    //},
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
                                            field: "Anio",
                                            title: "AÑO",
                                            attributes: { style: "text-align:center;" },
                                            width: "30px"//,
                                            //template: function (item) {
                                            //    return item.Anio;
                                            //}//,
                                            //aggregates: ["count"],
                                            //groupHeaderTemplate: "#= value # (Total: #= count#)"
                                        },
                                        {
                                            field: "NombreMes",
                                            title: "MES",
                                            width: "30px"
                                        },
                                        {
                                            field: "NombreOficina",
                                            title: "OFICINA",
                                            width: "300px"
                                        },
                                        {
                                            field: "NroDocumento",
                                            title: "NRO DOC",
                                            attributes: { style: "text-align:center;" },
                                            width: "30px"
                                        },
                                        {
                                            field: "NombreCompleto",
                                            title: "EMPLEADO",
                                            width: "200px"
                                        },
                                        {
                                            field: "EstadoNombre",
                                            title: "ESTADO EMPLEADO",
                                            width: "30px"
                                        },
                                        {
                                            field: "CorreoElectronicoLaboral",
                                            title: "EMAIL",
                                            attributes: { style: "text-align:center;" },
                                            width: "50px"
                                        },
                                        {
                                            field: "EstadoEnvioNombre",
                                            title: "ESTADO ENVÍO",
                                            width: "30px",
                                            aggregates: ["count"],
                                            groupHeaderTemplate: "#= value # (Total: #= count#)"
                                        },
                                        {
                                            field: "FechaEnvio",
                                            title: "FECHA ENVÍO",
                                            attributes: { style: "text-align:center;" },
                                            width: "50px"
                                        },
                                        //{
                                        //    field: "FechaRecepcion",
                                        //    title: "FECHA RECEPCIÓN",
                                        //    attributes: { style: "text-align:center;" },
                                        //    width: "50px"
                                        //},
                                        //{
                                        //    //ENVIO DE BOLETA AL MAIL PERSONAL
                                        //    title: '',
                                        //    attributes: { style: "text-align:center;" },
                                        //    template: function (item) {
                                        //        var controles = "";
                                        //        if (item.Contrasena != '') {
                                        //            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalEnvioMail(\'' + item.Contrasena + '\')">';
                                        //            controles += '<span class="glyphicon glyphicon-envelope" aria-hidden="true" data-uib-tooltip="Editar"></span>';
                                        //            controles += '</button>';
                                        //        }

                                        //        return controles;
                                        //    },
                                        //    width: '30px'
                                        //},
                                        {
                                            //INGRESAR DETALLE DE LA EVALUACION
                                            title: '',
                                            attributes: { style: "text-align:center;" },
                                            template: function (item) {
                                                var controles = "";
                                                if (item.Contrasena != '')
                                                    controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Boletas/DescargarArchivo/?id=' + item.Contrasena + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file"></span></a>';

                                                return controles;
                                            },
                                            width: '30px'
                                        }
                                    ]
                                }).data();
                            }
                            else if (data.responseText == 10){
                                controladorApp.notificarMensajeDeAlerta("La contraseña ingresada ya ha sido utilizada, por favor registre una nueva solicitud");
                                $("#txtPersonaClave").val('');
                            }
                            else 
                                controladorApp.notificarMensajeDeAlerta("La contraseña ingresada no es válida");
                        }
                        else {
                            controladorApp.notificarMensajeDeAlerta(data.responseText);
                        }
                    });
                });
        }
        //$("#step1").removeAttr("disabled");
        //$("#step1").removeClass("btn-default").addClass("btn-info");
        //$("#liTab2").hide();
        //$("#liTab3").hide();
    }

    this.BoletasJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
    };

    this.BoletasJS.prototype.buscarSisper = function (e) {
        e.preventDefault();

        var grilla = $('#divGridSisper').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
    };

    this.BoletasJS.prototype.buscarSispla = function (e) {
        e.preventDefault();

        var grilla = $('#divGridSispla').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
    };

    this.BoletasJS.prototype.abrirModalNuevo = function () {
        LimpiarModalNuevo();

        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.title("Envío de Boletas Laborales");

        $('#ddlAnio').data("kendoDropDownList").enable(true);
        $('#ddlMes').data("kendoDropDownList").enable(true);

        modal.open();
    }

    this.BoletasJS.prototype.abrirModalEnvioMail = function (contrasena) {
        $('#txtEnvioEmail').val('');
        $('#hdContrasenaEmail').val(contrasena);
        
        var modal = $('#divModalEnvioMail').data('kendoWindow');
        modal.title("Envío de Boletas de Pago");

        modal.open();
    }

    this.BoletasJS.prototype.abrirModalValidacion = function () {
        LimpiarModalValidar();

        var anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
        var mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();

        if (anio != '' && mes != '') {
            var modal = $('#divModalValidar').data('kendoWindow');
            modal.title("Validación de Boletas de Pago" + " - " + $("#ddlAnio_busquedaSisper").data("kendoDropDownList").text() + " " + $("#ddlMes_busquedaSisper").data("kendoDropDownList").text());

            var data_par = {};
            data_par.Anio = anio;
            data_par.Mes = mes;
            
            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Boletas/TotalBoletas',
                async: false,
                type: 'GET',
                dataType: 'json',
                data: data_par,
                success: function (result) {
                    if (result != null) {
                        $("#hdBoletas").val(result.Boletas);
                        $("#hdBoletasValidas").val(result.BoletasValidas);

                        //$("#lblBoletas").html("TOTAL DE BOLETAS: " + result.Boletas);
                        //$("#lblBoletasValidas").html("BOLETAS VALIDADAS: " + result.Boletas);

                        $("#divBoletas").html(result.Boletas);
                        $("#divBoletasValidas").html(result.BoletasValidas);

                        $("#progressBar").kendoProgressBar({
                            min: 0,
                            max: result.Boletas,
                            type: "percent",
                            change: onChange,
                            complete: onComplete
                        });

                        $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);
                    }
                }
            });



            //MOSTRAR TIPOS DE PLANILLAS PARA ENVIAR A FIRMA 
            this.$dataSourceS = [];
            this.$dataSourceS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Boletas/ListarPlantillasValidacion',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.Anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
                            data_param.Mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
                            //data_param.IdDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                            //data_param.IdEmpleado = $("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                    $("#lblTotalSisper").html(this.total());
                },
                schema: {
                    total: function (response) {
                        //var TotalDeRegistros = 0;
                        //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                        return response.length; // TotalDeRegistros;
                    },
                    //model: {
                    //    id: "planilla"
                    //}
                },
                sort:
                    { field: "NombrePlanilla", dir: "asc" }
            });

            this.$grid = $("#divGridPlanillas").kendoGrid({
                toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
                excel: {
                    fileName: "Listado de Planillas.xlsx",
                    filterable: false
                },
                dataSource: this.$dataSourceS,
                autoBind: true,
                selectable: false,
                scrollable: false,
                sortable: false,
                pageable: false,
                groupable: false,

                dataType: 'json',
                columns: [
                    {
                        selectable: true,
                        width: "30px",
                        attributes: { style: "text-align:center;" },
                    },
                    {
                        field: "AnioMes",
                        title: "AÑO",
                        attributes: { style: "text-align:center;" },
                        width: "30px",
                        template: function (item) {
                            return item.Anio;
                        },
                        //aggregates: ["count"],
                        //footerTemplate: "TOTAL: ",
                        //groupFooterTemplate: "Sub Total: ",
                        //groupHeaderTemplate: "#= value # <button id='btnNuevo' type='button' class='btn btn-info btn-sm' onclick='controlador.ingresarDetalleDistribucion(#= aggregates.IdDistribucion.max #)' style='float:right'><span class='glyphicon glyphicon-new-window' aria-hidden='true'></span> Detalle de la Distribución</button> "
                        //groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "NombreMes",
                        title: "MES",
                        width: "30px"
                    },
                    {
                        field: "IdPlanilla",
                        title: "ID PLANILLA",
                        attributes: { style: "text-align:center;" },
                        width: "50px",
                        hidden: true,
                    },
                    {
                        field: "NombrePlanilla",
                        title: "PLANILLA",
                        width: "100px"
                    },
                    {
                        field: "TipoPlanilla",
                        title: "TIPO PLANILLA",
                        attributes: { style: "text-align:center;" },
                        width: "50px",
                        hidden: true,
                    },
                    {
                        field: "NombreTipoPlanilla",
                        title: "TIPO PLANILLA",
                        width: "100px"
                    },
                    {
                        field: "DiasLaborados",
                        title: "TOTAL DE BOLETAS",
                        attributes: { style: "text-align:right;" },
                        width: "100px"
                    },
                    {
                        field: "Ingresos",
                        title: "TOTAL DE INGRESOS",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}",
                        width: "100px"
                    },
                    {
                        field: "Descuentos",
                        title: "TOTAL DE DESCUENTOS",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}",
                        width: "100px"
                    },
                    {
                        field: "Aportes",
                        title: "TOTAL DE APORTES",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}",
                        width: "100px"
                    }
                ]
            }).data();





            modal.open();
        }
        else {
            controladorApp.notificarMensajeDeAlerta("Debe seleccionar al periodo laboral (año y mes) que desea validar");
            return;
        }
        
    }

    //KMM METODO DE PRUEBA PARA ENVIAR NOTIFICAICIONES DE LA OGRH
    this.BoletasJS.prototype.enviarNotificacion = function () {
        controladorApp.abrirMensajeDeConfirmacion('¿Está seguro de enviar las notificaciones a los colaboradores del MIDIS?', 'SI', 'NO'
            , function (arg) {

                debugger;
                $.ajax({
                    type: 'POST',
                    url: controladorApp.obtenerRutaBase() + "Boletas/EnviarNotificacion",
                    contentType: 'application/json',
                    //processData: false,
                    dataType: 'json'//,
                    //data: JSON.stringify({
                    //    contrasena: $("#hdContrasenaEmail").val(),
                    //    email: $("#txtEnvioEmail").val()
                    //})
                }).done(function (data) {
                    controladorApp.notificarMensajeDeAlerta("Se envió la notificación al correo electrónico ingresado");
                });
            });
    }

    this.BoletasJS.prototype.cerrarModalNuevo = function () {
        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.close();
    }

    this.BoletasJS.prototype.cerrarModalValidar = function () {
        var modal = $('#divModalValidar').data('kendoWindow');
        modal.close();
    }

    this.BoletasJS.prototype.cerrarModalEnviarEmail = function () {
        $('#hdContrasenaEmail').val('');
        $('#hdAnioEmail').val('');
        $('#hdMesEmail').val('');
        var modal = $('#divModalEnvioMail').data('kendoWindow');
        modal.close();
    }

    //this.BoletasJS.prototype.exportarBoletas = function (e) {
    //    e.preventDefault();
        
    //    controladorApp.abrirMensajeDeConfirmacion('¿Desea exportar las boletas para los filtros seleccionados?', 'SI', 'NO'
    //        , function (arg) {

    //            window.location.href = "/Boletas/ExportarBoleta?anio=" + $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value() + "&mes=" + $("#ddlMes_busquedaSisper").data("kendoDropDownList").value(); // + trabajadorOld;
    //            //$(".descarga").each(function () {
    //            //    console.log(this);
    //            //    this.click();
    //            //});
                

    //            //var data_param = new FormData();
    //            //data_param.append('CodigoSisper', '000826');
    //            //data_param.append('Anio', '2019');
    //            //data_param.append('Mes', '8');

    //            //$.ajax({
    //            //    url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSisper?CodigoSisper=000826&Anio=2019&Mes=8',
    //            //    type: 'GET',
    //            //    dataType: 'json',
    //            //    contentType: false,
    //            //    processData: false,
    //            //    data: data_param,
    //            //    success: function (res) {
    //            //        if (res == null) {
    //            //            controladorApp.notificarMensajeDeAlerta("La exportación culminó con algunas observaciones");
    //            //        }
    //            //        else {
    //            //            debugger;
    //            //            var trabajadorOld = '';
    //            //            $.each(res, function (index, value) {
    //            //                //if (trabajadorOld != value.Trabajador) trabajadorOld = value.Trabajador;
    //            //                //generamos el archivo PDF
    //            //                window.location.href = "/Boletas/ExportarBoleta?anio=" + value.Anio + "&mes=" + value.Mes + "&trabajador=123"; // + trabajadorOld;
    //            //                //$.ajax({
    //            //                //    url: controladorApp.obtenerRutaBase() + 'Boletas/ExportarBoleta?anio=' + value.Anio + '&mes='+value.Mes+'&trabajador=' + trabajadorOld,
    //            //                //    type: 'GET',
    //            //                //    //dataType: 'json',
    //            //                //    //contentType: false,
    //            //                //    //processData: false,
    //            //                //    //data: data_param,
    //            //                //    success: function (res) {
    //            //                //        window.location.href = "/Boletas/DescargarArchivo?id=" + 1;
    //            //                //        //if (res == null) {
    //            //                //        //    controladorApp.notificarMensajeDeAlerta("La exportación culminó con algunas observaciones");
    //            //                //        //}
    //            //                //        //else {
    //            //                //        //    debugger;
    //            //                //        //    var trabajadorOld = '';
    //            //                //        //    $.each(res, function (index, value) {
    //            //                //        //        if (trabajadorOld != value.Trabajador) trabajadorOld = value.Trabajador;
    //            //                //        //        //generamos el archivo PDF

    //            //                //        //    });
    //            //                //        //}
    //            //                //    },
    //            //                //    error: function (res) {
    //            //                //        //alert(res);
    //            //                //    }
    //            //                //});
    //            //            });

    //            //            //window.location.href = "/Boletas/ExportarBoleta?anio=2019&mes=8&trabajador=12345678";
    //            //        }
    //            //    },
    //            //    error: function (res) {
    //            //        //alert(res);
    //            //    }
    //            //});
    //        });

    //    //if (frmNuevoValidador.validate()) {
    //    //    var modal = $('#divModalNuevo').data('kendoWindow');
    //    //    var esValido = true;
    //    //    var mensajeValidacion = '';

    //    //    var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
    //    //    var data_param = new FormData();
    //    //    if (dr != null) {
    //    //        data_param.append('IdEmpleado', dr.IdEmpleado);
    //    //        metodo = 'Guardar';
    //    //    }

    //    //    data_param.append('Anio', $('#ddlAnio').data("kendoDropDownList").value());
    //    //    data_param.append('Mes', $("#ddlMes").data("kendoDropDownList").value());
    //    //    //data_param.append('Estado', 1);

    //    //    debugger;
    //    //    var upload1 = $("#fileFirma").getKendoUpload();
    //    //    var firmas = upload1.getFiles();
    //    //    if (firmas.length == 0) {
    //    //        controladorApp.notificarMensajeDeAlerta('Debe seleccionar la(s) boleta(s) firmada(s)');
    //    //        return false;
    //    //    } else {
    //    //        for (var j = 0; j < firmas.length; j++) {
    //    //            if (firmas[0].extension.toLowerCase() == '.pdf')
    //    //                data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
    //    //            //else {
    //    //            //    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivos con extensión PDF');
    //    //            //    return false;
    //    //            //}
    //    //        }
    //    //    }

    //    //    $("#console").empty();
            

    //    //}
    //}

    this.BoletasJS.prototype.exportarBoletas = function (e) {
        //e.preventDefault();

        controladorApp.abrirMensajeDeConfirmacion('¿Desea exportar las boletas para los filtros seleccionados?', 'SI', 'NO'
            , function (arg) {
                var data_param = new FormData();
                data_param.append('anio', $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value());
                data_param.append('mes', $("#ddlMes_busquedaSisper").data("kendoDropDownList").value());

                $.ajax({
                    type: 'POST',
                    url: controladorApp.obtenerRutaBase() + "Boletas/ExportarBoleta",
                    contentType: false, //'application/json',
                    processData: false,
                    dataType: 'json',
                    data: data_param
                }).done(function (data) {
                    if (data.fileName != "") {
                        window.location.href = "/Boletas/DescargarBoletaArchivo?file=" + data.fileName;
                    }
                    else {
                        alert(data.responseText);
                    }
                });
            });
    }
    this.BoletasJS.prototype.eliminarTemporal = function (e) {
        //e.preventDefault();

        controladorApp.abrirMensajeDeConfirmacion('¿Desea eliminar los archivos temporales?', 'SI', 'NO'
            , function (arg) {
                var data_param = new FormData();
                
                $.ajax({
                    type: 'POST',
                    url: controladorApp.obtenerRutaBase() + "Boletas/EliminarTemporal",
                    contentType: false, //'application/json',
                    processData: false,
                    dataType: 'json',
                    data: data_param
                }).done(function (data) {
                    if (data.fileName == "temp") {
                        alert("Archivos eliminados");
                    }
                    else {
                        alert(data.responseText);
                    }
                });
            });
    }

    this.BoletasJS.prototype.validarBoletas = function (e) {
        //e.preventDefault();

        controladorApp.abrirMensajeDeConfirmacion('¿Desea validar las boletas de pago para el periodo seleccionado?', 'SI', 'NO'
            , function (arg) {
                var data_param = new FormData();
                data_param.append('anio', $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value());
                data_param.append('mes', $("#ddlMes_busquedaSisper").data("kendoDropDownList").value());

                initInvoker('W');

                //$.ajax({
                //    type: 'POST',
                //    url: controladorApp.obtenerRutaBase() + "Boletas/ExportarBoleta",
                //    contentType: false, //'application/json',
                //    processData: false,
                //    dataType: 'json',
                //    data: data_param
                //}).done(function (data) {
                //    if (data.fileName != "") {
                //        window.location.href = "/Boletas/DescargarBoletaArchivo?file=" + data.fileName;
                //    }
                //    else {
                //        alert(data.responseText);
                //    }
                //});
            });
    }

    this.BoletasJS.prototype.enviarEmail = function (e) {
        if ($('#txtEnvioEmail').val() == '') {
            controladorApp.notificarMensajeDeAlerta('Debe ingresar la cuenta de correo a la que se enviará la boleta de pago');
            return;
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Está seguro de enviar la boleta de pago a la cuenta de correo ingresada?', 'SI', 'NO'
            , function (arg) {
                
                debugger;
                $.ajax({
                    type: 'POST',
                    url: controladorApp.obtenerRutaBase() + "Boletas/EnviarEmailBoleta",
                    contentType: 'application/json',
                    //processData: false,
                    dataType: 'json',
                    data: JSON.stringify( {
                        contrasena: $("#hdContrasenaEmail").val(),
                        email: $("#txtEnvioEmail").val()
                    })
                }).done(function (data) {
                    controladorApp.notificarMensajeDeAlerta("Se envió la boleta de pago al correo electrónico ingresado");
                });
            });
    }
    this.BoletasJS.prototype.registrar = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';

        if (frmNuevoValidador.validate()) {            
            var modal = $('#divModalNuevo').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if (dr != null) {
                data_param.append('IdEmpleado', dr.IdEmpleado);
                metodo = 'Guardar';
            }

            data_param.append('Anio', $('#ddlAnio').data("kendoDropDownList").value());
            data_param.append('Mes', $("#ddlMes").data("kendoDropDownList").value());
            //data_param.append('Estado', 1);

            debugger;
            var upload1 = $("#fileFirma").getKendoUpload();
            var firmas = upload1.getFiles();
            if (firmas.length == 0) {
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar la(s) boleta(s) firmada(s)');
                return false;
            } else {
                for (var j = 0; j < firmas.length; j++){
                    if (firmas[0].extension.toLowerCase() == '.pdf')
                        data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                    //else {
                    //    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivos con extensión PDF');
                    //    return false;
                    //}
                }
            }

            debugger;
            controladorApp.abrirMensajeDeConfirmacion('¿Desea importar las boletas seleccionadas?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Boletas/' + metodo,
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            debugger;
                            if (res.success == 'False') {
                                $("#console").append(res.responseText);
                                controladorApp.notificarMensajeDeAlerta("La carga culminó con algunas observaciones");

                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio("Boletas cargadas correctamente");
                                modal.close();
                                $('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);

        }
    }

    this.BoletasJS.prototype.cerrarModalEdicion = function () {
        $('#divModalNuevo').data('kendoWindow').close();
    }

    this.BoletasJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.BoletasJS.prototype.abrirModalEliminacion = function (uid) {
        $('#hdnUid').val(uid);
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        modal.title("Confirmar eliminación");

        var data_param = new FormData();
        data_param.append('IdCad', dr.IdCad);
        data_param.append('IdPropuesta', dr.IdPropuesta);
        
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Propuesta/ConfirmarEliminacion',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                if (res.permite)
                    $('#hEliminacion').html('¿Está seguro de eliminar la propuesta seleccionada?')
                else
                    $('#hEliminacion').html('La propuesta seleccionada no puede ser eliminada')

                $("#pMensaje").html(res.listaMensaje[0]);
                $("#btnEliminar").prop('disabled', !res.permite);
                modal.open();
            },
            error: function (res) {

            }
        });
    }

    this.BoletasJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.BoletasJS.prototype.eliminar = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var grilla = $('#divGrid').data("kendoGrid");
        var dr = grilla.dataSource.getByUid($('#hdnUid').val());

        var data_param = new FormData();
        data_param.append('IdCad', dr.IdCad);
        data_param.append('IdPropuesta', dr.IdPropuesta);
        
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Propuesta/Eliminar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                controladorApp.notificarMensajeSatisfactorio("Registro eliminado correctamente");
                grilla.dataSource.page(1);
                modal.close();
            },
            error: function (res) {

            }
        });
    }

}(jQuery));