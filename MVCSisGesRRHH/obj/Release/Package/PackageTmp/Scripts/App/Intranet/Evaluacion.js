(function ($) {
    var frmPersonaValidador;
    var frmEvaluacionCurriValidador;
    var strMensajes = '';
    var PERFIL_NOMINA_ABASTECIMIENTO = '45';
    var PERFIL_NOMINA_CONTACTO = '46';
    var PERFIL_NOMINA_CONTABILIDAD = '47';
    var data = [
                    { Nombre: "PERIODO LIMITADO", Codigo: "1" },
                    { Nombre: "AL TERMINO DE LA DESIGNACIÓN", Codigo: "2" }
               ];
    var dataTipoContrato = [
                    { Nombre: "FORMATO ESTANDAR", Codigo: "1" }
    ];


    this.EvaluacionJS = function () { };
    this.EvaluacionJS.prototype.inicializarCurricular = function () {
        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });
        $('#divModalVisor').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Ver documento',
            visible: false,
            position: { top: '1%', left: "1%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Evaluación Curricular',
            visible: false,
            position: { top: '20%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {

            }
        }).data("kendoWindow");
        /* CONSULTA */
        //$('#divModalRegistroConvocatoria').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '98%',
        //    height: 'auto',
        //    title: 'Agregar Convocatoria',
        //    visible: false,
        //    position: { top: '1%', left: "1%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        frmPersonaValidador.hideMessages();

        //        $("#hdIdConvocatoria").val('0');
        //        $("#hdIdPerfil").val('0');
        //        $("#ddlBases").data("kendoDropDownList").value('');
        //        $("#ddlDependencia").data("kendoDropDownList").value('');
        //        $("#txtAIRHSPConvocatoria").val('');
        //        $("#txtMetaConvocatoria").val('');
        //        $("#ddlComiteDependencia1").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia2").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia3").data("kendoDropDownList").value('');

        //    }
        //}).data("kendoWindow");
        //frmPersonaValidador = $("#frmRegistroConvocatoria").kendoValidator().data("kendoValidator");

        //$('#divModalEvaluacionCurri').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '60%',
        //    height: 'auto',
        //    title: 'Notificación al Comité de Selección',
        //    visible: false,
        //    position: { top: '20%', left: "25%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        //frmPersonaValidador.hideMessages();

        //        //$("#hdIdPostulante").val('0');
        //        //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
        //        //$("#txtPersonaNumeroDeDocumento").val('');
        //        //$("#txtPersonaNombres").val('');
        //        //$("#txtPersonaApellidoPaterno").val('');
        //        //$("#txtPersonaApellidoMaterno").val('');
        //        //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
        //        //$("#txtPersonaDireccionDomicilio").val('');
        //    }
        //}).data("kendoWindow");
        //frmEvaluacionCurriValidador = $("#frmEvaluacionCurri").kendoValidator().data("kendoValidator");

        //$("#txtPersonaFechaNacimiento").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtPersonaFechaInicioLabores").kendoDatePicker({ format: "dd/MM/yyyy", min: new Date() });
        //$("#txtPersonaFechaFinLabores").kendoDatePicker({ format: "dd/MM/yyyy" }); // max: new Date()

        //$("#ddlEstado_busqueda").data("kendoDropDownList").value("1");
        
        this.inicializarGridCurricular();
    };
    this.EvaluacionJS.prototype.inicializarConocimientos = function () {
        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });
        $('#divModalVisor').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Ver documento',
            visible: false,
            position: { top: '1%', left: "1%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Evaluación Curricular',
            visible: false,
            position: { top: '20%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {

            }
        }).data("kendoWindow");
        /* CONSULTA */
        //$('#divModalRegistroConvocatoria').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '98%',
        //    height: 'auto',
        //    title: 'Agregar Convocatoria',
        //    visible: false,
        //    position: { top: '1%', left: "1%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        frmPersonaValidador.hideMessages();

        //        $("#hdIdConvocatoria").val('0');
        //        $("#hdIdPerfil").val('0');
        //        $("#ddlBases").data("kendoDropDownList").value('');
        //        $("#ddlDependencia").data("kendoDropDownList").value('');
        //        $("#txtAIRHSPConvocatoria").val('');
        //        $("#txtMetaConvocatoria").val('');
        //        $("#ddlComiteDependencia1").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia2").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia3").data("kendoDropDownList").value('');

        //    }
        //}).data("kendoWindow");
        //frmPersonaValidador = $("#frmRegistroConvocatoria").kendoValidator().data("kendoValidator");

        //$('#divModalEvaluacionCurri').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '60%',
        //    height: 'auto',
        //    title: 'Notificación al Comité de Selección',
        //    visible: false,
        //    position: { top: '20%', left: "25%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        //frmPersonaValidador.hideMessages();

        //        //$("#hdIdPostulante").val('0');
        //        //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
        //        //$("#txtPersonaNumeroDeDocumento").val('');
        //        //$("#txtPersonaNombres").val('');
        //        //$("#txtPersonaApellidoPaterno").val('');
        //        //$("#txtPersonaApellidoMaterno").val('');
        //        //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
        //        //$("#txtPersonaDireccionDomicilio").val('');
        //    }
        //}).data("kendoWindow");
        //frmEvaluacionCurriValidador = $("#frmEvaluacionCurri").kendoValidator().data("kendoValidator");

        //$("#txtPersonaFechaNacimiento").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtPersonaFechaInicioLabores").kendoDatePicker({ format: "dd/MM/yyyy", min: new Date() });
        //$("#txtPersonaFechaFinLabores").kendoDatePicker({ format: "dd/MM/yyyy" }); // max: new Date()

        //$("#ddlEstado_busqueda").data("kendoDropDownList").value("1");

        this.inicializarGridConocimientos();
    };
    this.EvaluacionJS.prototype.inicializarEntrevistas = function () {
        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });
        $('#divModalVisor').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Ver documento',
            visible: false,
            position: { top: '1%', left: "1%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Entrevista Personal',
            visible: false,
            position: { top: '20%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                $('#hdIdEvaluacion').val('');
            }
        }).data("kendoWindow");

        $('#divModalEntrevistaSE').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '80%',
            height: 'auto',
            title: 'Realizar Entrevista Personal',
            visible: false,
            position: { top: '10%', left: "10%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                $('#hdIdEvaluacion').val('');
                $('#hdIdExamen').val('');
            }
        }).data("kendoWindow");
        $('#divModalEntrevistaCE').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '80%',
            height: 'auto',
            title: 'Realizar Entrevista Personal',
            visible: false,
            position: { top: '10%', left: "10%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                $('#hdIdEvaluacion').val('');
                $('#hdIdExamen').val('');
            }
        }).data("kendoWindow");

        this.inicializarGridPreguntasMaestras();
        this.inicializarGridEntrevistas();
    };

    this.EvaluacionJS.prototype.inicializarEntrevistasPractica = function () {
        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });
        $('#divModalVisor').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Ver documento',
            visible: false,
            position: { top: '1%', left: "1%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Entrevista Personal',
            visible: false,
            position: { top: '20%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                $('#hdIdEvaluacion').val('');
            }
        }).data("kendoWindow");
        $('#divModalEntrevistaSE').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '80%',
            height: 'auto',
            title: 'Realizar Entrevista Personal',
            visible: false,
            position: { top: '10%', left: "10%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                $('#hdIdEvaluacion').val('');
                $('#hdIdExamen').val('');
            }
        }).data("kendoWindow");

        this.inicializarGridPreguntasMaestrasPractica();
        this.inicializarGridEntrevistasPractica();
    };

    this.EvaluacionJS.prototype.inicializarCurricularPractica = function () {
        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });
        $('#divModalVisor').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Ver documento',
            visible: false,
            position: { top: '1%', left: "1%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Evaluación Curricular',
            visible: false,
            position: { top: '20%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {

            }
        }).data("kendoWindow");
        /* CONSULTA */
        //$('#divModalRegistroConvocatoria').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '98%',
        //    height: 'auto',
        //    title: 'Agregar Convocatoria',
        //    visible: false,
        //    position: { top: '1%', left: "1%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        frmPersonaValidador.hideMessages();

        //        $("#hdIdConvocatoria").val('0');
        //        $("#hdIdPerfil").val('0');
        //        $("#ddlBases").data("kendoDropDownList").value('');
        //        $("#ddlDependencia").data("kendoDropDownList").value('');
        //        $("#txtAIRHSPConvocatoria").val('');
        //        $("#txtMetaConvocatoria").val('');
        //        $("#ddlComiteDependencia1").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia2").data("kendoDropDownList").value('');
        //        $("#ddlComiteDependencia3").data("kendoDropDownList").value('');

        //    }
        //}).data("kendoWindow");
        //frmPersonaValidador = $("#frmRegistroConvocatoria").kendoValidator().data("kendoValidator");

        //$('#divModalEvaluacionCurri').kendoWindow({
        //    draggable: true,
        //    modal: true,
        //    pinned: false,
        //    resizable: false,
        //    width: '60%',
        //    height: 'auto',
        //    title: 'Notificación al Comité de Selección',
        //    visible: false,
        //    position: { top: '20%', left: "25%" },
        //    //actions: ["Minimize", "Maximize", "Close"],
        //    actions: ["Close"],
        //    close: function () {
        //        //frmPersonaValidador.hideMessages();

        //        //$("#hdIdPostulante").val('0');
        //        //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
        //        //$("#txtPersonaNumeroDeDocumento").val('');
        //        //$("#txtPersonaNombres").val('');
        //        //$("#txtPersonaApellidoPaterno").val('');
        //        //$("#txtPersonaApellidoMaterno").val('');
        //        //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
        //        //$("#txtPersonaDireccionDomicilio").val('');
        //    }
        //}).data("kendoWindow");
        //frmEvaluacionCurriValidador = $("#frmEvaluacionCurri").kendoValidator().data("kendoValidator");

        //$("#txtPersonaFechaNacimiento").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtPersonaFechaInicioLabores").kendoDatePicker({ format: "dd/MM/yyyy", min: new Date() });
        //$("#txtPersonaFechaFinLabores").kendoDatePicker({ format: "dd/MM/yyyy" }); // max: new Date()

        //$("#ddlEstado_busqueda").data("kendoDropDownList").value("1");

        this.inicializarGridCurricularPractica();
    };

    function onSelect(e) {
        var files = e.files;
        for (var i = 0; i < files.length; i += 1) {
            var file = files[i];
            if (file.validationErrors && file.validationErrors.length > 0) {
                file.error = file.validationErrors[0];
            }
        }
    }
    function onUpload(e) {
        debugger;
        e.formData = new FormData();
        e.formData.append('IdContrato', $('#hdIdContrato').val());
        e.formData.append('NombreArchivo', e.files[0].name);
    }
    function onSuccess(e) {
        debugger;
        //if (e.response.success == 'False') {
        //    //console.log(e.response.responseText);
        //    strMensajes = '<div style="margin-top: 0px; background-color: rgb(255, 255, 255);">' + e.response.responseText + '</div>';
        //    alert(strMensajes); //$("#console").innerHTML + strMensajes;
        //    e.preventDefault();
        //    return false;
        //}
    }
    function onError(e) {
        //kendoConsole.log("Error (" + e.operation + ") :: " + getFileInfo(e));
    }
    function onComplete(e) {
        //$("#divConsole").show();
    }
    function onCancel(e) {
        //kendoConsole.log("Cancel :: " + getFileInfo(e));
    }
    function onRemove(e) {
        //kendoConsole.log("Remove :: " + getFileInfo(e));
    }
    function onProgress(e) {
        //console.log("Upload progress :: " + e.percentComplete + "% :: " + getFileInfo(e));
    }

    function LimpiarModalRegistroConvocatoria() {
        var today = new Date();
        var month = today.getMonth();
        if (today.getMonth() > 1) var month = month - 1;
        var year = today.getFullYear();
        
        $("#txtNroConvocatoria").val("");
        $("#txtCantidadConvocatoria").val("");
        $("#txtRemuneracionConvocatoria").val("");
        $("#txtCargoConvocatoria").val("");
        $("#txtAIRHSPConvocatoria").val("");
        $("#txtMetaConvocatoria").val("");

        $("#fileDocRequerimiento").data("kendoUpload").clearAllFiles();
        $("#fileDocCertificacion").data("kendoUpload").clearAllFiles();
        $("#fileDocComite").data("kendoUpload").clearAllFiles();

        $("#app").empty();
        //if ($("#divGridEntrevista").data('kendoGrid') != null)
        //    $("#divGridEntrevista").data('kendoGrid').dataSource.data([]);
    }

    this.EvaluacionJS.prototype.inicializarGridConocimientos = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoria").val(res.NroConvocatoria);
                $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
                $("#txtCargoConvocatoria").val(res.NombreCargo);
            },
            error: function (res) {
                debugger;
            }
        });

        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesEvaluacionConocimiento',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.IdConvocatoria = $("#hdIdConvocatoria").val();
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

                if (e.items.length > 0) {
                    if (e.action == "itemchange" && (e.field == "PuntajeTotal" ||
                        e.field == "FechaEntrevista" ||
                        e.field == "HoraEntrevista" ||
                        e.field == "Observacion" )) {
                        // ACTUALIZAMOS LOS VALORES ALCANZADOS

                        debugger;
                        //alert(e.items[0].IdEstudioPerfil)
                        var data_param = new FormData();
                        data_param.append('IdEvaluacion', e.items[0].IdEvaluacion);
                        data_param.append('PuntajeTotal', e.items[0].PuntajeTotal);
                        data_param.append('FechaEntrevista', kendo.toString(kendo.parseDate(e.items[0].FechaEntrevista), 'dd/MM/yyyy'));
                        data_param.append('HoraEntrevista', kendo.toString(kendo.parseDate(e.items[0].HoraEntrevista), 'HH:mm'));
                        data_param.append('Observacion', e.items[0].Observacion);
                        data_param.append('IdUsuarioModificacion', $("#hdIdTrabajador").val());

                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionConocimiento',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                //controladorApp.notificarMensajeSatisfactorio("Se actualizaron los montos correctamente");
                                //controlador.inicializarGrid();

                                $('#divGrid').data("kendoGrid").dataSource.read();
                                $('#divGrid').data("kendoGrid").refresh();
                            },
                            error: function (res) {
                                debugger;
                            }
                        });
                    }
                }
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdEvaluacion",
                    fields: {
                        IdEvaluacion: { editable: false, nullable: true },
                        PuntajeTotal: { validation: { required: true, type: "number", min: "0", max: "30" } },
                        //Observacion: { validation: { required: true } },
                        FechaEntrevista: {
                            validation: {
                                required: true,
                                type: "date",
                                format: "dd/MM/yyyy"//,
                                //fechaentrevistavalidation: function (input) {
                                //    if (input.is("[name='FechaEntrevista']") && input.val() == "") {
                                //        input.attr("data-fechaentrevistavalidation-msg", "requerido");
                                //        return false;
                                //    }

                                //    return true;
                                //}
                            }
                        },
                    }
                }
            }
        });

        this.$grid = $("#divGrid").kendoGrid({
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            dataType: 'json',
            //dataBound: function (e) {
            //    var columns = e.sender.columns;
            //    var dataItems = e.sender.dataSource.view();
            //    for (var j = 0; j < dataItems.length; j++) {
            //        var aptototal = dataItems[j].get("AptoTotal");

            //        var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
            //        //if (aptototal) 
            //        //    row.addClass("aptoTotal");
            //        //else 
            //        //    row.addClass("noaptoTotal");
            //    }

            //    //debugger;
            //    //alert(this.tbody.find("tr.k-master-row").first());
            //    //this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            //dataBound: function (e) {
            //    $('.k-grid-add').unbind("click");
            //    $('.k-grid-add').bind("click", function () {
            //        if ($("#hdIdEmpleado").val() == 0) {
            //            controladorApp.notificarMensajeDeAlerta('Primero debe completar el registro del empleado');
            //            return false;
            //        }
            //    });
            //},
            columns: [
                {
                    title: '',
                    width: "30px",
                    template: function (item) {
                        return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                    }
                },
                {
                    field: "Nombre",
                    title: "NOMBRE",
                    width: "200px",
                    editable: true,
                    template: function (item) {
                        return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                    }
                },
                {
                    field: "PuntajeTotal",
                    title: "PUNTAJE TOTAL",
                    attributes: { style: "text-align:center" },
                    width: "50px"
                    //editable: true
                },
                {
                    field: "AptoTotal",
                    title: "APTO / NO APTO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    },
                    editable: true
                },
                {
                    title: "ENTREVISTA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "FechaEntrevista",
                            title: "FECHA",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            editor: controlador.FechaEntrevistaEditor,
                            format: "{0: dd/MM/yyyy}",
                            width: "100px"
                            //template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "HoraEntrevista",
                            title: "HORA",
                            attributes: { style: "text-align:center" },
                            editor: controlador.HoraEntrevistaEditor,
                            format: "{0: HH:mm}",
                            width: "100px"
                            //editor: controlador.Bonifica3DropDownEditor,
                            //template: "#=CumpleBonifica3.Nombre#"
                        }
                    ]
                },
                {
                    field: "Observacion",
                    title: "OBSERVACIÓN",
                    attributes: { style: "text-align:left" },
                    editor: controlador.ObservacionEditor,
                    width: "400px"
                    //editable: true
                }
                //{ field: "Telefono", title: "TELEFONO", width: "80px" },
                //{ field: "Celular", title: "CELULAR", width: "80px" },
                //{ field: "CorreoElectronico", title: "EMAIL", width: "100px" },
                //{
                //    field: "FFAA",
                //    title: "PERTENECIO A LAS FF.AA.",
                //    width: "50px",
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var tipo = 'NO';
                //        if (item.FFAA == '1') tipo = "SI";

                //        return tipo;
                //    }
                //},
                //{
                //    field: "Discapacidad",
                //    title: "PRESENTA DISCAPACIDAD",
                //    width: "50px",
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var tipo = 'NO';
                //        if (item.Discapacidad == '1') tipo = "SI";

                //        return tipo;
                //    }
                //},
                //{ field: "FechaModificacion", attributes: { style: "text-align:center;" }, title: "FECHA POSTULACION", width: "100px" },
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'DOCUMENTOS',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.IdTieneRequerimiento == 1) {
                //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdContrato + '\')">'; //GenerarFormatoContrato
                //            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Documentos de la Convocatoria" title="Documentos de la Convocatoria"></span>';
                //            controles += '</button>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //}
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        if (item.Estado == 1) {
                //            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Contrato/DescargarArchivo/?id=' + item.IdContrato + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span></a>';
                //        }

                //        return controles;
                //    },
                //    width: '30px'
                //}
            ],
            editable: true
        }).data();
    };

    this.EvaluacionJS.prototype.inicializarGridEntrevistas = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoria").val(res.NroConvocatoria);
                $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
                $("#txtCargoConvocatoria").val(res.NombreCargo);
            },
            error: function (res) {
                debugger;
            }
        });

        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesEntrevistaPersonal',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.IdConvocatoria = $("#hdIdConvocatoria").val();
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
            filter: [
                { field: "IdTrabajador", operator: "equals", value: $("#hdIdTrabajador").val() }
            ],
            //                success: function (res) {
            //                    //controladorApp.notificarMensajeSatisfactorio("Se actualizaron los montos correctamente");
            //                    //controlador.inicializarGrid();

            //                    $('#divGrid').data("kendoGrid").dataSource.read();
            //                    $('#divGrid').data("kendoGrid").refresh();
            //                },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdEvaluacion",
                    fields: {
                        IdEvaluacion: { editable: false, nullable: true },
                        PuntajeTotal: { validation: { required: true, type: "number", min: "0", max: "50" } },
                        //Observacion: { validation: { required: true } },
                        FechaEntrevista: {
                            validation: {
                                required: true,
                                type: "date",
                                format: "dd/MM/yyyy",
                                fechaentrevistavalidation: function (input) {
                                    if (input.is("[name='FechaEntrevista']") && input.val() == "") {
                                        input.attr("data-fechaentrevistavalidation-msg", "requerido");
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

        this.$grid = $("#divGrid").kendoGrid({
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitEntrevista,
            dataType: 'json',
            //dataBound: function (e) {
            //    var columns = e.sender.columns;
            //    var dataItems = e.sender.dataSource.view();
            //    for (var j = 0; j < dataItems.length; j++) {
            //        var aptototal = dataItems[j].get("AptoTotal");

            //        var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
            //        //if (aptototal) 
            //        //    row.addClass("aptoTotal");
            //        //else 
            //        //    row.addClass("noaptoTotal");
            //    }

            //    //debugger;
            //    //alert(this.tbody.find("tr.k-master-row").first());
            //    //this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            //dataBound: function (e) {
            //    $('.k-grid-add').unbind("click");
            //    $('.k-grid-add').bind("click", function () {
            //        if ($("#hdIdEmpleado").val() == 0) {
            //            controladorApp.notificarMensajeDeAlerta('Primero debe completar el registro del empleado');
            //            return false;
            //        }
            //    });
            //},
            columns: [
                {
                    title: '',
                    width: "30px",
                    template: function (item) {
                        return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                    }
                },
                {
                    field: "Nombre",
                    title: "NOMBRE",
                    width: "200px",
                    editable: true,
                    template: function (item) {
                        return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                    }
                },
                {
                    field: "PuntajeTotal",
                    title: "PUNTAJE TOTAL",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        if (item.IdPresento == 0)
                            return "NSP";
                        else if (item.IdPresento == 1)
                            return item.PuntajeTotal;
                        else
                            return "";
                    }
                },
                {
                    field: "AptoTotal",
                    title: "APTO / NO APTO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    }
                },
                {
                    title: "ENTREVISTA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "FechaEntrevista",
                            title: "FECHA",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            editor: controlador.FechaEntrevistaEditor,
                            format: "{0: dd/MM/yyyy}",
                            width: "100px"
                            //template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "HoraEntrevista",
                            title: "HORA",
                            attributes: { style: "text-align:center" },
                            editor: controlador.HoraEntrevistaEditor,
                            format: "{0: HH:mm}",
                            width: "100px"
                            //editor: controlador.Bonifica3DropDownEditor,
                            //template: "#=CumpleBonifica3.Nombre#"
                        }
                    ]
                },
                {
                    field: "Observacion",
                    title: "OBSERVACIÓN",
                    attributes: { style: "text-align:left" },
                    editor: controlador.ObservacionEditor,
                    width: "400px"
                },
                {
                    title: 'ACTA DE EVALUACIÓN',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneActa == 1) {
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoEntrevista/?idEntrevista=' + item.IdEvaluacion + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span> DESCARGAR </a>';
                        }
                        else {
                            if (item.IdTieneActa == 0 && (item.PuntajeTotal > 0 || item.IdPresento == 0)) {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdEvaluacion + '\',\'' + item.Nombre + ' ' + item.Paterno + ' ' + item.Materno + '\',\'' + item.IdTieneExamen + '\',\'' + item.IdPresento + '\')">'; //GenerarFormatoContrato
                                controles += '<span class="glyphicon glyphicon-open" aria-hidden="true" data-uib-tooltip="Documento de entrevista personal" title="Documento de entrevista personal"></span>';
                                controles += ' ADJUNTAR </button>';
                            }
                        }
                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: 'EVALUACIÓN',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneActa == 0 && item.PuntajeTotal == 0 && item.IdPresento == 1) {
                            if (item.IdTieneExamen == 0) {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalEntrevistaSE(\'' + item.IdEvaluacion + '\',\'' + item.Nombre + ' ' + item.Paterno + ' ' + item.Materno + '\')">'; //GenerarFormatoContrato
                                controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Iniciar entrevista personal" title="Iniciar entrevista personal"></span>';
                                controles += ' INICIAR </button>';
                            }
                            if (item.IdTieneExamen == 1) {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalEntrevistaCE(\'' + item.IdEvaluacion + '\',\'' + item.Nombre + ' ' + item.Paterno + ' ' + item.Materno + '\')">'; //GenerarFormatoContrato
                                controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Iniciar entrevista personal" title="Iniciar entrevista personal"></span>';
                                controles += ' INICIAR </button>';
                            }
                        }
                        //else if(item.IdTieneActa == 0 && item.PuntajeTotal == 0 && item.IdPresento == 1) {
                        return controles;
                    },
                    width: '30px'
                }
                
            ]
        }).data();
    };
    this.EvaluacionJS.prototype.inicializarGridEntrevistasPractica = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerPracticaParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoria").val(res.NroConvocatoria);
                $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
                $("#txtCargoConvocatoria").val(res.NombreCargo);
            },
            error: function (res) {
                debugger;
            }
        });

        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesPracticaEntrevistaPersonal',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.IdConvocatoria = $("#hdIdConvocatoria").val();
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
            filter: [
                { field: "IdTrabajador", operator: "equals", value: $("#hdIdTrabajador").val() }
            ],
            schema: {
                total: function (response) {
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdEvaluacion",
                    fields: {
                        IdEvaluacion: { editable: false, nullable: true },
                        PuntajeTotal: { validation: { required: true, type: "number", min: "0", max: "40" } },
                        //Observacion: { validation: { required: true } },
                        FechaEntrevista: {
                            validation: {
                                required: true,
                                type: "date",
                                format: "dd/MM/yyyy",
                                fechaentrevistavalidation: function (input) {
                                    if (input.is("[name='FechaEntrevista']") && input.val() == "") {
                                        input.attr("data-fechaentrevistavalidation-msg", "requerido");
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

        this.$grid = $("#divGrid").kendoGrid({
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInitEntrevistaPractica,
            dataType: 'json',
            //dataBound: function (e) {
            //    var columns = e.sender.columns;
            //    var dataItems = e.sender.dataSource.view();
            //    for (var j = 0; j < dataItems.length; j++) {
            //        var aptototal = dataItems[j].get("AptoTotal");

            //        var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
            //        //if (aptototal) 
            //        //    row.addClass("aptoTotal");
            //        //else 
            //        //    row.addClass("noaptoTotal");
            //    }

            //    //debugger;
            //    //alert(this.tbody.find("tr.k-master-row").first());
            //    //this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},
            //dataBound: function (e) {
            //    $('.k-grid-add').unbind("click");
            //    $('.k-grid-add').bind("click", function () {
            //        if ($("#hdIdEmpleado").val() == 0) {
            //            controladorApp.notificarMensajeDeAlerta('Primero debe completar el registro del empleado');
            //            return false;
            //        }
            //    });
            //},
            columns: [
                {
                    title: '',
                    width: "30px",
                    template: function (item) {
                        return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                    }
                },
                {
                    field: "Nombre",
                    title: "NOMBRE",
                    width: "200px",
                    editable: true,
                    template: function (item) {
                        return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                    }
                },
                {
                    field: "PuntajeTotal",
                    title: "PUNTAJE TOTAL",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        if (item.IdPresento == 0)
                            return "NSP";
                        else if (item.IdPresento == 1)
                            return item.PuntajeTotal;
                        else
                            return "";
                    }
                },
                {
                    field: "AptoTotal",
                    title: "APTO / NO APTO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    }
                },
                {
                    title: "ENTREVISTA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "FechaEntrevista",
                            title: "FECHA",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            editor: controlador.FechaEntrevistaEditor,
                            format: "{0: dd/MM/yyyy}",
                            width: "100px"
                            //template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "HoraEntrevista",
                            title: "HORA",
                            attributes: { style: "text-align:center" },
                            editor: controlador.HoraEntrevistaEditor,
                            format: "{0: HH:mm}",
                            width: "100px"
                            //editor: controlador.Bonifica3DropDownEditor,
                            //template: "#=CumpleBonifica3.Nombre#"
                        }
                    ]
                },
                {
                    field: "Observacion",
                    title: "OBSERVACIÓN",
                    attributes: { style: "text-align:left" },
                    editor: controlador.ObservacionEditor,
                    width: "400px"
                },
                {
                    title: 'ACTA DE EVALUACIÓN',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneActa == 1) {
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Practicas/DescargarArchivoEntrevista/?idEntrevista=' + item.IdEvaluacion + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span> DESCARGAR </a>';
                        }
                        else {
                            if (item.IdTieneActa == 0 && (item.PuntajeTotal > 0 || item.IdPresento == 0)) {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.IdEvaluacion + '\',\'' + item.Nombre + ' ' + item.Paterno + ' ' + item.Materno + '\',\'' + item.IdTieneExamen + '\',\'' + item.IdPresento + '\')">'; //GenerarFormatoContrato
                                controles += '<span class="glyphicon glyphicon-open" aria-hidden="true" data-uib-tooltip="Documento de entrevista personal" title="Documento de entrevista personal"></span>';
                                controles += ' ADJUNTAR </button>';
                            }
                        }
                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: 'EVALUACIÓN',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneActa == 0 && item.PuntajeTotal == 0 && item.IdPresento == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalEntrevistaPracticas(\'' + item.IdEvaluacion + '\',\'' + item.Nombre + ' ' + item.Paterno + ' ' + item.Materno + '\')">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Iniciar entrevista personal" title="Iniciar entrevista personal"></span>';
                            controles += ' INICIAR </button>';
                        }
                        //else if(item.IdTieneActa == 0 && item.PuntajeTotal == 0 && item.IdPresento == 1) {
                        return controles;
                    },
                    width: '30px'
                }

            ]
        }).data();
    };

    this.EvaluacionJS.prototype.ObservacionEditor = function (container, options) {
        $('<textarea name="' + options.field + '" style="width: 98%;" />')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.FechaEntrevistaEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaEntrevista">')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.FechaConocimientoEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaConocimiento">')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.HoraEntrevistaEditor = function (container, options) {
        $('<input data-role="timepicker" type="time" min="08:30" max="18:00" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:HoraEntrevista">')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.HoraConocimientoEditor = function (container, options) {
        $('<input data-role="timepicker" type="time" min="08:30" max="18:00" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:HoraConocimiento">')
            .appendTo(container);
    };
    //$("#timepicker").kendoTimePicker({
    //    dateInput: true
    //});

    this.EvaluacionJS.prototype.inicializarGridCurricular = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoria").val(res.NroConvocatoria);
                $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
                $("#txtCargoConvocatoria").val(res.NombreCargo);

                if (res.IdTieneExamenConoc == 1)
                    $('#divAlertaConocimiento').show();
                
                this.$dataSource = [];
                this.$dataSource = new kendo.data.DataSource({
                    serverPaging: true,
                    serverSorting: true,
                    batch: false,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesEvaluacionCurri',
                            type: 'GET',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                data_param.IdConvocatoria = $("#hdIdConvocatoria").val();
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

                        if (e.items.length > 0) {
                            if (e.action == "itemchange" && (e.field == "AptoFormacion" ||
                                e.field == "AptoCapacitacion" ||
                                e.field == "AptoExperienciaGen" ||
                                e.field == "AptoExperienciaEsp" ||
                                e.field == "PuntajeBonifFormacion" ||
                                e.field == "PuntajeBonifExperienciaEsp" ||
                                e.field == "AptoDDJJ" ||
                                e.field == "AptoSanciones" ||
                                e.field == "BonifFFAA" ||
                                e.field == "BonifDiscapacidad" ||
                                e.field == "BonifDeporte" ||
                                e.field == "FechaConocimiento" ||
                                e.field == "HoraConocimiento" ||
                                e.field == "FechaEntrevista" ||
                                e.field == "HoraEntrevista")) {
                                // ACTUALIZAMOS LOS VALORES ALCANZADOS

                                debugger;
                                //alert(e.items[0].IdEstudioPerfil)
                                var data_param = new FormData();
                                data_param.append('IdEvaluacion', e.items[0].IdEvaluacion);
                                data_param.append('AptoFormacion', e.items[0].AptoFormacion);
                                data_param.append('AptoCapacitacion', e.items[0].AptoCapacitacion);
                                data_param.append('AptoExperienciaGen', e.items[0].AptoExperienciaGen);
                                data_param.append('AptoExperienciaEsp', e.items[0].AptoExperienciaEsp);

                                data_param.append('PuntajeBonifFormacion', e.items[0].PuntajeBonifFormacion);
                                data_param.append('PuntajeBonifExperienciaEsp', e.items[0].PuntajeBonifExperienciaEsp);
                                data_param.append('AptoDDJJ', e.items[0].AptoDDJJ);
                                data_param.append('AptoSanciones', e.items[0].AptoSanciones);
                                data_param.append('BonifFFAA', e.items[0].BonifFFAA);
                                data_param.append('BonifDiscapacidad', e.items[0].BonifDiscapacidad);
                                data_param.append('BonifDeporte', e.items[0].BonifDeporte);
                                data_param.append('FechaConocimiento', kendo.toString(kendo.parseDate(e.items[0].FechaConocimiento), 'dd/MM/yyyy'));
                                data_param.append('HoraConocimiento', kendo.toString(kendo.parseDate(e.items[0].HoraConocimiento), 'HH:mm'));
                                data_param.append('FechaEntrevista', kendo.toString(kendo.parseDate(e.items[0].FechaEntrevista), 'dd/MM/yyyy'));
                                data_param.append('HoraEntrevista', kendo.toString(kendo.parseDate(e.items[0].HoraEntrevista), 'HH:mm'));
                                data_param.append('IdUsuarioModificacion', $("#hdIdTrabajador").val());

                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionCurri',
                                    type: 'POST',
                                    dataType: 'json',
                                    contentType: false,
                                    processData: false,
                                    data: data_param,
                                    success: function (res) {
                                        //$('#divGrid').data("kendoGrid").dataSource.read();
                                        //$('#divGrid').data("kendoGrid").refresh();

                                        if (res.responseText != '') {
                                            debugger;
                                            //alert(res.responseText);
                                            if (e.field == "AptoFormacion") {
                                                if (e.items[0].AptoFormacion == '0') {
                                                    e.items[0].CumpleFormacion.Codigo = 0;
                                                    e.items[0].CumpleFormacion.Nombre = "NO";
                                                    //$("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(4)").value(0);
                                                    //$("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(4)").text('NO'); 
                                                }
                                                if (e.items[0].AptoFormacion == '1') {
                                                    e.items[0].CumpleFormacion.Codigo = 1;
                                                    e.items[0].CumpleFormacion.Nombre = "SI";
                                                    //$("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(4)").value(1);
                                                    //$("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(4)").text('SI'); 
                                                }
                                            }
                                            if (e.field == "AptoCapacitacion") {
                                                if (e.items[0].AptoCapacitacion == '0') {
                                                    e.items[0].CumpleCapacitacion.Codigo = 0;
                                                    e.items[0].CumpleCapacitacion.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoCapacitacion == '1') {
                                                    e.items[0].CumpleCapacitacion.Codigo = 1;
                                                    e.items[0].CumpleCapacitacion.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "AptoExperienciaGen") {
                                                if (e.items[0].AptoExperienciaGen == '0') {
                                                    e.items[0].CumpleExperienciaGen.Codigo = 0;
                                                    e.items[0].CumpleExperienciaGen.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoExperienciaGen == '1') {
                                                    e.items[0].CumpleExperienciaGen.Codigo = 1;
                                                    e.items[0].CumpleExperienciaGen.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "AptoExperienciaEsp") {
                                                if (e.items[0].AptoExperienciaEsp == '0') {
                                                    e.items[0].CumpleExperienciaEsp.Codigo = 0;
                                                    e.items[0].CumpleExperienciaEsp.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoExperienciaEsp == '1') {
                                                    e.items[0].CumpleExperienciaEsp.Codigo = 1;
                                                    e.items[0].CumpleExperienciaEsp.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "AptoDDJJ") {
                                                if (e.items[0].AptoDDJJ == '0') {
                                                    e.items[0].CumpleDDJJ.Codigo = 0;
                                                    e.items[0].CumpleDDJJ.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoDDJJ == '1') {
                                                    e.items[0].CumpleDDJJ.Codigo = 1;
                                                    e.items[0].CumpleDDJJ.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "AptoSanciones") {
                                                if (e.items[0].AptoSanciones == '0') {
                                                    e.items[0].CumpleHabilitacion.Codigo = 0;
                                                    e.items[0].CumpleHabilitacion.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoSanciones == '1') {
                                                    e.items[0].CumpleHabilitacion.Codigo = 1;
                                                    e.items[0].CumpleHabilitacion.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifFFAA") {
                                                if (e.items[0].BonifFFAA == '0') {
                                                    e.items[0].CumpleFFAA.Codigo = 0;
                                                    e.items[0].CumpleFFAA.Nombre = "NO";
                                                }
                                                if (e.items[0].BonifFFAA == '1') {
                                                    e.items[0].CumpleFFAA.Codigo = 1;
                                                    e.items[0].CumpleFFAA.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifDiscapacidad") {
                                                if (e.items[0].BonifDiscapacidad == '0') {
                                                    e.items[0].CumpleDiscapacidad.Codigo = 0;
                                                    e.items[0].CumpleDiscapacidad.Nombre = "NO";
                                                }
                                                if (e.items[0].BonifDiscapacidad == '1') {
                                                    e.items[0].CumpleDiscapacidad.Codigo = 1;
                                                    e.items[0].CumpleDiscapacidad.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifDeporte") {
                                                if (e.items[0].BonifDeporte == '0') {
                                                    e.items[0].CumpleDeportista.Codigo = 0;
                                                    e.items[0].CumpleDeportista.Nombre = "--";
                                                }
                                                if (e.items[0].BonifDeporte == '4') {
                                                    e.items[0].CumpleDeportista.Codigo = 4;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 4%";
                                                }
                                                if (e.items[0].BonifDeporte == '8') {
                                                    e.items[0].CumpleDeportista.Codigo = 8;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 8%";
                                                }
                                                if (e.items[0].BonifDeporte == '12') {
                                                    e.items[0].CumpleDeportista.Codigo = 12;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 12%";
                                                }
                                                if (e.items[0].BonifDeporte == '16') {
                                                    e.items[0].CumpleDeportista.Codigo = 16;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 16%";
                                                }
                                                if (e.items[0].BonifDeporte == '20') {
                                                    e.items[0].CumpleDeportista.Codigo = 20;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 20%";
                                                }
                                            }
                                            if (e.field == "PuntajeBonifFormacion") {
                                                e.items[0].CumpleBonifica3.Codigo = e.items[0].PuntajeBonifFormacion;
                                                e.items[0].CumpleBonifica3.Nombre = e.items[0].PuntajeBonifFormacion;
                                            }
                                            if (e.field == "PuntajeBonifExperienciaEsp") {
                                                e.items[0].CumpleBonifica2.Codigo = e.items[0].PuntajeBonifExperienciaEsp;
                                                e.items[0].CumpleBonifica2.Nombre = e.items[0].PuntajeBonifExperienciaEsp;
                                            }
                                            if (e.field == "FechaEntrevista") {
                                                e.items[0].FechaEntrevista = kendo.toString(kendo.parseDate(e.items[0].FechaEntrevista), 'dd/MM/yyyy');
                                            }
                                            if (e.field == "FechaConocimiento") {
                                                e.items[0].FechaConocimiento = kendo.toString(kendo.parseDate(e.items[0].FechaConocimiento), 'dd/MM/yyyy');
                                            }
                                            if (e.field == "HoraEntrevista") {
                                                e.items[0].HoraEntrevista = kendo.toString(kendo.parseDate(e.items[0].HoraEntrevista), 'HH:mm');
                                            }
                                            if (e.field == "HoraConocimiento") {
                                                e.items[0].HoraConocimiento = kendo.toString(kendo.parseDate(e.items[0].HoraConocimiento), 'HH:mm');
                                            }
                                            //e.items[0].AptoTotal = res.responseText.split("|")[0];
                                            //e.items[0].PuntajeTotal = res.responseText.split("|")[1];

                                            $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(14)").text(res.responseText.split("|")[1]); 
                                            if (res.responseText.split("|")[0] == 0)
                                                $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(15)").html("<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"); 
                                            if (res.responseText.split("|")[0] == 1)
                                                $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(15)").html("<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>");
                                            
                                        }
                                    },
                                    error: function (res) {
                                        debugger;
                                    }
                                });
                            }
                        }
                    },
                    schema: {
                        total: function (response) {
                            //debugger;
                            //var TotalDeRegistros = 0;
                            //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                            return response.length; // TotalDeRegistros;
                        },
                        model: {
                            id: "IdEvaluacion",
                            fields: {
                                Cumple: {
                                    validation: {
                                        required: true,
                                        requisitovalidation: function (input) {
                                            if (input.is("[name='Cumple']") && input.val() == "") {
                                                input.attr("data-cumplevalidation-msg", "El campo es requerido");
                                                return false;
                                            }

                                            return true;
                                        }
                                    }, defaultValue: { Codigo: 0, Nombre: "--" }
                                },
                            }
                        }
                    }
                    //aggregate: [
                    //        { field: "NombreCompleto", aggregate: "count" },
                    //        { field: "NombreOficina", aggregate: "count" }
                    //]
                });
                this.$grid = $("#divGrid").kendoGrid({
                    dataSource: this.$dataSource,
                    autoBind: true,
                    selectable: false,
                    scrollable: false,
                    sortable: false,
                    pageable: false,
                    //groupable: true,
                    detailTemplate: kendo.template($("#template").html()),
                    detailInit: detailInit,
                    dataType: 'json',
                    dataBound: function (e) {
                        var columns = e.sender.columns;
                        //var columnIndex = this.wrapper.find(".k-grid-header [data-field=" + "AptoTotal" + "]").index();

                        // iterate the data items and apply row styles where necessary
                        var dataItems = e.sender.dataSource.view();
                        for (var j = 0; j < dataItems.length; j++) {
                            var aptototal = dataItems[j].get("AptoTotal");

                            var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
                            //if (aptototal) 
                            //    row.addClass("aptoTotal");
                            //else 
                            //    row.addClass("noaptoTotal");
                        }

                        //debugger;
                        //alert(this.tbody.find("tr.k-master-row").first());
                        //this.expandRow(this.tbody.find("tr.k-master-row").first());
                    },
                    columns: [
                        {
                            title: '',
                            width: "30px",
                            template: function (item) {
                                return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                            }
                        },
                        {
                            field: "Nombre",
                            title: "NOMBRE",
                            width: "150px",
                            editable: true,
                            template: function (item) {
                                return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                            }
                        },
                        {
                            field: "AptoDDJJ",
                            title: "DDJJ DE<br>POSTULACIÓN<br>E INCOMPAT.",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleDDJJ.Nombre#"
                        },
                        {
                            title: "FORMACIÓN ACADÉMICA",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "AptoFormacion",
                                    title: "CUMPLE",
                                    //headerAttributes: {
                                    //    "class": "table-header-cell",
                                    //    style: "text-align:center; background-color: RGB(252, 248, 227)"
                                    //},
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleFormacion.Nombre#"
                                },
                                //{
                                //    field: "PuntajeFormacion",
                                //    title: "PUNTAJE",
                                //    attributes: { style: "text-align:center" },
                                //    //format: "{0:P}",
                                //    width: "50px",
                                //    editable: true
                                //},
                                {
                                    field: "PuntajeBonifFormacion",
                                    title: "BONIFICACIÓN",
                                    attributes: { style: "text-align:center" },
                                    //format: "{0:P}",
                                    width: "50px",
                                    editor: controlador.Bonifica3DropDownEditor,
                                    template: "#=CumpleBonifica3.Nombre#"
                                }
                            ]
                        },
                        {
                            title: "EXP. GENERAL",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "AptoExperienciaGen",
                                    title: "CUMPLE",
                                    //headerAttributes: {
                                    //    "class": "table-header-cell",
                                    //    style: "text-align:center; background-color: RGB(252, 248, 227)"
                                    //},
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleExperienciaGen.Nombre#"
                                }//,
                                //{
                                //    field: "PuntajeExperienciaGen",
                                //    title: "PUNTAJE",
                                //    attributes: { style: "text-align:center" },
                                //    //format: "{0:P}",
                                //    width: "50px",
                                //    editable: true
                                //},
                                //{
                                //    field: "PuntajeBonifExperienciaGen",
                                //    title: "BONIFICACIÓN",
                                //    attributes: { style: "text-align:center" },
                                //    //format: "{0:P}",
                                //    width: "50px"
                                //}
                            ]
                        },
                        {
                            title: "EXP. ESPECÍFICA",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "AptoExperienciaEsp",
                                    title: "CUMPLE",
                                    //headerAttributes: {
                                    //    "class": "table-header-cell",
                                    //    style: "text-align:center; background-color: RGB(252, 248, 227)"
                                    //},
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleExperienciaEsp.Nombre#"
                                },
                                //{
                                //    field: "PuntajeExperienciaEsp",
                                //    title: "PUNTAJE",
                                //    attributes: { style: "text-align:center" },
                                //    //format: "{0:P}",
                                //    width: "50px",
                                //    editable: true
                                //},
                                {
                                    field: "PuntajeBonifExperienciaEsp",
                                    title: "BONIFICACIÓN",
                                    attributes: { style: "text-align:center" },
                                    //format: "{0:P}",
                                    width: "50px",
                                    editor: controlador.Bonifica2DropDownEditor,
                                    template: "#=CumpleBonifica2.Nombre#"
                                }
                            ]
                        },
                        {
                            field: "AptoCapacitacion",
                            title: "PROGRAMAS DE<br>ESPECIALIZACIÓN,<br>DIPLOMADO, CURSO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleCapacitacion.Nombre#"
                        },
                        {
                            field: "AptoSanciones",
                            title: "HABILITADO PARA<br>TRABAJAR CON<br>EL ESTADO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleHabilitacion.Nombre#"
                        },
                        {
                            title: "BONIFICACIONES",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "BonifFFAA",
                                    title: "FFAA",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleFFAA.Nombre#"
                                },
                                {
                                    field: "BonifDiscapacidad",
                                    title: "DISCAPACIDAD",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleDiscapacidad.Nombre#"
                                },
                                {
                                    field: "BonifDeporte",
                                    title: "DEPORTISTA<br>CALIFICADO",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDeportistaDropDownEditor,
                                    template: "#=CumpleDeportista.Nombre#"
                                }
                            ]
                        },
                        {
                            field: "PuntajeTotal",
                            title: "PUNTAJE<br>TOTAL",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editable: true
                        },
                        {
                            field: "AptoTotal",
                            title: "APTO /<br>NO APTO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: function (item) {
                                return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : '--'));
                            },
                            editable: true
                        },
                        {
                            title: "EXAMEN DE CONOCIMIENTO",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "FechaConocimiento",
                                    title: "FECHA",
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    editor: controlador.FechaConocimientoEditor,
                                    width: "100px"
                                    //template: "#=CumpleFormacion.Nombre#"
                                },
                                {
                                    field: "HoraConocimiento",
                                    title: "HORA",
                                    attributes: { style: "text-align:center" },
                                    editor: controlador.HoraConocimientoEditor,
                                    width: "100px"
                                    //editor: controlador.Bonifica3DropDownEditor,
                                    //template: "#=CumpleBonifica3.Nombre#"
                                }
                            ],
                            hidden: (res.IdTieneExamenConoc == 0)
                        },
                        {
                            title: "ENTREVISTA",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "FechaEntrevista",
                                    title: "FECHA",
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    editor: controlador.FechaEntrevistaEditor,
                                    format: "{0: dd/MM/yyyy}",
                                    width: "100px"
                                    //template: "#=CumpleFormacion.Nombre#"
                                },
                                {
                                    field: "HoraEntrevista",
                                    title: "HORA",
                                    attributes: { style: "text-align:center" },
                                    editor: controlador.HoraEntrevistaEditor,
                                    format: "{0: HH:mm}",
                                    width: "100px"
                                    //editor: controlador.Bonifica3DropDownEditor,
                                    //template: "#=CumpleBonifica3.Nombre#"
                                }
                            ],
                            hidden: (res.IdTieneExamenConoc == 1)
                        }
                    ],
                    editable: true
                }).data();
            },
            error: function (res) {
                debugger;
            }
        });

    };
    function detailInit(e) {
        var detailRow = e.detailRow;

        //debugger;
        //detailRow.find(".fecha1").html(e.data.Fecha1);
        //detailRow.find(".fecha2").html(e.data.Fecha2);
        //detailRow.find(".fecha3").html(e.data.Fecha3);
        //detailRow.find(".fecha4").html(e.data.Fecha4);
        //detailRow.find(".fecha5").html(e.data.Fecha5);
        //detailRow.find(".fecha6").html(e.data.Fecha6);
        //detailRow.find(".fecha7").html(e.data.Fecha7);
        //detailRow.find(".fecha8").html(e.data.Fecha8);
        //detailRow.find(".fecha9").html(e.data.Fecha9);
        
        //if (e.data.EsFecha1 == 1) {
        //    detailRow.find(".divFecha1").css("border-style", "solid");
        //    detailRow.find(".divFecha1").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha1").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha2 == 1) {
        //    detailRow.find(".divFecha2").css("border-style", "solid");
        //    detailRow.find(".divFecha2").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha2").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha3 == 1) {
        //    detailRow.find(".divFecha3").css("border-style", "solid");
        //    detailRow.find(".divFecha3").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha3").css("border-radius", "10px");
        //    detailRow.find(".btnEvaluacionCurricular").attr("disabled", false);
        //    detailRow.find(".btnEvaluacionCurricular").click( function () {
        //        controlador.abrirModalEvaluacionCurri(e.data.IdConvocatoria);
        //    })
            
        //}
        //if (e.data.EsFecha4 == 1) {
        //    detailRow.find(".divFecha4").css("border-style", "solid");
        //    detailRow.find(".divFecha4").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha4").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha5 == 1) {
        //    detailRow.find(".divFecha5").css("border-style", "solid");
        //    detailRow.find(".divFecha5").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha5").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha6 == 1) {
        //    detailRow.find(".divFecha6").css("border-style", "solid");
        //    detailRow.find(".divFecha6").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha6").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha7 == 1) {
        //    detailRow.find(".divFecha7").css("border-style", "solid");
        //    detailRow.find(".divFecha7").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha7").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha8 == 1) {
        //    detailRow.find(".divFecha8").css("border-style", "solid");
        //    detailRow.find(".divFecha8").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha8").css("border-radius", "10px");
        //}
        //if (e.data.EsFecha9 == 1) {
        //    detailRow.find(".divFecha9").css("border-style", "solid");
        //    detailRow.find(".divFecha9").css("border-color", "#FFC107");
        //    detailRow.find(".divFecha9").css("border-radius", "10px");
        //}

        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridDocumento").kendoGrid({
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionDocumento',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: 'VER DOCUMENTO',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            if (item.IdDocumento == 6001) {
                                var newUrl = controladorApp.obtenerRutaBase() + 'Postulante/HojaVidaAnonimo/?idPostulante=' + e.data.IdPostulante + '&idPostulacion=' + e.data.IdPostulacion + '&idConvocatoria=' + e.data.IdConvocatoria
                                controles += '<a id="btnActualHojaVida" href="' + newUrl + '" target="_blank" class="btn btn-info btn-xs" >';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                controles += '</a>&nbsp;&nbsp;';
                            }
                            else {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdDocumento + ',5)">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                controles += '</button>&nbsp;&nbsp;';
                            }
                        }

                        return controles;
                    },
                    width: '30px'
                },
                {
                    field: "NombreTipoDocumento",
                    title: "TIPO DE DOCUMENTO",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                }
            ]
        });

        detailRow.find(".divGridEstudio").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionEstudio',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacionEstudio",
                        fields: {
                            Auditoria: {
                                validation: {
                                    required: true,
                                    requisitovalidation: function (input) {
                                        if (input.is("[name='Auditoria']") && input.val() == "") {
                                            input.attr("data-requisitovalidation-msg", "Campo requerido");
                                            return false;
                                        }

                                        return true;
                                    }
                                }, defaultValue: { Codigo: 0, Nombre: "--" }
                            },
                        }
                    }
                },
                change: function (e) {
                    //if (e.items.length == 0) {
                    //    controladorApp.notificarMensajeDeInformacion('Debe ingresar estudios de su formación académica en su perfil');
                    //}
                    //else {
                        //$("#btnReporte").prop('disabled', true);
                    debugger;
                    //alert(e.action);
                    //alert(e.field);
                    //alert(e.items[0].IdEstudioAuditoria);
                        if (e.action == "itemchange" && e.field == "IdEstudioAuditoria") {
                            // ACTUALIZAMOS LOS VALORES ALCANZADOS
                            var data_param = new FormData();
                            data_param.append('IdPostulacionEstudio', e.items[0].IdPostulacionEstudio);
                            data_param.append('IdTipoActualizacion', 2);
                            data_param.append('IdEstudioPerfil', 0);
                            data_param.append('IdEstudioAuditoria', e.items[0].IdEstudioAuditoria);

                            $.ajax({
                                url: controladorApp.obtenerRutaBase() + 'Postulante/ActualizarPostulacionEstudio',
                                type: 'POST',
                                dataType: 'json',
                                contentType: false,
                                processData: false,
                                data: data_param,
                                success: function (res) {
                                    controladorApp.notificarMensajeSatisfactorio("Actualizado correctamente");

                                    detailRow.find(".divGridEstudio").data("kendoGrid").dataSource.page(1);
                                    
                                },
                                error: function (res) {
                                    debugger;
                                }
                            });
                        }
                    //}
                }
            },
            scrollable: false,
            sortable: false,
            pageable: false,
            columns: [
                {
                    title: "INFORMACIÓN INGRESADA POR EL POSTULANTE",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            //INGRESAR DETALLE DE LA EVALUACION
                            title: 'VER DOCUMENTO',
                            attributes: { style: "text-align:center;" },
                            template: function (item) {
                                var controles = "";
                                if (item.IdTieneArchivo == 1) {
                                    controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdEstudio + ',1)">'; //GenerarFormatoContrato
                                    controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                    controles += '</button>&nbsp;&nbsp;';
                                }

                                return controles;
                            },
                            width: '30px'
                        },
                        {
                            field: "NombreGrado",
                            title: "NIVEL EDUCATIVO",
                            attributes: { style: "text-align:left;" },
                            width: "80px",
                            editable: true
                        },
                        {
                            field: "NombreNivel",
                            title: "NIVEL ALCANZADO",
                            attributes: { style: "text-align:left;" },
                            width: "100px",
                            editable: true
                        },
                        {
                            field: "Especialidad",
                            title: "ESPECIALIDAD",
                            attributes: { style: "text-align:left;" },
                            width: "150px"
                        },
                        {
                            field: "Institucion",
                            title: "INSTITUCIÓN",
                            attributes: { style: "text-align:left;" },
                            width: "150px",
                            editable: true
                        },
                        {
                            field: "Ciudad",
                            title: "CIUDAD / PAÍS",
                            width: "100px",
                            attributes: { style: "text-align:left;" },
                            editable: true
                        },
                        {
                            field: "AnioMes",
                            title: "OBTENCIÓN DEL ESTUDIO",
                            attributes: { style: "text-align:left;" },
                            width: "80px",
                            editable: true
                        },
                        {
                            field: "IdEstudioPerfil",
                            title: "¿ES REQUISITO PARA EL PERFIL?",
                            attributes: { style: "text-align:center;" },
                            width: "100px",
                            //editable: true,
                            //editor: controlador.RequisitoDropDownEditor,
                            template: function (item) {
                                return (item.Requisito.Nombre == 'NO' ? "<span style='text-align:center; background-color: RGB(247,187,187); padding: 2px; width: 70px; display: inline-block; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.Requisito.Nombre == 'SI' ? "<span style='text-align:center; background-color: RGB(199,249,215); padding: 2px; width: 70px; display: inline-block; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : ''));

                                //"#=Requisito.Nombre#"
                            },
                            editable: true
                        }
                    ]
                },
                {
                    title: "",
                    attributes: { style: "text-align:center; border-style: solid; border-width: 2px; border-color: red;" },
                    columns: [
                        {
                            field: "IdEstudioAuditoria",
                            title: "¿REQUERIDO PARA FISCALIZACIÓN POSTERIOR?",
                            attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); " }, //padding: 3px 5px 3px 5px
                            width: "100px",
                            //editable: true,
                            editor: controlador.RequisitoDropDownEditor,
                            template: "#=Auditoria.Nombre#"
                        }
                    ]
                }
            ],
            editable: true
        }); //.data();
        detailRow.find(".divGridCapacitacion").kendoGrid({
            //toolbar: ["create"], //"excel", 
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionCapacitacion',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacionCapacitacion",
                        fields: {
                            Auditoria: {
                                validation: {
                                    required: true,
                                    requisitovalidation: function (input) {
                                        if (input.is("[name='Auditoria']") && input.val() == "") {
                                            input.attr("data-requisitovalidation-msg", "Campo requerido");
                                            return false;
                                        }

                                        return true;
                                    }
                                }, defaultValue: { Codigo: 0, Nombre: "--" }
                            },
                        }
                    }
                },
                change: function (e) {
                    if (e.action == "itemchange" && e.field == "IdCapacitacionAuditoria") {
                        // ACTUALIZAMOS LOS VALORES ALCANZADOS
                        var data_param = new FormData();
                        data_param.append('IdPostulacionCapacitacion', e.items[0].IdPostulacionCapacitacion);
                        data_param.append('IdTipoActualizacion', 2);
                        data_param.append('IdCapacitacionPerfil', 0);
                        data_param.append('IdCapacitacionAuditoria', e.items[0].IdCapacitacionAuditoria);

                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Postulante/ActualizarPostulacionCapacitacion',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                controladorApp.notificarMensajeSatisfactorio("Actualizado correctamente");

                                detailRow.find(".divGridCapacitacion").data("kendoGrid").dataSource.page(1);

                            },
                            error: function (res) {
                                debugger;
                            }
                        });
                    }
                    //}
                }
            },
            scrollable: false,
            sortable: false,
            pageable: false,
            columns: [
                {
                    title: "INFORMACIÓN INGRESADA POR EL POSTULANTE",
                    attributes: { style: "text-align:center; border-style: solid; border-width: 2px; border-color: RGB(240,84,84);" },
                    columns: [
                        {
                            //INGRESAR DETALLE DE LA EVALUACION
                            title: 'VER DOCUMENTO',
                            attributes: { style: "text-align:center;" },
                            template: function (item) {
                                var controles = "";
                                if (item.IdTieneArchivo == 1) {
                                    controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdCapacitacion + ',2)">'; //GenerarFormatoContrato
                                    controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                    controles += '</button>&nbsp;&nbsp;';
                                }

                                return controles;
                            },
                            width: '30px',
                            editable: true
                        },
                        {
                            field: "Especialidad",
                            title: "NOMBRE DE LA CAPACITACIÓN",
                            attributes: { style: "text-align:left;" },
                            width: "200px",
                            editable: true
                        },
                        {
                            field: "Institucion",
                            title: "INSTITUCIÓN",
                            attributes: { style: "text-align:left;" },
                            width: "200px",
                            editable: true
                        },
                        {
                            field: "Ciudad",
                            title: "CIUDAD / PAIS",
                            attributes: { style: "text-align:left;" },
                            width: "100px",
                            editable: true
                        },
                        {
                            field: "FechaInicio",
                            title: "FECHA DE INICIO",
                            width: "50px",
                            attributes: { style: "text-align:center;" },
                            editable: true
                            //editor: controlador.FechaInicioEditor,
                        },
                        {
                            field: "FechaFin",
                            title: "FECHA DE TÉRMINO",
                            width: "50px",
                            attributes: { style: "text-align:center;" },
                            editable: true
                            //editor: controlador.FechaFinEditor
                        },
                        {
                            field: "Horas",
                            title: "HORAS LECTIVAS",
                            attributes: { style: "text-align:right;" },
                            width: "50px",
                            editable: true
                        },
                        {
                            field: "IdCapacitacionPerfil",
                            title: "¿ES REQUISITO PARA EL PERFIL?",
                            attributes: { style: "text-align:center;" },
                            width: "100px",
                            editable: true,
                            //editor: controlador.RequisitoDropDownEditor,
                            template: function (item) {
                                return (item.Requisito.Nombre == 'NO' ? "<span style='text-align:center; background-color: RGB(247,187,187); padding: 2px; width: 70px; display: inline-block; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.Requisito.Nombre == 'SI' ? "<span style='text-align:center; background-color: RGB(199,249,215); padding: 2px; width: 70px; display: inline-block; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : ''));

                                //"#=Requisito.Nombre#"
                            },
                            editable: true
                        }
                    ]
                },
                {
                    title: "",
                    attributes: { style: "text-align:center; border-style: solid; border-width: 2px; border-color: RGB(240,84,84);" },
                    columns: [
                        {
                            field: "IdCapacitacionAuditoria",
                            title: "¿REQUERIDO PARA FISCALIZACIÓN POSTERIOR?",
                            attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); " },
                            width: "100px",
                            //editable: true,
                            editor: controlador.RequisitoDropDownEditor,
                            template: "#=Auditoria.Nombre#"
                        }
                    ]
                }
            ],
            editable: true
        });
        detailRow.find(".divGridExperiencia").kendoGrid({
            dataSource: {
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionExperiencia',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacionExperiencia",
                        fields: {
                            Auditoria: {
                                validation: {
                                    required: true,
                                    requisitovalidation: function (input) {
                                        if (input.is("[name='Auditoria']") && input.val() == "") {
                                            input.attr("data-requisitovalidation-msg", "Campo requerido");
                                            return false;
                                        }

                                        return true;
                                    }
                                }, defaultValue: { Codigo: 0, Nombre: "--" }
                            },
                        }
                    }
                },
                change: function (e) {
                    if (e.action == "itemchange" && e.field == "IdExperienciaAuditoria") {
                        // ACTUALIZAMOS LOS VALORES ALCANZADOS
                        var data_param = new FormData();
                        data_param.append('IdPostulacionExperiencia', e.items[0].IdPostulacionExperiencia);
                        data_param.append('IdTipoActualizacion', 2);
                        data_param.append('IdExperienciaPerfil', 0);
                        data_param.append('IdExperienciaAuditoria', e.items[0].IdExperienciaAuditoria);

                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Postulante/ActualizarPostulacionExperiencia',
                            type: 'POST',
                            dataType: 'json',
                            contentType: false,
                            processData: false,
                            data: data_param,
                            success: function (res) {
                                controladorApp.notificarMensajeSatisfactorio("Actualizado correctamente");

                                detailRow.find(".divGridExperiencia").data("kendoGrid").dataSource.page(1);

                            },
                            error: function (res) {
                                debugger;
                            }
                        });
                    }
                    //}
                },
                group: {
                    field: "IdExperienciaPerfil", aggregates: [
                       { field: "ExperienciaPerfil", aggregate: "max" },
                       { field: "RangoFecha", aggregate: "sum" }
                    ]
                },
                aggregate: [
                        { field: "ExperienciaPerfil", aggregate: "count" },
                        { field: "RangoFecha", aggregate: "sum" }
                ]
            },
            scrollable: false,
            sortable: false,
            pageable: false,
            columns: [
                {
                    title: "INFORMACIÓN INGRESADA POR EL POSTULANTE",
                    attributes: { style: "text-align:center;" },
                    columns: [
                        {
                            //INGRESAR DETALLE DE LA EVALUACION
                            title: 'VER DOCUMENTO',
                            attributes: { style: "text-align:center;" },
                            template: function (item) {
                                var controles = "";
                                if (item.IdTieneArchivo == 1) {
                                    controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdLaboral + ',3)">'; //GenerarFormatoContrato
                                    controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                    controles += '</button>&nbsp;&nbsp;';
                                }

                                return controles;
                            },
                            width: '30px'
                        },
                        {
                            field: "IdExperienciaPerfil",
                            title: "NOMBRE DE LA ENTIDAD O EMPRESA",
                            attributes: { style: "text-align:left;" },
                            width: "300px",
                            template: function (item) {
                                return item.Empresa;
                            },
                            aggregates: ["count"],
                            groupHeaderTemplate: "TIPO DE EXPERIENCIA: #= aggregates.ExperienciaPerfil.max # ", //(Items: #= #)
                            //footerTemplate: "<div style='float: left; font-size: 14px'>TOTAL: </div>",
                            footerTemplate: "EXPERIENCIA TOTAL: ",
                            //footerTemplate: "Total: ",
                            //groupFooterTemplate: "Sub Total: #=count#",
                            groupFooterTemplate: "Sub Total: "
                        },
                        {
                            field: "Cargo",
                            title: "CARGO DESEMPEÑADO",
                            attributes: { style: "text-align:left;" },
                            width: "200px",
                            editable: true
                        },
                        {
                            field: "FechaInicio",
                            title: "FECHA DE INICIO",
                            width: "30px",
                            attributes: { style: "text-align:center;" },
                            editable: true
                        },
                        {
                            field: "FechaFin",
                            title: "FECHA DE TÉRMINO",
                            width: "30px",
                            attributes: { style: "text-align:center;" },
                            editable: true
                        },
                        //{
                        //    field: "RangoFechaCadena",
                        //    title: "TIEMPO DE EXPERIENCIA",
                        //    attributes: { style: "text-align:left;" },
                        //    width: "100px",
                        //    editable: true,
                        //    aggregates: ["sum"],
                        //    footerTemplate: "#= kendo.toString(sum, 'C') #",
                        //    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                        //},
                        {
                            field: "RangoFecha",
                            title: "TIEMPO DE EXPERIENCIA",
                            attributes: { style: "text-align:left;" },
                            width: "50px",
                            template: function (item) {
                                return item.RangoFechaCadena;
                            },
                            aggregates: ["sum"],
                            footerTemplate: "#= controlador.MostarRangoFechas(sum) #",
                            groupFooterTemplate: "#= controlador.MostarRangoFechas(sum) #"
                        },
                //{
                //    field: "Descripcion",
                //    title: "DESCRIPCIÓN DEL TRABAJO REALIZADO",
                //    attributes: { style: "text-align:left;" },
                //    width: "450px",
                //    editable: true
                //},

                //{
                //    field: "IdExperienciaPerfil",
                //    title: "TIPO DE EXPERIENCIA",
                //    attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px" },
                //    width: "0px",
                //    //aggregates: ["count"],
                //    //editable: true,
                //    //editor: controlador.RequisitoExpDropDownEditor,
                //    //template: "#=Requisito.Nombre#",
                //    groupHeaderTemplate: "TIPO DE EXPERIENCIA: #= aggregates.ExperienciaPerfil.max # " //(Items: #= #)
                //}
                    ]
                },
                {
                    title: "",
                    attributes: { style: "text-align:center; border-style: solid; border-width: 2px; border-color: red;" },
                    columns: [
                        {
                            field: "IdExperienciaAuditoria",
                            title: "¿REQUERIDO PARA FISCALIZACIÓN POSTERIOR?",
                            attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); " }, //padding: 3px 5px 3px 5px
                            width: "100px",
                            //editable: true,
                            editor: controlador.RequisitoDropDownEditor,
                            template: "#=Auditoria.Nombre#"
                        }
                    ]
                }
            ],
            editable: true
        });

        detailRow.find(".btnGuardarObs").click(function () {
            controlador.actualizarEvaluacionObservacion(this, e.data.IdEvaluacion);
        })

        detailRow.find(".txtObservacion").val(e.data.Observacion);
    }
    function detailInitPractica(e) {
        var detailRow = e.detailRow;

        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridDocumento").kendoGrid({
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionPracticaDocumento',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
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
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "NombreTipoDocumento",
                    title: "TIPO DE DOCUMENTO",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdDocumento + ',6)">';
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                            controles += '</button>&nbsp;&nbsp;';
                        }

                        return controles;
                    },
                    width: '30px'
                }
            ]
        });

        detailRow.find(".btnGuardarObs").click(function () {
            controlador.actualizarEvaluacionPracticaObservacion(this, e.data.IdEvaluacion);
        })

        detailRow.find(".txtObservacion").val(e.data.Observacion);
    }
    function detailInitEntrevista(e) {
        var detailRow = e.detailRow;

        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridDocumento").kendoGrid({
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionDocumento',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "NombreTipoDocumento",
                    title: "TIPO DE DOCUMENTO",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            if (item.IdDocumento == 6001) {
                                var newUrl = controladorApp.obtenerRutaBase() + 'Postulante/HojaVidaAnonimo/?idPostulante=' + e.data.IdPostulante + '&idPostulacion=' + e.data.IdPostulacion + '&idConvocatoria=' + e.data.IdConvocatoria
                                controles += '<a id="btnActualHojaVida" href="' + newUrl +'" target="_blank" class="btn btn-info btn-xs" >';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                controles += '</a>&nbsp;&nbsp;';
                            }
                            else {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdDocumento + ',5)">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                                controles += '</button>&nbsp;&nbsp;';
                            }
                        }

                        return controles;
                    },
                    width: '30px'
                }
            ]
        });
        detailRow.find(".divGridEstudio").kendoGrid({
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
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionEstudio',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "NombreGrado",
                    title: "NIVEL EDUCATIVO",
                    attributes: { style: "text-align:left;" },
                    width: "80px"
                },
                {
                    field: "NombreNivel",
                    title: "NIVEL ALCANZADO",
                    attributes: { style: "text-align:left;" },
                    width: "100px"
                },
                {
                    field: "Especialidad",
                    title: "ESPECIALIDAD",
                    attributes: { style: "text-align:left;" },
                    width: "150px"
                },
                {
                    field: "Institucion",
                    title: "INSTITUCIÓN",
                    attributes: { style: "text-align:left;" },
                    width: "150px"
                },
                {
                    field: "Ciudad",
                    title: "CIUDAD / PAÍS",
                    width: "100px",
                    attributes: { style: "text-align:left;" }
                },
                {
                    field: "AnioMes",
                    title: "OBTENCIÓN DEL ESTUDIO",
                    attributes: { style: "text-align:left;" },
                    width: "80px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdEstudio + ',1)">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                            controles += '</button>&nbsp;&nbsp;';
                        }

                        return controles;
                    },
                    width: '30px'
                },
                {
                    field: "IdEstudioPerfil",
                    title: "ES REQUISITO PARA EL PERFIL",
                    attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px" },
                    width: "100px",
                    //editable: true,
                    editor: controlador.RequisitoDropDownEditor,
                    template: "#=Requisito.Nombre#"
                }
            ]
        }); //.data();
        detailRow.find(".divGridCapacitacion").kendoGrid({
            //toolbar: ["create"], //"excel", 
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionCapacitacion',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: false,
            pageable: false,
            columns: [
                {
                    field: "Especialidad",
                    title: "NOMBRE DE LA CAPACITACIÓN",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    field: "Institucion",
                    title: "INSTITUCIÓN",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    field: "Ciudad",
                    title: "CIUDAD / PAIS",
                    attributes: { style: "text-align:left;" },
                    width: "100px"
                },
                {
                    field: "FechaInicio",
                    title: "FECHA DE INICIO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                    //editor: controlador.FechaInicioEditor,
                },
                {
                    field: "FechaFin",
                    title: "FECHA DE TÉRMINO",
                    width: "50px",
                    attributes: { style: "text-align:center;" }
                    //editor: controlador.FechaFinEditor
                },
                {
                    field: "Horas",
                    title: "HORAS LECTIVAS",
                    attributes: { style: "text-align:right;" },
                    width: "50px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdCapacitacion + ',2)">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                            controles += '</button>&nbsp;&nbsp;';
                        }

                        return controles;
                    },
                    width: '30px'
                },
                {
                    field: "IdCapacitacionPerfil",
                    title: "ES REQUISITO PARA EL PERFIL",
                    attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px" },
                    width: "100px",
                    //editable: true,
                    editor: controlador.RequisitoDropDownEditor,
                    template: "#=Requisito.Nombre#"
                }
            ]
        });
        detailRow.find(".divGridExperiencia").kendoGrid({
            dataSource: {
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionExperiencia',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacionExperiencia"
                    }
                },
                group: {
                    field: "IdExperienciaPerfil", aggregates: [
                        { field: "ExperienciaPerfil", aggregate: "max" },
                        { field: "RangoFecha", aggregate: "sum" }
                    ]
                },
                aggregate: [
                    { field: "ExperienciaPerfil", aggregate: "count" },
                    { field: "RangoFecha", aggregate: "sum" }
                ]
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "IdExperienciaPerfil",
                    title: "NOMBRE DE LA ENTIDAD O EMPRESA",
                    attributes: { style: "text-align:left;" },
                    width: "300px",
                    template: function (item) {
                        return item.Empresa;
                    },
                    aggregates: ["count"],
                    groupHeaderTemplate: "TIPO DE EXPERIENCIA: #= aggregates.ExperienciaPerfil.max # ", //(Items: #= #)
                    //footerTemplate: "<div style='float: left; font-size: 14px'>TOTAL: </div>",
                    footerTemplate: "EXPERIENCIA TOTAL: ",
                    //footerTemplate: "Total: ",
                    //groupFooterTemplate: "Sub Total: #=count#",
                    groupFooterTemplate: "Sub Total: "
                },
                {
                    field: "Cargo",
                    title: "CARGO DESEMPEÑADO",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    field: "FechaInicio",
                    title: "FECHA DE INICIO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "FechaFin",
                    title: "FECHA DE TÉRMINO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "RangoFechaCadena",
                //    title: "TIEMPO DE EXPERIENCIA",
                //    attributes: { style: "text-align:left;" },
                //    width: "100px",
                //    editable: true,
                //    aggregates: ["sum"],
                //    footerTemplate: "#= kendo.toString(sum, 'C') #",
                //    groupFooterTemplate: "#= kendo.toString(sum, 'C') #"
                //},
                {
                    field: "RangoFecha",
                    title: "TIEMPO DE EXPERIENCIA",
                    attributes: { style: "text-align:left;" },
                    width: "50px",
                    template: function (item) {
                        return item.RangoFechaCadena;
                    },
                    aggregates: ["sum"],
                    footerTemplate: "#= controlador.MostarRangoFechas(sum) #",
                    groupFooterTemplate: "#= controlador.MostarRangoFechas(sum) #"
                },
                //{
                //    field: "Descripcion",
                //    title: "DESCRIPCIÓN DEL TRABAJO REALIZADO",
                //    attributes: { style: "text-align:left;" },
                //    width: "450px",
                //    editable: true
                //},
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdLaboral + ',3)">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                            controles += '</button>&nbsp;&nbsp;';
                        }

                        return controles;
                    },
                    width: '30px'
                }//,
                //{
                //    field: "IdExperienciaPerfil",
                //    title: "TIPO DE EXPERIENCIA",
                //    attributes: { style: "text-align:center; background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px" },
                //    width: "0px",
                //    //aggregates: ["count"],
                //    //editable: true,
                //    //editor: controlador.RequisitoExpDropDownEditor,
                //    //template: "#=Requisito.Nombre#",
                //    groupHeaderTemplate: "TIPO DE EXPERIENCIA: #= aggregates.ExperienciaPerfil.max # " //(Items: #= #)
                //}
            ]
        });
    }
    function detailInitEntrevistaPractica(e) {
        var detailRow = e.detailRow;

        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".divGridDocumento").kendoGrid({
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Postulante/ListarPostulacionPracticaDocumento',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.IdPostulacion = e.data.IdPostulacion;
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
                //pageSize: 2,
                //change: function (e) {
                //    debugger;

                //    //var detailRow = e.detailRow;
                //    detailRow.find(".totalPostulantes").html(this.total());

                //    //$(".totalPostulantes").html(this.total());
                //},
                schema: {
                    total: function (response) {
                        return response.length; // TotalDeRegistros;
                    },
                    model: {
                        id: "IdPostulacion"
                    }
                }
            },
            scrollable: false,
            sortable: true,
            pageable: false,
            columns: [
                {
                    field: "NombreTipoDocumento",
                    title: "TIPO DE DOCUMENTO",
                    attributes: { style: "text-align:left;" },
                    width: "200px"
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' + item.IdDocumento + ',6)">';
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Ver documento adjunto" title="Ver documento adjunto"></span>';
                            controles += '</button>&nbsp;&nbsp;';
                        }

                        return controles;
                    },
                    width: '30px'
                }
            ]
        });
    }

    this.EvaluacionJS.prototype.RequisitoDropDownEditor = function (container, options) {
        var data = [
            { Nombre: "NO", Codigo: "0" },
            { Nombre: "SI", Codigo: "1" }
        ];

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                //optionLabel: "-- Seleccione --",
                dataSource: data
            });
    };
    this.EvaluacionJS.prototype.CumpleDeportistaDropDownEditor = function (container, options) {
        var data = [
                        { Nombre: "--", Codigo: "0" },
                        { Nombre: "BONIF 20%", Codigo: "20" },
                        { Nombre: "BONIF 16%", Codigo: "16" },
                        { Nombre: "BONIF 12%", Codigo: "12" },
                        { Nombre: "BONIF 8%", Codigo: "8" },
                        { Nombre: "BONIF 4%", Codigo: "4" }
        ];

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                //optionLabel: "-- Seleccione --",
                dataSource: data
            });
    };
    this.EvaluacionJS.prototype.CumpleDropDownEditor = function (container, options) {
        var data = [
            { Nombre: "--", Codigo: "0" },
            { Nombre: "SI", Codigo: "1" },
            { Nombre: "NO", Codigo: "0" }
        ];

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                //optionLabel: "-- Seleccione --",
                dataSource: data
            });
    };
    this.EvaluacionJS.prototype.Bonifica3DropDownEditor = function (container, options) {
        var data = [
                        { Nombre: "0", Codigo: "0" },
                        { Nombre: "3", Codigo: "3" }
        ];

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                //optionLabel: "-- Seleccione --",
                dataSource: data
            });
    };
    this.EvaluacionJS.prototype.Bonifica2DropDownEditor = function (container, options) {
        var data = [
                        { Nombre: "0", Codigo: "0" },
                        { Nombre: "2", Codigo: "2" }
        ];

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                //optionLabel: "-- Seleccione --",
                dataSource: data
            });
    };
    this.EvaluacionJS.prototype.MostarRangoFechas = function (suma) {
        var years = Math.trunc((suma / 365.25));
        var months = Math.trunc((((suma / 365.25) - years) * 12));
        var days = Math.trunc(((((suma / 365.25) - years) * 12) - months) * 365.25 / 12);

        return years + " años, " + " " + months + " meses" + " " + days + " días";
    }
    this.EvaluacionJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.EvaluacionJS.prototype.inicializarGridCurricularPractica = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerPracticaParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoria").val(res.NroConvocatoria);
                $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
                $("#txtCargoConvocatoria").val(res.NombreCargo);

                if (res.IdTieneExamenConoc == 1)
                    $('#divAlertaConocimiento').show();

                this.$dataSource = [];
                this.$dataSource = new kendo.data.DataSource({
                    serverPaging: true,
                    serverSorting: true,
                    batch: false,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesPracticaEvaluacionCurri',
                            type: 'GET',
                            dataType: 'json',
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};

                            if ($operation === "read") {
                                data_param.IdConvocatoria = $("#hdIdConvocatoria").val();
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

                        if (e.items.length > 0) {
                            if (e.action == "itemchange" && (e.field == "AptoDDJJ" ||
                                //e.field == "FechaConocimiento" ||
                                //e.field == "HoraConocimiento" ||
                                e.field == "BonifFFAA" ||
                                e.field == "BonifDiscapacidad" ||
                                e.field == "BonifDeporte" ||
                                e.field == "FechaEntrevista" ||
                                e.field == "HoraEntrevista")) {
                                // ACTUALIZAMOS LOS VALORES ALCANZADOS

                                debugger;
                                //alert(e.items[0].IdEstudioPerfil)
                                var data_param = new FormData();
                                data_param.append('IdEvaluacion', e.items[0].IdEvaluacion);
                                data_param.append('AptoDDJJ', e.items[0].AptoDDJJ);
                                data_param.append('BonifFFAA', e.items[0].BonifFFAA);
                                data_param.append('BonifDiscapacidad', e.items[0].BonifDiscapacidad);
                                data_param.append('BonifDeporte', e.items[0].BonifDeporte);
                                //data_param.append('FechaConocimiento', kendo.toString(kendo.parseDate(e.items[0].FechaConocimiento), 'dd/MM/yyyy'));
                                //data_param.append('HoraConocimiento', kendo.toString(kendo.parseDate(e.items[0].HoraConocimiento), 'HH:mm'));
                                data_param.append('FechaEntrevista', kendo.toString(kendo.parseDate(e.items[0].FechaEntrevista), 'dd/MM/yyyy'));
                                data_param.append('HoraEntrevista', kendo.toString(kendo.parseDate(e.items[0].HoraEntrevista), 'HH:mm'));
                                data_param.append('IdUsuarioModificacion', $("#hdIdTrabajador").val());

                                $.ajax({
                                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarPracticaEvaluacionCurri',
                                    type: 'POST',
                                    dataType: 'json',
                                    contentType: false,
                                    processData: false,
                                    data: data_param,
                                    success: function (res) {
                                        //$('#divGrid').data("kendoGrid").dataSource.read();
                                        //$('#divGrid').data("kendoGrid").refresh();

                                        if (res.responseText != '') {
                                            debugger;
                                            if (e.field == "AptoDDJJ") {
                                                if (e.items[0].AptoDDJJ == '0') {
                                                    e.items[0].CumpleDDJJ.Codigo = 0;
                                                    e.items[0].CumpleDDJJ.Nombre = "NO";
                                                }
                                                if (e.items[0].AptoDDJJ == '1') {
                                                    e.items[0].CumpleDDJJ.Codigo = 1;
                                                    e.items[0].CumpleDDJJ.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifFFAA") {
                                                if (e.items[0].BonifFFAA == '0') {
                                                    e.items[0].CumpleFFAA.Codigo = 0;
                                                    e.items[0].CumpleFFAA.Nombre = "NO";
                                                }
                                                if (e.items[0].BonifFFAA == '1') {
                                                    e.items[0].CumpleFFAA.Codigo = 1;
                                                    e.items[0].CumpleFFAA.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifDiscapacidad") {
                                                if (e.items[0].BonifDiscapacidad == '0') {
                                                    e.items[0].CumpleDiscapacidad.Codigo = 0;
                                                    e.items[0].CumpleDiscapacidad.Nombre = "NO";
                                                }
                                                if (e.items[0].BonifDiscapacidad == '1') {
                                                    e.items[0].CumpleDiscapacidad.Codigo = 1;
                                                    e.items[0].CumpleDiscapacidad.Nombre = "SI";
                                                }
                                            }
                                            if (e.field == "BonifDeporte") {
                                                if (e.items[0].BonifDeporte == '0') {
                                                    e.items[0].CumpleDeportista.Codigo = 0;
                                                    e.items[0].CumpleDeportista.Nombre = "--";
                                                }
                                                if (e.items[0].BonifDeporte == '4') {
                                                    e.items[0].CumpleDeportista.Codigo = 4;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 4%";
                                                }
                                                if (e.items[0].BonifDeporte == '8') {
                                                    e.items[0].CumpleDeportista.Codigo = 8;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 8%";
                                                }
                                                if (e.items[0].BonifDeporte == '12') {
                                                    e.items[0].CumpleDeportista.Codigo = 12;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 12%";
                                                }
                                                if (e.items[0].BonifDeporte == '16') {
                                                    e.items[0].CumpleDeportista.Codigo = 16;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 16%";
                                                }
                                                if (e.items[0].BonifDeporte == '20') {
                                                    e.items[0].CumpleDeportista.Codigo = 20;
                                                    e.items[0].CumpleDeportista.Nombre = "BONIF 20%";
                                                }
                                            }
                                            if (e.field == "FechaEntrevista") {
                                                e.items[0].FechaEntrevista = kendo.toString(kendo.parseDate(e.items[0].FechaEntrevista), 'dd/MM/yyyy');
                                            }
                                            //if (e.field == "FechaConocimiento") {
                                            //    e.items[0].FechaConocimiento = kendo.toString(kendo.parseDate(e.items[0].FechaConocimiento), 'dd/MM/yyyy');
                                            //}
                                            if (e.field == "HoraEntrevista") {
                                                e.items[0].HoraEntrevista = kendo.toString(kendo.parseDate(e.items[0].HoraEntrevista), 'HH:mm');
                                            }
                                            //if (e.field == "HoraConocimiento") {
                                            //    e.items[0].HoraConocimiento = kendo.toString(kendo.parseDate(e.items[0].HoraConocimiento), 'HH:mm');
                                            //}
                                            //e.items[0].AptoTotal = res.responseText.split("|")[0];
                                            //e.items[0].PuntajeTotal = res.responseText.split("|")[1];

                                            $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(7)").text(res.responseText.split("|")[1]);
                                            if (res.responseText.split("|")[0] == 0)
                                                $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(8)").html("<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>");
                                            if (res.responseText.split("|")[0] == 1)
                                                $("#divGrid").find("tr[data-uid='" + e.items[0].uid + "'] td:eq(8)").html("<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>");

                                        }
                                    },
                                    error: function (res) {
                                        debugger;
                                    }
                                });
                            }
                        }
                    },
                    schema: {
                        total: function (response) {
                            //debugger;
                            //var TotalDeRegistros = 0;
                            //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                            return response.length; // TotalDeRegistros;
                        },
                        model: {
                            id: "IdEvaluacion",
                            fields: {
                                Cumple: {
                                    validation: {
                                        required: true,
                                        requisitovalidation: function (input) {
                                            if (input.is("[name='Cumple']") && input.val() == "") {
                                                input.attr("data-cumplevalidation-msg", "El campo es requerido");
                                                return false;
                                            }

                                            return true;
                                        }
                                    }, defaultValue: { Codigo: 0, Nombre: "--" }
                                },
                            }
                        }
                    }
                    //aggregate: [
                    //        { field: "NombreCompleto", aggregate: "count" },
                    //        { field: "NombreOficina", aggregate: "count" }
                    //]
                });
                this.$grid = $("#divGrid").kendoGrid({
                    dataSource: this.$dataSource,
                    autoBind: true,
                    selectable: false,
                    scrollable: false,
                    sortable: false,
                    pageable: false,
                    //groupable: true,
                    detailTemplate: kendo.template($("#template").html()),
                    detailInit: detailInitPractica,
                    dataType: 'json',
                    dataBound: function (e) {
                        var columns = e.sender.columns;
                        //var columnIndex = this.wrapper.find(".k-grid-header [data-field=" + "AptoTotal" + "]").index();

                        // iterate the data items and apply row styles where necessary
                        var dataItems = e.sender.dataSource.view();
                        for (var j = 0; j < dataItems.length; j++) {
                            var aptototal = dataItems[j].get("AptoTotal");

                            var row = e.sender.tbody.find("[data-uid='" + dataItems[j].uid + "']");
                            //if (aptototal) 
                            //    row.addClass("aptoTotal");
                            //else 
                            //    row.addClass("noaptoTotal");
                        }

                        //debugger;
                        //alert(this.tbody.find("tr.k-master-row").first());
                        //this.expandRow(this.tbody.find("tr.k-master-row").first());
                    },
                    columns: [
                        {
                            title: '',
                            width: "30px",
                            template: function (item) {
                                return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                            }
                        },
                        {
                            field: "Nombre",
                            title: "NOMBRE",
                            width: "150px",
                            editable: true,
                            template: function (item) {
                                return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                            }
                        },
                        {
                            field: "AptoDDJJ",
                            title: "CUMPLE REQUISITOS MÍNIMOS",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleDDJJ.Nombre#"
                        },
                        {
                            title: "BONIFICACIONES",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "BonifFFAA",
                                    title: "FFAA",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleFFAA.Nombre#"
                                },
                                {
                                    field: "BonifDiscapacidad",
                                    title: "DISCAPACIDAD",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDropDownEditor,
                                    template: "#=CumpleDiscapacidad.Nombre#"
                                },
                                {
                                    field: "BonifDeporte",
                                    title: "DEPORTISTA<br>CALIFICADO",
                                    attributes: { style: "text-align:center" },
                                    width: "50px",
                                    editor: controlador.CumpleDeportistaDropDownEditor,
                                    template: "#=CumpleDeportista.Nombre#"
                                }
                            ]
                        },
                        {
                            field: "PuntajeTotal",
                            title: "PUNTAJE<br>TOTAL",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            editable: true
                        },
                        {
                            field: "AptoTotal",
                            title: "APTO /<br>NO APTO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: function (item) {
                                return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : '--'));
                            },
                            editable: true
                        },
                        //{
                        //    title: "EXAMEN DE CONOCIMIENTO",
                        //    attributes: { style: "text-align:center" },
                        //    columns: [
                        //        {
                        //            field: "FechaConocimiento",
                        //            title: "FECHA",
                        //            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                        //            editor: controlador.FechaConocimientoEditor,
                        //            width: "100px"
                        //            //template: "#=CumpleFormacion.Nombre#"
                        //        },
                        //        {
                        //            field: "HoraConocimiento",
                        //            title: "HORA",
                        //            attributes: { style: "text-align:center" },
                        //            editor: controlador.HoraConocimientoEditor,
                        //            width: "100px"
                        //            //editor: controlador.Bonifica3DropDownEditor,
                        //            //template: "#=CumpleBonifica3.Nombre#"
                        //        }
                        //    ],
                        //    hidden: (res.IdTieneExamenConoc == 0)
                        //},
                        {
                            title: "ENTREVISTA",
                            attributes: { style: "text-align:center" },
                            columns: [
                                {
                                    field: "FechaEntrevista",
                                    title: "FECHA",
                                    attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                                    editor: controlador.FechaEntrevistaEditor,
                                    format: "{0: dd/MM/yyyy}",
                                    width: "100px"
                                    //template: "#=CumpleFormacion.Nombre#"
                                },
                                {
                                    field: "HoraEntrevista",
                                    title: "HORA",
                                    attributes: { style: "text-align:center" },
                                    editor: controlador.HoraEntrevistaEditor,
                                    format: "{0: HH:mm}",
                                    width: "100px"
                                    //editor: controlador.Bonifica3DropDownEditor,
                                    //template: "#=CumpleBonifica3.Nombre#"
                                }
                            ]//,
                            //hidden: (res.IdTieneExamenConoc == 1)
                        }
                    ],
                    editable: true
                }).data();
            },
            error: function (res) {
                debugger;
            }
        });

    };


    this.EvaluacionJS.prototype.abrirModalVisor = function (idPostulacion, idDetalle, idTipo) {
        var modal = $('#divModalVisor').data('kendoWindow');
        //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        modal.title("Ver documento");

        debugger;
        //var options = {
        //    width: "80rem",
        //    height: "100rem"
        //};
        var data_param = new FormData();
        data_param.append('IdPostulacion', idPostulacion);
        data_param.append('IdDetalle', idDetalle);
        data_param.append('IdTipo', idTipo);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Postulante/DescargarArchivoPostulacion',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                if (res.success == "True") {
                    var options = {
                        //height: "780px",
                        pdfOpenParams: { view: 'FitH', page: '1', pagemode: 'none' }
                    };
                    PDFObject.embed(res.responseText, "#app", options);
                }

                modal.center().open();
            },
            error: function (res) {

            }
        });

        //initPDFViewer("https://cdn.www.gob.pe/uploads/document/file/575243/RD_050-2020-MIDISP65-DE.pdf");
        //initPDFViewer("http://localhost:36205/temp/RM_076_2020MIDIS.pdf"); //575243/RD_050-2020-MIDISP65-DE
        //debugger;
        //modal.open();
    }

    this.EvaluacionJS.prototype.cerrarModalRegistroConvocatoria = function () {
        var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
        modal.close();
    }

    this.EvaluacionJS.prototype.CargarFormularioTrabajador = function (id) {
        var data_param = new FormData();
        data_param.append('IdEmpleado', id);
        
        if (($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) || ($("#hdPerfil").val() == PERFIL_NOMINA_CONTABILIDAD)) {
            if ($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) {
                $('#tab1').removeClass('in active');
                $('#tab2').removeClass('in active');
                $('#tab4').removeClass('in active');
                $('#tab3').addClass('in active');
                $('#liTab1').removeClass('active');
                $('#liTab2').removeClass('active');
                $('#liTab3').addClass('active');
            }
            if ($("#hdPerfil").val() == PERFIL_NOMINA_CONTABILIDAD) {
                $('#tab1').removeClass('in active');
                $('#tab2').removeClass('in active');
                $('#tab3').removeClass('in active');
                $('#tab4').addClass('in active');
                $('#liTab1').removeClass('active');
                $('#liTab2').removeClass('active');
                $('#liTab4').addClass('active');
            }
            $('#ddlPersonaTipoDeDocumento').data("kendoDropDownList").readonly();
            $('#txtPersonaNumeroDeDocumento').prop('readonly', true);
            $('#btnBuscarP').prop('disabled', true);
            $('#ddlPersonaSexo').data("kendoDropDownList").readonly();
            $('#ddlPersonaEstadoCivil').data("kendoDropDownList").readonly();
            $('#ddlPersonaGrupoSan').data("kendoDropDownList").readonly();
            //$('#ddlPersonaEstado').data("kendoDropDownList").readonly();
            $('#ddlPersonaCondicion').data("kendoDropDownList").readonly();
            $('#ddlPersonaSede').data("kendoDropDownList").readonly();
            $('#txtPersonaFechaNacimiento').data("kendoDatePicker").readonly();
            $('#txtPersonaFechaInicioLabores').data("kendoDatePicker").readonly();
            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
            $('#ddlPersonaDependencia').data("kendoDropDownList").readonly();
            $('#txtPersonaCargo').prop('readonly', true);
            $('#txtPersonaRUC').prop('readonly', true);

            $('#txtPersonaCorreoElectronicoP').prop('readonly', true);
            $('#txtTelefonoCelularP').prop('readonly', true);
            $('#txtTelefonoFijoP').prop('readonly', true);
            $('#txtPersonaCorreoElectronico').prop('readonly', false);
            $('#txtTelefonoCelular').prop('readonly', false);
            $('#txtTelefonoLaboral').prop('readonly', false);
            $('#txtTelefonoAnexo').prop('readonly', false);
        }
        else {
            $('#tab2').removeClass('in active');
            $('#tab3').removeClass('in active');
            $('#tab4').removeClass('in active');
            $('#tab1').addClass('in active');
            $('#liTab2').removeClass('active');
            $('#liTab3').removeClass('active');
            $('#liTab4').removeClass('active');
            $('#liTab1').addClass('active');
            $('#ddlPersonaTipoDeDocumento').data("kendoDropDownList").enable(true);
            $('#txtPersonaNumeroDeDocumento').prop('readonly', false);
            $('#btnBuscarP').prop('disabled', false);
            $('#ddlPersonaSexo').data("kendoDropDownList").enable(true);
            $('#ddlPersonaEstadoCivil').data("kendoDropDownList").enable(true);
            $('#ddlPersonaGrupoSan').data("kendoDropDownList").enable(true);
            $('#ddlPersonaEstado').data("kendoDropDownList").enable(true);
            $('#ddlPersonaCondicion').data("kendoDropDownList").enable(true);
            $('#ddlPersonaSede').data("kendoDropDownList").enable(true);
            $('#txtPersonaFechaNacimiento').data("kendoDatePicker").enable(true);
            $('#txtPersonaFechaInicioLabores').data("kendoDatePicker").enable(true);
            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
            $('#ddlPersonaDependencia').data("kendoDropDownList").enable(true);
            $('#txtPersonaCargo').prop('readonly', false);
            $('#txtRUC').prop('readonly', false);

            $('#txtPersonaCorreoElectronicoP').prop('readonly', false);
            $('#txtTelefonoCelularP').prop('readonly', false);
            $('#txtTelefonoFijoP').prop('readonly', false);
            $('#txtPersonaCorreoElectronico').prop('readonly', true);
            $('#txtTelefonoCelular').prop('readonly', true);
            $('#txtTelefonoLaboral').prop('readonly', true);
            $('#txtTelefonoAnexo').prop('readonly', true);

            if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO)
                $("#txtPersonaCargo").removeAttr("required");
            else {
                $("#txtPersonaCargo").attr("required", true);
                $("#txtPersonaCargo").attr("validationmessage", "requerido");
            }
        }

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Empleado/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value(res.TipoDocumento);
                $("#txtPersonaNumeroDeDocumento").val(res.NroDocumento);
                $("#txtPersonaNombres").val(res.Nombre);
                $("#txtPersonaApellidoPaterno").val(res.Paterno);
                $("#txtPersonaApellidoMaterno").val(res.Materno);
                $("#ddlPersonaSexo").data("kendoDropDownList").value(res.IdGenero);
                $("#ddlPersonaEstadoCivil").data("kendoDropDownList").value(res.IdEstadoCivil);
                $("#ddlPersonaGrupoSan").data("kendoDropDownList").value(res.IdGrupoSanguineo);
                $("#txtPersonaDireccionDomicilio").val(res.Domicilio);
                $("#txtPersonaUbigeo").val(res.DescripcionUbigeo);
                $("#txtPersonaCorreoElectronicoP").val(res.CorreoElectronico);
                $("#txtTelefonoCelularP").val(res.Celular);
                $("#txtTelefonoFijoP").val(res.Telefono);
                $("#txtPersonaCorreoElectronico").val(res.CorreoElectronicoLaboral);
                $("#txtTelefonoCelular").val(res.CelularLaboral);
                $("#txtTelefonoLaboral").val(res.TelefonoLaboral);
                $("#txtTelefonoAnexo").val(res.AnexoLaboral);
                $("#ddlPersonaEstado").data("kendoDropDownList").value(res.Estado);
                $("#ddlPersonaCondicion").data("kendoDropDownList").value(res.IdCondicion);

                if (res.Estado == 1) {
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
                    $("#txtPersonaFechaFinLabores").removeAttr("required");
                    $("#txtPersonaFechaFinLabores").val('');
                }//$("#divCese").hide();
                if (res.Estado == 0) {
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
                    $("#txtPersonaFechaFinLabores").attr("required", true);
                    $("#txtPersonaFechaFinLabores").attr("validationmessage", "requerido");
                    $("#txtPersonaFechaFinLabores").data("kendoDatePicker").value(res.FechaCese);
                }//$("#divCese").show();

                if (res.IdCondicion == '5') {
                    $("#divOrdenes").show();
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
                    $("#txtPersonaCargo").removeAttr("required");
                    $("#txtRUC").attr("required", true);
                }
                else {
                    $("#divOrdenes").hide();
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
                    $("#txtPersonaCargo").attr("required", true);
                    $("#txtRUC").removeAttr("required");
                } 

                $("#ddlPersonaSede").data("kendoDropDownList").value(res.IdSede);
                $("#ddlPersonaDependencia").data("kendoDropDownList").value(res.IdDependencia);
                $("#txtPersonaCargo").val(res.NombreCargo);
                $("#txtRUC").val(res.RUC);
                $("#hdIdUbigeoEmpleado").val(res.Ubigeo);
                
                $("#txtPersonaFechaNacimiento").data("kendoDatePicker").value(kendo.parseDate(res.FechaNacimiento));
                $("#txtPersonaFechaInicioLabores").data("kendoDatePicker").value(res.FechaInicio);
                
                if (res.Foto != '') {
                    document.getElementById('imgFoto').setAttribute('src', 'data:image/png;base64,' + res.Foto);
                    $('#hdFoto').val(res.Foto);
                }
                else {
                    if (($("#hdPerfil").val() != PERFIL_NOMINA_CONTACTO) && ($("#hdPerfil").val() != PERFIL_NOMINA_CONTABILIDAD)) 
                        $("#btnBuscarFoto").show();
                }   
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.EvaluacionJS.prototype.abrirModalFormato = function (id, postulante, examen, presento) {
        $("#fileFormato").data("kendoUpload").clearAllFiles();
        $('#hdIdEvaluacion').val(id);
        $('#hdIdExamen').val(examen);
        $('#hdIdPresento').val(presento);

        var modal = $('#divModalFormato').data('kendoWindow');
        
        modal.open().center();
    }
    this.EvaluacionJS.prototype.abrirModalEntrevistaPracticas = function (id, postulante) {
        controladorApp.abrirMensajeDeConfirmacion2(
            '<span>¿El postulante se presentó a la entrevista personal y se puede iniciar la evaluación de entrevista personal?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al presionar NO la evaluación finaliza y el postulante se registrará como <strong>NO SE PRESENTO</strong></div>', 'SI', 'NO'
            , function () {
                $('#hdIdEvaluacion').val(id);
                //$('#txtPostulanteSE').val(postulante);
                $("input[type=radio][name=optP1SE]").prop("checked", false);
                $("input[type=radio][name=optP2SE]").prop("checked", false);
                $("input[type=radio][name=optP3SE]").prop("checked", false);
                $("input[type=radio][name=optP4SE]").prop("checked", false);
                //$("input[type=radio][name=optP5SE]").prop("checked", false);
                $('#txtP1SE').val(0);
                $('#txtP2SE').val(0);
                $('#txtP3SE').val(0);
                $('#txtP4SE').val(0);
                //$('#txtP5SE').val(0);
                $('#txtTotalSE').val(0);
                $('#txtObsSE').val('');

                var modal = $('#divModalEntrevistaSE').data('kendoWindow');
                modal.title("Realizar entrevista personal: " + postulante);

                controlador.IniciarPreguntasMaestrasPracticas(id);
                
                modal.open().center();
            }, function () {
                //NO SE PRESENTO
                var data_param = new FormData();
                data_param.append('IdEvaluacion', id);

                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de finalizar la evaluación personal debido a que el postulante NO SE PRESENTÓ?', 'SI', 'NO'
                    , function () {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionPracticaNSP',
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
                                    controladorApp.notificarMensajeSatisfactorio("Evaluación ingresada correctamente");

                                    controlador.inicializarGridEntrevistasPractica();
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                    }, null);
            }
        );
    }
    this.EvaluacionJS.prototype.abrirModalEntrevistaSE = function (id, postulante) {
        controladorApp.abrirMensajeDeConfirmacion2(
            '<span>¿El postulante se presentó a la entrevista personal y se puede iniciar la evaluación de entrevista personal?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al presionar NO la evaluación finaliza y el postulante se registrará como <strong>NO SE PRESENTO</strong></div>', 'SI', 'NO'
            , function () {
                $('#hdIdEvaluacion').val(id);
                //$('#txtPostulanteSE').val(postulante);
                $("input[type=radio][name=optP1SE]").prop("checked", false);
                $("input[type=radio][name=optP2SE]").prop("checked", false);
                $("input[type=radio][name=optP3SE]").prop("checked", false);
                $("input[type=radio][name=optP4SE]").prop("checked", false);
                $("input[type=radio][name=optP5SE]").prop("checked", false);
                $('#txtP1SE').val(0);
                $('#txtP2SE').val(0);
                $('#txtP3SE').val(0);
                $('#txtP4SE').val(0);
                $('#txtP5SE').val(0);
                $('#txtTotalSE').val(0);
                $('#txtObsSE').val('');

                var modal = $('#divModalEntrevistaSE').data('kendoWindow');
                modal.title("Realizar entrevista personal: " + postulante);

                //controlador.CargarFormularioPreguntasSE(id);
                controlador.IniciarPreguntasMaestrasSE(id);
                modal.open().center();
            }, function () {
                //NO SE PRESENTO
                var data_param = new FormData();
                data_param.append('IdEvaluacion', id);

                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de finalizar la evaluación personal debido a que el postulante NO SE PRESENTÓ?', 'SI', 'NO'
                    , function () {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionNSP',
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
                                    controladorApp.notificarMensajeSatisfactorio("Evaluación ingresada correctamente");

                                    controlador.inicializarGridEntrevistas();
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                    }, null);
            }
        );
    }
    this.EvaluacionJS.prototype.abrirModalEntrevistaCE = function (id, postulante) {
        controladorApp.abrirMensajeDeConfirmacion2(
            '<span>¿El postulante se presentó a la entrevista personal y se puede iniciar la evaluación de entrevista personal?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al presionar NO la evaluación finaliza y el postulante se registrará como <strong>NO SE PRESENTO</strong></div>', 'SI', 'NO'
            , function () {
                $('#hdIdEvaluacion').val(id);
                //$('#txtPostulanteCE').val(postulante);
                $("input[type=radio][name=optP1CE]").prop("checked", false);
                $("input[type=radio][name=optP2CE]").prop("checked", false);
                $("input[type=radio][name=optP3CE]").prop("checked", false);
                $("input[type=radio][name=optP4CE]").prop("checked", false);
                $("input[type=radio][name=optP5CE]").prop("checked", false);
                $('#txtP1CE').val(0);
                $('#txtP2CE').val(0);
                $('#txtP3CE').val(0);
                $('#txtP4CE').val(0);
                $('#txtP5CE').val(0);
                $('#txtTotalCE').val(0);
                $('#txtObsCE').val('');

                var modal = $('#divModalEntrevistaCE').data('kendoWindow');
                modal.title("Realizar entrevista personal: " + postulante);

                //controlador.CargarFormularioPreguntasCE(id);
                controlador.IniciarPreguntasMaestrasCE(id);
                modal.open().center();

            },  function () {
                //NO SE PRESENTO
                var data_param = new FormData();
                data_param.append('IdEvaluacion', id);

                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de finalizar la evaluación personal debido a que el postulante NO SE PRESENTÓ?', 'SI', 'NO'
                    , function () {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionNSP',
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
                                    controladorApp.notificarMensajeSatisfactorio("Evaluación ingresada correctamente");

                                    controlador.inicializarGridEntrevistas();
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                    }, null);
            }
        );
    }
    this.EvaluacionJS.prototype.CargarFormularioPreguntasCE = function (id) {
        this.$dataSourcePreguntasCE = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarEntrevistaPersonalPreguntas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/RegistrarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdEvaluacion = id;
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
                            data_param.IdEvaluacion = $("#hdIdEvaluacion").val();
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            //}
                            break;
                        case "update":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            break;
                        case "destroy":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridPreguntasCE').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdPregunta",
                    fields: {
                        IdPregunta: { editable: false, nullable: true },
                        Descripcion: { validation: { required: true }, maxlength: 490 }
                    }
                }
            },
        });

        this.divGridPreguntas = $("#divGridPreguntasCE").kendoGrid({
            toolbar: ["create"], //"excel", 
            //excel: {
            //    fileName: "Listado de preguntas realizadas.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourcePreguntasCE,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Descripcion",
                    title: "Pregunta",
                    width: "600px"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
    }
    this.EvaluacionJS.prototype.inicializarGridPreguntasMaestras = function () {
        
        this.$dataSourcePreguntaMaestra = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarEntrevistaPreguntasMaestras',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/RegistrarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdConvocatoria = $('#hdIdConvocatoria').val();
                            data_param.IdTrabajador = $('#hdIdTrabajador').val();
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
                            data_param.IdConvocatoria = $('#hdIdConvocatoria').val();
                            data_param.IdTrabajador = $('#hdIdTrabajador').val();
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            //}
                            break;
                        case "update":
                            data_param.IdPreguntaMaestra = $options.IdPreguntaMaestra;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            break;
                        case "destroy":
                            data_param.IdPreguntaMaestra = $options.IdPreguntaMaestra;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridPreguntaMaestra').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdPreguntaMaestra",
                    fields: {
                        IdPreguntaMaestra: { editable: false, nullable: true },
                        Descripcion: { validation: { required: true }, maxlength: 490 }
                    }
                }
            },
        });

        this.divGridPreguntaMaestra = $("#divGridPreguntaMaestra").kendoGrid({
            toolbar: ["create"], //"excel", 
            //excel: {
            //    fileName: "Listado de preguntas realizadas.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourcePreguntaMaestra,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Descripcion",
                    title: "Pregunta",
                    width: "600px"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
    }
    this.EvaluacionJS.prototype.inicializarGridPreguntasMaestrasPractica = function () {

        this.$dataSourcePreguntaMaestra = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Practicas/ListarEntrevistaPreguntasMaestras',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/ActualizarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/ActualizarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/RegistrarPreguntaMaestra",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdConvocatoria = $('#hdIdConvocatoria').val();
                            data_param.IdTrabajador = $('#hdIdTrabajador').val();
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
                            data_param.IdConvocatoria = $('#hdIdConvocatoria').val();
                            data_param.IdTrabajador = $('#hdIdTrabajador').val();
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            //}
                            break;
                        case "update":
                            data_param.IdPreguntaMaestra = $options.IdPreguntaMaestra;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            break;
                        case "destroy":
                            data_param.IdPreguntaMaestra = $options.IdPreguntaMaestra;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridPreguntaMaestra').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdPreguntaMaestra",
                    fields: {
                        IdPreguntaMaestra: { editable: false, nullable: true },
                        Descripcion: { validation: { required: true }, maxlength: 490 }
                    }
                }
            },
        });

        this.divGridPreguntaMaestra = $("#divGridPreguntaMaestra").kendoGrid({
            toolbar: ["create"], //"excel", 
            //excel: {
            //    fileName: "Listado de preguntas realizadas.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourcePreguntaMaestra,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Descripcion",
                    title: "Pregunta",
                    width: "600px"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
    }
    this.EvaluacionJS.prototype.CargarFormularioPreguntasSE = function (id) {
        this.$dataSourcePreguntasSE = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarEntrevistaPersonalPreguntas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Convocatoria/RegistrarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdEvaluacion = id;
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
                            data_param.IdEvaluacion = $("#hdIdEvaluacion").val();
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            //}
                            break;
                        case "update":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            break;
                        case "destroy":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridPreguntasSE').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdPregunta",
                    fields: {
                        IdPregunta: { editable: false, nullable: true },
                        Descripcion: { validation: { required: true }, maxlength: 490 }
                    }
                }
            },
        });

        this.divGridPreguntas = $("#divGridPreguntasSE").kendoGrid({
            toolbar: ["create"], //"excel", 
            //excel: {
            //    fileName: "Listado de preguntas realizadas.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourcePreguntasSE,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Descripcion",
                    title: "Pregunta",
                    width: "600px"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
    }
    this.EvaluacionJS.prototype.CargarFormularioPreguntasPracticas = function (id) {
        this.$dataSourcePreguntasSE = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Practicas/ListarEntrevistaPersonalPreguntas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/ActualizarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Practicas/RegistrarEntrevistaPersonalPregunta",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdEvaluacion = id;
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
                            data_param.IdEvaluacion = $("#hdIdEvaluacion").val();
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            //}
                            break;
                        case "update":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 1;
                            break;
                        case "destroy":
                            data_param.IdPregunta = $options.IdPregunta;
                            data_param.Descripcion = $options.Descripcion;
                            data_param.Estado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridPreguntasSE').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                total: function (response) {
                    var TotalDeRegistros = 0;
                    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return TotalDeRegistros;
                },
                model: {
                    id: "IdPregunta",
                    fields: {
                        IdPregunta: { editable: false, nullable: true },
                        Posicion: { editable: false },
                        Descripcion: { validation: { required: true }, maxlength: 490 }
                    }
                }
            },
        });

        this.divGridPreguntas = $("#divGridPreguntasSE").kendoGrid({
            toolbar: ["create"], //"excel", 
            //excel: {
            //    fileName: "Listado de preguntas realizadas.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourcePreguntasSE,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Posicion",
                    title: "Nro",
                    attributes: { style: "text-align:center" },
                    width: "30px"
                },
                {
                    field: "Descripcion",
                    title: "Pregunta",
                    width: "600px"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
    }
    this.EvaluacionJS.prototype.IniciarPreguntasMaestrasPracticas = function (id)   {
        var data_param = new FormData();
        data_param.append('IdEvaluacion', id);
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTrabajador', $('#hdIdTrabajador').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Practicas/IniciarEntrevistaPreguntasMaestras',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                controlador.CargarFormularioPreguntasPracticas(id);

            },
            error: function (res) {
                debugger;
            }
        });
    }
    this.EvaluacionJS.prototype.IniciarPreguntasMaestrasSE = function (id) {
        var data_param = new FormData();
        data_param.append('IdEvaluacion', id);
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTrabajador', $('#hdIdTrabajador').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/IniciarEntrevistaPreguntasMaestras',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                controlador.CargarFormularioPreguntasSE(id);

            },
            error: function (res) {
                debugger;
            }
        });
    }
    this.EvaluacionJS.prototype.IniciarPreguntasMaestrasCE = function (id) {
        var data_param = new FormData();
        data_param.append('IdEvaluacion', id);
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTrabajador', $('#hdIdTrabajador').val());
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/IniciarEntrevistaPreguntasMaestras',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                controlador.CargarFormularioPreguntasCE(id);

            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.EvaluacionJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    } 
    this.EvaluacionJS.prototype.cerrarModalEntrevistaSE = function () {
        $('#divModalEntrevistaSE').data('kendoWindow').close();
    }
    this.EvaluacionJS.prototype.cerrarModalEntrevistaCE = function () {
        $('#divModalEntrevistaCE').data('kendoWindow').close();
    }
    this.EvaluacionJS.prototype.cerrarModalVisor = function () {
        $('#divModalVisor').data('kendoWindow').close();
    }
    this.EvaluacionJS.prototype.agregarEvaluacionCurricularArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTipoDocumento', 10);
        data_param.append('NombreDocumento', $("#txtEnlaceReunion").val());

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el acta de evaluación curricular firmado (Formato PDF)');
            return false;
        } else {
            for (var j = 0; j < firmas.length; j++) {
                if (firmas[0].extension.toLowerCase() == '.pdf')
                    data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                else {
                    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                    return false;
                }
            }
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Desea finalizar la evaluación curricular?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/RegistrarConvocatoriaDocumento',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') {
                            $("#console").append(res.responseText);
                            controladorApp.notificarMensajeDeAlerta("El ingreso del documento de evaluación curricular no se pudo realizar");
                        }
                        else {
                            controladorApp.notificarMensajeSatisfactorio("El ingreso del documento de evaluación curricular se actualizó de forma correcta");
                            modal.close();

                            window.close();
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }
    this.EvaluacionJS.prototype.agregarEvaluacionCurricularPracticaArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTipoDocumento', 10);
        data_param.append('NombreDocumento', $("#txtEnlaceReunion").val());

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el acta de evaluación curricular firmado (Formato PDF)');
            return false;
        } else {
            for (var j = 0; j < firmas.length; j++) {
                if (firmas[0].extension.toLowerCase() == '.pdf')
                    data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                else {
                    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                    return false;
                }
            }
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Desea finalizar la evaluación curricular?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/RegistrarConvocatoriaPracticaDocumento',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') {
                            $("#console").append(res.responseText);
                            controladorApp.notificarMensajeDeAlerta("El ingreso del documento de evaluación curricular no se pudo realizar");
                        }
                        else {
                            controladorApp.notificarMensajeSatisfactorio("El ingreso del documento de evaluación curricular se actualizó de forma correcta");
                            modal.close();

                            window.close();
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }
    this.EvaluacionJS.prototype.finalizarEvaluacionCurricular = function () {
        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de finalizar la evaluación curricular?', 'SI', 'NO'
            , function () {
                window.close();

                return false;
            }, null);
    }
    this.EvaluacionJS.prototype.finalizarEvaluacionConocimientos = function () {
        //controladorApp.notificarMensajeSatisfactorio("Trabajador registrado correctamente");

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de finalizar la evaluación de conocimientos?', 'SI', 'NO'
            , function () {
                window.close();

                return false;
            }, null);
    }
    this.EvaluacionJS.prototype.finalizarEvaluacionEntrevista = function () {
        //controladorApp.notificarMensajeSatisfactorio("Trabajador registrado correctamente");

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de finalizar la evaluación de entrevista personal?', 'SI', 'NO'
            , function () {
                window.close();

                return false;
            }, null);
    }
    this.EvaluacionJS.prototype.agregarEvaluacionEntrevistaArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdEvaluacion', $('#hdIdEvaluacion').val());
        //data_param.append('IdTipoDocumento', 10);

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el formato de entrevista personal firmado (Formato PDF)');
            return false;
        } else {
            for (var j = 0; j < firmas.length; j++) {
                if (firmas[0].extension.toLowerCase() == '.pdf')
                    data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                else {
                    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                    return false;
                }
            }
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Desea finalizar la entrevista personal del postulante seleccionado?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarActaEntrevistaPersonal',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') {
                            $("#console").append(res.responseText);
                            controladorApp.notificarMensajeDeAlerta("El ingreso del documento de entrevista personal no se pudo realizar");
                        }
                        else {
                            controladorApp.notificarMensajeSatisfactorio("El ingreso del documento de entrevista personal se actualizó de forma correcta");
                            modal.close();

                            //this.inicializarEntrevistas();
                            var grilla = $('#divGrid').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);

                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }
    this.EvaluacionJS.prototype.agregarEvaluacionEntrevistaPracticaArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdEvaluacion', $('#hdIdEvaluacion').val());
        //data_param.append('IdTipoDocumento', 10);

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el formato de entrevista personal firmado (Formato PDF)');
            return false;
        } else {
            for (var j = 0; j < firmas.length; j++) {
                if (firmas[0].extension.toLowerCase() == '.pdf')
                    data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                else {
                    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                    return false;
                }
            }
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Desea finalizar la entrevista personal del postulante seleccionado?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Practicas/ActualizarActaEntrevistaPersonal',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') {
                            $("#console").append(res.responseText);
                            controladorApp.notificarMensajeDeAlerta("El ingreso del documento de entrevista personal no se pudo realizar");
                        }
                        else {
                            controladorApp.notificarMensajeSatisfactorio("El ingreso del documento de entrevista personal se actualizó de forma correcta");
                            modal.close();

                            //this.inicializarEntrevistas();
                            var grilla = $('#divGrid').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);

                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }

    this.EvaluacionJS.prototype.GenerarFormatoPerfil = function () {
        var IdConvocatoria = $('#hdIdConvocatoria').val();
        var IdTrabajador = $('#hdIdTrabajador').val();
        
        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Convocatoria/DescargarAnexo07?id=" + IdConvocatoria + "&idTra=" + IdTrabajador, '_blank');
    }
    this.EvaluacionJS.prototype.GenerarFormatoPerfilPractica = function () {
        var IdConvocatoria = $('#hdIdConvocatoria').val();
        var IdTrabajador = $('#hdIdTrabajador').val();

        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Convocatoria/DescargarAnexo07Practica?id=" + IdConvocatoria + "&idTra=" + IdTrabajador, '_blank');
    }
    this.EvaluacionJS.prototype.GenerarFormatoEntrevista = function () {
        var IdEvaluacion = $('#hdIdEvaluacion').val();
        var IdTrabajador = $('#hdIdTrabajador').val();
        var IdExamen = $('#hdIdExamen').val();
        var IdPresento = $('#hdIdPresento').val();

        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Convocatoria/DescargarFormatoEntrevista?id=" + IdEvaluacion + "&idTra=" + IdTrabajador + "&idEx=" + IdExamen + "&idPre=" + IdPresento, '_blank');
    }
    this.EvaluacionJS.prototype.GenerarFormatoEntrevistaPractica = function () {
        var IdEvaluacion = $('#hdIdEvaluacion').val();
        var IdTrabajador = $('#hdIdTrabajador').val();
        var IdExamen = $('#hdIdExamen').val();
        var IdPresento = $('#hdIdPresento').val();

        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Practicas/DescargarFormatoEntrevista?id=" + IdEvaluacion + "&idTra=" + IdTrabajador + "&idEx=" + IdExamen + "&idPre=" + IdPresento, '_blank');
    }
    this.EvaluacionJS.prototype.FirmarFormatoEntrevista = function () {
        var IdEvaluacion = $('#hdIdEvaluacion').val();
        var IdTrabajador = $('#hdIdTrabajador').val();
        var IdExamen = $('#hdIdExamen').val();
        var IdPresento = $('#hdIdPresento').val();

        
        controladorApp.abrirMensajeDeConfirmacion('¿Está seguro de realizar la firma de la entrevista seleccionada?', 'SI', 'NO'
            , function (arg) {
                initInvoker('E');

            });
    }

    this.EvaluacionJS.prototype.NombreOrdenEditor = function (container, options) {
        $('<textarea required name="' + options.field + '" style="width: ' + (container.width() + 30) + 'px;height:' + (container.height() + 30) + 'px" />')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.FechaInicioEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaInicio">')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.FechaFinEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaFin">')
            .appendTo(container);
    };
    this.EvaluacionJS.prototype.agregarConvocatoria = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        
        if (frmPersonaValidador.validate()) {
            var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();
            
            data_param.append('IdBase', $("#ddlBases").data("kendoDropDownList").value());
            data_param.append('IdPerfil', $("#hdIdPerfil").val());
            data_param.append('IdDependencia', $("#ddlDependencia").data("kendoDropDownList").value());
            data_param.append('NroAIRHSP', $("#txtAIRHSPConvocatoria").val());
            data_param.append('Meta', $("#txtMetaConvocatoria").val());
            //data_param.append('ResponsableCurricular', $("#ddlResponsableCurricular").data("kendoDropDownList").value());
            data_param.append('Estado', 1);
            
            var existeFile = false;
            var upload1 = $("#fileDocCertificacion").getKendoUpload();
            var file1 = upload1.getFiles();
            if (file1.length == 0) {
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el documento de sustento de la certificación (Formato PDF)');
                return false;
            } else {
                if (file1.length == 0) {
                    data_param.append('formatos[0]', null);
                }
                else {
                    if (file1[0].extension.toLowerCase() == '.pdf') {
                        existeFile = true;
                        data_param.append('formatos[0]', file1[0].rawFile);
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                        return false;
                    }
                }
            }
            var upload2 = $("#fileDocRequerimiento").getKendoUpload();
            var file2 = upload2.getFiles();
            if (file2.length == 0) {
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el documento de sustento del requerimiento (Formato PDF)');
                return false;
            } else {
                if (file2.length == 0) {
                    data_param.append('formatos[1]', null);
                }
                else {
                    if (file2[0].extension.toLowerCase() == '.pdf') {
                        existeFile = true;
                        data_param.append('formatos[1]', file2[0].rawFile);
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                        return false;
                    }
                }
            }
            var upload3 = $("#fileDocComite").getKendoUpload();
            var file3 = upload3.getFiles();
            if (file3.length == 0) {
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el documento de sustento de la creación del comité de selección (Formato PDF)');
                return false;
            } else {
                if (file3.length == 0) {
                    data_param.append('formatos[2]', null);
                }
                else {
                    if (file3[0].extension.toLowerCase() == '.pdf') {
                        existeFile = true;
                        data_param.append('formatos[2]', file3[0].rawFile);
                    }
                    else {
                        controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                        return false;
                    }
                }
            }

            data_param.append('IdComiteDependencia1', $("#ddlComiteDependencia1").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro1T', $("#ddlComiteMiembro1T").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro1S', $("#ddlComiteMiembro1S").data("kendoDropDownList").value());
            data_param.append('IdComiteDependencia2', $("#ddlComiteDependencia2").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro2T', $("#ddlComiteMiembro2T").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro2S', $("#ddlComiteMiembro2S").data("kendoDropDownList").value());
            data_param.append('IdComiteDependencia3', $("#ddlComiteDependencia3").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro3T', $("#ddlComiteMiembro3T").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro3S', $("#ddlComiteMiembro3S").data("kendoDropDownList").value());

            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de ingresar la convocatoria?.</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación la convocatoria automaticamente será publicada</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Convocatoria registrado correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                $("#hdIdConvocatoria").val(res.responseText);

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
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
    }

    this.EvaluacionJS.prototype.agregarEvaluacionCurri = function (e) {
        e.preventDefault();

        if (frmEvaluacionCurriValidador.validate()) {
            var modal = $('#divModalEvaluacionCurri').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();

            data_param.append('IdConvocatoria', $("#hdIdConvocatoria").val());
            
            data_param.append('IdComiteDependencia1', $("#ddlComiteDependencia1Curri").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro1T', $("#ddlComiteMiembro1Curri").data("kendoDropDownList").value());
            data_param.append('IdComiteDependencia2', $("#ddlComiteDependencia2Curri").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro2T', $("#ddlComiteMiembro2Curri").data("kendoDropDownList").value());
            data_param.append('IdComiteDependencia3', $("#ddlComiteDependencia3Curri").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro3T', $("#ddlComiteMiembro3Curri").data("kendoDropDownList").value());
            
            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de notificar el inicio de la evaluación curricular a los miembros del comité de seleccionados?.</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación los miembros del comité seleccionados podrán ingresar la evaluación curricular del proceso CAS</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/RegistrarConvocatoriaComiteEvaluacionCurri',
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
                                controladorApp.notificarMensajeSatisfactorio("Se envió correctamente la notificación a los miembros del comité seleccionados");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                $("#hdIdConvocatoria").val('');

                                modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
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
    this.EvaluacionJS.prototype.GenerarFormatoContrato = function () {
        var IdContrato = $('#hdIdContrato').val();

        window.open(controladorApp.obtenerRutaBase() + "Contrato/Ficha?idContrato=" + IdContrato, "_blank");
        $("#btnGenerarFicha").css("display", "");
    }
    this.EvaluacionJS.prototype.agregarContratoArchivo = function (e) {
        e.preventDefault();
        
        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdContrato', $('#hdIdContrato').val());
        
        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el contrato que será enviado al postulante ganador del proceso (Formato PDF)');
            return false;
        } else {
            for (var j = 0; j < firmas.length; j++) {
                if (firmas[0].extension.toLowerCase() == '.pdf')
                    data_param.append('formatos[' + j.toString() + ']', firmas[j].rawFile);
                else {
                    controladorApp.notificarMensajeDeAlerta('Sólo se admite archivo con extensión PDF');
                    return false;
                }
            }
        }

        controladorApp.abrirMensajeDeConfirmacion('¿Desea actualizar el contrato y enviarlo al postulante ganador del proceso para su revisión?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Contrato/RegistrarContratoArchivo',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            debugger;
                            if (res.success == 'False') {
                                $("#console").append(res.responseText);
                                controladorApp.notificarMensajeDeAlerta("La actualización del contrato no se pudo realizar");
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio("El contrato se actualizó de forma correcta");
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
    this.EvaluacionJS.prototype.actualizarEvaluacionObservacion = function (e, idEvaluacion) {
        debugger;
        if (e.previousElementSibling != null) {
            if (e.previousElementSibling.value == '') {
                controladorApp.notificarMensajeDeAlerta('Debe ingresar una observación');
                return false;
            }
            else {
                //alert('Se guardar ' + e.previousElementSibling.value);
                var data_param = new FormData();
                data_param.append('IdEvaluacion', idEvaluacion);
                data_param.append('Observacion', e.previousElementSibling.value);
                
                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de ingresar la observación?', 'SI', 'NO'
                    , function (arg) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionCurriObs',
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
                                    controladorApp.notificarMensajeSatisfactorio("Observación ingresada correctamente");

                                    //LimpiarModalRegistroConvocatoria();
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                    }, data_param);
            }
        }
        
        if (frmPersonaValidador.validate()) {
            
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }
    this.EvaluacionJS.prototype.actualizarEvaluacionPracticaObservacion = function (e, idEvaluacion) {
        debugger;
        if (e.previousElementSibling != null) {
            if (e.previousElementSibling.value == '') {
                controladorApp.notificarMensajeDeAlerta('Debe ingresar una observación');
                return false;
            }
            else {
                //alert('Se guardar ' + e.previousElementSibling.value);
                var data_param = new FormData();
                data_param.append('IdEvaluacion', idEvaluacion);
                data_param.append('Observacion', e.previousElementSibling.value);

                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de ingresar la observación?', 'SI', 'NO'
                    , function (arg) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarPracticaEvaluacionCurriObs',
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
                                    controladorApp.notificarMensajeSatisfactorio("Observación ingresada correctamente");

                                    //LimpiarModalRegistroConvocatoria();
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                    }, data_param);
            }
        }

        if (frmPersonaValidador.validate()) {

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }
    this.EvaluacionJS.prototype.agregarEvaluacionEntrevistaSE = function (e) {
        e.preventDefault();

        var modal = $('#divModalEntrevistaSE').data('kendoWindow');
        var data_param = new FormData();
        data_param.append('IdEvaluacion', $('#hdIdEvaluacion').val());
        data_param.append('PuntajeAspecto1', $('#txtP1SE').val());
        data_param.append('PuntajeAspecto2', $('#txtP2SE').val());
        data_param.append('PuntajeAspecto3', $('#txtP3SE').val());
        data_param.append('PuntajeAspecto4', $('#txtP4SE').val());
        data_param.append('PuntajeAspecto5', $('#txtP5SE').val());
        data_param.append('Observacion', $('#txtObsSE').val());

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de ingresar la evaluación de entrevista personal?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionEntrevistaPersonal',
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
                            controladorApp.notificarMensajeSatisfactorio("Evaluación de entrevista personal ingresada correctamente");

                            modal.close();
                            //this.inicializarEntrevistas();
                            var grilla = $('#divGrid').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);

    }
    this.EvaluacionJS.prototype.agregarEvaluacionEntrevistaCE = function (e) {
        e.preventDefault();

        var modal = $('#divModalEntrevistaCE').data('kendoWindow');
        var data_param = new FormData();
        data_param.append('IdEvaluacion', $('#hdIdEvaluacion').val());
        data_param.append('PuntajeAspecto1', $('#txtP1CE').val());
        data_param.append('PuntajeAspecto2', $('#txtP2CE').val());
        data_param.append('PuntajeAspecto3', $('#txtP3CE').val());
        data_param.append('PuntajeAspecto4', $('#txtP4CE').val());
        data_param.append('PuntajeAspecto5', $('#txtP5CE').val());
        data_param.append('Observacion', $('#txtObsCE').val());

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de ingresar la evaluación de entrevista personal?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionEntrevistaPersonal',
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
                            controladorApp.notificarMensajeSatisfactorio("Evaluación de entrevista personal ingresada correctamente");

                            modal.close();
                            //this.inicializarEntrevistas();
                            var grilla = $('#divGrid').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);

        //if (frmPersonaValidador.validate()) {

        //}
        //else {
        //    controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        //}

    }

    this.EvaluacionJS.prototype.agregarEvaluacionEntrevistaPracticas = function (e) {
        e.preventDefault();

        var modal = $('#divModalEntrevistaSE').data('kendoWindow');
        var data_param = new FormData();
        data_param.append('IdEvaluacion', $('#hdIdEvaluacion').val());
        data_param.append('PuntajeAspecto1', $('#txtP1SE').val());
        data_param.append('PuntajeAspecto2', $('#txtP2SE').val());
        data_param.append('PuntajeAspecto3', $('#txtP3SE').val());
        data_param.append('PuntajeAspecto4', $('#txtP4SE').val());
        data_param.append('PuntajeAspecto5', 0);
        data_param.append('Observacion', $('#txtObsSE').val());

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de ingresar la evaluación de entrevista personal?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionPracticaEntrevistaPersonal',
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
                            controladorApp.notificarMensajeSatisfactorio("Evaluación de entrevista personal ingresada correctamente");

                            modal.close();
                            //this.inicializarEntrevistas();
                            var grilla = $('#divGrid').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);

    }

    this.EvaluacionJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    }

    this.EvaluacionJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.EvaluacionJS.prototype.abrirModalEvaluacionCurri = function (IdConvocatoria) {
        //$("#fileFormato").data("kendoUpload").clearAllFiles();
        $('#hdIdConvocatoria').val(IdConvocatoria);
        var data_param = new FormData();
        data_param.append('IdConvocatoria', IdConvocatoria);

        var modal = $('#divModalEvaluacionCurri').data('kendoWindow');
        
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoriaCurri").val(res.NroConvocatoria);
                $("#txtCargoConvocatoriaCurri").val(res.NombreCargo);

                //ListarConvocatoriaComite
                modal.open().center();
            },
            error: function (res) {
                debugger;
            }
        });
        
        $("#ddlComiteMiembro1Curri").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "DescripcionMiembro",
            dataValueField: "IdTrabajador",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 1 }
                ]
            }
        });
        $("#ddlComiteMiembro2Curri").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "DescripcionMiembro",
            dataValueField: "IdTrabajador",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 2 }
                ]
            }
        });
        $("#ddlComiteMiembro3Curri").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "DescripcionMiembro",
            dataValueField: "IdTrabajador",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 3 }
                ]
            }
        });
        $("#ddlComiteDependencia1Curri").kendoDropDownList({
            autoBind: true,
            dataTextField: "NombreDependencia",
            dataValueField: "IdDependencia",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 1 },
                    { field: "IdTitular", operator: "equals", value: 1 }
                ]
            }
        });
        $("#ddlComiteDependencia2Curri").kendoDropDownList({
            autoBind: true,
            dataTextField: "NombreDependencia",
            dataValueField: "IdDependencia",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 2 },
                    { field: "IdTitular", operator: "equals", value: 1 }
                ]
            }
        });
        $("#ddlComiteDependencia3Curri").kendoDropDownList({
            autoBind: true,
            dataTextField: "NombreDependencia",
            dataValueField: "IdDependencia",
            dataSource: {
                //serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaComite",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.IdConvocatoria = IdConvocatoria;

                        return $.toDictionary(data_param);
                    }
                },
                filter: [
                    { field: "IdMiembro", operator: "equals", value: 3 },
                    { field: "IdTitular", operator: "equals", value: 1 }
                ]
            }
        });
    }

    this.EvaluacionJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.EvaluacionJS.prototype.eliminar = function () {
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