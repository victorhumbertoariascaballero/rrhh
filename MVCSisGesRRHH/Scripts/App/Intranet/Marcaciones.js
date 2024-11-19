(function ($) {
    var PERFIL_JEFE_CTRL_ASISTENCIA = '182';
    var PERFIL_EMPLE_CTRL_ASISTENCIA = '183';
    var PERFIL_ADMIN_CTRL_ASISTENCIA = '184';

    this.MarcacionesJS = function () { };
    this.MarcacionesJS.prototype.inicializarMaestro = function () {
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

        $("#ddlTipoMarcacion_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            //optionLabel: "--Seleccione--",
            dataTextField: "vDescripcion",
            dataValueField: "iCodTipoMarcacion",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Marcaciones/ListarTipoMarcaciones",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        $("#ddlFecha_busqueda").kendoDateRangePicker({
            range: {
                start: new Date(),
                end: new Date()
            },
            "messages": {
                "startLabel": null,
                "endLabel": null
            }
        });

        /* CONSULTA */

        $("#pnlEmpleado").kendoPanelBar({
            expandMode: "single"
        });

        /* REGISTRO NUEVO */
        $('#divModalMarcacionWeb').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '30%',
            height: 'auto',
            title: 'Marcación Web',
            visible: false,
            position: { top: '10%', left: "35%" },
            actions: ["Close"],
            close: function () {
                //frmPersonaValidador.hideMessages();    
                //this.clearSelection();
            }
        });

        /* RELOJ ANALOGICO */
        this.calculateLines();
        setInterval(() => {
            fechaServer.setSeconds(fechaServer.getSeconds() + 1);
            this.calculateHourDegrees();
            this.calculateMinuteDegrees();
            this.calculateSeconds();
        }, 1000);

        this.inicializarGridMaestro();
    };

    /* RELOJ ANALOGICO INI */
    this.MarcacionesJS.prototype.linearMap = function (value, min, max, newMin, newMax) {
        return newMin + (newMax - newMin) * (value - min) / (max - min);
    }

    this.MarcacionesJS.prototype.calculateHourDegrees = function () {
        const currentHour = fechaServer.getHours() - 12;
        const angle = this.linearMap(currentHour, 0, 12, 0, 360);
        document.querySelector(".hours").style.transform = `rotate(${angle}deg)`;
    }

    this.MarcacionesJS.prototype.calculateMinuteDegrees = function () {
        const currentMinutes = fechaServer.getMinutes();
        const angle = this.linearMap(currentMinutes, 0, 60, 0, 360);
        document.querySelector(".minutes").style.transform = `rotate(${angle}deg)`;
    }

    this.MarcacionesJS.prototype.calculateSeconds = function () {
        const currentMinutes = fechaServer.getSeconds();
        const angle = this.linearMap(currentMinutes, 0, 60, 0, 360);
        document.querySelector(".seconds").style.transform = `rotate(${angle}deg)`;
    }

    this.MarcacionesJS.prototype.calculateLines = function () {
        const lines = document.querySelectorAll(".line");
        const numberLines = lines.length;
        for (let i = 0; i < numberLines; i++) {
            const line = lines[i];
            const angle = this.linearMap(i, 0, numberLines, 0, 360);
            line.style.transform = `rotate(${angle}deg)`;
        }
    }
    /* RELOJ ANALOGICO FIN */

    this.MarcacionesJS.prototype.inicializarGridMaestro = function () {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Marcaciones/ListarMarcaciones',
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

                        var range = $("#ddlFecha_busqueda").data("kendoDateRangePicker").range();
                        //alert("Start - " + range.start + " End - " + range.end);
                        data_param.dtFechaMarcacionIni = range.start;
                        data_param.dtFechaMarcacionFin = range.end;

                        data_param.iCodTipoMarcacion = $("#ddlTipoMarcacion_busqueda").data("kendoDropDownList").value();

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
                    id: "iCodMarcaciones"
                }
            }
        });

        debugger;
        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel",], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Listado de Marcaciones.xlsx",
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
                    field: "dtFechaMarcacion",
                    title: "FECHA DE MARCACION",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        return kendo.toString(kendo.parseDate(item.dtFechaMarcacion), 'dd/MM/yyyy');
                    },
                    width: "50px"
                },
                {
                    field: "vTipoMarcacion",
                    title: "TIPO",
                    attributes: { style: "text-align:center;" },
                    width: "50px",
                }
            ],
            excelExport: function (e) {
                var sheet = e.workbook.sheets[0];
                var template = kendo.template(this.columns[4].template);

                //alert(template);
                for (var i = 1; i < sheet.rows.length; i++) {
                    debugger;
                    var row = sheet.rows[i];
                    if (row.cells[5] != undefined) {
                        if (row.cells[5].value != undefined) {
                            if (row.cells[5].value.toString().startsWith('/Date')) {
                                var dataItem = { FechaInicio: row.cells[5].value };
                                row.cells[5].value = template(dataItem);
                            }
                            else {
                                if (row.cells[4].value != undefined) {
                                    if (row.cells[4].value.toString().startsWith('/Date')) {
                                        var dataItem2 = { FechaInicio: row.cells[4].value };
                                        row.cells[4].value = template(dataItem2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }).data();
        //}
    };

    this.MarcacionesJS.prototype.buscar = function (e) {
        e.preventDefault();
        debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };
    this.MarcacionesJS.prototype.abrirModalMarcacionWeb = function () {
        var modal = $('#divModalMarcacionWeb').data('kendoWindow');
        modal.open();
    }

    this.MarcacionesJS.prototype.closeModalMarcacionWeb = function () {
        var modal = $('#divModalMarcacionWeb').data('kendoWindow');
        modal.close();
    }

    this.MarcacionesJS.prototype.marcar = function (e) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    var data_param = new FormData();
                    data_param.append('dtFechaMarcacion', fechaServer.toISOString());
                    data_param.append('vLongitud', position.coords.longitude);
                    data_param.append('vLatitud', position.coords.latitude);
                    data_param.append('iCodTipoMarcacion', 2);

                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Marcaciones/GrabarMarcacion',
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
                },
                function (error) {
                    console.log(error);
                });
        } else {
            alert("Geolocalización no es soportada por este navegador.");
        }

        
    }
}(jQuery));