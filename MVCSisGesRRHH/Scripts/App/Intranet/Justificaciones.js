(function ($) {
    var frmJustificacion = null;
    var frmRegistroDenegar = null;
    var empleadoSet = null;

    this.JustificacionesJS = function () {
        this.PERFIL = -1;
        this.PERFIL_JEFE_CTRL_ASISTENCIA = '';
        this.PERFIL_EMPLE_CTRL_ASISTENCIA = '';
        this.PERFIL_ADMIN_CTRL_ASISTENCIA = '';
        this.Empleado = null;
        this.codUsuario = 0;
    };
    this.JustificacionesJS.prototype.inicializarMaestro = function () {
        var _this = this;

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



        $("#ddlTipo_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTipoJustificacion",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Justificaciones/ListarTipoJustificacion",
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

        $("#txtFecha_busqueda").kendoDatePicker().data("kendoDatePicker").max(new Date());
        $("#dtpFechaIni_registro").kendoDatePicker().data("kendoDatePicker").value(new Date());
        $("#dtpFechaFin_registro").kendoDatePicker().data("kendoDatePicker").value(new Date());


        $("#dtpFechaHoraIni_registro").kendoDateTimePicker({
            format: "yyyy-MM-dd HH:mm", // Formato de fecha y hora
            max: new Date() // Límite máximo de la fecha y hora
        });

        $("#dtpFechaHoraFin_registro").kendoDateTimePicker({
            format: "yyyy-MM-dd HH:mm", // Formato de fecha y hora
            max: new Date() // Límite máximo de la fecha y hora
        });
        //$("#dtpFechaHoraIni_registro").kendoDatePicker().data("kendoDatePicker").max(new Date());
        //$("#dtpFechaHoraFin_registro").kendoDatePicker().data("kendoDatePicker").max(new Date());



        /* REGISTRO NUEVO */

        $("#dllMotivo_registro").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodMotivoJustificacion",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Justificaciones/ListarMotivoJustificacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                },
                // Añadir una propiedad personalizada (puedes hacerlo directamente en los datos o mediante el `schema`)
                schema: {
                    data: function (response) {
                        // Suponiendo que los datos de respuesta incluyen un campo `customProperty` que quieres agregar
                        // Modificar los datos de la respuesta para incluir la propiedad personalizada
                        return response.map(item => ({
                            ...item,
                            customProperty: item.bConGoce  // Asigna un valor personalizado
                        }));
                    }
                }
            },
            change: function (e) {
                debugger;
                var item = this.dataItem();

                if (item.bConGoce) {
                    $("#ddlTipoGoce_registro").data("kendoDropDownList").value(1);
                }
                else {
                    $("#ddlTipoGoce_registro").data("kendoDropDownList").value(2);
                }

                $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value(item.iCodTipoJustificacion);

                $("#ddlTipoJustificacion_registro").data("kendoDropDownList").enable(!item.bBloquearTipoJustificacion);
                $("#ddlTipoGoce_registro").data("kendoDropDownList").enable(!item.bBloquearGoce);

                _this.changeDateOrTime(item.iCodTipoJustificacion);

                if (item.iCodMotivoJustificacion == 3) {
                    _this.loadEmpleado(function (empleado) {
                        empleadoSet = empleado;
                        //debugger;
                        var fechaMin = kendo.parseDate(empleado.FechaNacimiento, "dd/MM/yyyy");
                        var fechaMax = kendo.parseDate(empleado.FechaNacimiento, "dd/MM/yyyy");;
                        var anioActual = new Date().getFullYear();
                        if (anioActual) {
                            fechaMin.setFullYear(anioActual);
                            fechaMax.setFullYear(anioActual);// Cambia el año a 2025
                        }

                        fechaMax.setDate(fechaMax.getDate() + 4);  // Suma 5 días

                        $("#dtpFechaIni_registro").data("kendoDatePicker").value(fechaMin);
                        $("#dtpFechaFin_registro").data("kendoDatePicker").value(fechaMin);

                        //$("#dtpFechaIni_registro").data("kendoDatePicker").max(fechaMax);
                        //$("#dtpFechaFin_registro").data("kendoDatePicker").max(fechaMax);

                        //$("#dtpFechaIni_registro").data("kendoDatePicker").min(fechaMin);
                        //$("#dtpFechaFin_registro").data("kendoDatePicker").min(fechaMin);
                    });
                }
                else {



                    //$("#dtpFechaIni_registro").data("kendoDatePicker").enable(true);
                    //$("#dtpFechaFin_registro").data("kendoDatePicker").enable(true);
                }



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

        $("#ddlTipoJustificacion_registro").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTipoJustificacion",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Justificaciones/ListarTipoJustificacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                },
                // Añadir una propiedad personalizada (puedes hacerlo directamente en los datos o mediante el `schema`)
                schema: {
                    data: function (response) {
                        // Suponiendo que los datos de respuesta incluyen un campo `customProperty` que quieres agregar
                        // Modificar los datos de la respuesta para incluir la propiedad personalizada
                        return response.map(item => ({
                            ...item,
                            customProperty: item.vDescripcion  // Asigna un valor personalizado
                        }));
                    }
                }
            },
            change: function (e) {
                /*1 DIAS | 2 HORAS | 3 HORA EXACTA*/
                var item = this.dataItem();
                _this.changeDateOrTime(item.iCodTipoJustificacion);
            }
        });

        $("#ddlTipoGoce_registro").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTipoGoce",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Justificaciones/ListarTipoGoce",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        $('#divModal').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '90%',
            height: 'auto',
            title: 'Registro Justificacion',
            visible: false,
            position: {
                top: "50%",         // Centrado vertical
                left: "50%"         // Centrado horizontal
            },
            actions: ["Close"],
            close: function () {
                var modal = $('#divModal').data('kendoWindow');
                modal.center(); // Asegura que el modal se centra nuevamente cuando se cierra
                frmJustificacion.reset();
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
            title: 'Aprobar Justificacion',
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
            title: 'Denegar Justificacion',
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
            title: 'Historial Justificacion',
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
            title: 'Archivos de Justificacion',
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

        $('#chkMadrugada_registro').kendoCheckBox({
            label: "Madrugada"
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
            placeholder: "ingrese historial",
            readonly: true
        });

        $("#txtObservaciones_aprobar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese observacion"
        });

        $("#txtHistorial_denegar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial",
            readonly: true
        });

        $("#txtObservaciones_denegar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese observacion"
        });

        $("#txtHistorial_visualizar").kendoTextArea({
            rows: 5,
            maxLength: 4000,
            placeholder: "ingrese historial",
            readonly: true
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


        $("#lvArchivos").kendoListView({
            dataSource: {
                data: []
            },
            template:
                "<div class='file-item'>" +
                "<span class='glyphicon glyphicon-file' aria-hidden='true'></span>" +
                "<a href='" + controladorApp.obtenerRutaBase() + 'Justificaciones/DescargarArchivo?fileName=' + "#= vUrlArchivo #' target='_blank'>#= vNombre #</a>" +
                "</div>"
        });

        $("#lvArchivos_registro").kendoListView({
            dataSource: {
                data: []
            },
            template:
                "<div class='file-item'>" +
                "<span class='glyphicon glyphicon-file' aria-hidden='true'></span>" +
                "<a href='" + controladorApp.obtenerRutaBase() + 'Justificaciones/DescargarArchivo?fileName=' + "#= vUrlArchivo #' target='_blank'>#= vNombre #</a>" +
                "</div>"
        });

        if (this.Empleado != null) {
            $("#ddlDependencia_busqueda").data("kendoDropDownList").value(this.Empleado.IdDependencia);
            onChangeDependencia(this.Empleado.IdDependencia);
            $("#ddlEmpleado_busqueda").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
            $("#dllEmpleado_registro").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
        }

        frmJustificacion = $("#frmRegistroNuevo").kendoValidator({
            rules: {
                // Validación personalizada para 'control1' y 'control2'
                validateFechaHora: function (input) {
                    var tipoJusti = $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value();

                    if (input.attr("name") === "dtpFechaIni_registro" && tipoJusti === "1") {
                        return input.val() !== "";
                    }
                    if (input.attr("name") === "dtpFechaFin_registro" && tipoJusti === "1") {
                        return input.val() !== "";
                    }

                    if (input.attr("name") === "dtpFechaHoraIni_registro" && tipoJusti === "2") {
                        return input.val() !== "";
                    }
                    if (input.attr("name") === "dtpFechaHoraFin_registro" && tipoJusti === "2") {
                        return input.val() !== "";
                    }

                    if (input.attr("name") === "dtpFechaHoraIni_registro" && tipoJusti === "3") {
                        return input.val() !== "";
                    }
                    if (input.attr("name") === "dtpFechaHoraFin_registro" && tipoJusti === "3") {
                        return input.val() !== "";
                    }

                    return true;
                },
                validateCompararFechaHora: function (input) {
                    var tipoJusti = $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value();
                    var motivo = $("#dllMotivo_registro").data("kendoDropDownList").value();
                    if (motivo != "3") {
                        if (tipoJusti == "1") {
                            if (input.attr("name") === "dtpFechaIni_registro" || input.attr("name") === "dtpFechaFin_registro") {
                                var dateObj1 = $('#dtpFechaIni_registro').data("kendoDatePicker").value();
                                var dateObj2 = $('#dtpFechaFin_registro').data("kendoDatePicker").value();
                                if (dateObj1 && dateObj2) {
                                    return dateObj1 <= dateObj2;
                                }
                            }
                        }
                        else {
                            if (input.attr("name") === "dtpFechaHoraIni_registro" || input.attr("name") === "dtpFechaHoraFin_registro") {
                                var dateObj1 = $('#dtpFechaHoraIni_registro').data("kendoDateTimePicker").value();
                                var dateObj2 = $('#dtpFechaHoraFin_registro').data("kendoDateTimePicker").value();
                                if (dateObj1 && dateObj2) {
                                    return dateObj1 < dateObj2;
                                }
                            }
                        }
                    }
                    return true;
                },
                validateFechaHoraCumple: function (input) {
                    debugger;
                    var tipoJusti = $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value();
                    var motivo = $("#dllMotivo_registro").data("kendoDropDownList").value();
                    if (motivo == "3") {
                        if (tipoJusti == "1") {
                            if (input.attr("name") === "dtpFechaIni_registro" || input.attr("name") === "dtpFechaFin_registro") {
                                var dateObj1 = $('#dtpFechaIni_registro').data("kendoDatePicker").value();
                                var dateObj2 = $('#dtpFechaFin_registro').data("kendoDatePicker").value();
                                if (dateObj1 && dateObj2) {
                                    //debugger;
                                    var fechaMin = kendo.parseDate(empleadoSet.FechaNacimiento, "dd/MM/yyyy");
                                    var fechaMax = kendo.parseDate(empleadoSet.FechaNacimiento, "dd/MM/yyyy");;
                                    var anioActual = new Date().getFullYear();
                                    if (anioActual) {
                                        fechaMin.setFullYear(anioActual);
                                        fechaMax.setFullYear(anioActual);// Cambia el año a 2025
                                    }

                                    fechaMax.setDate(fechaMax.getDate() + 4);  // Suma 5 días

                                    return (dateObj1.toString() == dateObj2.toString()) && (fechaMin<= dateObj1 && dateObj1 <= fechaMax);
                                }
                            }
                        }
                    }
                    return true;
                }
            },
            messages: {
                validateFechaHora: "requerido",
                validateCompararFechaHora: "la fecha inicio tiene que ser mayor que fecha fin",
                validateFechaHoraCumple: "En el caso de onomastico la fecha inicio y fin tiene q ser igual y estar dentro de los 5 dias posteriores",
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


    this.JustificacionesJS.prototype.inicializarGridMaestro = function () {
        var _this = this;
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Justificaciones/ListarJustificacionesTrabajador',
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
                        data_param.iCodTipoJustificacion = $("#ddlTipo_busqueda").data("kendoDropDownList").value();



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
                    id: "iCodJustificaciones",
                    fields: {
                        iCodJustificaciones: { type: "number" },
                        vMotivoJustificacion: { type: "string" },
                        dtFechaHoraInicio: { type: "string", parse: _this.parseDateTime },
                        dtFechaHoraFin: { type: "string", parse: _this.parseDateTime },
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
                fileName: "Listado Justificaciones.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            //selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,
            filterable: true,
            persistSelection: true,
            dataType: 'json',
            columns: [
                //{
                //    selectable: true,
                //    width: "50px",
                //    attributes: { style: "text-align:center;" },

                //    template: function (item) {
                //        debugger;
                //        var ret = "";
                //        //if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA) {
                //        //    if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 6) {
                //        //        ret = "<center><input type='checkbox' class='select-row' data-icodjustificaciones='" + item.iCodJustificaciones + "'/></center>";
                //        //    }
                //        //}
                //        //else if (_this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                //        //    if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 2 || item.iCodEstadoProceso == 6 || item.iCodEstadoProceso == 7) {
                //        //        ret = "<center><input type='checkbox' class='select-row' data-icodjustificaciones='" + item.iCodJustificaciones + "'/></center>";
                //        //    }
                //        //}
                //        return ret;
                //    },
                //},
                {

                    // Columna para seleccionar una fila
                    field: "selected",
                    title: "Seleccionar",
                    width: "20px",
                    headerTemplate: function (item) {
                        return '<input id="select-row-all" class="k-checkbox k-checkbox-md k-rounded-md" data-role="checkbox" aria-label="Select all rows" aria-checked="false" type="checkbox">';
                    },
                    template: function (item) {
                        var ret = "";
                        if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 6) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodjustificaciones="' + item.iCodJustificaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 2 || item.iCodEstadoProceso == 6 || item.iCodEstadoProceso == 7) {
                                ret = '<center><input class="k-checkbox k-checkbox-md k-rounded-md select-row" data-icodjustificaciones="' + item.iCodJustificaciones + '" data-role="checkbox" aria-label="Deselect row" aria-checked="true" type="checkbox"></center>';
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
                    width: "50px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vNombreTrabajador",
                    title: "NOMBRE",
                    width: "200px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtAuditCreacion",
                    title: "FECHA REGISTRO",
                    width: "100px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtFechaHoraInicio",
                    title: "FECHA INICIO",
                    width: "100px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "dtFechaHoraFin",
                    title: "FECHA FIN",
                    width: "100px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vMotivoJustificacion",
                    title: "MOTIVO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vTipoJustificacion",
                    title: "TIPO",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vTipoGoce",
                    title: "GOCE",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vDescripcion",
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
                    //format: "{0:dd/MM/yyyy HH:mm}", // Formato de la fecha
                    filterable: { multi: true, search: true, ignoreCase: true },
                    //template: function (item) {
                    //    if (item.dFechaAprobadoAdmin && item.dFechaAprobadoAdmin != "/Date(-62135578800000)/") {
                    //        return kendo.toString(kendo.parseDate(item.dFechaAprobadoAdmin), 'dd/MM/yyyy HH:mm');
                    //    }
                    //    return "";
                    //},
                    //filterable: {
                    //    multi: true,
                    //    search: true,
                    //    ignoreCase: true,
                    //    itemTemplate: function (e) {
                    //        debugger;
                    //        return ({ dFechaAprobadoAdmin, all }) => {
                    //            if (all) {
                    //                return `<li class="k-item k-check-all-wrap"><label class="k-label k-checkbox-label"><input type="checkbox" class="k-checkbox k-checkbox-md k-rounded-md k-check-all" value="Seleccionar todo"><span>Seleccionar todo</span></label></li>`;
                    //            }
                    //            else if (dFechaAprobadoAdmin && dFechaAprobadoAdmin != "/Date(-62135578800000)/") {
                    //                var dFechaAprobadoAdmin_parse = kendo.toString(kendo.parseDate(dFechaAprobadoAdmin), 'dd/MM/yyyy HH:mm')

                    //                return `<li class="k-item"><label class="k-label k-checkbox-label"><input type="checkbox" class="k-checkbox k-checkbox-md k-rounded-md" value="\\${dFechaAprobadoAdmin}"/><span>${dFechaAprobadoAdmin_parse}</span></label></li>`;
                    //                //return `<span><label><span>${dFechaAprobadoAdmin_parse || all}</span><input type='checkbox' name='" + e.field + "' value='${dFechaAprobadoAdmin_parse}'/></label></span><br>`
                    //            }
                    //            return "";
                    //        }
                    //    },
                    //}
                },
                {
                    field: "dFechaDenegadoAdmin",
                    title: "FECHA DENE",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                //{
                //    field: "vAprovadoJeje",
                //    title: "APROBADO JEFE",
                //    width: "100px",
                //},
                //{
                //    field: "vFechaAprobadoJefe",
                //    title: "FECHA APROBADO JEFE",
                //    width: "100px",
                //    template: function (item) {
                //        if (item.vFechaAprobadoJefe) {
                //            return kendo.toString(kendo.parseDate(item.vFechaAprobadoJefe), 'dd/MM/yyyy');
                //        }
                //        return "";
                //    },

                //},
                //{
                //    field: "vAprobadoAdmin",
                //    title: "APROBADO ADMIN",
                //    width: "100px",
                //},
                //{
                //    field: "vFechaAprobadoAdmin",
                //    title: "FECHA APROBADO ADMIN",
                //    width: "100px",
                //    template: function (item) {
                //        if (item.vFechaAprobadoAdmin) {
                //            return kendo.toString(kendo.parseDate(item.vFechaAprobadoAdmin), 'dd/MM/yyyy');
                //        }
                //        return "";
                //    },
                //},
                {
                    // Columna para el botón al final de la fila
                    field: "iCodJustificaciones",
                    title: "Acciones",
                    width: "150px",
                    template: function (item) {
                        //debugger;
                        var ret = "<center>";
                        if (_this.PERFIL == _this.PERFIL_EMPLE_CTRL_ASISTENCIA) {
                            if (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5) {
                                ret += "<button title='Editar'  style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.iCodJustificaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        else if (_this.PERFIL == _this.PERFIL_JEFE_CTRL_ASISTENCIA || _this.PERFIL == _this.PERFIL_ADMIN_CTRL_ASISTENCIA) {
                            if ((_this.codUsuario == item.vAuditCreacion) && (item.iCodEstadoProceso == 1 || item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5)) {
                                ret += "<button title='Editar' style='margin-right:3px' class='btn btn-primary btn-xs' onclick='controlador.editar(" + item.iCodJustificaciones + ")'><span class='glyphicon glyphicon-check' aria-hidden='true'></span></button>";
                            }
                        }
                        if ((_this.codUsuario == item.vAuditCreacion) && (item.iCodEstadoProceso == 3 || item.iCodEstadoProceso == 5)) {
                            ret += "<button title='Ver Historial' style='margin-right:3px' class='btn btn-success btn-xs' onclick='controlador.abrirModalHistorial(" + item.iCodJustificaciones + ")'><span class='glyphicon glyphicon-comment' aria-hidden='true'></span></button>";
                        }
                        //if (item.vUrlArchivo) {
                        //    var path = controladorApp.obtenerRutaBase() + 'Justificaciones/DescargarArchivo?fileName=' + item.vUrlArchivo;
                        //    ret += "<a href='" + path + "' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-file' aria-hidden='true'></span></a>";
                        //}
                        if (item.vUrlArchivo) {
                            ret += "<button title='Ver Archivos' style='margin-right:3px' class='btn btn-info btn-xs' onclick='controlador.abrirModalArchivos(" + item.iCodJustificaciones + ")'><span class='glyphicon glyphicon-file' aria-hidden='true'></span></button>";
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
    //    //if ($(this).is(":checked")) {
    //    //    checkboxes.not(this).prop("checked", false); // Desmarcar todos los demás
    //    //}

    //    var row = $(this).closest("tr"); // Obtenemos la fila que contiene el checkbox
    //    obtenerSeleccionFila(row); // Llamamos a la función cuando el checkbox cambie
    //});

    //// Función para obtener los valores de los checkboxes seleccionados
    //function obtenerSeleccionFila(row) {
    //    var checkbox = row.find("input[type='checkbox']");
    //    var isChecked = checkbox.is(":checked");

    //    // Verificar si está seleccionado y obtener el valor del checkbox
    //    if (isChecked) {
    //        var icodJustificaciones = checkbox.data("icodjustificaciones");
    //        $('#hdIdJustificacionAprobar').val(icodJustificaciones);
    //    }
    //    else {
    //        $('#hdIdJustificacionAprobar').val(0);
    //    }
    //}

    function obtenerSeleccionados() {
        var seleccionados = [];

        // Recorrer todas las filas del grid y verificar si están seleccionadas
        $("#divGrid").find(".select-row:checked").each(function () {
            var icodJustificaciones = $(this).data("icodjustificaciones");
            seleccionados.push(icodJustificaciones);
        });

        return seleccionados;
    }


    this.JustificacionesJS.prototype.buscar = function (e) {
        e.preventDefault();
        debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
        $("#select-row-all").prop("checked", false)

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.JustificacionesJS.prototype.abrirModalNuevo = function () {
        debugger;
        $('#divModal_wnd_title').text('Registro Justificacion');
        $("#dllMotivo_registro").data("kendoDropDownList").value("");
        //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
        $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value("");
        $("#ddlTipoGoce_registro").data("kendoDropDownList").value("");
        //var date = new Date();
        $('#dtpFechaIni_registro').data("kendoDatePicker").value(null);
        $('#dtpFechaFin_registro').data("kendoDatePicker").value(null);
        $('#dtpFechaHoraIni_registro').data("kendoDateTimePicker").value(null);
        $('#dtpFechaHoraFin_registro').data("kendoDateTimePicker").value(null);

        $('#txtDescripcion_registro').val('');
        $('#chkMadrugada_registro').prop('checked', false);
        $("#div-download-files").html("");
        $("#ufArchivos_registro").data("kendoUpload").clearAllFiles();
        $("#lvArchivos_registro").data("kendoListView").dataSource.data([]);
        $('#divModal').data('kendoWindow').open();
    }

    this.JustificacionesJS.prototype.closeModalNuevo = function () {
        $('#divModal').data('kendoWindow').close();
    }

    this.JustificacionesJS.prototype.abrirModalAprobar = function () {
        //debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodjustificaciones"));
        });

        if (vIds.length > 0) {

            if (vIds.length == 1) {
                $('#divHistorial_aprobar').show();
                var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/ListarJustificacionProcesoHistorial';

                var id = vIds[0];

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodJustificaciones: id },
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

    this.JustificacionesJS.prototype.closeModalAprobar = function () {
        $('#divModalAprobar').data('kendoWindow').close();
    }

    this.JustificacionesJS.prototype.abrirModalDenegar = function () {
        debugger;
        var vIds = [];
        var checkboxesSeleccionados = $("#divGrid input[type='checkbox'].select-row:checked");
        checkboxesSeleccionados.each(function () {
            vIds.push($(this).attr("data-icodjustificaciones"));
        });

        if (vIds.length > 0) {
            if (vIds.length == 1) {
                $('#divHistorial_denegar').show();

                var id = vIds[0];
                var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/ListarJustificacionProcesoHistorial';

                $.ajax({
                    url: urlMetodo,
                    type: 'GET',
                    dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                    contentType: 'application/json',  // Especificamos que enviamos JSON
                    data: { iCodJustificaciones: id },
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

    this.JustificacionesJS.prototype.closeModalDenegar = function () {
        $('#divModalDenegar').data('kendoWindow').close();
    }

    this.JustificacionesJS.prototype.abrirModalHistorial = function (iCodJustificaciones) {
        debugger;
        if (iCodJustificaciones > 0) {
            debugger;
            // Crear una instancia de FormData
            //var data_param = new FormData();
            //// Agregar propiedades simples
            //data_param.append('iCodJustificaciones', parseInt($('#hdIdJustificacion').val()));
            //data_param.append('iCodJustificacionesProceso', null);
            //data_param.append('iCodEstadoProceso', null);


            var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/ListarJustificacionProcesoHistorial';

            $.ajax({
                url: urlMetodo,
                type: 'GET',
                dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                contentType: 'application/json',  // Especificamos que enviamos JSON
                data: { iCodJustificaciones: iCodJustificaciones },
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

    this.JustificacionesJS.prototype.closeModalHistorial = function () {
        $('#divModalHistorial').data('kendoWindow').close();
    }

    this.JustificacionesJS.prototype.abrirModalArchivos = function (iCodJustificaciones) {
        var _this = this;
        debugger;
        if (iCodJustificaciones > 0) {

            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Justificaciones/ObtenerJustificacionPorId',
                type: 'GET',
                dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
                contentType: 'application/json',  // Especificamos que enviamos JSON
                data: { iCodJustificaciones: iCodJustificaciones },
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

    this.JustificacionesJS.prototype.closeModalArchivos = function () {
        $('#divModalArchivos').data('kendoWindow').close();
    }

    this.JustificacionesJS.prototype.grabar = function (e) {
        debugger;
        e.preventDefault();
        if (frmJustificacion.validate()) {
            var justificacion = justificacionObjeto($, 0);

            // Crear una instancia de FormData
            var data_param = new FormData();

            // Agregar propiedades simples
            data_param.append('iCodJustificaciones', justificacion.iCodJustificaciones);
            data_param.append('iCodTrabajador', justificacion.iCodTrabajador);
            data_param.append('iCodigoDependencia', justificacion.iCodigoDependencia);
            data_param.append('iCodMotivoJustificacion', justificacion.iCodMotivoJustificacion);
            data_param.append('iCodTipoJustificacion', justificacion.iCodTipoJustificacion);
            data_param.append('iCodTipGoce', justificacion.iCodTipGoce);
            data_param.append('iCodEstadoProceso', justificacion.iCodEstadoProceso);
            data_param.append('bMadrugada', justificacion.bMadrugada);
            data_param.append('dtFechaHoraInicio', justificacion.dtFechaHoraInicio.toISOString());
            data_param.append('dtFechaHoraFin', justificacion.dtFechaHoraFin.toISOString());
            data_param.append('vDescripcion', justificacion.vDescripcion);
            data_param.append('bEstado', justificacion.bEstado);
            data_param.append('dtAuditCreacion', justificacion.dtAuditCreacion);
            data_param.append('vAuditCreacion', justificacion.vAuditCreacion);
            data_param.append('dtAuditModificacion', justificacion.dtAuditModificacion);
            data_param.append('vAuditModificacion', justificacion.vAuditModificacion);

            debugger;
            var filesUpload = [];

            $.each($("#ufArchivos_registro").data("kendoUpload").getFiles(), function (index, file) {
                data_param.append('filesUpload[' + index + ']', file.rawFile);
            });

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/GrabarJustificacionTrabajador';

            $.ajax({
                url: urlMetodo,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    $("#dllMotivo_registro").data("kendoDropDownList").value("");
                    //$("#dllEmpleado_registro").data("kendoDropDownList").value("");
                    $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value("");
                    $("#ddlTipoGoce_registro").data("kendoDropDownList").value("");
                    $('#dtpFechaIni_registro').data("kendoDatePicker").value(null);
                    $('#dtpFechaFin_registro').data("kendoDatePicker").value(null);
                    $('#dtpFechaHoraIni_registro').data("kendoDateTimePicker").value(null);
                    $('#dtpFechaHoraFin_registro').data("kendoDateTimePicker").value(null);
                    $('#txtDescripcion_registro').val('');
                    $('#chkMadrugada_registro').prop('checked', false);
                    $('#divModal').data('kendoWindow').close();
                    $('#hdIdJustificacion').val(0);
                    $('#fechasdate').show();
                    $('#fechasdatetime').hide();
                    $('#divGrid').data("kendoGrid").dataSource.page(1);
                },
                error: function (res) {
                    //alert(res);
                }
            });
        }
    }

    this.JustificacionesJS.prototype.aprobar = function (e) {
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
                vIds.push($(this).attr("data-icodjustificaciones"));
            });

            var data_param = new FormData();

            data_param.append('vIds', vIds.join(','));
            data_param.append('vDescripcion', observaciones);
            data_param.append('aprobarDenegar', aprobarDenegar);

            var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/AprobarDenegarJustificacionTrabajadorMas';

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

    //this.JustificacionesJS.prototype.aprobar = function (e) {
    //    e.preventDefault();
    //    debugger;
    //    var justificacion = justificacionObjeto($, 0);
    //    justificacion.iCodJustificaciones = $("#hdIdJustificacionAprobar").val();
    //    // Crear una instancia de FormData
    //    var data_param = new FormData();

    //    // Agregar propiedades simples
    //    data_param.append('iCodJustificaciones', justificacion.iCodJustificaciones);
    //    data_param.append('iCodTrabajador', justificacion.iCodTrabajador);
    //    data_param.append('iCodigoDependencia', justificacion.iCodigoDependencia);
    //    data_param.append('iCodMotivoJustificacion', justificacion.iCodMotivoJustificacion);
    //    data_param.append('iCodTipoJustificacion', justificacion.iCodTipoJustificacion);
    //    data_param.append('iCodTipGoce', justificacion.iCodTipGoce);
    //    data_param.append('iCodEstadoProceso', justificacion.iCodEstadoProceso);
    //    data_param.append('bMadrugada', justificacion.bMadrugada);


    //    data_param.append('dtFechaHoraInicio', justificacion.dtFechaHoraInicio == null ? null : justificacion.dtFechaHoraInicio.toISOString());
    //    data_param.append('dtFechaHoraFin', justificacion.dtFechaHoraFin == null ? null : justificacion.dtFechaHoraFin.toISOString());


    //    data_param.append('vDescripcion', justificacion.vDescripcion);
    //    data_param.append('bEstado', justificacion.bEstado);
    //    data_param.append('dtAuditCreacion', justificacion.dtAuditCreacion);
    //    data_param.append('vAuditCreacion', justificacion.vAuditCreacion);
    //    data_param.append('dtAuditModificacion', justificacion.dtAuditModificacion);
    //    data_param.append('vAuditModificacion', justificacion.vAuditModificacion);
    //    data_param.append('aprobarDenegar', justificacion.aprobarDenegar);

    //    var urlMetodo = controladorApp.obtenerRutaBase() + 'Justificaciones/AprobarDenegarJustificacionTrabajador';

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

    //            //$('#hdIdJustificacion').val(0);
    //            $('#hdIdJustificacionAprobar').val(0);
    //            $('#divGrid').data("kendoGrid").dataSource.page(1);
    //        },
    //        error: function (res) {
    //            //alert(res);
    //        }
    //    });
    //}

    this.JustificacionesJS.prototype.editar = function (iCodJustificaciones) {
        var _this = this;
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Justificaciones/ObtenerJustificacionPorId',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodJustificaciones: iCodJustificaciones },
            success: function (res) {
                if (res) {
                    $('#hdIdJustificacion').val(iCodJustificaciones);

                    $("#dllEmpleado_registro").data("kendoDropDownList").value(res.iCodTrabajador);
                    $("#dllMotivo_registro").data("kendoDropDownList").value(res.iCodMotivoJustificacion);
                    $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value(res.iCodTipoJustificacion);
                    /*$("#ddlTipoJustificacion_registro").data("kendoDropDownList").trigger("change");*/
                    switch (res.iCodTipoJustificacion) {
                        case 1:
                            $('#fechasdate').show();
                            $('#fechasdatetime').hide();
                            $('#dtpFechaIni_registro').data("kendoDatePicker").value(res.dtFechaHoraInicio);
                            $('#dtpFechaFin_registro').data("kendoDatePicker").value(res.dtFechaHoraFin);
                            break;
                        case 2:
                            $('#fechasdate').hide();
                            $('#fechasdatetime').show();
                            $('#dtpFechaHoraIni_registro').data("kendoDateTimePicker").value(res.dtFechaHoraInicio);
                            $('#dtpFechaHoraFin_registro').data("kendoDateTimePicker").value(res.dtFechaHoraFin);
                            break;
                        case 3:
                            $('#fechasdate').show();
                            $('#fechasdatetime').hide();
                            $('#dtpFechaIni_registro').data("kendoDatePicker").value(res.dtFechaHoraInicio);
                            $('#dtpFechaFin_registro').data("kendoDatePicker").value(res.dtFechaHoraFin);
                            break;
                        default:
                    }
                    $("#ddlTipoGoce_registro").data("kendoDropDownList").value(res.iCodTipGoce);
                    $('#chkMadrugada_registro').prop('checked', res.bMadrugada === true);

                    $("#txtDescripcion_registro").val(res.vDescripcion);

                    $('#hdIdEstadoProceso').val(res.iCodEstadoProceso);
                    $("#ufArchivos_registro").data("kendoUpload").clearAllFiles();

                    $("#lvArchivos_registro").data("kendoListView").dataSource.data(res.Archivos);

                    $('#divModal_wnd_title').text('Modificar Justificacion');
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

    this.JustificacionesJS.prototype.parseDate = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy');
        }
        return "";
    }

    this.JustificacionesJS.prototype.parseDateTime = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy HH:mm');
        }
        return "";
    }

    this.JustificacionesJS.prototype.changeDateOrTime = function (iCodTipoJustificacion) {
        debugger;
        /* 1 DIAS, 2 HORAS, 3 HORA EXACTA */
        if (iCodTipoJustificacion == 1) {
            $('#fechasdate').show();
            $('#fechasdatetime').hide();
        } else if (iCodTipoJustificacion == 2 || iCodTipoJustificacion == 3) {
            $('#fechasdate').hide();
            $('#fechasdatetime').show();
        } else {
            $('#fechasdate').show();
            $('#fechasdatetime').hide();
        }
    }

    this.JustificacionesJS.prototype.loadEmpleado = function (callBack) {
        debugger;
        var id = $('#dllEmpleado_registro').data("kendoDropDownList").value();
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Justificaciones/GetEmpleado',
            type: 'GET',
            dataType: 'json',  // Tipo de datos que esperamos recibir del servidor
            contentType: 'application/json',  // Especificamos que enviamos JSON
            data: { iCodTrabajador: id },
            success: function (res) {
                if (res) {
                    callBack(res);
                }
            },
            error: function (xhr, status, error) {
                // Aquí puedes manejar el error, si ocurrió alguna falla en la solicitud
                console.error('Error en la solicitud:', status, error);
                // Puedes mostrar un mensaje de error en el UI si lo deseas.
                callBack(null);
            }
        });
    }

}(jQuery));

function justificacionObjeto($, iCodJustificaciones) {
    var iCodEstadoProceso = 0;
    var varFechaHoraInicio = null;
    var vardtFechaHoraFin = null;
    var observaciones = '';
    var aprobarDenegar = false;

    if ($("#hdIdJustificacion").val() > 0) {
        iCodJustificaciones = $("#hdIdJustificacion").val();
        iCodEstadoProceso = $("#hdIdEstadoProceso").val();
    }

    if ($('#fechasdate').is(':visible')) {
        varFechaHoraInicio = $('#dtpFechaIni_registro').data("kendoDatePicker").value();
        vardtFechaHoraFin = $('#dtpFechaFin_registro').data("kendoDatePicker").value();
    } else if ($('#fechasdatetime').is(':visible')) {
        varFechaHoraInicio = $('#dtpFechaHoraIni_registro').data("kendoDateTimePicker").value();
        vardtFechaHoraFin = $('#dtpFechaHoraFin_registro').data("kendoDateTimePicker").value();
    }

    debugger;
    observaciones = $("#txtDescripcion_registro").val();
    if ($('#divModalDenegar').is(':visible')) {
        observaciones = $('#txtObservaciones_denegar').val();
        aprobarDenegar = false;
    } else if ($('#divModalAprobar').is(':visible')) {
        aprobarDenegar = true;
        observaciones = $('#txtObservaciones_aprobar').val();
    }

    return {
        iCodJustificaciones: iCodJustificaciones,
        iCodTrabajador: $("#dllEmpleado_registro").data("kendoDropDownList").value(),
        iCodigoDependencia: $("#ddlDependencia_busqueda").data("kendoDropDownList").value(),
        iCodMotivoJustificacion: $("#dllMotivo_registro").data("kendoDropDownList").value(),
        iCodTipoJustificacion: $("#ddlTipoJustificacion_registro").data("kendoDropDownList").value(),
        iCodTipGoce: $("#ddlTipoGoce_registro").data("kendoDropDownList").value(),
        iCodEstadoProceso: iCodEstadoProceso,
        bMadrugada: $('#chkMadrugada_registro').prop('checked'),
        dtFechaHoraInicio: varFechaHoraInicio,
        dtFechaHoraFin: vardtFechaHoraFin,
        vDescripcion: observaciones,
        bEstado: true,
        aprobarDenegar: aprobarDenegar,
        Archivos: [
        ],
        Procesos: [
        ]
    };
}