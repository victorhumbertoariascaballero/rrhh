(function ($) {
    var frmHorasCompensables = null;
    var frmRegistroDenegar = null;
    var selectedDatesCompensaciones = [];
    this.HorasCompensablesJS = function () {
        this.PERFIL = -1;
        this.PERFIL_JEFE_CTRL_ASISTENCIA = '';
        this.PERFIL_EMPLE_CTRL_ASISTENCIA = '';
        this.PERFIL_ADMIN_CTRL_ASISTENCIA = '';
        this.Empleado = null;
        this.codUsuario = 0;
    };
    this.HorasCompensablesJS.prototype.inicializarMaestro = function () {
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
            },
            change: function () {
                var val = this.value();
                //acceder a empleados 
                onChangeDependencia(val);
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

        //$('#btnAprobar').prop('disabled', true);
        //$('#btnDenegar').prop('disabled', true);

        /*$("#txtFecha_busqueda").kendoDatePicker().data("kendoDatePicker").max(new Date());*/

        $("#txtFecha_busqueda").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });


        /*$("#dtpFechaCompensacion_registro").kendoDatePicker().data("kendoDatePicker").max(new Date());*/


        $("#dtpFechaCompensacion_registro").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });

        //$('#dtpHoraEntradaCompensacion_registroDetalle_Completo').kendoDatePicker().data("kendoDatePicker").max(new Date());
        //$('#dtpHoraSalidaCompensacion_registroDetalle_Completo').kendoDatePicker().data("kendoDatePicker").max(new Date());


        $("#dtpHoraEntradaCompensacion_registroDetalle_Completo").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });

        /*$("#dtpFechaCompensacion_registroDetalle").kendoDatePicker().data("kendoDatePicker").max(new Date());*/

        $("#dtpHoraSalidaCompensacion_registroDetalle_Completo").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });

        $("#dtpFechaCompensacion_registroDetalle").kendoDatePicker({
            start: "month",  // Comienza en la vista mensual
            depth: "month",  // La vista de calendario será mensual (completo)
            //max: new Date(), // Aquí defines que no se pueda seleccionar una fecha posterior a hoy (opcional)
            change: function () {
                // Código opcional para manejar el evento de cambio de fecha
                console.log("Fecha seleccionada:", this.value());
            }
        });


        $("#dtpHoraEntradaCompensacion_registroDetalle").kendoTimePicker({
            format: "HH:mm", // Formato de 24 horas
            interval: 15,    // Intervalo de 15 minutos
            value: new Date(), // Valor por defecto (hora actual)
            change: function () {
                // Acción que ocurre cuando cambia la hora
                var selectedTime = this.value();
                console.log("Hora seleccionada: ", selectedTime);
            }
        });

        $("#dtpHoraSalidaCompensacion_registroDetalle").kendoTimePicker({
            format: "HH:mm", // Formato de 24 horas
            interval: 15,    // Intervalo de 15 minutos
            value: new Date(), // Valor por defecto (hora actual)
            change: function () {
                // Acción que ocurre cuando cambia la hora
                var selectedTime = this.value();
                console.log("Hora seleccionada: ", selectedTime);
            }
        });

        $("#dtpFechaCompensacionIzquierda_registro").kendoCalendar({
            componentType: "modern"
        });               

        $("#dtpFechaCompensacionDerecha_registro").kendoCalendar({
            componentType: "modern",
            change: function () {
                // Obtener la fecha seleccionada actual
                this.selectedDatesCompensaciones = [];
                var selectedDate = this.value();

                if (selectedDate) {
                    // Verificar si la fecha ya está seleccionada
                    var index = selectedDatesCompensaciones.indexOf(kendo.toString(selectedDate, 'dd/MM/yyyy'));

                    if (index === -1) {
                        // Si la fecha no está en el array, agregarla
                        selectedDatesCompensaciones.push(kendo.toString(selectedDate, 'dd/MM/yyyy'));
                    } else {
                        // Si ya está seleccionada, eliminarla (deseleccionar)
                        selectedDatesCompensaciones.splice(index, 1);
                    }
                }
            },
            select: function () {
                ////debugger;
                // Configura un array para las fechas seleccionadas.
                //var selectedDates = this.selectedDates();

            }
        });

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

        $('#divModal').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Registro horas de Sobretiempo',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModal').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
                frmHorasCompensables.reset();
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
            title: 'Aprobar horas de Sobretiempo',
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
            title: 'Denegar horas de Sobretiempo',
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
            title: 'Historial Horas de Sobretiempo',
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

        $('#divModalCompensacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '90%',
            height: 'auto',
            title: 'Registrar Nueva Compensacion',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModalCompensacion').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
            }
        });
        $('#divModalCompensacion').data('kendoWindow').center();

        $('#chkExacto_registro').kendoCheckBox({
            label: "Exacto"
        });

        $('#chkDiaCompleto_registroDetalle').kendoCheckBox({
            label: "Dia Completo"
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

        $("#txtDescripcion_registro").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "Descripcion",
        });

        $("#txtHistorial_visualizar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial",
            readonly: true
        });

        $("#txtHoras_Registro").kendoTextBox();

        if (this.Empleado != null) {
            $("#ddlDependencia_busqueda").data("kendoDropDownList").value(this.Empleado.IdDependencia);
            onChangeDependencia(this.Empleado.IdDependencia);
            $("#ddlEmpleado_busqueda").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
            $("#dllEmpleado_registro").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
        }

        frmHorasCompensables = $("#frmRegistroNuevo").kendoValidator().data("kendoValidator");
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


    this.HorasCompensablesJS.prototype.inicializarGridMaestro = function () {
        var _this = this;
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Compensaciones/ListarCompensaciones',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    ////debugger;
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
                    ////debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "ICodCompensaciones",
                    fields: {
                        ICodCompensaciones: { type: "number" },
                        vNumeroDocumento: { type: "string" },
                        vNombreTrabajador: { type: "string" },
                        DtAuditCreacion: { type: "string", parse: _this.parseDate },
                        DtFechaCompensacion: { type: "string", parse: _this.parseDate },
                        Horas: { type: "string" },                       
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
                fileName: "Listado Horas Sobretiempo.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,
            filterable:true,
            dataType: 'json',
            columns: [

                {
                    // Columna para seleccionar una fila
                    field: "ICodCompensaciones",
                    title: "Seleccionar",
                    width: "20px",
                    headerTemplate: function (item) {
                        return '<input id="select-row-all" class="k-checkbox k-checkbox-md k-rounded-md" data-role="checkbox" aria-label="Select all rows" aria-checked="false" type="checkbox">';
                    },
                    template: function (item) {
                        ////debugger;
                        var ret = "";
                        if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA) {
                            if (item.ICodEstadoProceso == 1 || item.ICodEstadoProceso == 6) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodcompensaciones="' + item.ICodCompensaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if (item.ICodEstadoProceso == 1 || item.ICodEstadoProceso == 2 || item.ICodEstadoProceso == 6 || item.ICodEstadoProceso == 7) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodcompensaciones="' + item.ICodCompensaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
                            }
                        }
                        return ret;
                    },
                    filterable: false,
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
                    field: "DtAuditCreacion",
                    title: "FECHA REGISTRO",
                    width: "100px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "DtFechaCompensacion",
                    title: "FECHA",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "Horas",
                    title: "HORAS",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "VDescripcion",
                    title: "DESCRIPCION",
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
                    field: "ICodCompensaciones",
                    title: "Acciones",
                    width: "150px",
                    template: function (item) {

                        var ret = "<center>";
                        if (_this.PERFIL == _this.PERFIL_EMPLE_CTRL_ASISTENCIA) {
                            if (item.ICodEstadoProceso == 1 || item.ICodEstadoProceso == 3 || item.ICodEstadoProceso == 5) {
                                ret += "<button title='Editar'  style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.ICodCompensaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA || _this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if ((_this.codUsuario == item.VAuditCreacion) && (item.ICodEstadoProceso == 1 || item.ICodEstadoProceso == 3 || item.ICodEstadoProceso == 5)) {
                                ret += "<button title='Editar' style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.ICodCompensaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        if ((_this.codUsuario == item.VAuditCreacion) && (item.ICodEstadoProceso == 3 || item.ICodEstadoProceso == 5)) {
                            ret += "<button title='Ver Historial' style='margin-right:3px' class='btn btn-success btn-xs' onclick='controlador.abrirModalHistorial(" + item.ICodCompensaciones + ")'><span class='glyphicon glyphicon-comment' aria-hidden='true'></span></button>";
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
            excelExport: function (e) {
                //var sheet = e.workbook.sheets[0];
                //var template = kendo.template(this.columns[4].template);
            }
        }).data();
        //}


        this.datosCompensacion = [];
        this.datosCompensacion  = new kendo.data.DataSource({
            data: [],
            schema: {
                model: {
                    fields: {
                        NroDia: { type: "number" },
                        Fecha: { type: "date" },
                        Minutos: { type: "number" },
                        Hora: { type: "string" },
                        Entrada: { type: "string" },
                        HoraSalida: { type: "string" }
                    }
                }
            }
        });


        $("#divGridCompensacion").kendoGrid({
            dataSource: this.datosCompensacion , // Aquí va tu fuente de datos
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,
            dataType: 'json',
            columns: [
                {
                    field: "NroDia", // NroDia columna
                    title: "NRO DIA",
                    width: "80px"
                },
                {
                    field: "Fecha", // Fecha columna
                    title: "FECHA",
                    width: "100px",
                    template: function (item) {
                        if (item.Fecha) {
                            return kendo.toString(kendo.parseDate(item.Fecha), 'dd/MM/yyyy');
                        }
                        return "";
                    }
                },
                {
                    field: "Minutos", // Minutos columna
                    title: "MINUTOS",
                    width: "80px"
                },
                {
                    field: "Entrada", // Entrada columna
                    title: "ENTRADA",
                    width: "100px"
                },
                {
                    field: "Salida", // Hora Salida columna
                    title: "SALIDA",
                    width: "100px"
                },
                {
                    field: "Observacion", // Hora Salida columna
                    title: "Observacion",
                    width: "100px"
                },
                {
                    // Columna para el botón de acción
                    field: "Acciones",
                    title: "Acciones",
                    width: "100px",
                    template: function (item) {
                        return "<button type='button' class='k-button' onclick='abrirPopup(" + JSON.stringify(item) + ")'>Ver</button>";
                    }
                }
            ]
        });
    };



    $("#divGrid").on("change", ".select-row", function () {
        var allChecked = $("#divGrid input[type='checkbox'].select-row").length === $("#divGrid input[type='checkbox'].select-row:checked").length;
        $("#select-row-all").prop("checked", allChecked);
    });

    this.HorasCompensablesJS.prototype.buscar = function (e) {
        e.preventDefault();
        ////debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
        $("#select-row-all").prop("checked", false)
        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.HorasCompensablesJS.prototype.abrirModalNuevo = function () {

        $('#divModal_wnd_title').text('Registro horas de Sobretiempo');
        //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
        $('#chkActivo_registro').prop('checked', false);
        $('#divModal').data('kendoWindow').open();
    }

    this.HorasCompensablesJS.prototype.closeModalNuevo = function () {
        $('#divModal').data('kendoWindow').close();
    }

    this.HorasCompensablesJS.prototype.abrirModalAprobar = function () {
        debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodcompensaciones"));
        });

        if (vIds.length > 0) {

            if (vIds.length == 1) {
                $('#divHistorial_aprobar').show();
                var id = vIds[0];

                var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/ListarCompensacionesProcesoHistorial';

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodCompensaciones: id },
                    success: function (res) {
                        var historialComentarios = [];
                        for (var i = 0; i < res.length; i++) {
                            historialComentarios.push(res[i].VComentario);
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

    this.HorasCompensablesJS.prototype.closeModalAprobar = function () {
        $('#divModalAprobar').data('kendoWindow').close();
    }

    this.HorasCompensablesJS.prototype.abrirModalDenegar = function () {
        //debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodcompensaciones"));
        });

        if (vIds.length > 0) {
            if (vIds.length == 1) {
                $('#divHistorial_denegar').show();

                var id = vIds[0];
                var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/ListarCompensacionesProcesoHistorial';

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodCompensaciones: id },
                    success: function (res) {
                        var historialComentarios = [];
                        for (var i = 0; i < res.length; i++) {
                            historialComentarios.push(res[i].VComentario);
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

    this.HorasCompensablesJS.prototype.closeModalDenegar = function () {
        $('#divModalDenegar').data('kendoWindow').close();
    }

    this.HorasCompensablesJS.prototype.abrirModalHistorial = function (icodCompensaciones) {
        //debugger;
        if (icodCompensaciones > 0) {
            var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/ListarCompensacionesProcesoHistorial';

            $.ajax({
                url: urlMetodo,
                type: 'GET',
                dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                contentType: 'application/json',  // Especificamos que enviamos JSON
                data: { iCodCompensaciones: icodCompensaciones },
                success: function (res) {
                    var historialComentarios = [];
                    for (var i = 0; i < res.length; i++) {
                        historialComentarios.push(res[i].VComentario);
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

    this.HorasCompensablesJS.prototype.closeModalHistorial = function () {
        $('#divModalHistorial').data('kendoWindow').close();
    }

    this.HorasCompensablesJS.prototype.closeModalCompensacionDetalle = function () {
        limpiarTodo();
        $('#divModalCompensacion').data('kendoWindow').close();
        $('#hdIdCompensacion').val(0);
        $('#hdIdEstadoProceso').val(0);
        $('#hdHorasFaltantes').val(0);
        $('#divGrid').data("kendoGrid").dataSource.page(1);
        $('#divModalCompensacion').data('kendoWindow').close();
    }

    this.HorasCompensablesJS.prototype.grabar = function (e) {
        e.preventDefault();
        //debugger;

        if (frmHorasCompensables.validate()) {
            var compensacion = compensacionObjeto($, 0);
            //debugger;
            // Crear una instancia de FormData
            var data_param = new FormData();

            // Agregar propiedades simples
            data_param.append('ICodCompensaciones', compensacion.ICodCompensaciones);
            data_param.append('ICodTrabajador', compensacion.ICodTrabajador);
            data_param.append('ICodigoDependencia', compensacion.ICodigoDependencia);
            data_param.append('ICodEstadoProceso', compensacion.ICodEstadoProceso);
            data_param.append('Exacto', compensacion.Exacto);
            data_param.append('Horas', compensacion.Hora);
            data_param.append('DtFechaCompensacion', compensacion.DtFechaCompensacion.toISOString());
            data_param.append('BEstado', compensacion.BEstado);
            data_param.append('VDescripcion', compensacion.VDescripcion);

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/GrabarCompensacionTrabajador';

            $.ajax({
                url: urlMetodo,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {

                    //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
                    $('#txtHoras_Registro').val("");
                    $('#chkExacto_registro').prop('checked', false);
                    $('#dtpFechaCompensacion_registro').data("kendoDatePicker").value(null);
                    $("#txtDescripcion_registro").val("");

                    $('#divModal').data('kendoWindow').close();
                    $('#hdIdCompensacion').val(0);
                    $('#hdIdEstadoProceso').val(0);
                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    this.HorasCompensablesJS.prototype.limpiarDatos = function () {
        this.datosCompensacion = [];  // Limpiamos el arreglo
    };

    this.HorasCompensablesJS.prototype.agregarCompensacion = function () {
        //debugger;
        var calendar = $("#dtpFechaCompensacionDerecha_registro").data("kendoCalendar");
        var selectedDates = calendar.value();
        if (selectedDates) {

            var horasIzquierda = $('#txtTotalHorasSobretiempoIzquierda_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalHorasSobretiempoIzquierda_Registro').val());
            var minutosIzquierda = $('#txtTotalMinutosSobretiempoIzquierda_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalMinutosSobretiempoIzquierda_Registro').val()) / 60;
            var sumaHoraMinutosIzquierda = horasIzquierda + minutosIzquierda;

            var horasDerecha = $('#txtTotalHorasSobretiempoDerecha_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalHorasSobretiempoDerecha_Registro').val());
            var minutosDerecha = $('#txtTotalMinutosSobretiempoDerecha_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalMinutosSobretiempoDerecha_Registro').val()) / 60;
            var sumaHoraMinutosDerecha = horasDerecha + minutosDerecha;

            var valorHorasFaltantes = parseFloat($('#hdHorasFaltantes').val());

            if (sumaHoraMinutosIzquierda == valorHorasFaltantes) {
                controladorApp.notificarMensajeDeAlerta('No puede ingresar horas a la compensacion');
                $("#txtTotalHorasSobretiempoDerecha_Registro").focus();
                return;
            }

            // Obtener todos los elementos (sin paginación)
            var allData = $("#divGridCompensacion").data("kendoGrid").dataSource.data();//dataSource.data();
            var lastItem;
            var lastNroDia;
            lastItem = allData[allData.length - 1];


            if (sumaHoraMinutosIzquierda >= (valorHorasFaltantes + sumaHoraMinutosDerecha)) {
                $('#hdHorasFaltantes').val(parseFloat(valorHorasFaltantes + sumaHoraMinutosDerecha))
                if (allData.length > 0) {
                    lastNroDia = lastItem.NroDia + 1;
                }
                else {
                    lastNroDia = 1;
                }


                var nuevoNroDia = lastNroDia;
                var nuevaFecha = kendo.toString(selectedDates, 'dd/MM/yyyy');
                var nuevosMinutos = sumaHoraMinutosDerecha * 60;
                var nuevaEntrada = "";
                var nuevaHoraSalida = "";

                // Crear el nuevo objeto JSON
                var nuevoDato = {
                    NroDia: nuevoNroDia,
                    Fecha: nuevaFecha,
                    Minutos: nuevosMinutos,
                    Entrada: nuevaEntrada,
                    Salida: nuevaHoraSalida,
                    Observacion: ''
                };

                this.datosCompensacion.add(nuevoDato);

                // Refrescar el grid para que muestre los nuevos datos
                $("#divGridCompensacion").data("kendoGrid").refresh();
                this.selectedDatesCompensaciones = [];

            }
            else {
                controladorApp.notificarMensajeDeAlerta('No puede ingresar horas de mas a la compensacion');
                $("#txtTotalHorasSobretiempoDerecha_Registro").focus();
                return;
            }
            $("#txtTotalHorasSobretiempoDerecha_Registro").val('');
            $("#txtTotalMinutosSobretiempoDerecha_Registro").val('');
        }            
    }

    this.HorasCompensablesJS.prototype.aprobar = function (e) {
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
                vIds.push($(this).attr("data-icodcompensaciones"));
            });

            var data_param = new FormData();

            data_param.append('vIds', vIds.join(','));
            data_param.append('vDescripcion', observaciones);
            data_param.append('aprobarDenegar', aprobarDenegar);

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/AprobarDenegarCompensacionTrabajadorMas';

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
                    $('#hdIdCompensacionAprobar').val(0);
                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                    $("#select-row-all").prop("checked", false);
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    //this.HorasCompensablesJS.prototype.aprobar = function (e) {
    //    e.preventDefault();
    //    //debugger;
    //    var compensacion = compensacionObjeto($, 0);
    //    compensacion.ICodCompensaciones = $('#hdIdCompensacionAprobar').val();
    //    // Crear una instancia de FormData
    //    var data_param = new FormData();

    //    // Agregar propiedades simples
    //    data_param.append('ICodCompensaciones', compensacion.ICodCompensaciones);
    //    data_param.append('ICodTrabajador', compensacion.ICodTrabajador);
    //    data_param.append('ICodigoDependencia', compensacion.ICodigoDependencia);
    //    data_param.append('ICodEstadoProceso', compensacion.ICodEstadoProceso);
    //    data_param.append('BExacto', compensacion.BExacto);

    //    data_param.append('vDescripcion', compensacion.VDescripcion)
    //    data_param.append('aprobarDenegar', compensacion.aprobarDenegar);

    //    var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/AprobarDenegarCompensacionTrabajador';

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
    //            $('#divModalAprobar').data('kendoWindow').center().close();
    //            $('#divModalDenegar').data('kendoWindow').center().close();

    //            $('#hdIdCompensacionAprobar').val(0);
    //            $('#divGrid').data("kendoGrid").dataSource.page(1);
    //        },
    //        error: function (res) {
    //            //alert(res);
    //        }
    //    });
    //}

    this.HorasCompensablesJS.prototype.aceptarCambioCompensacion = function () {
        var itemEditado = window.itemEditando;

        // Obtén los valores editados
        
        itemEditado.Observacion = $('#txtDescripcion_registroDetalle').val();

        // Actualiza el DataSource de la grilla
        //var dataSource = $("#divGridCompensacion").data("kendoGrid").dataSource;
        //var dataItem = dataSource.get(itemEditado.NroDia); // Encuentra el ítem por un identificador único

        var dataSource = $("#divGridCompensacion").data("kendoGrid").dataSource;

        // Encuentra el ítem en el DataSource usando el método .find()
        var dataItem = dataSource.data().find(function (item) {
            return item.NroDia === itemEditado.NroDia; // Filtra por el NroDia
        });


        if (dataItem) {

            if ($('#chkDiaCompleto_registroDetalle').prop('checked')) {
                itemEditado.Fecha = kendo.toString($('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(), 'dd/MM/yyyy');
                itemEditado.Entrada = kendo.toString($('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(), 'dd/MM/yyyy');
                itemEditado.Salida = kendo.toString($('#dtpHoraSalidaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(), 'dd/MM/yyyy');
                itemEditado.Minutos = $('#txtTotalHorasSobretiempoDerecha_registroDetalle_Completo').val();
            }
            else {
                itemEditado.Entrada = devolverFormatoHorasMinutos($('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker").value());
                itemEditado.Salida = devolverFormatoHorasMinutos($('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker").value());
                itemEditado.Minutos = $('#txtTotalMinutosSobretiempoDerecha_registroDetalle').val();
                itemEditado.Fecha = $('#dtpFechaCompensacion_registroDetalle').data("kendoDatePicker").value();
            }
            dataItem.set("Fecha", itemEditado.Fecha);
            dataItem.set("Minutos", itemEditado.Minutos);
            dataItem.set("Entrada", itemEditado.Entrada);
            dataItem.set("Salida", itemEditado.Salida);
            dataItem.set("Observacion", itemEditado.Observacion);

            $('#dtpFechaCompensacion_registroDetalle').data("kendoDatePicker").value(null);
            $('#txtTotalMinutosSobretiempoDerecha_registroDetalle').val('');
            $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
            $('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
            $('#txtDescripcion_registroDetalle').val('');
        }
    }

    this.HorasCompensablesJS.prototype.convertirHorasMinutosDerecha = function () {
        var valor = $('#txtTotalHorasSobretiempoDerecha_Registro').val();

        // Verificamos que el valor ingresado sea un número
        if (!isNaN(valor) && valor !== "") {
            var totalMinutos = parseInt(valor);
            var horas = Math.floor(totalMinutos / 60); // Calcular las horas
            var minutos = totalMinutos % 60; // Calcular los minutos restantes

            // Mostramos el resultado
            $('#txtTotalMinutosSobretiempoDerecha_Registro').val(minutos);
        } else {
            // Si no es un número válido, dejamos el campo vacío
            $('#txtTotalHorasSobretiempoDerecha_Registro').val('');
        }
    }

    this.HorasCompensablesJS.prototype.convertirMinutosHorasDerecha = function () {

        var valor = $('#txtTotalMinutosSobretiempoDerecha_Registro').val();

        // Verificamos que el valor ingresado sea un número
        if (!isNaN(valor) && valor !== "") {
            var totalMinutos = parseInt(valor);
            var horas = Math.floor(totalMinutos / 60); // Calcular las horas
            var minutos = totalMinutos % 60; // Calcular los minutos restantes

            // Mostramos el resultado
            $('#txtTotalHorasSobretiempoDerecha_Registro').val(horas);
        } else {
            // Si no es un número válido, dejamos el campo vacío
            $('#txtTotalMinutosSobretiempoDerecha_Registro').val('');
        }

    }

    this.HorasCompensablesJS.prototype.aceptarDiaCompleto = function () {
        //debugger;
        if ($('#chkDiaCompleto_registroDetalle').prop('checked')) {
            // Si el checkbox está marcado
            var self = this;
            var diasColocarGrid = parseInt($('#txtTotalHorasSobretiempoIzquierda_Registro').val()/ 8);

            if (diasColocarGrid == 0) {
                controladorApp.notificarMensajeDeAlerta('La cantidad de horas asignadas no son suficientes para el dia completo');
                $("#txtTotalHorasSobretiempoDerecha_Registro").focus();
                $('#chkDiaCompleto_registroDetalle').prop('checked', false);
                $('#diaCompleto').hide();
                $('#diaNoCompelto').show();
                return;
            }      

            var horasIzquierda = $('#txtTotalHorasSobretiempoIzquierda_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalHorasSobretiempoIzquierda_Registro').val());
            var minutosIzquierda = $('#txtTotalMinutosSobretiempoIzquierda_Registro').val() == '' ? parseFloat(0) : parseFloat($('#txtTotalMinutosSobretiempoIzquierda_Registro').val()) / 60;
            var sumaHoraMinutosIzquierda = horasIzquierda + minutosIzquierda;

            var horasDerecha = parseFloat(8);
            var minutosDerecha = parseFloat(0);
            var sumaHoraMinutosDerecha = horasDerecha + minutosDerecha;

            var valorHorasFaltantes = parseFloat($('#hdHorasFaltantes').val());


            for (var i = 0; i < diasColocarGrid; i++) {
                var calendar = $("#dtpFechaCompensacionDerecha_registro").data("kendoCalendar");
                var selectedDates = calendar.value();

                // Obtener todos los elementos (sin paginación)
                var allData = $("#divGridCompensacion").data("kendoGrid").dataSource.data();//dataSource.data();
                var lastItem;
                var lastNroDia;
                lastItem = allData[allData.length - 1];
                $('#hdHorasFaltantes').val(parseFloat(valorHorasFaltantes + sumaHoraMinutosDerecha))
                if (allData.length > 0) {
                    lastNroDia = lastItem.NroDia + 1;
                }
                else {
                    lastNroDia = 1;
                }

                var nuevoNroDia = lastNroDia;

                var nuevaFecha;
                var contadorInterno = 0;
                if (i == 0) {
                    nuevaFecha = kendo.toString(selectedDates, 'dd/MM/yyyy');
                }
                else {
                    contadorInterno += 1;
                    selectedDates.setDate(selectedDates.getDate() + contadorInterno);
                    nuevaFecha = kendo.toString(selectedDates, 'dd/MM/yyyy');
                }

                var nuevosMinutos = sumaHoraMinutosDerecha * 60;
                var nuevaEntrada = nuevaFecha;
                var nuevaHoraSalida = nuevaFecha;

                // Crear el nuevo objeto JSON
                var nuevoDato = {
                    NroDia: nuevoNroDia,
                    Fecha: nuevaFecha,
                    Minutos: nuevosMinutos,
                    Entrada: nuevaEntrada,
                    Salida: nuevaHoraSalida,
                    Observacion: ''
                };

                self.datosCompensacion.add(nuevoDato);
                // Refrescar el grid para que muestre los nuevos datos
                $("#divGridCompensacion").data("kendoGrid").refresh();
                this.selectedDatesCompensaciones = [];
                $("#txtTotalHorasSobretiempoDerecha_Registro").val('');
                $("#txtTotalMinutosSobretiempoDerecha_Registro").val('');
            }


            $('#diaCompleto').show();
            $('#diaNoCompelto').hide();

        } else {


            $('#dtpFechaCompensacion_registroDetalle').data("kendoDatePicker").value(null);
            $('#txtTotalMinutosSobretiempoDerecha_registroDetalle').val('');
            $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
            $('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
            $('#txtDescripcion_registroDetalle').val('');
            $('#txtTotalHorasSobretiempoDerecha_Registro').val('');
            $('#txtTotalMinutosSobretiempoDerecha_Registro').val('');
            $('#txtTotalHorasSobretiempoIzquierda_Registro').val('');            


            $('#txtTotalHorasSobretiempoDerecha_registroDetalle_Completo').val('');
            $('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);
            $('#dtpHoraSalidaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);

            $('#chkDiaCompleto_registroDetalle').prop('checked', false);

            var grid = $("#divGridCompensacion").data("kendoGrid");
            var dataSource = grid.dataSource;
            // Limpiar los datos del DataSource
            dataSource.data([]);

            // Si el checkbox NO está marcado
            $('#diaCompleto').hide();
            $('#diaNoCompelto').show();
        }
    }

    this.HorasCompensablesJS.prototype.grabarDetalleCompensacion = function () {
        

        //debugger;

        var grid = $("#divGridCompensacion").data("kendoGrid");

        // Obtener todos los datos del dataSource del Grid
        var allData = grid.dataSource.data();


        for (var i = 0; i < allData.length; i++) {
            var compensacion = compensacionDetalleObjeto(allData[i]);
            // Crear una instancia de FormData
            var data_param = new FormData();

            // Agregar propiedades simples
            data_param.append('ICodCompensaciones', compensacion.ICodCompensaciones);
            data_param.append('INroDia', compensacion.INroDia);
            data_param.append('BDiaCompleto', compensacion.BDiaCompleto);
            data_param.append('IMinutos', compensacion.IMinutos);
            data_param.append('IHoras', compensacion.IHoras);
            data_param.append('DtFechaHoraIni', compensacion.DtFechaHoraIni);
            data_param.append('DtFechaHoraFin', compensacion.DtFechaHoraFin);
            data_param.append('BEstado', true);
            data_param.append('VComentario', compensacion.VComentario);

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Compensaciones/GrabarDetalleCompensacionTrabajador';

            $.ajax({
                url: urlMetodo,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    alert(res);
                    if (res) {
                        /*limpiarTodo();*/
                        $('#divModalCompensacion').data('kendoWindow').center().close();
                        //$('#hdIdCompensacion').val(0);
                        $('#hdIdEstadoProceso').val(0);
                        $('#hdHorasFaltantes').val(0);
                        $('#divGrid').data("kendoGrid").dataSource.page(1);
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
        }
    }

    this.HorasCompensablesJS.prototype.editar = function (icodCompensaciones) {
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Compensaciones/ObtenerCompensacionPorId',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodCompensaciones: icodCompensaciones },
            success: function (res) {
                //debugger;
                if (res) {
                    $('#hdIdCompensacion').val(icodCompensaciones);
                    $("#dllEmpleado_registro").data("kendoDropDownList").value(res.ICodTrabajador);
                    $('#txtHoras_Registro').val(res.Horas);
                    $('#chkExacto_registro').prop('checked', res.Exacto === true);
                    $('#dtpFechaCompensacion_registro').data("kendoDatePicker").value(res.DtFechaCompensacion);
                    $("#txtDescripcion_registro").val(res.VDescripcion);
                    $('#hdIdEstadoProceso').val(res.ICodEstadoProceso);
                    $('#divModal_wnd_title').text('Modificar Compensacion');
                    $('#divModal').data('kendoWindow').open();
                }
            },
            error: function (xhr, status, error) {
                // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                console.error('Error en la solicitud:', status, error);
                // Puedes mostrar un mensaje de error en el UI si lo deseas.
            }
        });
    }

    this.HorasCompensablesJS.prototype.compensar = function (icodCompensaciones) {
        limpiarTodo();

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Compensaciones/ObtenerCompensacionPorId',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodCompensaciones: icodCompensaciones },
            success: function (res) {
                //debugger;
                if (res) {
                    $('#hdIdCompensacion').val(icodCompensaciones);
                    $('#hdHorasFaltantes').val(parseFloat(res.IHoras));
                    $('#txtTotalHorasSobretiempoIzquierda_Registro').val(res.Horas);
                    $('#dtpFechaCompensacionIzquierda_registro').data("kendoCalendar").value(res.DtFechaCompensacion);

                    var fechaHoy = new Date();
                    $('#dtpFechaCompensacionDerecha_registro').data("kendoCalendar").value(fechaHoy);

                    $('#hdIdEstadoProceso').val(res.ICodEstadoProceso);
                    $('#divModalCompensacion').data('kendoWindow').open();
                }
            },
            error: function (xhr, status, error) {
                // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                console.error('Error en la solicitud:', status, error);
                // Puedes mostrar un mensaje de error en el UI si lo deseas.
            }
        });
    }

    this.HorasCompensablesJS.prototype.compensar_all = function () {
        limpiarTodo();

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Compensaciones/ObtenerCompensacionPorId',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodCompensaciones: icodCompensaciones },
            success: function (res) {
                //debugger;
                if (res) {
                    $('#hdIdCompensacion').val(icodCompensaciones);
                    $('#hdHorasFaltantes').val(parseFloat(res.IHoras));
                    $('#txtTotalHorasSobretiempoIzquierda_Registro').val(res.Horas);
                    $('#dtpFechaCompensacionIzquierda_registro').data("kendoCalendar").value(res.DtFechaCompensacion);

                    var fechaHoy = new Date();
                    $('#dtpFechaCompensacionDerecha_registro').data("kendoCalendar").value(fechaHoy);

                    $('#hdIdEstadoProceso').val(res.ICodEstadoProceso);
                    $('#divModalCompensacion').data('kendoWindow').open();
                }
            },
            error: function (xhr, status, error) {
                // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                console.error('Error en la solicitud:', status, error);
                // Puedes mostrar un mensaje de error en el UI si lo deseas.
            }
        });
    }

    this.HorasCompensablesJS.prototype.parseDate = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy');
        }
        return "";
    }

    this.HorasCompensablesJS.prototype.parseDateTime = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy HH:mm');
        }
        return "";
    }
    

}(jQuery));

function devolverFormatoHorasMinutos(item) {
    var horaSalida = item;
    // Extraer solo la hora en formato de 24 horas
    var horas = horaSalida ? horaSalida.getHours() : null;

    // Si necesitas también los minutos (por ejemplo, en formato 'HH:mm'):
    var minutos = horaSalida ? horaSalida.getMinutes() : null;
    var horaCompleta = horaSalida ? (horas < 10 ? '0' + horas : horas) + ':' + (minutos < 10 ? '0' + minutos : minutos) : null;
    return horaCompleta;
}

function compensacionObjeto($, iCodJustificaciones) {
    var ICodCompensaciones = 0;
    var ICodEstadoProceso = 0;
    var varFecha = null;
    var aprobarDenegar = false;

    varFecha = $('#dtpFechaCompensacion_registro').data("kendoDatePicker").value();

    if ($("#hdIdCompensacion").val() > 0) {
        ICodCompensaciones = $("#hdIdCompensacion").val();
        ICodEstadoProceso = $("#hdIdEstadoProceso").val();
    }


    observaciones = $('#txtDescripcion_registro').val();
    if ($('#divModalDenegar').is(':visible')) {
        observaciones = $('#txtObservaciones_denegar').val();
        aprobarDenegar = false;
    } else if ($('#divModalAprobar').is(':visible')) {
        aprobarDenegar = true;
        observaciones = $('#txtObservaciones_aprobar').val();
    }

    return {
        ICodCompensaciones: ICodCompensaciones,
        ICodTrabajador: $("#dllEmpleado_registro").data("kendoDropDownList").value(),
        ICodigoDependencia: $("#ddlDependencia_busqueda").data("kendoDropDownList").value(),
        ICodEstadoProceso: ICodEstadoProceso,
        Exacto: $('#chkExacto_registro').prop('checked'),
        Hora: $('#txtHoras_Registro').val(),
        DtFechaCompensacion: varFecha,
        bEstado: true,
        aprobarDenegar: aprobarDenegar,
        VDescripcion: observaciones,
        Procesos: [
        ]
    };
}

function compensacionDetalleObjeto(item) {
    //debugger;
    var timePickerEntrada;
    var timePickerSalida;
    var fechaString;
    var fechaParts;
    var dateEntrada;
    var dateSalida;
    var DtFechaHoraIni;
    var DtFechaHoraFin;
    var diaCompleto = false;
    if ($('#chkDiaCompleto_registroDetalle').prop('checked')) {

        timePickerEntrada = $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker");

        fechaString = kendo.toString(item.Fecha, 'dd/MM/yyyy'); // La fecha en formato 'dd/MM/yyyy'
        fechaParts = fechaString.split('/'); // Separar la fecha por '/'
        //var dateEntrada = new Date(fechaParts[2], fechaParts[1] - 1, fechaParts[0]);
        dateEntrada = new Date(fechaParts[2], fechaParts[1] - 1, fechaParts[0]);

        timePickerEntrada.value(dateEntrada);

        DtFechaHoraIni = timePickerEntrada.value().toISOString();
        DtFechaHoraFin = timePickerEntrada.value().toISOString();
        diaCompleto = true;

    }
    else {

        timePickerEntrada = $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker");
        timePickerSalida = $('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker");

        fechaString = kendo.toString(item.Fecha, 'dd/MM/yyyy'); // La fecha en formato 'dd/MM/yyyy'
        fechaParts = fechaString.split('/'); // Separar la fecha por '/'
        //var dateEntrada = new Date(fechaParts[2], fechaParts[1] - 1, fechaParts[0]);
        dateEntrada = new Date(fechaParts[2], fechaParts[1] - 1, fechaParts[0]);

        dateEntrada.setHours(item.Entrada.split(':')[0]);  // Establecer las horas
        dateEntrada.setMinutes(item.Entrada.split(':')[1]); // Establecer los minutos

        timePickerEntrada.value(dateEntrada);


        dateSalida = new Date(fechaParts[2], fechaParts[1] - 1, fechaParts[0]);
        dateSalida.setHours(item.Salida.split(':')[0]);  // Establecer las horas
        dateSalida.setMinutes(item.Salida.split(':')[1]); // Establecer los minutos

        timePickerSalida.value(dateSalida);

        DtFechaHoraIni = timePickerEntrada.value().toISOString();
        DtFechaHoraFin = timePickerSalida.value().toISOString();

    }
    // Crear un objeto Date y establecer la hora a las 08:30    

    var ICodCompensaciones = $("#hdIdCompensacion").val();
    var INroDia = item.NroDia;
    var BDiaCompleto = diaCompleto;
    var IMinutos = parseInt(item.Minutos);
    var IHoras = parseFloat(item.Minutos) / 60;

    

    var VComentario = item.Observacion;


    return {
        ICodCompensaciones: ICodCompensaciones,
        INroDia: INroDia,
        BDiaCompleto: BDiaCompleto,
        IMinutos: IMinutos,
        IHoras: IHoras,
        DtFechaHoraIni: DtFechaHoraIni,
        DtFechaHoraFin: DtFechaHoraFin,
        VComentario: VComentario
    };
}

function abrirPopup(item) {
    //debugger;
    $('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);
    $('#dtpHoraSalidaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);
    if (item) {

        if ($('#chkDiaCompleto_registroDetalle').prop('checked')) {
            $('#txtTotalHorasSobretiempoDerecha_registroDetalle_Completo').val(item.Minutos);
            $('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(kendo.parseDate(item.Fecha, 'dd/MM/yyyy'));
            $('#dtpHoraSalidaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(kendo.parseDate(item.Fecha, 'dd/MM/yyyy'));
            $('#txtDescripcion_registroDetalle').val(item.Observacion);
        }
        else {
            $('#dtpFechaCompensacion_registroDetalle').data("kendoDatePicker").value(kendo.parseDate(item.Fecha, 'dd/MM/yyyy'));
            $('#txtTotalMinutosSobretiempoDerecha_registroDetalle').val(item.Minutos);
            $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker").value(item.Entrada == '' ? null : item.Entrada);
            $('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker").value(item.Salida == '' ? null : item.Salida);
            $('#txtDescripcion_registroDetalle').val(item.Observacion);
        }
        window.itemEditando = item;
    }
}


function limpiarTodo() {
    $('#dtpFechaCompensacion_registroDetalle').data("kendoDatePicker").value(null);
    $('#txtTotalMinutosSobretiempoDerecha_registroDetalle').val('');
    $('#dtpHoraEntradaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
    $('#dtpHoraSalidaCompensacion_registroDetalle').data("kendoTimePicker").value(null);
    $('#txtDescripcion_registroDetalle').val('');
    $('#txtTotalHorasSobretiempoDerecha_Registro').val('');
    $('#txtTotalMinutosSobretiempoDerecha_Registro').val('');
    $('#txtTotalHorasSobretiempoIzquierda_Registro').val('');
    $('#txtTotalMinutosSobretiempoIzquierda_Registro').val('');


    $('#txtTotalHorasSobretiempoDerecha_registroDetalle_Completo').val('');
    $('#dtpHoraEntradaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);
    $('#dtpHoraSalidaCompensacion_registroDetalle_Completo').data("kendoDatePicker").value(null);

    $('#chkDiaCompleto_registroDetalle').prop('checked',false);

    var grid = $("#divGridCompensacion").data("kendoGrid");
    var dataSource = grid.dataSource;
    // Limpiar los datos del DataSource
    dataSource.data([]);
}