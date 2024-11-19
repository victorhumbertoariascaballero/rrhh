(function ($) {
    var frmRegistroAnexo1;
    var frmRegistroAnexo2;
    var frmRegistroPerfilMision;
    var frmRegistroPerfilExperiencia;
    var frmRegistroNivelBasico;
    var frmRegistroNivelEducativo;
    var frmRegistroMaestria;
    var frmRegistroDoctorado;
    var frmPerfilesValidador;
    var frmNuevaSolicitud;
    var CodOrgano;
    var CodDependencia;
    var CodOrganoBand;
    var CodDependenciaBand;
    this.PerfilPuestoJS = function () { };

    ////////////////////////////VER//////////////////////////////////
    this.PerfilPuestoJS.prototype.inicializarVer = function () {
        debugger;

        if ($("#hdIdPerfilPuesto").val() != "") {

            //$("#ddlOrgano").kendoDropDownList({
            //    autoBind: true,
            //    optionLabel: "--Seleccione--",
            //    dataTextField: "strUnidad_Organica",
            //    dataValueField: "iCodigoDependencia",
            //    filter: "contains",
            //    minLength: 3,
            //    dataSource: {
            //        serverFiltering: true,
            //        transport: {
            //            read: {
            //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
            //                type: "GET",
            //                dataType: "json",
            //                cache: false
            //            },
            //            parameterMap: function ($options, $operation) {
            //                var data_param = {};
            //                if ($options.filter != undefined && $options.filter.filters.length > 0)
            //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;
            //                return $.toDictionary(data_param);
            //            }
            //        }
            //    }
            //});

            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                //optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });



            //alert($("#hdIdPerfilPuesto").val())
            var id = $("#hdIdPerfilPuesto").val();
            this.CargarFormularioPerfilVer(id);



        }
        else {            
            this.CargarFormularioFunciones($("#hdIdCodTrabajador").val());
            
            //$("#ddlOrgano").kendoDropDownList({
            //    autoBind: true,
            //    optionLabel: "--Seleccione--",
            //    dataTextField: "strUnidad_Organica",
            //    dataValueField: "iCodigoDependencia",
            //    filter: "contains",
            //    minLength: 3,
            //    dataSource: {
            //        serverFiltering: true,
            //        transport: {
            //            read: {
            //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
            //                type: "GET",
            //                dataType: "json",
            //                cache: false
            //            },
            //            parameterMap: function ($options, $operation) {
            //                var data_param = {};
            //                if ($options.filter != undefined && $options.filter.filters.length > 0)
            //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;
            //                return $.toDictionary(data_param);
            //            }
            //        }
            //    }
            //});

            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                //optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            //$("#ddlOrgano").data('kendoDropDownList').value(CodOrgano);

            //$("#ddlUUOO").kendoDropDownList({
            //    autoBind: true,
            //    cascadeFrom: "ddlOrgano",
            //    optionLabel: "--Seleccione--",
            //    dataTextField: "strUnidad_Organica",
            //    dataValueField: "iCodigoDependencia",
            //    filter: "contains",
            //    minLength: 3,
            //    dataSource: {
            //        serverFiltering: true,
            //        transport: {
            //            read: {
            //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
            //                type: "GET",
            //                dataType: "json",
            //                cache: false
            //            },
            //            parameterMap: function ($options, $operation) {
            //                var data_param = {};
            //                if ($options.filter != undefined && $options.filter.filters.length > 0)
            //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

            //                data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

            //                return $.toDictionary(data_param);
            //            }
            //        }
            //    }
            //});

            //$("#ddlUUOO").data('kendoDropDownList').value(CodDependencia);
            //$("#ddlUUOO").data("kendoDropDownList").readonly();
            controlador.CargarPerfilFunciones(-1);
            controlador.CargarPerfilRequisitosAdicionales(-1);
            controlador.CargarPerfilHabilidades(-1);
            controlador.CargarPerfilCompetencias(-1);
            controlador.CargarPerfilCoordInterna(-1);
            controlador.CargarPerfilCoordExterna(-1);
            controlador.CargarPerfilConocimientosTecnicos(-1);
            controlador.CargarPerfilConocimientosCurProg(-1);
            controlador.CargarPerfilConocimientosOfficeIdiomas(-1);
            controlador.CargarDatosFormAcaNivelBasico(-1);
            controlador.CargarDatosFormAcaNivelEducativo(-1);
            controlador.CargarDatosFormAcaMaestria(-1);
            controlador.CargarDatosFormAcaDoctorado(-1);
        }
        /// fin   
    };

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaNivelBasicoVer = function (id) {
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaNivelBasico',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraBasico").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            //selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                //{
                //    field: "strCodCarrera",
                //    title: "codCarrera",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strGrado",
                    title: "NIVEL EDUCATIVO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bCompleto",
                    title: "bCompleto",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vCompleto",
                    title: "COMPLETO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                }
            ],
            editable: "inline"
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaNivelEducativoVer = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaNivelEducativo',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraPreGrado").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strGrado",
                    title: "NIVEL EDUCATIVO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bCompleto",
                    title: "bCompleto",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vCompleto",
                    title: "COMPLETO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bColegiatura",
                    title: "bColegiatura",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vColegiatura",
                    title: "COLEGIATURA",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bHabilitado",
                    title: "bHabilitado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vHabilitado",
                    title: "HABILITADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "CARRERA NIVEL 1",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "CARRERA NIVEL 2",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "CARRERA NIVEL 3",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "300px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "CARRERA PROFESIONAL",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Editar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        //controles += '<a href="perfiles/nuevo">';
                //        //var items = new Object();
                //        //items.iCodPerfil = item.iCodPerfil;
                //        //items.iSecuencia = item.iSecuencia;
                //        //items.iCodNivel = item.iCodNivel;
                //        //items.bCompleto = item.bCompleto;
                //        //items.iCodGrado = item.iCodGrado;
                //        //items.bColegiatura = item.bColegiatura;
                //        //items.bHabilitado = item.bHabilitado;
                //        //items.strCodCarreraN1 = item.strCodCarreraN1;
                //        //items.strCodCarreraN2 = item.strCodCarreraN2;
                //        //items.strCodCarreraN3 = item.strCodCarreraN3;
                //        //items.strCodCarreraN4 = item.strCodCarreraN4;

                //        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.bCompleto, item.iCodGrado, item.bColegiatura, item.bHabilitado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                //        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroNivelEducativo(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Eliminar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodPerfil, item.iSecuencia];
                //        //controles += '<a href="perfiles/nuevo">';
                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaMaestriaVer = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaMaestria',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraMaestria").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strGrado",
                //    title: "Nivel Educativo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bCompleto",
                //    title: "bCompleto",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vCompleto",
                //    title: "Completo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bColegiatura",
                //    title: "bColegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vColegiatura",
                //    title: "Colegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bHabilitado",
                //    title: "bHabilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vHabilitado",
                //    title: "Habilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "Carrera Nivel 1",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "Carrera Nivel 2",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "Carrera Nivel 3",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "ESPECIALIDAD",
                    width: "500px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Editar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        //controles += '<a href="perfiles/nuevo">';
                //        //var items = new Object();
                //        //items.iCodPerfil = item.iCodPerfil;
                //        //items.iSecuencia = item.iSecuencia;
                //        //items.iCodNivel = item.iCodNivel;
                //        //items.bCompleto = item.bCompleto;
                //        //items.iCodGrado = item.iCodGrado;
                //        //items.bColegiatura = item.bColegiatura;
                //        //items.bHabilitado = item.bHabilitado;
                //        //items.strCodCarreraN1 = item.strCodCarreraN1;
                //        //items.strCodCarreraN2 = item.strCodCarreraN2;
                //        //items.strCodCarreraN3 = item.strCodCarreraN3;
                //        //items.strCodCarreraN4 = item.strCodCarreraN4;

                //        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.iCodGrado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                //        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroMaestria(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Eliminar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                //        //controles += '<a href="perfiles/nuevo">';
                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaDoctoradoVer = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaDoctorado',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraProDoctorado").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strGrado",
                //    title: "Nivel Educativo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bCompleto",
                //    title: "bCompleto",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vCompleto",
                //    title: "Completo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bColegiatura",
                //    title: "bColegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vColegiatura",
                //    title: "Colegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bHabilitado",
                //    title: "bHabilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vHabilitado",
                //    title: "Habilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "Carrera Nivel 1",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "Carrera Nivel 2",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "Carrera Nivel 3",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "ESPECIALIDAD",
                    width: "500px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Editar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        //controles += '<a href="perfiles/nuevo">';
                //        //var items = new Object();
                //        //items.iCodPerfil = item.iCodPerfil;
                //        //items.iSecuencia = item.iSecuencia;
                //        //items.iCodNivel = item.iCodNivel;
                //        //items.bCompleto = item.bCompleto;
                //        //items.iCodGrado = item.iCodGrado;
                //        //items.bColegiatura = item.bColegiatura;
                //        //items.bHabilitado = item.bHabilitado;
                //        //items.strCodCarreraN1 = item.strCodCarreraN1;
                //        //items.strCodCarreraN2 = item.strCodCarreraN2;
                //        //items.strCodCarreraN3 = item.strCodCarreraN3;
                //        //items.strCodCarreraN4 = item.strCodCarreraN4;

                //        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.iCodGrado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                //        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroDoctorado(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: 'Eliminar',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                //        //controles += '<a href="perfiles/nuevo">';
                //        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                //        controles += '</button>';
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //},
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarPerfilFuncionesVer = function (id) {
        debugger;
        this.$dataSourceFunciones = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFunciones',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                //update: {
                //    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarFunciones",
                //    type: 'POST',
                //    dataType: 'json',
                //    cache: false
                //},
                //destroy: {
                //    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarFunciones",
                //    type: 'POST',
                //    dataType: 'json',
                //    cache: false
                //},
                //create: {
                //    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarFunciones",
                //    type: 'POST',
                //    dataType: 'json',
                //    cache: false
                //},
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
                            //data_param.Estado = 0;
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = $options.pageSize;
                            data_param.Grilla.PaginaActual = $options.page
                            if ($options !== undefined && $options.sort !== undefined) {
                                data_param.Grilla.OrdenarDeForma = $options.sort[0].dir;
                                data_param.Grilla.OrdenarPor = $options.sort[0].field;
                            }
                            break;
                            //case "create":
                            //    //if (frmCompromiso.validate()) {                                
                            //    data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            //    data_param.iSecuencia = 0;
                            //    data_param.iCodVerbo = $options.Verbo.iCodVerbo;
                            //    //data_param.strVerbo = $options.strVerbo;
                            //    data_param.strObjeto = $options.strObjeto;
                            //    data_param.strDescripcion = $options.strDescripcion;
                            //    //}
                            //    break;
                            //case "update":
                            //    data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            //    data_param.iSecuencia = $options.iSecuencia;
                            //    data_param.iCodVerbo = $options.Verbo.iCodVerbo;
                            //    //data_param.strVerbo = $options.strVerbo;
                            //    data_param.strObjeto = $options.strObjeto;
                            //    data_param.strDescripcion = $options.strDescripcion;
                            //    break;
                            //case "destroy":
                            //    data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            //    data_param.iSecuencia = $options.iSecuencia;
                            //    data_param.iCodVerbo = 0;
                            //    //data_param.strVerbo = $options.strVerbo;
                            //    data_param.strObjeto = '';
                            //    data_param.strDescripcion = '';
                            //    break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridFunciones').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        //Verbo: {
                        //    validation: {
                        //        required: true,
                        //        verbovalidation: function (input) {
                        //            if (input.is("[name='Verbo']") && input.val() == "") {
                        //                input.attr("data-verbovalidation-msg", "Seleccionar Verbo es requerido");
                        //                return false;
                        //            }

                        //            return true;
                        //        }
                        //    }, defaultValue: { iCodVerbo: 0, strDescripcion: "strDescripcion" }
                        //},
                        //Objetivo: { validation: { required: true, maxlength: "95" } },
                        Funcion: { validation: { required: true, maxlength: "495" } },
                    }
                }
            },
        });

        this.divGridFunciones = $("#divGridFunciones").kendoGrid({
            dataSource: this.$dataSourceFunciones,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                //{
                //    field: "Verbo",
                //    title: "Verbos",
                //    width: "200px",
                //    editor: controlador.VerboDropDownEditor,
                //    template: "#=Verbo.strDescripcion#"
                //},
                //{
                //    field: "Objetivo",
                //    title: "Objetivo",
                //    attributes: { style: "text-align:left;" },

                //    width: "200px"
                //},
                {
                    field: "Funcion",
                    title: "Descripción",
                    width: "500px",
                    template: function (item) {
                        if (item.Verbo.iCodVerbo == 0)
                            return item.Funcion;
                        else
                            return item.Verbo.strDescripcion + ' ' + item.Objetivo + ' ' + item.Funcion;
                    },
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilCoordInternaVer = function (id) {
        debugger;
        this.$dataSourceCoordInterna = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCoordinacionInterna',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.Coordinacion = $options.Coordinacion;
                            data_param.iTipoCoordinacion = 1;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Coordinacion = $options.Coordinacion;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Coordinacion = '';
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCoordInternas').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        vDescripcion: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridCoordInternas = $("#divGridCoordInternas").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceCoordInterna,
            autoBind: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Coordinacion",
                    title: "COORDINACIÓN INTERNA",
                    width: "500px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilCoordExternaVer = function (id) {
        debugger;
        this.$dataSourceCoordExterna = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCoordinacionExterna',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.Coordinacion = $options.Coordinacion;
                            data_param.iTipoCoordinacion = 2;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Coordinacion = $options.Coordinacion;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Coordinacion = '';
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCoordExternas').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        Coordinacion: { validation: { required: true, maxlength: "490"  } },
                    }
                }
            },
        });

        this.divGridCoordExternas = $("#divGridCoordExternas").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceCoordExterna,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Coordinacion",
                    title: "Coordinacion Externa",
                    width: "500px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosTecnicosVer = function (id) {
        debugger;
        this.$dataSourceConocimientoTecnico = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosTecnicos',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.Conocimientos = $options.Conocimientos;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Conocimientos = $options.Conocimientos;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridConocTec').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        Conocimientos: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridConocTec = $("#divGridConocTec").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoTecnico,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Conocimientos",
                    title: "Conocimientos Tecnicos",
                    width: "500px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosCurProgVer = function (id) {
        debugger;
        this.$dataSourceConocimientoCurProg = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosCursosProgramas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;
                            data_param.Conocimientos = $options.Conocimientos;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;
                            data_param.Conocimientos = $options.Conocimientos;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCursoProg').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        PerfilTipoMateria: {
                            validation: {
                                required: true,
                                tipomateriavalidation: function (input) {
                                    if (input.is("[name='PerfilTipoMateria']") && input.val() == "") {
                                        input.attr("data-tipomateriavalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        Conocimientos: { validation: { required: true } },
                        PerfilNivelMateria: {
                            validation: {
                                required: true,
                                nivelmateriavalidation: function (input) {
                                    if (input.is("[name='PerfilNivelMateria']") && input.val() == "") {
                                        input.attr("data-nivelmateriavalidation-msg", "Seleccionar Nivel de Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoNivelMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        bConDocumento: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridCursoProg = $("#divGridCursoProg").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoCurProg,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "PerfilTipoMateria",
                    title: "TIPO MATERIA",
                    width: "200px",
                    editor: controlador.TipoMateriaDropDownEditor,
                    template: "#=PerfilTipoMateria.strDescripcion#"
                },
                {
                    field: "Conocimientos",
                    title: "CONOCIMIENTOS TÉCNICOS",
                    width: "600px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                }//,
                //{
                //    field: "PerfilNivelMateria",
                //    title: "NIVEL",
                //    width: "200px",
                //    editor: controlador.NivelMateriaDropDownEditor,
                //    template: "#=PerfilNivelMateria.strDescripcion#"
                //},
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosOfficeIdiomasVer = function (id) {
        debugger;
        this.$dataSourceConocimientoOfficeIdiomas = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosOfficeIdiomas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.iCodTipoMateriaOtros = $options.PerfilTipoMateriaOtros.iCodTipoMateriaOtros;
                            data_param.iCodTipoSubMateriaOtros = $options.PerfilTipoSubMateriaOtros.iCodTipoSubMateriaOtros;
                            //data_param.strConocimientos = $options.strConocimientos;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateriaOtros = $options.PerfilTipoMateriaOtros.iCodTipoMateriaOtros;
                            //data_param.strConocimientos = $options.strConocimientos;
                            data_param.iCodTipoSubMateriaOtros = $options.PerfilTipoSubMateriaOtros.iCodTipoSubMateriaOtros;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            //data_param.iCodTipoMateria = $options.MaePerfilTipoMateria.iCodTipoMateria;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridOfimatica_Idiomas').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        PerfilTipoMateriaOtros: {
                            validation: {
                                required: true,
                                tipomateriaotrosvalidation: function (input) {
                                    if (input.is("[name='PerfilTipoMateriaOtros']") && input.val() == "") {
                                        input.attr("data-tipomateriaotrosvalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoMateriaOtros: 0, strDescripcion: "strDescripcion" }
                        },
                        PerfilTipoSubMateriaOtros: {
                            validation: {
                                required: true,
                                tiposubmateriaotrosvalidation: function (input) {
                                    if (input.is("[name='PerfilTipoSubMateriaOtros']") && input.val() == "") {
                                        input.attr("data-tiposubmateriaotrosvalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoSubMateriaOtros: 0, strDescripcion: "strDescripcion" }
                        },
                        //strConocimientos: { validation: { required: true } },
                        PerfilNivelMateria: {
                            validation: {
                                required: true,
                                nivelmateriavalidation: function (input) {
                                    if (input.is("[name='PerfilNivelMateria']") && input.val() == "") {
                                        input.attr("data-nivelmateriavalidation-msg", "Seleccionar Nivel de Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoNivelMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        bConDocumento: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridOfimatica_Idiomas = $("#divGridOfimatica_Idiomas").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoOfficeIdiomas,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "PerfilTipoMateriaOtros",
                    title: "TIPO MATERIA",
                    width: "200px",
                    editor: controlador.TipoMateriaOtrosDropDownEditor,
                    template: "#=PerfilTipoMateriaOtros.strDescripcion#"
                },
                //{
                //    field: "strConocimientos",
                //    title: "Conocimientos Tecnicos",
                //    width: "500px"
                //    //editor: controlador.EstadoDropDownEditor,
                //    //template: "#=Estado.Nombre#"
                //},
                {
                    field: "PerfilTipoSubMateriaOtros",
                    title: "TIPO SUB MATERIA",
                    width: "200px",
                    editor: function (container, options) {
                        $('<input required id="' + options.field + '" name="' + options.field + '"/>')
                            .appendTo(container)
                            .kendoDropDownList({
                                autoBind: true,
                                dataTextField: "strDescripcion",
                                dataValueField: "iCodTipoMateriaOtros",
                                optionLabel: "-- Seleccione --",
                                cascadeFrom: "PerfilTipoMateriaOtros",
                                dataSource: {
                                    serverFiltering: true,
                                    transport: {
                                        read: {
                                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoSubMateriaOtros",
                                            type: "GET",
                                            dataType: "json",
                                            cache: false
                                        },
                                        parameterMap: function ($options, $operation) {
                                            var data_param = {};
                                            data_param.Grilla = {};
                                            data_param.Grilla.RegistrosPorPagina = 0;
                                            data_param.Grilla.OrdenarPor = "strDescripcion";
                                            data_param.Grilla.OrdenarDeForma = "ASC";

                                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                                data_param.strDescripcion = $options.filter.filters[0].value;

                                            data_param.iCodTipoMateriaOtros = $('#PerfilTipoMateriaOtros').data("kendoDropDownList").value();;
                                            return $.toDictionary(data_param);
                                        }
                                    },
                                    requestEnd: function (e) {

                                    }
                                }
                            });
                    },
                    template: "#=PerfilTipoSubMateriaOtros.strDescripcion#"
                },
                {
                    field: "PerfilNivelMateria",
                    title: "NIVEL",
                    width: "200px",
                    editor: controlador.NivelMateriaDropDownEditor,
                    template: "#=PerfilNivelMateria.strDescripcion#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }
    
    this.PerfilPuestoJS.prototype.CargarPerfilHabilidadesVer = function (id) {
        debugger;
        this.$dataSourceHabilidades = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetHabilidades',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridHabilidades').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        Cualidad: {
                            validation: {
                                required: true,
                                cualidadvalidation: function (input) {
                                    if (input.is("[name='Cualidad']") && input.val() == "") {
                                        input.attr("data-cualidadvalidation-msg", "Seleccionar Habilidad es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodCualidad: 0, strNombre: "strNombre" }
                        },
                    }
                }
            },
        });

        this.divGridHabilidades = $("#divGridHabilidades").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceHabilidades,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Cualidad",
                    title: "Habilidades",
                    width: "200px",
                    editor: controlador.CualidadHabilidadDropDownEditor,
                    template: "#=Cualidad.strNombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilCompetenciasVer = function (id) {
        debugger;
        this.$dataSourceCompetencias = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCompetencias',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;

                            //data_param.strVerbo = $options.strVerbo;                            
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            //data_param.strVerbo = $options.strVerbo;                            
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            data_param.iCodTipoCualidad = 0;
                            //data_param.strVerbo = $options.strVerbo;                           
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCompetencias').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        Cualidad: {
                            validation: {
                                required: true,
                                cualidadvalidation: function (input) {
                                    if (input.is("[name='Cualidad']") && input.val() == "") {
                                        input.attr("data-cualidadvalidation-msg", "Seleccionar cualidad es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodCualidad: 0, strNombre: "strNombre" }
                        }
                    }
                }
            },
        });

        this.divGridCompetencias = $("#divGridCompetencias").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceCompetencias,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Cualidad",
                    title: "Compentencias",
                    width: "200px",
                    editor: controlador.CualidadCompetenciaDropDownEditor,
                    template: "#=Cualidad.strNombre#"
                },
                //{
                //    field: "strDescripcion",
                //    title: "Funciones",
                //    width: "500px"
                //    //editor: controlador.EstadoDropDownEditor,
                //    //template: "#=Estado.Nombre#"
                //},
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }

    this.PerfilPuestoJS.prototype.CargarPerfilRequisitosAdicionalesVer = function (id) {
        debugger;
        this.$dataSourceRequisitosAdicionales = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetRequisitosAdicionales',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.Requisito = $options.Requisito;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Requisito = $options.Requisito;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Requisito = '';
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridRequisitosAdicionales').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        Requisito: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridRequisitosAdicionales = $("#divGridRequisitosAdicionales").kendoGrid({
            //toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceRequisitosAdicionales,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Requisito",
                    title: "Requisitos Adicionales",
                    width: "1000px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{ title: "Acciones", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();

    }
    
    this.PerfilPuestoJS.prototype.CargarFormularioPerfilVer = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoPorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#ddlOrgano").data("kendoDropDownList").value(res[0].iCodOrgano);
                //$("#ddlUUOO").kendoDropDownList({
                //    autoBind: true,
                //    cascadeFrom: "ddlOrgano",
                //    optionLabel: "--Seleccione--",
                //    dataTextField: "strUnidad_Organica",
                //    dataValueField: "iCodigoDependencia",
                //    filter: "contains",
                //    minLength: 3,
                //    dataSource: {
                //        serverFiltering: true,
                //        transport: {
                //            read: {
                //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                //                type: "GET",
                //                dataType: "json",
                //                cache: false
                //            },
                //            parameterMap: function ($options, $operation) {
                //                var data_param = {};
                //                if ($options.filter != undefined && $options.filter.filters.length > 0)
                //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                //                data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                //                return $.toDictionary(data_param);
                //            }
                //        }
                //    }
                //});

                //$("#ddlUUOO").data("kendoDropDownList").value(res[0].iCodUnidadOrganica);
                debugger;
                $("#txtOrgano").val(res[0].strOrgano);
                $("#txtUUOO").val(res[0].strUnidadOrganica);
                $("#txtPuestoEstructural").val(res[0].strPuestoEstructural);
                $("#txtNombrePuesto").val(res[0].strNombrePuesto);
                $("#txtDependenciaJerarquicaLineal").val(res[0].strDependenciaJerarquicaLineal);
                $("#txtDependenciaFuncional").val(res[0].strDependenciaFuncional);
                $("#txtPuestoCargo").val(res[0].strPuestos_a_su_Cargo);
                $("#txtMision").val(res[0].strMision);

                $("#txtAnioExperienciaGeneral").val(res[0].iAnioExpGeneral);
                $("#txtAnioExperienciaEspecifica").val(res[0].iAnioExpEspecifica);
                $("#txtDesExperienciaEspecifica").val(res[0].strDesExpEspecifica);
                $("#txtAnioExperienciaSectorPublico").val(res[0].iAnioExpSectorPublico);
                $("#ddlNivelMinimoPuesto").data("kendoDropDownList").value(res[0].iCodNivelMinimo);
                
                debugger;
                //if (_id>0) {
                controlador.CargarPerfilFuncionesVer(_id);
                controlador.CargarPerfilRequisitosAdicionalesVer(_id);
                controlador.CargarPerfilHabilidadesVer(_id);
                controlador.CargarPerfilCompetenciasVer(_id);
                controlador.CargarPerfilCoordInternaVer(_id);
                controlador.CargarPerfilCoordExternaVer(_id);
                controlador.CargarPerfilConocimientosTecnicosVer(_id);
                controlador.CargarPerfilConocimientosCurProgVer(_id);
                controlador.CargarPerfilConocimientosOfficeIdiomasVer(_id);
                controlador.CargarDatosFormAcaNivelBasicoVer(_id);
                controlador.CargarDatosFormAcaNivelEducativoVer(_id);
                controlador.CargarDatosFormAcaMaestriaVer(_id);
                controlador.CargarDatosFormAcaDoctoradoVer(_id);
            },
            error: function (res) {
                debugger;
            }
        });
    }
    ////////////////////////////NUEVO//////////////////////////////////
    debugger;

    this.PerfilPuestoJS.prototype.inicializarNuevoRegistro = function () {
        debugger;

        $("#txtPerfilCese").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date()  }); 
        $("#ddlCabTipoRequerimiento").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoReqPerfil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
            change: function (e) {
                var estado = this.value();

                debugger;
                if (estado == 2) {
                    $('#divCese1').show();
                    $('#divCese2').show();
                }
                else {
                    $('#divCese1').hide();
                    $('#divCese2').hide();
                    $("#txtPerfilNombreCese").val('');
                }
            }
        });
        $("#ddlCabTipoServicio").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoServicioPerfil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
        });
        $("#ddlCabExamenConoc").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarRequiereExamenPerfil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
        });
        $("#ddlCabExamenPsico").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarRequiereExamenPerfil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
        });

        if ($("#hdIdPerfilPuesto").val() != "") {
            $("#ddlOrgano").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                //filter: "contains",
                //minLength: 3,
                dataSource: {
                    //serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlPeriodicidad").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarPeriodicidad",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#txtAnioExperienciaGeneral").attr("required", true);
            $("#txtAnioExperienciaEspecifica").attr("required", true);
            $("#txtAnioExperienciaSectorPublico").attr("required", true);
            //$("#ddlNivelMinimoPuesto").attr("required", true);
            $("#btnFinalizar").show();

            $("#divFunciones").show();
            $("#divCondiciones").show();
            //$("#divCoordinaciones").show();
            $("#divFormacion").show();
            $("#divExperiencia").show();
            $("#divConocimientos").show();
            $("#divHabilidades").show();
            $("#divRequisitos").show();

            frmRegistroAnexo1 = $("#frmRegistroAnexo1").kendoValidator().data("kendoValidator");
            frmRegistroAnexo2 = $("#frmRegistroAnexo2").kendoValidator().data("kendoValidator");
            frmRegistroPerfilMision = $("#frmRegistroPerfilMision").kendoValidator().data("kendoValidator");
            frmRegistroPerfilExperiencia = $("#frmRegistroPerfilExperiencia").kendoValidator().data("kendoValidator");
            frmRegistroNivelBasico = $("#frmRegistroNivelBasico").kendoValidator().data("kendoValidator");
            frmRegistroNivelEducativo = $("#frmRegistroNivelEducativo").kendoValidator().data("kendoValidator");
            frmRegistroMaestria = $("#frmRegistroMaestria").kendoValidator().data("kendoValidator");
            frmRegistroDoctorado = $("#frmRegistroDoctorado").kendoValidator().data("kendoValidator");

            //alert($("#hdIdPerfilPuesto").val())
            var id = $("#hdIdPerfilPuesto").val();
            this.CargarFormularioAnexo1(id);
            this.CargarFormularioAnexo2(id);
        }
        else {
            this.CargarFormularioFunciones($("#hdIdCodTrabajador").val());

            $("#ddlOrgano").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                //filter: "contains",
                //minLength: 3,
                dataSource: {
                    //serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlPeriodicidad").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarPeriodicidad",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlOrgano").data('kendoDropDownList').value(CodOrgano);

            $("#ddlUUOO").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlOrgano",
                optionLabel: "NO APLICA",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;

                            data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlUUOO").data('kendoDropDownList').value(CodDependencia);
            $("#txtDependenciaJerarquicaLineal").removeAttr("readonly");

            frmRegistroAnexo1 = $("#frmRegistroAnexo1").kendoValidator().data("kendoValidator");
            frmRegistroAnexo2 = $("#frmRegistroAnexo2").kendoValidator().data("kendoValidator");
            frmRegistroPerfilMision = $("#frmRegistroPerfilMision").kendoValidator().data("kendoValidator");
            frmRegistroPerfilExperiencia = $("#frmRegistroPerfilExperiencia").kendoValidator().data("kendoValidator");
            frmRegistroNivelBasico = $("#frmRegistroNivelBasico").kendoValidator().data("kendoValidator");
            frmRegistroNivelEducativo = $("#frmRegistroNivelEducativo").kendoValidator().data("kendoValidator");
            frmRegistroMaestria = $("#frmRegistroMaestria").kendoValidator().data("kendoValidator");
            frmRegistroDoctorado = $("#frmRegistroDoctorado").kendoValidator().data("kendoValidator");

            if ($("#hdIdPerfilPuesto").val() != "") {
                controlador.CargarPerfilFunciones(-1);
                controlador.CargarPerfilRequisitosAdicionales(-1);
                controlador.CargarPerfilHabilidades(-1);
                controlador.CargarPerfilCompetencias(-1);
                controlador.CargarPerfilCoordInterna(-1);
                controlador.CargarPerfilCoordExterna(-1);
                controlador.CargarPerfilConocimientosTecnicos(-1);
                controlador.CargarPerfilConocimientosCurProg(-1);
                controlador.CargarPerfilConocimientosOfficeIdiomas(-1);
                controlador.CargarDatosFormAcaNivelBasico(-1);
                controlador.CargarDatosFormAcaNivelEducativo(-1);
                controlador.CargarDatosFormAcaMaestria(-1);
                controlador.CargarDatosFormAcaDoctorado(-1);
            }
        }
        /// fin   
        $("#ddlNivelAlcanzado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelAlcanzadoMaestria").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoMaestria",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelAlcanzadoDoctorado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoDoctorado",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelEducativo").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodGrado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarGrados",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelEducativoBasico").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodGrado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarGradosBasico",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        //$("#ddlCarreraN1").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strDescripcion",
        //    dataValueField: "strCodCarrera",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strDescripcion = $options.filter.filters[0].value;
        //                data_param.iCodTipoCarrera = 4;
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        /*
        $("#ddlCarreraN1").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlNivelEducativo",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        var iCodGrado = $('#ddlNivelEducativo').data("kendoDropDownList").value();
                        var iCodTipoCarrera = 0;
                        debugger;
                        //data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();
                        if (iCodGrado > 2) {
                            switch (iCodGrado) {
                                case "3":
                                    {
                                        iCodTipoCarrera = 1;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                                case "4":
                                    {
                                        iCodTipoCarrera = 2;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                                case "5":
                                    {
                                        iCodTipoCarrera = 4;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                            }                                
                        }
                        else {                            
                            $('#ddlCarreraN1').data("kendoDropDownList").readonly();
                        }

                        data_param.iCodTipoCarrera = iCodTipoCarrera;

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN1Maestria").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        data_param.iCodTipoCarrera = 5;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN1Doctorado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        data_param.iCodTipoCarrera = 6;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2Maestria").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2Doctorado").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3Maestria").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3Doctorado").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        */

        $("#ddlCarreraN4").kendoDropDownList({
            autoBind: true,
            //cascadeFrom: "ddlCarreraN3",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la carrera profesional ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevaCarrera();'>CREAR CARRERA</button>"
        });

        $("#ddlCarreraN4Maestria").kendoDropDownList({
            autoBind: false,
            //cascadeFrom: "ddlCarreraN3Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la especialidad ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevaMaestria();'>CREAR MAESTRÍA</button>"
        });

        $("#ddlCarreraN4Doctorado").kendoDropDownList({
            autoBind: false,
            //cascadeFrom: "ddlCarreraN3Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la especialidad ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevoDoctorado();'>CREAR DOCTORADO</button>"
        });
    };

    this.PerfilPuestoJS.prototype.inicializarNuevo = function () {
        debugger;

        if ($("#hdIdPerfilPuesto").val() != "") {

            $("#ddlOrgano").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                //filter: "contains",
                //minLength: 3,
                dataSource: {
                    //serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlPeriodicidad").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarPeriodicidad",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#txtAnioExperienciaGeneral").attr("required", true);
            $("#txtAnioExperienciaEspecifica").attr("required", true);
            $("#txtAnioExperienciaSectorPublico").attr("required", true);
            //$("#ddlNivelMinimoPuesto").attr("required", true);
            $("#btnFinalizar").show();

            $("#divFunciones").show();
            $("#divCondiciones").show();
            //$("#divCoordinaciones").show();
            $("#divFormacion").show();
            $("#divExperiencia").show();
            $("#divConocimientos").show();
            $("#divHabilidades").show();
            $("#divRequisitos").show();

            frmRegistroAnexo1 = $("#frmRegistroAnexo1").kendoValidator().data("kendoValidator");
            frmRegistroAnexo2 = $("#frmRegistroAnexo2").kendoValidator().data("kendoValidator");
            frmRegistroPerfilMision = $("#frmRegistroPerfilMision").kendoValidator().data("kendoValidator");
            frmRegistroPerfilExperiencia = $("#frmRegistroPerfilExperiencia").kendoValidator().data("kendoValidator");
            frmRegistroNivelBasico = $("#frmRegistroNivelBasico").kendoValidator().data("kendoValidator");
            frmRegistroNivelEducativo = $("#frmRegistroNivelEducativo").kendoValidator().data("kendoValidator");
            frmRegistroMaestria = $("#frmRegistroMaestria").kendoValidator().data("kendoValidator");
            frmRegistroDoctorado = $("#frmRegistroDoctorado").kendoValidator().data("kendoValidator");

            //alert($("#hdIdPerfilPuesto").val())
            var id = $("#hdIdPerfilPuesto").val();
            this.CargarFormularioAnexo2(id);
        }
        else {
            this.CargarFormularioFunciones($("#hdIdCodTrabajador").val());
            
            $("#ddlOrgano").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                //filter: "contains",
                //minLength: 3,
                dataSource: {
                    //serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlNivelMinimoPuesto").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivelMinimo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMimino",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlPeriodicidad").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "Nombre",
                dataValueField: "Codigo",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarPeriodicidad",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlOrgano").data('kendoDropDownList').value(CodOrgano);

            $("#ddlUUOO").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlOrgano",
                optionLabel: "NO APLICA",
                dataTextField: "strUnidad_Organica",
                dataValueField: "iCodigoDependencia",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strUnidad_Organica = $options.filter.filters[0].value;

                            data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlUUOO").data('kendoDropDownList').value(CodDependencia);
            $("#txtDependenciaJerarquicaLineal").removeAttr("readonly");

            frmRegistroAnexo1 = $("#frmRegistroAnexo1").kendoValidator().data("kendoValidator");
            frmRegistroAnexo2 = $("#frmRegistroAnexo2").kendoValidator().data("kendoValidator");
            frmRegistroPerfilMision = $("#frmRegistroPerfilMision").kendoValidator().data("kendoValidator");
            frmRegistroPerfilExperiencia = $("#frmRegistroPerfilExperiencia").kendoValidator().data("kendoValidator");
            frmRegistroNivelBasico = $("#frmRegistroNivelBasico").kendoValidator().data("kendoValidator");
            frmRegistroNivelEducativo = $("#frmRegistroNivelEducativo").kendoValidator().data("kendoValidator");
            frmRegistroMaestria = $("#frmRegistroMaestria").kendoValidator().data("kendoValidator");
            frmRegistroDoctorado = $("#frmRegistroDoctorado").kendoValidator().data("kendoValidator");
            
            if ($("#hdIdPerfilPuesto").val() != "")
            {
                controlador.CargarPerfilFunciones(-1);
                controlador.CargarPerfilRequisitosAdicionales(-1);
                controlador.CargarPerfilHabilidades(-1);
                controlador.CargarPerfilCompetencias(-1);
                controlador.CargarPerfilCoordInterna(-1);
                controlador.CargarPerfilCoordExterna(-1);
                controlador.CargarPerfilConocimientosTecnicos(-1);
                controlador.CargarPerfilConocimientosCurProg(-1);
                controlador.CargarPerfilConocimientosOfficeIdiomas(-1);
                controlador.CargarDatosFormAcaNivelBasico(-1);
                controlador.CargarDatosFormAcaNivelEducativo(-1);
                controlador.CargarDatosFormAcaMaestria(-1);
                controlador.CargarDatosFormAcaDoctorado(-1);
            }
        }
           /// fin   
        $("#ddlNivelAlcanzado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelAlcanzadoMaestria").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoMaestria",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelAlcanzadoDoctorado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodNivel",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoDoctorado",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        
        $("#ddlNivelEducativo").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodGrado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarGrados",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlNivelEducativoBasico").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "iCodGrado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarGradosBasico",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        //$("#ddlCarreraN1").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strDescripcion",
        //    dataValueField: "strCodCarrera",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strDescripcion = $options.filter.filters[0].value;
        //                data_param.iCodTipoCarrera = 4;
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        /*
        $("#ddlCarreraN1").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlNivelEducativo",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        var iCodGrado = $('#ddlNivelEducativo').data("kendoDropDownList").value();
                        var iCodTipoCarrera = 0;
                        debugger;
                        //data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();
                        if (iCodGrado > 2) {
                            switch (iCodGrado) {
                                case "3":
                                    {
                                        iCodTipoCarrera = 1;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                                case "4":
                                    {
                                        iCodTipoCarrera = 2;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                                case "5":
                                    {
                                        iCodTipoCarrera = 4;
                                        //data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                                        $('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                                        break;
                                    }
                            }                                
                        }
                        else {                            
                            $('#ddlCarreraN1').data("kendoDropDownList").readonly();
                        }

                        data_param.iCodTipoCarrera = iCodTipoCarrera;

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN1Maestria").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        data_param.iCodTipoCarrera = 5;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN1Doctorado").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;
                        data_param.iCodTipoCarrera = 6;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2Maestria").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN2Doctorado").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN1Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN1Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3Maestria").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $("#ddlCarreraN3Doctorado").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlCarreraN2Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = $('#ddlCarreraN2Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        */

        $("#ddlCarreraN4").kendoDropDownList({
            autoBind: true,
            //cascadeFrom: "ddlCarreraN3",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la carrera profesional ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevaCarrera();'>CREAR CARRERA</button>"
        });

        $("#ddlCarreraN4Maestria").kendoDropDownList({
            autoBind: false,
            //cascadeFrom: "ddlCarreraN3Maestria",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Mae",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3Maestria').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la especialidad ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevaMaestria();'>CREAR MAESTRÍA</button>"
        });

        $("#ddlCarreraN4Doctorado").kendoDropDownList({
            autoBind: false,
            //cascadeFrom: "ddlCarreraN3Doctorado",
            optionLabel: "--Seleccione--",
            dataTextField: "strDescripcion",
            dataValueField: "strCodCarrera",
            filter: "contains",
            minLength: 5,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Doc",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strDescripcion = $options.filter.filters[0].value;

                        data_param.strCodCarrera = ''; //$('#ddlCarreraN3Doctorado').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                    }
                }
            },
            noDataTemplate: "No existe la especialidad ingresada!... <button class='btn btn-info btn-xs' onclick='controlador.mostrarNuevoDoctorado();'>CREAR DOCTORADO</button>"
        });
    };

    

    this.PerfilPuestoJS.prototype.CargarFormularioAnexo1 = function (_id) {
        var data_param = new FormData();
        
        data_param.append('id', _id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesAnexo1PorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#ddlOrgano").data("kendoDropDownList").value(res[0].iCodOrgano);
                //$("#ddlUUOO").kendoDropDownList({
                //    autoBind: true,
                //    cascadeFrom: "ddlOrgano",
                //    optionLabel: "NO APLICA",
                //    dataTextField: "strUnidad_Organica",
                //    dataValueField: "iCodigoDependencia",
                //    filter: "contains",
                //    minLength: 3,
                //    dataSource: {
                //        serverFiltering: true,
                //        transport: {
                //            read: {
                //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                //                type: "GET",
                //                dataType: "json",
                //                cache: false
                //            },
                //            parameterMap: function ($options, $operation) {
                //                var data_param = {};
                //                if ($options.filter != undefined && $options.filter.filters.length > 0)
                //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                //                data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                //                return $.toDictionary(data_param);
                //            }
                //        }
                //    }
                //});

                //$("#ddlUUOO").data("kendoDropDownList").value(res[0].iCodUnidadOrganica);
                //$("#ddlUUOO").data("kendoDropDownList").readonly();
                //$("#hdiCodOrgano").val(res[0].iCodOrgano);
                //$("#hdiCodDependencia").val(res[0].iCodUnidadOrganica);
                //$("#txtOrgano").val(res[0].strOrgano);
                //$("#txtUUOO").val(res[0].strUnidadOrganica);

                $("#ddlCabTipoRequerimiento").data("kendoDropDownList").value(res[0].iTipoReq);
                if (res[0].iTipoReq == 2) {
                    $('#divCese1').show();
                    $('#divCese2').show();
                    $("#txtPerfilCese").data("kendoDatePicker").value(kendo.parseDate(res.datFechaCese));
                    $("#txtPerfilNombreCese").val(res[0].strTrabajadorCese);
                }
                else {
                    $('#divCese1').hide();
                    $('#divCese2').hide();
                    $("#txtPerfilNombreCese").val('');
                }
                
                $("#txtNombrePuesto").val(res[0].strNombrePuesto);
                $("#txtCabNombrePuesto").val(res[0].strNombrePuesto);
                $("#txtCabRemuneracion").val(res[0].strRemuneracion);
                $("#txtCabCantPuestos").val(res[0].iPosiciones);
                $("#txtCabPeriodoCont").val(res[0].strPeriodo);
                $("#txtCabMeta").val(res[0].strMeta);
                $("#ddlCabTipoServicio").data("kendoDropDownList").value(res[0].iTipoServicio);

                $("#txtCabJustificacion").val(res[0].strJustificacion);
                $("#ddlCabExamenConoc").data("kendoDropDownList").value(res[0].iConocimiento);
                $("#ddlCabExamenPsico").data("kendoDropDownList").value(res[0].iPsicologico);

            },
            error: function (res) {
                debugger;
            }
        });
    }
    this.PerfilPuestoJS.prototype.CargarFormularioAnexo2 = function (_id) {
        var data_param = new FormData();

        data_param.append('id', _id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoPorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#ddlOrgano").data("kendoDropDownList").value(res[0].iCodOrgano);
                $("#ddlUUOO").kendoDropDownList({
                    autoBind: true,
                    cascadeFrom: "ddlOrgano",
                    optionLabel: "NO APLICA",
                    dataTextField: "strUnidad_Organica",
                    dataValueField: "iCodigoDependencia",
                    filter: "contains",
                    minLength: 3,
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: {
                                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                                type: "GET",
                                dataType: "json",
                                cache: false
                            },
                            parameterMap: function ($options, $operation) {
                                var data_param = {};
                                if ($options.filter != undefined && $options.filter.filters.length > 0)
                                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                                data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                                return $.toDictionary(data_param);
                            }
                        }
                    }
                });

                $("#ddlUUOO").data("kendoDropDownList").value(res[0].iCodUnidadOrganica);
                $("#ddlUUOO").data("kendoDropDownList").readonly();
                $("#hdiCodOrgano").val(res[0].iCodOrgano);
                $("#hdiCodDependencia").val(res[0].iCodUnidadOrganica);
                $("#txtOrgano").val(res[0].strOrgano);
                $("#txtUUOO").val(res[0].strUnidadOrganica);
                $("#txtPuestoEstructural").val(res[0].strPuestoEstructural);
                $("#txtNombrePuesto").val(res[0].strNombrePuesto);
                $("#txtDependenciaJerarquicaLineal").val(res[0].strDependenciaJerarquicaLineal);
                $("#txtDependenciaFuncional").val(res[0].strDependenciaFuncional);
                $("#txtPuestoCargo").val(res[0].strPuestos_a_su_Cargo);
                $("#txtMision").val(res[0].strMision);

                $("#txtAnioExperienciaGeneral").val(res[0].iAnioExpGeneral);
                $("#txtAnioExperienciaEspecifica").val(res[0].iAnioExpEspecifica);
                $("#txtDesExperienciaEspecifica").val(res[0].strDesExpEspecifica);
                $("#txtAnioExperienciaSectorPublico").val(res[0].iAnioExpSectorPublico);
                $("#ddlNivelMinimoPuesto").data("kendoDropDownList").value(res[0].iCodNivelMinimo);
                $("#ddlPeriodicidad").data("kendoDropDownList").value(res[0].iPeriodicidad);
                $("#txtPeriodicidad").val(res[0].strPeriodicidad);
                $("#txtCondiciones").val(res[0].strCondiciones);

                debugger;
                //if (_id>0) {
                controlador.CargarPerfilFunciones(_id);
                controlador.CargarPerfilRequisitosAdicionales(_id);
                controlador.CargarPerfilHabilidades(_id);
                controlador.CargarPerfilCompetencias(_id);
                controlador.CargarPerfilCoordInterna(_id);
                controlador.CargarPerfilCoordExterna(_id);
                controlador.CargarPerfilConocimientosTecnicos(_id);
                controlador.CargarPerfilConocimientosCurProg(_id);
                controlador.CargarPerfilConocimientosOfficeIdiomas(_id);
                controlador.CargarDatosFormAcaNivelBasico(_id);
                controlador.CargarDatosFormAcaNivelEducativo(_id);
                controlador.CargarDatosFormAcaMaestria(_id);
                controlador.CargarDatosFormAcaDoctorado(_id);

                if (res[0].strEstadoCompletado == 'Finalizado') {
                    $("#divAnexo1").hide();
                    $("#divAnexo2").hide();
                    $("#btnSolEvaluacion").hide();
                }
                else {
                    $("#divAnexo1").show();
                    $("#divAnexo2").show();
                    $("#btnSolEvaluacion").show();
                }
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.PerfilPuestoJS.prototype.registrarNivelBasico = function (e) {
        e.preventDefault();
        var id = $("#hdIdPerfilPuesto").val();
        if (id > 0) {
        var metodo = 'InsertarDetFormacionAcademica';
        debugger;
        if (frmRegistroNivelBasico.validate()) {
            var modal = $('#ModalAgregarNivelBasico').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) 
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
            
            if ($("#hdISecuenciaNivelEducativoBasico").val() != 0) {

                data_param.append('iSecuencia', $("#hdISecuenciaNivelEducativoBasico").val());
                metodo = 'ActualizarDetFormacionAcademica';
            }
            else 
                metodo = 'InsertarDetFormacionAcademica';
            
            data_param.append('iCodGrado', $("#ddlNivelEducativoBasico").data("kendoDropDownList").value());
            
            if ($("#chkCompletoNivelEducBasico").prop('checked')) 
                data_param.append('bCompleto', true);
            else 
                data_param.append('bCompleto', false);
            
            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Formacion básica registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                controlador.CargarDatosFormAcaNivelBasico($("#hdIdPerfilPuesto").val());
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }

    }

    this.PerfilPuestoJS.prototype.registrarNivelEducativo = function (e) {
        e.preventDefault();
        var id = $("#hdIdPerfilPuesto").val();
        if (id > 0) {
        var metodo = 'InsertarDetFormacionAcademica';

        if (frmRegistroNivelEducativo.validate()) {
            var modal = $('#ModalAgregarNivelEducativo').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) {
                
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                //metodo = 'Guardar';
            }

            if ($("#hdISecuenciaNivelEducativo").val() != 0) {

                data_param.append('iSecuencia', $("#hdISecuenciaNivelEducativo").val());
                metodo = 'ActualizarDetFormacionAcademica';
            }
            else {
                metodo = 'InsertarDetFormacionAcademica';
            }

            data_param.append('iCodGrado', $("#ddlNivelEducativo").data("kendoDropDownList").value());
            data_param.append('iCodNivel', $("#ddlNivelAlcanzado").data("kendoDropDownList").value());
            

            var iCodGrado = $('#ddlNivelEducativo').data("kendoDropDownList").value();
            var iCodTipoCarrera = 0;
            debugger;
            //data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();
            //if (iCodGrado > 2) {
                switch (iCodGrado) {
                    case "3":
                        {
                            iCodTipoCarrera = 1;
                            data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                            //$('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                            break;
                        }
                    case "4":
                        {
                            iCodTipoCarrera = 2;
                            data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                            //$('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                            break;
                        }
                    case "5":
                        {
                            iCodTipoCarrera = 4;
                            data_param.append('strCodCarrera', $("#ddlCarreraN4").data("kendoDropDownList").value());
                            //$('#ddlCarreraN1').data("kendoDropDownList").enable(true);
                            break;
                        }
                        
                }
            //}
            //else {
            //    $('#ddlCarreraN1').data("kendoDropDownList").readonly();
            //}

            //data_param.iCodTipoCarrera = iCodTipoCarrera;

            data_param.append('iCodSubTipoCarrera', iCodTipoCarrera);

            if ($("#chkCompletoNivelEduc").prop('checked')) {
                data_param.append('bCompleto', true);
            }
            else {
                data_param.append('bCompleto', false);
            }
            
            if ($("#chkColegiado").prop('checked')) {
                data_param.append('bColegiatura', true);
            }
            else {
                data_param.append('bColegiatura', false);
            }
            if ($("#chkHabilitado").prop('checked')) {
                data_param.append('bHabilitado', true);
            }
            else {
                data_param.append('bHabilitado', false);
            }
            
            
            

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Formacion académica registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                controlador.CargarDatosFormAcaNivelEducativo($("#hdIdPerfilPuesto").val());
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);
                                                                
                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();
            
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.registrarMaestria = function (e) {
        e.preventDefault();
        var id = $("#hdIdPerfilPuesto").val();
        if (id > 0) {
        var metodo = 'InsertarDetFormacionAcademica';

        if (frmRegistroMaestria.validate()) {
            var modal = $('#ModalAgregarMaestria').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) {

                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                //metodo = 'Guardar';
            }

            if ($("#hdISecuenciaMaestria").val() != 0) {

                data_param.append('iSecuencia', $("#hdISecuenciaMaestria").val());
                metodo = 'ActualizarDetFormacionAcademica';
            }
            else {
                metodo = 'InsertarDetFormacionAcademica';
            }

            data_param.append('iCodGrado', 6);
            data_param.append('iCodNivel', $("#ddlNivelAlcanzadoMaestria").data("kendoDropDownList").value());
            data_param.append('strCodCarrera', $("#ddlCarreraN4Maestria").data("kendoDropDownList").value());
            data_param.append('iCodSubTipoCarrera', 5);




            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Formacion académica registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                controlador.CargarDatosFormAcaMaestria($("#hdIdPerfilPuesto").val());
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.registrarDoctorado = function (e) {
        e.preventDefault();
        var id = $("#hdIdPerfilPuesto").val();
        if (id > 0) {
        var metodo = 'InsertarDetFormacionAcademica';

        if (frmRegistroDoctorado.validate()) {
            var modal = $('#ModalAgregarDoctorado').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) {

                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                //metodo = 'Guardar';
            }

            if ($("#hdISecuenciaDoctorado").val() != 0) {

                data_param.append('iSecuencia', $("#hdISecuenciaDoctorado").val());
                metodo = 'ActualizarDetFormacionAcademica';
            }
            else {
                metodo = 'InsertarDetFormacionAcademica';
            }

            data_param.append('iCodGrado', 7);
            data_param.append('iCodNivel', $("#ddlNivelAlcanzadoDoctorado").data("kendoDropDownList").value());
            data_param.append('strCodCarrera', $("#ddlCarreraN4Doctorado").data("kendoDropDownList").value());
            data_param.append('iCodSubTipoCarrera', 6);




            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Formacion académica registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                controlador.CargarDatosFormAcaDoctorado($("#hdIdPerfilPuesto").val());
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }
        
    this.PerfilPuestoJS.prototype.CargarFormularioFunciones = function (_id) {
        //alert('funciones ' + _id);
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                CodOrgano = res.iCodOrgano;
                CodDependencia = res.iCodDependencia;
                $("#ddlOrgano").data('kendoDropDownList').value(res.iCodOrgano);
                $("#ddlUUOO").data('kendoDropDownList').value(res.iCodDependencia);
                $("#hdiCodOrgano").val(res.iCodOrgano);
                $("#hdiCodDependencia").val(res.iCodDependencia);
                //$("#ddlOrgano").data('kendoDropDownList').value(res.iCodOrgano);
                //$("#ddlUUOO").data('kendoDropDownList').value(res.iCodDependencia);
                $("#txtOrgano").val(res.strOrgano);
                $("#txtUUOO").val(res.strUnidad_Organica);
                //$("#ddlUUOO").data('kendoDropDownList').readonly();
                //$("#txtDependenciaJerarquicaLineal").val(res.strDependencia_Jerarquica_Lineal);
                //$("#txtDependenciaFuncional").val(res.strDependencia_Funcional);
            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(res);
    }

    this.PerfilPuestoJS.prototype.CargarBandeja = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                CodOrgano = res.iCodOrgano;
                CodDependencia = res.iCodDependencia;
                //$("#ddlOrgano").data('kendoDropDownList').value(res.iCodOrgano);
                //$("#ddlUUOO").data('kendoDropDownList').value(res.iCodDependencia);
                //$("#ddlUUOO").data("kendoDropDownList").readonly();
                $("#txtOrgano").val(res.strOrgano);
                $("#txtUUOO").val(res.strUnidad_Organica);
                $("#txtDependenciaJerarquicaLineal").val(res.strDependencia_Jerarquica_Lineal);
                //$("#txtDependenciaFuncional").val(res.strDependencia_Funcional);
            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(res);
    }

    this.PerfilPuestoJS.prototype.RegAnexo1 = function (e) {
        e.preventDefault();
        var metodo = '';
        if ($("#hdIdPerfilPuesto").val() != "") 
            metodo = 'ActualizarPerfilAnexo1';
        else 
            metodo = 'RegistrarPerfilAnexo1';
        
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        debugger;
        if (frmRegistroAnexo1.validate()) {
            var esValido = true;
            var mensajeValidacion = '¿Está seguro de registrar el anexo 1 del perfil de puesto?';
            var mensajeExito = 'El anexo 1 se actualizó correctamente';
            var data_param = new FormData();           

            if ($("#hdIdPerfilPuesto").val() != "") {
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                mensajeValidacion = '¿Está seguro de actualizar el anexo 1 del perfil de puesto?';
                mensajeExito = 'Anexo 1 actualizado correctamente';
            }
            //data_param.append('iCodOrgano', $("#hdiCodOrgano").val());
            //data_param.append('iCodUnidadOrganica', $("#hdiCodDependencia").val());

            debugger;

            data_param.append('iCodOrgano', $("#ddlOrgano").data("kendoDropDownList").value());
            data_param.append('iCodUnidadOrganica', $("#ddlUUOO").data("kendoDropDownList").value());
            data_param.append('iTipoReq', $("#ddlCabTipoRequerimiento").data("kendoDropDownList").value());
            if ($("#ddlCabTipoRequerimiento").data("kendoDropDownList").value() == 2) {
                var cese = kendo.toString(kendo.parseDate($("#txtPerfilCese").data("kendoDatePicker").value()), 'dd/MM/yyyy');
                if (cese == null) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de fin de labores no es válida');
                    $("#txtPerfilCese").focus();
                    return;
                }

                data_param.append('datFechaCese', kendo.toString(kendo.parseDate($("#txtPerfilCese").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
                data_param.append('strTrabajadorCese', $("#txtPerfilNombreCese").val());
            }
            else
                data_param.append('strTrabajadorCese', '');
                
            
            data_param.append('strNombrePuesto', $("#txtCabNombrePuesto").val());
            data_param.append('strRemuneracion', $("#txtCabRemuneracion").val());
            data_param.append('iPosiciones', $("#txtCabCantPuestos").val());
            data_param.append('strPeriodo', $("#txtCabPeriodoCont").val());
            data_param.append('strMeta', $("#txtCabMeta").val());
            data_param.append('iTipoServicio', $("#ddlCabTipoServicio").data("kendoDropDownList").value());
            data_param.append('strJustificacion', $("#txtCabJustificacion").val());
            data_param.append('iConocimiento', $("#ddlCabExamenConoc").data("kendoDropDownList").value());
            data_param.append('iPsicologico', $("#ddlCabExamenPsico").data("kendoDropDownList").value());
            //data_param.append('bEstado', 1);
            data_param.append('iCodTrabajador', $("#hdIdCodTrabajador").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensajeValidacion, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio(mensajeExito);

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.Actualizar($("#hdIdPerfilPuesto").val());
                                //window.location.assign("ActualizarPerfil?id=" + $("#hdIdPerfilPuesto").val());
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
    this.PerfilPuestoJS.prototype.RegAnexo2 = function (e) {
        e.preventDefault();
        var metodo = '';
        if ($("#hdIdPerfilPuesto").val() != "")
            metodo = 'ActualizarPerfilCab';
        else
            metodo = 'RegistrarPerfilCab';

        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        debugger;
        if (frmRegistroAnexo2.validate()) {
            var esValido = true;
            var mensajeValidacion = '¿Está seguro de registrar el anexo 2 del perfil de puesto?';
            var mensajeExito = 'El anexo 2 se actualizó correctamente';
            var data_param = new FormData();

            if ($("#hdIdPerfilPuesto").val() != "") {
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                mensajeValidacion = '¿Está seguro de actualizar el anexo 2 del perfil de puesto?';
                mensajeExito = 'Anexo 2 actualizado correctamente';
            }
            //data_param.append('iCodOrgano', $("#hdiCodOrgano").val());
            //data_param.append('iCodUnidadOrganica', $("#hdiCodDependencia").val());

            data_param.append('iCodOrgano', $("#ddlOrgano").data("kendoDropDownList").value());
            data_param.append('iCodUnidadOrganica', $("#ddlUUOO").data("kendoDropDownList").value());
            data_param.append('strPuestoEstructural', $("#txtPuestoEstructural").val());
            data_param.append('strNombrePuesto', $("#txtNombrePuesto").val());
            data_param.append('strDependenciaJerarquicaLineal', $("#txtDependenciaJerarquicaLineal").val());
            data_param.append('strDependenciaFuncional', $("#txtDependenciaFuncional").val());
            data_param.append('strPuestos_a_su_Cargo', $("#txtPuestoCargo").val());
            data_param.append('strMision', $("#txtMision").val());

            data_param.append('iAnioExpGeneral', $("#txtAnioExperienciaGeneral").val());
            data_param.append('iAnioExpEspecifica', $("#txtAnioExperienciaEspecifica").val());
            data_param.append('strDesExpEspecifica', $("#txtDesExperienciaEspecifica").val());
            data_param.append('iAnioExpSectorPublico', $("#txtAnioExperienciaSectorPublico").val());
            data_param.append('iCodNivelMinimo', $("#ddlNivelMinimoPuesto").data("kendoDropDownList").value());

            data_param.append('iPeriodicidad',  $("#ddlPeriodicidad").data("kendoDropDownList").value());
            data_param.append('strPeriodicidad',  $("#txtPeriodicidad").val());
            data_param.append('strCondiciones',  $("#txtCondiciones").val());

            controladorApp.abrirMensajeDeConfirmacion(
                mensajeValidacion, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio(mensajeExito);

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.Actualizar($("#hdIdPerfilPuesto").val());
                                //window.location.assign("ActualizarPerfil?id=" + $("#hdIdPerfilPuesto").val());
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


    this.PerfilPuestoJS.prototype.RegMision = function (e) {
        e.preventDefault();
        var id = $("#hdIdPerfilPuesto").val();
        if (id > 0)
        {
            var metodo = 'RegistrarPerfilMision';
            //controladorApp.notificarMensajeDeAlerta('hola');
            //if (frmPerfilesValidador.validate()) {
            if (frmRegistroPerfilMision.validate()) {
                var esValido = true;
                var mensajeValidacion = '';
                var data_param = new FormData();

                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val()); 
                data_param.append('strMision', $("#txtMision").val());           

                debugger;

                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de agregar el perfil?', 'SI', 'NO'
                    , function (arg) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                    controladorApp.notificarMensajeSatisfactorio("Misión - Perfil registrado correctamente");

                                    // REFRESCAR INFORMACION DEL TRABAJADOR
                                    $("#hdIdPerfilPuesto").val(res.responseText);
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
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
            
    }

    //this.PerfilPuestoJS.prototype.RegExperiencia = function (e) {
    //    e.preventDefault();
    //    var id = $("#hdIdPerfilPuesto").val();
    //    if (id > 0) 
    //    {
    //        var metodo = 'RegistrarPerfilExperiencia';
    //        //controladorApp.notificarMensajeDeAlerta('hola');
    //        //if (frmPerfilesValidador.validate()) {
    //        if (frmRegistroPerfilExperiencia.validate()) {
    //            var esValido = true;
    //            var mensajeValidacion = '';
    //            var data_param = new FormData();

    //            data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
    //            data_param.append('iAnioExpGeneral', $("#txtAnioExperienciaGeneral").val());
    //            data_param.append('iAnioExpEspecifica', $("#txtAnioExperienciaEspecifica").val());
    //            data_param.append('iAnioExpSectorPublico', $("#txtAnioExperienciaSectorPublico").val());            
    //            data_param.append('iCodNivelMinimo', $("#ddlNivelMinimoPuesto").data("kendoDropDownList").value());

    //            debugger;

    //            controladorApp.abrirMensajeDeConfirmacion(
    //                '¿Está seguro de agregar la experiencia?', 'SI', 'NO'
    //                , function (arg) {
    //                    $.ajax({
    //                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
    //                        type: 'POST',
    //                        dataType: 'json',
    //                        contentType: false,
    //                        processData: false,
    //                        data: arg,
    //                        success: function (res) {
    //                            debugger;
    //                            if (res.success == 'False') {
    //                                controladorApp.notificarMensajeDeAlerta(res.responseText);
    //                            }
    //                            else {
    //                                controladorApp.notificarMensajeSatisfactorio("Experiencia - Perfil registrado correctamente");

    //                                // REFRESCAR INFORMACION DEL TRABAJADOR
    //                                $("#hdIdPerfilPuesto").val(res.responseText);
    //                            }
    //                        },
    //                        error: function (res) {
    //                            //alert(res);
    //                        }
    //                    });
    //                }, data_param);
    //        }
    //        else {
    //            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
    //        }
    //    } else {
    //        controladorApp.notificarMensajeDeAlerta('No existe registro');
    //    }
    //}

    this.PerfilPuestoJS.prototype.CargarPerfilFunciones = function (id) {
        debugger;
        
        if (id > 0) {
            this.$dataSourceFunciones = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFunciones',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    update: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarFunciones",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    destroy: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarFunciones",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    create: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarFunciones",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },                
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        switch ($operation) {                        
                            case "read":
                                data_param.iCodPerfil = id;
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
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                                data_param.iSecuencia = 0;
                                data_param.iCodVerbo = 0; //$options.Verbo.iCodVerbo;
                                //data_param.strVerbo = $options.strVerbo;
                                data_param.Objetivo = ''; //$options.Objetivo;
                                data_param.Funcion = $options.Funcion;
                                //}
                                break;
                            case "update":
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                                data_param.iSecuencia = $options.iSecuencia;
                                data_param.iCodVerbo = 0; //$options.Verbo.iCodVerbo;
                                //data_param.strVerbo = $options.strVerbo;
                                data_param.Objetivo = ''; //$options.Objetivo;
                                data_param.Funcion = $options.Funcion;
                                break;
                            case "destroy":
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                                data_param.iSecuencia = $options.iSecuencia;
                                data_param.iCodVerbo = 0;
                                //data_param.strVerbo = $options.strVerbo;
                                data_param.Objetivo = '';
                                data_param.Funcion = '';
                                break;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                requestEnd: function (e) {
                    switch (e.type) {
                        case "create": case "update": case "destroy":
                            var grilla = $('#divGridFunciones').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);

                            break;
                    }
                },
                schema: {
                    //total: function (response) {
                    //    var TotalDeRegistros = 0;
                    //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    //    return TotalDeRegistros;
                    //},
                    model: {
                        id: "iCodPerfil",
                        fields: {
                            iCodPerfil: { editable: false, nullable: true },
                            iSecuencia: { validation: { required: true } },                        
                            //Verbo: {
                            //    validation: {
                            //        required: true,
                            //        verbovalidation: function (input) {
                            //            if (input.is("[name='Verbo']") && input.val() == "") {
                            //                input.attr("data-verbovalidation-msg", "Seleccionar Verbo es requerido");
                            //                return false;
                            //            }

                            //            return true;
                            //        }
                            //    }, defaultValue: { iCodVerbo: 0, strDescripcion: "strDescripcion" }
                            //},
                            //Objetivo: {
                            //    validation: {
                            //        required: true, maxlength: "95",  
                            //        objetivovalidation: function (input) {
                            //            if (input.is("[name='Objetivo']") && input.val() == "") {
                            //                input.attr("data-objetivovalidation-msg", "El objetivo es requerido");
                            //                return false;
                            //            }

                            //            return true;
                            //        }
                            //    }
                            //},
                            Funcion: {
                                validation: {
                                    required: true, maxlength: "495",
                                    funcionvalidation: function (input) {
                                        debugger;
                                        if (input.is("[name='Funcion']") && input.val() == "") {
                                        //if (input.val() == "") {
                                            input.attr("data-funcionvalidation-msg", "La función es requerida");
                                            return false;
                                        }

                                        return true;
                                    }
                                }
                            },
                        }
                    }
                },
            });

            this.divGridFunciones = $("#divGridFunciones").kendoGrid({
                toolbar: ["create"],
                //excel: {
                //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
                //    filterable: false
                //},
                dataSource: this.$dataSourceFunciones,
                autoBind: true,

                scrollable: false,
                sortable: false,
                pageable: false,
                groupable: false,
                dataType: 'json',
                columns: [
                    //{
                    //    field: "Verbo",
                    //    title: "VERBO",
                    //    width: "200px",
                    //    editor: controlador.VerboDropDownEditor,
                    //    template: "#=Verbo.strDescripcion#"
                    //},
                    //{
                    //    field: "Objetivo",
                    //    title: "OBJETO",
                    //    attributes: { style: "text-align:left;" },
                    //    width: "300px"
                    //},
                    {
                        field: "Funcion",
                        title: "RESULTADO",
                        width: "500px"
                        //editor: controlador.EstadoDropDownEditor,
                        //template: "#=Estado.Nombre#"
                    },
                    { title: "ACCIONES", command: ["edit", "destroy"], width: "150px" },
                ],
                editable: "inline"
            }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }
    
    this.PerfilPuestoJS.prototype.CargarPerfilCoordInterna = function (id) {
        debugger;
        
        if (id > 0) {
        this.$dataSourceCoordInterna = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCoordinacionInterna',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetCoordinacionesInt_Ext",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;                            
                            data_param.Coordinacion = $options.Coordinacion;
                            data_param.iTipoCoordinacion = 1;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;                            
                            data_param.Coordinacion = $options.Coordinacion;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;                            
                            data_param.Coordinacion = '';
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCoordInternas').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        Coordinacion: {
                            validation:
                                {
                                required: true, maxlength: "490", 
                                    coordinacionvalidation: function (input) {
                                        if (input.is("[name='Coordinacion']") && input.val() == "") {
                                            input.attr("data-coordinacionvalidation-msg", "La Descripcion es requerida");
                                            return false;
                                        }

                                        return true;
                                    }
                                }
                        },
                    }
                }
            },
        });

        this.divGridCoordInternas = $("#divGridCoordInternas").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceCoordInterna,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Coordinacion",
                    title: "COORDINACIÓN INTERNA",
                    width: "450px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.CargarPerfilCoordExterna = function (id) {
        debugger;
        if (id > 0) {
            this.$dataSourceCoordExterna = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCoordinacionExterna',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    update: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetCoordinacionesInt_Ext",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    destroy: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetCoordinacionesInt_Ext",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    create: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetCoordinacionesInt_Ext",
                        type: 'POST',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        switch ($operation) {
                            case "read":
                                data_param.iCodPerfil = id;
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
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                                data_param.iSecuencia = 0;
                                data_param.Coordinacion = $options.Coordinacion;
                                data_param.iTipoCoordinacion = 2;
                                //}
                                break;
                            case "update":
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                                data_param.iSecuencia = $options.iSecuencia;
                                data_param.Coordinacion = $options.Coordinacion;
                                break;
                            case "destroy":
                                data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                                data_param.iSecuencia = $options.iSecuencia;
                                data_param.Coordinacion = '';
                                break;
                        }

                        return $.toDictionary(data_param);
                    }
                },
                requestEnd: function (e) {
                    switch (e.type) {
                        case "create": case "update": case "destroy":
                            var grilla = $('#divGridCoordExternas').data("kendoGrid");
                            grilla.dataSource._sort = undefined;
                            grilla.dataSource.page(1);

                            break;
                    }
                },
                schema: {
                    //total: function (response) {
                    //    var TotalDeRegistros = 0;
                    //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    //    return TotalDeRegistros;
                    //},
                    model: {
                        id: "iCodPerfil",
                        fields: {
                            iCodPerfil: { editable: false, nullable: true },
                            iSecuencia: { validation: { required: true } },                        
                            Coordinacion: {
                                validation:
                                    {
                                    required: true, maxlength: "490" ,
                                        coordinacionvalidation: function (input) {
                                            if (input.is("[name='Coordinacion']") && input.val() == "") {
                                                input.attr("data-coordinacionvalidation-msg", "La Descripcion es requerida");
                                                return false;
                                            }

                                            return true;
                                        }
                                    }
                            },
                        }
                    }
                },
            });

            this.divGridCoordExternas = $("#divGridCoordExternas").kendoGrid({
                toolbar: ["create"],
                //excel: {
                //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
                //    filterable: false
                //},
                dataSource: this.$dataSourceCoordExterna,
                autoBind: true,

                scrollable: false,
                sortable: false,
                pageable: false,
                groupable: false,
                dataType: 'json',
                columns: [
                    {
                        field: "Coordinacion",
                        title: "COORDINACIÓN EXTERNA",
                        width: "450px"
                        //editor: controlador.EstadoDropDownEditor,
                        //template: "#=Estado.Nombre#"
                    },
                    { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
                ],
                editable: "inline"
            }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosTecnicos = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceConocimientoTecnico = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosTecnicos',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.Conocimientos = $options.Conocimientos;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.Conocimientos = $options.Conocimientos;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;                            
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridConocTec').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        //strConocimientos: { validation: { required: true } },
                        Conocimientos: {
                            validation:
                                {
                                    required: true,
                                    conocimientosvalidation: function (input) {
                                        if (input.is("[name='Conocimientos']") && input.val() == "") {
                                            input.attr("data-conocimientosvalidation-msg", "La descripcion del conocimiento es requerida");
                                            return false;
                                        }

                                        return true;
                                    }
                                }
                        },
                    }
                }
            },
        });

        this.divGridConocTec = $("#divGridConocTec").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoTecnico,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "Conocimientos",
                    title: "CONOCIMIENTOS TÉCNICOS",
                    width: "800px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosCurProg = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceConocimientoCurProg = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosCursosProgramas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;                            
                            data_param.Conocimientos = $options.Conocimientos;
                            data_param.iTipoNivelMateria = 3; //$options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;
                            data_param.Conocimientos = $options.Conocimientos;
                            data_param.iTipoNivelMateria = 3; //$options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateria = $options.PerfilTipoMateria.iCodTipoMateria;                            
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCursoProg').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        PerfilTipoMateria: {
                            validation: {
                                required: true,
                                tipomateriavalidation: function (input) {
                                    if (input.is("[name='PerfilTipoMateria']") && input.val() == "") {
                                        input.attr("data-tipomateriavalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        //strConocimientos: { validation: { required: true } },
                        Conocimientos: {
                            validation:
                                {
                                    required: true,
                                    conocimientosvalidation: function (input) {
                                        if (input.is("[name='Conocimientos']") && input.val() == "") {
                                            input.attr("data-conocimientosvalidation-msg", "La descripcion del conocimiento es requerida");
                                            return false;
                                        }

                                        return true;
                                    }
                                }
                        },
                        PerfilNivelMateria: {
                            validation: {
                                required: true,
                                //nivelmateriavalidation: function (input) {
                                //    if (input.is("[name='PerfilNivelMateria']") && input.val() == "") {
                                //        input.attr("data-nivelmateriavalidation-msg", "Seleccionar Nivel de Materia es requerido");
                                //        return false;
                                //    }

                                //    return true;
                                //}
                            }, defaultValue: { iCodTipoNivelMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        bConDocumento: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridCursoProg = $("#divGridCursoProg").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoCurProg,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "PerfilTipoMateria",
                    title: "TIPO MATERIA",
                    width: "200px",
                    editor: controlador.TipoMateriaDropDownEditor,
                    template: "#=PerfilTipoMateria.strDescripcion#"
                },
                {
                    field: "Conocimientos",
                    title: "CONOCIMIENTOS TÉCNICOS",
                    width: "600px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                //{
                //    field: "PerfilNivelMateria",
                //    title: "NIVEL",
                //    width: "200px",
                //    editor: controlador.NivelMateriaDropDownEditor,
                //    template: "#=PerfilNivelMateria.strDescripcion#"
                //},                
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.CargarPerfilConocimientosOfficeIdiomas = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceConocimientoOfficeIdiomas = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetConocimientosOfficeIdiomas',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetConocimientos",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;
                            data_param.iCodTipoMateriaOtros = $options.PerfilTipoMateriaOtros.iCodTipoMateriaOtros;
                            data_param.iCodTipoSubMateriaOtros = $options.PerfilTipoSubMateriaOtros.iCodTipoSubMateriaOtros;
                            //data_param.strConocimientos = $options.strConocimientos;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            data_param.iCodTipoMateriaOtros = $options.PerfilTipoMateriaOtros.iCodTipoMateriaOtros;
                            //data_param.strConocimientos = $options.strConocimientos;
                            data_param.iCodTipoSubMateriaOtros = $options.PerfilTipoSubMateriaOtros.iCodTipoSubMateriaOtros;
                            data_param.iTipoNivelMateria = $options.PerfilNivelMateria.iCodTipoNivelMateria;
                            data_param.bConDocumento = true;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = $options.iSecuencia;
                            //data_param.iCodTipoMateria = $options.MaePerfilTipoMateria.iCodTipoMateria;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridOfimatica_Idiomas').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },
                        PerfilTipoMateriaOtros: {
                            validation: {
                                required: true,
                                tipomateriaotrosvalidation: function (input) {
                                    if (input.is("[name='PerfilTipoMateriaOtros']") && input.val() == "") {
                                        input.attr("data-tipomateriaotrosvalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoMateriaOtros: 0, strDescripcion: "strDescripcion" }
                        },
                        PerfilTipoSubMateriaOtros: {
                            validation: {
                                required: true,
                                tiposubmateriaotrosvalidation: function (input) {
                                    if (input.is("[name='PerfilTipoSubMateriaOtros']") && input.val() == "") {
                                        input.attr("data-tiposubmateriaotrosvalidation-msg", "Seleccionar Tipo Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoSubMateriaOtros: 0, strDescripcion: "strDescripcion" }
                        },
                        //strConocimientos: { validation: { required: true } },
                        PerfilNivelMateria: {
                            validation: {
                                required: true,
                                nivelmateriavalidation: function (input) {
                                    if (input.is("[name='PerfilNivelMateria']") && input.val() == "") {
                                        input.attr("data-nivelmateriavalidation-msg", "Seleccionar Nivel de Materia es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodTipoNivelMateria: 0, strDescripcion: "strDescripcion" }
                        },
                        bConDocumento: { validation: { required: true } },
                    }
                }
            },
        });

        this.divGridOfimatica_Idiomas = $("#divGridOfimatica_Idiomas").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceConocimientoOfficeIdiomas,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "PerfilTipoMateriaOtros",
                    title: "TIPO MATERIA",
                    width: "200px",
                    editor: controlador.TipoMateriaOtrosDropDownEditor,
                    template: "#=PerfilTipoMateriaOtros.strDescripcion#"
                },
                //{
                //    field: "strConocimientos",
                //    title: "Conocimientos Tecnicos",
                //    width: "500px"
                //    //editor: controlador.EstadoDropDownEditor,
                //    //template: "#=Estado.Nombre#"
                //},
                {
                    field: "PerfilTipoSubMateriaOtros",
                    title: "TIPO SUB MATERIA",
                    width: "200px",
                    editor: function (container, options) {
                        $('<input required id="' + options.field + '" name="' + options.field + '"/>')
                            .appendTo(container)
                            .kendoDropDownList({
                                autoBind: true,
                                dataTextField: "strDescripcion",
                                dataValueField: "iCodTipoMateriaOtros",
                                optionLabel: "-- Seleccione --",
                                cascadeFrom: "PerfilTipoMateriaOtros",
                                dataSource: {
                                    serverFiltering: true,
                                    transport: {
                                        read: {
                                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoSubMateriaOtros",
                                            type: "GET",
                                            dataType: "json",
                                            cache: false
                                        },
                                        parameterMap: function ($options, $operation) {
                                            var data_param = {};
                                            data_param.Grilla = {};
                                            data_param.Grilla.RegistrosPorPagina = 0;
                                            data_param.Grilla.OrdenarPor = "strDescripcion";
                                            data_param.Grilla.OrdenarDeForma = "ASC";

                                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                                data_param.strDescripcion = $options.filter.filters[0].value;
                                            
                                            data_param.iCodTipoMateriaOtros = $('#PerfilTipoMateriaOtros').data("kendoDropDownList").value();;
                                            return $.toDictionary(data_param);
                                        }
                                    },
                                    requestEnd: function (e) {

                                    }
                                }
                            });
                    },
                    template: "#=PerfilTipoSubMateriaOtros.strDescripcion#"
                },
                {
                    field: "PerfilNivelMateria",
                    title: "NIVEL",
                    width: "200px",
                    editor: controlador.NivelMateriaDropDownEditor,
                    template: "#=PerfilNivelMateria.strDescripcion#"
                },
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }


    this.PerfilPuestoJS.prototype.CargarPerfilHabilidades = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceHabilidades = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetHabilidades',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();                            
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            data_param.iCodCualidadAnt = $options.iCodCualidadAnt;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridHabilidades').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                //total: function (response) {
                //    var TotalDeRegistros = 0;
                //    if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                //    return TotalDeRegistros;
                //},
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },                        
                        Cualidad: {
                            validation: {
                                required: true,
                                cualidadvalidation: function (input) {
                                    if (input.is("[name='Cualidad']") && input.val() == "") {
                                        input.attr("data-cualidadvalidation-msg", "Seleccionar Habilidad es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodCualidad: 0, strNombre: "strNombre" }
                        },                        
                    }
                }
            },
        });

        this.divGridHabilidades = $("#divGridHabilidades").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceHabilidades,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "iCodCualidadAnt",
                    title: "",
                    width: "0px",
                    hidden: true
                },
                {
                    field: "Cualidad",
                    title: "HABILIDADES",
                    width: "450px",
                    editor: controlador.CualidadHabilidadDropDownEditor,
                    template: "#=Cualidad.strNombre#"
                },
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }
    
    this.PerfilPuestoJS.prototype.CargarPerfilCompetencias = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceCompetencias = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetCompetencias',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetHabilidades_Competencias",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            data_param.iCodCualidadAnt = $options.iCodCualidadAnt;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iCodCualidad = $options.Cualidad.iCodCualidad;
                            data_param.iCodTipoCualidad = 0;
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridCompetencias').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        Cualidad: {
                            validation: {
                                required: true,
                                cualidadvalidation: function (input) {
                                    if (input.is("[name='Cualidad']") && input.val() == "") {
                                        input.attr("data-cualidadvalidation-msg", "Seleccionar cualidad es requerido");
                                        return false;
                                    }

                                    return true;
                                }
                            }, defaultValue: { iCodCualidad: 0, strNombre: "strNombre" }
                        }
                    }
                }
            },
        });

        this.divGridCompetencias = $("#divGridCompetencias").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceCompetencias,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [
                {
                    field: "iCodCualidadAnt",
                    title: "",
                    width: "0px",
                    hidden: true
                },
                {
                    field: "Cualidad",
                    title: "COMPETENCIAS",
                    width: "450px",
                    editor: controlador.CualidadCompetenciaDropDownEditor,
                    template: "#=Cualidad.strNombre#"
                },
                //{
                //    field: "strDescripcion",
                //    title: "Funciones",
                //    width: "500px"
                //    //editor: controlador.EstadoDropDownEditor,
                //    //template: "#=Estado.Nombre#"
                //},
                { title: "ACCIONES", command: ["edit", "destroy"], width: "200px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.CargarPerfilRequisitosAdicionales = function (id) {
        debugger;
        if (id > 0) {
        this.$dataSourceRequisitosAdicionales = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetRequisitosAdicionales',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                update: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/ActualizarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                destroy: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/EliminarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                create: {
                    url: controladorApp.obtenerRutaBase() + "Perfiles/InsertarDetRequisitosAdicionales",
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    switch ($operation) {
                        case "read":
                            data_param.iCodPerfil = id;
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
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                            data_param.iSecuencia = 0;                            
                            data_param.Requisito = $options.Requisito;
                            //}
                            break;
                        case "update":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;                            
                            data_param.Requisito = $options.Requisito;
                            break;
                        case "destroy":
                            data_param.iCodPerfil = $("#hdIdPerfilPuesto").val()
                            data_param.iSecuencia = $options.iSecuencia;                           
                            data_param.Requisito = '';
                            break;
                    }

                    return $.toDictionary(data_param);
                }
            },
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridRequisitosAdicionales').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            schema: {
                model: {
                    id: "iCodPerfil",
                    fields: {
                        iCodPerfil: { editable: false, nullable: true },
                        iSecuencia: { validation: { required: true } },                        
                        //vDescripcion: { validation: { required: true } },
                        Requisito: {
                            validation:
                                {
                                    required: true,
                                    requisitovalidation: function (input) {
                                        if (input.is("[name='Requisito']") && input.val() == "") {
                                            input.attr("data-requisitovalidation-msg", "La descripcion del requisito adicional es requerido");
                                            return false;
                                        }

                                        return true;
                                    }
                                }
                        },
                    }
                }
            },
        });

        this.divGridRequisitosAdicionales = $("#divGridRequisitosAdicionales").kendoGrid({
            toolbar: ["create"],
            //excel: {
            //    fileName: "Listado de cuentas bancarias del trabajador.xlsx",
            //    filterable: false
            //},
            dataSource: this.$dataSourceRequisitosAdicionales,
            autoBind: true,

            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
            dataType: 'json',
            columns: [                
                {
                    field: "Requisito",
                    title: "REQUISITOS ADICIONALES",
                    width: "1000px"
                    //editor: controlador.EstadoDropDownEditor,
                    //template: "#=Estado.Nombre#"
                },
                { title: "ACCIONES", command: ["edit", "destroy"], width: "250px" },
            ],
            editable: "inline"
        }).data();
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.VerboDropDownEditor = function (container, options) {        
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strDescripcion",
                dataValueField: "iCodVerbo",
                optionLabel: "-- Seleccione --",
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarVerbos",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strDescripcion";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };

    this.PerfilPuestoJS.prototype.NivelMateriaDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strDescripcion",
                dataValueField: "iCodTipoNivelMateria",
                optionLabel: "-- Seleccione --",
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelMateria",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strDescripcion";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };
       
    this.PerfilPuestoJS.prototype.TipoMateriaDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strDescripcion",
                dataValueField: "iCodTipoMateria",
                optionLabel: "-- Seleccione --",
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoMateria",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strDescripcion";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };

    this.PerfilPuestoJS.prototype.TipoMateriaOtrosDropDownEditor = function (container, options) {
        
        $('<input required id="' + options.field + '" name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strDescripcion",
                dataValueField: "iCodTipoMateriaOtros",
                optionLabel: "-- Seleccione --",
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarTipoMateriaOtros",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strDescripcion";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };

    this.PerfilPuestoJS.prototype.CualidadHabilidadDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strNombre",
                dataValueField: "iCodCualidad",
                optionLabel: "-- Seleccione --",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCualidadHabilidades",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strNombre";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };
    
    this.PerfilPuestoJS.prototype.CualidadCompetenciaDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "strNombre",
                dataValueField: "iCodCualidad",                
                optionLabel: "-- Seleccione --",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCualidadCompetencias",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.Grilla = {};
                            data_param.Grilla.RegistrosPorPagina = 0;
                            data_param.Grilla.OrdenarPor = "strNombre";
                            data_param.Grilla.OrdenarDeForma = "ASC";

                            return $.toDictionary(data_param);
                        }
                    },
                    requestEnd: function (e) {

                    }
                }
            });
    };

    this.PerfilPuestoJS.prototype.abrirModalRegistroNivelBasico = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPerfil = _item[0];
            items.iSecuencia = _item[1];            
            items.bCompleto = _item[2];
            items.iCodGrado = _item[3];
            
        }
        else 
            items.iCodPerfil = item;
        
        if ($("#hdIdPerfilPuesto").val() != "") {
        //debugger;
        //console.log(items);

        var modal = $('#ModalAgregarNivelBasico').data('kendoWindow');

        //LimpiarModalRegistroPersona();

        if (items.iCodPerfil == 0) {
            modal.title("Agregar Nivel Básico");

            $("#ddlNivelEducativoBasico").data("kendoDropDownList").value('');
            $("#hdISecuenciaNivelEducativoBasico").val(0);
            $("#chkCompletoNivelEducBasico").prop("checked", false);
            
            modal.open();
        }
        else {
            modal.title("Editar Nivel Básico");

            $("#hdISecuenciaNivelEducativoBasico").val(items.iSecuencia);
            
            $("#ddlNivelEducativoBasico").data("kendoDropDownList").value(items.iCodGrado);
            
            debugger;
            if (items.bCompleto == "false") {
                $("#chkCompletoNivelEducBasico").prop("checked", false);
            }
            else {
                $("#chkCompletoNivelEducBasico").prop("checked", true);
            };           

            modal.open();
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAgregarNivelBasico = function () {
        debugger;
        var modal = $('#ModalAgregarNivelBasico').data('kendoWindow');
        modal.close();
    }
        
    this.PerfilPuestoJS.prototype.abrirModalRegistroNivelEducativo = function (item) {
        debugger;
        var items = new Object();
        if (item!=0) {
            var _item = item.split(',');
            
            items.iCodPerfil = _item[0];
            items.iSecuencia = _item[1];
            items.iCodNivel = _item[2];
            items.bCompleto = _item[3];
            items.iCodGrado = _item[4];
            items.bColegiatura = _item[5];
            items.bHabilitado = _item[6];
            items.strCodCarreraN1 = _item[7];
            items.strCodCarreraN2 = _item[8];
            items.strCodCarreraN3 = _item[9];
            items.strCodCarreraN4 = _item[10];
        }
        else {
            items.iCodPerfil = item;
        }
        
        if ($("#hdIdPerfilPuesto").val() != "") {
        //debugger;
        //console.log(items);

        var modal = $('#ModalAgregarNivelEducativo').data('kendoWindow');

        //LimpiarModalRegistroPersona();
        
        if (items.iCodPerfil == 0)
        {
            modal.title("Agregar Nivel Educativo");
            
            $("#ddlNivelEducativo").data("kendoDropDownList").value('');
            $("#hdISecuenciaNivelEducativo").val(0);
            $("#ddlNivelAlcanzado").data("kendoDropDownList").value('');
            //$("#ddlCarreraN1").data("kendoDropDownList").value('');
            //$("#ddlCarreraN2").data("kendoDropDownList").value('');
            //$("#ddlCarreraN3").data("kendoDropDownList").value('');
            $("#ddlCarreraN4").data("kendoDropDownList").value('');
            $("#chkCompletoNivelEduc").prop("checked", false);
            $("#chkColegiado").prop("checked", false);
            $("#chkHabilitado").prop("checked", false);

            modal.open();
        }
        else
        {            
            modal.title("Editar Nivel Educativo");
            debugger;
            $("#hdISecuenciaNivelEducativo").val(items.iSecuencia);
            $("#ddlNivelAlcanzado").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivel",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativo",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlNivelEducativo").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodGrado",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarGrados",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlCarreraN1").kendoDropDownList({
                autoBind: true,
                //cascadeFrom: "ddlNivelEducativo",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            //var iCodGrado = $('#ddlNivelEducativo').data("kendoDropDownList").value();
                            //var iCodTipoCarrera = 0;
                            ////data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();
                            //if (iCodGrado > 2) {
                            //    switch (iCodGrado) {
                            //        case 3:
                            //            {
                            //                iCodTipoCarrera = 1;
                            //                break;
                            //            }
                            //        case 4:
                            //            {
                            //                iCodTipoCarrera = 2;
                            //                break;
                            //            }
                            //        case 5:
                            //            {
                            //                iCodTipoCarrera = 4;
                            //                break;
                            //            }
                            //        default:

                            //    }                                
                            //}
                            debugger;
                            data_param.iCodTipoCarrera = 4;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlNivelEducativo").data("kendoDropDownList").value(items.iCodGrado);
            $("#ddlNivelAlcanzado").data("kendoDropDownList").value(items.iCodNivel);

            $("#ddlCarreraN1").data("kendoDropDownList").value(items.strCodCarreraN1);
            $("#ddlCarreraN2").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN1",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN1').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN2").data("kendoDropDownList").value(items.strCodCarreraN2);

            $("#ddlCarreraN3").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN2",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN2').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN3").data("kendoDropDownList").value(items.strCodCarreraN3);

            $("#ddlCarreraN4").kendoDropDownList({
                autoBind: true,
                //cascadeFrom: "ddlCarreraN3",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 5,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = ''; //$('#ddlCarreraN3').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN4").data("kendoDropDownList").value(items.strCodCarreraN4);

            debugger;
            if (items.bCompleto == "false") $("#chkCompletoNivelEduc").prop("checked", false);
            else $("#chkCompletoNivelEduc").prop("checked", true);

            if (items.bColegiatura == "false") $("#chkColegiado").prop("checked", false);
            else $("#chkColegiado").prop("checked", true);
            
            if (items.bHabilitado == "false") $("#chkHabilitado").prop("checked", false);
            else $("#chkHabilitado").prop("checked", true);
            
            modal.open();
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAgregarNivelEducativo = function () {
        debugger;
        var modal = $('#ModalAgregarNivelEducativo').data('kendoWindow');
        modal.close();
    }

    this.PerfilPuestoJS.prototype.abrirModalRegistroMaestria = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPerfil = _item[0];
            items.iSecuencia = _item[1];
            items.iCodNivel = _item[2];
            //items.bCompleto = _item[3];
            items.iCodGrado = _item[3];
            //items.bColegiatura = _item[5];
            //items.bHabilitado = _item[6];
            items.strCodCarreraN1 = _item[4];
            items.strCodCarreraN2 = _item[5];
            items.strCodCarreraN3 = _item[6];
            items.strCodCarreraN4 = _item[7];
        }
        else {
            items.iCodPerfil = item;
        }

        if ($("#hdIdPerfilPuesto").val() != "") {
        debugger;
        //console.log(items);

        var modal = $('#ModalAgregarMaestria').data('kendoWindow');

        //LimpiarModalRegistroPersona();

        if (items.iCodPerfil == 0) {
            modal.title("Agregar Maestría");

            $("#ddlNivelAlcanzadoMaestria").data("kendoDropDownList").value('');            
            $("#hdISecuenciaMaestria").val(0);
            //$("#ddlCarreraN1Maestria").data("kendoDropDownList").value('');
            //$("#ddlCarreraN2Maestria").data("kendoDropDownList").value('');
            //$("#ddlCarreraN3Maestria").data("kendoDropDownList").value('');
            $("#ddlCarreraN4Maestria").data("kendoDropDownList").value('');            

            modal.open();
        }
        else {
            modal.title("Editar Maestría");

            $("#hdISecuenciaMaestria").val(items.iSecuencia);
            $("#ddlNivelAlcanzadoMaestria").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivel",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoMaestria",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlNivelAlcanzadoMaestria").data("kendoDropDownList").value(items.iCodNivel);

            $("#ddlCarreraN1Maestria").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Mae",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            data_param.iCodTipoCarrera = 5;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN1Maestria").data("kendoDropDownList").value(items.strCodCarreraN1);
            $("#ddlCarreraN2Maestria").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN1Maestria",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Mae",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN1Maestria').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlCarreraN2Maestria").data("kendoDropDownList").value(items.strCodCarreraN2);
            $("#ddlCarreraN3Maestria").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN2Maestria",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Mae",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN2Maestria').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlCarreraN3Maestria").data("kendoDropDownList").value(items.strCodCarreraN3);
            $("#ddlCarreraN4Maestria").kendoDropDownList({
                autoBind: false,
                //cascadeFrom: "ddlCarreraN3Maestria",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 5,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Mae",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = ''; //$('#ddlCarreraN3Maestria').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN4Maestria").data("kendoDropDownList").value(items.strCodCarreraN4);

            debugger;
            

            modal.open();
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAgregarMaestria = function () {
        debugger;
        var modal = $('#ModalAgregarMaestria').data('kendoWindow');
        modal.close();
    }

    this.PerfilPuestoJS.prototype.abrirModalRegistroDoctorado = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPerfil = _item[0];
            items.iSecuencia = _item[1];
            items.iCodNivel = _item[2];
            //items.bCompleto = _item[3];
            items.iCodGrado = _item[3];
            //items.bColegiatura = _item[5];
            //items.bHabilitado = _item[6];
            items.strCodCarreraN1 = _item[4];
            items.strCodCarreraN2 = _item[5];
            items.strCodCarreraN3 = _item[6];
            items.strCodCarreraN4 = _item[7];
        }
        else {
            items.iCodPerfil = item;
        }

        if ($("#hdIdPerfilPuesto").val() != "") {
        debugger;
        //console.log(items);

        var modal = $('#ModalAgregarDoctorado').data('kendoWindow');

        //LimpiarModalRegistroPersona();

        if (items.iCodPerfil == 0) {
            modal.title("Agregar Doctorado");

            $("#ddlNivelAlcanzadoDoctorado").data("kendoDropDownList").value('');
            $("#hdISecuenciaDoctorado").val(0);
            //$("#ddlCarreraN1Doctorado").data("kendoDropDownList").value('');
            //$("#ddlCarreraN2Doctorado").data("kendoDropDownList").value('');
            //$("#ddlCarreraN3Doctorado").data("kendoDropDownList").value('');
            $("#ddlCarreraN4Doctorado").data("kendoDropDownList").value('');

            modal.open();
        }
        else {
            modal.title("Editar Doctorado");

            $("#hdISecuenciaDoctorado").val(items.iSecuencia);
            $("#ddlNivelAlcanzadoDoctorado").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "iCodNivel",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarNivelEducativoDoctorado",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlCarreraN1Doctorado").kendoDropDownList({
                autoBind: true,
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel1_Doc",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;
                            data_param.iCodTipoCarrera = 6;
                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlNivelAlcanzadoDoctorado").data("kendoDropDownList").value(items.iCodNivel);
            $("#ddlCarreraN1Doctorado").data("kendoDropDownList").value(items.strCodCarreraN1);

            $("#ddlCarreraN2Doctorado").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN1Doctorado",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel2_Doc",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN1Doctorado').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN2Doctorado").data("kendoDropDownList").value(items.strCodCarreraN2);
            $("#ddlCarreraN3Doctorado").kendoDropDownList({
                autoBind: true,
                cascadeFrom: "ddlCarreraN2Doctorado",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel3_Doc",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = $('#ddlCarreraN2Doctorado').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });

            $("#ddlCarreraN3Doctorado").data("kendoDropDownList").value(items.strCodCarreraN3);
            $("#ddlCarreraN4Doctorado").kendoDropDownList({
                autoBind: false,
                //cascadeFrom: "ddlCarreraN3Doctorado",
                optionLabel: "--Seleccione--",
                dataTextField: "strDescripcion",
                dataValueField: "strCodCarrera",
                filter: "contains",
                minLength: 3,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: {
                            url: controladorApp.obtenerRutaBase() + "Perfiles/ListarCarreraNivel4_Doc",
                            type: "GET",
                            dataType: "json",
                            cache: false
                        },
                        parameterMap: function ($options, $operation) {
                            var data_param = {};
                            if ($options.filter != undefined && $options.filter.filters.length > 0)
                                data_param.strDescripcion = $options.filter.filters[0].value;

                            data_param.strCodCarrera = ''; //$('#ddlCarreraN3Maestria').data("kendoDropDownList").value();

                            return $.toDictionary(data_param);
                        }
                    }
                }
            });
            $("#ddlCarreraN4Doctorado").data("kendoDropDownList").value(items.strCodCarreraN4);

            modal.open();
        }
        } else {
            controladorApp.notificarMensajeDeAlerta('No existe registro');
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAgregarDoctorado = function () {
        debugger;
        var modal = $('#ModalAgregarDoctorado').data('kendoWindow');
        modal.close();
    }
    //this.PerfilPuestoJS.prototype.cerrarModalRegistroPersona = function () {
    //    var modal = $('#divModalRegistroPersona').data('kendoWindow');
    //    modal.close();
    //}

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaNivelBasico = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaNivelBasico',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraBasico").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                //{
                //    field: "strCodCarrera",
                //    title: "codCarrera",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strGrado",
                    title: "NIVEL EDUCATIVO",
                    width: "300px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bCompleto",
                    title: "bCompleto",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vCompleto",
                    title: "COMPLETO",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "iCodNivel",
                //    title: "codNivel",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "strNivel",
                //    title: "Nivel Alcanzado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                
                
                
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.bCompleto, item.iCodGrado];
                        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalRegistroNivelBasico(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                        //controles += '<a href="perfiles/nuevo">';
                        controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaNivelEducativo = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaNivelEducativo',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }           
        });
        this.$grid = $("#divGridCarreraPreGrado").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strGrado",
                    title: "NIVEL EDUCATIVO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },                
                {
                    field: "bCompleto",
                    title: "bCompleto",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vCompleto",
                    title: "COMPLETO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bColegiatura",
                    title: "bColegiatura",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vColegiatura",
                    title: "COLEGIATURA",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "bHabilitado",
                    title: "bHabilitado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "vHabilitado",
                    title: "HABILITADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "CARRERA NIVEL 1",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "CARRERA NIVEL 2",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "CARRERA NIVEL 3",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "CARRERA PROFESIONAL",
                    width: "300px",
                    attributes: { style: "text-align:center;" }
                },                
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.bCompleto, item.iCodGrado, item.bColegiatura, item.bHabilitado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalRegistroNivelEducativo(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                        //controles += '<a href="perfiles/nuevo">';
                        controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaMaestria = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaMaestria',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraMaestria").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strGrado",
                //    title: "Nivel Educativo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bCompleto",
                //    title: "bCompleto",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vCompleto",
                //    title: "Completo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bColegiatura",
                //    title: "bColegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vColegiatura",
                //    title: "Colegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bHabilitado",
                //    title: "bHabilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vHabilitado",
                //    title: "Habilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "CARRERA NIVEL 133",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "CARRERA NIVEL 2",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "CARRERA NIVEL 3",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "ESPECIALIDAD",
                    width: "500px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.iCodGrado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalRegistroMaestria(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                        //controles += '<a href="perfiles/nuevo">';
                        controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.CargarDatosFormAcaDoctorado = function (id) {
        //e.preventDefault();
        //var data_param = new FormData();
        //data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
        //this.$dataSource = [];                
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilDetFormAcaDoctorado',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
                        data_param.iCodPerfil = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
        });
        this.$grid = $("#divGridCarreraProDoctorado").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "iSecuencia",
                    title: "Secuencia",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strCodCarrera",
                    title: "codCarrera",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "iCodGrado",
                    title: "codGrado",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strGrado",
                //    title: "Nivel Educativo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bCompleto",
                //    title: "bCompleto",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vCompleto",
                //    title: "Completo",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "iCodNivel",
                    title: "codNivel",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel",
                    title: "NIVEL ALCANZADO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                //{
                //    field: "bColegiatura",
                //    title: "bColegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vColegiatura",
                //    title: "Colegiatura",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                //{
                //    field: "bHabilitado",
                //    title: "bHabilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" },
                //    hidden: true
                //},
                //{
                //    field: "vHabilitado",
                //    title: "Habilitado",
                //    width: "30px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN1",
                    title: "codCarreraN1",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel1",
                //    title: "CARRERA NIVEL 1",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN2",
                    title: "CodCarreraN2",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel2",
                //    title: "CARRERA NIVEL 2",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN3",
                    title: "CodCarreraN3",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                //{
                //    field: "strNivel3",
                //    title: "CARRERA NIVEL 3",
                //    width: "200px",
                //    attributes: { style: "text-align:center;" }
                //},
                {
                    field: "strCodCarreraN4",
                    title: "CodCarreraN4",
                    width: "30px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    field: "strNivel4",
                    title: "ESPECIALIDAD",
                    width: "500px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodNivel, item.iCodGrado, item.strCodCarreraN1, item.strCodCarreraN2, item.strCodCarreraN3, item.strCodCarreraN4];
                        //var items = ["iCodPerfil:" + item.iCodPerfil, "iSecuencia:" + item.iSecuencia, "iCodNivel:" + item.iCodNivel, "bCompleto:" + item.bCompleto, "iCodGrado:" + item.iCodGrado, "bColegiatura:" + item.bColegiatura, "bHabilitado:" + item.bHabilitado, "strCodCarreraN1:" + item.strCodCarreraN1, "strCodCarreraN2:" + item.strCodCarreraN2, "strCodCarreraN3:" + item.strCodCarreraN3, "strCodCarreraN4:" + item.strCodCarreraN4];

                        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalRegistroDoctorado(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.iSecuencia, item.iCodGrado];
                        //controles += '<a href="perfiles/nuevo">';
                        controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarFormacionAcademica(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar información de la formación académica"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.EliminarFormacionAcademica = function (item) {
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPerfil = _item[0];
            items.iSecuencia = _item[1];
            items.iCodGrado = _item[2];
        }
        else {
            items.iCodPerfil = item;            
            items.iSecuencia = 0;
        }
        var data_param = new FormData();
        if (items.iCodPerfil>0) {
            data_param.append('iCodPerfil', items.iCodPerfil);
        }
        
        if (items.iSecuencia>0) 
            data_param.append('iSecuencia', items.iSecuencia);            
        
        controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de eliminar el registro?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/EliminarDetFormacionAcademica',
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
                                controladorApp.notificarMensajeSatisfactorio("Operación completada correctamente");

                                if (items.iCodGrado <=2) {
                                    controlador.CargarDatosFormAcaNivelBasico(items.iCodPerfil);
                                }
                                else if (items.iCodGrado <= 5) {
                                    controlador.CargarDatosFormAcaNivelEducativo(items.iCodPerfil);
                                }
                                else if (items.iCodGrado == 6)
                                {
                                    controlador.CargarDatosFormAcaMaestria(items.iCodPerfil);
                                }
                                else {
                                    controlador.CargarDatosFormAcaDoctorado(items.iCodPerfil);
                                }
                                
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
    };
    
    this.PerfilPuestoJS.prototype.EliminarPerfil = function (IdPerfil) {
        var data_param = new FormData();
        data_param.append('iCodPerfil', IdPerfil);
                
        controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de eliminar el perfil de puesto?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/PerfilEliminar',
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
                                controladorApp.notificarMensajeSatisfactorio("Perfil de puesto eliminado  correctamente");

                                controlador.CargarBandejaPrincipal(event);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
    };

    this.PerfilPuestoJS.prototype.Finalizar = function () {
        var id = $("#hdIdPerfilPuesto").val();
        var data_param = new FormData();

        if (frmRegistroAnexo2.validate()) {
            if (id > 0) {
                data_param.append('iCodPerfil', id);
                data_param.append('iCodOrgano', $("#ddlOrgano").data("kendoDropDownList").value());
                data_param.append('iCodUnidadOrganica', $("#ddlUUOO").data("kendoDropDownList").value());
                data_param.append('strPuestoEstructural', $("#txtPuestoEstructural").val());
                data_param.append('strNombrePuesto', $("#txtNombrePuesto").val());
                data_param.append('strDependenciaJerarquicaLineal', $("#txtDependenciaJerarquicaLineal").val());
                data_param.append('strDependenciaFuncional', $("#txtDependenciaFuncional").val());
                data_param.append('strPuestos_a_su_Cargo', $("#txtPuestoCargo").val());
                data_param.append('strMision', $("#txtMision").val());

                data_param.append('iAnioExpGeneral', $("#txtAnioExperienciaGeneral").val());
                data_param.append('iAnioExpEspecifica', $("#txtAnioExperienciaEspecifica").val());
                data_param.append('strDesExpEspecifica', $("#txtDesExperienciaEspecifica").val());
                data_param.append('iAnioExpSectorPublico', $("#txtAnioExperienciaSectorPublico").val());
                data_param.append('iCodNivelMinimo', $("#ddlNivelMinimoPuesto").data("kendoDropDownList").value());


                controladorApp.abrirMensajeDeConfirmacion(
                    '¿Está seguro de finalizar la elaboración del perfil del puesto?', 'SI', 'NO'
                    , function (arg) {
                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Perfiles/ActualizarPerfilCab',
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
                                    $.ajax({
                                        url: controladorApp.obtenerRutaBase() + 'Perfiles/PerfilFinalizar',
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
                                                controladorApp.notificarMensajeSatisfactorio("Operación completada correctamente");

                                                window.location.assign("Index");
                                            }
                                        },
                                        error: function (res) {
                                            //alert(res);
                                        }
                                    });

                                    //controladorApp.notificarMensajeSatisfactorio(mensajeExito);

                                    //// REFRESCAR INFORMACION DEL TRABAJADOR
                                    //$("#hdIdPerfilPuesto").val(res.responseText);
                                    ////controlador.Actualizar($("#hdIdPerfilPuesto").val());
                                    //window.location.assign("ActualizarPerfil?id=" + $("#hdIdPerfilPuesto").val());
                                }
                            },
                            error: function (res) {
                                //alert(res);
                            }
                        });
                        
                    }, data_param);

            } else {
                controladorApp.notificarMensajeDeAlerta('No existe registro');
            }
        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }


        
    };

    $('#ModalAgregarNivelBasico').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Agregar Nivel Básico',
        visible: false,
        position: { top: '25%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmPersonaValidador.hideMessages();

            //$("#hdIdEmpleado").val('0');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
            //$("#txtPersonaNumeroDeDocumento").val('');
            //$("#chkPersonaDniValidoEnReniec").prop("checked", false);
            //$("#txtPersonaNombres").val('');
            //$("#txtPersonaApellidoPaterno").val('');
            //$("#txtPersonaApellidoMaterno").val('');
            //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
            //$("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            //$("#txtPersonaFechaDeNacimiento").data("kendoDatePickerssms").value('');
            //$("#ddlPersonaIdUbigeoNacimiento").data("kendoDropDownList").value('');
            //$("#ddlPersonaIdUbigeoDomicilio").data("kendoDropDownList").value('');
            //$("#txtPersonaDireccionDomicilio").val('');
        }
    }).data("kendoWindow");

    $('#ModalAgregarNivelEducativo').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Agregar Nivel Educativo',
        visible: false,
        position: { top: '25%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmPersonaValidador.hideMessages();

            //$("#hdIdEmpleado").val('0');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
            //$("#txtPersonaNumeroDeDocumento").val('');
            //$("#chkPersonaDniValidoEnReniec").prop("checked", false);
            //$("#txtPersonaNombres").val('');
            //$("#txtPersonaApellidoPaterno").val('');
            //$("#txtPersonaApellidoMaterno").val('');
            //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
            //$("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            //$("#txtPersonaFechaDeNacimiento").data("kendoDatePickerssms").value('');
            //$("#ddlPersonaIdUbigeoNacimiento").data("kendoDropDownList").value('');
            //$("#ddlPersonaIdUbigeoDomicilio").data("kendoDropDownList").value('');
            //$("#txtPersonaDireccionDomicilio").val('');
        }
    }).data("kendoWindow");

    $('#ModalAgregarMaestria').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Agregar Maestría',
        visible: false,
        position: { top: '25%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmPersonaValidador.hideMessages();

            //$("#hdIdEmpleado").val('0');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
            //$("#txtPersonaNumeroDeDocumento").val('');
            //$("#chkPersonaDniValidoEnReniec").prop("checked", false);
            //$("#txtPersonaNombres").val('');
            //$("#txtPersonaApellidoPaterno").val('');
            //$("#txtPersonaApellidoMaterno").val('');
            //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
            //$("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            //$("#txtPersonaFechaDeNacimiento").data("kendoDatePickerssms").value('');
            //$("#ddlPersonaIdUbigeoNacimiento").data("kendoDropDownList").value('');
            //$("#ddlPersonaIdUbigeoDomicilio").data("kendoDropDownList").value('');
            //$("#txtPersonaDireccionDomicilio").val('');
        }
    }).data("kendoWindow");

    $('#ModalAgregarDoctorado').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Agregar Doctorado',
        visible: false,
        position: { top: '45%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmPersonaValidador.hideMessages();

            //$("#hdIdEmpleado").val('0');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
            //$("#txtPersonaNumeroDeDocumento").val('');
            //$("#chkPersonaDniValidoEnReniec").prop("checked", false);
            //$("#txtPersonaNombres").val('');
            //$("#txtPersonaApellidoPaterno").val('');
            //$("#txtPersonaApellidoMaterno").val('');
            //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
            //$("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            //$("#txtPersonaFechaDeNacimiento").data("kendoDatePickerssms").value('');
            //$("#ddlPersonaIdUbigeoNacimiento").data("kendoDropDownList").value('');
            //$("#ddlPersonaIdUbigeoDomicilio").data("kendoDropDownList").value('');
            //$("#txtPersonaDireccionDomicilio").val('');
        }
    }).data("kendoWindow");


    ////////////////////////////BANDEJA ADMINISTRADOR//////////////////////////////////

    this.PerfilPuestoJS.prototype.inicializarBandeja = function () {
        debugger;
        this.CargarDatosBandeja($("#hdIdCodTrabajadorBand").val());
        
        $("#ddlOrgano_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            //filter: "contains",
            //minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(CodOrganoBand);

        $("#ddlUUOO_busqueda").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlOrgano_busqueda",
            optionLabel: "--Todos--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;

                        data_param.iCodOrgano = $('#ddlOrgano_busqueda').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                        ///this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
                    }
                }
            }
        });

        $("#ddlEstado_busqueda").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarEstadoPerfil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        $("#fileFormato").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }//,
            //cancel: onCancel,
            //complete: onComplete,
            //error: onError,
            //progress: onProgress,
            //remove: onRemove,
            //select: onSelect,
            //success: onSuccess,
            //upload: onUpload
        });

        $("#ddlComiteDependencia1").kendoDropDownList({
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
        $("#ddlComiteMiembro1T").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlComiteDependencia1",
            optionLabel: "--Seleccione--",
            dataTextField: "NombreCompleto",
            dataValueField: "IdEmpleado",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleados",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.Nombre = $options.filter.filters[0].value;

                        data_param.IdDependencia = $("#ddlComiteDependencia1").data("kendoDropDownList").value();
                        data_param.Estado = 1
                        data_param.IdEmpleado = 0
                        data_param.Grilla = {};
                        data_param.Grilla.RegistrosPorPagina = 0;
                        data_param.Grilla.OrdenarPor = "Nombre";
                        data_param.Grilla.OrdenarDeForma = "ASC";

                        if (data_param.Nombre == data_param.IdDependencia) data_param.Nombre = '';
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        $('#divModalNuevaSolicitud').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Asignar el Registro de Perfil de Puesto',
            visible: false,
            position: { top: '20%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        frmNuevaSolicitud = $("#frmNuevaSolicitud").kendoValidator().data("kendoValidator");


        $("#ddlOrgano").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            //filter: "contains",
            //minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });

        //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(CodOrganoBand);

        $("#ddlUUOO").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlOrgano",
            optionLabel: "--Todos--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;

                        data_param.iCodOrgano = $('#ddlOrgano').data("kendoDropDownList").value();

                        return $.toDictionary(data_param);
                        ///this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
                    }
                }
            }
        });


        ////$("#ddlUUOO_busqueda").data('kendoDropDownList').value(CodDependenciaBand);        
        ////controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //$("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });
        ////$("#txtFechaInicio").data("kendoDatePicker").value(todayDate);
        //$("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        ////$("#txtFechaFin").data("kendoDatePicker").value(todayDate);

        this.CargarBandejaPrincipal(event);
    };


    this.PerfilPuestoJS.prototype.CargarDatosBandeja = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#hdIdCodOrganoBand").val(res.iCodOrgano);
                
                CodOrganoBand = res.iCodOrgano;
                CodDependenciaBand = res.iCodDependencia;
                $("#hdIdCodDependenciaBand").val(res.iCodDependencia);
                //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(res.iCodOrgano);
                //$("#ddlUUOO_busqueda").data('kendoDropDownList').value(res.iCodDependencia);

            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    //this.PerfilPuestoJS.prototype.CargarBandeja = function () {
    //    //controladorApp.notificarMensajeDeAlerta(_id);
    //    var _id = $('#ddlUUOO_busqueda').data("kendoDropDownList").value()
    //    //alert(_id);
    //    var data_param = new FormData();
    //    //data_param.append('id', _id);
    //    data_param.append('id', _id);
        
    //    $.ajax({
    //        url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
    //        type: 'POST',
    //        dataType: 'json',
    //        contentType: false,
    //        processData: false,
    //        data: data_param,
    //        success: function (res) {
    //            debugger;
    //            controladorApp.notificarMensajeDeAlerta(res);
    //        },
    //        error: function (res) {
    //            debugger;
    //        }
    //    });
    //    //controladorApp.notificarMensajeDeAlerta(res);
    //}

    this.PerfilPuestoJS.prototype.CargarBandejaPrincipal = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    //url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoUserRRHH',                    
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busqueda').data("kendoDropDownList").value();                       
                        data_param.strOrgano = $('#ddlOrgano_busqueda').data("kendoDropDownList").value();
                        data_param.strUO = $('#ddlUUOO_busqueda').data("kendoDropDownList").value();
                        data_param.strEstado = $('#ddlEstado_busqueda').data("kendoDropDownList").value();
                        data_param.fechaIni = ''; //$("#txtFechaInicio").data("kendoDatePicker").value();
                        data_param.fechaFin = ''; //$("#txtFechaFin").data("kendoDatePicker").value();
                        data_param.strNombre = $("#txtNombrePuesto_busqueda").val();
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodPerfil"
                }
            },
            group: {
                field: "strOrgano", aggregates: [
                   { field: "strOrgano", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "strOrgano", aggregate: "count" },
                    { field: "strOrgano", aggregate: "count" }
            ]
        });
        debugger;
        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Listado de Perfiles.xlsx",
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
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                { 
                    field: "strOrgano",
                    title: "ÓRGANO",
                    width: "200px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "strUnidadOrganica",
                    title: "UNIDAD ORGÁNICA",
                    width: "300px"
                },
                {
                    field: "strNombrePuesto",
                    title: "NOMBRE DEL PUESTO",
                    width: "300px"
                },
                {
                    field: "datFechaReg",
                    title: "FECHA DE REGISTRO",
                    width: "100px",
                    //format: "{0: yyyy-MM-dd HH:mm:ss}",
                    template: "#= kendo.toString(kendo.parseDate(datFechaReg, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strEstadoCompletado",
                    title: "ESTADO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        return (item.strEstadoCompletado == 'PENDIENTE' ? item.strEstadoCompletado 
                            : (item.strEstadoCompletado == 'REVISADO' ? "<span style='background-color: RGB(255,255,204); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,235,59);'> " + item.strEstadoCompletado + "</span>"
                            : (item.strEstadoCompletado == 'FIRMADO' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i>  " + item.strEstadoCompletado + "</span>"
                            : '')));
                    },
                },
                {
                    title: " ",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        var controles = "";
                        if (item.bEstadoCompletado == true) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.VerPerfil(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver información del perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        else {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.Actualizar(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        
                        return controles;
                    }
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')">';
                        //controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar PDF" title="Generar PDF perfil de puesto"></span>';
                        //controles += '</button>';
                        
                        if (item.IdTieneArchivo == 1) {
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Perfiles/DescargarArchivo/?id=' + item.iCodPerfil + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-download-alt" title="Descargar perfil de puesto"></span></a>';
                        }
                        else {
                            if (item.strEstadoCompletado == 'REVISADO') {
                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Actualizar perfil de puesto"></span>';
                                controles += '</button>';
                            }
                        }

                        return controles;
                    },
                    width: '30px'
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.strEstadoCompletado == 'PENDIENTE') {
                            controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarPerfil(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        
                        return controles;
                    },
                    width: '30px'
                },
            ]
        }).data();
        debugger;
    };
    this.PerfilPuestoJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };
    this.PerfilPuestoJS.prototype.Actualizar = function (id) {        
        //window.location.assign("ActualizarPerfil?id="+id);
        window.location.href = controladorApp.obtenerRutaBase() + "Perfiles/ActualizarPerfil?id=" + id;
    };

    this.PerfilPuestoJS.prototype.VerPerfil = function (id) {
        //window.location.assign("VerPerfil?id=" + id);
        window.location.href = controladorApp.obtenerRutaBase() + "Perfiles/VerPerfil?id=" + id;
    };

    this.PerfilPuestoJS.prototype.abrirModalFormato = function (IdPerfil) {
        $("#fileFormato").data("kendoUpload").clearAllFiles();
        $('#hdIdPerfilPuesto').val(IdPerfil);

        var modal = $('#divModalFormato').data('kendoWindow');
        //modal.title("Enviar Contrato de Trabajo");

        modal.open();
    }

    this.PerfilPuestoJS.prototype.cerrarModalNuevaSolicitud = function () {
        var modal = $('#divModalNuevaSolicitud').data('kendoWindow');
        modal.close();
    }
    this.PerfilPuestoJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    }
    this.PerfilPuestoJS.prototype.agregarPerfilArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('iCodPerfil', $('#hdIdPerfilPuesto').val());

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el archivo de perfil de puesto firmado (Formato PDF)');
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

        controladorApp.abrirMensajeDeConfirmacion('¿Desea actualizar el perfil de puesto?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/RegistrarPerfilArchivo',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            debugger;
                            if (res.success == 'False') {
                                $("#console").append(res.responseText);
                                controladorApp.notificarMensajeDeAlerta("La actualización del perfil de puesto no se pudo realizar");
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio("El perfil de puesto se actualizó de forma correcta");
                                modal.close();
                                controlador.CargarBandejaPrincipal(event);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
    }

    this.PerfilPuestoJS.prototype.abrirModalAprobarPerfilAdm = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalAprobarPerfilAdm').data('kendoWindow');
        $("#txtObservacion").val('');
        //LimpiarModalRegistroPersona();

        if (id != 0) {

            $("#hdIdPerfilPuesto").val(id);
            modal.title("Aprobar Perfil");

            modal.open();
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAprobarPerfilAdm = function () {
        debugger;
        var modal = $('#ModalAprobarPerfilAdm').data('kendoWindow');
        modal.close();
    }

    $('#divModalFormato').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Adjuntar Formato del Perfil de Puesto',
        visible: false,
        position: { top: '20%', left: "20%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {

        }
    }).data("kendoWindow");

    $('#ModalAprobarPerfilAdm').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Aprobar Perfil',
        visible: false,
        position: { top: '45%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PerfilPuestoJS.prototype.AprobarPerfilAdm = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDerivarUserRRHH';

        if (true) {
            var modal = $('#ModalAprobarPerfilAdm').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBand").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBand").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }

            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Aprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.inicializarBandeja();
                                controlador.CargarBandejaPrincipal(event);
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }

    this.PerfilPuestoJS.prototype.DesaprobarPerfilAdm = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDesaprobar';

        if (true) {
            var modal = $('#ModalAprobarPerfilAdm').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuesto").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBand").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBand").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }

            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Desaprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.inicializarBandeja();
                                controlador.CargarBandejaPrincipal(event);
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }




    ////////////////////////////BANDEJA JEFE RRHH//////////////////////////////////

    this.PerfilPuestoJS.prototype.CargarBandejaPrincipalJefeRRHH = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    //url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoJefeRRHH',                    
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaJefeRRHH').data("kendoDropDownList").value();;
                        data_param.id = "";
                        data_param.fechaIni = $("#txtFechaInicioJefeRRHH").data("kendoDatePicker").value();
                        data_param.fechaFin = $("#txtFechaFinJefeRRHH").data("kendoDatePicker").value();
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodPerfil"
                }
            },
            group: {
                field: "strEstadoCompletado", aggregates: [
                   { field: "strEstadoCompletado", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "strEstadoCompletado", aggregate: "count" },
                    { field: "strEstadoCompletado", aggregate: "count" }
            ]
        });
        debugger;
        this.$grid = $("#divGridJefeRRHH").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strUnidadOrganica",
                    title: "UnidadOrganica",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strNombrePuesto",
                    title: "Puesto",
                    width: "200px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "datFechaReg",
                    title: "FechaReg",
                    width: "100px",
                    //format: "{0: yyyy-MM-dd HH:mm:ss}",
                    template: "#= kendo.toString(kendo.parseDate(datFechaReg, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                    attributes: { style: "text-align:center;" }


                },
                {
                    field: "strEstadoCompletado",
                    title: "Estado",
                    width: "100px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)",
                    attributes: { style: "text-align:center;" }

                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: 'Acciones',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //controles += '<a href="perfiles/nuevo">';                                                   
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerPerfilJefeRRHH(\'' + item.iCodPerfil + '\')">';
                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver información del perfil de puesto"></span>';
                        controles += '</button>';

                        if (item.bEstadoCompletado == true) {
                            //var items = [item.iCodPerfil, $("#hdIdCodTrabajadorBandJefe").val()];

                            if (item.strEstadoMovimiento == 'P') {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalHistoricoPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver histórico"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalAprobarPerfilJefeRRHH(\'' + item.iCodPerfil + '\')" >';
                                controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Aprobar" title="Aprobar perfil de puesto"></span>';
                                controles += '</button>';
                            }
                            else {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalHistoricoPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver histórico"></span>';
                                controles += '</button>';
                            }

                            
                        }

                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
    };

    this.PerfilPuestoJS.prototype.inicializarBandejaJefeRRHH = function () {
        debugger;
        this.CargarDatosBandejaJefeRRHH($("#hdIdCodTrabajadorBandJefeRRHH").val());
        $("#ddlOrgano_busquedaJefeRRHH").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Todos--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            filter: "contains",
            minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($options.filter != undefined && $options.filter.filters.length > 0)
                            data_param.strUnidad_Organica = $options.filter.filters[0].value;
                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        

        debugger;
        //$("#ddlUUOO_busqueda").data('kendoDropDownList').value(CodDependenciaBand);        
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        $("#txtFechaInicioJefeRRHH").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtFechaInicio").data("kendoDatePicker").value(todayDate);
        $("#txtFechaFinJefeRRHH").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtFechaFin").data("kendoDatePicker").value(todayDate);
    };
    
    this.PerfilPuestoJS.prototype.CargarDatosBandejaJefeRRHH = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#hdIdCodOrganoBand").val(res.iCodOrgano);

                CodOrganoBand = res.iCodOrgano;
                CodDependenciaBand = res.iCodDependencia;
                $("#hdIdCodDependenciaBandJefeRRHH").val(res.iCodDependencia);
                //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(res.iCodOrgano);
                $("#ddlUUOO_busquedaJefeRRHH").kendoDropDownList({
                    autoBind: true,
                    cascadeFrom: "ddlOrgano_busquedaJefeRRHH",
                    optionLabel: "--Todos--",
                    dataTextField: "strUnidad_Organica",
                    dataValueField: "iCodigoDependencia",
                    filter: "contains",
                    minLength: 3,
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: {
                                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                                type: "GET",
                                dataType: "json",
                                cache: false
                            },
                            parameterMap: function ($options, $operation) {
                                var data_param = {};
                                if ($options.filter != undefined && $options.filter.filters.length > 0)
                                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                                data_param.iCodOrgano = res.iCodOrgano;

                                return $.toDictionary(data_param);
                                this.CargarBandeja($('#ddlUUOO_busquedaJefeRRHH').data("kendoDropDownList").value());
                            }
                        }
                    }
                });

                //$("#ddlUUOO_busquedaJefe").data('kendoDropDownList').value(res.iCodDependencia);

            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    this.PerfilPuestoJS.prototype.VerPerfilJefeRRHH = function (id) {
        window.location.assign("VerPerfil_JefeRRHH?id=" + id);
    };

    this.PerfilPuestoJS.prototype.abrirModalAprobarPerfilJefeRRHH = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalAprobarPerfilJefeRRHH').data('kendoWindow');
        $("#txtObservacion").val('');
        //LimpiarModalRegistroPersona();

        if (id != 0) {

            $("#hdIdPerfilPuestoJefeRRHH").val(id);
            modal.title("Aprobar Perfil");

            modal.open();
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalAprobarPerfilJefeRRHH = function () {
        debugger;
        var modal = $('#ModalAprobarPerfilJefeRRHH').data('kendoWindow');
        modal.close();        
    }

    $('#ModalAprobarPerfilJefeRRHH').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Aprobar Perfil',
        visible: false,
        position: { top: '45%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PerfilPuestoJS.prototype.AprobarPerfilJefeRRHH = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDerivarJefeRRHH';

        if (true) {
            var modal = $('#ModalAprobarPerfilJefeRRHH').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuestoJefeRRHH").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuestoJefeRRHH").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBandJefeRRHH").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandJefeRRHH").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }

            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Aprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.inicializarBandejaJefeRRHH();
                                controlador.CargarBandejaPrincipalJefeRRHH(event);
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }

    this.PerfilPuestoJS.prototype.DesaprobarPerfilJefeRRHH = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDesaprobar';

        if (true) {
            var modal = $('#ModalAprobarPerfilJefeRRHH').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuestoJefeRRHH").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuestoJefeRRHH").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBandJefeRRHH").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandJefeRRHH").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }

            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Desaprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.inicializarBandejaJefeRRHH();
                                controlador.CargarBandejaPrincipalJefeRRHH(event);
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }

    ////////////////////////////BANDEJA JEFE//////////////////////////////////

    this.PerfilPuestoJS.prototype.abrirModalAprobarPerfil = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalAprobarPerfil').data('kendoWindow');
        $("#txtObservacion").val('');
        //LimpiarModalRegistroPersona();

        if (id != 0) {

            $("#hdIdPerfilPuestoJefe").val(id);
            modal.title("Aprobar Perfil");
            
            modal.open();
        }        
    }

    this.PerfilPuestoJS.prototype.cerrarModalAprobarPerfil = function () {
        debugger;
        var modal = $('#ModalAprobarPerfil').data('kendoWindow');
        modal.close();
    }

    $('#ModalAprobarPerfil').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Aprobar Perfil',
        visible: false,
        position: { top: '45%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {            
        }
    }).data("kendoWindow");



    this.PerfilPuestoJS.prototype.AprobarPerfil = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDerivarJefe';
        debugger;
        if (true) {
            var modal = $('#ModalAprobarPerfil').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuestoJefe").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuestoJefe").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBandJefe").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandJefe").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }            

            data_param.append('strObservacion', $("#txtObservacion").val());
            
            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Aprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.inicializarBandejaJefe();
                                window.location.href = "index_jefe";
                                //controlador.CargarBandejaPrincipalJefe();
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }

    this.PerfilPuestoJS.prototype.DesaprobarPerfilJefe = function (e) {
        e.preventDefault();
        var metodo = 'PerfilDesaprobar';
        debugger;
        if (true) {
            var modal = $('#ModalAprobarPerfil').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdPerfilPuestoJefe").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodPerfil', $("#hdIdPerfilPuestoJefe").val());
                //metodo = 'Guardar';
            }

            if ($("#hdIdCodTrabajadorBandJefe").val() != 0) {

                data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandJefe").val());
                //metodo = 'ActualizarDetFormacionAcademica';
            }

            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Desaprobación registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                //$("#hdIdEmpleado").val(res.responseText);
                                //$("#hdIdPerfilPuesto").val(res.responseText);
                                controlador.inicializarBandejaJefe();
                                //controlador.CargarDatosFormAcaNivelEducativo(res.responseText);
                                //controlador.CargarFormularioTrabajador(res.responseText);

                                //modal.close();
                                //$('#divGrid').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
            modal.close();

        }
        else {
            controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
        }

    }
    
    this.PerfilPuestoJS.prototype.CargarHistorico = function (id) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ListarPerfilHistorico',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.id = id;
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }            
        });
        debugger;
        this.$grid = $("#divGridHistorico").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "strFechaReg",
                    title: "Fecha Registro",                    
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "strUnidadOrganica_Envia",
                    title: "Envia",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                },
                {
                    field: "strUnidadOrganica_Recibe",
                    title: "Recibe",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strObservacion",
                    title: "Observacion",
                    attributes: { style: "text-align:center;" },
                    width: "30px"
                    
                },
                {
                    field: "strEstadoAprobacion",
                    title: "Estado",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
            ]
        }).data();
        debugger;
    };

    this.PerfilPuestoJS.prototype.abrirModalHistoricoPerfil = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalHistoricoPerfil').data('kendoWindow');

        //LimpiarModalRegistroPersona();

        if (id != 0) {

            //$("#hdIdPerfilPuestoJefe").val(id);
            modal.title("Histórico Perfil");

            modal.open();
            controlador.CargarHistorico(id);
        }
    }

    this.PerfilPuestoJS.prototype.cerrarModalHistoricoPerfil = function () {
        debugger;
        var modal = $('#ModalHistoricoPerfil').data('kendoWindow');
        modal.close();
    }

    $('#ModalHistoricoPerfil').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Aprobar Perfil',
        visible: false,
        position: { top: '45%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
        }
    }).data("kendoWindow");

    this.PerfilPuestoJS.prototype.inicializarBandejaJefe = function () {
        debugger;
        this.CargarDatosBandejaJefe($("#hdIdCodTrabajadorBandJefe").val());

        //$("#ddlOrgano_busqueda").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strUnidad_Organica",
        //    dataValueField: "iCodigoDependencia",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(CodOrganoBand);

        //$("#ddlUUOO_busquedaJefe").kendoDropDownList({
        //    autoBind: true,
        //    cascadeFrom: "ddlOrgano_busqueda",
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strUnidad_Organica",
        //    dataValueField: "iCodigoDependencia",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

        //                data_param.iCodOrgano = $('#ddlOrgano_busqueda').data("kendoDropDownList").value();

        //                return $.toDictionary(data_param);
        //                this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
        //            }
        //        }
        //    }
        //});

        debugger;
        //$("#ddlUUOO_busqueda").data('kendoDropDownList').value(CodDependenciaBand);        
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        $("#txtFechaInicioJefe").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtFechaInicio").data("kendoDatePicker").value(todayDate);
        $("#txtFechaFinJefe").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtFechaFin").data("kendoDatePicker").value(todayDate);
    };


    this.PerfilPuestoJS.prototype.CargarDatosBandejaJefe = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#hdIdCodOrganoBand").val(res.iCodOrgano);

                CodOrganoBand = res.iCodOrgano;
                CodDependenciaBand = res.iCodDependencia;
                $("#hdIdCodDependenciaBandJefe").val(res.iCodDependencia);
                //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(res.iCodOrgano);
                $("#ddlUUOO_busquedaJefe").kendoDropDownList({
                    autoBind: true,
                    //cascadeFrom: "ddlOrgano_busqueda",
                    optionLabel: "--Seleccione--",
                    dataTextField: "strUnidad_Organica",
                    dataValueField: "iCodigoDependencia",
                    filter: "contains",
                    minLength: 3,
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: {
                                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                                type: "GET",
                                dataType: "json",
                                cache: false
                            },
                            parameterMap: function ($options, $operation) {
                                var data_param = {};
                                if ($options.filter != undefined && $options.filter.filters.length > 0)
                                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                                data_param.iCodOrgano = res.iCodOrgano;

                                return $.toDictionary(data_param);
                                this.CargarBandeja($('#ddlUUOO_busquedaJefe').data("kendoDropDownList").value());
                            }
                        }
                    }
                });

                $("#ddlUUOO_busquedaJefe").data('kendoDropDownList').value(res.iCodDependencia);

            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    //this.PerfilPuestoJS.prototype.CargarBandeja = function () {
    //    //controladorApp.notificarMensajeDeAlerta(_id);
    //    var _id = $('#ddlUUOO_busqueda').data("kendoDropDownList").value()
    //    //alert(_id);
    //    var data_param = new FormData();
    //    //data_param.append('id', _id);
    //    data_param.append('id', _id);

    //    $.ajax({
    //        url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
    //        type: 'POST',
    //        dataType: 'json',
    //        contentType: false,
    //        processData: false,
    //        data: data_param,
    //        success: function (res) {
    //            debugger;
    //            controladorApp.notificarMensajeDeAlerta(res);
    //        },
    //        error: function (res) {
    //            debugger;
    //        }
    //    });
    //    //controladorApp.notificarMensajeDeAlerta(res);
    //}

    this.PerfilPuestoJS.prototype.CargarBandejaDependenciasJefe = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerDependenciasPorUUOO',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.id = $('#ddlUUOO_busquedaJefe').data("kendoDropDownList").value();                        
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            }
            //change: function (e) {
            //    $("#lblTotal").html(this.total());
            //    debugger;
            //},
            //schema: {
            //    total: function (response) {
            //        //debugger;
            //        //var TotalDeRegistros = 0;
            //        //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
            //        return response.length; // TotalDeRegistros;
            //    },
            //    model: {
            //        id: "iCodPerfil"
            //    }
            //},
            //group: {
            //    field: "strNombrePuesto", aggregates: [
            //       { field: "strNombrePuesto", aggregate: "count" }
            //    ]
            //},
            //aggregate: [
            //        { field: "strNombrePuesto", aggregate: "count" },
            //        { field: "strNombrePuesto", aggregate: "count" }
            //]
        });
        debugger;
        this.$grid = $("#divGridDependencias").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "IdDependencia",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "Nombre",
                    title: "UnidadOrganica",
                    width: "100px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "RegistroUsuarioCreacion",
                    title: "Solicitudes Pendientes de Aprobar",
                    width: "5px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";

                        var items = [$('#ddlUUOO_busquedaJefe').data("kendoDropDownList").value(), item.IdDependencia];
                        //controles += '<a href="perfiles/nuevo">';
                        //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.CargarBandejaPrincipalJefe(\'' + item.IdDependencia + '\')">';
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.CargarBandejaPrincipalJefe(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-search" aria-hidden="true" data-uib-tooltip="Buscar" title="Buscar perfil de puesto"></span>';
                        controles += '</button>';
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
    };

    this.PerfilPuestoJS.prototype.CargarBandejaPrincipalJefe = function (item) {
        //e.preventDefault();

        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodUnidadOrganica = _item[0];
            items.IdDependencia = _item[1];
            
        }
        else {
            items.iCodPerfil = item;
        }
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    //url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoJefe',                    
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.IdDependencia = items.IdDependencia;
                        data_param.iCodUnidadOrganica = items.iCodUnidadOrganica;
                        data_param.fechaIni = $("#txtFechaInicioJefe").data("kendoDatePicker").value();
                        data_param.fechaFin = $("#txtFechaFinJefe").data("kendoDatePicker").value();
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
            debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                        id: "iCodPerfil"
                }
            },
            group: {
                field: "strEstadoCompletado", aggregates: [
                   { field: "strEstadoCompletado", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "strEstadoCompletado", aggregate: "count" },
                    { field: "strEstadoCompletado", aggregate: "count" }
            ]
        });
        debugger;
        this.$grid = $("#divGridJefe").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strUnidadOrganica",
                    title: "UnidadOrganica",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strNombrePuesto",
                    title: "Puesto",
                    width: "200px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "datFechaReg",
                    title: "FechaReg",
                    width: "100px",
                    //format: "{0: yyyy-MM-dd HH:mm:ss}",
                    template: "#= kendo.toString(kendo.parseDate(datFechaReg, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                    attributes: { style: "text-align:center;" }


                },                
                {
                    field: "strEstadoCompletado",
                    title: "Estado",
                    width: "100px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)",
                    attributes: { style: "text-align:center;" }

                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: 'Acciones',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //controles += '<a href="perfiles/nuevo">';                                                   
                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerPerfilJefe(\'' + item.iCodPerfil + '\')">';
                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver información del perfil de puesto"></span>';
                            controles += '</button>';
                        
                        if (item.bEstadoCompletado == true) {
                            //var items = [item.iCodPerfil, $("#hdIdCodTrabajadorBandJefe").val()];

                            if (item.strEstadoMovimiento == 'P') {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalHistoricoPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver histórico"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalAprobarPerfil(\'' + item.iCodPerfil + '\')" >';
                                controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Aprobar" title="Aprobar perfil de puesto"></span>';
                                controles += '</button>';
                            }
                            else {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalHistoricoPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver histórico"></span>';
                                controles += '</button>';
                            }

                            
                        }
                        
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
    };

    //this.PerfilPuestoJS.prototype.ActualizarJefe = function (id) {
    //    window.location.assign("Perfiles/ActualizarPerfil?id=" + id);
    //};
    this.PerfilPuestoJS.prototype.VerPerfilJefe = function (id) {
        window.location.assign("VerPerfil_Jefe?id=" + id);
    };
    ////////////////////////////BANDEJA USER//////////////////////////////////

    this.PerfilPuestoJS.prototype.inicializarBandejaUser = function () {
        debugger;
        this.CargarDatosBandejaUser($("#hdIdCodTrabajadorBandUser").val());

        //$("#ddlOrgano_busquedaUser").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strUnidad_Organica",
        //    dataValueField: "iCodigoDependencia",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        //$("#ddlOrgano_busqueda").data('kendoDropDownList').value(CodOrganoBand);

        //$("#ddlUUOO_busquedaUser").kendoDropDownList({
        //    autoBind: true,
        //    cascadeFrom: "ddlOrgano_busquedaUser",
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "strUnidad_Organica",
        //    dataValueField: "iCodigoDependencia",
        //    filter: "contains",
        //    minLength: 3,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

        //                data_param.iCodOrgano = $('#ddlOrgano_busquedaUser').data("kendoDropDownList").value();

        //                return $.toDictionary(data_param);
        //                this.CargarBandeja($('#ddlUUOO_busquedaUser').data("kendoDropDownList").value());
        //            }
        //        }
        //    }
        //});

        debugger;
        //$("#ddlUUOO_busqueda").data('kendoDropDownList').value(CodDependenciaBand);        
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        $("#txtFechaInicioUser").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtFechaInicio").data("kendoDatePicker").value(todayDate);
        $("#txtFechaFinUser").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtFechaFin").data("kendoDatePicker").value(todayDate);
    };

    this.PerfilPuestoJS.prototype.CargarDatosBandejaUser = function (_id) {
        var data_param = new FormData();
        data_param.append('id', _id);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Perfiles/ConsultarUUOO',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                //$("#hdIdCodOrganoBand").val(res.iCodOrgano);

                CodOrganoBand = res.iCodOrgano;
                CodDependenciaBand = res.iCodDependencia;
                $("#hdIdCodDependenciaBandUser").val(res.iCodDependencia);
                //$("#ddlOrgano_busquedaUser").data('kendoDropDownList').value(res.iCodOrgano);

                $("#ddlUUOO_busquedaUser").kendoDropDownList({
                    autoBind: true,
                    //cascadeFrom: "ddlOrgano_busquedaUser",
                    optionLabel: "--Seleccione--",
                    dataTextField: "strUnidad_Organica",
                    dataValueField: "iCodigoDependencia",
                    filter: "contains",
                    minLength: 3,
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: {
                                url: controladorApp.obtenerRutaBase() + "Perfiles/ListarUnidadesOrganicas",
                                type: "GET",
                                dataType: "json",
                                cache: false
                            },
                            parameterMap: function ($options, $operation) {
                                var data_param = {};
                                if ($options.filter != undefined && $options.filter.filters.length > 0)
                                    data_param.strUnidad_Organica = $options.filter.filters[0].value;

                                data_param.iCodOrgano = res.iCodOrgano;

                                return $.toDictionary(data_param);
                                this.CargarBandeja($('#ddlUUOO_busquedaUser').data("kendoDropDownList").value());
                            }
                        }
                    }
                });




                $("#ddlUUOO_busquedaUser").data('kendoDropDownList').value(res.iCodDependencia);

            },
            error: function (res) {
                debugger;
            }
        });
        //controladorApp.notificarMensajeDeAlerta(CodDependenciaBand);
        //this.CargarBandeja(CodDependenciaBand);
        //this.CargarBandeja($('#ddlUUOO_busqueda').data("kendoDropDownList").value());
    }

    //this.PerfilPuestoJS.prototype.CargarBandeja = function () {
    //    //controladorApp.notificarMensajeDeAlerta(_id);
    //    var _id = $('#ddlUUOO_busqueda').data("kendoDropDownList").value()
    //    //alert(_id);
    //    var data_param = new FormData();
    //    //data_param.append('id', _id);
    //    data_param.append('id', _id);

    //    $.ajax({
    //        url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuesto',
    //        type: 'POST',
    //        dataType: 'json',
    //        contentType: false,
    //        processData: false,
    //        data: data_param,
    //        success: function (res) {
    //            debugger;
    //            controladorApp.notificarMensajeDeAlerta(res);
    //        },
    //        error: function (res) {
    //            debugger;
    //        }
    //    });
    //    //controladorApp.notificarMensajeDeAlerta(res);
    //}

    this.PerfilPuestoJS.prototype.CargarBandejaPrincipalUser = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/ObtenerPerfilesPuestoUser',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.fechaIni = $("#txtFechaInicioUser").data("kendoDatePicker").value();
                        data_param.fechaFin = $("#txtFechaFinUser").data("kendoDatePicker").value();
                        //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                debugger;
            },
            schema: {
                total: function (response) {
                    //debugger;
                    //var TotalDeRegistros = 0;
                    //if (response.length > 0) TotalDeRegistros = response[0].Grilla.TotalDeRegistros;
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "iCodPerfil"
                }
            },
            group: {
                field: "strEstadoCompletado", aggregates: [
                   { field: "strEstadoCompletado", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "strEstadoCompletado", aggregate: "count" },
                    { field: "strEstadoCompletado", aggregate: "count" }
            ]
        });
        debugger;
        this.$grid = $("#divGridUser").kendoGrid({
            //toolbar: ["excel", ],
            //excel: {
            //    fileName: "Listado de Empleados.xlsx",
            //    filterable: false
            //},            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            //groupable: true,

            dataType: 'json',
            columns: [
                {
                    field: "iCodPerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strUnidadOrganica",
                    title: "UnidadOrganica",
                    width: "30px",                    
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "strNombrePuesto",
                    title: "Puesto",
                    width: "200px",                    
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "datFechaReg",
                    title: "FechaReg",
                    width: "100px",
                    //format: "{0: yyyy-MM-dd HH:mm:ss}",
                    template: "#= kendo.toString(kendo.parseDate(datFechaReg, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                    attributes: { style: "text-align:center;" }


                },
                {
                    field: "strEstadoCompletado",
                    title: "Estado",
                    width: "100px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "strEstadoDerivado",
                    title: "EstadoDerivado",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    hidden: true
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //controles += '<a href="perfiles/nuevo">';
                        if (item.strEstadoDerivado == 'N' || item.strEstadoDerivado == 'D') {
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.ActualizarUser(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Actualizar" title="Actualizar información del perfil de puesto"></span>';
                            controles += '</button>';
                            if (item.bEstadoCompletado == true) {

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalHistoricoPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver histórico"></span>';
                                controles += '</button>';

                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.DerivarPerfil(\'' + item.iCodPerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Aprobar" title="Aprobar perfil de puesto"></span>';
                                controles += '</button>';
                            }
                        }
                        else {
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerPerfilUser(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver información del perfil de puesto"></span>';
                            controles += '</button>';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + item.iCodPerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Generar" title="Generar PDF perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        
                        
                        //controles += '</a>';

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
    };

    this.PerfilPuestoJS.prototype.DerivarPerfil = function (id) {
        //e.preventDefault();
        var metodo = 'PerfilDerivarUser';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        if (true) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();

            data_param.append('iCodPerfil', id);
            data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandUser").val());

            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de derivar el perfil?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Perfil derivado correctamente");
                                //controlador.inicializarBandejaUser();
                                controlador.CargarBandejaPrincipalUser(event);
                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                
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

    };

    this.PerfilPuestoJS.prototype.GenerarFormatoPerfil = function () {
        var IdPerfil = $('#hdIdPerfilPuesto').val();

        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Perfiles/PlantillaPerfilPuesto?id=" + IdPerfil, '_blank');
    }
    this.PerfilPuestoJS.prototype.GenerarFormatoRequerimiento = function () {
        var IdPerfil = $('#hdIdPerfilPuesto').val();

        //controlador.GenerarPDFRRHH(\'' + item.iCodPerfil + '\')
        window.open(controladorApp.obtenerRutaBase() + "Perfiles/PlantillaRequerimientoPuesto?id=" + IdPerfil, '_blank');
    }

    this.PerfilPuestoJS.prototype.ActualizarUser = function (id) {
        window.location.assign("ActualizarPerfil?id=" + id);
    };
    this.PerfilPuestoJS.prototype.VerPerfilUser = function (id) {
        window.location.assign("VerPerfil_User?id=" + id);
    };
    this.PerfilPuestoJS.prototype.GenerarPDF = function (id) {
        window.location.assign("PlantillaPerfilPuesto?id=" + id);
    };
    this.PerfilPuestoJS.prototype.GenerarPDFRRHH = function (id) {
        window.open(controladorApp.obtenerRutaBase() + "Perfiles/PlantillaPerfilPuesto?id=" + id, '_blank');
        //window.location.assign();
    };
    this.PerfilPuestoJS.prototype.registrarNuevaCarrera = function () {
        if ($("#txtNuevaCarrera").val() == '') {
            controladorApp.notificarMensajeDeAlerta('Debe ingresar el nombre de la carrera profesional que desea crear');
            return;
        }
        else {
            var data_param = new FormData();
            data_param.append('strNivel4', $("#txtNuevaCarrera").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/InsertarCarreraProfesional',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            controladorApp.notificarMensajeSatisfactorio("Carrera profesional registrada correctamente");
                            controlador.cancelarNuevaCarrera();
                        },
                        error: function (res) {
                            alert(res);
                        }
                    });
                }, data_param);
            
        }
    };
    this.PerfilPuestoJS.prototype.registrarNuevaMaestria = function () {
        if ($("#txtNuevaMaestria").val() == '') {
            controladorApp.notificarMensajeDeAlerta('Debe ingresar el nombre de la especialidad que desea crear');
            return;
        }
        else {
            var data_param = new FormData();
            data_param.append('strNivel4', $("#txtNuevaMaestria").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/InsertarMaestria',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            controladorApp.notificarMensajeSatisfactorio("Especialidad registrada correctamente");
                            controlador.cancelarNuevaMaestria();
                        },
                        error: function (res) {
                            alert(res);
                        }
                    });
                }, data_param);

        }
    };
    this.PerfilPuestoJS.prototype.registrarNuevoDoctorado = function () {
        if ($("#txtNuevoDoctorado").val() == '') {
            controladorApp.notificarMensajeDeAlerta('Debe ingresar el nombre de la especialidad que desea crear');
            return;
        }
        else {
            var data_param = new FormData();
            data_param.append('strNivel4', $("#txtNuevoDoctorado").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realizar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/InsertarDoctorado',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            controladorApp.notificarMensajeSatisfactorio("Especialidad registrada correctamente");
                            controlador.cancelarNuevoDoctorado();
                        },
                        error: function (res) {
                            alert(res);
                        }
                    });
                }, data_param);

        }
    };
    this.PerfilPuestoJS.prototype.cancelarNuevaCarrera = function () {
        $("#divNuevaCarrera").hide();
        $("#divCarrera").show();
    };
    this.PerfilPuestoJS.prototype.mostrarNuevaCarrera = function () {
        $("#divNuevaCarrera").show();
        $("#divCarrera").hide();
        $("#txtNuevaCarrera").focus();
        
    };
    this.PerfilPuestoJS.prototype.cancelarNuevaMaestria = function () {
        $("#divNuevaMaestria").hide();
        $("#divMaestria").show();
    };
    this.PerfilPuestoJS.prototype.mostrarNuevaMaestria = function () {
        $("#divNuevaMaestria").show();
        $("#divMaestria").hide();
        $("#txtNuevaMaestria").focus();

    };
    this.PerfilPuestoJS.prototype.cancelarNuevoDoctorado = function () {
        $("#divNuevoDoctorado").hide();
        $("#divDoctorado").show();
    };
    this.PerfilPuestoJS.prototype.mostrarNuevoDoctorado = function () {
        $("#divNuevoDoctorado").show();
        $("#divDoctorado").hide();
        $("#txtNuevoDoctorado").focus();

    };

    this.PerfilPuestoJS.prototype.abrirModalNuevaSolicitud = function () {

        //$('#hdIdConvocatoria').val(IdConvocatoria);

        //$("#txtNroConvocatoriaCurri").val('');
        //$("#txtCargoConvocatoriaCurri").val('');
        //$("#txtEnlaceReunion").val('');

        //var data_param = new FormData();
        //data_param.append('IdConvocatoria', IdConvocatoria);

        var modal = $('#divModalNuevaSolicitud').data('kendoWindow');

        //$.ajax({
        //    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
        //    type: 'POST',
        //    dataType: 'json',
        //    contentType: false,
        //    processData: false,
        //    data: data_param,
        //    success: function (res) {
        //        debugger;
        //        $("#txtNroConvocatoriaAsigna").val(res.NroConvocatoria);
        //        $("#txtCargoConvocatoriaAsigna").val(res.NombreCargo);

        //        //if (res.IdTieneExamenConoc == 1)
        //        //    controlador.inicializarGridConocimientos();
        //        //else
        //        //    controlador.inicializarGridCurricular();


                modal.open(); //.center();
        //    },
        //    error: function (res) {
        //        debugger;
        //    }
        //});

        //$("#ddlAsignarEvaluacion").kendoDropDownList({
        //    autoBind: true,
        //    optionLabel: "--Seleccione--",
        //    dataTextField: "NombreCompleto",
        //    dataValueField: "IdEmpleado",
        //    filter: "contains",
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Empleado/ListarEmpleados",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                if ($options.filter != undefined && $options.filter.filters.length > 0)
        //                    data_param.Nombre = $options.filter.filters[0].value;

        //                data_param.IdDependencia = IdDependencia;
        //                data_param.Estado = 1
        //                data_param.IdEmpleado = 0
        //                data_param.Grilla = {};
        //                data_param.Grilla.RegistrosPorPagina = 0;
        //                data_param.Grilla.OrdenarPor = "Nombre";
        //                data_param.Grilla.OrdenarDeForma = "ASC";

        //                if (data_param.Nombre == data_param.IdDependencia) data_param.Nombre = '';
        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

    }
    this.PerfilPuestoJS.prototype.registrarNuevaSolicitud = function (e) {
        e.preventDefault();

        if (frmNuevaSolicitud.validate()) {
            var modal = $('#divModalNuevaSolicitud').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();
            //data_param.append('IdConvocatoria', $("#hdIdConvocatoria").val());
            data_param.append('IdUsuarioAsignado', $("#ddlComiteMiembro1T").data("kendoDropDownList").value());

            data_param.append('iCodOrgano', $("#ddlOrgano").data("kendoDropDownList").value());
            data_param.append('iCodUnidadOrganica', $("#ddlUUOO").data("kendoDropDownList").value());
            data_param.append('strNombrePuesto', $("#txtNombrePuesto").val());
            data_param.append('strDependenciaJerarquicaLineal', $("#txtDependenciaJerarquicaLineal").val());
            data_param.append('strPuestoEstructural', 'No Aplica');
            data_param.append('strDependenciaFuncional', 'No Aplica');
            data_param.append('strPuestos_a_su_Cargo', 'No Aplica');
            data_param.append('strMision', '');

            data_param.append('iAnioExpGeneral', 0);
            data_param.append('iAnioExpEspecifica', 0);
            data_param.append('strDesExpEspecifica', '');
            data_param.append('iAnioExpSectorPublico', 0);
            data_param.append('iCodNivelMinimo', 0);


            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de asignar el registro de perfil de puesto al trabajador seleccionado?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación el trabajador seleccionado podrá realizar el registro del perfil de puesto</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Perfiles/AsignarNuevaSolicitudPerfilPuesto',
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
                                controladorApp.notificarMensajeSatisfactorio("Se envió correctamente la asignación al trabajador seleccionado");

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
    this.PerfilPuestoJS.prototype.finalizarRegistroPerfil = function () {
        var data_param = new FormData();
        data_param.append('strNombrePuesto', $("#txtNombrePuesto").val());

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de finalizar el registro del perfil de puesto y enviarlo a la OGRH para su revisión?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/SolicitarEvaluacionPerfilAnexo1',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') 
                            controladorApp.notificarMensajeDeAlerta(res.responseText);
                        else 
                            controladorApp.notificarMensajeSatisfactorio("Se envió correctamente la solicitud de evaluación a la OGRH");
                            //modal.close();
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }
    this.PerfilPuestoJS.prototype.aprobarRegistroPerfil = function () {
        var id = $("#hdIdPerfilPuesto").val();
        var data_param = new FormData();
        data_param.append('iCodPerfil', id);

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de aprobar el perfil de puesto y finalizar su registro?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Perfiles/PerfilFinalizar',
                    type: 'POST',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: arg,
                    success: function (res) {
                        debugger;
                        if (res.success == 'False') 
                            controladorApp.notificarMensajeDeAlerta(res.responseText);
                        else 
                            controladorApp.notificarMensajeSatisfactorio("El perfil de puesto se aprobó correctamente");
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }

}(jQuery));