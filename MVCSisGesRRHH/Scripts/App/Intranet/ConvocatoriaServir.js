(function ($) {
    var frmRegistroConvocatoria;
    var frmEdicionConvocatoria;
    var frmEvaluacionCurriValidador;
    var frmAsignarEvaluacionValidador;
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


    this.ConvocatoriaServirJS = function () { };
    this.ConvocatoriaServirJS.prototype.inicializar = function () {
        $("#fileDocComite").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            select: function (e) {
                $("#hdDocComite").val('1');
            },
            remove: function (e) {
                $("#hdDocComite").val('');
            }
        });
        $("#fileDocRequerimiento").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            select: function (e){
                $("#hdDocRequerimiento").val('1');
            },
            remove: function (e) {
                $("#hdDocRequerimiento").val('');
            }
        });
        $("#fileDocCertificacion").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            select: function (e) {
                $("#hdDocCertificacion").val('1');
            },
            remove: function (e) {
                $("#hdDocCertificacion").val('');
            }
        });
        $("#fileDocComiteE").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            localization: {
                select: 'Actualizar documento...',
                remove: '',
                cancel: ''
            },
            select: function (e) {
                $("#hdDocComite").val('1');
            },
            remove: function (e) {
                $("#hdDocComite").val('');
            }
        });
        $("#fileDocRequerimientoE").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            localization: {
                select: 'Actualizar documento...',
                remove: '',
                cancel: ''
            },
            select: function (e) {
                $("#hdDocRequerimiento").val('1');
            },
            remove: function (e) {
                $("#hdDocRequerimiento").val('');
            }
        });
        $("#fileDocCertificacionE").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            },
            localization: {
                select: 'Actualizar documento...',
                remove: '',
                cancel: ''
            },
            select: function (e) {
                $("#hdDocCertificacion").val('1');
            },
            remove: function (e) {
                $("#hdDocCertificacion").val('');
            }
        });

        /* BUSQUEDA */
        $("#ddlEstado_busqueda").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarEstados",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlTipoFormato").kendoDropDownList({
            autoBind: false,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarConvocatoriaTipoDocumento",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
            change: function (e) {
                var estado = this.value();

                debugger;
                if (estado == '30' || estado == '31' || estado == '33') 
                    $('#btnGenerarFicha').show();
                else 
                    $('#btnGenerarFicha').hide();
            }
        });
        //$("#ddlCondicion_busqueda").kendoDropDownList({
        //    autoBind: false,
        //    optionLabel: "--Todos--",
        //    dataTextField: "Nombre",
        //    dataValueField: "IdCondicion",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Condicion/ListarCondicion",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            },
        //            parameterMap: function ($options, $operation) {
        //                var data_param = {};
        //                data_param.perfil = $("#hdPerfil").val();

        //                return $.toDictionary(data_param);
        //            }
        //        }
        //    }
        //});

        //$("#ddlSede_busqueda").kendoDropDownList({
        //    autoBind: false,
        //    optionLabel: "--Todos--",
        //    dataTextField: "Nombre",
        //    dataValueField: "IdSede",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Sede/ListarSedes",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            }
        //        }
        //    }
        //});

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
        $("#ddlDependencia").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "strUnidad_Organica",
            dataValueField: "iCodigoDependencia",
            //filter: "contains",
            //minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Perfiles/ListarOrganos", //"Dependencia/ListarDependencias",
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
        $("#ddlBases").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlDependencia",
            optionLabel: "--Seleccione--",
            dataTextField: "strNombrePuesto",
            dataValueField: "iCodBasePerfil",
            //filter: "contains",
            //minLength: 3,
            dataSource: {
                serverFiltering: true,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Bases/ObtenerBasesPerfilesPuestoConvocatoria",
                        type: "POST",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        if ($operation === "read") {
                            data_param.idDependencia = $('#ddlDependencia').data("kendoDropDownList").value();
                            data_param.iTipo = 2;
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
            },
            change: function (e) {
                var estado = this.value();
                
                debugger;
                if (estado != '') {
                    LimpiarModalRegistroConvocatoria();
                    CargarFormularioConvocatoria(estado);
                } else {
                    LimpiarModalRegistroConvocatoria();
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
        $("#fileFormatoEnt").kendoUpload({
            multiple: false,
            validation: {
                allowedExtensions: [".pdf"],
                maxFileSize: 5242880
            }
        });

        //$("#txtPersonaRemuneracion").kendoNumericTextBox({
        //    format: "c",
        //    decimals: 2
        //});

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
        $("#ddlComiteDependencia2").kendoDropDownList({
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
        $("#ddlComiteDependencia3").kendoDropDownList({
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
        $("#ddlComiteMiembro1S").kendoDropDownList({
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
        $("#ddlComiteMiembro2T").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlComiteDependencia2",
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

                        data_param.IdDependencia = $("#ddlComiteDependencia2").data("kendoDropDownList").value();
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
        $("#ddlComiteMiembro2S").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlComiteDependencia2",
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

                        data_param.IdDependencia = $("#ddlComiteDependencia2").data("kendoDropDownList").value();
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
        $("#ddlComiteMiembro3T").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlComiteDependencia3",
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

                        data_param.IdDependencia = $("#ddlComiteDependencia3").data("kendoDropDownList").value();
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
        $("#ddlComiteMiembro3S").kendoDropDownList({
            autoBind: true,
            cascadeFrom: "ddlComiteDependencia3",
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

                        data_param.IdDependencia = $("#ddlComiteDependencia3").data("kendoDropDownList").value();
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
        /* CONSULTA */
        $('#divModalEliminacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '30%',
            height: 'auto',
            title: 'Confirmar eliminación',
            visible: false,
            position: { top: '5%', left: "35%" },
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $("#ddlPersonaTipoDeDocumento").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarTipoDeDocumento",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPersonaSede").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "IdSede",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Sede/ListarSedes",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        //$("#ddlPersonaEstado").kendoDropDownList({
        //    autoBind: false,
        //    //optionLabel: "--Seleccione--",
        //    dataTextField: "Nombre",
        //    dataValueField: "Codigo",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                url: controladorApp.obtenerRutaBase() + "Persona/ListarEstado",
        //                type: "GET",
        //                dataType: "json",
        //                cache: false
        //            }
        //        }
        //    },
        //    change: function (e) {
        //        debugger;
        //        var estado = this.value();
                
        //        if (estado == '1') {
        //            //$("#divCese").hide();
        //            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
        //            $("#txtPersonaFechaFinLabores").removeAttr("required");
        //            $("#txtPersonaFechaFinLabores").val('');
        //        }
        //        if (estado == '0') {
        //            //$("#divCese").show();
        //            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
        //            $("#txtPersonaFechaFinLabores").attr("required", true);
        //            $("#txtPersonaFechaFinLabores").attr("validationmessage", "requerido");
        //        }
        //    }
        //});
        $('#divModalRegistroConvocatoria').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '98%',
            height: 'auto',
            title: 'Agregar Convocatoria',
            visible: false,
            position: { top: '10%', left: "1%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmRegistroConvocatoria.hideMessages();

                $("#hdIdConvocatoria").val('0');
                $("#hdIdPerfil").val('0');
                $("#ddlBases").data("kendoDropDownList").value('');
                $("#ddlDependencia").data("kendoDropDownList").value('');
                $("#txtAIRHSPConvocatoria").val('');
                $("#txtMetaConvocatoria").val('');
                $("#ddlComiteDependencia1").data("kendoDropDownList").value('');
                $("#ddlComiteDependencia2").data("kendoDropDownList").value('');
                $("#ddlComiteDependencia3").data("kendoDropDownList").value('');

            }
        }).data("kendoWindow");
        frmRegistroConvocatoria = $("#frmRegistroConvocatoria").kendoValidator().data("kendoValidator");

        $('#divModalEdicionConvocatoria').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '90%',
            height: 'auto',
            title: 'Actualizar Convocatoria',
            visible: false,
            position: { top: '5%', left: "5%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close", "Maximize"],
            close: function () {
                frmEdicionConvocatoria.hideMessages();

                $("#hdIdConvocatoria").val('0');
                $("#hdIdPerfil").val('0');
                //$("#ddlBases").data("kendoDropDownList").value('');
                //$("#ddlDependencia").data("kendoDropDownList").value('');
                $("#txtAIRHSPConvocatoriaE").val('');
                $("#txtMetaConvocatoriaE").val('');

                $("#hdComiteDependencia1E").val('');
                $("#hdComiteDependencia2E").val('');
                $("#hdComiteDependencia3E").val('');

                $("#txtComiteDependencia1E").val('');
                $("#txtComiteDependencia2E").val('');
                $("#txtComiteDependencia3E").val('');

                var panelBar = $("#pnlConvocatoria").data("kendoPanelBar");
                panelBar.clearSelection();
                panelBar.collapse($("#liTab1E"), false);
                panelBar.collapse($("#liTab2E"), false);
                panelBar.collapse($("#liTab3E"), false);
                panelBar.collapse($("#liTab4E"), false);
                panelBar.collapse($("#liTab5E"), false);
                panelBar.collapse($("#liTab6E"), false);
            }//,
            //open: function () {
            //    this.center();
            //}
        }).data("kendoWindow");
        frmEdicionConvocatoria = $("#frmEdicionConvocatoria").kendoValidator().data("kendoValidator");

        $('#divModalEvaluacionCurri').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Notificación de Entrevistas al Comité de Selección',
            visible: false,
            position: { top: '20%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        frmEvaluacionCurriValidador = $("#frmEvaluacionCurri").kendoValidator().data("kendoValidator");

        $('#divModalAsignarEvaluacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '60%',
            height: 'auto',
            title: 'Asignar la Evaluación Curricular',
            visible: false,
            position: { top: '20%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        frmAsignarEvaluacionValidador = $("#frmAsignarEvaluacion").kendoValidator().data("kendoValidator");

        $('#divModalFormato').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Agregar documento de comunicación sobre la convocatoria',
            visible: false,
            position: { top: '25%', left: "25%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");
        $('#divModalFormatoEnt').kendoWindow({
            draggable: true,
            modal: true,
            pinned: true,
            resizable: false,
            width: '50%',
            height: 'auto',
            title: 'Finalizar Evaluación de Entrevista',
            visible: false,
            position: { top: '20%', left: "20%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
            }
        }).data("kendoWindow");

        $("#ddlTipoConvocatoria").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "ConvocatoriaServir/ListarTipoConvocatoria",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        //$("#txtPersonaFechaNacimiento").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        //$("#txtPersonaFechaInicioLabores").kendoDatePicker({ format: "dd/MM/yyyy", min: new Date() });
        //$("#txtPersonaFechaFinLabores").kendoDatePicker({ format: "dd/MM/yyyy" }); // max: new Date()

        $("#ddlEstado_busqueda").data("kendoDropDownList").value("0");

        $("#pnlConvocatoria").kendoPanelBar({
            expandMode: "single"
        });

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
    function LimpiarModalEdicionConvocatoria() {
        var today = new Date();
        var month = today.getMonth();
        if (today.getMonth() > 1) var month = month - 1;
        var year = today.getFullYear();
        
        $("#txtNroConvocatoriaE").val("");
        $("#txtCantidadConvocatoriaE").val("");
        $("#txtRemuneracionConvocatoriaE").val("");
        $("#txtCargoConvocatoriaE").val("");
        $("#txtAIRHSPConvocatoriaE").val("");
        $("#txtMetaConvocatoriaE").val("");

        $("#fileDocRequerimientoE").data("kendoUpload").clearAllFiles();
        $("#fileDocCertificacionE").data("kendoUpload").clearAllFiles();
        $("#fileDocComiteE").data("kendoUpload").clearAllFiles();

        $("#appE").empty();

        if ($("#divGridCurricular").data('kendoGrid') != null)
            $("#divGridCurricular").data('kendoGrid').dataSource.data([]);
        if ($("#divGridConocimientos").data('kendoGrid') != null)
            $("#divGridConocimientos").data('kendoGrid').dataSource.data([]);
        if ($("#divGridEntrevistas").data('kendoGrid') != null)
            $("#divGridEntrevistas").data('kendoGrid').dataSource.data([]);
    }

    this.ConvocatoriaServirJS.prototype.inicializarGrid = function () {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'ConvocatoriaServir/ListarConvocatorias',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Estado = $("#ddlEstado_busqueda").data("kendoDropDownList").value();
                        data_param.IdOrgano = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        data_param.NroCAS = $("#txtProceso_busqueda").val();
                        data_param.NombreCargo = $("#txtCargo_busqueda").val();
                        data_param.IdTipo = 3;

                        data_param.IdConvocatoria = 0; //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                    id: "IdConvocatoria"
                }
            },
            group: [
                {   field: "AñoServir", aggregate: "count", dir: "desc" },
            ],
            sort: 
                { field: "AñoServir", dir: "desc" }
        });

        this.$grid = $("#divGrid").kendoGrid({
            toolbar: ["excel", ], 
            excel: {
                fileName: "Listado de Convocatorias.xlsx",
                filterable: false
            },
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: true,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            dataType: 'json',
            dataBound: function () {
                //debugger;
                //alert("#= this.datasource.data.NroConvocatoria #"); //#= myCustomFunction(data[i]) #
                //this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                {
                    field: "NroConvocatoria",
                    title: "Nro DE PROCESO",
                    attributes: { style: "text-align:center;" },
                    width: "30px"                    
                },
                {
                    field: "Organo",
                    title: "ÓRGANO",
                    width: "250px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"                    
                },
                {
                    field: "Dependencia",
                    title: "UNIDAD ORGÁNICA",
                    width: "250px"//,
                    //aggregates: ["count"],
                    //groupHeaderTemplate: "#= value # (Total: #= count#)"
                },
                {
                    field: "NombreCargo",
                    title: "CARGO",
                    width: "250px"
                },
                {
                    field: "CantidadVacantes",
                    title: "VACANTES ",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
                },
                {
                    field: "Meta",
                    title: "META",
                    width: "50px"
                },
                {
                    field: "NroAIRHSP",
                    title: "AIRHSP",
                    width: "50px"
                },
                {
                    field: "IdTipoApertura",
                    title: "Tipo de Apertura",
                    width: "50px",
                    template: function (item) {
                        return (item.IdTipoApertura == 1 ? "ABIERTO" : (item.IdTipoApertura == 2 ? "CERRADO" : ""));
                    }
                },
                {
                    field: "EstadoNombre",
                    title: "ESTADO",
                    attributes: { style: "text-align:center;" },
                    width: "50px",
                    aggregates: ["count"],
                    groupHeaderTemplate: "#= value # (Total: #= count#)",
                    template: function (item) {
                        return (item.EstadoNombre == 'PENDIENTE' ? item.strEstadoAprobado
                        : (item.EstadoNombre == 'EN PROCESO' ? "<span style='background-color: RGB(252,248,227); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(247,207,10);'> " + item.EstadoNombre + "</span>"
                        : (item.EstadoNombre == 'CONCLUIDO' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> " + item.EstadoNombre + "</span>"
                                    : (item.EstadoNombre == 'CANCELADO' ? "<span style='background-color: RGB(255,199,199); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,142,139);'> " + item.EstadoNombre + "</span>" //<i class='fa fa-check'></i>  
                        : ''))));
                    }
                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        //if (item.Estado == 1) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalEdicionConvocatoria(\'' + item.IdConvocatoria + '\')">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Detalle del proceso de convocatoria" title="Detalle del proceso de convocatoria"></span>';
                            controles += '</button>';
                        //}
                        
                        return controles;
                    },
                    width: '30px'
                }
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
            ]
        }).data();
    };
    function detailInit(e) {
        var detailRow = e.detailRow;

        debugger;
        detailRow.find(".fecha1").html(e.data.Fecha1);
        detailRow.find(".fecha2").html(e.data.Fecha2);
        detailRow.find(".fecha3").html(e.data.Fecha3);
        detailRow.find(".fecha4").html(e.data.Fecha4);
        detailRow.find(".fecha5").html(e.data.Fecha5);
        detailRow.find(".fecha6").html(e.data.Fecha6);
        detailRow.find(".fecha7").html(e.data.Fecha7);
        detailRow.find(".fecha8").html(e.data.Fecha8);
        detailRow.find(".fecha9").html(e.data.Fecha9);
        
        if (e.data.EsFecha1 == 1) {
            detailRow.find(".divFecha1").css("border-style", "solid");
            detailRow.find(".divFecha1").css("border-color", "#FFC107");
            detailRow.find(".divFecha1").css("border-radius", "10px");
        }
        if (e.data.EsFecha2 == 1) {
            detailRow.find(".divFecha2").css("border-style", "solid");
            detailRow.find(".divFecha2").css("border-color", "#FFC107");
            detailRow.find(".divFecha2").css("border-radius", "10px");
        }
        if (e.data.EsFecha3 == 1) {
            detailRow.find(".divFecha3").css("border-style", "solid");
            detailRow.find(".divFecha3").css("border-color", "#FFC107");
            detailRow.find(".divFecha3").css("border-radius", "10px");
            detailRow.find(".btnEvaluacionCurricular").attr("disabled", false);
            detailRow.find(".btnEvaluacionCurricular").click(function () {
                controlador.abrirModalEvaluacionCurri(e.data.Contrasena);
            })
        }
        if (e.data.EsFecha4 == 1) {
            detailRow.find(".divFecha4").css("border-style", "solid");
            detailRow.find(".divFecha4").css("border-color", "#FFC107");
            detailRow.find(".divFecha4").css("border-radius", "10px");
        }
        if (e.data.EsFecha5 == 1) {
            detailRow.find(".divFecha5").css("border-style", "solid");
            detailRow.find(".divFecha5").css("border-color", "#FFC107");
            detailRow.find(".divFecha5").css("border-radius", "10px");
            detailRow.find(".btnEvaluacionConocimiento").attr("disabled", false);
            detailRow.find(".btnEvaluacionConocimiento").click(function () {
                controlador.abrirModalEvaluacionConocimiento(e.data.Contrasena);
            })
        }
        if (e.data.EsFecha6 == 1) {
            detailRow.find(".divFecha6").css("border-style", "solid");
            detailRow.find(".divFecha6").css("border-color", "#FFC107");
            detailRow.find(".divFecha6").css("border-radius", "10px");
        }
        if (e.data.EsFecha7 == 1) {
            detailRow.find(".divFecha7").css("border-style", "solid");
            detailRow.find(".divFecha7").css("border-color", "#FFC107");
            detailRow.find(".divFecha7").css("border-radius", "10px");
            //detailRow.find(".btnEvaluacionEntrevista").attr("disabled", false);
            //detailRow.find(".btnEvaluacionEntrevista").click(function () {
            //    controlador.abrirModalEvaluacionEntrevista(e.data.IdConvocatoria); //Contrasena
            //})
        }
        if (e.data.EsFecha8 == 1) {
            detailRow.find(".divFecha8").css("border-style", "solid");
            detailRow.find(".divFecha8").css("border-color", "#FFC107");
            detailRow.find(".divFecha8").css("border-radius", "10px");
        }
        if (e.data.EsFecha9 == 1) {
            detailRow.find(".divFecha9").css("border-style", "solid");
            detailRow.find(".divFecha9").css("border-color", "#FFC107");
            detailRow.find(".divFecha9").css("border-radius", "10px");
        }

        detailRow.find(".btnEvaluacionCurricularOtro").attr("disabled", false);
        detailRow.find(".btnEvaluacionCurricularOtro").click(function () {
            controlador.abrirModalAsignarEvaluacion(e.data.IdConvocatoria, 37); //37 ES EL ID DE LA OGRH     e.data.IdDependencia
        })
        detailRow.find(".btnEvaluacionEntrevista").attr("disabled", false);
        detailRow.find(".btnEvaluacionEntrevista").click(function () {
            controlador.abrirModalEvaluacionEntrevista(e.data.IdConvocatoria); //Contrasena
        })

        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".postulantes").kendoGrid({
            toolbar: ["excel"],
            excel: {
                fileName: "Listado de postulantes.xlsx",
                filterable: false
            },
            dataSource: {
                //filter: { field: "EmployeeID", operator: "eq", value: e.data.EmployeeID }
                serverPaging: false,
                serverSorting: true,
                batch: false,
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesServir',
                        type: 'GET',
                        dataType: 'json',
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};

                        if ($operation === "read") {
                            data_param.IdConvocatoria = e.data.IdConvocatoria;
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
                    debugger;

                    detailRow.find(".totalPostulantes").html(this.total());
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
            sortable: true,
            pageable: false,
            columns: [
                {
                    title: '',
                    width: "30px",
                    template: function (item) {
                        return "<img src='data:image/png;base64," + item.Foto + "' style='width: 45px' />";
                    }
                },
                {
                    field: "TipoDocumento",
                    title: "TIPO DOCUMENTO",
                    width: "80px",
                    template: function (item) {
                        var tipo = '';
                        if (item.TipoDocumento == '1') tipo = "DNI";
                        if (item.TipoDocumento == '3') tipo = "CARNET EXTRANJERIA";

                        return tipo;
                    }
                },
                { field: "NroDocumento", title: "Nro DOCUMENTO", width: "80px" },
                { field: "Nombre", title: "NOMBRE", width: "100px" },
                { field: "Paterno", title: "APELLIDO PATERNO", width: "100px" },
                { field: "Materno", title: "APELLIDO MATERNO", width: "100px" },
                { field: "Telefono", title: "TELEFONO", width: "80px" },
                { field: "Celular", title: "CELULAR", width: "80px" },
                { field: "CorreoElectronico", title: "EMAIL", width: "100px" },
                {
                    field: "FFAA",
                    title: "PERTENECIO A LAS FF.AA.",
                    width: "50px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = 'NO';
                        if (item.FFAA == '1') tipo = "SI";
                        
                        return tipo;
                    }
                },
                {
                    field: "Discapacidad",
                    title: "PRESENTA DISCAPACIDAD",
                    width: "50px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var tipo = 'NO';
                        if (item.Discapacidad == '1') tipo = "SI";

                        return tipo;
                    }
                },
                { field: "FechaModificacion", attributes: { style: "text-align:center;" }, title: "FECHA POSTULACION", width: "100px" }
            ]
        }); //.data()
    }

    this.ConvocatoriaServirJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    function CargarFormularioConvocatoria(id) {
        var data_param = new FormData();
        data_param.append('id', id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Bases/ObtenerBasesPerfilesConvocatoriaPorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                $("#hdIdPerfil").val(res.iCodPerfil);
                $("#txtNroConvocatoria").val(res.strNroCAS);
                //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value(res.TipoDocumento);
                $("#txtCantidadConvocatoria").val(res.iCantPersonalRequerido);
                $("#txtRemuneracionConvocatoria").val(res.decRemuneracion);
                $("#txtCargoConvocatoria").val(res.strNombrePuesto);
                $("#txtMetaConvocatoria").val(res.strMeta);

                //$("#spCronograma1").text(kendo.toString(kendo.parseDate(res.dFechaDesdePubMIDIS), 'dd/MM/yyyy') + ' - ' + kendo.toString(kendo.parseDate(res.dFechaHastaPubMIDIS), 'dd/MM/yyyy'));
                //$("#spCronograma2").text(kendo.toString(kendo.parseDate(res.dFechaRegCVPostulante), 'dd/MM/yyyy'));
                //$("#spCronograma3").text(kendo.toString(kendo.parseDate(res.dFechaDesdeEvaCV), 'dd/MM/yyyy') + ' - ' + kendo.toString(kendo.parseDate(res.dFechaDesdeEvaCV), 'dd/MM/yyyy'));
                //$("#spCronograma4").text(kendo.toString(kendo.parseDate(res.dFechaPubResultadoMIDIS), 'dd/MM/yyyy'));
                //$("#spCronograma5").text(kendo.toString(kendo.parseDate(res.dFechaDesdeEntrevista), 'dd/MM/yyyy') + ' - ' + kendo.toString(kendo.parseDate(res.dFechaDesdeEntrevista), 'dd/MM/yyyy'));
                //$("#spCronograma6").text(kendo.toString(kendo.parseDate(res.dFechaPubResultadoFinalMIDIS), 'dd/MM/yyyy'));
                //$("#spCronograma7").text(kendo.toString(kendo.parseDate(res.dFechaDesdeSuscripcionContrato), 'dd/MM/yyyy') + ' - ' + kendo.toString(kendo.parseDate(res.dFechaHastaSuscripcionContrato), 'dd/MM/yyyy'));
                
                
                PDFObject.embed(res.strNombreArchivo, "#app");
            },
            error: function (res) {
                debugger;
                controladorApp.notificarMensajeDeAlerta(res); //[0].responseText);
            }
        });
    }

    this.ConvocatoriaServirJS.prototype.abrirModalRegistroConvocatoria = function (id) {
        var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
        
        LimpiarModalRegistroConvocatoria();

        if (id == 0) {           
            //$("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('01');
            
            $('#tab2').removeClass('in active');
            $('#tab1').addClass('in active');
            $('#liTab2').removeClass('active');
            $('#liTab1').addClass('active');
            
            //controlador.CargarFormularioCuentasBancarias(-1);
            //controlador.CargarFormularioOrdenesServicio(-1);
            //$('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
            //$("#txtPersonaCargo").attr("required", true);
            //$("#txtRUC").removeAttr("required");
            
            //if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");
            //if ($("#btnGuardarAbastecimiento") != null) $("#btnGuardarAbastecimiento").text("Guardar");
            //if ($("#btnGuardarContacto") != null) $("#btnGuardarContacto").text("Guardar");

            modal.title("Ingresar Proceso de Convocatoria SERVIR");
            //modal.center();
            modal.open();
        }
        //else {
        //    $("#hdIdConvocatoria").val(id);

        //    $("#_tab4").attr("data-toggle", "tab");
        //    $('#_tab4').prop("onclick", null).off("click");

        //    controlador.CargarFormularioTrabajador(id);
        //    controlador.CargarFormularioCuentasBancarias(id);
        //    controlador.CargarFormularioOrdenesServicio(id);

        //    if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");
        //    if ($("#btnGuardarAbastecimiento") != null) $("#btnGuardarAbastecimiento").text("Actualizar");
        //    if ($("#btnGuardarContacto") != null) $("#btnGuardarContacto").text("Actualizar");

        //    modal.title("Actualizar Trabajador");
        //    modal.open();
        //}
    }
    this.ConvocatoriaServirJS.prototype.abrirModalEdicionConvocatoria = function (id) {
        var modal = $('#divModalEdicionConvocatoria').data('kendoWindow');
        
        LimpiarModalEdicionConvocatoria();

        if (id != 0) {           
            $("#hdIdConvocatoria").val(id);
            var data_param = new FormData();
            data_param.append('IdConvocatoria', id);

            $.ajax({
                url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerServirParaEditar',
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: data_param,
                success: function (res) {
                    debugger;
                    $("#txtDependenciaE").val(res.Dependencia);
                    $("#txtNroConvocatoriaE").val(res.NroConvocatoria);
                    $("#txtCantidadConvocatoriaE").val(res.CantidadVacantes);
                    $("#txtRemuneracionConvocatoriaE").val(res.Remuneracion);
                    $("#txtCargoConvocatoriaE").val(res.NombreCargo);
                    $("#txtAIRHSPConvocatoriaE").val(res.NroAIRHSP);
                    $("#txtMetaConvocatoriaE").val(res.Meta);

                    if (res.IdTieneExamenConoc == 1) {
                        $("#liTab3E").removeClass('disabled');

                        $("#liTab3E").attr("data-toggle", "tab");
                        $('#liTab3E').prop("onclick", null).off("click");
                    }
                    else {
                        $("#liTab3E").addClass('disabled');

                        $('#liTab3E').unbind();
                        $("#liTab3E").removeAttr("data-toggle");
                        $('#liTab3E').click(function () {
                            controladorApp.notificarMensajeDeAlerta('Este proceso de convocatoria no tiene evaluación de conocimiento');
                        })
                        $('#liTab3E').removeClass('in active');
                        $('#liTab3E').removeClass('active');
                    }   

                    var data_param2 = {};
                    data_param2.IdConvocatoria = id;
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarConvocatoriaServirComite',
                        type: 'GET',
                        dataType: 'json',
                        //contentType: false,
                        //processData: false,
                        data: data_param2,
                        success: function (resul) {
                            debugger;
                            if (resul != null && resul.length > 0) {
                                resul.forEach(function (item) {
                                    if (item.IdMiembro == 1) {
                                        if (item.IdTitular == 1) {
                                            $("#hdComiteDependencia1E").val(item.IdDependencia);
                                            $("#txtComiteDependencia1E").val(item.NombreDependencia);
                                            $("#ddlComiteMiembro1TE").kendoDropDownList({
                                                autoBind: true,
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

                                                            data_param.IdDependencia = item.IdDependencia;
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

                                            $('#ddlComiteMiembro1TE').data("kendoDropDownList").value(item.IdTrabajador);
                                        }

                                        if (item.IdTitular == 0) {
                                            $("#ddlComiteMiembro1SE").kendoDropDownList({
                                                autoBind: true,
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

                                                            data_param.IdDependencia = item.IdDependencia;
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

                                            $('#ddlComiteMiembro1SE').data("kendoDropDownList").value(item.IdTrabajador);
                                        }
                                    }

                                    if (item.IdMiembro == 2) {
                                        if (item.IdTitular == 1) {
                                            $("#hdComiteDependencia2E").val(item.IdDependencia);
                                            $("#txtComiteDependencia2E").val(item.NombreDependencia);
                                            $("#ddlComiteMiembro2TE").kendoDropDownList({
                                                autoBind: true,
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

                                                            data_param.IdDependencia = item.IdDependencia;
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

                                            $('#ddlComiteMiembro2TE').data("kendoDropDownList").value(item.IdTrabajador);
                                        }

                                        if (item.IdTitular == 0) {
                                            $("#ddlComiteMiembro2SE").kendoDropDownList({
                                                autoBind: true,
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

                                                            data_param.IdDependencia = item.IdDependencia;
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

                                            $('#ddlComiteMiembro2SE').data("kendoDropDownList").value(item.IdTrabajador);
                                        }
                                    }

                                    if (item.IdMiembro == 3) {
                                        if (item.NombreDependencia != '') {
                                            $('#divMiembro2').show();
                                            if (item.IdTitular == 1) {
                                                $("#hdComiteDependencia3E").val(item.IdDependencia);
                                                $("#txtComiteDependencia3E").val(item.NombreDependencia);
                                                $("#ddlComiteMiembro3TE").kendoDropDownList({
                                                    autoBind: true,
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

                                                                data_param.IdDependencia = item.IdDependencia;
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

                                                $('#ddlComiteMiembro3TE').data("kendoDropDownList").value(item.IdTrabajador);
                                            }

                                            if (item.IdTitular == 0) {
                                                $("#ddlComiteMiembro3SE").kendoDropDownList({
                                                    autoBind: true,
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

                                                                data_param.IdDependencia = item.IdDependencia;
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

                                                $('#ddlComiteMiembro3SE').data("kendoDropDownList").value(item.IdTrabajador);
                                            }
                                        }
                                        else {
                                            $("#hdComiteDependencia3E").val('0');
                                            $('#divMiembro2').hide();
                                        }
                                    }
                                })
                            }

                            $("#txtDependenciaE").val(res.Dependencia);
                            $("#txtNroConvocatoriaE").val(res.NroConvocatoria);
                            $("#txtCantidadConvocatoriaE").val(res.CantidadVacantes);
                            $("#txtRemuneracionConvocatoriaE").val(res.Remuneracion);
                            $("#txtCargoConvocatoriaE").val(res.NombreCargo);
                            $("#txtAIRHSPConvocatoriaE").val(res.NroAIRHSP);
                            $("#txtMetaConvocatoriaE").val(res.Meta);

                            if (res.IdTieneRequerimiento == 1) {
                                $("#btnActualDocRequerimientoE").show();
                                $("#btnActualDocRequerimientoE").attr("href", controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoServir/?idConvocatoria=' + res.IdConvocatoria + '&idTipo=1');
                                $("#hdDocRequerimientoE").removeAttr("required");
                            }
                            else
                                $("#hdDocRequerimientoE").attr("required", true);
                            if (res.IdTieneCertificacion == 1) {
                                $("#btnActualDocCertificacionE").show();
                                $("#btnActualDocCertificacionE").attr("href", controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoServir/?idConvocatoria=' + res.IdConvocatoria + '&idTipo=2');
                                $("#hdDocCertificacionE").removeAttr("required");
                            }
                            else
                                $("#hdDocCertificacionE").attr("required", true);
                            if (res.IdTieneComite == 1) {
                                $("#btnActualDocComiteE").show();
                                $("#btnActualDocComiteE").attr("href", controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoServir/?idConvocatoria=' + res.IdConvocatoria + '&idTipo=3');
                                $("#hdDocComiteE").removeAttr("required");
                            }
                            else
                                $("#hdDocComiteE").attr("required", true);
                            //if (res.IdTieneActaCurri == 1) {
                            //    $("#btnActaEvaluacionCurriE").show();
                            //    $("#btnActaEvaluacionCurriE").attr("href", controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivo/?idConvocatoria=' + res.IdConvocatoria + '&idTipo=10');
                            //}
                            //else
                            //    $("#btnActaEvaluacionCurriE").hide();

                            //ListarConvocatoriaComite

                            var panelConvocatoria = $("#pnlConvocatoria").data("kendoPanelBar");
                            panelConvocatoria.clearSelection();
                            panelConvocatoria.expand($("#liTab1E"));
                            panelConvocatoria.select($("#liTab1E"));

                            modal.title("Detalle del Proceso de Convocatoria");
                            //modal.center();
                            modal.open(); //.center();
                        },
                        error: function (res) {
                            debugger;
                        }
                    });
                },
                error: function (res) {
                    debugger;
                }
            });
        }

        this.$dataSourceCurri = [];
        this.$dataSourceCurri = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesServirEvaluacionCurri',
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
                $("#lblTotalCurri").html(this.total());
            },
            schema: {
                total: function (response) {
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
        });
        this.$grid = $("#divGridCurricular").kendoGrid({
            toolbar: ["excel", ], 
            excel: {
                fileName: "Evaluacion Curricular.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceCurri,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            dataType: 'json',
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
                            //editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "PuntajeBonifFormacion",
                            title: "BONIFICACIÓN",
                            attributes: { style: "text-align:center" },
                            //format: "{0:P}",
                            width: "50px"//,
                            //editor: controlador.Bonifica3DropDownEditor,
                            //template: "#=CumpleBonifica3.Nombre#"
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
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            //editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleExperienciaGen.Nombre#"
                        }
                    ]
                },
                {
                    title: "EXP. ESPECÍFICA",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoExperienciaEsp",
                            title: "CUMPLE",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            //editor: controlador.CumpleDropDownEditor,
                            template: "#=CumpleExperienciaEsp.Nombre#"
                        },
                        {
                            field: "PuntajeBonifExperienciaEsp",
                            title: "BONIFICACIÓN",
                            attributes: { style: "text-align:center" },
                            //format: "{0:P}",
                            width: "50px"//,
                            //editor: controlador.Bonifica2DropDownEditor,
                            //template: "#=CumpleBonifica2.Nombre#"
                        }
                    ]
                },
                {
                    field: "AptoCapacitacion",
                    title: "PROGRAMAS DE<br>ESPECIALIZACIÓN,<br>DIPLOMADO, CURSO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: "#=CumpleCapacitacion.Nombre#"
                },
                {
                    field: "AptoSanciones",
                    title: "HABILITADO PARA<br>TRABAJAR CON<br>EL ESTADO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
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
                            template: "#=CumpleFFAA.Nombre#"
                        },
                        {
                            field: "BonifDiscapacidad",
                            title: "DISCAPACIDAD",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: "#=CumpleDiscapacidad.Nombre#"
                        },
                        {
                            field: "BonifDeporte",
                            title: "DEPORTISTA<br>CALIFICADO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: "#=CumpleDeportista.Nombre#"
                        }
                    ]
                },                
                {
                    field: "PuntajeTotal",
                    title: "PUNTAJE<br>TOTAL",
                    attributes: { style: "text-align:center" },
                    width: "50px"
                },
                {
                    field: "AptoTotal",
                    title: "APTO /<br>NO APTO",
                    attributes: { style: "text-align:center" },
                    width: "50px",
                    template: function (item) {
                        return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    }//,
                    //editable: true
                },
                {
                    field: "Observacion",
                    title: "OBSERVACIÓN",
                    //attributes: { style: "text-align:center" },
                    width: "100px"
                    //template: function (item) {
                    //    return (item.AptoTotal == '0' ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                    //        : (item.AptoTotal == '1' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                    //            : ''));
                    //}//,
                    //editable: true
                }
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
            ]
        }).data();

        this.$dataSourceConoc = [];
        this.$dataSourceConoc = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesServirEvaluacionConocimiento',
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
                $("#lblTotalConoc").html(this.total());

                if (e.items.length > 0) {
                    if (e.action == "itemchange" && (e.field == "AptoFormacion" ||
                        e.field == "AptoCapacitacion" ||
                        e.field == "AptoExperienciaGen" ||
                        e.field == "AptoExperienciaEsp" ||
                        e.field == "PuntajeBonifFormacion" ||
                        e.field == "PuntajeBonifExperienciaEsp" ||
                        e.field == "AptoDDJJ" ||
                        e.field == "AptoSanciones" ||
                        e.field == "BonifDeporte")) {
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
                        data_param.append('BonifDeporte', e.items[0].BonifDeporte);
                        data_param.append('IdUsuarioModificacion', $("#hdIdTrabajador").val());

                        $.ajax({
                            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ActualizarServirEvaluacionCurri',
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
        this.$grid = $("#divGridConocimientos").kendoGrid({
            toolbar: ["excel",],
            excel: {
                fileName: "Evaluacion Conocimientos.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceConoc,
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
                            width: "100px"
                        },
                        {
                            field: "HoraEntrevista",
                            title: "HORA",
                            attributes: { style: "text-align:center" },
                            width: "100px"
                        }
                    ]
                },
                {
                    field: "Observacion",
                    title: "OBSERVACIÓN",
                    attributes: { style: "text-align:left" },
                    width: "400px"
                    //editable: true
                },
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
            ]
        }).data();

        this.$dataSourceEntre = [];
        this.$dataSourceEntre = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesEntrevistaPersonalXXXXXXX',
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
            //filter: [
            //    { field: "IdTrabajador", operator: "equals", value: $("#hdIdTrabajador").val() }
            //],
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
            },
            group: {
                field: "NombreEvaluador", aggregates: [
                    { field: "PuntajeTotal", aggregate: "average" },
                    { field: "NombreEvaluador", aggregate: "count" }
                ]
            },
            aggregate: [
                { field: "PuntajeTotal", aggregate: "average" }
            ]
        });
        this.$grid = $("#divGridEntrevistas").kendoGrid({
            toolbar: ["excel",],
            excel: {
                fileName: "Entrevistas Personales.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceEntre,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,
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
                    field: "NombreEvaluador",
                    width: "0px",
                    template: function (item) {
                        return item.NombreEvaluador;
                    },
                    hidden: true,
                    aggregates: ["count"],
                    groupHeaderTemplate: "Evaluador: #= value # "
                },
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
                            return 'NSP';
                        else
                            return item.PuntajeTotal;
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
                            width: "100px"
                            //template: "#=CumpleFormacion.Nombre#"
                        },
                        {
                            field: "HoraEntrevista",
                            title: "HORA",
                            attributes: { style: "text-align:center" },
                            editor: controlador.HoraEntrevistaEditor,
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
                    title: 'ACTA DE<BR>EVALUACIÓN',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneActa == 1) {
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoEntrevista/?idEntrevista=' + item.IdEvaluacion + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span> DESCARGAR </a>';
                        }
                        else {
                            if (item.IdTieneActa == 0 && item.PuntajeTotal > 0) 
                                controles += "<span style='background-color: RGB(255,255,204); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,235,59);'> PENDIENTE</span>"
                            else 
                                controles += "<span style='padding: 2px; width: 95%; float: right; '> --- </span>"
                        }
                        return controles;
                    },
                    width: '30px'
                },
                {
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if ((item.IdTieneActa == 1) || (item.IdTieneActa == 0 && item.PuntajeTotal > 0) || (item.IdPresento == 0)) {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.limpiarEvaluacionEntrevista(\'' + item.IdEvaluacion + '\')">'; //GenerarFormatoContrato
                            controles += '<span class="glyphicon glyphicon-repeat" aria-hidden="true" data-uib-tooltip="Limpiar la evaluación ingresada" title="Limpiar la evaluación ingresada"></span>';
                            controles += ' LIMPIAR </button>';
                        }

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

        this.$dataSourceTotales = [];
        this.$dataSourceTotales = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarPostulantesResultadosTotalesXXXXXX',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation == "read") {
                        data_param.IdConvocatoria = id;
                        data_param.Estado = 0;
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
                $("#lblTotalTot").html(this.total());
            },
            schema: {
                total: function (response) {
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdPostulacion"
                }
            }
        });
        this.$gridTotales = $("#divGridTotales").kendoGrid({
            toolbar: ["excel"],
            excel: {
                fileName: "Resultados finales.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceTotales,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            dataType: 'json',
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
                    title: "EVALUACIÓN CURRICULAR",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoCurricular",
                            title: "APTO / NO APTO",
                            attributes: { style: "text-align:center" }, //background-color: RGB(252, 248, 227); padding: 3px 5px 3px 5px
                            width: "50px",
                            template: function (item) {
                                return (item.AptoCurricular == '0' ? "<span style='background-color: RGB(247,187,187); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.AptoCurricular == '1' ? "<span style='background-color: RGB(199,249,215); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : ''));
                            }
                        },
                        {
                            field: "PuntajeCurricular",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            width: "50px"
                        }
                    ]
                },
                {
                    title: "EVALUACIÓN DE CONOCIMIENTOS",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoConocimiento",
                            title: "APTO / NO APTO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: function (item) {
                                return (item.AptoConocimiento == '0' ? "<span style='background-color: RGB(247,187,187); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.AptoConocimiento == '1' ? "<span style='background-color: RGB(199,249,215); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : ''));
                            }
                        },
                        {
                            field: "PuntajeConocimiento",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            width: "50px"
                            //editable: true
                        }
                    ]
                },
                {
                    title: "ENTREVISTA PERSONAL",
                    attributes: { style: "text-align:center" },
                    columns: [
                        {
                            field: "AptoEntrevista",
                            title: "APTO / NO APTO",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: function (item) {
                                return (item.AptoEntrevista == '0' ? "<span style='background-color: RGB(247,187,187); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                                    : (item.AptoEntrevista == '1' ? "<span style='background-color: RGB(199,249,215); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                        : ''));
                            }
                        },
                        {
                            field: "PuntajeEntrevista",
                            title: "PUNTAJE",
                            attributes: { style: "text-align:center" },
                            width: "50px",
                            template: function (item) {
                                if (item.PresentoEntrevista == 0)
                                    return 'NSP';
                                else
                                    return item.PuntajeEntrevista;
                            }
                        }
                    ]
                },
                {
                    field: "BonifFFAA",
                    title: "BONIFICACIÓN<br>POR FFAA",
                    attributes: { style: "text-align:center" },
                    width: "100px",
                    template: function (item) {
                        return (item.BonifFFAA == '0' ? "<span style='background-color: RGB(247,187,187); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.BonifFFAA == '1' ? "<span style='background-color: RGB(199,249,215); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    }
                },
                {
                    field: "BonifDiscapacidad",
                    title: "BONIFICACIÓN<br>POR DISCAPACIDAD",
                    attributes: { style: "text-align:center" },
                    width: "100px",
                    template: function (item) {
                        return (item.BonifDiscapacidad == '0' ? "<span style='background-color: RGB(247,187,187); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> NO </span>"
                            : (item.BonifDiscapacidad == '1' ? "<span style='background-color: RGB(199,249,215); padding: 5px 15px; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> SI </span>"
                                : ''));
                    }
                },
                {
                    field: "PuntajeTotal",
                    title: "PUNTAJE TOTAL",
                    attributes: { style: "text-align:center" },
                    width: "100px"
                },
                {
                    field: "Posicion",
                    title: "RESULTADO FINAL",
                    attributes: { style: "text-align:center" },
                    width: "100px",
                    template: function (item) {
                        var aux = '<button type="button" class="btn btn-info btn-xs" onclick="controlador.declararAccesitarioGanador(\'' + item.IdPostulacion + '\',\'' + item.IdPostulante + '\',\'' + item.IdConvocatoria + '\')" style="margin-top: 3px;" title="Declarar como ganador en caso desista el ganador">'; 
                        aux += '<span class="glyphicon glyphicon-open" aria-hidden="true" data-uib-tooltip="Declarar como ganador en caso desista el ganador" title="Declarar como ganador en caso desista el ganador" ></span>';
                        aux += ' DECLARAR GANADOR </button>';
                        return (item.AptoGanador == 1 && item.Posicion <= item.Vacantes ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i> GANADOR(A) </span>"
                            : (item.AptoGanador == 1 && item.Posicion - 1 == item.Vacantes ? "<span style='background-color: RGB(247,187,187); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(240,84,84);'> ACCESITARIO(A) </span>" + aux
                                : '--'));
                    }
                }
            ]
        }).data();


        this.$dataSourceComunicacion = [];
        this.$dataSourceComunicacion = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ListarConvocatoriaDocumento',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation == "read") {
                        data_param.IdConvocatoria = id;
                        data_param.IdTipo = 3;
                        data_param.Estado = 0;
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
            requestEnd: function (e) {
                switch (e.type) {
                    case "create": case "update": case "destroy":
                        var grilla = $('#divGridComunicacion').data("kendoGrid");
                        grilla.dataSource._sort = undefined;
                        grilla.dataSource.page(1);

                        break;
                }
            },
            change: function (e) {
                $("#lblTotalComu").html(this.total());
            },
            schema: {
                total: function (response) {
                    return response.length; // TotalDeRegistros;
                },
                model: {
                    id: "IdConvocatoriaDocumento"
                }
            }
        });
        this.$gridComunicacion = $("#divGridComunicacion").kendoGrid({
            toolbar: ["excel"],
            excel: {
                fileName: "Listado de comunicaciones de la convocatoria.xlsx",
                filterable: false
            },
            dataSource: this.$dataSourceComunicacion,
            autoBind: true,
            selectable: false,
            scrollable: false,
            sortable: false,
            pageable: false,
            dataType: 'json',
            columns: [
                {
                    field: "NombreDocumento",
                    title: "TIPO DE DOCUMENTO",
                    width: "100px"
                },
                {
                    field: "TipoDocumento",
                    title: "ÁMBITO",
                    width: "100px"
                },
                {
                    field: "FechaRegistro",
                    title: "FECHA DE REGISTRO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        return kendo.toString(kendo.parseDate(item.FechaRegistro), 'dd/MM/yyyy HH:mm');
                    }
                },
                {
                    //DESCARGAR ARCHIVO ADJUNTO
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoServirPorId/?idConvocatoriaDoc=' + item.IdConvocatoriaDocumento + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar comunicado"></span></a>';
                        
                        return controles;
                    },
                    width: '30px'
                },
                {
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        if (item.IdTipoDocumento >= 20) {
                            controles += '<button type="button" class="btn btn-danger btn-xs" onclick="controlador.EliminarComunicado(\'' + item.IdConvocatoriaDocumento + '\')">';
                            controles += '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-uib-tooltip="Eliminar" title="Eliminar comunicado"></span>';
                            controles += '</button>';
                        }

                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();

    }
    this.ConvocatoriaServirJS.prototype.abrirModalFormato = function () {
        $("#fileFormato").data("kendoUpload").clearAllFiles();
        //$('#ddlTipoFormato').data("kendoDropDownList").value('0');

        var modal = $('#divModalFormato').data('kendoWindow');

        modal.center().open();
        //modal.open();
    }
    this.ConvocatoriaServirJS.prototype.abrirModalFormatoEnt = function (id, postulante, examen) {
        $("#fileFormatoEnt").data("kendoUpload").clearAllFiles();
        $('#hdIdEvaluacion').val(id);
        $('#hdIdExamen').val(examen);

        var modal = $('#divModalFormatoEnt').data('kendoWindow');

        modal.open();
    }

    this.ConvocatoriaServirJS.prototype.cerrarModalRegistroConvocatoria = function () {
        var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
        modal.close();
    }
    this.ConvocatoriaServirJS.prototype.cerrarModalEdicionConvocatoria = function () {
        var modal = $('#divModalEdicionConvocatoria').data('kendoWindow');
        modal.close();
    }
    this.ConvocatoriaServirJS.prototype.cerrarModalEvaluacionCurri = function () {
        var modal = $('#divModalEvaluacionCurri').data('kendoWindow');
        modal.close();
    }
    this.ConvocatoriaServirJS.prototype.cerrarModalAsignarEvaluacion = function () {
        var modal = $('#divModalAsignarEvaluacion').data('kendoWindow');
        modal.close();
    }

    this.ConvocatoriaServirJS.prototype.TipoDocumentoDropDownEditor = function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: "Nombre",
                dataValueField: "IdDocumento",
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

    this.ConvocatoriaServirJS.prototype.EstadoDropDownEditor = function (container, options) {
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

    this.ConvocatoriaServirJS.prototype.CargarFormularioOrdenesServicio = function (id) {
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
     

    this.ConvocatoriaServirJS.prototype.NombreOrdenEditor = function (container, options) {
        $('<textarea required name="' + options.field + '" style="width: ' + (container.width() + 30) + 'px;height:' + (container.height() + 30) + 'px" />')
            .appendTo(container);
    };
    this.ConvocatoriaServirJS.prototype.FechaInicioEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaInicio">')
            .appendTo(container);
    };
    this.ConvocatoriaServirJS.prototype.FechaFinEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaFin">')
            .appendTo(container);
    };
    this.ConvocatoriaServirJS.prototype.agregarConvocatoria = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        
        if (frmRegistroConvocatoria.validate()) {
            var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();
            
            data_param.append('IdBase', $("#ddlBases").data("kendoDropDownList").value());
            data_param.append('IdPerfil', $("#hdIdPerfil").val());
            data_param.append('IdDependencia', $("#ddlDependencia").data("kendoDropDownList").value());
            data_param.append('NroAIRHSP', $("#txtAIRHSPConvocatoria").val());
            data_param.append('Meta', $("#txtMetaConvocatoria").val());
            data_param.append('IdTipoApertura', $("#ddlTipoConvocatoria").data("kendoDropDownList").value());
            //data_param.append('ResponsableCurricular', $("#ddlResponsableCurricular").data("kendoDropDownList").value());
            data_param.append('Estado', 1);
            data_param.append('IdTipo', 3); //SERVIR

            var existeFile = false;
            var upload1 = $("#fileDocCertificacion").getKendoUpload();
            var file1 = upload1.getFiles();
            if (file1.length == 0) {
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el Anexo 04: Acta de instalación del comité de selección (Formato PDF)');
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
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el Anexo 01: Formato de requerimiento de personal CAS (Formato PDF)');
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
                controladorApp.notificarMensajeDeAlerta('Debe seleccionar el Anexo 03: Formato para conformación del comité de selección (Formato PDF)');
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

            //alert($("#ddlComiteDependencia3").data("kendoDropDownList").value());
            //if ($("#ddlComiteDependencia3").data("kendoDropDownList").value() != '0') {
                data_param.append('IdComiteDependencia3', $("#ddlComiteDependencia3").data("kendoDropDownList").value());
                data_param.append('IdComiteMiembro3T', $("#ddlComiteMiembro3T").data("kendoDropDownList").value());
                data_param.append('IdComiteMiembro3S', $("#ddlComiteMiembro3S").data("kendoDropDownList").value());
            //}

            var d1 = $("#ddlComiteDependencia1").data("kendoDropDownList").value();
            var d2 = $("#ddlComiteDependencia2").data("kendoDropDownList").value();
            var d3 = $("#ddlComiteDependencia3").data("kendoDropDownList").value();

            //if (d1 == d2 || d1 == d3 || d2 == d3) {
            //    controladorApp.notificarMensajeDeAlerta('No puede repetir la misma dependencia como miembro del comité de selección');
            //    return false;
            //}
            if ($("#ddlComiteMiembro1T").data("kendoDropDownList").value() == $("#ddlComiteMiembro1S").data("kendoDropDownList").value()) {
                controladorApp.notificarMensajeDeAlerta('PRESIDENTE: Los miembros titular y suplente no pueden ser la misma persona');
                return false;
            }
            if ($("#ddlComiteMiembro2T").data("kendoDropDownList").value() == $("#ddlComiteMiembro2S").data("kendoDropDownList").value()) {
                controladorApp.notificarMensajeDeAlerta('MIEMBRO 1: Los miembros titular y suplente no pueden ser la misma persona');
                return false;
            }
            debugger;
            if ($("#ddlComiteDependencia3").data("kendoDropDownList").value() != '') {
                if ($("#ddlComiteMiembro3T").data("kendoDropDownList").value() == $("#ddlComiteMiembro3S").data("kendoDropDownList").value()) {
                    controladorApp.notificarMensajeDeAlerta('MIEMBRO 2: Los miembros titular y suplente no pueden ser la misma persona');
                    return false;
                }
            }

            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de ingresar la convocatoria SERVIR?.</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación la convocatoria automaticamente será publicada</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'ConvocatoriaServir/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Convocatoria registrada correctamente");

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
    this.ConvocatoriaServirJS.prototype.actualizarConvocatoria = function (e) {
        e.preventDefault();
        var metodo = 'Actualizar';

        if (frmEdicionConvocatoria.validate()) {
            var modal = $('#divModalEdicionConvocatoria').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();

            //data_param.append('IdBase', $("#ddlBases").data("kendoDropDownList").value());
            //data_param.append('IdPerfil', $("#hdIdPerfil").val());
            data_param.append('IdConvocatoria', $("#hdIdConvocatoria").val());
            data_param.append('NroAIRHSP', $("#txtAIRHSPConvocatoriaE").val());
            data_param.append('Meta', $("#txtMetaConvocatoriaE").val());
            data_param.append('Estado', 1);

            var existeFile = false;
            var upload1 = $("#fileDocCertificacionE").getKendoUpload();
            var file1 = upload1.getFiles();
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
            
            var upload2 = $("#fileDocRequerimientoE").getKendoUpload();
            var file2 = upload2.getFiles();
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
            
            var upload3 = $("#fileDocComiteE").getKendoUpload();
            var file3 = upload3.getFiles();
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
            
            data_param.append('IdComiteDependencia1', $("#hdComiteDependencia1E").val());
            data_param.append('IdComiteMiembro1T', $("#ddlComiteMiembro1TE").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro1S', $("#ddlComiteMiembro1SE").data("kendoDropDownList").value());
            data_param.append('IdComiteDependencia2', $("#hdComiteDependencia2E").val());
            data_param.append('IdComiteMiembro2T', $("#ddlComiteMiembro2TE").data("kendoDropDownList").value());
            data_param.append('IdComiteMiembro2S', $("#ddlComiteMiembro2SE").data("kendoDropDownList").value());
            if ($("#hdComiteDependencia3E").val() != '0') {
                data_param.append('IdComiteDependencia3', $("#hdComiteDependencia3E").val());
                data_param.append('IdComiteMiembro3T', $("#ddlComiteMiembro3TE").data("kendoDropDownList").value());
                data_param.append('IdComiteMiembro3S', $("#ddlComiteMiembro3SE").data("kendoDropDownList").value());
            }
            else {
                data_param.append('IdComiteDependencia3', $("#hdComiteDependencia3E").val());
                data_param.append('IdComiteMiembro3T', '0');
                data_param.append('IdComiteMiembro3S', '0');
            }

            if ($("#ddlComiteMiembro1TE").data("kendoDropDownList").value() == $("#ddlComiteMiembro1SE").data("kendoDropDownList").value()) {
                controladorApp.notificarMensajeDeAlerta('PRESIDENTE: Los miembros titular y suplente no pueden ser la misma persona');
                return false;
            }
            if ($("#ddlComiteMiembro2TE").data("kendoDropDownList").value() == $("#ddlComiteMiembro2SE").data("kendoDropDownList").value()) {
                controladorApp.notificarMensajeDeAlerta('MIEMBRO 1: Los miembros titular y suplente no pueden ser la misma persona');
                return false;
            }
            //debugger;
            //if ($("#hdComiteDependencia3E").val() != '') {
            //    if ($("#ddlComiteMiembro3TE").data("kendoDropDownList").value() == $("#ddlComiteMiembro3SE").data("kendoDropDownList").value()) {
            //        controladorApp.notificarMensajeDeAlerta('MIEMBRO 2: Los miembros titular y suplente no pueden ser la misma persona');
            //        return false;
            //    }
            //}

            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de actualizar la convocatoria?.</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación la convocatoria automaticamente será publicada</div>', 'SI', 'NO'
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
                                controladorApp.notificarMensajeSatisfactorio("Convocatoria actualizada correctamente");

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
    this.ConvocatoriaServirJS.prototype.agregarEvaluacionEntrevista = function (e) {
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
            data_param.append('IdTipoNotificacion', $("#ddlTipoNotificacion").data("kendoDropDownList").value());            
            data_param.append('Meta', $("#txtEnlaceReunion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de notificar el inicio de la entrevista personal a los miembros del comité seleccionados?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación los miembros del comité seleccionados podrán ingresar la evaluación de entrevistas personales del proceso CAS</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/RegistrarConvocatoriaComiteEntrevista',
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
                                controladorApp.notificarMensajeSatisfactorio("Se envió correctamente la notificación");

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
    this.ConvocatoriaServirJS.prototype.asignarEvaluacionCurricular = function (e) {
        e.preventDefault();

        if (frmAsignarEvaluacionValidador.validate()) {
            var modal = $('#divModalAsignarEvaluacion').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            var data_param = new FormData();
            data_param.append('IdConvocatoria', $("#hdIdConvocatoria").val());
            data_param.append('IdComiteMiembro1T', $("#ddlAsignarEvaluacion").data("kendoDropDownList").value());
            
            controladorApp.abrirMensajeDeConfirmacion(
                '<span>¿Está seguro de asignar la evaluación curricular al trabajador seleccionado?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación el trabajador seleccionado podrá realizar la evaluación curricular del proceso CAS</div>', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Convocatoria/AsignarConvocatoriaEvaluacionCurricular',
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
    this.ConvocatoriaServirJS.prototype.agregarResultadoFinalArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormatoEnt').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTipoDocumento', 11);

        var upload1 = $("#fileFormatoEnt").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el acta de resultado final firmado (Formato PDF)');
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

        controladorApp.abrirMensajeDeConfirmacion('¿Desea ingresar el acta de resultado final?', 'SI', 'NO'
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
                            controladorApp.notificarMensajeDeAlerta("El ingreso del acta de resultado final no se pudo realizar");
                        }
                        else {
                            controladorApp.notificarMensajeSatisfactorio("El ingreso del acta de resultado final se actualizó de forma correcta");
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

    this.ConvocatoriaServirJS.prototype.limpiarEvaluacionEntrevista = function (IdEvaluacion) {
        
        var data_param = new FormData();
        //data_param.append('IdEvaluacion', $("#hdIdConvocatoria").val());
        data_param.append('IdEvaluacion', IdEvaluacion);
            
        controladorApp.abrirMensajeDeConfirmacion(
            '<span>¿Está seguro de limpiar la evaluación de entrevista seleccionada ?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación el miembro del comité seleccionado deberá ingresar nuevamente la evaluación de entrevista</div>', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/LimpiarEvaluacionEntrevista',
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
                            controladorApp.notificarMensajeSatisfactorio("Se limpió la evaluación correctamente");

                            //CARGA DE ENTREVISTAS PERSONALES 
                            this.$dataSourceEntre = [];
                            this.$dataSourceEntre = new kendo.data.DataSource({
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
                                //filter: [
                                //    { field: "IdTrabajador", operator: "equals", value: $("#hdIdTrabajador").val() }
                                //],
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
                                },
                                group: {
                                    field: "NombreEvaluador", aggregates: [
                                        { field: "PuntajeTotal", aggregate: "average" },
                                        { field: "NombreEvaluador", aggregate: "count" }
                                    ]
                                },
                                aggregate: [
                                    { field: "PuntajeTotal", aggregate: "average" }
                                ]
                            });
                            this.$grid = $("#divGridEntrevistas").kendoGrid({
                                toolbar: ["excel",],
                                excel: {
                                    fileName: "Entrevistas Personales.xlsx",
                                    filterable: false
                                },
                                dataSource: this.$dataSourceEntre,
                                autoBind: true,
                                selectable: false,
                                scrollable: false,
                                sortable: false,
                                pageable: false,
                                groupable: false,
                                dataType: 'json',
                                columns: [
                                    {
                                        title: '',
                                        field: "NombreEvaluador",
                                        width: "0px",
                                        template: function (item) {
                                            return item.NombreEvaluador;
                                        },
                                        hidden: true,
                                        aggregates: ["count"],
                                        groupHeaderTemplate: "Evaluador: #= value # "
                                    },
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
                                                return 'NSP';
                                            else
                                                return item.PuntajeTotal;
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
                                                width: "100px"
                                                //template: "#=CumpleFormacion.Nombre#"
                                            },
                                            {
                                                field: "HoraEntrevista",
                                                title: "HORA",
                                                attributes: { style: "text-align:center" },
                                                editor: controlador.HoraEntrevistaEditor,
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
                                        title: 'ACTA DE<BR>EVALUACIÓN',
                                        attributes: { style: "text-align:center;" },
                                        template: function (item) {
                                            var controles = "";
                                            if (item.IdTieneActa == 1) {
                                                controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Convocatoria/DescargarArchivoEntrevista/?idEntrevista=' + item.IdEvaluacion + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-file" title="Descargar contrato firmado/aprobado"></span> DESCARGAR </a>';
                                            }
                                            else {
                                                if (item.IdTieneActa == 0 && item.PuntajeTotal > 0)
                                                    controles += "<span style='background-color: RGB(255,255,204); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,235,59);'> PENDIENTE</span>"
                                                else
                                                    controles += "<span style='padding: 2px; width: 95%; float: right; '> --- </span>"
                                            }
                                            return controles;
                                        },
                                        width: '30px'
                                    },
                                    {
                                        title: '',
                                        attributes: { style: "text-align:center;" },
                                        template: function (item) {
                                            var controles = "";
                                            if ((item.IdTieneActa == 1) || (item.IdTieneActa == 0 && item.PuntajeTotal > 0) || (item.IdPresento == 0)) {
                                                controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.limpiarEvaluacionEntrevista(\'' + item.IdEvaluacion + '\')">'; //GenerarFormatoContrato
                                                controles += '<span class="glyphicon glyphicon-repeat" aria-hidden="true" data-uib-tooltip="Limpiar la evaluación ingresada" title="Limpiar la evaluación ingresada"></span>';
                                                controles += ' LIMPIAR </button>';
                                            }

                                            return controles;
                                        },
                                        width: '30px'
                                    }
                                ]
                            }).data();
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }

    this.ConvocatoriaServirJS.prototype.declararAccesitarioGanador = function (IdPostulacion, IdPostulante, IdConvocatoria) {

        var data_param = new FormData();
        //data_param.append('IdEvaluacion', $("#hdIdConvocatoria").val());
        data_param.append('IdPostulacion', IdPostulacion);
        data_param.append('IdPostulante', IdPostulante);
        data_param.append('IdConvocatoria', IdConvocatoria);

        controladorApp.abrirMensajeDeConfirmacion(
            '<span>¿Está seguro de declarar el accesitario seleccionado como ganador del proceso de convocatoria ?</span></br></br><div class="alert alert-warning" style="padding: 5px; border: 1px solid #f7cf0aa0;">Recuerde que al confirmar esta operación el accesitario podrá continuar con el proceso como ganador de la convocatoria</div>', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/DeclararAccesitarioGanador',
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
                            controladorApp.notificarMensajeSatisfactorio("Se actualizó el accesitario correctamente");

                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    }

    this.ConvocatoriaServirJS.prototype.GenerarFormatoContrato = function () {
        var IdContrato = $('#hdIdContrato').val();

        window.open(controladorApp.obtenerRutaBase() + "Contrato/Ficha?idContrato=" + IdContrato, "_blank");
        $("#btnGenerarFicha").css("display", "");
    }
    this.ConvocatoriaServirJS.prototype.agregarConvocatoriaDocumento = function (e) {
        e.preventDefault();
        
        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        data_param.append('IdTipoDocumento', $('#ddlTipoFormato').data("kendoDropDownList").value());

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el documento a publicar (Formato PDF)');
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

        controladorApp.abrirMensajeDeConfirmacion('¿Desea publicar el documento seleccionado ?', 'SI', 'NO'
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
                                controladorApp.notificarMensajeDeAlerta("El documento no se pudo publicar");
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio("El documento se actualizó de forma correcta");
                                modal.close();

                                $('#divGridComunicacion').data("kendoGrid").dataSource.page(1);
                            }
                        },
                        error: function (res) {
                            //alert(res);
                        }
                    });
                }, data_param);
    }
    ////this.ConvocatoriaServirJS.prototype.actualizarTrabajadorContacto = function (e) {
    //    e.preventDefault();
        
    //    if (frmPersonaValidador.validate()) {
    //        var modal = $('#divModalRegistroConvocatoria').data('kendoWindow');
    //        var esValido = true;
    //        var mensajeValidacion = '';

    //        //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
    //        var data_param = new FormData();
    //        if ($("#hdIdEmpleado").val() != 0) 
    //            data_param.append('IdEmpleado', $("#hdIdEmpleado").val());
            
    //        data_param.append('CorreoElectronico', $("#txtPersonaCorreoElectronicoP").val());
    //        data_param.append('Celular', $("#txtTelefonoCelularP").val());
    //        data_param.append('Telefono', $("#txtTelefonoFijoP").val());
    //        data_param.append('CorreoElectronicoLaboral', $("#txtPersonaCorreoElectronico").val());
    //        data_param.append('CelularLaboral', $("#txtTelefonoCelular").val());
    //        data_param.append('TelefonoLaboral', $("#txtTelefonoLaboral").val());
    //        data_param.append('AnexoLaboral', $("#txtTelefonoAnexo").val());
            
    //        controladorApp.abrirMensajeDeConfirmacion(
    //            '¿Está seguro de actualizar el trabajador?', 'SI', 'NO'
    //            , function (arg) {
    //                $.ajax({
    //                    url: controladorApp.obtenerRutaBase() + 'Empleado/GuardarContacto',
    //                    type: 'POST',
    //                    dataType: 'json',
    //                    contentType: false,
    //                    processData: false,
    //                    data: arg,
    //                    success: function (res) {
    //                        debugger;
    //                        if (res.success == 'False') {
    //                            controladorApp.notificarMensajeDeAlerta(res.responseText);
    //                        }
    //                        else {
    //                            if (($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) || ($("#hdPerfil").val() == PERFIL_NOMINA_CONTABILIDAD)) {
    //                                modal.close();
    //                                $('#divGrid').data("kendoGrid").dataSource.page(1);
    //                            }
    //                            else {
    //                                controladorApp.notificarMensajeSatisfactorio("Trabajador registrado correctamente");

    //                                // REFRESCAR INFORMACION DEL TRABAJADOR
    //                                LimpiarModalRegistroConvocatoria();

    //                                $("#hdIdEmpleado").val(res.responseText);

    //                                $("#_tab4").attr("data-toggle", "tab");
    //                                $('#_tab4').prop("onclick", null).off("click");

    //                                controlador.CargarFormularioTrabajador(res.responseText);
    //                                controlador.CargarFormularioCuentasBancarias(res.responseText);

    //                                modal.title("Actualizar Trabajador");
    //                                $('#divGrid').data("kendoGrid").dataSource.page(1);
    //                            }
    //                        }
    //                    },
    //                    error: function (res) {
    //                        //alert(res);
    //                    }
    //                });
    //            }, data_param);
    //    }
    //    else {
    //        controladorApp.notificarMensajeDeAlerta('Completar los campos requeridos');
    //    }

    //}

    this.ConvocatoriaServirJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    }
    this.ConvocatoriaServirJS.prototype.cerrarModalFormatoEnt = function () {
        $('#divModalFormatoEnt').data('kendoWindow').close();
    }

    this.ConvocatoriaServirJS.prototype.inicializarGridConocimientos = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());
        //$.ajax({
        //    url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
        //    type: 'POST',
        //    dataType: 'json',
        //    contentType: false,
        //    processData: false,
        //    data: data_param,
        //    success: function (res) {
        //        debugger;
        //        $("#txtNroConvocatoria").val(res.NroConvocatoria);
        //        $("#txtCantidadConvocatoria").val(res.CantidadVacantes);
        //        $("#txtCargoConvocatoria").val(res.NombreCargo);
        //    },
        //    error: function (res) {
        //        debugger;
        //    }
        //});

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
                $("#lblTotalPos").html(this.total());

                if (e.items.length > 0) {
                    if (e.action == "itemchange" && (e.field == "FechaEntrevista" ||
                        e.field == "HoraEntrevista")) {
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
            },
            filter: [
                { field: "AptoTotal", operator: "equals", value: 1 }
            ]
            //aggregate: [
            //        { field: "NombreCompleto", aggregate: "count" },
            //        { field: "NombreOficina", aggregate: "count" }
            //]
        });
        this.$grid = $("#divGridEntrevista").kendoGrid({
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
                    width: "100px",
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
                    editable: true
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
                }
                //{
                //    field: "Observacion",
                //    title: "OBSERVACIÓN",
                //    attributes: { style: "text-align:left" },
                //    editor: controlador.ObservacionEditor,
                //    width: "400px"
                //    //editable: true
                //}
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
    this.ConvocatoriaServirJS.prototype.inicializarGridCurricular = function () {
        var data_param = new FormData();
        data_param.append('IdConvocatoria', $('#hdIdConvocatoria').val());

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
                $("#lblTotalPos").html(this.total());

                if (e.items.length > 0) {
                    if (e.action == "itemchange" && (e.field == "FechaEntrevista" ||
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
            },
            filter: [
                { field: "AptoTotal", operator: "equals", value: 1 }
            ]
            //aggregate: [
            //        { field: "NombreCompleto", aggregate: "count" },
            //        { field: "NombreOficina", aggregate: "count" }
            //]
        });
        this.$grid = $("#divGridEntrevista").kendoGrid({
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
                    width: "100px",
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
                    editable: true
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
                }
            ],
            editable: true
        }).data();
    };

    this.ConvocatoriaServirJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.ConvocatoriaServirJS.prototype.abrirModalEvaluacionCurri = function (IdConvocatoria) {
        //$("#fileFormato").data("kendoUpload").clearAllFiles();
        //$('#hdIdConvocatoria').val(IdConvocatoria);
        //var data_param = new FormData();
        //data_param.append('IdConvocatoria', IdConvocatoria);

        window.open(controladorApp.obtenerRutaBase() + 'Convocatoria/EvaluacionCurricular/?id=' + IdConvocatoria, '_blank');
        //html = html.Replace("_URLEVALUACION_", ConfigurationManager.AppSettings["SERVER_PATH"].ToString() + "Convocatoria/EvaluacionCurricular/?id=" + HttpUtility.UrlEncode(new Crypto().Encriptar(convocatoria.IdConvocatoria + "|" + trabajador.IdEmpleado + "|" + trabajador.NroDocumento))); //HttpUtility.UrlEncode(
    }
    this.ConvocatoriaServirJS.prototype.abrirModalAsignarEvaluacion = function (IdConvocatoria, IdDependencia) {
        $('#hdIdConvocatoria').val(IdConvocatoria);

        $("#txtNroConvocatoriaCurri").val('');
        $("#txtCargoConvocatoriaCurri").val('');
        $("#txtEnlaceReunion").val('');

        var data_param = new FormData();
        data_param.append('IdConvocatoria', IdConvocatoria);

        var modal = $('#divModalAsignarEvaluacion').data('kendoWindow');

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Convocatoria/ObtenerParaEditar',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;
                $("#txtNroConvocatoriaAsigna").val(res.NroConvocatoria);
                $("#txtCargoConvocatoriaAsigna").val(res.NombreCargo);

                //if (res.IdTieneExamenConoc == 1)
                //    controlador.inicializarGridConocimientos();
                //else
                //    controlador.inicializarGridCurricular();


                modal.open(); //.center();
            },
            error: function (res) {
                debugger;
            }
        });

        $("#ddlAsignarEvaluacion").kendoDropDownList({
            autoBind: true,
            optionLabel: "--Seleccione--",
            dataTextField: "NombreCompleto",
            dataValueField: "IdEmpleado",
            filter: "contains",
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

                        data_param.IdDependencia = IdDependencia;
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
        
    }

    this.ConvocatoriaServirJS.prototype.abrirModalEvaluacionConocimiento = function (IdConvocatoria) {
        window.open(controladorApp.obtenerRutaBase() + 'Convocatoria/EvaluacionConocimiento/?id=' + IdConvocatoria, '_blank');
    }
    this.ConvocatoriaServirJS.prototype.abrirModalEvaluacionEntrevista = function (IdConvocatoria) {
        //$("#fileFormato").data("kendoUpload").clearAllFiles();
        $('#hdIdConvocatoria').val(IdConvocatoria);

        $("#txtNroConvocatoriaCurri").val('');
        $("#txtCargoConvocatoriaCurri").val('');
        $("#txtEnlaceReunion").val('');

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

                if (res.IdTieneExamenConoc == 1)
                    controlador.inicializarGridConocimientos();
                else
                    controlador.inicializarGridCurricular();

                modal.open();
            },
            error: function (res) {
                debugger;
            }
        });

        $("#ddlComiteMiembro1Curri").kendoDropDownList({
            autoBind: true,
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
            autoBind: true,
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
            autoBind: true,
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
        $("#ddlTipoNotificacion").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            //dataSource: dataTipoContrato
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Convocatoria/ListarTipoNotificacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
        });

        $('#ddlComiteDependencia1Curri').data("kendoDropDownList").readonly();
        $('#ddlComiteDependencia2Curri').data("kendoDropDownList").readonly();
        $('#ddlComiteDependencia3Curri').data("kendoDropDownList").readonly();

        if ($('#ddlComiteDependencia3Curri').data("kendoDropDownList").dataSource.data().length == 0)
            $('#divMiembro2Entrevista').hide();
        else
            $('#divMiembro2Entrevista').show();
    }
    this.ConvocatoriaServirJS.prototype.ObservacionEditor = function (container, options) {
        $('<textarea name="' + options.field + '" style="width: 98%;" />')
            .appendTo(container);
    };
    this.ConvocatoriaServirJS.prototype.FechaEntrevistaEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaEntrevista">')
            .appendTo(container);
    };
    this.ConvocatoriaServirJS.prototype.HoraEntrevistaEditor = function (container, options) {
        $('<input data-role="timepicker" type="time" min="08:30" max="18:00" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:HoraEntrevista">')
            .appendTo(container);
    };
    //$("#timepicker").kendoTimePicker({
    //    dateInput: true
    //});

    this.ConvocatoriaServirJS.prototype.GenerarFormatoComunicado = function () {
        var IdConvocatoria = $('#hdIdConvocatoria').val();
        var IdTipo = $('#ddlTipoFormato').data("kendoDropDownList").value();

        window.open(controladorApp.obtenerRutaBase() + "Convocatoria/FichaComunicado?idConvocatoria=" + IdConvocatoria + "&idTipo=" + IdTipo, "_blank");
    }
    this.ConvocatoriaServirJS.prototype.GenerarFormatoResultadoFinal = function () {
        var IdConvocatoria = $('#hdIdConvocatoria').val();

        window.open(controladorApp.obtenerRutaBase() + "Convocatoria/DescargarAnexo11?id=" + IdConvocatoria, '_blank');
    }
    this.ConvocatoriaServirJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.ConvocatoriaServirJS.prototype.eliminar = function () {
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

    this.ConvocatoriaServirJS.prototype.EliminarComunicado = function (IdComunicado) {
        var data_param = new FormData();
        data_param.append('IdConvocatoriaDocumento', IdComunicado);

        controladorApp.abrirMensajeDeConfirmacion(
            '¿Está seguro de eliminar el comunicado?', 'SI', 'NO'
            , function (arg) {
                $.ajax({
                    url: controladorApp.obtenerRutaBase() + 'Convocatoria/EliminarComunicadoServir',
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
                            controladorApp.notificarMensajeSatisfactorio("Comunicado eliminado correctamente");

                            $('#divGridComunicacion').data("kendoGrid").dataSource.page(1);
                        }
                    },
                    error: function (res) {
                        //alert(res);
                    }
                });
            }, data_param);
    };

}(jQuery));