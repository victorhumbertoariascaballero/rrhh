(function ($) {
    var PERFIL_JEFE_CTRL_ASISTENCIA = '182';
    var PERFIL_EMPLE_CTRL_ASISTENCIA = '183';
    var PERFIL_ADMIN_CTRL_ASISTENCIA = '184';

    this.TurnosJS = function () { };
    this.TurnosJS.prototype.inicializarMaestro = function () {
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
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleadosMaestro",
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
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleadosMaestro",
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
                    id: "iCodTurnoTrabajador"
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

            dataType: 'json',
            columns: [
                {
                    field: "vNumeroDocumento",
                    title: "DNI",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "vNombreTrabajador",
                    title: "NOMBRE",
                    width: "200px",
                },
                {
                    field: "vDependencia",
                    title: "DEPENDENCIA",
                    width: "100px",
                },
                {
                    field: "vTurno",
                    title: "TURNO",
                    attributes: { style: "text-align:center;" },
                    width: "50px",
                }
            ],
            excelExport: function (e) {
                var sheet = e.workbook.sheets[0];
                var template = kendo.template(this.columns[4].template);
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
        var data_param = new FormData();
        //data_param.append('iCodTurnoTrabajador', fechaServer.toISOString());
        data_param.append('iCodTurno', $("#ddlTurno_registro").data("kendoDropDownList").value());
        data_param.append('iCodTrabajador', $("#dllEmpleado_registro").data("kendoDropDownList").value());
        data_param.append('iCodigoDependencia', 0);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Turnos/GrabarTurnoTrabajador',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                $('#divGrid').data("kendoGrid").dataSource.page(1);
            },
            error: function (res) {
                //alert(res);
            }
        });
    }
}(jQuery));