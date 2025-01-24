
(function ($) {
    var frmRegistroNuevo = null;
    var frmRegistroDenegar = null;
    this.VacacionesJS = function () {
        this.PERFIL = -1;
        this.PERFIL_JEFE_CTRL_ASISTENCIA = '';
        this.PERFIL_EMPLE_CTRL_ASISTENCIA = '';
        this.PERFIL_ADMIN_CTRL_ASISTENCIA = '';
        this.Empleado = null;
        this.codUsuario = 0;
    };
    this.VacacionesJS.prototype.inicializarMaestro = function () {
        var _this = this;
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    console.log(position);
                },
                function (error) {
                    console.log(error);
                });
        } else {
            alert("Geolocalización no es soportada por este navegador.");
        }

        /* BUSQUEDA */
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

        $("#ddlEmpleado_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "NombreCompleto",
            dataValueField: "IdEmpleado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: false,
                data: []
            }
        });

        var onChangeDependencia = function (val) {
            var empleadosDdl = $("#ddlEmpleado_busqueda").data("kendoDropDownList");
            var empleadosRegDdl = $("#dllEmpleado_registro").data("kendoDropDownList");

            $.ajax({
                url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleadosMaestro",
                data: { IdDependencia: val, Estado: 1 },
                dataType: "json",
                async: false,
                success: function (data) {

                    empleadosDdl.setDataSource(new kendo.data.DataSource({
                        data: data
                    }));

                    empleadosRegDdl.setDataSource(new kendo.data.DataSource({
                        data: data
                    }));
                },
                error: function () {
                    alert("Error al cargar empleado.");
                }
            });
        }

        $("#ddlEstado_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodEstadoProceso",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Justificaciones/ListarEstadoProceso",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.Nombre = $options.filter.filters[0].value;

                        //if (data_param.cod.length = 1) data_param.cod = '0' + data_param.cod;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });


        $("#txtFecha_busqueda").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });

        $("#dtpFechaHoraIni_registro").kendoDatePicker({
            format: "yyyy-MM-dd", // Formato de fecha y hora
            start: "month",  // Comienza en la vista mensual
            depth: "month", // La vista de calendario será mensual (completo)
            change: function (e) {
                var dateIni = this.value();
                var dateFin = $("#dtpFechaHoraFin_registro").data("kendoDatePicker").value();
                _this.calcularDias(dateIni, dateFin)
            }
        });

        $("#dtpFechaHoraFin_registro").kendoDatePicker({
            format: "yyyy-MM-dd", // Formato de fecha y hora
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            change: function (e) {
                var dateIni = $("#dtpFechaHoraIni_registro").data("kendoDatePicker").value();
                var dateFin = this.value();
                _this.calcularDias(dateIni, dateFin)
            }
        });
        //$("#dtpFechaHoraIni_registro").kendoDatePicker().data("kendoDatePicker").max(new Date());
        //$("#dtpFechaHoraFin_registro").kendoDatePicker().data("kendoDatePicker").max(new Date());



        /* REGISTRO NUEVO */



        $("#dllEmpleado_registro").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "NombreCompleto",
            dataValueField: "IdEmpleado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: false,
                data: []
            }
        });

        $("#dllTipoVacaciones_registro").kendoDropDownList({
            dataTextField: "vDescripcion",   // Campo que se mostrará en el dropdown
            dataValueField: "iCodTipoVacaciones",      // Campo que representará el valor de la opción seleccionada
            dataSource: [],    // Array de objetos como fuente de datos
            change: function () {
                _this.configurarArchivos();
            }
        });

        $('#divModal').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '90%',
            height: 'auto',
            title: 'Asignar Vacaciones',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModal').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
                frmRegistroNuevo.reset();
            }
        });
        $('#divModal').data('kendoWindow').center();

        $('#divModalAprobar').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '40%',
            height: 'auto',
            title: 'Aprobar Vacaciones',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModalAprobar').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
            }
        });
        $('#divModalAprobar').data('kendoWindow').center();

        $('#divModalDenegar').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '40%',
            height: 'auto',
            title: 'Denegar Vacaciones',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModalDenegar').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
            }
        });
        $('#divModalDenegar').data('kendoWindow').center();

        $('#divModalHistorial').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '40%',
            height: 'auto',
            title: 'Historial Vacaciones',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModalHistorial').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
            }
        });
        $('#divModalHistorial').data('kendoWindow').center();

        $('#divModalArchivos').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '40%',
            height: 'auto',
            title: 'Archivos de Vacaciones',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModalArchivos').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
            }
        });
        $('#divModalArchivos').data('kendoWindow').center();

        $('#chkFraccionamientoDescanso_registro').kendoCheckBox({
            label: "Fraccionamiento del descanso vacacional media jornada",
            change: function () {
                _this.configurarArchivos();
            }
        });

        $("#txtDescripcion_registro").kendoTextArea({
            rows: 4,
            maxLength: 8000,
            placeholder: "ingrese descripcion"
        });

        $("#txtObservaciones_registro").kendoTextArea({
            rows: 4,
            maxLength: 8000,
            placeholder: "ingrese descripcion"
        });

        $("#txtHistorial_aprobar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial"
        });

        $("#txtObservaciones_aprobar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese observacion"
        });

        $("#txtHistorial_denegar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial"
        });

        $("#txtObservaciones_denegar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese observacion"
        });

        //$("#txtObservaciones_registro").kendoTextArea({
        //    rows: 5,
        //    maxLength: 4000,
        //    placeholder: "ingrese observacion"
        //    ,visible: false
        //});


        $("#ufArchivos_registro").kendoUpload({
            validation: {
                // Validación del tipo de archivo (solo PDF)
                allowedExtensions: [".pdf"],

                // Validación del tamaño máximo del archivo (2MB)
                maxFileSize: 2 * 1024 * 1024
            },
            localization: {
                invalidFileExtension: "El tipo de archivo no es valido.",
                invalidMaxFileSize: "El maximo permitido para el archivo es 2MB",
            }
        });

        $("#ufArchivosFormato_registro").kendoUpload({
            multiple: false,
            validation: {
                // Validación del tipo de archivo (solo PDF)
                allowedExtensions: [".pdf"],

                // Validación del tamaño máximo del archivo (2MB)
                maxFileSize: 2 * 1024 * 1024
            },
            localization: {
                invalidFileExtension: "El tipo de archivo no es valido.",
                invalidMaxFileSize: "El maximo permitido para el archivo es 2MB",
            }
        });

        $("#txtHistorial_visualizar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial",
            readonly: true
        });

        $("#lvArchivos").kendoListView({
            dataSource: {
                data: []
            },
            template:
                "<div class='file-item'>" +
                "<span class='glyphicon glyphicon-file' aria-hidden='true'></span>" +
                "<a href='" + controladorApp.obtenerRutaBase() + 'Vacaciones/DescargarArchivo?fileName=' + "#= vUrlArchivo #' target='_blank'>#= vNombre #</a>" +
                "</div>"
        });

        $("#lvArchivos_registro").kendoListView({
            dataSource: {
                data: []
            },
            template:
                "<div class='file-item'>" +
                "<span class='glyphicon glyphicon-file' aria-hidden='true'></span>" +
                "<a href='" + controladorApp.obtenerRutaBase() + 'Vacaciones/DescargarArchivo?fileName=' + "#= vUrlArchivo #' target='_blank'>#= vNombre #</a>" +
                "</div>"
        });

        $("#lvArchivosFormatos_registro").kendoListView({
            dataSource: {
                data: []
            },
            template:
                "<div class='file-item'>" +
                "<span class='glyphicon glyphicon-file' aria-hidden='true'></span>" +
                "<a href='" + controladorApp.obtenerRutaBase() + 'Vacaciones/DescargarArchivo?fileName=' + "#= vUrlArchivo #' target='_blank'>#= vNombre #</a>" +
                "</div>"
        });

        $("#divFormato").hide();
        $("#divFormato2").hide();

        if (this.Empleado != null) {
            $("#ddlDependencia_busqueda").data("kendoDropDownList").value(this.Empleado.IdDependencia);
            onChangeDependencia(this.Empleado.IdDependencia);
            $("#ddlEmpleado_busqueda").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
            $("#dllEmpleado_registro").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
        }

        frmRegistroNuevo = $("#frmRegistroNuevo").kendoValidator({
            rules: {
                validarFormatos: function (input) {
                    if (input.is("[name=ufArchivosFormato_registro]")) {
                        var tipoVacaciones = $("#dllTipoVacaciones_registro").data("kendoDropDownList").value();
                        var fraccionamiento = $("#chkFraccionamientoDescanso_registro").data("kendoCheckBox").check();
                        if (fraccionamiento || tipoVacaciones == 2) {
                            var upload = $("#ufArchivosFormato_registro").data("kendoUpload");
                            return upload.getFiles().length > 0;  // Aseguramos que haya archivos cargados
                        }

                    }
                    return true;
                },
                validarPeriodo: function (input) {
                    if (input.is(":checkbox") && input.hasClass("select-row-periodo")) {
                        return input.prop("checked");  // Verificamos si el checkbox está marcado
                    }
                    return true;
                },
                validarFechaFin: function (input) {
                    if (input.is("[name=dtpFechaHoraFin_registro]")) {
                        var fecha = $("#dtpFechaHoraFin_registro").data("kendoDatePicker").value();
                        return (fecha);
                    }
                    return true;
                },
                validarDiasAdelanto: function (input) {
                    debugger;
                    if (input.is("[name=dtpFechaHoraFin_registro]")) {
                        var tipoVacaciones = $("#dllTipoVacaciones_registro").data("kendoDropDownList").value();
                        var asignados = $("#txtAsignado_registro").val();
                        var disponibles = $("#txtDiasVacaciones").val();
                        if (tipoVacaciones == "2")
                            return parseInt(asignados) <= parseInt(disponibles);
                    }
                    return true;
                }
            },
            messages: {
                validarFormatos: "Debe cargar un formato.",
                validarPeriodo: "requerido.",
                validarFechaFin: "requerido",
                validarDiasAdelanto: "no puede adelantar mas dias de los disponible",
            }
        }).data("kendoValidator");

        frmRegistroDenegar = $("#frmRegistroDenegar").kendoValidator({
            rules: {
                // Validación personalizada para 'control1' y 'control2'
                validateObservacion: function (input) {
                    if (input.attr("name") == "txtObservaciones_denegar") {
                        if ($('#divModalDenegar').is(':visible')) {
                            return input.val() !== "";
                        }
                    }
                    return true;
                }
            },
            messages: {
                validateObservacion: "Este campo es requerido"
            }
        }).data("kendoValidator");

        this.inicializarGridMaestro();
    };


    this.VacacionesJS.prototype.inicializarGridMaestro = function () {
        var _this = this;
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesTrabajador',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    debugger;
                    if ($operation === "read") {
                        data_param.iCodigoDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        data_param.iCodTrabajador = $("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
                        data_param.fecha = $("#txtFecha_busqueda").val();
                        data_param.iCodEstadoProceso = $("#ddlEstado_busqueda").data("kendoDropDownList").value();

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
                    id: "iCodVacaciones",
                    fields: {
                        iCodVacaciones: { type: "number" },
                        vNumeroDocumento: { type: "string" },
                        vNombreTrabajador: { type: "string" },
                        vTipoVacaciones: { type: "string" },
                        dtFechaInicio: { type: "string", parse: _this.parseDate },
                        dtFechaFin: { type: "string", parse: _this.parseDate },
                        dtAuditCreacion: { type: "string", parse: _this.parseDate },
                        dFechaAprobadoJefe: { type: "string", parse: _this.parseDateTime },
                        dFechaAprobadoAdmin: { type: "string", parse: _this.parseDateTime },
                        dFechaDenegadoAdmin: { type: "string", parse: _this.parseDateTime },
                    }
                }
            }
        });

        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel",], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado Vacaciones.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,
            filterable: true,
            dataType: 'json',
            columns: [

                {
                    // Columna para seleccionar una fila
                    field: "iCodVacaciones",
                    title: "Seleccionar",
                    width: "20px",
                    headerTemplate: function (item) {
                        return '<input id="select-row-all" class="k-checkbox k-checkbox-md k-rounded-md" data-role="checkbox" aria-label="Select all rows" aria-checked="false" type="checkbox">';
                    },
                    template: function (item) {
                        var ret = "";
                        if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 6) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodvacaciones="' + item.iCodVacaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 2 || item.iCodEstadoProceso == 6 || item.iCodEstadoProceso == 7) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodvacaciones="' + item.iCodVacaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
                            }
                        }
                        return ret;
                    },
                    filterable: false
                },
                {
                    field: "vDependencia",
                    title: "DEPENDENCIA",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vNumeroDocumento",
                    title: "DNI",
                    attributes: { style: "text-align:center;" },
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vNombreTrabajador",
                    title: "NOMBRE",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtAuditCreacion",
                    title: "FECHA REGISTRO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vTipoVacaciones",
                    title: "TIPO",
                    attributes: { style: "text-align:center;" },
                    filterable: { multi: true, search: true, ignoreCase: true },
                },
                {
                    field: "iAsignados",
                    title: "DIAS ASIGNADOS",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "iDisponibles",
                    title: "DIAS DISPONIBLES",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtFechaInicio",
                    title: "FECHA INICIO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtFechaFin",
                    title: "FECHA FIN",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "iPeriodo",
                    title: "PERIODO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vEstadoProceso",
                    title: "ESTADO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dFechaAprobadoJefe",
                    title: "FECHA APROB JEFE",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dFechaAprobadoAdmin",
                    title: "FECHA APROB ADMIN",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dFechaDenegadoAdmin",
                    title: "FECHA DENE",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    // Columna para el botón al final de la fila
                    field: "iCodVacaciones",
                    title: "Acciones",
                    width: "150px",
                    template: function (item) {
                        debugger;

                        var ret = "<center>";
                        if (_this.PERFIL == _this.PERFIL_EMPLE_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5) {
                                ret += "<button title='Editar'  style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.iCodVacaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA || _this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if ((_this.codUsuario == item.vAuditCreacion) && (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5)) {
                                ret += "<button title='Editar' style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.iCodVacaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        if ((_this.codUsuario == item.vAuditCreacion) && (item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5)) {
                            ret += "<button title='Ver Historial' style='margin-right:3px' class='btn btn-success btn-xs' onclick='controlador.abrirModalHistorial(" + item.iCodVacaciones + ")'><span class='glyphicon glyphicon-comment' aria-hidden='true'></span></button>";
                        }
                        //if (item.vUrlArchivo) {
                        //    var path = controladorApp.obtenerRutaBase() + 'Justificaciones/DescargarArchivo?fileName=' + item.vUrlArchivo;
                        //    ret += "<a href='" + path + "' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-file' aria-hidden='true'></span></a>";
                        //}
                        if (item.vUrlArchivo) {
                            ret += "<button title='Ver Archivos' style='margin-right:3px' class='btn btn-info btn-xs' onclick='controlador.abrirModalArchivos(" + item.iCodVacaciones + ")'><span class='glyphicon glyphicon-file' aria-hidden='true'></span></button>";
                        }
                        ret += "</center>";
                        return ret;
                    },
                    filterable: false
                }
            ],
            dataBound: function () {
                // Event listener para seleccionar/desmarcar todas las filas cuando el checkbox de la cabecera cambie
                $("#select-row-all").on("change", function () {
                    var checked = $(this).prop("checked");
                    $("#divGrid input[type='checkbox'].select-row").prop("checked", checked);
                });


            },
            //excelExport: function (e) {
            //    var sheet = e.workbook.sheets[0];
            //    var template = kendo.template(this.columns[4].template);
            //}
        }).data();


        this.datosPeriodo = [];
        this.datosPeriodo = new kendo.data.DataSource({
            data: [],
            schema: {
                model: {
                    fields: {
                        IdPeriodo: { type: "number" },
                        Periodo: { type: "number" },
                        FechaInicio: { type: "date" },
                        FechaFin: { type: "number" },
                        Prog: { type: "string" },
                        Asignado: { type: "string" },
                        Disp: { type: "string" },
                        Fracc: { type: "string" },
                        IdPeriodoSet: { type: "number" }
                    }
                }
            }
        });


        $("#divGridPeriodo").kendoGrid({
            dataSource: this.datosPeriodo, // Aquí va tu fuente de datos
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "IdPeriodo", // NroDia columna
                    title: "PERIODO",
                    width: "20px",
                    template: function (item) {
                        if (item.Disp > 0) {
                            if (item.IdPeriodoSet == item.Periodo) {
                                return "<center> <input type='checkbox' class='select-row-periodo' data-iperiodo='" + item.Periodo + "' checked/></center>";
                            } else {
                                return "<center> <input type='checkbox' class='select-row-periodo' data-iperiodo='" + item.Periodo + "'/></center>";
                            }
                        }
                        else { return ""; }
                    }

                },
                {
                    field: "Periodo", // NroDia columna
                    title: "PERIODO"
                },
                {
                    field: "FechaInicio", // Fecha columna
                    title: "FECHA INICIO",
                    template: function (item) {
                        if (item.FechaInicio) {
                            return kendo.toString(kendo.parseDate(item.FechaInicio), 'dd/MM/yyyy');
                        }
                        return "";
                    }
                },
                {
                    field: "FechaFin", // Fecha columna
                    title: "FECHA INICIO",
                    template: function (item) {
                        if (item.FechaFin) {
                            return kendo.toString(kendo.parseDate(item.FechaFin), 'dd/MM/yyyy');
                        }
                        return "";
                    }
                },
                {
                    field: "Prog", // Minutos columna
                    title: "PEND"
                },
                {
                    field: "Asignado", // Entrada columna
                    title: "ASIGNADO"
                },
                {
                    field: "Disp", // Hora Salida columna
                    title: "DISP"
                },
                //{
                //    field: "Fracc", // Hora Salida columna
                //    title: "FRACC",
                //    width: "50px"
                //}
            ]
        });


        //}
    };

    $("#divGrid").on("change", ".select-row", function () {
        var allChecked = $("#divGrid input[type='checkbox'].select-row").length === $("#divGrid input[type='checkbox'].select-row:checked").length;
        $("#select-row-all").prop("checked", allChecked);
    });

    // Aquí, se delega el evento a los checkboxes dentro del grid
    //$(document).on("change", ".select-row", function (e) {
    //    debugger;
    //    var checkboxes = $(".select-row");
    //    // Si el checkbox actual está marcado
    //    if ($(this).is(":checked")) {
    //        checkboxes.not(this).prop("checked", false); // Desmarcar todos los demás
    //    }

    //    var row = $(this).closest("tr"); // Obtenemos la fila que contiene el checkbox
    //    obtenerSeleccionFila(row); // Llamamos a la función cuando el checkbox cambie
    //});

    //// Función para obtener los valores de los checkboxes seleccionados
    //function obtenerSeleccionFila(row) {
    //    var checkbox = row.find("input[type='checkbox']");
    //    var isChecked = checkbox.is(":checked");

    //    // Verificar si está seleccionado y obtener el valor del checkbox
    //    if (isChecked) {
    //        var icodVacaciones = checkbox.data("icodvacaciones");
    //        $('#hdIdVacacionAprobar').val(icodVacaciones);
    //    }
    //    else {
    //        $('#hdIdVacacionAprobar').val(0);
    //    }
    //}

    $(document).on("change", ".select-row-periodo", function (e) {
        debugger;
        var checkboxes = $(".select-row-periodo");
        // Si el checkbox actual está marcado
        if ($(this).is(":checked")) {
            checkboxes.not(this).prop("checked", false); // Desmarcar todos los demás
        }

        var row = $(this).closest("tr"); // Obtenemos la fila que contiene el checkbox
        obtenerSeleccionFilaPeriodo(row); // Llamamos a la función cuando el checkbox cambie
    });

    function obtenerSeleccionFilaPeriodo(row) {
        var checkbox = row.find("input[type='checkbox']");
        var isChecked = checkbox.is(":checked");

        // Verificar si está seleccionado y obtener el valor del checkbox
        if (isChecked) {
            var iperiodo = checkbox.data("iperiodo");
            $('#hdIdVacacionPeriodo').val(iperiodo);
        }
        else {
            $('#hdIdVacacionPeriodo').val(0);
        }
    }

    this.VacacionesJS.prototype.buscar = function (e) {
        e.preventDefault();
        debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
        $("#select-row-all").prop("checked", false)
        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.VacacionesJS.prototype.abrirModalNuevo = function () {
        var _this = this;
        $('#divModal_wnd_title').text('ASIGNAR VACACIONES');

        //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
        $('#dtpFechaHoraIni_registro').data("kendoDatePicker").value(null);
        $('#dtpFechaHoraFin_registro').data("kendoDatePicker").value(null);
        $('#txtAsignado_registro').val('0');
        $('#txtDescripcion_registro').val('');
        $('#chkFraccionamientoDescanso_registro').prop('checked', false);
        $("#ufArchivos_registro").data("kendoUpload").clearAllFiles();
        $("#ufArchivosFormato_registro").data("kendoUpload").clearAllFiles();

        $("#div-download-files").html("");

        var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesPeriodo';

        $.ajax({
            url: urlMetodo,
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodTrabajador: this.Empleado.IdEmpleado, iCodigoDependencia: this.Empleado.IdDependencia },
            success: function (res) {
                debugger;
                var suma = 0;
                _this.datosPeriodo.data([]);
                for (var i = 0; i < res.length; i++) {
                    var item = res[i];
                    suma += item.iDisponibles;
                    _this.datosPeriodo.add({
                        IdPeriodo: item.iCodVacacionesPeriodo,
                        Periodo: item.iPeriodo,
                        FechaInicio: item.dtFechaInicio,
                        FechaFin: item.dtFechaFin,
                        Prog: item.iProgramados,
                        Asignado: item.iAsignados,
                        Disp: item.iDisponibles,
                        Fracc: item.iFraccionamiento,
                        IdPeriodoSet: null
                    });
                }
                if (suma > 0) {
                    $("#divAdelanto").hide();
                    var datos = _this.itemsTipoVacaciones(true, true);
                    $("#dllTipoVacaciones_registro").data("kendoDropDownList").dataSource.data(datos);
                }
                else {
                    $("#divAdelanto").show();
                    var datos = _this.itemsTipoVacaciones(false, true);
                    $("#dllTipoVacaciones_registro").data("kendoDropDownList").dataSource.data(datos);
                    var total = _this.calcularVacaciones(kendo.parseDate(_this.Empleado.FechaInicio));
                    $("#txtDiasVacaciones").val(total);
                }

                _this.configurarArchivos();
                $('#divModal').data('kendoWindow').open();
            },
            error: function (res) {
                //alert(res);
            }
        });
    }

    this.VacacionesJS.prototype.closeModalNuevo = function () {
        $('#divModal').data('kendoWindow').close();
    }

    this.VacacionesJS.prototype.abrirModalAprobar = function () {
        debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodvacaciones"));
        });

        if (vIds.length > 0) {

            if (vIds.length == 1) {
                $('#divHistorial_aprobar').show();

                var id = vIds[0];
                var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesProcesoHistorial';

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodVacaciones: id },
                    success: function (res) {
                        var historialComentarios = [];
                        for (var i = 0; i < res.length; i++) {
                            historialComentarios.push(res[i].vComentario);
                        }
                        var vComentario = historialComentarios.join('\n');
                        $('#txtHistorial_aprobar').val(vComentario);
                        $('#txtObservaciones_aprobar').val('');
                        $('#divModalAprobar').data('kendoWindow').open();
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }
            else {
                $('#divHistorial_aprobar').hide();
                $('#divModalAprobar').data('kendoWindow').open();
            }
        }

    }

    this.VacacionesJS.prototype.closeModalAprobar = function () {
        $('#divModalAprobar').data('kendoWindow').close();
    }

    this.VacacionesJS.prototype.abrirModalDenegar = function () {
        debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodvacaciones"));
        });

        if (vIds.length > 0) {
            if (vIds.length == 1) {
                $('#divHistorial_denegar').show();

                var id = vIds[0];
                var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesProcesoHistorial';

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodVacaciones: id },
                    success: function (res) {
                        var historialComentarios = [];
                        for (var i = 0; i < res.length; i++) {
                            historialComentarios.push(res[i].vComentario);
                        }
                        var vComentario = historialComentarios.join('\n');
                        $('#txtHistorial_denegar').val(vComentario);
                        $('#txtObservaciones_denegar').val('');
                        $('#divModalDenegar').data('kendoWindow').open();
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }
            else {
                $('#divHistorial_denegar').hide();
                $('#divModalDenegar').data('kendoWindow').open();
            }
        }
    }

    this.VacacionesJS.prototype.closeModalDenegar = function () {
        $('#divModalDenegar').data('kendoWindow').close();
    }

    this.VacacionesJS.prototype.abrirModalHistorial = function (iCodVacaciones) {
        if (iCodVacaciones > 0) {
            var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesProcesoHistorial';

            $.ajax({
                url: urlMetodo,
                type: 'GET',
                dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                contentType: 'application/json',  // Especificamos que enviamos JSON
                data: { iCodVacaciones: iCodVacaciones },
                success: function (res) {
                    var historialComentarios = [];
                    for (var i = 0; i < res.length; i++) {
                        historialComentarios.push(res[i].vComentario);
                    }
                    var vComentario = historialComentarios.join('\n');
                    $('#txtHistorial_visualizar').val(vComentario);

                    $('#divModalHistorial').data('kendoWindow').open();
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    this.VacacionesJS.prototype.closeModalHistorial = function () {
        $('#divModalHistorial').data('kendoWindow').close();
    }

    this.VacacionesJS.prototype.abrirModalArchivos = function (iCodVacaciones) {
        var _this = this;
        debugger;
        if (iCodVacaciones > 0) {

            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Vacaciones/ObtenerVacacionesPorId',
                type: 'GET',
                dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                contentType: 'application/json',  // Especificamos que enviamos JSON
                data: { iCodVacaciones: iCodVacaciones },
                success: function (res) {
                    debugger;
                    if (res) {
                        if (res.Archivos.length > 0) {
                            //TRAER FICHEROS didijoca

                            //var btn = "<a href='" + path + "' class='btn btn-primary btn-sm'><span class='glyphicon glyphicon-check' aria-hidden='true'></span> Descargar</a>";
                            //$("#div-download-files").html(btn);

                            $("#lvArchivos").data("kendoListView").dataSource.data(res.Archivos);


                        }
                        $('#divModalArchivos').data('kendoWindow').open();
                    }
                },
                error: function (xhr, status, error) {
                    // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                    console.error('Error en la solicitud:', status, error);
                    // Puedes mostrar un mensaje de error en el UI si lo deseas.
                }
            });
        }


    }

    this.VacacionesJS.prototype.closeModalArchivos = function () {
        $('#divModalArchivos').data('kendoWindow').close();
    }

    this.VacacionesJS.prototype.grabar = function (e) {
        debugger;
        e.preventDefault();

        if (frmRegistroNuevo.validate()) {
            var vacaciones = vacacionesObjeto($, 0);

            vacaciones.iCodTrabajador = this.Empleado.IdEmpleado;
            vacaciones.iCodigoDependencia = this.Empleado.IdDependencia;

            // Crear una instancia de FormData
            var data_param = new FormData();

            // Agregar propiedades simples
            data_param.append('iCodVacaciones', vacaciones.iCodVacaciones);
            data_param.append('iCodTipoVacaciones', vacaciones.iCodTipoVacaciones);
            data_param.append('iCodTrabajador', vacaciones.iCodTrabajador);
            data_param.append('iCodigoDependencia', vacaciones.iCodigoDependencia);
            data_param.append('iCodEstadoProceso', vacaciones.iCodEstadoProceso);
            data_param.append('bFraccionamientoVacacionalMediaJornada', vacaciones.bFraccionamientoVacacionalMediaJornada);
            data_param.append('dtFechaInicio', vacaciones.dtFechaInicio.toISOString());
            data_param.append('dtFechaFin', vacaciones.dtFechaFin.toISOString());
            data_param.append('vDescripcion', vacaciones.vDescripcion);
            data_param.append('iDisponibles', vacaciones.iDisponibles);
            data_param.append('iAsignados', vacaciones.iAsignados);
            data_param.append('iPeriodo', vacaciones.iPeriodo);
            data_param.append('bEstado', vacaciones.bEstado);
            data_param.append('dtAuditCreacion', vacaciones.dtAuditCreacion);
            data_param.append('vAuditCreacion', vacaciones.vAuditCreacion);
            data_param.append('dtAuditModificacion', vacaciones.dtAuditModificacion);
            data_param.append('vAuditModificacion', vacaciones.vAuditModificacion);

            debugger;
            var filesUpload = [];

            $.each($("#ufArchivos_registro").data("kendoUpload").getFiles(), function (index, file) {
                data_param.append('filesUpload[' + index + ']', file.rawFile);
            });

            if (vacaciones.bFraccionamientoVacacionalMediaJornada || vacaciones.iCodTipoVacaciones == 2) {
                var fileFormato = $("#ufArchivosFormato_registro").data("kendoUpload").getFiles();
                data_param.append('filesUploadFormato', fileFormato[0].rawFile);
            }

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/GrabarVacacionesTrabajador';

            $.ajax({
                url: urlMetodo,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
                    $('#dtpFechaHoraIni_registro').data("kendoDatePicker").value(null);
                    $('#dtpFechaHoraFin_registro').data("kendoDatePicker").value(null);
                    $('#txtDescripcion_registro').val('');
                    $('#txtAsignado_registro').val('');
                    $('#chkFraccionamientoDescanso_registro').prop('checked', false);
                    $('#divModal').data('kendoWindow').close();
                    $('#hdIdVacacion').val(0)
                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    this.VacacionesJS.prototype.aprobar = function (e) {
        e.preventDefault();
        debugger;
        var aprobarDenegar = false;
        var observaciones = "";
        var isValid = true;

        if ($('#divModalDenegar').is(':visible')) {
            observaciones = $('#txtObservaciones_denegar').val();
            aprobarDenegar = false;
            isValid = frmRegistroDenegar.validate();
        } else if ($('#divModalAprobar').is(':visible')) {
            aprobarDenegar = true;
            observaciones = $('#txtObservaciones_aprobar').val();
        }

        if (isValid) {
            var vIds = [];
            var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
            checkboxesSeleccionados.each(function () {
                vIds.push($(this).attr("data-icodvacaciones"));
            });

            var data_param = new FormData();

            data_param.append('vIds', vIds.join(','));
            data_param.append('vDescripcion', observaciones);
            data_param.append('aprobarDenegar', aprobarDenegar);

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/AprobarDenegarVacacionesTrabajadorMas';

            $.ajax({
                url: urlMetodo,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    $('#txtHistorial_aprobar').val('');
                    $('#txtObservaciones_aprobar').val('');
                    $('#txtHistorial_denegar').val('');
                    $('#txtObservaciones_denegar').val('');

                    $('#divModalAprobar').data('kendoWindow').close();
                    $('#divModalDenegar').data('kendoWindow').close();

                    //$('#hdIdJustificacion').val(0);
                    $('#hdIdJustificacionAprobar').val(0);
                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                    $("#select-row-all").prop("checked", false);
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    //this.VacacionesJS.prototype.aprobar = function (e) {
    //    e.preventDefault();
    //    debugger;
    //    var vacaciones = vacacionesObjeto($, 0);
    //    vacaciones.iCodTrabajador = this.Empleado.IdEmpleado;
    //    vacaciones.iCodigoDependencia = this.Empleado.IdDependencia;

    //    vacaciones.iCodVacaciones = $("#hdIdVacacionAprobar").val();
    //    // Crear una instancia de FormData
    //    var data_param = new FormData();

    //    // Agregar propiedades simples
    //    data_param.append('iCodVacaciones', vacaciones.iCodVacaciones);
    //    data_param.append('iCodTrabajador', vacaciones.iCodTrabajador);
    //    data_param.append('iCodigoDependencia', vacaciones.iCodigoDependencia);
    //    data_param.append('iCodEstadoProceso', vacaciones.iCodEstadoProceso);
    //    data_param.append('bFraccionamientoVacacionalMediaJornada', vacaciones.bFraccionamientoVacacionalMediaJornada);
    //    data_param.append('vDescripcion', vacaciones.vDescripcion);
    //    data_param.append('iDisponibles', vacaciones.iDisponibles);
    //    data_param.append('iAsignados', vacaciones.iAsignados);
    //    data_param.append('iPeriodo', vacaciones.iPeriodo);
    //    data_param.append('bEstado', vacaciones.bEstado);
    //    data_param.append('dtAuditCreacion', vacaciones.dtAuditCreacion);
    //    data_param.append('vAuditCreacion', vacaciones.vAuditCreacion);
    //    data_param.append('dtAuditModificacion', vacaciones.dtAuditModificacion);
    //    data_param.append('vAuditModificacion', vacaciones.vAuditModificacion);

    //    data_param.append('aprobarDenegar', vacaciones.aprobarDenegar);

    //    var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/AprobarDenegarVacacionesTrabajador';

    //    $.ajax({
    //        url: urlMetodo,
    //        type: 'POST',
    //        dataType: 'json',
    //        contentType: false,
    //        processData: false,
    //        data: data_param,
    //        success: function (res) {
    //            $('#txtHistorial_aprobar').val('');
    //            $('#txtObservaciones_aprobar').val('');
    //            $('#txtHistorial_denegar').val('');
    //            $('#txtObservaciones_denegar').val('');

    //            $('#divModalAprobar').data('kendoWindow').close();
    //            $('#divModalDenegar').data('kendoWindow').close();

    //            $("#hdIdVacacionAprobar").val(0);
    //            $('#divGrid').data("kendoGrid").dataSource.page(1);
    //        },
    //        error: function (res) {
    //            //alert(res);
    //        }
    //    });
    //}

    this.VacacionesJS.prototype.editar = function (iCodVacaciones) {
        var _this = this;

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Vacaciones/ObtenerVacacionesPorId',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodVacaciones: iCodVacaciones },
            success: function (res) {
                debugger;
                if (res) {
                    $('#hdIdVacacion').val(iCodVacaciones);
                    $('#hdIdVacacionPeriodo').val(res.iPeriodo);

                    $("#dllEmpleado_registro").data("kendoDropDownList").value(res.iCodTrabajador);
                    $('#dtpFechaHoraIni_registro').data("kendoDatePicker").value(res.dtFechaInicio);
                    $('#dtpFechaHoraFin_registro').data("kendoDatePicker").value(res.dtFechaFin);
                    $('#txtAsignado_registro').val(res.iAsignados)

                    $("#ufArchivos_registro").data("kendoUpload").clearAllFiles();
                    $("#ufArchivosFormato_registro").data("kendoUpload").clearAllFiles();


                    $("#txtDescripcion_registro").val(res.vDescripcion);
                    $('#hdIdEstadoProceso').val(res.iCodEstadoProceso);

                    //$("#div-download-files").html("");
                    //if (res.Archivos.length > 0) {
                    //    var path = controladorApp.obtenerRutaBase() + 'Vacaciones/DescargarArchivo?fileName=' + res.Archivos[0].vUrlArchivo;
                    //    var btn = "<a href='" + path + "' class='btn btn-primary btn-sm'><span class='glyphicon glyphicon-check' aria-hidden='true'></span> Descargar</a>";
                    //    $("#div-download-files").html(btn);
                    //}
                    $("#lvArchivos_registro").data("kendoListView").dataSource.data(res.Archivos.filter(archivo => archivo.iCodTipoVacacionesFormato == 1));
                    $("#lvArchivosFormatos_registro").data("kendoListView").dataSource.data(res.Archivos.filter(archivo => archivo.iCodTipoVacacionesFormato == 2 || archivo.iCodTipoVacacionesFormato == 3));

                    $('#divModal_wnd_title').text('Modificar Vacaciones');

                    var urlMetodo = controladorApp.obtenerRutaBase() + 'Vacaciones/ListarVacacionesPeriodo';

                    $.ajax({
                        url: urlMetodo,
                        type: 'GET',
                        dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                        contentType: 'application/json',  // Especificamos que enviamos JSON
                        data: { iCodTrabajador: _this.Empleado.IdEmpleado, iCodigoDependencia: _this.Empleado.IdDependencia },
                        success: function (res2) {
                            debugger;
                            _this.datosPeriodo.data([]);
                            for (var i = 0; i < res2.length; i++) {
                                var suma = 0;
                                var item = res2[i];
                                suma += item.iDisponibles;
                                _this.datosPeriodo.add({
                                    IdPeriodo: item.iCodVacacionesPeriodo,
                                    Periodo: item.iPeriodo,
                                    FechaInicio: item.dtFechaInicio,
                                    FechaFin: item.dtFechaFin,
                                    Prog: item.iProgramados,
                                    Asignado: item.iAsignados,
                                    Disp: item.iDisponibles,
                                    Fracc: item.iFraccionamiento,
                                    IdPeriodoSet: res.iPeriodo
                                });

                                if (suma > 0) {
                                    $("#divAdelanto").hide();
                                    var datos = _this.itemsTipoVacaciones(true, true);
                                    $("#dllTipoVacaciones_registro").data("kendoDropDownList").dataSource.data(datos);
                                }
                                else {
                                    $("#divAdelanto").show();
                                    var datos = _this.itemsTipoVacaciones(false, true);
                                    $("#dllTipoVacaciones_registro").data("kendoDropDownList").dataSource.data(datos);
                                    var total = _this.calcularVacaciones(kendo.parseDate(_this.Empleado.FechaInicio));
                                    $("#txtDiasVacaciones").val(total);
                                }

                                $("#dllTipoVacaciones_registro").data("kendoDropDownList").value(res.iCodTipoVacaciones);
                                _this.configurarArchivos();
                            }

                            $('#divModal').data('kendoWindow').open();
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }
            },
            error: function (xhr, status, error) {
                // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                console.error('Error en la solicitud:', status, error);
                // Puedes mostrar un mensaje de error en el UI si lo deseas.
            }
        });
    }

    this.VacacionesJS.prototype.calcularDias = function (fechaIni, fechaFin) {
        //console.log(moment());
        if (fechaIni && fechaFin) {
            var diferenciaDias = (Math.abs(fechaIni - fechaFin) / (1000 * 60 * 60 * 24)) + 1;
            $('#txtAsignado_registro').val(diferenciaDias);
        }
    }

    this.VacacionesJS.prototype.configurarArchivos = function () {
        var fraccionamiento = $("#chkFraccionamientoDescanso_registro").data("kendoCheckBox").check();
        var tipoVacaciones = $("#dllTipoVacaciones_registro").data("kendoDropDownList").value();
        if (fraccionamiento || tipoVacaciones == 2) {
            $("#divFormato").show();
            $("#divFormato2").show();
        }
        else {
            $("#divFormato").hide();
            $("#divFormato2").hide();
        }
    }

    this.VacacionesJS.prototype.calcularVacaciones = function (fechaIngreso) {
        // La fecha actual
        const fechaActual = new Date();

        // Calcular la diferencia en milisegundos
        const diferenciaTiempo = fechaActual - fechaIngreso;

        // Convertir la diferencia a días
        const diasTrabajados = Math.floor(diferenciaTiempo / (1000 * 3600 * 24));

        // Si el trabajador ha trabajado más de un año (365 días)
        if (diasTrabajados >= 365) {
            return 30; // Vacaciones completas
        } else {
            // Calcular las vacaciones proporcionales
            const vacacionesProporcionales = (diasTrabajados / 365) * 30;
            return Math.round(vacacionesProporcionales); // Redondear al número entero más cercano
        }
    }

    this.VacacionesJS.prototype.itemsTipoVacaciones = function (incluyeVaca, includeAdelanto) {
        var datos = [];
        if (incluyeVaca) datos.push({ iCodTipoVacaciones: 1, vDescripcion: "VACACIONES" });
        if (includeAdelanto) datos.push({ iCodTipoVacaciones: 2, vDescripcion: "ADELANTO VACACIONES" });
        return datos;
    }

    this.VacacionesJS.prototype.parseDate = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy');
        }
        return "";
    }

    this.VacacionesJS.prototype.parseDateTime = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy HH:mm');
        }
        return "";
    }
}(jQuery));

function vacacionesObjeto($, iCodVacaciones) {
    debugger;
    var iCodEstadoProceso = 0;
    var periodo = 0;
    var observaciones = '';
    var aprobarDenegar = false;

    if ($("#hdIdVacacion").val() > 0) {
        iCodVacaciones = $("#hdIdVacacion").val();
        iCodEstadoProceso = $("#hdIdEstadoProceso").val();
    }
    var dtFechaInicio = $('#dtpFechaHoraIni_registro').data("kendoDatePicker").value();
    var dtFechaFin = $('#dtpFechaHoraFin_registro').data("kendoDatePicker").value();
    periodo = $('#hdIdVacacionPeriodo').val();

    var dataSource = $("#divGridPeriodo").data("kendoGrid").dataSource;
    var dataItem = dataSource.data().find(function (item) {
        return item.Periodo == periodo
    });

    observaciones = $("#txtDescripcion_registro").val();
    if ($('#divModalDenegar').is(':visible')) {
        observaciones = $('#txtObservaciones_denegar').val();
        aprobarDenegar = false;
    } else if ($('#divModalAprobar').is(':visible')) {
        aprobarDenegar = true;
        observaciones = $('#txtObservaciones_aprobar').val();
    }


    return {
        iCodVacaciones: iCodVacaciones,
        iCodTipoVacaciones: $("#dllTipoVacaciones_registro").data("kendoDropDownList").value(),
        iCodTrabajador: 0,
        iCodigoDependencia: 0,
        iCodEstadoProceso: iCodEstadoProceso,
        bFraccionamientoVacacionalMediaJornada: $('#chkFraccionamientoDescanso_registro').prop('checked') ? true : false,
        dtFechaInicio: dtFechaInicio,
        dtFechaFin: dtFechaFin,
        vDescripcion: observaciones,
        iAsignados: $('#txtAsignado_registro').val(),
        iDisponibles: dataItem ? dataItem.Disp : 0,
        iPeriodo: periodo,
        bEstado: true,
        aprobarDenegar: aprobarDenegar,
        Archivos: [
        ],
        Procesos: [
        ]
    };
}