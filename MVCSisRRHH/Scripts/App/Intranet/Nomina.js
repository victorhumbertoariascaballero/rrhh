(function ($) {
    var frmPersonaValidador;
    var strMensajes = '';
    var PERFIL_NOMINA_ABASTECIMIENTO = '45';
    var PERFIL_NOMINA_CONTACTO = '46';
    var PERFIL_NOMINA_CONTABILIDAD = '47';

    this.NominaJS = function () { };
    this.NominaJS.prototype.inicializar = function () {

        /* BUSQUEDA */
        $("#ddlEstado_busqueda").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Nomina/ListarEstados",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlCondicion_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "IdCondicion",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Condicion/ListarCondicion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                        var data_param = {};
                        data_param.perfil = $("#hdPerfil").val();

                        return $.toDictionary(data_param);
                    }
                }
            }
        });
        $("#ddlSede_busqueda").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
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
        //$("#ddlEmpleado_busqueda").kendoDropDownList({
        //    autoBind: true,
        //    cascadeFrom: "ddlDependencia_busqueda",
        //    optionLabel: "--Todos--",
        //    dataTextField: "NombreCompleto",
        //    dataValueField: "IdEmpleado",
        //    filter: "contains",
        //    minLength: 3,
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

        //                data_param.IdDependencia = $('#ddlDependencia_busqueda').data("kendoDropDownList").value();
        //                data_param.Estado = -1
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

        /* REGISTRO */
        $("#ddlAnio").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Anio",
            dataValueField: "Anio",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarAnios",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlMes").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Todos--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Boletas/ListarMeses",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        
        /* CONSULTA */
        $('#divModalEliminacion').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
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
        $("#ddlPersonaSexo").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarSexo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPersonaEstadoCivil").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarEstadoCivil",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPersonaGrupoSan").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarGrupoSanguineo",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });
        $("#ddlPersonaCondicion").kendoDropDownList({
            autoBind: false,
            optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "IdCondicion",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Condicion/ListarCondicion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    },
                    parameterMap: function ($options, $operation) {
                            var data_param = {};
                            data_param.perfil = $("#hdPerfil").val();

                            //if (data_param.cod.length = 1) data_param.cod = '0' + data_param.cod;
                            return $.toDictionary(data_param);
                        }
                }
            },
            change: function (e) {
                var estado = this.value();
                if (estado == '5') {
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
        $("#ddlPersonaEstado").kendoDropDownList({
            autoBind: false,
            //optionLabel: "--Seleccione--",
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Persona/ListarEstado",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
            change: function (e) {
                debugger;
                var estado = this.value();
                //if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO)
                //{
                    if (estado == '1') {
                        //$("#divCese").hide();
                        $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
                        $("#txtPersonaFechaFinLabores").removeAttr("required");
                        $("#txtPersonaFechaFinLabores").val('');
                    }
                    if (estado == '0') {
                        //$("#divCese").show();
                        $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);
                        $("#txtPersonaFechaFinLabores").attr("required", true);
                        $("#txtPersonaFechaFinLabores").attr("validationmessage", "requerido");
                    }
                //}
            }
        });
        $("#ddlPersonaDependencia").kendoDropDownList({
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
        $('#divModalRegistroPersona').kendoWindow({
            draggable: true,
            modal: true,
            pinned: false,
            resizable: false,
            width: '90%',
            height: 'auto',
            title: 'Agregar Trabajador',
            visible: false,
            position: { top: '5%', left: "5%" },
            //actions: ["Minimize", "Maximize", "Close"],
            actions: ["Close"],
            close: function () {
                frmPersonaValidador.hideMessages();

                $("#hdIdEmpleado").val('0');
                $("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
                $("#txtPersonaNumeroDeDocumento").val('');
                $("#chkPersonaDniValidoEnReniec").prop("checked", false);
                $("#txtPersonaNombres").val('');
                $("#txtPersonaApellidoPaterno").val('');
                $("#txtPersonaApellidoMaterno").val('');
                $("#ddlPersonaSexo").data("kendoDropDownList").value('');
                $("#ddlPersonaEstado").data("kendoDropDownList").value('1');
                //$("#txtPersonaFechaDeNacimiento").data("kendoDatePickerssms").value('');
                //$("#ddlPersonaIdUbigeoNacimiento").data("kendoDropDownList").value('');
                //$("#ddlPersonaIdUbigeoDomicilio").data("kendoDropDownList").value('');
                $("#txtPersonaDireccionDomicilio").val('');
            }
        }).data("kendoWindow");
        frmPersonaValidador = $("#frmRegistroPersona").kendoValidator().data("kendoValidator");

        $("#txtPersonaFechaNacimiento").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        $("#txtPersonaFechaInicioLabores").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtPersonaFechaFinLabores").kendoDatePicker({ format: "dd/MM/yyyy" }); // max: new Date()

        $("#ddlEstado_busqueda").data("kendoDropDownList").value("1");
        
        this.inicializarGrid();
    };

    function LimpiarModalRegistroPersona() {
        var today = new Date();
        var month = today.getMonth();
        if (today.getMonth() > 1) var month = month - 1;
        var year = today.getFullYear();
        $("#txtPersonaFechaNacimiento").kendoDatePicker().data("kendoDatePicker").max(new Date());
        $("#txtPersonaFechaInicioLabores").kendoDatePicker().data("kendoDatePicker").max(new Date());
        //$("#txtPersonaFechaFinLabores").kendoDatePicker().data("kendoDatePicker").max(new Date());
        document.getElementById('imgFoto').setAttribute('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAKAAAADgCAYAAACEl21BAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAAB90RVh0U29mdHdhcmUATWFjcm9tZWRpYSBGaXJld29ya3MgOLVo0ngAAAAWdEVYdENyZWF0aW9uIFRpbWUAMTAvMjQvMTlHEqosAAAIRXByVld4nO0aS24jRbTsxK5J0oPjmk6lvGDNAXILViw5AMqaDWLDATgCI07ABViwQGiEhISiEYglV0BiOzMLQtfn/arL8fRrjxCjlF3uru7qevX+n/av//z4t/nEfH4f293wub+7u0tnd6nfPR8+8ed56s9vh+9t/EmH29huhs/tzc1NOrtJ/d4Mqz32x/6/6cduj/Af4U+Fbxvr2Lc8qwdK/G1exlZAbA2CweJg6dnp8B/Gfuo1Ff52hDC/NoLy0Lyp8K1YRSy+B8zDZwr8W5i2+PxW17T6t0e2JzY7HT6hLbdgKwbQrQfn6ekvsB/Jw3qvjMhrOvq3eL9H8g/MU9EfV9jD+8XaDq19r9qfBv/RFizDytrObZ3vvQtu27V4P5f/e3GJP53bhQF4Au/76+Au9tNBx/+W3Ye26YN3zuU+fHrnd9tzOU+K7zz85SbWbjdAjrgn9ON32MF1v9lPLy3/m04WYXoHp5EU/morsBfPzsFfcvY0w4w0d5kEmQHDyW5MAXhWxf+mN/ugwGYbyAyI1OgGpWw+OxV+E3Q6hkzwvmCNbPDpbL2HcjP0X24hYxoKuKQB+UL8unBu2tZQy/+aDrYIHolgQFVIxyb2SvsrLF/+7TLmDoifKOEK7PgjzT5S7lj2DyQfD0nsEHqSQNPwhGr40ptZwNb1QIXEdxBEf7Vpy42G/g1DagucbPcR8TJgAlB7AyX+FfbG7lDmwP4X2UOJXDU95pH436Hko+kpxqhYwuDGflAd/9Q+P4k/Sn4hBFnDPnHCSsBK+rcCzAwf8KcjuKFMhHbEpMCfoY2rdQF0nTbgSB4iERrSP9P/sfW6bOwlxtIStuLTo+V/Uf7B+zK5y3KQbZGtn1XJXzOIiuafgh7PwwBgR3DtoEXJfykGyf54FD7vWBiANqkCPMf+1tgbsxKSV3ZB0IfdbEzz2cnwJe9RqWTw5SH0Q4LsupHcqOIfI7ePjbt87gscKOaFaTYd/HEk0wUHwD2LfFAWw+J02Xp2Bv0lQYuUQdDXkzfIRmik/rPi//EWit474H0o1rBwgYufZc8ewf7ldhmYrvPAL1Ok2/PYdPg1BuXQOb4B7oiZ9bNmNv9r+uNKPaU9ZIizVZLh72z+C9iFFKdG8Lucox6em2WDcsfkf8kAMlTIvDAPXDZQ1/O/EcebnP4Kz+dhnILvVc0ylf1rgs7tnGIgj8KQtzJMWh7b/4ktpDMM99AEF3cU0V9UkGf6v3Ekk1SgF14HfHJDXtX4t3mfTzsPGgdWOO3BjgHjszr5b2JvsAaBfI+DZxdykpQbtf61oiBjNleefE/aQDhLvB97TF38N8ZAknbTYx4WSbAbwt5Fg1jY5tm/BlbWB0hBXQiXayxF15YvH45n/2AHy7NYcktF0I01Texn2b8ag8bS1nZDy7eWVeVLUk4d/wiAe+yBqbFvTJrPf1rddjHMENcG3osquDGccir/18R0se7cducbWf6Fvxoub/LrgPGzav4bztNu+8wna4N1PpxXQoKd254vG5SbCn+MwcmZ8yz7r+Sho3JYcNuKCLr4Q0jwgPqOFTp8cHx1zAtLRHLtziTl5up/53jimZyfiHRdLyKSSKczdntW/peIywpM8GV63lEUhIW4IFKRGfhbijgp6vfob02KiKgYAJlxqQVapf9B5DqPNR+BZVo+ldtteObpLQgVZ3gxTo1/FGz09izuplrnSoQBVByNP0FN/0K37TUvbgOc/On9FtlD74JSZI7Vkd0Ttf4VwYJVRb29DHadWUM2hIXgnuSlx3rkdPhJegPwkyqcBXTZTUexuCv3PbIni2Jyixr8h1zLSb1j+JW4N1RvgjxIArGk08rfyRm+3HDwrodxAisgtIHEKZ4VpzknWvmn3AYqvT03QFABEW9DevY+sJSkLrT8R3I7rHQz8+KYLjDgTPhLitYp4CfpQ2wRWO+ZhLmeVIHqQaIck+cp6W9Rz6sNcFsA2sDI0qMNKCKrhd9zMIQPyEOFNFbBvZM6ooJvk1OH/IJoGa8BQUjoHBZh86ugwv+iAXr8kc+kA1B/QoS5XiD/hTfU8x9eNeD7vjKmDaBNYAUBlhfO5D/KP+V6YFVI+uleXQ0rj8/iP1i74nsIDpkB0BLyA7Q7p8ffIFcRMZT5wM6YMWQhAKnjRm1/EwzGf5J6Jl/Aa9graUKmVAwBJvu/HMGDhHO7z8GykAiwxh3SvJU6/gJ8es5jevMBMo6KgBtgMnB1oY8/Vtsd2Hl640T6TeccfXSaaW75Q4AK/yGR27hwDZJ+SX89IcagPRLSn68Fv3PK/AcD59Puaco+4mp9ynGH9DeOI5R8MRRQw3nIfwvLV1xJBOflf/EM27r0oa1K57fS3/HgwsmpoWVU9F+ZI7V5+V+z9lO1kyZUMyf/WfKyzgqXXNfXWnsb1Qx1+l//uzS2ReNau+Y7q/62SgQ4vHizOlqfKfi/TFtYrMcLNuu7VoKTG9LnfzUYA/wfldpG88w8+o9rueUgSo01H/ZSZA7+Ffa2Wr+Jc62jGvi2AaTGvgVrRJG34P/p6WLx6ccvnsZHfvrzbt1Yz3Ddb9mgJuPV+NfqVLA/qPst7I/A/5ntEf5/Dv+jKf3Y8F+/fv3t0L+D/ubNG3HOez2vdZ8fp9J2X3/16tVvQ38JfVj/JR+LPtx78H7Vj7XH963/sTS///LhC/PNzz+svz75XtjdZOtLBPCZuQc5Fspx5PFX1fjJgflfTpx/cuD+oecXE+fX4/WB8aH91fi+a37MHd8fwLem51z6Hptf7/u41rd6XM//YiK9D42n8m/qerW+TNWfvw6Mp9J7Kv0OzX9n9uFf4Hf9AwJ5OjgAAABIbWtCRvreyv4AAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACmkM6EAAB2AbWtUU3ic7V3ddttGksYk49iSf2Qn2czF3OicPbt7lQwAgiB5KYqipJiSGIKK5dz4gCBhcyJbHklW4vDwYfcR9swLbFV1Nwg0GiAASiQdw0oEEn/d+Krqq+rqaujoefNm0u7tjifGtPfT8XhSMQ2/NqjVpmedXX+iT1+yzYvDlj8xbH16cNj3J6ZpTXt7jj+pmdOeczqGE5r7cA+f/5t2O52bSbMLv3Z3+lcT7YE20DztldbUXO1KG2ve9PD4CPY/gv3vYP8B7L/Uhtq21oGj77TRtNc6GeBtd47p7jsOdG1UnzZbh+OJNW0eHY0nI9jAbnfadPboJKeNj9F0Duhbs8M2z2mze8xvsNem770+ndtu0rd2jzbHbKfThXO9abPPDvbZ3fsOa+SI3Y9tDnewl8fYK33aOjGwO60TE2/TOqnQpg07TdiYbFPBzTQHNo52ofna9SLYGL5VGB1jQXSMu0FnK6Y5PcDpA+wbaiNtuKj+GJ++/mzF9GdBhGQtyoPRirXoK47RCeBzzvH5huPTBzR+B3w+gB6NSJOa8GkMWF5r53OsrVZQk8yBF0FJV6FkMZR8l6FE8IdwsgYMKJMBZamAsi2GlFlnUOEWsarqDCzaVmjrdH+BNszG1HHEhy70xfRhB2y9wdTp8e89fCzcOrSdB/QmBzo7rG5RAx3Y82GtMFgNk2ufV2ew0hZ7YUZwVSqgMeK4clgZqri3zdFuM7Sd3i8cux2SpNM9AanYDEPLzozhw8CY3wN6LmB4Dcp6mQNFfLysMA5HKTBWPDdixYRbUwC6xxU2G9mh/pElW9yULYYkMAMhSdsKbQlJfDaC0KwClo740Dth5kEaS+j2GNo9jsc8eJ+FVPQC4EUWWArU5igT1CbXWc+UsB56DGvGzXPQbthqNvDqDG2P661XZ2zAQBYfeuKDGn/2oSlk1OMcgpKgPVlFsRlo+jUIwyV3dZUKfp2Bb1QY+rbF3ZU7yqDqdWs+Y1Qlxoj6K0CVkDdSkBc8nFXPhQ4bnG1NzrZmlCkec/xekJqCksK3C1DaC1BXhub9wMm9weAxFUef4+hHcURzvhXi5XxRdQt6fTXpcrdvMQAtrrkDP6S5qLAVY8gUFj/kw/BBBENP+zUVRbOucl7k8OeiWDUzo4jSQRTRWyGMRnWUmXXJ/mNAVlRAoiI6fBuz+2IwHsBRV7vRPqbCiM8VgtGs1vMEoTmARPONUCp5v4xIqlWS0ylnU06mTAGFbnaFx3LEh95JyOx1ZvbwPYzxI47xDqjhJY9TP0BA9UZCeDYSSEXYiyAszJ2OL0abFTMK8XDBsVAUXUulpwxLHmuRvaPPQXBhRx4MH6pHBXl01c0CpNHINBSQoRTBFu2/MzDFKGAemE84mLugcOfwM9Zek89+D0z5kQO6EVLKt/D5QrtIBdOwou48Q/Rv6JZ68JnqhnRpWJUathoZCFSJZaVuMixlv5MFu3SDrg0ZdHWPITe6pbSYnpbWMAs672yQFVWuHhwdkHK9S4XIaNwuRreb1rgbjL7iGL0AHrtWolOTTE9K+6TlDvHSCD7myvFxuk3GXE6zEGEJo/sJuH+MZ0Uws6oMM2MggcajZldnqHlp5I9UM1+ryMkqQj4iqjsBTuargPzDGD4MMHxDuVePRsznkkXukt9Eyk+P8ySdoyhMoXUS4de8/GZp8UDP1UWcZy0YMWcBsCliZu5q8iPZJfLHmPo8F5K+lcFz+sJzYjyRc+jh69HE2S3jKOJlloo8EWGJ+NAVqQZHfOjlhvi+FkmR54HXyzQeIXXOje9IGpHQEOVWAaZczmg0S9hkx6xIgKx2LGxMlwgd5rry2ngQHlfFsNj0FhzMKd1LoHhSQmYjgA69ykeKR4qMzaJDM5Y8KDZHlcWQzTr3KDQoyYhWxVShZTO0bIaWLecRg6GvMGVKjR8UwDFbMkZSvExAZnPOaiTFXALlw+6AE0ntBJC9MDkG+a3ciN4PYmhX+3XOxAzPbTUYnBSghODUF9VLS0q0xhUTk4fxyZmESYUGT7Y2uG5yTG0Oqm0lqWeQ5RJg+q4SzBYZ8xviv6jTbgHAbOJgWROtaerZ8KyCITeHUmdI6gxIPnnI5w71jNBsBdAERzLzoTLBsgbz0UbB+ehklJ4qUWrRzP1g7sTpuuJk3jpOT5Q48WqQ/BhFHYK+/OqPNE2q8sl6Ple/kL1ly76va/0HR8ng02lVBlOVweQymFwGU1bGVhtcn2oV3n+yBlcpaHAi9m/jcJOKhvqIQ4HZxNxFVhkz4rpy9ia3Y1NHXXZs5qYpJhOb8TmvdLCE/XUoy/SGjh8RP/0aqyeQM3GeCjwqTEkiqdlIs0BK5M7QC8dSs3yIwNOoj3LgGWZ8xDAZzYSoNc8ggKZTMxcILAtOnuBQZTazYXfAdfE80EkZOwywKb+Jw2+iXD0S8wcTsdGYX+kxiQfvuiogG3YqX9CFh38Hn2a1J3/jMBna93B0rP1OYG5rO7D3Gr5/D58QOvQUc3Me0bRw4QF8ml8ozH71ooFYHLNNFWbLREYZgOVARr8rZERR2a4ohODlpeEzVaZHJpboQt0UJ5AAVA4Hmr10LxNQybUjRGZUXdKM1TkkQyqSj/vBrM0YOexuVgt42cOQXNPyUlUTj2VdBqSbUkJCk1qhafnEYhwBYI/Xz11obyUAUSffaT5OCSKzqQA0qhYD0I7WhXEEM5WF2co6HKXFshRPEZMVHiCKoMnrmWhboS2rrbF5cY08IZ8M2qwk8V8wCnfJkNNLEqtFdS5H4RIrXAglHXNME1gDdYoM97f5/jbbT5XLQuOMGp+BqbEST1EVTrW2BywdmQ1TMZR4SXimDyWi8Vumia08NsyzjgNljkyqsleacFJ1Z1LCMTpFiLgRjk4sqEuGT0zqt3H+RQXewIpUJgvjzVMFnoodOSlVzjYKnyiO5fBVVMroiQhOKktu8CCuwWcTGvZMGXsiOducKaFI1mYG8b4WTtamW3RFZdHSJL/apPXsKkigxkEcWPOT3oEOShaNcmEmbfIaY/zAyrxg+MVmWPFDV3CjI+y5x0cbfKmCqoruFPB7RxWfH8gjCz99jyNb0VoFJgZrWSoQs8Q4lejsTBRV4DDF+pls6xPMumKlxwEb2ubBaXOGEwSG+xQm/paOmFIRsxVtCsiM+ZroKRXRGKms2VgqZI+CCOYNlZfgCG0UK/ySYbOLrigysg9hg0riip1/OlVMPs8LAw9E2HcQD/vmQfcscBrnVNuUd5WLOiu1uOpRJU5omYurzAUI5TMysKDsSpTKJ5iPiI7WquQnOmHAJ7D/mnID8xbo3oYBV7OvGpQwbMyPZsTEvhRO35L5Pg5SyBfkdd+A/vH6pnTYbi/TnuYq6sr6VlzTEYLNzLPEJwk4Fvs58XHIPAS/4wj+TIspPZoZvKIkFaofjpi3qWYMVTG9pElpz6wCLyuklfmQSq6EKoidsbzMMpf7rfA8Q4UnGmBLEQ2vjggbd5cHipF1FjSc6Y2Dxa316NrWhCUDSQJ5ElLp36koZZuWweRWasoe5Fg6EEjBmj+8LpwQw/FzBsdE9RNeTVHeQ1lr2iE+CKy7fOSDj8gq+EJDIbkqLatzC1T/7p1brFJIaQ2VRj26iNNXhqPqZcdSzZWntgfVSElOGM2Wb/KhUnhVN/OD8IC1KkMfM3LTdqd1M2mHq6d9PpIawfaaxlOvQQwI8WVgDD6vJlKdc5rhHC6kNoOozYi7zaBp75FGt3stOqXXY8cOUGBt54w2+0F/n8HdvSBbOOTseB1KyF4FXsnj1XnYB0/7FQxY5Bbb+z8DCse71P7+IXze7+IrUdrsjSc6/ZuGDhniEH8ZCh57icf0xe9jFLyFOIT/FBJl6cBr8iWXAi9JoupzTjOcU0yiFSZRwyhFmkekDwORjql2N2qeG4HpMfSQP08TjywkuEoptzxy24yYIkYR4VFG2LzEsdOUY8UkZzHJWaXk8kjuAZecQ4iMQ7WgTDLHhNJbha2FjxSTmM4kpkc6tME7xN8XpTD/8JHTxCPFumSyLpmlEuVRosdcZnt8Xdp7KjgIk8BjLiHVGadzzygmywaTZaOUZREq786GHkG1vaDr6LHTlGPFJOczyfmRjj3iHcOloAOtRaJ4QyMjkVv1g6Wi0eOnc44X66TB6Qu3LSOEaLtlRr5VIt+syLf+QKK/GR+7ZACzpSM+n8YV+08T9hd7mip7mGppLEW8Z5ccjxd6mYUwBrH/NGF/MWnVmLRqpbSKSKtNiAwDPIRUZvtPE/YXk1adSateSiuPtO5zaTWpyOIqmPb2g6WIl4Gk5L3F5OQxOXmlnPLISQTsPQra2KoDebw+OyKP12dHislsyGQ2LGVWhAlf0HKaUYwJZ/tPE/YXk9aISWtUSmuutKb7lMAugtMGxwmPoHVhjDvDx1J1qlGzbMMNd0r/oRI82WBY96rRg/XgqO2ZI8NWPs/IHw68YRza1XThFqSykDgceiPLjSQOW4lFpaE39OjjmnVx1DLxJ3rUqoqjg+qgMjCiR+0AKfoXPVgLLh2O8EcJQl3Hn7goP73ur0oNnobY6wOc1aOK+pdUA8RKp2ZKUVH2raHrAwm34NlZ/5KIydMVorurRlYF8DMJ4BC0Yl8SxD/MOjHQTddNev6hD4ejBxvylakw33JDq4L6ScjDDDBFRzPWcZCV7KDrlTo6SjU76LrZcGtJ7ADKacvX2mmX1lIuNeQeZSK3te7+qsltj4oaqbSBKvT2ab0BXDVPKUy94umJLsMYNGrGIAlVY2T5VjUBVdsdebqRhGr8xobco0xKsdbdX5VSbHGlYJ4uUIV5ZJzsbUIMqev4f2K3VdZ8V42sGl6spbugQuJr7YS/dvz1/CGR2dDdaiPBmdeUqr/AfVYF0uc0ZiwE0CYHKPSmPjg2h61ZJJ5Edxhqy15uRne2zZ5ZSXd4Yc1PojvWcJKqwX8Z2Xqtu79qRWBsHedoZX9SRgRiJJHJRrLdZ1XQPAxG8O951R++d/vtfCuJ+xYzfDCmZbMOm25KnCh7Jf2H+jSkY4lkw1HIZiLr2vdVe5LAf8x8ynwrkTkj3qtsVpLlPreRfdxrt24me+1QhceIYDqkAmaM8Jvw+4ZelyAmZkZBSh/ij+le17mZtHb38NdzOP4/MDL4FQaLbcqCjQjISw7kCVxxrn3U2Is534KVXZA4LmHfnubTUm+0ulM4/5gtnJq2dn+mgWiTVhKwV12w0ukR3Rf/dsM01P5W5E6HcA1bgzDmbyZj9/srt+2RdK1DyoDWf0RFidf8BUtjyuaxa/+iGdJVbGFNBK2ZYvGr7mv/CWrnazr7idzhCa3u/I2HzIjvMHb9Blyvh34qmh97bvbq+iv4aZPCjubeRe7JM7G+j/60DUrmmkoorngFtBcg+BfpyocR3I/odayHWouf/V/aRKvRUVsz4EfXTO17+Ix9xE+4b0iv56jDvhocYb2r0pk1+G3AEfw2jbS6GUL8Ba0Wu9be8Da/wHYiZz+W5POO//keeLrgmppWla7Zg7tCnE0vlKJV9bClieIEJB7Tq1hekyTF37V7R9ddBVdUIlc8orcEXYHdJJ0vtzB7X5NaOl8SxrqE1eyqoIeh8yuxJz8gu03XST/0I+vkQ7rDObd+lUWErpaudGjp5JB0UXVlkgZv0pV41QlYwz+ZPvLr7kE7mLG4kuz+ITzjB6pmQp1gmn+dqOkbIS3qA3O9TzlzxifymVXpTGF3shbTPZGoc3D0BufoX0jev8DTlDxd8nTJ0yVPlzydl6crd8jTm3Ge1sySqUumLpm6ZOqSqXMytbWEiNoBRuDvhCh5uuTpkqdLni55OidPG3fI0yI73aaU/XXJ0SVHlxxdcnTJ0Tk5Wj7zNjn663gszc+nmnmN/UGqkrlL5i6Zu2TukrnzMbe5hCxID54KsUU5lzxd8nTJ0yVPlzydj6fthXlawTprVKPH/vB2ydElR5ccXXL0OnG0nH++yxq9ZI5elxq9kqdLni55uuTpT5mnF6/RS+bp9anRK5m6ZOqSqUum/pSZevEavfkR9apr9EqeLnm65OmSpz9lnl68Rm9+dnqVNXolR5ccXXJ0ydGfMkcvXqOXzNHrW6NXMnfJ3CVzl8z9KTP34jV687Mgq67RK3m65OmSp0ue/pR5evEavRZoI947hFTwVl/G07MX1r6KnLUczv6KtBk5Cc6X+C4s3SR+qd0ar8/nJhfsvqFZ8DOE+9VvhZvSdVhmYlfy3JuRq9PHZ9Fz2WtXZ97KSulVnAcq8DTV3Bon8m6cvz4JDUP/YH5WOmZLrHN7OlZfWMeecB0LRzFy/PkgiD9dFg0uSc+26IXGLArE/mzTN/xMz1jGn2X8qYg/5bHYpxt9ys9RRp/rGX3KY/+7yO/m4WnM6eKYfVnxQJyn3wCyqDvjkqcX4mlDQqDk6ZKnS54uytNyTfFd8PRWBJttkjz7a1rnoXHbZmhF9uzYctj6a2gbRxFvNVxJyHp6l+uvs7LvKmJTk/4EXh14BcakYCHIbyb86AHn4T48B3ES/asTS/o0okTmm+bmMVuy5vkck73K0iqkkY8iGrn8rNVWOKcwRx/XL49V5R7RJ1+IHrMGPxacX1yP8uaxqgqmXY88VjYNZBpwTvoc0UDtB/xZ2oxXVA+vtH/zPrnb/x1B4u+gCU1owaf2ma6+gpYuyfoxIv0Nvl8HvUNd+yNo5x5pxjb+zqlrA60B8vbgN8ZbI4rTLIpdha5hjgs1zScmY9qBZyO3oYYO4fyorj2gWD1Lru6e5koy/wLuVpXO8ebEInrMbz4CXRpCRPCBerEd0nfxd5x2yI+gr+oE/Hq1JK34Br6PeZvIy6jDrDdvtf8lGeMTX0ksdQX7wJIoin7F+foVfH+tDRIscUu65g2PCaNXfQlyrEqYJ7c2SoyF01qbXaVq7WlCa6818TdD1S0+S2xRvlL9jENC/zpHi1uha/K1JqMjWhxo7M+rqduTkZm1Gb1u3vPluW5VTIRey4LjPo0KDfJ6BvTXio0Y65yJXPKO6BeH8D+OkhsSE23AU6PlvYXfsq3Is3zRM8N6bkjRVVxv0qzwsVIO8yxQ3Uqy9SW1km55DyNPHdd+I7GNbPq7Gbm/fK6Rk7W/A728DMa1PKbV/iHHxUv18N/wPrG/Lx7vy4j3JspbA8VzvKL7XlCu6zoBzy3yDOH7Z7lqVRZdAdtEmxxQdMqi1wZooRGJY/E46pVO8QdadIMyGkOKMSgHlEtLNkN/sm+bS/s8pg33EnMUqlHj3ejOU57pZDkXjEWGlO+cxTvLG3MMCW+LomSPRqwu5eIa0pgD5edG8nWUC6LZ0JHEvvcBm/eUm0SkPqbkqzZCmSfU5mGUUXLJfwP2oMe8IbtaJhfMWnaDltfPDkeBHVby2mGE2cmDUf8v+Pj6arrfBTHtd/s3k7POLv6xyZdsM53tM6tVthc/xO+Jf3L2Nu/5aObVbvW+D4U3W/yuubT7mXZArf4DRpaYufxAunlNeY13S/Z8R7zlP6TeoL3/O+iNHLHHe53Fhz2lcXN+j7keNjcMbM5cyObecI2b/9yP4NxzwijwJtKThfLjuTTwq/AqjyXp2oPIypKx5BlXN2IxyWcaXG7fU75uqNmKEYvInChHLDn9WyhjsVSL35TyFnH7LrMUZZbic8pSmEGWolpmKT6TLMU9YFBc7TgKGPkpH/UL/7TNM8s7cKf3OPOwJH5+JvVjTCNgjHy3ydf7Un3tqmwIvWUFfmOU45INebC1aKYhbENVmtFKqQ2ZI5dNwpfNDonKjmXl+B9TncEVPRlKYUBXkmauhQx8qrzRSRI+4e3TrM5sjlEnGeCoPpXHIjL4azDfxyQw+76sCKVFto8678NVhP6a4G3AMZ88pBVkt8VoPIffSMV7Q3yHPh4Rastb26dAHnqBM1rrovFhCZh3JIGn2j7N5l1Rv13iXiGTZcbqf5Pk8S8aOW6Tpwv37kIRxX8bO0t4xUvy1i7N734I6km+Box+IC5J/rHXQgei0ZseRG+VW9WBJ9S2yG6sRv5/T5A/yzBj9gBxfktYv6N4RZWteRt6jrAOxGftG1JM9W3itcm1AeuRuamCjOsUZ9jQa5s8o89H9RYxhk/ZcYPqJqpUP4GVrhiT1ukMVxtl0JEetyOMzFajI98m6MhlqGdxrfg6cjyqFXJW6gvAMirVbxKvxtZd7TwyV/AFWuFaaMWQvEeFKpKY9zDpOeqkFTinVSWtQH7wSS+GFFtZxCU+cYc3Vyseab9oF1QHshqN+I9U1viD9y2uE0+DY3n14ZnyyvXWhQZf/YecYAd5XJMiCVwZOCCGQA6xaUZtQOzg0xybSZlfHMnIunCfV6ldUg3xu2BNZnTvsvKtXUKTIbsuMbRHPhlHKVhpy8YsbP1AfMxSy+y91bg/iu5dejz9SJbAWsXSBlUD+pSrYZnvBllELRZHiQrV4pLYAnTe0UoMdmQ7qG5eJjN+x5nxvVoqfLzjKrjxO2LAWf9fUZ7pitbzX2dcZ7IeYyar+JgpEgEk43EbY4u0+6s8UjVWYXAPZH+hsbXJgg12iL23Z0eWqn1b0P7/cWwuKUL7oNFqOKUvdqmvr2h0x/qa8uQR5FRXfuBbuQo3aYXNqnR1RFqHVfPogT3KKuoUkX1PRwa8gqUCz21QjM/WZqAvHpFHx8hN5qSvtF166g9w/CqYAQ3vW9ZbEsJtrgPePo2iLc7vHo99GqF6TVY5rgO+WSvHH8J9xFPGNQ4rtITNV2Lrr9DCP6Zc5VOGs5FBvs9oFQh7+kvy/h7Fv8uXepaerIMuDCn6rVEs1iBdsGns04jowoBGyo2ILuD/Pp0rV48tXxe+pnmCjxxhtq7kI3y2eI9xrcAe15Hwuiwcq7GVuMv0CWk9WA+dqFOOZET2zqL1OuVUwtG6TbM8Fcqb4G/23SI9shR5lDwy2qBI8ZLVRy65EjDe8p9BJkPat4hMtqTcaJ8QHQeZpmXFU+m9+HPIqsr8a0RW39Cs9JiPaR3Adsw/vacRzXVEWg9m86ZLlU+83U9HIjWQQ42yhDXKFuJvmzxjlSKk+KiXvYsjun5RvIvrgGLsi6XxVmw1963VoH9+79tYZO25/C6udXvfRrH3ctqxa+a/cUOu7pn3xg3ME+ddDy+/DaN858Z6vnNDfm/BbbxzQ37DgZqPxVuTj4lLMaMUX1NUcvKfm5PX/R1IRTk5/1uQ7p6T4+8KKFn5c2JltjLqyAFannZ3+jeT5m5nPPF9nf5N2+xbg/5N292AvR/QbMWr2ds/Av72Y/x9mnik1zoZTKCVfnOMm702bZyj8cSEb/3xxJi2ey06pddjxw7Y5gw30/5Z82bCGv4SlPVY24FHeX4zedGF47Y+PeDbvvML3Auep38IT9A/bI0nNd8dDKnmuH/WXvwm072z7s2kfdTHfu12qLPdDj1Bd4fA7RyzfT12k26ffwcEjOlOt8M2Dj7szs4ufdtp0caB24zgzBZesN+hJn7s/jSeVHHrsK8nbNPF6/fbh7j50cFzXNjusa99vN2PTpMA7XQJyWPs3L7TwX0d5xQ3LbbpOIT8rnOEl+3tOvgwxy8d/NZx6NtB/whvctDvk6K3iMbQqf5GWyqQnp61qYtnR9T/fo9uB1fi5qy1Qzdvn8ENtOnxkXUzgV+A95Q2PtsYbKNLG9i28XxQm+qUNiCLveNd3PZ3OtRc9wU1jh2Fg0fHcMHRcYtam3ZewsN3dl6C1j/fx2c97TEcgukHlxbWbgMRHzGIjnZJUK1DgmG3gwaxh3fZfY6H9zpHoEtH+8GOs5M2rU5kG1qX+H2NL0uckoa5HtMwnylYXdKvRtWais+ujw6hvwMSm2ZspJGlkXrD0As1YvIngctZMy63FlUzQ79WD5pp2EM3czMWb0av3mkztmjmbp+mzpoxGnf7NELL9KgGDCylCniePRDNWAPfFp/rtYrBP4/q1cpMNeDfz4dgTD8zSwBjku3I4Hb0MGRHrMDpBfweMIsymEXp6RYVa2y632vdTPZPzvB5909e0saBbxUbti/Zlvkum/7BFS0YRey3qLH91nPm5PB/+HaAHNj6GRs6ccgXnTg7xDD/DzM2jIi0dHqVAAAAvG1rQlN4nF1OywqDMBDMqd/RT4gWHz1qfAWTtmhKtTctBHIrFHJZ9t+bqPXQhWWGmZ1lZJtbqDpmIMBBMA0UxxUevNAQRjE2XGkIkhC7stfgsb8bd5DXLqW3QTEKCyIbLeRtPTv73gkP4iI/QA6EkYm83R5JgLJfLCZchJU+x1ovlEJa4LLeheFaLY1W+Ot3G1xpSrHxZHqh6p8GUoqKu4+KFwYSfQ7TADceJfP041N4incepRFioTILuM0XAw9e0HPwAVEAAAMMbWtCVPrOyv4AffeAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB4nO3W527tKhAG0Pv+j5nee28cfZGQktmDdfN/IS0p2w4wzIDtcX9/P87Ozsb7+/v42fL7+Ph4XF5ejtqenp7G4eHhSN/arq+vx9HR0Xh5efl1/fPz83ue8/Pz8fHx8ete/jfjpW9tDw8P7XhfX1/j6upqnJyc7IyX2HM9sef/auxZV8atLeNlrre3t53YLy4uvtWWuDLe3d3dzr3kJ3F0uUgeko/8/bNl7vS5ublpc5E8ZQ2rXNTYk4vT09Pv2GsuElfm6Wqfsbo+j4+P4+DgoF3v7e3tZu0TR13v6+vrd59V7bv1pqW2yXuNPXsh15PfLvatfZt7q9hXtUrsyX1tmSNxPD8//7qemBJbctHt28TQnbnEvr+/v7NvM15iz1zJZc1F6pj5auzzzNXr+T33S22zT5e/7IfcW623O6fJX/ZSl7+sM/ey7try/5mrO6fJQ1erxJX8ZY/WljOQezV/aclFYq/jpVapfVerrX2bvCbGmov8znhd3reet4k992rss46rM5c+9Rnzc6/XszP7dPmbta/rzXjJT/LXndPVev/POe1qv6pV9m32UvdMzXr29vaW+7ar1az9Vq223i81F3Pfds+suW9Xsa+eWavaJ2/pU89c5s16uufSfCet9npi787pfC51tc8e697PWe/WMyb56/b6Kvb5fvnL+zRt1qqrfebpapVx0md1TlffFvNbYHXm/vJuzBjzzP2ljok5a+5qlevZa933Q2Lv8pf4Mk9db8ZLHZOPOl7mzlq7s5N9kRp334EZL/26Z3Ri6L5VMk7udbHnWpeLzJW4t+bqzkGuJfbu/TLzXsdLnjLPKvaM1801897VMTUsdfwPAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANvwDsw8NH3AXnE8AAAuXbWtCVPrOyv4AfoUxAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB4nO2c+28dRxXH+aMoBDVpHJLUdv24r1xfX8c29rWdbQoIhKBUolSlTaM2kVDUqAgqVYhCKyGBxM8gKDRqhUpUSPNoEOGR9/vtOM7zMN/ZnX3MnjVFyuZGV9+RvrLuzr1raT9zzpwzZ3ak3t4iTrXWnFTHZqXWnpf0dahq+zqmbyHbNz4f/ma8k/tNLerDd1rTz8iajQ0Zm9wqJ0+dltu3b8nZs2flzp07km53796Vc+fOyYULF+T+/fuZPvP5c+YP9eBkOAVWlm9z1jLLs++EHDPsg7AP7Fs++8B+t2LHRfKbsamtsmZDQ57/wU65fPmyGQO3M3zv3btnuZ8/fz7HHn3m+91+Xr2mkFXL2GlrRqqWfZBiGMQ+oT6us69a9r5PWDDXwX4uc70xEVhf0PdkXfZ88OcMX/AGd9g+fEC64fONGzfgK7r9vHpNoY9uzuRYZeYDj33Nsbc+QfP5M9afaPerm3ExWJ2Ubzz7Iqhn2J85cyY3H+DzpUuX5ObNm/IIPK9eU8jesgo8jivEAmPafBDZvWWvxwJ2PjC+pj6xRQbr0/KnPR9axmB/6tQpNRbAdfCP5oNuP69ek8IqsIzCWGDBGxcLYQyosW+Hdh/6kcAbFy5GTPr6RyfluRdeNXa9ZPifk+XlWzn28AcYG5j7o9bt59Vriuf5jP+280He7msx+y1K32zkR55WcwB/LA03ZmwusPfjfblYb4UcoNvPq9dUwKog/9NiPfgE5AcFPj+x+2xfpdmRvv6mvPPL32TYuxwAStk9+ZfMv5h9KgfIsUdMN6vHetH9qi3Fj5hrFeNjnnhyk+x6460c+4sXLzL/e2j8g5BVy2cfRKw64byu5f5gr6z7hHGgbvfJuJiTDUMT8t3nt5sYYNkydrm/b/eYDxD/M/8rgf94Om5LsZoILHvYqWb3SQ6gs6+qOUWSa9ZMX39lSjpbv21i/LPGtpfl9OnTudwfOQH8wfKyHSPdfl69JsujmrPTkH21+RUl/wvXdorZ6/kf+irNZK5oGA3Wpm0uePDQYcP5duwHXHP5H9YKmf+VxL8o94fdj4e5enbu7lj+oU/I5obID1T27fnUWmH4G6wFgj/Wgv6y96/iN8ceNYJULNDt59VrKpifC/I/Gws4vumccUuKvZ9PRrFAaz5zHfwHRifkKcN/3ycHM+wx/4M7ckAvFuj28+o1qbFZXc3/5iO798dLuCaU2H2Qu19RbbB/pC3Dm2Zl34FPM+yL4kDW/8riH8R2n2cfrQcWrPtUC3x+rZ2OA73fmP8BXzJQmZQR8N9/yPG1a73g78eBGAuM/0viX5QD2L5wvs/V/duuLqzN9yuNi8THDFanLf9PDhy2jBHna+yRAywtLZF/CfyTtX5tvp+LxoWy7mMYVgrGRU21+8SP4H52/q9OyWhzTg4dPmJs/65a/8MeAYwL1v/K4V8psHvM2Qn7wLNtbU8AfIWrC3dsTJhlv5CJK0P+sP+Osf9PjX+/m6sBYSwgB7hy5Yq71O3n1WtSa7/wzZWWW/fx6jmF+Z/LAQr2BDSzcWWc/1U2y0d7Pxa/Mf97OPzz8326Xqewj+PAoGBcBJKrGbfya0wu/xs2+d/f9h3IsEesh9wP8wHzv4fHP6wDdHLzfa2d2gPozQe1tr8fRMn91bjS5H/D42H8t/9wDBi2jhhQyf3JvzT+QcgqVwNy+dqcUgPKxgJ+X7Luo8SBWP83/6so/0Pu7+cA6DPXuv28ek2RLS4U5n9J7l+8Jzy/L9ixV/YWuXWGdpL/7TsQ8gd72L22Dwz7P8m/BP5gX7Tfx4yHSsF+EFsbzOV/QZIDKHZfT+0tcvF/xYyvT/9+xLC9U5j/YVyA/yPwvHpNlm/VW5sPfUJYA9L6knUBpTYY13nyawkV1BNTPqbf+P/W1FflH0f+bfjfdjXeuGEsIP5n/a88/up+zZa+Zy9h7/dFPh/13aI5RPEx2AM6vfBNOfLPf4nfwB77ATAfMP8rj3+Y4/k1G8fKr+WlYz1vTQg5nn0/zMsNC9aWMX76Bsbk2e+9bHz7Uoa9q/9BzP/K5p+wqro9+rGdpvpaYSyQ5H/pvpXeBZm1c4k/XrAXZPX6qvzozbdz7JH/KezJvxT+6TU6ff9+NYoFCt8NzK35pd7/U+JAjKWh+rQMG/32D3tiuC7/094BY/2vPP61QlZFe7ijd0bHOivkfzPq/arR/RD7BV97ThYXb8SMwR52r73/t7i4yP2/JfCvjRfX6WP2ai1PfwesZvO/GVsL0PJJF1v0j07Jz975VcwYMT7W+/13gsEefaj/cv/Hg+cf1usK3tlquvc4/TrPXKoGlM3/UE9UYwGb/81Iw3xnqD4jna3fkWvXrsfsT548Kbdu5d8Bgz/g+38l8lfX5t26fT73r44Xvwti9xHA7if8+83bcQH2o2bsbBjZLO9/8NFnYu/NB91+Xr2mXCxfb/v795McP9wX7L8P7uy7aE/4nJ0rGmZMDNVn7fkPb7/7awvz2rVr1uf76z7gjRqAEgt0+3n1miSb30fsY5+Q6lPPAgjHS9G+71q0zw/j5suDbVnbPyY/j+Z8zOcae5f/aTkA5/8y+Kfzv4K9uuq5QO4MkPx7QFjbR04x0piWgcpmeXx9w1773Xthrgdfj7U9jAGPb2ENCH3M/8rjX8mt2weJz1f2BTdsPQffn5Pm5qelObnVCvWCp2pTsm6gKavX16zf37nrJ/Kfo8ctR+zjg9377NEQC4C9di4Q93+Ww7+5OTAsUY9biBk2J8HzGdmE83oM303mO+G1sB+fYfMjjSnDelo2mnhu7cCY1brBlqzZWJPJua/Lmz99N67toyGHP3HihP3rN9g9fIKW/2H/H/O/cvg/tnpQHlszJKvWjhpV5It9o/KldVVZ9cSwfP7xQfmC+buqrxL2u76+EWPbFekbaNkazlBj1q77z2z5lmzfsVvee//D2N5dA7/jx4/bmE9jr+UA8PmIAVPvg3f7efWa5OVXd8m213YbvW61fecb8uK2H8oLL70WXjM8t+14XV4x33ll5275/ks7zPVd8uO3fmHPbvj9H/fI/oOH5cSpM4bVOVm8nucL9rD7q1ev5vrg89GnxYHYDwAx/yuPP5479l3Hf40NYq/F0tLN6Hqqz2hp6YbcNJ8Rn6XrM7gO+Q2/xXyvsS/K/9x5YMr74N1+Xr2mTIP/Dc9iWva7jF1fj85kyddlwvNa8nv2cB/Yr+bzr5v7oS96ryNuYA+foNUBHoHn1WuKGzjA3rTYDPzAyp+fVzqrB98Fw9S7G3HD/0Df/5P/4f7c/1cOf+ejNTuF3y5apyk6rxMxPMYL7NhvmFu03B8NcR7GhZYDYHyS/4PnD66IvYvmZ/RpPtqd16mx985sjBvsHveL9nJmmqv/aXUA+BC+/1cOf/DQfDTYIy732aO58zq193RxP/T7DeyPHTumzi+we+SGRTkA63/l8cez1ViBvW+nYABWYOz7aGf33n5N2zCGwFfzMRh7R48ezY0zx97LAbr9vHpNuebqMpqdrrRHAzavndvuYosiH4OxVFQDYv73cPm7s9a02Ay+AH0+e/AGW4wNPwdwZ3drcSWYp871jhvugdwQ92P+9/D447mDhcYX16Lz93N9+A18hXaGL65rawnowxjz/xca4j/cUzv/k/F/Ofyj2qq23zru83mggaM/Jv5XX7SPV73fSn24H+s/FEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEVRFEV9Bv0XaCVRnyQ2bzgAABGrbWtCVPrOyv4Af0v2AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB4nO1d+4teRxn2XxLqrbS11vS+bXaT7G6yaXCzySaxCP4kggiC/lKwoJRi0VItBFQK/qAi/iQI/qAIvSCiaDGkzbXNpbc0aZrLeJ7Z7/32nDnP8377le/slGFeGNp8J2dOmGfmnfeZ9zIhELl9+3Zs6nf27NatWxOfMcEz9Tt55zO1zbR1BON97dq1cP36dYrH1atXw82bN3vPPv744/gee/bRRx/F/lIs299Kn924cUN9K/d4ldY6eHzwwQfhypUrvTUJPN59992IVyrA6Z133olzIBX0h5b2hz+/9957sb8Ue8wHPGP9Ne/lHq/S2hiPCxcuhIsXL/bWHHA4d+5cxCQVYHv27Nk4B1LBnHj77bfj3GkL+sfvly9f7r2D+XD+/Pk4B1NBP83cyD1epbW4/oAHcEyxx5ifPn06XLp0qYfHhx9+GE6ePBnnQCrA9syZM719BP1jLuF7qU7APoFvQc+kgvmF79X1P3v8seaBSYo9/gwMgRXD44033qA6Aev+1KlTPeyB91tvvRXXd4o9/i6+xXQC5hd000iP5B6v0lrEg+loYIVxT/dn6GhgxbDHb3iW2gnAG/MIfbL9xcMeOqG1v+Qer9Jaz84yWwA6P8UeOhr7xPvvv0+xgh5h2KM/hT3mH7BPvwV9j2/hvy3JPV6ltY607cAUD8OK7c/AHs8Y9phHzA7En/E79ov0W1jvwg7MPV6ltbEY9mhsf8baVuseWKUcAJhiXaO/VMcYB/CwT9a9/Rtzj1dpzcY14sTscmAHPax0NOYFwx7rntkWZgey/QX6A/0xToF/R9Nyj1dpLWIA7BkHAHbAHvtBKsAethnj6pgr6E9xAGZXwrZQdiW+NTpHyj1epbUxVmx/BvbAKhWsd2DP9gPodDz7JPwP76YC3KGXRntI7vEqrUWMGf/D78BL8T8Pe6zltqAP9IV5xvYXvKP435tvvln534D4p+vUzgPRmI5WdiB+Y9wffagzprZtkQp0Ps6Rkm/lHq/SWkfMFsBaZeez0N3KD8DsQAiwZdwfOgf9MZ2PfjAviB2Ye7xKa2NpY6/OaYB9qhNg/ynsgS3rr+0DYvuLwB5/N/d4ldZsXCMfA1bMDpzyjC4KsMd7jPt754tqf6n8bxj8bX9mdqBhz7AC5tjv2ToF9uiPcQCse++Mie0v0C2YE5X/zR5/YAsc03UK7IEHOwv2+J/5gFIOMMkHZByA7S94bzSXco9XaS2uU4a9OqcB9pP8fwx79AVdkmIPXBUHgG5Bf5X/DYc/O5/FemPr3tufDSuGPfpS6x79Meyx7qETkv0l93iV1jqyFR8QW/fACmuYxQeaXck4gO0vqTgcIPd4ldY6Yj4gtU4VV1f8D75ixilMx0wbY1D53zD4Gwdg/rq2jla2APMBQU8w28LbXzwdg2c1/nMY/O18VvkBlL9uWg5gZ8ueLcB0DPrBd5p3co9Xac3lf9AHyg+Ad1gsELDHvJgmBtB8jWw/gG7B/Kz+v2HwB8YsThs4YdyZH0DpaPMBpbaAcQC2v9g8Y/sBsE/2l9zjVVqj2CsO4GGv4j/tbBnPmI5RdqCdLVf+Nyz+bWnHfzI/vfL/YX2y+E+LAWQcAP3hd2VXijiw3ONVWhuL5//zfLUWA8i4v/mApuV/Kgaw2n/D4N+O0WAcAL+zHDA7p2H+P+gJpvNtnjEdY2fBTMfU/L/h8AcWsLMU9ioPSPE/8AIVB4Z1z/wAFguk+F/N/xsGf6xrFa8J7FW8JmKzGFaYD3jG9gPMI8U1Ff8D7niv+v+GwR/j7p3TKD+AitXFXEr3A+MAytfo+Zkr/xsWf4Y9sGL8z2x2du5jPqAUe/ShYoHMz6zWffX/DY9/Wwx7VQcCOkHlZ7CcLfSHudKK3x+LnTMw/uf4mXOPV2ltLIY98//ZOlW1GYAV8wGZ/4+dMXn8D3OJ8b9q/w2Df/uMTnEAFqPhxGlH7NnZctv/Ny3/q/Gfw+APLLDXevwvFaxT2PkqDkzxPxVjgG+rHDBwicr/hsEf6xo4Mj891i/LATPsGVbAHjlbzBZQeaZWA0TlmVb/33D4szpNbV/ttPl6mBfMFvDOmNAf8zNj3Sf95R6v0hrV0ZPiNVU9MBULpPIBzM+s8s1Inmnu8SqtdaRds4PVgFT+OuN/HgdQdYaUHYj+Kv/bPvwNe69Wj8rXU9jDFmjlbnS+pXLA7Iyp8r/tw9+wV7XgVA6YcXUVA8i4/1bOFxmnqPl/w+Bv2DNfrWGv6nWqOhD4jeUDbKUWHDtjwregD6r/f/b4e/VaPP6nuLry/1kNEK/eiKoDUfP/hsOf8b+2n57F6E9Rr3MsFmeszphYndnK/4bHX/E/FaPh5esBXxYLpGLMjf+pGHNSDzr3eJXWemJ1+5QPiNmBng9I5QGZjlG5hniH2AK5x6u01hHs58oHZP66FCvjf4yvwRZQPiDoGMUBlB1Y8/+Gw9+r16L8dZPqgXn5AMCe7S/Kz4w52bTc41Vai4LxZjrasGdYWZ4uw0rlAUG8emDqfNH4Xz3/mT3+wE/5gFQtOGd/nhgDqM4ZzK/AbAvonpGdmnu8SmuyTq9Xr0VxAPP/Mezx9xX3x3e8uyZatkXu8SqtTeX/a9eCS8WrBWD+P+YD8vgfyTHOPV6ltY54foD2/pyK5QEp/ufpGK/ObOV/24u/xwG8mv0qBhDYM/5nOkbdN6ByDav9Nxz+k2o2Kj+9qgVnPqBp9xelYzAvav7fMPgbB2C122x/nrZmv+cHUPcNWM0J5nOo9V+Gwd9yNdk5jfmAGPZ2LySr2aGwNw6g+B/TMeASmBeV/w2DPzunmRQDqHI1rS6s4gDKB2T3DbCz5cr/hsVfcfVJNTtS8e5umsQB2DwT90zmHq/SWkeAPdZp677Vsdg5jRcDyHy/5gfw6gxNcbace7xKa2Mx7FWsrsrXs1hdz/+ncoxVrVHlA6r+v+Hw9+5sm+Sn/yT1wNRdE1ZbIJ1nNf9vOPxVDCAwsBogqma/qgUwif95HIDpmJr/Nwz+WLuqXgvWPYvZs1xNlq8H7JH/xziAV2tGxRnj39Xak3KPV2lNrlPoe+h2705Qxv/InW1RjP+xMyZgz84ZgD3mUmt/yT1epTWpo5WvVtmB5gf4JHfCsJxgO2NKbIHc41Va64nVbZj23i6PA0wbB+bEGOQer9JaBw/L2WL8D/pe8T+Vr4ff8EzdD67qDNX4z+3HH2vNiwNj8TlOnu543SvsVZ6p8v1intT4z2Hwn+T/U34AdQeA8b9pzpbRv+UVKf5X/X+zx9/qtbD92eMAwErVgQAHYPsB+sO8YLaF3TeeCuYk9oma/zcM/sn96mNRd3a374RJxfgfOw+ETse3mI5Bf/gWW/cJn8w9XqU1N16T6Wh1L6Tw10XxYgzQn6oJZfyvJbnHq7TWE69mv6rX4sRrjrm/2l+8PFOyv+Qer9JaR6Bn1X19xteYD8j8dSwWCPgyDmB1hphtgd+ZD6jyv+Hw93y1KldzEvaM+0OU/8/OmKr/b3vx30qsrqoDz/ia3QWscsCYzm/HmKeC/QDzqeI/e/wtVnca/mcxgMwH5PE/8/+x/UXlgGGe1fy/4fBn/r/2fQ0qRkPxP3UnDHQ65oV3z4iqNVP533D4s5g9qwOh8oCY39/j/hZjwLA3/sfygMgZU+7xKq31BLacwn7S3fAqH5z5FSDqjMn8AJX/bS/+Kla3zQGYr1b56+xOGBZbZLVGPTsw3Q8+BeNVWhsL1qniAHZOk9oC7bug1P2t3r2QKfbtM6a0Pzyr/p9h8DcdzTiAd2+zqtnh1QK3nGBVD5rVmDL+V/GfPf4qV9O7s83OZz3+58UAqnqTqs5s5X/D4a/OaSxXM8XKbDPG1Y3/Me7vxZh7tWaS/ILc41Va6+nodj1oxdcY9nYnjKoHpu4DUhzAdEz1/w2LfyqqbvtW+B/T+Z5tYbVmprhvPPd4ldY6Yj4gZZupfD1gr+6F9DiA8gHZXROpVP/fcPhbzUbFAdSdbapuu+WDM/+fxZgzP4C6ZwTfrvVfhsHfq9lod7apHDDF/1gdQIjVg/bqDLE7YfBvrPG/s8ffsGdc3YvX9O5sQ5yfygNSdqB33ziejfRI7vEqrUm+pvif+etUrqby/3n3gij+Z3cA4N3RPMs9XqU1eU4DjFU+gOJ/Kv7TywdQsUAWY5DYArnHq7TWExUHNikPSPn/jAOoMybFKSr/2378Pey9+xrUnW12LyTDXtUZsrsGWQ5Y5X/D4T8pTpvt9+YDYuve+J+KL2E5Zeb7ZXwS367xf8Pg7/mAlG3WrtmfitXvUvfBqvxydc8knuHfVuu/zB5/xf0hwB42u8KexQCSnK2xeHdNoD921yDmSc3/Gw7/SbG6zA9guZoqX4/pb+P+bH/BuvdiAFv7S+7xKq1JXy1wZHe2AXdWq2crdwOq/UXlmVqNqcr/hsM/FXU+267bMO3d8KoGiPL/mS1AzoJzj1dprSNWszHlfxAPe+NrLB6kdXbbEcX/vBjAyv+Gw//KFd9PPwn7VIC5Wvf4+3hP+ZmV/6/6f2aP/4WLlyMOV69eCTfIfm/ns4qrqzqA6j5Y0/nT1BjGPEOfNf5z9vgffvLb4amnnw0vv/qPnt4HHl6+HvP/eTlgVmOKYa84QK3/Miz+n7vrkfDZOx8Kjy6shu9+/4fhz3/5azh3Hnex327G/hqN0beaUFu8sy0KfmP1XyCYRyrPFDyktb/kHq/SWlg+cDQsHXgyfOXh5fCFe3eGex9abnTCt8Jzzx8Pr//vZAcPi81i69TzAZnvl2Hv1ZjCO/heS3KPV2kt7Np3JLalA18Pe1aOhgce2x/u3jEf7rxvIc6LZ577RfjXv/8bB//mzRtT1+w37q/sSpVjLO6FzD1epbUx/rHtPbTR9q2HxxfXwv1zK+GeBxbDyuo3wtM//kl4+ZXXQipevRazA1UMoGdXMj/zp2C8Smsj7I+Ghb2Hw/zSati1fCj+trvRBWg7lw+HBx9fCQ8+tjfseeJY+NGzz4dTZ86OAYFt5mGvYgDZvZBejDn+XPnfQPgD+8XVOAc6+qBpC818mF9ei88eXzwU94e53QfDT184Hk6cONGz5SHAnsV8QOx8UcWXsPw/6Jha/2Ug/Bt9v4n90T72SwebZ+vj3/bsP9bohEPhi1+ea/7/SPjDH/8Urrb29ngP9OkzMv5T5Rh790xiTozsh9zjVVqLOn9h76Heut8VsV8dYb85LxYa2wBzYnH/erMvHAj3PrgcvvO9p8J/Xj8R9ffly5fcGiAqvgQ2oqozVPnfcPhvrPtjybo/vLHu960nOmEDe9MJS0882fzd9fCl++ajvfji8Zd667eNvZdf4NWYaknu8SqtjfDdxHi+Wfc7O+v+6GjdH4l2ALA3mzHaic1+sNjYhffcvxA+f89c+MFTz4TTZ86NAVO+XwiwZeeLlv9H6gzlHq/SWmfdz5vO35fagesU+/F+0Dzbs3IkzO05GO64ey7sX/tmePW1f0bALl26KP1/6i5glQf0KRiv0tomjpEDHKS2QMR+cbVjB24+Oxifm22IuYIzg51La+Gl3/yuhztkUg0QVmcW86La/8PgD+x3Lh9M+F+zxvfCFliLGLNn0Q5Y2jgvsme7V45FXfDA3N7w0M4D4dcv/baHPbtrwqszC+yxj1T+P3v8N2w9zgGA7SYHSNb90sa86L2DudToCpwpzO1aDTseXQkvvPiriCP8SV6tUXXPCPhfrf8yDP4btnyf+0d8lc5vni2MdH73/Hh99M7mXHpk4WtxP/jZz38Zrjf437rVxderL1bjP7cB/966PxrxU2dCG3bgWkfnb3LD1d68gE3w8PyBcMddjzT2wO972Fv9F1ZjitQYzj1epbUe/1vonfu0n621zgW62C9E7Mke0uwvu5v59NVHl8OOuf3hb39/ZYyxxQJ5/K/6/wZt/wcYlULHvcgUDQAACrVta0JU+s7K/gB/V7oAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHic7Z2Nkds4DEZTSBpJISkkjaSQFJJGUkhukJt38+4LSMlZrx3beDOe1eqHpAgSogCQ+vlzGIZhGIZhGIZhGIZheEm+f//+2+/Hjx//HbsnVY57l+HZ+fDhw2+/r1+//qr32r5n/Vc5qgzD+4G8z+L28Jb+ubu2jtVvJ3+uR1cNez5+/NjW1Ur+7v9sf/r06dffb9++/fzy5ct/+qL2F7Wv8ikqL87lGOeRTv1crtrPsdpv+ZN2nVtpWl/VsWHPSs6d/i86+X/+/PnXNvVP/y25lAyQOTJiP+dU/sgUmdf+bBf0a84lP7cT2gLlG/bs5F8y8viv6OTPMeRCf7UMkXO1FfdZ5Mc14D6+OoY+AMpjPTHs2cn/rP5P+XfvDOh55F5/qy0g19q2LP3MWMnfegDo+5WedcPQc035I9eSVV3rPkhf95jAefhZksd2uiHbifWM5V9txGkM/1J14v5ztB9dzVicbR+nX2f7KVlZ3ikP+m3mXdd5LJeyrG3aIHqGMcnqmmEYhmEYhmF4RRjH35NHsNen//NvL+9Z8t36Hlzqa7o29a54hMvo7WoHz+ZnSJ3wlva+u5b38538z9jxj3yGeZ73db7ELr2V/P+G/vMWXP70s2HPw6aOTSb9d+nbwxfka+kjnc+Q+iQ/zl35A03nb6SMXI/9yL4s2y/t39qll/K3H+JR20DK3342H3M/KX2Jziy5IBtsvuznnPQL2GdYICPsdgXnUee0D5P2Z7cd2gz3Qp6ZFvLu7NmZXsrfdfSo44Gu/wN1aL3gvm0/jn17XYzQLn7IfdB2X/f/SjvreOdvzGdK9uv0WV2S3rPrf0C26QMu7KspmeFvcX9Dlvy/kz993z5Ax/tYn8DO35jyJy38AOTTyf8ovVeRP8/2+puysbyL9MXbF+f63ukG9InbCbrFuhh2/saUv8/r5E+cypn0Uv6c1/nD/nbsW0s/W0F9pT8t/Xf27eW11G3R1ZH9fTxHyGPlS4SVvzF9iLyndeXxeOZMet6mHh5V/sMwDMMwDMNQY1vsm/w8Pr9nXD32gBljvx+2ffGzTb6LC70Vf8P8w2dnZ9Pq/ODWCegOx4Tn3MD0LUJe6/NrX2c/zPKgr0Y/nKOzqyD/ld3XdjB8fNiO0BvYfz3Hp0i/UMbu22fnc+y34y/HaB/YkfFJDcd0/dx+F9d7kfLn+m5ep32Btu9a5vgPunlEnuuX88/st/M16Ijp/+dYyX+l/1d28PSlp08dGyntIvuxYzDOHMt2WeCT2MULDP/nWvLvfH7guV8lL88FLM70f3BcgMvJuXnOsOda8i/Qyek7L3iGF9bhznP1/F/pBrc5P/8dq1DM3K813btc7Vu943l83tkCGMPn9cSNOJ3Uz934n2cA5Pu/y8qxTHvkPwzDMAzDMAznGF/gazO+wOeGPrSS4/gCnxvb3MYX+HrkGqvJ+AJfg538xxf4/FxT/uMLfDyuKf9ifIGPxcrnN77AYRiGYRiGYXhuLrWVdOuGHGF/Ej9sxPdeQ+OV3xF2a62s2L0jruD93H5l+5DuKf+0MzwzXtcH2xu2ucJr8KxkbPljf8Emt2pLK5uc5W9/ImXy+jwu48qeYJvB6l4oM3rM8s/26HUKn8GmbNsrNrv633a07ps8mYbXEMOvhw2+azdd/y9s02MbW2D9T9r2+dBufb3X5/KahKvvC5FHyt/rjrEGmtfEenSQEbhedt/kMil/PztXbcZy9TWd/B1v5GP2H7Of/kl67D/6vpiPkU/u93p494x7uSbYxyH7hWW5ei7+qfy7/Z380xfUxSLRr9HtpH/0DbndMfwU1vPkwfFHZ9f/7Xsr0o8Dt5J/1x5s+3c8Af09fUfdvezaRsaokF76KR/1nYG27HpJHXDkR7+V/Auv40vsAKzWnM57zXvZyd9lyO8L+5pHlX+RMTLpx9utr89xr6eZaXVtZheXkz6/Lr/V/t19rK7N6/Kcrn6eYew/DMMwDMMwDLCaW3W0v5sr8Df4U3ZxrMPv7ObWrfZ5zoXnCh29P96CkX+PfRi2oeWcGlj553ftxbaR2nbMP9/lsN+p8PdE8P+Bj/la25PwLXEvlj/fs/E9v+o8EcvMfraMm4cj/d/Z5q3/2ea7PrbT2UZr/4zbInH++HqwAXKtv1Hobwk5xsRypiz4iO6tp27NWVs7HO2nb+Y6ASl/QA+4LWDXpy3YN4v8KHvOG7Hfr5tT0u2n3fq7QK/CteXf9Z9L5O85H+ju/Nagv8m4k38+DzqfbsEz6RXnCl9b/18qf+ttdLBjbezDQz7kcaT/U/60jUyT+BDHCDyyP+cSPG6ij9GvbiH/wj499+fdPPK8Nsd/O/njx6v0c/z36P7cYRiGYRiGYRiGe+B4y4yZXMV/3ord++pwHXjntj8w14u8FyP/NZ7f4Ph65sfRj5mDY79dprOyoXgOXvrqbIfyvKCVD9DHKBPXZvmx/zp+H5+my9PZo14BbKBpD8Vu5zUaOa+zqReeV8fPfrdcOxTbP3b+bo6X7bv255I2Zcxypd/R/b/zVWJTfnb5p/6jXrn3VQxPN08o6Xw7K/lTz+lH9Pw0fD/YZu0ftP/Q97YqP8dyjpf3V37PMs9vxU7+ltmfyn+l/1P+Of/XfmSOYavnmOfy7taH3MnfbRRIizb27G3AWP9b/91K/oX9kH7Ocy7jEtoDeZzR/5BtgzTZtk/c7e8VfEIe/61k/J7y9/gv5/jZB5j+wWI1/tvJv8h5/t3471XkPwzDMAzDMAzDMAzDMAzDMAzDMAzDMLwuxFAWl34PBB/+KtbOMUBHXOKfv+TcS8rw3hDfcktY/5i1czJ/4rEo36Xy57qOSuvstxa6OJSOjCc+4pJYQOKWvA7OUaz7Uf0aYqPg2nH0jp3yd3iJC+xi9ymTv+vuuF/KS3yVj5F2zhcg3twx547VTbw2EGsIZZ9lLTLHm+/6NfmfOZfzHT9LXo5FuqR+iTnyz7FR77GuWa7XRrk4lut/EQ9OP+V+Ozo9SjyX79vf/qEt7HQA8brEknlOQd4bx+lnu/5D/o4JXOH7Tv3iWMpL6pdzKSfpXkv/Z1x+4ucyfZs27X3Us7+34e8puR7cbl1Pu/ty3h1eG8z3s2qHfoYit+57H3DmueL5Mjl3gDaUHNUv0C4cn3otdu06+yv9x/+j87JNe95Xlx79j/tKWbmvWvetyuq1omAlt4wN7dKkbDmPhbwS55XtnraZHNWvzyNPz1V6K+jBVf8/O+79E/lzjufcZJp+Hnbx4E63m4dEnec3Ki5Z56sbK3Y603llO/T4OMt9pn7p/918hbeyK8OR3oVO/jl/o+DdwH2Ve0LGniN0Bq/pmNd47pDj1a1zj1jJv2uvjFOsH1btm/wv1ee7dUo9b+oMR/2/8DyL1btMJ/+jsvNMrPI6D+REXbI23GqsZp2Z8mdMmOsEep0vryvYvVt7jpnfHbpy8N1D9E2uWddxpn7h6Fu7HHuPeYu8o67yzXkaCWMFyHpBv6fe9Lv0kd470+5374SrsYDHOZesE3rJc3pXv5T7SK6c8+zzVodheDP/AKCC+iDgvyWjAAAWl21rQlT6zsr+AH+HMQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAeJztXemPHMd1zx8ViRQlXuIhnjszuzs7s1xZ3HOGdBwgkBTDjq2Dt0SJlkXSOe0gESDEH4IkXwwFAYIAcaLYzocADpAgMQwhlETxEMVzTx6V+lX1666u+r1eMh+DKuCB2q7pqlfvqndUtYwJ2oMHD8y1a9fMl19+aR4+fBh2meXlZfP555+bO3fumLjdunXLvYf23oUfmk07Rs341FEzdmhg2hOzptObNaOTgwjmfV8/7ev0bV93xv47FzwfOmj35ux7tm9yIXpvwT8nc7nxenhnPhrP4md/DzxS/Dzu7WQ8+15/geDnYczicXD8sNm4rWVOn71grl67bn7y0d+b5/f2za4Dk5Ym8/a9GL9ivF46nsN9gs01LHCfcbRMcMd6ybpi2sa8BzDef/bZZ+b27dsJ7+/evWs+/fRT998//fgXdo1TbtzxqWFA24U6/YTujLY1Pg6j9Xreg/4p76cLWtTH47IUjdeIH+mbYLyCXMyZVnfaycDIxLzZdXDK/PYrr9vfLpidB/qW9wsR76v1Yq5OMt68W5fHPaVFy/aNJnogMj2d8j6YS56hPXr0yNy8edNcv37dyUHYVldXHX+h44z3ly5dMktLS2ZxccnMHX3V7G2/ZHl/xPGv5XAn+AGHRC4GXq/AR6LDnPfDYLyY94OCfjPu3TodBg4/N0+C31Dh/cDxrhXRL5Qz8L7d87o4duiIex8ysH/0JScbfryAj1hvb8YB7VPkrNODPZsmuA+czre7TC4WTKuX4i76Dd2O9X5tbc3ZfMhG3BYXF51NAO/RPvzxX5ud+ycdvztioyM75/EreK/YwPYEsYHgh9PhyM71ZbxYhwveaza1sJvpHlLYhF4qS/htS7HD2FfAez9XXU/xe0+LWJYWHJ/oehW5DeWMyS320pbTkfX1XgA8X1lZSXh///5988UXX1Dey34AGUC7fv2Gmf/675rdVtZlvaF+1mTT0X0Y9Xk9YDbfjzftZD7lI+c9aOtsD+XHfGEbU1+gwp3tBzNkXUPPx3KuGD/G+6GzP+3urKNHJ9J7x6tSbiP6iS9AaeFtfjweZKHuFw1r44n9j3l/9epV89VXXyV9kJXLly/X/MCf/K31b/b1nd/D7Kan3yzXUyeb3EesfB/m38wG+18kS9R/GHi76XxONt5c0cdx7zibUB9v1O0H04qfNUfseuhbxOMNC96z/W9Y0mKU+JydRr+I+1neL5pOdFv8QMZ7+AJXrlyp8f7Rwwfm5FvnzeZdY2akm9pUJ+ul3sc0qnzstE/nfUeRpY7sB1SWNBqJXPC+ileEtpRXA+cDVPtVaufQN6r0dZL9YFj6MaMJLYaqvRW6a3ovOsJ4f+PGjWQ/AO+xH8QxwK8/uWT1fsbGN13n31K90ug3wX3sx+L9Y8v6wO3PXv7IeEJbarPmqG43xpo98dlj/lY+bOoLhLxie9ksjTUrmY7XtRDY2/idOp2kgd9a/Afew+azGOAf/+lnZuvuMbOn9TXL/8ie0fiqSYeHjXuco19X02FFlnoyHvGXFH6UfXQvs/6tst+PlnIb70nDYr16PJn6eoMg/mN6MK/HV8pcXg+ma+tyNtzaeeg18j6xzZcYgPEe9uKPfvSB2frChLX9czX+e9oy/IZG87+bch0unuxO87xPsd54D2mr9BM5Y75AmGeI7YjkGZp8lQj3fuX7JLj39RyTxAdOzg4x3LkN7Li8QLzXDgs80jwD+K3Ff/ADoffwBeKGvnuL98ypt89b/e+WcW+pi12mV1UMkMZ/If3iuMHnVeh4XcklDKP1LjTsIYWvR/NmWgywUMT+PMcksX8igxPeL2/HuPdF75kseb+yrcQvzu9NbIweD41KrEl8WOgw+B/zHs+x38MmxA0x/40bX7rffOfYWfPM9naAexj7H0np7nR4UMdDYn8a784V8W7THhfFk+UeksoS/Od293A0nn9fbJZqo5UcUyvx9SRXLbJO8s7dWSJLg0LOuF/pbH5XZCmlbYvsPVVMQXxE+9vY3st+IH4gi/8Q+0vO4JVvnzDPPt9xtr+e64hj12LvZvGuwqvKlqXjQa9aE+l4ZS6Y+oFFDifxLUTOlBhA4b3bT3vMZhW0SPI0oVzMRrj7f33eQvFhu5IHIXpV0v1IhUMtL058AbtejffIBa/nC6yurTr+P7dzrKRTbG87CX4hjST2V3JgTtaZz879yk6Z62ioo7DcSc1vS3nf0uZye5Iea9K9pz/rcz9ajoTWFRp0pB/GNrFezbp4OMkxBTmrsIUxQFwHAO8R+4d+4OraffPqt0+aTc+3PB1oXsXrSGwDS14x+hUx1KiW+6ZxQ7HeZLwwnuT5VB5DSUzGeD+v5KyCeEPJWbFY2Md4M8VcsbwUuUwS/7V7HPcqxuNzhXQP9R57vRb/sToA+A/937TtoPVRWT5ai6212kal91yWNL3Xx6vm0mKAhjyDmrfQbH44HouFldyoYgNlL2O5hDIvzuphWl6c0F14L/Efq/+B9ywfiJrfN17+rtv/xw8xu8Tz9rzmPix8KS3/qcXqkmtV6Edt6qD0o1Xe0xqLjNcQ/zXkbmkc2pstckyKnE3E8d8gyDMQuZBYk+YF9Pofcrqs/ocYgMV/kJN79+6al7913GzZ3U1jf/jzxGfHftTqkf2v0Hua5yr8wCSGKutamu5w3o+63ElTflGrWxe56jh2Leww1pvksdUc08Dl9fQYQNYVxy9ajkniPyVOVuik1f/Ae8T+iAHidu/ePXPr5lfmvrX/3/zuaev/jZb8r+vpkdqa2lrsPym5DlazmQvq2XEMMONzozH9xBfQaqt0fxlWep/kBaQ+OVfULiPb0xU7ovjl7NzRBMt/Cp2mi/wEoUVhszrReL4Ontb/OqUecJulxX/w9VjsL+fAIDOwAa/+3smS/6XNp7lvnjtxst6V/TmM44elTWC5Dlf/I/IivrJWU6riv5iPms2PcW+q/YZ98yRXHZxho2ef5NwWq5+H+xXJM5T5sSP18cocU2pvW6T+J/EfgNX/YBOkBrRi4z/Pfx//eZsV47dO7bcnZz4I7yfiPaQ4L1XL9ac2ga839KPjfBFi/8Op3Bb52RbJnVQ5Na5Xqm8xEdatmY3WajYh7ys82jX7k+oIYk3dh51NeI+6P/Z8Fv/FNaAy/tvRdmPF5zU7k+uf2WO5Dj1vL3ZTO6/J9ar0e2kNjftmHRlvIvZVpI4yQ2LD0Abq9bqk/idyy+q4pR5EZ9j69f0g7tPqf6Ml7vXzn2jI+YDHyO3Hei9+YGgTVtce2PjvpI//WC69P1fYMub7zFC/t4pPtbwPf4f70UGeoSHWVGuNap5BOZfX5OcXZ5ApnRSZboprS9+iR3wLWv+Lz0UNSv5L/AebDz1/HN6joQ7wjZe/Y57d3kriP/VsclHP1uooXHcWyvM5TO99/Mfr9C0tx9QPz+zFc3HcG/nR12xMWAfn+0GH6ql2xiDMMSlnmqmcyR5Sn0t8Otj1WO8l54e8T8h7/PcjGy/cvXvH6v8xs3X3eD3+60vensd/lI+Fv0Rj9Z4WJ0m8pp3RUO4eCO9prCnxc/RcYk1q17W+ofOHtNqlP8Om857TYq7BJnC9d3NRWariv3i/bzoDiHOfN23899C+c+zM953/X9NFLU/TyCse7zbFz9W5MsWvVHjfpnof1K0Vf6mqNYZyod0vqPSA5zJnfb2J1WrduuaUXMK031c1uaB1hZkiVor75h3/Y/6G58DiBpuP/QA2A+0P/uQDl/9p4XxNQ16qqqMMoj45h9GkiyzXIfXOVJZaau1Xu09R6WI6l/A+PkcX3t3QfAGeZ2gXcubzIPEZwNnCjtTjIWfzu9NBXzBXkW9bH/c673GHIG5SA4IvGOeE5OxveAbwo7/7B7N9b9/sG33J+2BU1rUamsev1bSfTpJYveabkbpwT+QiOhPQDeONegzF8Kt0kZzTXscXaFE9KM4xTaRnINx4tdxyFAOUNqYuF22pA7AzC+W5ZW5v4/hPeA+7z+oA4H1cA/q3X/6H2/937p9w954S3jv/RrmnlOxJBd70fFN0pkJdL7P5Oq/atF4X507q+luN9yS5/jhXOEzw4z77fIRHmGMqzqtoZ9a7Ci16afyHPQA6D39PqwGx8yCfX75ieoePmh37evU1HRpUdo6cpdLu11X5AmY3tXNvmlwMyjwNv1/AzqQEMVSSY3oMuXA5xNRGj9byIMRGk/sAYS4z5W+Tb1GM9xj1EuE96j/QexYDyBnAmPfwAe6vrZoLf/hnZsuu6Aygy3Md5v6Isic11eJ9/MzvZ7Qkv53YC382q7FeF+dVBD/im3UacJdzW+q5YKWmXc9bDJO5dBuzTu1Sozup/2Ffx54e817ugDG9hx8Im4D237/6xM47cHc/ofdNOFT39UgdxemAkjdzeOtnOVO9HzgatZrO6rK8j8SMSo6O2xE5s9dQZ07mWijPrKfnoEO9185I87NFLWU/6JT1sDrdsefDvrOzv7AH2hlA2ATc/5V20dqAnQcOFf6IwqsyPxvJunpmpjrvynPB2v0CuUuq3AnW8uVqbnmdO2oN58Cq2J/Pxc/ATyv1qzlqs9x4tNYofbOFHqS1MpfLIfEffAF2Dwi8j++Aod22f+P+9/Y9kgti+cCGuzT0jIucr1RqG7TW3XxGo7rDy/OVLN6o3/2sZBP/VrFm1Cc2n9Uaa/mswJ+bjG10VGvU8gxSI38CWohvETfwG7zXzgKxO2AiM//88S/MxNeOunvA+PaH5ARL2ibfPvCxP7Wbk+H+nO4VNF6DHzgx25BnaMipsfvlwqsk/xT6ZjzP0FLqCtX9NXb/L6zZMB+RyFmvweZTORP74+Us5r3Ef5ofyO6D4zwI+tB++e//aSYP/5bZvGvczjHv1tnppfdR9Hx5cE6NntFo+LZAqcOx3uv52abzUh2pU5Ack3bGvH73IMYv3HtCPSjw67LvUdTjtYQWyXhBn1ITqXRuoeSh1P1Z7bfpHhBywfAD5VsAaJ/8zyXzO99802zaPmK2vzBW+mCwB/67GMU5By2XTvuq+I/7Atq3APTvvzSezynvCJEcRNM3jZpsdJKfDc8Cse+/1M/q1mkx1+AXKXc/J9N4Ungv8V9c/wPvwV92N1C+AQL9j9tdO96ff/hj6xO8Yrbt6bkc4d7OS+6uMGg3PrVgui8ecd+K8XJR3X9P42ex+drdHBZvhPlZ5Xsyiu6oOaaGO1vVeZCmXDA7xxTKbWqjWfzH7zCFcsZjL/btGjQt/oMdkHNgLP4D78MYQBrsBM4Hon1x5ar5q7/5yLzyrRPmhdaL5rkdIxZaZsvucbNj/yGzx8aMB91dKO8L4A5598WjBRxx/45Ped3Cv7W+Kdtnn49ZenSnhrV3upAr9JHxxqzsjfVnnY8yLr+XPvt7vIf36+PZPktv1xe9M/7i0I83Cfy+Xu9zPpC/G1tfU4Gf7Rt/AtyxTvC/e2iQ9I27M/gEd6zL0Wk+eUfuf8Y2H74A7AHjPX4fx3/SIEf4XtTS0nLt+c2bt82//OxfzZ9+8BfuzNiYpecBawv2dg5bOZgym3eMunMkG7e1zYatLQsj7l7hxq0HzW8+t8c8vfWA/e8RB9K3YcsB27fX/WZj8Q6+u/bM9pZ52j5/6rl9ZoP9e2Mw3jPbRuxzO97m/bW+jW68/W6uDdtG3PONxXibnkffPvuenavoq8azc222c23e535b69s64t552uK5YVvb4y742d8/9azFY8tB+/u2f6fow/OnIvwcLQR3i6efq04L3wf86nNhLIf71hD3lqMFeBvHeJILZnfAmmIA7ANhbbAmM9Ze3L+/6sa7dfuO+ezyFfNfv/q1+enHPzcffPiX5o9/+KH5/sUfmTPnfmBOvn3enH7nojl2+j3z+vGz9u/3zcmz591z13f2onnj5Dnzxomz/vnZC+XzU+9cMK/Zd46detecwvOy7wfmxFvvm9ftOyfOfM8+832nZLxT37PjvevnKN7B++jDeG+eOlf0FTi848d77djb5vhpGc/3nXn3983xM8D9bTvXe/b5RT8XxrPvvXHyXbu2c8W6Lrh3sV58RwVz+fHO+/fsb/AOxnvt+Ft2zveKNctcfrw3QYu33i/ekbnsuo6/Y2lxLhivwh20jVt4D6jpDhjbD2AT5Htg4XiIGVhMgbaysmzWVlds3wOzasdfsfK1srLq/sVYsCPub7tHefD9S0uLZml5ufy76lsxS9YvWV5eiZ6vumcYb3kl7VtaXgrGC+ayzxbZeKv+ncXFe7TPz7XE5wIOyyt13N07yx732lqDvnK8lToeS4uuP6UFm0vwW0psdMj7prNAcZNcMPMDISt4D3YjbJJj4t8WWHN9sSyhYc/x3ylMZQlzaXdVsC62X4FW169fc/+y8ZgNtJRy99+ZDQTu16ysLwXxkDTQR8Pd+UzKNzagO5CzuIE+zGdHgz/P8nePHj10uN+N8nfCe1b/C+8BaXWAmPfOztv14L2Ytut9ZzY+YyAN/GPjYS7gpsktnmM8Fr9gvMWIVzIe+phPLLhr38fScEdfjLvk29i3N4XuTOcwnmZvNVrgbznXE86FdzCe9o7UgOImvGd6BZyBH1uvnC+J14vfat+blByTxnsmt8Adz7X6FdbFbBbGw1yxXgF38J3diwFeGI99H3k93rM7t+y+dUgL0EnjPd5jcTzz58X/Z+c/hX5NsT9bL+S/iffAQ/vGlMZ7tl408FbTUzxn9ltyVprcyv0mhrvGKy0/JjYr9onFx2ZnbSTnwnQOuCO+YvYWuGMutteCj+xMF/pA1/h5GP/FTfSU0Q/ygD5tvdr+Alli9MM6w2+Nhg2/x1xMT0EH7VvWoB+z0dAB9MX0QwMtgLv2jSTtnqQmt6Ary7Xib4ynfXdHoztwR1+Mu/BR0zlW/wtlne1xmIfRT3gfrxfjgT6gH9NTbb2Po6eM9xqv5Ns1zKZiPfiWtSa3TXnxJl417S8xLURumc0SudVw12yWxnv5lkvcxA9kdkn2JE3WmS+FJnZJ86XY/iz3TDUbA/oxWddwb/qGYZOeCq+08/GMVxILa3qq+RbiC2g69yR7I8YQnXsSPgJnrJnxCs+ZH+1jKB6vAT/ME68X44GPLJeAubFWpjuQC/CY+YEYD+8xGw0cmK+CcdDHcMczRguJbZrmYnqAZ8Cd7S9Cd7Y/Yx4Nd4zH5hK6Mz6Ch4yPq0XOhfkCoDuAnRUB7ix+xnjAg9FP8hls70EfoxFkiI0nc4EODHe8w/grvg+Ln/FMo4X//x0sUlrgOcMdOINObK7VIr8Tjyd0ahqP9YEXwCPWe4yHPo2P4g+wuRjN8Vs8j+cJ+2L6yXjaO6BPUx/DA3PgOaOf9k7TXHiGPkYLoRNr6GPjAb//y1xNdF9vXRotIhx+I0OGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZMiQIUOGDBkyZPh/CP8LtkXEO3kbMY0AAAS3bWtCVPrOyv4Af4kQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB4nO2ZaW4bRxBGDfgEQe7hk/kW+anz+QLa910iKYqazKPdNlP8ehQTBowEr4EHiNPsqq+reqmhPgzD8GGC2N7e3taktlqtfrqvPZ/q28VX7/lUX8/elJ9dfPXm+6tjO9U3PpvKfcz/crkcZrPZ8Pr6utU3n8/X1Dnj+/n5eVgsFls6sNOzx/fpSzHED/1prvhC5y7aqz7sNV+1D3v4mtJex2zGorYWi12093zx/OXlJdobx72XfxEREfn/8Y9GHXF9fR1rk4eHh+Hm5mar/qA+4vn9/f1W/UHdcXt7G+09PT2t+2qtgw18Qa378M0YxtZGndPT/vj4ONzd3W3Z4zPP6a/asdOzR/2GjhQL4jDyefy7xXivaSdOjE3asZfqNOz1fKE9xYI89uLOXFPc+e7Jyck67rWhm76qD02np6fD5eXlVmzRcHx8vNZfG7rp4zu1XV1dDWdnZ1vzRS++0FJ9EVPspVgwV+xV7dg4Pz9f+6u5Rxe+UiyYD7GoscXGxcXF2tdo+/PwY4/t4ZvnaK9tSjvrj3E1V2jneU879shzT3uNO7HGV8oV8Ts4ONgag19s4avmg9gcHh6u9dVGTPf396M+4nd0dBTXGX7oT7nHXk87Othfm63FL60zfKMh5QrNxCLtYeZKPL7l6nbkr5E9PqOdtZa0oy9pJx/Mq66zTe0p7mhnbG3Enb6qvb0vpzm1+aZ92nKV4sd8U+6xx3xT7tFMX13r7Ywhfmmtoy/lijMm5R4b2MJmOmN62tmfPe34Z1w6Y5r2mit0ET909rTXuLfcY7Pam9KO5pT7ZjP93sB8Wc8pfuQKDXUcn9HHfVBz1c65dKYy33S/tDM6xa+dc+zx2nprHU1ln35v+OZ5OlNbrpJ2/KMj7VP2SE879nra07qlEdfeum33cIo78+rdL6kxBntpvbDWU+7RxFyxmeI3dZ+yZtJaZz6ptiDW2EvxY60z36S9rdupOiZpf2/dptxji3jUXG1qr77aPp2qY3px761bfKUzC3v01fZeLdWrA1nrkNYfftIdR0zpS7UF80n3fauliH311c6sqr29o+wav11yj/bqi3kyppd74p72/VQdiJ+edsb0cp9qYnKVzrl25tNX59vOfDSkO475pjuO9ZDmu5n7VFv06ui279O6bbmv2rHf7qt0xvTeh1odnXKFr6/7YPXn+BG+8Hz1bZ2lWLQ6q6c93VdT2tv7y9S+r7GYel9rtVl6b8AWOlJtxpifueOw0e64NN9efTN1Rrf5TtUWvXeoqr39NpH00ZbLl2G+/v/N66f5YvFx5I+RL7PZfNxbX/8PwzyqP3K8WGzff73ztmlP7wD/5r17I/e/+/cnERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERER+e/zNw8KUFvN57hsAAAO121rQlT6zsr+AH+SgQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAeJztnY2RHCkMhR2IE3EgDsSJOBAH4kQcyF7p6j7Xu2dJQM/P/livampnu2kQEgjQg56Xl8FgMBgMBoPBYDAYDAaDweA//Pr16+Xnz59/fOI696rn4nOlrABl+PfB/1Hp+Yr+M3z//v3l06dPf3ziOvcyfPny5d/PLr59+/Y777A3ZQT0+0dG1Pu0npWeT/W/AjbR/q72X/VR+naVppPX7d/5nV1U8qzkBF0avV6ly65n7bx7PnBq56t66+wf5Wvfdbm0b3semg95Bar+r3ll9Y77nz9//vd76C3S/fjx4/e9eIa6qC8LRDq9HukzRP6eJvKIvLkXZateSBfX9XnqoGkjL09HHfR6/I3Pqv/H369fv/5+7go6+3NNZdHyI02UzzNZnyM99zL7uwxRntsIm8ff0Jmmie+MW1xzPUUanfM4tH1FPqRHF8ip6VTu+KAL2rLKHddUH6pnLZ/xfdf++swVrPx/VmbW/+l/nbyBzP7qb6hTVnfsHHpWfdEu4oMv0D6ofoE8VnJ2ukA+yiE/9xVVnf35kM/L3xn/7zEXuMX+6Dz6I/Xu5KX+lf19HeLAttg9/kZbIH/+936GrPRR2otC86FOmS7wty4r7ZG5XmV/ZNTnvfxMbytbXMUt9qcda7vv5A1k9ld/h+/N+ih93f2P6jbucd39JL4jsz960DaW6ULTqc1p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');

        $("#hdIdEmpleado").val(0);
        $("#txtPersonaNumeroDeDocumento").val('');
        $("#txtPersonaNombres").val('');
        $("#txtPersonaApellidoPaterno").val('');
        $("#txtPersonaApellidoMaterno").val('');
        $("#ddlPersonaSexo").data("kendoDropDownList").value(0);
        $("#ddlPersonaEstadoCivil").data("kendoDropDownList").value(0);
        $("#ddlPersonaGrupoSan").data("kendoDropDownList").value(0);
        $("#txtPersonaUbigeo").val('');
        $("#txtPersonaDireccionDomicilio").val('');
        $("#ddlPersonaCondicion").data("kendoDropDownList").value(0);
        $("#ddlPersonaSede").data("kendoDropDownList").value(0);
        $("#txtRUC").val('');
        $("#txtPersonaFechaNacimiento").val('');
        $("#txtPersonaFechaInicioLabores").val('');
        $("#txtPersonaFechaFinLabores").val('');
        $("#ddlPersonaDependencia").data("kendoDropDownList").value(0);
        $("#txtPersonaCargo").val('');
        $("#txtPersonaCorreoElectronicoP").val('');
        $("#txtTelefonoCelularP").val('');
        $("#txtTelefonoFijoP").val('');
        $("#txtPersonaCorreoElectronico").val('');
        $("#txtTelefonoCelular").val('');
        $("#txtTelefonoLaboral").val('');
        $("#txtTelefonoAnexo").val('');
        $('#hdFoto').val('');
        $("#hdIdUbigeoEmpleado").val('');

        $("#divExiste").hide();
        $("#btnBuscarFoto").hide();

        if ($("#divGridCuentas").data('kendoGrid') != null)
            $("#divGridCuentas").data('kendoGrid').dataSource.data([]);
        if ($("#divGridOrdenes").data('kendoGrid') != null)
            $("#divGridOrdenes").data('kendoGrid').dataSource.data([]);
    }

    //this.NominaJS.prototype.crearCuentaBancaria = function (e) {
    //    if ($('#divGridNudosCriticos').data("kendoGrid").dataSource.data().length >= NudosCriticosLimite) {
    //        controladorApp.notificarMensajeDeAlerta("No se puede agregar más de " + NudosCriticosLimite + " nudos críticos por compromiso.");
    //        e.preventDefault();
    //        return false;
    //    }
    //}
    this.NominaJS.prototype.inicializarGrid = function () {
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Empleado/ListarEmpleados',
                    type: 'GET',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        data_param.Estado = $("#ddlEstado_busqueda").data("kendoDropDownList").value();
                        data_param.IdDependencia = $("#ddlDependencia_busqueda").data("kendoDropDownList").value();
                        data_param.IdSede = $("#ddlSede_busqueda").data("kendoDropDownList").value();
                        data_param.NroDocumento = $("#txtDNI_busqueda").val();
                        data_param.Nombre = $("#txtEmpleado_busqueda").val().toUpperCase();

                        if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO) 
                            data_param.IdCondicion = "5";
                        else
                            data_param.IdCondicion = $("#ddlCondicion_busqueda").data("kendoDropDownList").value();

                        data_param.IdEmpleado = 0; //$("#ddlEmpleado_busqueda").data("kendoDropDownList").value();
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
                    id: "IdEmpleado"
                }
            },
            group: {
                field: "NombreOficina", aggregates: [
                   { field: "NombreOficina", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "NombreCompleto", aggregate: "count" },
                    { field: "NombreOficina", aggregate: "count" }
            ]
        });

        if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO) {
            this.$grid = $("#divGrid").kendoGrid({
                toolbar: ["excel", ],
                excel: {
                    fileName: "Listado de Empleados.xlsx",
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
                    //{
                    //    field: "IdEmpleado",
                    //    title: "ID",
                    //    attributes: { style: "text-align:right;" },
                    //    width: "30px"                    
                    //},
                    {
                        field: "NroDocumento",
                        title: "DNI",
                        attributes: { style: "text-align:center;" },
                        width: "30px"
                    },
                    {
                        field: "NombreCompleto",
                        title: "EMPLEADO",
                        width: "200px",
                        aggregates: ["count"],
                        footerTemplate: "Total: #= count#"
                    },
                    {
                        field: "CondicionLaboral",
                        title: "CONDICIÓN",
                        width: "100px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "Sede",
                        title: "SEDE",
                        width: "100px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "NombreOficina",
                        title: "DEPENDENCIA",
                        width: "300px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "NroOrden",
                        title: "Nro OS",
                        attributes: { style: "text-align:center;" },
                        width: "30px"
                    },
                    {
                        field: "NombreOrden",
                        title: "NOMBRE OS",
                        width: "200px"
                    },
                    {
                        field: "DuracionOrden",
                        title: "DURACIÓN",
                        attributes: { style: "text-align:right;" },
                        width: "30px"
                    },
                    {
                        field: "InicioOrden",
                        title: "INICIO",
                        attributes: { style: "text-align:center;" },
                        width: "30px"
                    },
                    {
                        field: "FinOrden",
                        title: "FIN",
                        attributes: { style: "text-align:center;" },
                        width: "30px"
                    },
                    {
                        field: "MontoOrden",
                        title: "MONTO",
                        attributes: { style: "text-align:right;" },
                        format: "{0:c}",
                        width: "30px"
                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: '',
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";
                            if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO) {
                                if (item.IdCondicion == 5) {
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroPersona(\'' + item.IdEmpleado + '\')">';
                                    controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del empleado"></span>';
                                    controles += '</button>';
                                }
                            }
                            else {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroPersona(\'' + item.IdEmpleado + '\')">';
                                controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del empleado"></span>';
                                controles += '</button>';
                            }

                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }
        else {

            this.$grid = $("#divGrid").kendoGrid({
                toolbar: ["excel", ], //{ template: kendo.template('<div>Esto es una prueba ' + this.$dataSource.total() + ' </div>') }
                excel: {
                    fileName: "Listado de Empleados.xlsx",
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
                    //{
                    //    field: "IdEmpleado",
                    //    title: "ID",
                    //    attributes: { style: "text-align:right;" },
                    //    width: "30px"                    
                    //},
                    {
                        field: "NroDocumento",
                        title: "DNI",
                        attributes: { style: "text-align:center;" },
                        width: "30px"                    
                    },
                    {
                        field: "NombreCompleto",
                        title: "EMPLEADO",
                        width: "200px",
                        aggregates: ["count"],
                        footerTemplate: "Total: #= count#"
                    },
                    {
                        field: "CondicionLaboral",
                        title: "CONDICIÓN",
                        width: "100px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"                    
                    },
                    {
                        field: "Sede",
                        title: "SEDE",
                        width: "100px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "NombreOficina",
                        title: "DEPENDENCIA",
                        width: "300px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        field: "CorreoElectronicoLaboral",
                        title: "EMAIL LABORAL",
                        //attributes: { style: "text-align:center;" },
                        width: "50px"
                    },
                    {
                        field: "NombreCargo",
                        title: "CARGO",
                        width: "300px"
                    },
                    {
                        field: "EstadoNombre",
                        title: "ESTADO",
                        attributes: { style: "text-align:center;" },
                        width: "30px",
                        aggregates: ["count"],
                        groupHeaderTemplate: "#= value # (Total: #= count#)"
                    },
                    {
                        //INGRESAR DETALLE DE LA EVALUACION
                        title: '',
                        attributes: { style: "text-align:center;" },
                        template: function (item) {
                            var controles = "";
                            if ($("#hdPerfil").val() == PERFIL_NOMINA_ABASTECIMIENTO) {
                                if (item.IdCondicion == 5) {
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroPersona(\'' + item.IdEmpleado + '\')">';
                                    controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del empleado"></span>';
                                    controles += '</button>';
                                }                            
                            }
                            else {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalRegistroPersona(\'' + item.IdEmpleado + '\')">';
                                controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del empleado"></span>';
                                controles += '</button>';
                            }
                        
                            return controles;
                        },
                        width: '30px'
                    }
                ]
            }).data();
        }
    };
    
    this.NominaJS.prototype.buscar = function (e) {
        e.preventDefault();

        var grilla = $('#divGrid').data("kendoGrid");
        grilla.dataSource._sort = undefined;
        grilla.dataSource.page(1);

        //$("#lblTotal").html(grilla.dataSource.total());
    };

    this.NominaJS.prototype.abrirModalRegistroPersona = function (id) {
        var modal = $('#divModalRegistroPersona').data('kendoWindow');
        
        LimpiarModalRegistroPersona();

        if (id == 0) {           
            $("#ddlPersonaEstado").data("kendoDropDownList").value('1');
            $("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('01');
            //$("#divCese").hide();
            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
            $('#_tab4').unbind();
            $("#_tab4").removeAttr("data-toggle");
            $('#_tab4').click(function () {
                controladorApp.notificarMensajeDeAlerta('Primero debe guardar los datos del trabajador');
            })

            $("#btnGuardarContacto").hide();

            $('#tab2').removeClass('in active');
            $('#tab3').removeClass('in active');
            $('#tab4').removeClass('in active');
            $('#tab1').addClass('in active');
            $('#liTab2').removeClass('active');
            $('#liTab3').removeClass('active');
            $('#liTab4').removeClass('active');
            $('#liTab1').addClass('active');
            
            if ($("#hdPerfil").val() == PERFIL_NOMINA_CONTACTO) {
                $('#txtPersonaCorreoElectronicoP').prop('readonly', true);
                $('#txtTelefonoCelularP').prop('readonly', true);
                $('#txtTelefonoFijoP').prop('readonly', true);
                $('#txtPersonaCorreoElectronico').prop('readonly', false);
                $('#txtTelefonoCelular').prop('readonly', false);
                $('#txtTelefonoLaboral').prop('readonly', false);
                $('#txtTelefonoAnexo').prop('readonly', false);
            }
            else {
                $('#txtPersonaCorreoElectronicoP').prop('readonly', false);
                $('#txtTelefonoCelularP').prop('readonly', false);
                $('#txtTelefonoFijoP').prop('readonly', false);
                $('#txtPersonaCorreoElectronico').prop('readonly', true);
                $('#txtTelefonoCelular').prop('readonly', true);
                $('#txtTelefonoLaboral').prop('readonly', true);
                $('#txtTelefonoAnexo').prop('readonly', true);
            }

            controlador.CargarFormularioCuentasBancarias(-1);
            controlador.CargarFormularioOrdenesServicio(-1);
            if ($("#ddlPersonaCondicion").data("kendoDropDownList").value() == '5'){
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

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Guardar");
            if ($("#btnGuardarAbastecimiento") != null) $("#btnGuardarAbastecimiento").text("Guardar");
            if ($("#btnGuardarContacto") != null) $("#btnGuardarContacto").text("Guardar");

            modal.title("Agregar Trabajador");
            modal.open();
        }
        else {
            $("#hdIdEmpleado").val(id);

            $("#_tab4").attr("data-toggle", "tab");
            $('#_tab4').prop("onclick", null).off("click");

            controlador.CargarFormularioTrabajador(id);
            controlador.CargarFormularioCuentasBancarias(id);
            controlador.CargarFormularioOrdenesServicio(id);

            if ($("#btnGuardarAdmin") != null) $("#btnGuardarAdmin").text("Actualizar");
            if ($("#btnGuardarAbastecimiento") != null) $("#btnGuardarAbastecimiento").text("Actualizar");
            if ($("#btnGuardarContacto") != null) $("#btnGuardarContacto").text("Actualizar");

            modal.title("Actualizar Trabajador");
            modal.open();
        }
    }

    this.NominaJS.prototype.cerrarModalRegistroPersona = function () {
        var modal = $('#divModalRegistroPersona').data('kendoWindow');
        modal.close();
    }

    this.NominaJS.prototype.CargarFormularioTrabajador = function (id) {
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
            $('#ddlPersonaEstado').data("kendoDropDownList").readonly();
            $('#ddlPersonaCondicion').data("kendoDropDownList").readonly();
            $('#ddlPersonaSede').data("kendoDropDownList").readonly();
            $('#txtPersonaFechaNacimiento').data("kendoDatePicker").readonly();
            $('#txtPersonaFechaInicioLabores').data("kendoDatePicker").readonly();
            $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly();
            $('#ddlPersonaDependencia').data("kendoDropDownList").readonly();
            $('#txtPersonaCargo').prop('readonly', true);
            $('#txtRUC').prop('readonly', true);

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

                if (res.Estado == 1)
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").readonly(); //$("#divCese").hide();
                else
                    $('#txtPersonaFechaFinLabores').data("kendoDatePicker").enable(true);  //$("#divCese").show();

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
                $("#txtPersonaFechaFinLabores").data("kendoDatePicker").value(res.FechaCese);
                
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

    this.NominaJS.prototype.CargarFormularioCuentasBancarias = function (id) {
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

    this.NominaJS.prototype.BancoDropDownEditor = function (container, options) {
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

    this.NominaJS.prototype.EstadoDropDownEditor = function (container, options) {
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

    this.NominaJS.prototype.CargarFormularioOrdenesServicio = function (id) {
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
     

    this.NominaJS.prototype.NombreOrdenEditor = function (container, options) {
        $('<textarea required name="' + options.field + '" style="width: ' + (container.width() + 30) + 'px;height:' + (container.height() + 30) + 'px" />')
            .appendTo(container);
    };
    this.NominaJS.prototype.FechaInicioEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaInicio">')
            .appendTo(container);
    };
    this.NominaJS.prototype.FechaFinEditor = function (container, options) {
        $('<input data-role="datepicker" data-format="dd/MM/yyyy" type="date" name="' + options.field + '" style="width: ' + container.width() + 'px" data-bind="value:FechaFin">')
            .appendTo(container);
    };
    this.NominaJS.prototype.agregarTrabajador = function (e) {
        e.preventDefault();
        var metodo = 'Registrar';
        
        if (frmPersonaValidador.validate()) {
            var modal = $('#divModalRegistroPersona').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdEmpleado").val() != 0) {
                data_param.append('IdEmpleado', $("#hdIdEmpleado").val());
                metodo = 'Guardar';
            }

            data_param.append('TipoDocumento', $("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value());
            data_param.append('NroDocumento', $("#txtPersonaNumeroDeDocumento").val());
            data_param.append('Nombre', $("#txtPersonaNombres").val());
            data_param.append('Paterno', $("#txtPersonaApellidoPaterno").val());
            data_param.append('Materno', $("#txtPersonaApellidoMaterno").val());
            data_param.append('IdGenero', $("#ddlPersonaSexo").data("kendoDropDownList").value());
            data_param.append('IdEstadoCivil', $("#ddlPersonaEstadoCivil").data("kendoDropDownList").value());
            data_param.append('IdGrupoSanguineo', $("#ddlPersonaGrupoSan").data("kendoDropDownList").value());
            data_param.append('DescripcionUbigeo', $("#txtPersonaUbigeo").val());
            data_param.append('Domicilio', $("#txtPersonaDireccionDomicilio").val());
            data_param.append('Foto', $('#hdFoto').val());
            data_param.append('Estado', $("#ddlPersonaEstado").data("kendoDropDownList").value());
            data_param.append('IdCondicion', $("#ddlPersonaCondicion").data("kendoDropDownList").value());
            data_param.append('IdSede', $("#ddlPersonaSede").data("kendoDropDownList").value());
            data_param.append('FechaNacimiento', kendo.toString(kendo.parseDate($("#txtPersonaFechaNacimiento").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('FechaInicio', kendo.toString(kendo.parseDate($("#txtPersonaFechaInicioLabores").data("kendoDatePicker").value()), 'dd/MM/yyyy'));

            if ($("#txtPersonaFechaFinLabores").data("kendoDatePicker").value() != '')
                data_param.append('FechaCese', kendo.toString(kendo.parseDate($("#txtPersonaFechaFinLabores").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            
            data_param.append('IdDependencia', $("#ddlPersonaDependencia").data("kendoDropDownList").value());
            data_param.append('NombreCargo', $("#txtPersonaCargo").val().toUpperCase());
            data_param.append('CorreoElectronico', $("#txtPersonaCorreoElectronicoP").val());
            data_param.append('Celular', $("#txtTelefonoCelularP").val());
            data_param.append('Telefono', $("#txtTelefonoFijoP").val());
            data_param.append('CorreoElectronicoLaboral', $("#txtPersonaCorreoElectronico").val());
            data_param.append('CelularLaboral', $("#txtTelefonoCelular").val());
            data_param.append('TelefonoLaboral', $("#txtTelefonoLaboral").val());
            data_param.append('AnexoLaboral', $("#txtTelefonoAnexo").val());
            data_param.append('DescripcionUbigeo', $("#txtPersonaUbigeo").val());
            data_param.append('RUC', $("#txtRUC").val());
            data_param.append('Ubigeo', $("#hdIdUbigeoEmpleado").val());

            debugger;
            var nacimiento = kendo.toString(kendo.parseDate($("#txtPersonaFechaNacimiento").data("kendoDatePicker").value()), 'dd/MM/yyyy');
            var inicio = kendo.toString(kendo.parseDate($("#txtPersonaFechaInicioLabores").data("kendoDatePicker").value()), 'dd/MM/yyyy');
            var fin = kendo.toString(kendo.parseDate($("#txtPersonaFechaFinLabores").data("kendoDatePicker").value()), 'dd/MM/yyyy');

            if (nacimiento == null) {
                controladorApp.notificarMensajeDeAlerta('La fecha de nacimiento no es válida');
                $("#txtPersonaFechaNacimiento").focus();
                return;
            }
            if (inicio == null) {
                controladorApp.notificarMensajeDeAlerta('La fecha de inicio de labores no es válida');
                $("#txtPersonaFechaInicioLabores").focus();
                return;
            }
            if (inicio != null && fin != null) {
                if (Date(inicio) > Date(fin)) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de cese de labores debe ser mayor que la fecha de inicio de labores');
                    $("#txtPersonaFechaFinLabores").focus();
                    return;
                }
            }
            
            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de agregar el trabajador?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Empleado/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Trabajador registrado correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                $("#hdIdEmpleado").val(res.responseText);

                                $("#_tab4").attr("data-toggle", "tab");
                                $('#_tab4').prop("onclick", null).off("click");

                                controlador.CargarFormularioTrabajador(res.responseText);
                                controlador.CargarFormularioCuentasBancarias(res.responseText);
                                controlador.CargarFormularioOrdenesServicio(res.responseText);

                                modal.title("Actualizar Trabajador");
                                //modal.close();
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

    this.NominaJS.prototype.actualizarTrabajadorContacto = function (e) {
        e.preventDefault();
        
        if (frmPersonaValidador.validate()) {
            var modal = $('#divModalRegistroPersona').data('kendoWindow');
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
                                    LimpiarModalRegistroPersona();

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

    this.NominaJS.prototype.cerrarModalEdicion = function () {
        $('#divModalNuevo').data('kendoWindow').close();
    }

    this.NominaJS.prototype.ingresarDetalleEvaluacion = function (idCad, idEvaluacion) {
        //window.location = controladorApp.obtenerRutaBase() + 'PropuestaDetalle/?IdCad=' + idCad + "&IdPropuesta=" + idPropuesta + "&IdVersion=" + idVersion;
        window.open(controladorApp.obtenerRutaBase() + 'EvaluacionDetalle/?IdCad=' + idCad + "&IdEvaluacion=" + idEvaluacion, '_blank');
    }

    this.NominaJS.prototype.abrirModalEliminacion = function (uid) {
        $('#hdnUid').val(uid);
        var modal = $('#divModalEliminacion').data('kendoWindow');
        var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid(uid);
        modal.title("Confirmar eliminación");

        var data_param = new FormData();
        data_param.append('IdCad', dr.IdCad);
        data_param.append('IdPropuesta', dr.IdPropuesta);
        
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Propuesta/ConfirmarEliminacion',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                if (res.permite)
                    $('#hEliminacion').html('¿Está seguro de eliminar la propuesta seleccionada?')
                else
                    $('#hEliminacion').html('La propuesta seleccionada no puede ser eliminada')

                $("#pMensaje").html(res.listaMensaje[0]);
                $("#btnEliminar").prop('disabled', !res.permite);
                modal.open();
            },
            error: function (res) {

            }
        });
    }

    this.NominaJS.prototype.cerrarModalEliminacion = function () {
        var modal = $('#divModalEliminacion').data('kendoWindow');
        $('#hdnUid').val('');
        modal.close();
    }

    this.NominaJS.prototype.eliminar = function () {
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