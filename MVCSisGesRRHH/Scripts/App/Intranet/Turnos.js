(function ($) {
    this.TurnosJS = function () {
        this.PERFIL_JEFE_CTRL_ASISTENCIA = '';
        this.PERFIL_EMPLE_CTRL_ASISTENCIA = '';
        this.PERFIL_ADMIN_CTRL_ASISTENCIA = '';
        this.Empleado = null;
    };

    this.TurnosJS.prototype.inicializarMaestro = function () {
        

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

        

        $("#ddlTurno_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTurno",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Turnos/ListarTurnos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        /* CONSULTA */

        $("#pnlEmpleado").kendoPanelBar({
            expandMode: "single"
        });

        /* REGISTRO NUEVO */
        $("#ddlTurno_registro").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTurno",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Turnos/ListarTurnos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
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


        if (this.Empleado != null) {
            $("#ddlDependencia_busqueda").data("kendoDropDownList").value(this.Empleado.IdDependencia);
            onChangeDependencia(this.Empleado.IdDependencia);
            $("#ddlEmpleado_busqueda").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
            $("#dllEmpleado_registro").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
        }

        $('#divModal').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '30%',
            height: 'auto',
            title: 'Asignacion de Turno',
            visible: false,
            position: { top: '10%', left: "35%" },
            actions: ["Close"],
            close: function () {
                //frmPersonaValidador.hideMessages();    
                //this.clearSelection();
            }
        });


        this.inicializarGridMaestro();
    };


    this.TurnosJS.prototype.inicializarGridMaestro = function () {
        var _this = this;
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Turnos/ListarTurnosTrabajador',
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
                        data_param.iCodTurno = $("#ddlTurno_busqueda").data("kendoDropDownList").value();
                        

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
                    id: "iCodTurnoTrabajador",
                    fields: {
                        vNombreTrabajador: { type: "string" },
                        dtAuditCreacion: { type: "string", parse: _this.parseDate },
                        vDependencia: { type: "string" },
                        vTurno: { type: "string" }
                    }
                }
            }
        });

        debugger;
        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel",], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado Turnos.xlsx",
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
                    field: "vNumeroDocumento",
                    title: "DNI",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
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
                    width: "200px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vDependencia",
                    title: "DEPENDENCIA",
                    width: "100px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                },
                {
                    field: "vTurno",
                    title: "TURNO",
                    attributes: { style: "text-align:center;" },
                    width: "50px",
                    filterable: { multi: true, search: true, ignoreCase: true }
                }
            ],
            excelExport: function (e) {
                //var sheet = e.workbook.sheets[0];
                //var template = kendo.template(this.columns[4].template);
            }
        }).data();
        //}
    };

    this.TurnosJS.prototype.buscar = function (e) {
        e.preventDefault();
        debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };
    this.TurnosJS.prototype.abrirModalNuevo = function () {
        var modal = $('#divModal').data('kendoWindow');
        modal.open();
    }

    this.TurnosJS.prototype.closeModalNuevo = function () {
        var modal = $('#divModal').data('kendoWindow');
        modal.close();
    }

    this.TurnosJS.prototype.grabar = function (e) {
        var _this = this;
        e.preventDefault();
        var data_param = new FormData();
        //data_param.append('iCodTurnoTrabajador', fechaServer.toISOString());
        data_param.append('iCodTurno', $("#ddlTurno_registro").data("kendoDropDownList").value());
        data_param.append('iCodTrabajador', $("#dllEmpleado_registro").data("kendoDropDownList").value());
        data_param.append('iCodigoDependencia', $("#ddlDependencia_busqueda").data("kendoDropDownList").value());

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Turnos/GrabarTurnoTrabajador',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                _this.closeModalNuevo();
                $('#divGrid').data("kendoGrid").dataSource.page(1);
            },
            error: function (res) {
                //alert(res);
            }
        });
    }

    this.TurnosJS.prototype.parseDate = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy');
        }
        return "";
    }

    this.TurnosJS.prototype.parseDateTime = function (item) {
        if (item && item != "/Date(-62135578800000)/") {
            return kendo.toString(kendo.parseDate(item), 'dd/MM/yyyy HH:mm');
        }
        return "";
    }
}(jQuery));