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


    this.EntrevistaJS = function () { };
    this.EntrevistaJS.prototype.inicializar = function () {
        
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
        
        this.inicializarGrid();
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

    this.EntrevistaJS.prototype.inicializarGrid = function () {
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

                //ListarConvocatoriaComite
                //modal.open().center();
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
                                                     e.field == "AptoSanciones" )) {
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
                        data_param.append('IdUsuarioModificacion', $("#hdIdTrabajador").val());

                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarEvaluacionCurri',
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
            //toolbar: ["excel", ], 
            //excel: {
            //    fileName: "Listado de Postulantes.xlsx",
            //    filterable: false
            //},
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
                    if (aptototal) 
                        row.addClass("aptoTotal");
                    else 
                        row.addClass("noaptoTotal");
                }

                //debugger;
                //alert(this.tbody.find("tr.k-master-row").first());
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    title: '',
                    width: "30px",
                    template: function (item) {
                        return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                    }
                },
                //{
                //    field: "TipoDocumento",
                //    title: "TIPO DOCUMENTO",
                //    width: "80px",
                //    template: function (item) {
                //        var tipo = '';
                //        if (item.TipoDocumento == '1') tipo = "DNI";
                //        if (item.TipoDocumento == '2') tipo = "CARNET EXTRANJERIA";

                //        return tipo;
                //    }
                //},
                //{ field: "NroDocumento", title: "Nro DOCUMENTO", width: "80px" },
                {
                    field: "Nombre",
                    title: "NOMBRE",
                    width: "100px",
                    editable: true,
                    template: function (item) {
                        return item.Nombre + ' ' + item.Paterno + ' ' + item.Materno;
                    }
                },
                {
                    title: "FORMACIÓN ACADÉMICA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoFormacion",
                            title: "CUMPLE",
                            headerAttributes: {
                                "class": "table-header-cell",
                                style: "text-align:center; background-color: RGB(252, 248, 227)"
                            },
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "PuntajeFormacion",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            //format: "{0:P}",
                            width: "50px",
                            editable: true
                        },
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
                    title: "EXPERIENCIA GENERAL",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoExperienciaGen",
                            title: "CUMPLE",
                            headerAttributes: {
                                "class": "table-header-cell",
                                style: "text-align:center; background-color: RGB(252, 248, 227)"
                            },
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleExperienciaGen.Nombre#"
                        },
                        {
                            field: "PuntajeExperienciaGen",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            //format: "{0:P}",
                            width: "50px",
                            editable: true
                        }//,
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
                    title: "EXPERIENCIA ESPECÍFICA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoExperienciaEsp",
                            title: "CUMPLE",
                            headerAttributes: {
                                "class": "table-header-cell",
                                style: "text-align:center; background-color: RGB(252, 248, 227)"
                            },
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleExperienciaEsp.Nombre#"
                        },
                        {
                            field: "PuntajeExperienciaEsp",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            //format: "{0:P}",
                            width: "50px",
                            editable: true
                        },
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
                    field: "AptoDDJJ",
                    title: "DDJJ DE<br>POSTULACIÓN E<br>INCOMPATIBILIDAD",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    editor: controlador.CumpleDropDownEditor,
                    template: "#=CumpleDDJJ.Nombre#"
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
                    field: "BonifDiscapacidad",
                    title: "BONIFICACIÓN<br>POR<br>DISCAPACIDAD",
                    attributes: { style: "text-align:center" },
                    width: "50px"
                },
                {
                    field: "BonifFFAA",
                    title: "BONIFICACIÓN<br>POR FF.AA.",
                    attributes: { style: "text-align:center" },
                    width: "50px"
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
                    //attributes: function (e) {
                    //    debugger;
                    //    return {
                    //        style: e.AptoTotal == "1" ? "text-align:center; background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px" : "text-align:center; background-color: RGB(252, 248, 227)"
                    //    };
                    //},
                    width: "50px",
                    template: function (item) {
                        var tipo = 'NO';
                        if (item.AptoTotal == '1') tipo = "SI";
                        
                        return tipo;
                    },
                    editable: true
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
                    template: function(item) {
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
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalVisor(' + item.IdPostulacion + ',' +  item.IdLaboral + ',3)">'; //GenerarFormatoContrato
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

    this.EntrevistaJS.prototype.CumpleDropDownEditor = function (container, options) {
        var data = [
                        { Nombre: "--", Codigo: "0" },
                        { Nombre: "SI", Codigo: "1" },
                        { Nombre: "NO", Codigo: "2" }
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
    this.EntrevistaJS.prototype.Bonifica3DropDownEditor = function (container, options) {
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
    this.EntrevistaJS.prototype.Bonifica2DropDownEditor = function (container, options) {
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
    this.EntrevistaJS.prototype.MostarRangoFechas = function (suma) {
        var years = Math.trunc((suma / 365.25));
        var months = Math.trunc((((suma / 365.25) - years) * 12));
        var days = Math.trunc(((((suma / 365.25) - years) * 12) - months) * 365.25 / 12);

        return years + " años, " + " " + months + " meses" + " " + days + " días";
    }
    this.EntrevistaJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.EntrevistaJS.prototype.abrirModalVisor = function (idPostulacion, idDetalle, idTipo) {
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

    this.EntrevistaJS.prototype.cerrarModalRegistroConvocatoria = function () {
        var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
        modal.close();
    }

    this.EntrevistaJS.prototype.CargarFormularioTrabajador = function (id) {
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

    this.EntrevistaJS.prototype.CargarFormularioCuentasBancarias = function (id) {
        this.$dataSourceCuentas = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Empleado/ListarCuentasEmpleado',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/ActualizarCuentaEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/EliminarCuentaEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/RegistrarCuentaEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdEmpleado = id;
                            data_param.Estado = 0;
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
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.IdBanco = $options.Banco.IdBanco;
                            data_param.CCI = $options.CCI;
                            data_param.NroCuenta = '';
                            data_param.IdEstado = $options.Estado.Codigo;
                            //}
                            break;
                        case "update":
                            data_param.IdEmpleadoBanco = $options.IdEmpleadoBanco;
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.IdBanco = $options.Banco.IdBanco;
                            data_param.CCI = $options.CCI;
                            data_param.NroCuenta = '';
                            data_param.IdEstado = $options.Estado.Codigo;
                            break;
                        case "destroy":
                            data_param.IdEmpleadoBanco = $options.IdEmpleadoBanco;
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.IdBanco = $options.Banco.IdBanco;
                            data_param.CCI = $options.CCI;
                            data_param.NroCuenta = '';
                            data_param.IdEstado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCuentas').data("kendoGrid");
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
                    id: "IdEmpleadoBanco",
                    fields: {
                        IdEmpleadoBanco: { editable: false, nullable: true },
                        CCI: { validation: { required: true } },
                        Banco: {
                            validation: {
                                required: true,
                                bancovalidation: function (input) {
                                    if (input.is("[name='Banco']") && input.val() == "") {
                                        input.attr("data-bancovalidation-msg", "Entidad bancaria es requerida");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { IdBanco: 8, Nombre: "Nombre" }
                        },
                        Estado: {
                            validation: {
                                required: true,
                                estadovalidation: function (input) {
                                    if (input.is("[name='Estado']") && input.val() == "") {
                                        input.attr("data-estadovalidation-msg", "El estado es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { Codigo: 1, Nombre: "ACTIVO" }
                        },
                    }
                }
            },
        });

        this.divGridCuentas = $("#divGridCuentas").kendoGrid({
            toolbar: ["excel", "create"],
            excel: {
                fileName: "Listado de cuentas bancarias del trabajador.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceCuentas,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Banco",
                    title: "Entidad Bancaria",
                    width: "200px",
                    editor: controlador.BancoDropDownEditor,
                    template: "#=Banco.Nombre#"
                },
                {
                    field: "CCI",
                    title: "Código de Cuenta Interbancaria (CCI)",
                    attributes: { style: "text-align:left;" },

                    width: "200px"
                },
                {
                    field: "Estado",
                    title: "Estado",
                    width: "50px",
                    editor: controlador.EstadoDropDownEditor,
                    template: "#=Estado.Nombre#"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();
    }

    this.EntrevistaJS.prototype.BancoDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "Nombre",
                dataValueField: "IdBanco",
                optionLabel: "-- Seleccione --",
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Persona/ListarBancos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "Nombre";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };

    this.EntrevistaJS.prototype.EstadoDropDownEditor = function (container, options) {
        var data = [
                        { Nombre: "ACTIVO", Codigo: "1" },
                        { Nombre: "INACTIVO", Codigo: "0" }
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

    this.EntrevistaJS.prototype.CargarFormularioOrdenesServicio = function (id) {
        this.$dataSourceOrdenes = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Empleado/ListarOrdenesEmpleado',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/ActualizarOrdenEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/EliminarOrdenEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Empleado/RegistrarOrdenEmpleado",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },

                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.IdEmpleado = id;
                            data_param.Estado = 0;
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                            data_param.Grilla.PaginaActual = $options.page
                            if ($options !== undefined && $options.sort !== undefined) {
                                data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                                data_param.Grilla.OrdenarPor = $options.sort[0].field;
                            }
                            break;
                        case "create":
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.NroOrden = $options.NroOrden;
                            data_param.NroSIAF = $options.NroSIAF;
                            data_param.Nombre = $options.Nombre.toUpperCase();
                            data_param.Duracion = $options.Duracion;
                            data_param.FechaInicio = $options.FechaInicio;
                            data_param.FechaFin = $options.FechaFin;
                            data_param.Monto = $options.Monto;
                            data_param.IdEstado = $options.Estado.Codigo;
                            break;
                        case "update":
                            data_param.IdEmpleadoOrden = $options.IdEmpleadoOrden;
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.NroOrden = $options.NroOrden;
                            data_param.NroSIAF = $options.NroSIAF;
                            data_param.Nombre = $options.Nombre.toUpperCase();
                            data_param.Duracion = $options.Duracion;
                            data_param.FechaInicio = $options.FechaInicio;
                            data_param.FechaFin = $options.FechaFin;
                            data_param.Monto = $options.Monto;
                            data_param.IdEstado = $options.Estado.Codigo;
                            break;
                        case "destroy":
                            data_param.IdEmpleadoOrden = $options.IdEmpleadoOrden;
                            data_param.IdEmpleado = $("#hdIdEmpleado").val();
                            data_param.NroOrden = $options.NroOrden;
                            data_param.Nombre = $options.Nombre.toUpperCase();
                            data_param.Duracion = $options.Duracion;
                            data_param.FechaInicio = $options.FechaInicio;
                            data_param.FechaFin = $options.FechaFin;
                            data_param.IdEstado = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridOrdenes').data("kendoGrid");
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
                    id: "IdEmpleadoOrden",
                    fields: {
                        IdEmpleadoOrden: { editable: false, nullable: true },
                        NroOrden: { validation: { required: true, maxlength: "15" } },
                        NroSIAF: { validation: { required: true, maxlength: "15" } },
                        Duracion: { validation: { required: true, type: "number" } },
                        Nombre: { validation: { required: true } },
                        FechaInicio: {
                            validation: {
                                required: true,
                                type: "date",
                                format: "dd/MM/yyyy",
                                fechainiciovalidation: function (input) {
                                    if (input.is("[name='FechaInicio']") && input.val() == "") {
                                        input.attr("data-fechainiciovalidation-msg", "requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }
                        },
                        FechaFin: {
                            validation: {
                                required: true,
                                type: "date",
                                format: "dd/MM/yyyy",
                                fechafinvalidation: function (input) {
                                    if (input.is("[name='FechaFin']") && input.val() == "") {
                                        input.attr("data-fechafinvalidation-msg", "requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }
                        },
                        Monto: {
                            validation: {
                                required: true,
                                type: "number",
                                montovalidation: function (input) {
                                    if (input.is("[name='Monto']") && input.val() == "") {
                                        input.attr("data-montovalidation-msg", "requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }
                        },
                        Estado: {
                            validation: {
                                required: true,
                                estadovalidation: function (input) {
                                    if (input.is("[name='Estado']") && input.val() == "") {
                                        input.attr("data-estadovalidation-msg", "El estado es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { Codigo: 1, Nombre: "ACTIVO" }
                        },
                    }
                }
            },
        });

        this.divGridOrdenes = $("#divGridOrdenes").kendoGrid({
            toolbar: ["excel", "create"], //, { text: "Add New Email", className: "k-grid-addEmail", imageClass: "k-add" }, { text: "Add New Cell Text Message", className: "k-grid-addText", imageClass: "k-add" }
            excel: {
                fileName: "Listado de ordenes de servicio del trabajador.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceOrdenes,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            dataBound: function (e) {
                $('.k-grid-add').unbind("click");
                $('.k-grid-add').bind("click", function () {
                    if ($("#hdIdEmpleado").val() == 0) {
                        controladorApp.notificarMensajeDeAlerta('Primero debe completar el registro del empleado');
                        return false;
                    }
                });
            },
            columns: [
                {
                    field: "NroOrden",
                    title: "Nro Orden",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "NroSIAF",
                    title: "Nro SIAF",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "Nombre",
                    title: "Nombre de la Orden de Servicio",
                    attributes: { style: "text-align:left;" },
                    editor: controlador.NombreOrdenEditor,
                    width: "200px"
                },
                {
                    field: "Duracion",
                    title: "Plazo",
                    attributes: { style: "text-align:center;" },

                    width: "30px"
                },
                {
                    field: "FechaInicio",
                    title: "Fecha Inicio",
                    attributes: { style: "text-align:center;" },
                    editor: controlador.FechaInicioEditor,
                    width: "70px"
                },
                {
                    field: "FechaFin",
                    title: "Fecha Fin",
                    attributes: { style: "text-align:center;" },
                    editor: controlador.FechaFinEditor,
                    width: "80px"
                },
                {
                    field: "Monto",
                    title: "Monto",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}",
                    width: "50px"
                },
                {
                    field: "Estado",
                    title: "Estado",
                    width: "30px",
                    editor: controlador.EstadoDropDownEditor,
                    template: "#=Estado.Nombre#"
                },
                { title: "Acciones", command: ["edit", "destroy"], width: "50px" },
            ],
            editable: "inline"
        }).data();
                
    }
     

    this.EntrevistaJS.prototype.NombreOrdenEditor = function (container, options) {
        $('<textarea required name="' + options.field + '" style="width: ' + (container.width() + 30) + 'px;height:' + (container.height() + 30) + 'px" />')
            .appendTo(container);
    };
    this.EntrevistaJS.prototype.FechaInicioEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaInicio">')
            .appendTo(container);
    };
    this.EntrevistaJS.prototype.FechaFinEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaFin">')
            .appendTo(container);
    };
    this.EntrevistaJS.prototype.agregarConvocatoria = function (e) {
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

    this.EntrevistaJS.prototype.agregarEvaluacionCurri = function (e) {
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
    this.EntrevistaJS.prototype.GenerarFormatoContrato = function () {
        var IdContrato = $('#hdIdContrato').val();

        window.open(controladorApp.obtenerRutaBase() + "Contrato/Ficha?idContrato=" + IdContrato, "_blank");
        $("#btnGenerarFicha").css("display", "");
    }
    this.EntrevistaJS.prototype.agregarContratoArchivo = function (e) {
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
    this.EntrevistaJS.prototype.actualizarTrabajadorContacto = function (e) {
        e.preventDefault();
        
        if (frmPersonaValidador.validate()) {
            var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdEmpleado").val() != 0) 
                data_param.append('IdEmpleado', $("#hdIdEmpleado").val());
            
            data_param.append('CorreoElectronico', $("#txtPersonaCorreoElectronicoP").val());
            data_param.append('Celular', $("#txtTelefonoCelularP").val());
            data_param.append('Telefono', $("#txtTelefonoFijoP").val());
            data_param.append('CorreoElectronicoLaboral', $("#txtPersonaCorreoElectronico").val());
            data_param.append('CelularLaboral', $("#txtTelefonoCelular").val());
            data_param.append('TelefonoLaboral', $("#txtTelefonoLaboral").val());
            data_param.append('AnexoLaboral', $("#txtTelefonoAnexo").val());
            
            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de actualizar el trabajador?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Empleado/GuardarContacto',
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
                                if (($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) || ($("#hdPerfil").val() == PERFIL_NOMINA_CONTABILIDAD)) {
                                    modal.close();
                                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                                }
                                else {
                                    controladorApp.notificarMensajeSatisfactorio("Trabajador registrado correctamente");

                                    // REFRESCAR INFORMACION DEL TRABAJADOR
                                    LimpiarModalRegistroConvocatoria();

                                    $("#hdIdEmpleado").val(res.responseText);

                                    $("#_tab4").attr("data-toggle", "tab");
                                    $('#_tab4').prop("onclick", null).off("click");

                                    controlador.CargarFormularioTrabajador(res.responseText);
                                    controlador.CargarFormularioCuentasBancarias(res.responseText);

                                    modal.title("Actualizar Trabajador");
                                    $('#divGrid').data("kendoGrid").dataSource.page(1);
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

    }

    this.EntrevistaJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    }

    this.EntrevistaJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.EntrevistaJS.prototype.abrirModalEvaluacionCurri = function (IdConvocatoria) {
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

    this.EntrevistaJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.EntrevistaJS.prototype.eliminar = function () {
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