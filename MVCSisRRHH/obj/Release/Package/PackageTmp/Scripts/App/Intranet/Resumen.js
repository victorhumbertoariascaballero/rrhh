(function ($) {
    var frmNuevoValidador;
    var strMensajes = '';
        
    this.ResumenJS = function () { };
    this.ResumenJS.prototype.inicializar = function () {

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
        
        debugger;
        var fecha = new Date();
        var anio = fecha.getFullYear();
        var mes = fecha.getMonth() + 1;
        mes = (mes.toString().length == 1 ? "0" : "") + mes.toString();
        $("#ddlAnio_busqueda").data("kendoDropDownList").value(anio);
        $("#ddlMes_busqueda").data("kendoDropDownList").value(mes);
        
        this.inicializarGrid();
    };


    function onChange(e) {
        //kendoConsole.log("Change event :: value is " + e.value);
    }

    function onComplete(e) {
        //kendoConsole.log("Complete event :: value is " + e.value);
        $("#startProgress").text("Restart Progress").removeClass("k-state-disabled");
    }

    function progress() {
        var pb = $("#progressBar").data("kendoProgressBar");
        pb.value($("#divBoletasValidas").val());
        pb.enable();

        //kendo.ui.progress($("#progressBar"), true);
        //$.ajaxSetup({ async: false });

        //RECORREMOS LOS 813 EMPLEADOS DE PRUEBA
        var data_param = {};
        data_param.Anio = $("#ddlAnio_busquedaSisper").data("kendoDropDownList").value();
        data_param.Mes = $("#ddlMes_busquedaSisper").data("kendoDropDownList").value();
        
        $(document).off('ajaxSend');
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Boletas/ListarEmpleadosSisper',
            timeout: 3000000, //3 second timeout
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
                            async: false,
                            type: 'GET',
                            dataType: 'json',
                            data: data_par,
                            success: function (result) {
                                //alert(result);
                                if (result != null) {
                                    debugger;
                                    if (result.success == 'True') {
                                        if (result.operacion == 1) {
                                            $("#hdBoletasValidas").val(parseInt($("#hdBoletasValidas").val()) + 1);
                                            $("#divBoletasValidas").html($("#hdBoletasValidas").val());

                                            pb.value(pb.value() + 1);
                                            $('#bar').css('width', pb.value() + '%');

                                            //var interval = setInterval(function () {
                                            //    if (pb.value() < $("#hdBoletasValidas").val()) {
                                            //        pb.value(pb.value() + 1);
                                            //        $('#bar').css('width', pb.value() + '%');
                                            //    } else {
                                            //        clearInterval(interval);
                                            //    }
                                            //}, 100);
                                        }
                                    }
                                    //pb.value(pb.value() + 1);

                                    
                                }
                            }
                        });
                    });

                    if ($("#startProgress").hasClass("k-state-disabled")) {
                        $("#startProgress").removeClass("k-state-disabled");
                        
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

                                    $("#progressBar").kendoProgressBar({
                                        min: 0,
                                        max: result.Boletas,
                                        type: "percent",
                                        change: onChange,
                                        complete: onComplete
                                    });

                                    $("#progressBar").data("kendoProgressBar").value(result.BoletasValidas);
                                    //pb.max = result.Boletas;
                                }
                            }
                        });
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

    function onComplete(e) {
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


    this.ResumenJS.prototype.inicializarGrid = function () {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Boletas/ListarResumenBoletas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Anio = $("#ddlAnio_busqueda").data("kendoDropDownList").value();
                        data_param.Mes = $("#ddlMes_busqueda").data("kendoDropDownList").value();
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
                //$("#lblTotal").html(this.total());
            },
            schema: {
                total: function (response) {
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "AnioMes"
                }
            }//,
            //group: {
            //    field: "AnioMes", aggregates: [
            //       { field: "AnioMes", aggregate: "count" }
            //       //{ field: "MontoMaximo", aggregate: "sum" },
            //       //{ field: "MontoNoAlcanzado", aggregate: "sum" },
            //       //{ field: "MontoAlcanzado", aggregate: "sum" }
            //    ]
            //}
        });

        
        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel"], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Resumen de Boletas Laborales.xlsx",
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
                    field: "Anio",
                    title: "AÑO",
                    attributes: { style: "text-align:center;" },
                    width: "50px"
                },
                {
                    field: "NombreMes",
                    title: "MES",
                    width: "50px"
                },
                {
                    field: "TotalSisper",
                    title: "TOTAL DE BOLETAS EN SISPER",
                    attributes: { style: "text-align:right;" },
                    width: "100px"
                },
                {
                    field: "TotalValidado",
                    title: "TOTAL DE BOLETAS VALIDADAS",
                    attributes: { style: "text-align:right;" },
                    width: "100px"
                },
                {
                    field: "TotalEnviado",
                    title: "TOTAL DE BOLETAS ENVIADAS",
                    attributes: { style: "text-align:right;" },
                    width: "100px"
                },
                {
                    field: "Glosa",
                    title: "GLOSA DE BOLETA DE PAGO",
                    width: "200px"
                },
                {
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.TotalEnviado == 0 && item.TotalValidado > 0) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.firmarPeriodoBoletas(' + item.Anio + ',' + item.Mes + ')">';
                            controles += '<span class="glyphicon glyphicon-check" aria-hidden="true" data-uib-tooltip="Editar" title="Iniciar proceso de firma para este periodo"></span>';
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
    
    this.ResumenJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
    };

    this.ResumenJS.prototype.firmarPeriodoBoletas = function (anio, mes) {
        controladorApp.abrirMensajeDeConfirmacion('¿Está seguro de realizar la firma de boletas de pago para el periodo seleccionado?', 'SI', 'NO'
            , function (arg) {
                var data_param = new FormData();
                data_param.append('Anio', anio);
                data_param.append('Mes', mes);

                $.ajax({
                    type: 'POST',
                    url: controladorApp.obtenerRutaBase() + "Boletas/CrearZipFirma",
                    async: false,
                    contentType: false, //'application/json',
                    processData: false,
                    dataType: 'json',
                    data: data_param,
                    success: function (data) {
                        debugger;
                        if (data.success == "True") {
                            document.getElementById('hdArchivoFirmado').value = data.responseText;
                            initInvoker('W');
                        }
                    },
                    error: function (data) { }
                });
            });
    }

    this.ResumenJS.prototype.abrirModalNuevo = function () {
        LimpiarModalNuevo();

        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.title("Envío de Boletas Laborales");

        $('#ddlAnio').data("kendoDropDownList").enable(true);
        $('#ddlMes').data("kendoDropDownList").enable(true);

        modal.open();
    }

    this.ResumenJS.prototype.cerrarModalNuevo = function () {
        var modal = $('#divModalNuevo').data('kendoWindow');
        modal.close();
    }

    this.ResumenJS.prototype.exportarBoletas = function (e) {
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

    this.ResumenJS.prototype.validarBoletas = function (e) {
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

    this.ResumenJS.prototype.registrar = function (e) {
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

    this.ResumenJS.prototype.cerrarModalEdicion = function () {
        $('#divModalNuevo').data('kendoWindow').close();
    }

    this.ResumenJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.ResumenJS.prototype.abrirModalEliminacion = function (uid) {
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

    this.ResumenJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.ResumenJS.prototype.eliminar = function () {
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