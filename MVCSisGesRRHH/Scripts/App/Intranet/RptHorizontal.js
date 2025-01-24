(function ($) {
    this.HorizontalJS = function () {
        this.PERFIL = -1;
        this.PERFIL_JEFE_CTRL_ASISTENCIA = '';
        this.PERFIL_EMPLE_CTRL_ASISTENCIA = '';
        this.PERFIL_ADMIN_CTRL_ASISTENCIA = '';
        this.Empleado = null;
        this.codUsuario = 0;
    };
    this.HorizontalJS.prototype.inicializarMaestro = function () {
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

            $.ajax({
                url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleadosMaestro",
                data: { IdDependencia: val, Estado: 1 },
                dataType: "json",
                async: false,
                success: function (data) {

                    empleadosDdl.setDataSource(new kendo.data.DataSource({
                        data: data
                    }));
                },
                error: function () {
                    alert("Error al cargar empleado.");
                }
            });
        }


        var meses = [
            { text: "Enero", value: "1" },
            { text: "Febrero", value: "2" },
            { text: "Marzo", value: "3" },
            { text: "Abril", value: "4" },
            { text: "Mayo", value: "5" },
            { text: "Junio", value: "6" },
            { text: "Julio", value: "7" },
            { text: "Agosto", value: "8" },
            { text: "Septiembre", value: "9" },
            { text: "Octubre", value: "10" },
            { text: "Noviembre", value: "11" },
            { text: "Diciembre", value: "12" }
        ];

        $("#ddlMeses_busqueda").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: meses,
        });

        if (this.Empleado != null) {
            $("#ddlDependencia_busqueda").data("kendoDropDownList").value(this.Empleado.IdDependencia);
            onChangeDependencia(this.Empleado.IdDependencia);
            $("#ddlEmpleado_busqueda").data("kendoDropDownList").value(this.Empleado.IdEmpleado);
        }

        this.inicializarGridMaestro();
    };


    this.HorizontalJS.prototype.inicializarGridMaestro = function () {
        var _this = this;
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'ReportesCA/GetHorizontal',
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
                        data_param.iMes = $("#ddlMeses_busqueda").data("kendoDropDownList").value();

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
                    id: "iCodHorizontal",
                    
                }
            }
        });
        

        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel",], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
            excel: {
                fileName: "Reporte Horizontal.xlsx",
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
            columns: this.generarColumnas(31),           
            excelExport: function (e) {
                //var sheet = e.workbook.sheets[0];
                //var template = kendo.template(this.columns[4].template);
            }
        }).data();
        //}
    };


    this.HorizontalJS.prototype.buscar = function (e) {
        e.preventDefault();
        ////debugger;
        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);
        $("#select-row-all").prop("checked", false)
        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.HorizontalJS.prototype.generarColumnas = function (nroDias) {
        var columnas = [
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
                field: "vDependencia",
                title: "DEPENDENCIA",
                filterable: { multi: true, search: true, ignoreCase: true }
            },
            {
                field: "vRegimen",
                title: "REGIMEN",
                filterable: { multi: true, search: true, ignoreCase: true }
            },
            {
                field: "vHorario",
                title: "HORARIO",
                filterable: { multi: true, search: true, ignoreCase: true }
            },
        ]
        for (let i = 1; i <= nroDias; i++) {
            columnas.push({
                field: "vDia" + i,
                title: i.toString(),
                filterable:false,
                //filterable: { multi: true, search: true, ignoreCase: true }
            });
        }

        return columnas;
    };

}(jQuery));