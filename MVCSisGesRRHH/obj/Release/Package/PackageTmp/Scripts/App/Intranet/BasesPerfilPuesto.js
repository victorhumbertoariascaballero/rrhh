(function ($) {
    var frmRegBasesPerfil;
    var frmLiberarBase;
    //var frmRegistroPerfilMision;
    //var frmRegistroPerfilExperiencia;
    //var frmRegistroNivelBasico;
    //var frmRegistroNivelEducativo;
    //var frmRegistroMaestria;
    //var frmRegistroDoctorado;
    //var frmPerfilesValidador;
    var CodOrgano;
    var CodDependencia;
    var CodOrganoBand;
    var CodDependenciaBand;
    var today = new Date();

    this.BasesPerfilPuestoJS = function () { };

    ////////////////////////////VER//////////////////////////////////
    this.BasesPerfilPuestoJS.prototype.inicializarVer = function () {
        //debugger;
        //var todayMax = today.setDate(today.getDate() + 60);
        //var todayMin = today.setDate(today.getDate() - 75);

        //$("#txtAprobacionConvocatoria").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtPubConvSNE_MTPE_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtPubConvSNE_MTPE_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtPubConvMIDIS_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtPubConvMIDIS_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaInscripcionCV").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEvaluacionCV_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEvaluacionCV_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubResultadosCV").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaConocimiento").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubConocimiento").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEntrevista_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEntrevista_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubResultadoFinal").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaSuscripcionContrato_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaSuscripcionContrato_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });

        $("#ddlEvalConocimientos").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Bases/ListarTipoEvaluacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
            change: function (e) {
                var estado = this.value();

                if (estado == '0') {
                    $('#txtFechaConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaConocimiento").removeAttr("required");
                    $("#txtFechaConocimiento").val('');

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaPubConocimiento").removeAttr("required");
                    $("#txtFechaPubConocimiento").val('');
                }
                if (estado == '1') {
                    $('#txtFechaConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaConocimiento").attr("required", true);
                    $("#txtFechaConocimiento").attr("validationmessage", "requerido");

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaPubConocimiento").attr("required", true);
                    $("#txtFechaPubConocimiento").attr("validationmessage", "requerido");
                }
            }
        });
        $("#ddlEvalPsicologico").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Bases/ListarTipoEvaluacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        if ($("#hdIdCodBasePerfil").val() != "") {
            var id = $("#hdIdCodBasePerfil").val();
            this.CargarFormularioBasesPerfilPuestoVer(id);
        }

        frmRegBasesPerfil = $("#frmRegistroABasesPerfil").kendoValidator().data("kendoValidator");

        $("#ddlEvalConocimientos").data("kendoDropDownList").value(0);
        $("#ddlEvalPsicologico").data("kendoDropDownList").value(0);
        $('#ddlEvalConocimientos').data("kendoDropDownList").readonly();
        $('#ddlEvalPsicologico').data("kendoDropDownList").readonly();

        //$('#txtFechaConocimiento').data("kendoDatePicker").readonly();
        //$('#txtFechaPubConocimiento').data("kendoDatePicker").readonly();
    }

    this.BasesPerfilPuestoJS.prototype.CargarFormularioBasesPerfilPuestoVer = function (_id) {
        var data_param = new FormData();

        data_param.append('id', _id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Bases/ObtenerBasesPerfilesPuestoPorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;

                $("#txtCantPersonalRequerido").val(res[0].iCantPersonalRequerido);
                //$("#txtCantDuracionContrato").val(res[0].strDuracionContrato);
                if (res[0].bDuracionContrato31Diciembre == true) {
                    $("#chkCantDuracionContrato").prop("checked", true);
                    $("#txtCantDuracionContrato").prop('readonly', true);
                    $("#txtCantDuracionContrato").val("");
                }
                else {
                    $("#txtCantDuracionContrato").val(res[0].strDuracionContrato);
                    $("#txtCantDuracionContrato").prop('readonly', false);
                }
                if (res[0].IdExamenConocimiento == 1) {
                    $("#ddlEvalConocimientos").data("kendoDropDownList").value(1);
                    $('#txtFechaConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaConocimiento").attr("required", true);
                    $("#txtFechaConocimiento").attr("validationmessage", "requerido");

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaPubConocimiento").attr("required", true);
                    $("#txtFechaPubConocimiento").attr("validationmessage", "requerido");
                }
                else {
                    $("#ddlEvalConocimientos").data("kendoDropDownList").value(0);
                    $('#txtFechaConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaConocimiento").removeAttr("required");
                    $("#txtFechaConocimiento").val('');

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaPubConocimiento").removeAttr("required");
                    $("#txtFechaPubConocimiento").val('');
                }
                
                $("#txtRemuneracion").val(res[0].decRemuneracion);
                $("#hdIdPerfilPuesto").val(res[0].iCodPerfil);
                $("#txtPerfilPuesto").val(res[0].strNombrePuesto);
                //$("#txtAprobacionConvocatoria").data("kendoDatePicker").value(res[0].dFechaAprobConv);
                //$("#txtPubConvSNE_MTPE_Desde").data("kendoDatePicker").value(res[0].dFechaDesdePubSNE_MTPE);
                //$("#txtPubConvSNE_MTPE_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaPubSNE_MTPE);
                $("#txtPubConvMIDIS_Desde").data("kendoDatePicker").value(res[0].dFechaDesdePubMIDIS);
                $("#txtPubConvMIDIS_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaPubMIDIS);
                $("#txtFechaInscripcionCV").data("kendoDatePicker").value(res[0].dFechaRegCVPostulante);
                $("#txtFechaEvaluacionCV_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeEvaCV);
                $("#txtFechaEvaluacionCV_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaEvaCV);
                $("#txtFechaPubResultadosCV").data("kendoDatePicker").value(res[0].dFechaPubResultadoMIDIS);
                $("#txtFechaEntrevista_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeEntrevista);
                $("#txtFechaEntrevista_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaEntrevista);
                $("#txtFechaPubResultadoFinal").data("kendoDatePicker").value(res[0].dFechaPubResultadoFinalMIDIS);
                $("#txtFechaSuscripcionContrato_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeSuscripcionContrato);
                $("#txtFechaSuscripcionContrato_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaSuscripcionContrato);

                if (res[0].IdExamenConocimiento == 1) {
                    $("#txtFechaConocimiento").data("kendoDatePicker").value(res[0].dFechaConocimiento);
                    $("#txtFechaPubConocimiento").data("kendoDatePicker").value(res[0].dFechaPubConocimiento);
                }   
            },
            error: function (res) {
                debugger;
            }
        });
    }

   
    ////////////////////////////NUEVO//////////////////////////////////
    
    this.BasesPerfilPuestoJS.prototype.inicializarNuevo = function () {
        //debugger;
        //var todayMax = today.setDate(today.getDate() + 60);
        //var todayMin = today.setDate(today.getDate() - 75);

        //$("#txtAprobacionConvocatoria").kendoDatePicker({ format: "dd/MM/yyyy" }); //, max: new Date(todayMax), min: new Date(todayMin) });
        //$("#txtPubConvSNE_MTPE_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        //$("#txtPubConvSNE_MTPE_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });

        $("#txtPubConvMIDIS_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtPubConvMIDIS_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaInscripcionCV").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEvaluacionCV_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEvaluacionCV_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubResultadosCV").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaConocimiento").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubConocimiento").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEntrevista_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaEntrevista_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaPubResultadoFinal").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaSuscripcionContrato_Desde").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaSuscripcionContrato_Hasta").kendoDatePicker({ format: "dd/MM/yyyy" });

        $("#ddlEvalConocimientos").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Bases/ListarTipoEvaluacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            },
            change: function (e) {
                var estado = this.value();
                
                if (estado == '0') {
                    $('#txtFechaConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaConocimiento").removeAttr("required");
                    $("#txtFechaConocimiento").val('');

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").readonly();
                    $("#txtFechaPubConocimiento").removeAttr("required");
                    $("#txtFechaPubConocimiento").val('');
                }
                if (estado == '1') {
                    $('#txtFechaConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaConocimiento").attr("required", true);
                    $("#txtFechaConocimiento").attr("validationmessage", "requerido");

                    $('#txtFechaPubConocimiento').data("kendoDatePicker").enable(true);
                    $("#txtFechaPubConocimiento").attr("required", true);
                    $("#txtFechaPubConocimiento").attr("validationmessage", "requerido");
                }
            }
        });
        $("#ddlEvalPsicologico").kendoDropDownList({
            autoBind: true,
            dataTextField: "Nombre",
            dataValueField: "Codigo",
            dataSource: {
                transport: {
                    read: {
                        url: controladorApp.obtenerRutaBase() + "Bases/ListarTipoEvaluacion",
                        type: "GET",
                        dataType: "json",
                        cache: false
                    }
                }
            }
        });

        if ($("#hdIdCodBasePerfil").val() != "")
        {
            var id = $("#hdIdCodBasePerfil").val();
            this.CargarFormularioBasesPerfilPuesto(id);
        }
        frmRegBasesPerfil = $("#frmRegistroABasesPerfil").kendoValidator().data("kendoValidator");

        $("#ddlEvalConocimientos").data("kendoDropDownList").value(0);
        $("#ddlEvalPsicologico").data("kendoDropDownList").value(0);
        //$('#ddlEvalConocimientos').data("kendoDropDownList").readonly();
        $('#ddlEvalPsicologico').data("kendoDropDownList").readonly();

        $('#txtFechaConocimiento').data("kendoDatePicker").readonly();
        $('#txtFechaPubConocimiento').data("kendoDatePicker").readonly();
    }
    
    this.BasesPerfilPuestoJS.prototype.CargarFormularioBasesPerfilPuesto = function (_id) {
        var data_param = new FormData();

        data_param.append('id', _id);

        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Bases/ObtenerBasesPerfilesPuestoPorID',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                debugger;                

                $("#txtCantPersonalRequerido").val(res[0].iCantPersonalRequerido);
                //$("#txtCantDuracionContrato").val(res[0].strDuracionContrato);
                if (res[0].bDuracionContrato31Diciembre == true) {
                    $("#chkCantDuracionContrato").prop("checked", true);
                    $("#txtCantDuracionContrato").prop('readonly', true);
                    $("#txtCantDuracionContrato").val("");
                }
                else {
                    $("#txtCantDuracionContrato").val(res[0].strDuracionContrato);
                    $("#txtCantDuracionContrato").prop('readonly', false);
                }
                $("#txtRemuneracion").val(res[0].decRemuneracion);
                $("#hdIdPerfilPuesto").val(res[0].iCodPerfil);
                $("#txtPerfilPuesto").val(res[0].strNombrePuesto);                
                //$("#txtAprobacionConvocatoria").data("kendoDatePicker").value(res[0].dFechaAprobConv);
                //$("#txtPubConvSNE_MTPE_Desde").data("kendoDatePicker").value(res[0].dFechaDesdePubSNE_MTPE);
                //$("#txtPubConvSNE_MTPE_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaPubSNE_MTPE);
                $("#txtPubConvMIDIS_Desde").data("kendoDatePicker").value(res[0].dFechaDesdePubMIDIS);
                $("#txtPubConvMIDIS_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaPubMIDIS);
                $("#txtFechaInscripcionCV").data("kendoDatePicker").value(res[0].dFechaRegCVPostulante);
                $("#txtFechaEvaluacionCV_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeEvaCV);
                $("#txtFechaEvaluacionCV_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaEvaCV);
                $("#txtFechaPubResultadosCV").data("kendoDatePicker").value(res[0].dFechaPubResultadoMIDIS);
                $("#txtFechaEntrevista_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeEntrevista);
                $("#txtFechaEntrevista_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaEntrevista);
                $("#txtFechaPubResultadoFinal").data("kendoDatePicker").value(res[0].dFechaPubResultadoFinalMIDIS);
                $("#txtFechaSuscripcionContrato_Desde").data("kendoDatePicker").value(res[0].dFechaDesdeSuscripcionContrato);
                $("#txtFechaSuscripcionContrato_Hasta").data("kendoDatePicker").value(res[0].dFechaHastaSuscripcionContrato);
                debugger;                
            },
            error: function (res) {
                debugger;
            }
        });
    }

    this.BasesPerfilPuestoJS.prototype.abrirModalBuscarPerfilPuesto = function (e) {
        e.preventDefault();
        debugger;
        var modal = $('#ModalBuscarPerfilPuesto').data('kendoWindow');

        modal.title("Buscar Perfil de Puesto");            
        controlador.CargarPerfilesPuesto(event);
        modal.open(); //.center();
    }

    this.BasesPerfilPuestoJS.prototype.cerrarModalBuscarPerfilPuesto = function () {
        debugger;
        var modal = $('#ModalBuscarPerfilPuesto').data('kendoWindow');
        modal.close();
    }
    
    this.BasesPerfilPuestoJS.prototype.RegBasesPerfil = function (e) {
        e.preventDefault();
        var metodo = '';
        if ($("#hdIdCodBasePerfil").val() != "")
            metodo = 'ActualizarBasesPerfilPuesto';
        else
            metodo = 'InsertarBasesPerfilPuesto';

        if ($("#hdIdPerfilPuesto").val() == "" || $("#hdIdPerfilPuesto").val() == "0") {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el perfil de puesto');
            return false;
        }

        debugger;
        if (frmRegBasesPerfil.validate()) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();
            var mensaje = '¿Está seguro de registrar las bases del proceso de convocatoria?';

            if ($("#hdIdCodBasePerfil").val() != "") {
                data_param.append('iCodBasePerfil', $("#hdIdCodBasePerfil").val());
                mensaje = '¿Está seguro de actualizar las bases del proceso de convocatoria?';
            }

            data_param.append('iCodPerfil', $("#hdIdPerfilPuesto").val());
            //data_param.iCodPerfil = $("#hdIdPerfilPuesto").val();
            data_param.append('iCantPersonalRequerido', $("#txtCantPersonalRequerido").val());
            data_param.append('strDuracionContrato', $("#txtCantDuracionContrato").val());
            if ($("#chkCantDuracionContrato").is(':checked'))
                data_param.append('bDuracionContrato31Diciembre', true);
            else
                data_param.append('bDuracionContrato31Diciembre', false);

            //if (kendo.parseDate($("#txtAprobacionConvocatoria").data("kendoDatePicker").value()) > kendo.parseDate($("#txtPubConvSNE_MTPE_Desde").data("kendoDatePicker").value())) {
            //    controladorApp.notificarMensajeDeAlerta('La fecha de publicación del proceso no puede ser menor que la fecha de aprobación de convocatoria');
            //    return false;
            //}
            //if (kendo.parseDate($("#txtPubConvSNE_MTPE_Hasta").data("kendoDatePicker").value()) > kendo.parseDate($("#txtPubConvMIDIS_Desde").data("kendoDatePicker").value())) {
            //    controladorApp.notificarMensajeDeAlerta('La fecha de publicación de convocatoria en MIDIS no puede ser menor que la fecha de publicación del proceso');
            //    return false;
            //}
            if (kendo.parseDate($("#txtPubConvMIDIS_Hasta").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaInscripcionCV").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de publicación de convocatoria en MIDIS no puede ser menor que la fecha de registro del postulante');
                return false;
            }
            if (kendo.parseDate($("#txtFechaInscripcionCV").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaEvaluacionCV_Desde").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de registro del postulante no puede ser menor que la fecha de evaluación de hoja de vida');
                return false;
            }
            if (kendo.parseDate($("#txtFechaEvaluacionCV_Hasta").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaPubResultadosCV").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de publicación de resultados de hoja de vida no puede ser menor que la fecha de evaluación de hoja de vida');
                return false;
            }
            if ($("#ddlEvalConocimientos").data("kendoDropDownList").value() == '1') {
                if (kendo.parseDate($("#txtFechaPubResultadosCV").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaConocimiento").data("kendoDatePicker").value())) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de evaluación de conocimientos no puede ser menor que la fecha de publicación de resultados de hoja de vida');
                    return false;
                }
                if (kendo.parseDate($("#txtFechaConocimiento").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaPubConocimiento").data("kendoDatePicker").value())) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de publicación de resultados de conocimientos no puede ser menor que la fecha de evaluación de conocimientos');
                    return false;
                }
                if (kendo.parseDate($("#txtFechaPubConocimiento").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaEntrevista_Desde").data("kendoDatePicker").value())) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de entrevistas no puede ser menor que la fecha de publicación de resultados de conocimientos');
                    return false;
                }
            }
            else {
                if (kendo.parseDate($("#txtFechaPubResultadosCV").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaEntrevista_Desde").data("kendoDatePicker").value())) {
                    controladorApp.notificarMensajeDeAlerta('La fecha de entrevistas no puede ser menor que la fecha de publicación de resultados de hoja de vida');
                    return false;
                }
            }
            if (kendo.parseDate($("#txtFechaEntrevista_Hasta").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaPubResultadoFinal").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de publicación de resultados finales no puede ser menor que la fecha de entrevistas');
                return false;
            }
            if (kendo.parseDate($("#txtFechaPubResultadoFinal").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaSuscripcionContrato_Desde").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de suscripción de contrato no puede ser menor que la fecha de publicación de resultados finales');
                return false;
            }

            //if (kendo.parseDate($("#txtPubConvSNE_MTPE_Desde").data("kendoDatePicker").value()) > kendo.parseDate($("#txtPubConvSNE_MTPE_Hasta").data("kendoDatePicker").value())) {
            //    controladorApp.notificarMensajeDeAlerta('La fecha de inicio de publicación del proceso no puede ser mayor que la fecha de fin');
            //    return false;
            //}
            if (kendo.parseDate($("#txtPubConvMIDIS_Desde").data("kendoDatePicker").value()) > kendo.parseDate($("#txtPubConvMIDIS_Hasta").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de inicio de publicación en MIDIS no puede ser mayor que la fecha de fin');
                return false;
            }
            if (kendo.parseDate($("#txtFechaEvaluacionCV_Desde").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaEvaluacionCV_Hasta").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de inicio de evaluación curricular no puede ser mayor que la fecha de fin');
                return false;
            }
            if (kendo.parseDate($("#txtFechaEntrevista_Desde").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaEntrevista_Hasta").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de inicio de entrevistas no puede ser mayor que la fecha de fin');
                return false;
            }
            if (kendo.parseDate($("#txtFechaSuscripcionContrato_Desde").data("kendoDatePicker").value()) > kendo.parseDate($("#txtFechaSuscripcionContrato_Hasta").data("kendoDatePicker").value())) {
                controladorApp.notificarMensajeDeAlerta('La fecha de inicio de suscripción de contrato no puede ser mayor que la fecha de fin');
                return false;
            }

            


            data_param.append('bDuracionContrato31Diciembre', $("#chkCantDuracionContrato").val());
            data_param.append('decRemuneracion', $("#txtRemuneracion").val());
            //data_param.append('dFechaAprobConv', kendo.toString(kendo.parseDate($("#txtAprobacionConvocatoria").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            //data_param.append('dFechaDesdePubSNE_MTPE', kendo.toString(kendo.parseDate($("#txtPubConvSNE_MTPE_Desde").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            //data_param.append('dFechaHastaPubSNE_MTPE', kendo.toString(kendo.parseDate($("#txtPubConvSNE_MTPE_Hasta").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaDesdePubMIDIS', kendo.toString(kendo.parseDate($("#txtPubConvMIDIS_Desde").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaHastaPubMIDIS', kendo.toString(kendo.parseDate($("#txtPubConvMIDIS_Hasta").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaRegCVPostulante', kendo.toString(kendo.parseDate($("#txtFechaInscripcionCV").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaDesdeEvaCV', kendo.toString(kendo.parseDate($("#txtFechaEvaluacionCV_Desde").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaHastaEvaCV', kendo.toString(kendo.parseDate($("#txtFechaEvaluacionCV_Hasta").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaPubResultadoMIDIS', kendo.toString(kendo.parseDate($("#txtFechaPubResultadosCV").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaDesdeEntrevista', kendo.toString(kendo.parseDate($("#txtFechaEntrevista_Desde").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaHastaEntrevista', kendo.toString(kendo.parseDate($("#txtFechaEntrevista_Hasta").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaPubResultadoFinalMIDIS', kendo.toString(kendo.parseDate($("#txtFechaPubResultadoFinal").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaDesdeSuscripcionContrato', kendo.toString(kendo.parseDate($("#txtFechaSuscripcionContrato_Desde").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            data_param.append('dFechaHastaSuscripcionContrato', kendo.toString(kendo.parseDate($("#txtFechaSuscripcionContrato_Hasta").data("kendoDatePicker").value()), 'dd/MM/yyyy'));

            data_param.append('IdExamenConocimiento', $("#ddlEvalConocimientos").data("kendoDropDownList").value());
            if ($("#ddlEvalConocimientos").data("kendoDropDownList").value() == '1') {
                data_param.append('dFechaConocimiento', kendo.toString(kendo.parseDate($("#txtFechaConocimiento").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
                data_param.append('dFechaPubConocimiento', kendo.toString(kendo.parseDate($("#txtFechaPubConocimiento").data("kendoDatePicker").value()), 'dd/MM/yyyy'));
            }   

            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                mensaje, 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Bases/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Bases registrada correctamente");

                                // REFRESCAR INFORMACION DEL TRABAJADOR
                                $("#hdIdPerfilPuesto").val(res.responseText);
                                //controlador.Actualizar($("#hdIdPerfilPuesto").val());
                                window.location.assign("index");
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

    this.BasesPerfilPuestoJS.prototype.CargarPerfilesPuesto = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Bases/ListarPerfilPuesto',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};
                    if ($operation === "read") {
                        data_param.strPerfil = $("#txtPerfilPuestoBuscar").val();
                        
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
        this.$grid = $("#divPerfilesPuesto").kendoGrid({
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
                    title: "Envia",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strOrgano",
                    title: "ORGANO",
                    width: "150px"
                },
                {
                    field: "strUnidadOrganica",
                    title: "UNIDAD ORGÁNICA",
                    width: "150px"
                },
                {
                    field: "strNombrePuesto",
                    title: "NOMBRE DEL PUESTO",
                    width: "150px"
                },                                
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodPerfil, item.strNombrePuesto];

                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.CargarNombrePuesto(\'' + items + '\')">';
                        controles += '<span class="glyphicon glyphicon-plus" aria-hidden="true" data-uib-tooltip="Ver" title="Actualizar información del perfil de puesto"></span>';
                        controles += '</button>';
                        return controles;
                    },
                    width: '30px'
                }
            ]
        }).data();
        debugger;
    };


    $('#ModalBuscarPerfilPuesto').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '80%',
        height: 'auto',
        title: 'Buscar Perfil de Puesto',
        visible: false,
        position: { top: '10%', left: "10%" },
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
    
    $('#divModalFormato').kendoWindow({
        draggable: true,
        modal: true,
        pinned: true,
        resizable: false,
        width: '50%',
        height: 'auto',
        title: 'Actualizar Documento de Bases de Convocatoria',
        visible: false,
        position: { top: '20%', left: "25%" },
        //actions: ["Minimize", "Maximize", "Close"],
        actions: ["Close"],
        close: function () {
            //frmPersonaValidador.hideMessages();

            //$("#hdIdPostulante").val('0');
            //$("#ddlPersonaTipoDeDocumento").data("kendoDropDownList").value('');
            //$("#txtPersonaNumeroDeDocumento").val('');
            //$("#txtPersonaNombres").val('');
            //$("#txtPersonaApellidoPaterno").val('');
            //$("#txtPersonaApellidoMaterno").val('');
            //$("#ddlPersonaSexo").data("kendoDropDownList").value('');
            //$("#txtPersonaDireccionDomicilio").val('');
        }
    }).data("kendoWindow");

    this.BasesPerfilPuestoJS.prototype.CargarNombrePuesto = function (item) {
        debugger;
        var items = new Object();
        if (item != 0) {
            var _item = item.split(',');

            items.iCodPerfil = _item[0];
            items.vNombrePuesto = _item[1];            
        }
        if (items.iCodPerfil != "") {
            $("#hdIdPerfilPuesto").val(items.iCodPerfil);
            $("#txtPerfilPuesto").val(items.vNombrePuesto);
            controlador.cerrarModalBuscarPerfilPuesto();
        }
        
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        

    }

    ////////////////////////////BANDEJA//////////////////////////////////
    
    this.BasesPerfilPuestoJS.prototype.inicializarBandeja = function () {
        
        
        //$("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });        
        //$("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });

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
                        url: controladorApp.obtenerRutaBase() + "Bases/ListarEstadoBases",
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

        this.CargarBandejaPrincipal(event);
    };

    this.BasesPerfilPuestoJS.prototype.CargarBandejaPrincipal = function (e) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Bases/ObtenerBasesPerfilesPuesto',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.strOrgano = $('#ddlOrgano_busqueda').data("kendoDropDownList").value();
                        data_param.strUO = $('#ddlUUOO_busqueda').data("kendoDropDownList").value();
                        data_param.strEstado = $('#ddlEstado_busqueda').data("kendoDropDownList").value();
                        data_param.fechaIni = ''; //$("#txtFechaInicio").data("kendoDatePicker").value();
                        data_param.fechaFin = ''; //$("#txtFechaFin").data("kendoDatePicker").value();
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
                    id: "iCodBasePerfil"
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
        this.$grid = $("#divGridBases").kendoGrid({
            toolbar: ["excel", ],
            excel: {
                fileName: "Listado de Bases de Convocatoria.xlsx",
                filterable: false
            },            
            dataSource: this.$dataSource,
            autoBind: true,
            selectable: true,
            scrollable: false,
            sortable: false,
            pageable: false,
            groupable: false,

            dataType: 'json',
            columns: [
                {
                    field: "iCodBasePerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strNroCAS",
                    title: "NRO DE PROCESO",
                    width: "30px",
                    attributes: { style: "text-align:center;" }
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
                    field: "decRemuneracion",
                    title: "REMUNERACION",
                    width: "100px",
                    attributes: { style: "text-align:right;" },
                    format: "{0:c}"
                },
                {
                    field: "strEstadoAprobado",
                    title: "ESTADO",
                    width: "100px",
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        return (item.strEstadoAprobado == 'PENDIENTE' ? item.strEstadoAprobado
                        : (item.strEstadoAprobado == 'APROBADO' ? "<span style='background-color: RGB(255,255,204); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,235,59);'> " + item.strEstadoAprobado + "</span>"
                        : (item.strEstadoAprobado == 'PUBLICADO' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i>  " + item.strEstadoAprobado + "</span>"
                        : '')));
                    }

                },
                //{
                //    field: "strEstadoCompletado",
                //    title: "ESTADO",
                //    width: "100px",
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        return (item.strEstadoCompletado == 'PENDIENTE' ? item.strEstadoCompletado
                //            : (item.strEstadoCompletado == 'FINALIZADO' ? "<span style='background-color: RGB(255,255,204); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(255,235,59);'> " + item.strEstadoCompletado + "</span>"
                //            : (item.strEstadoCompletado == 'APROBADO' ? "<span style='background-color: RGB(199,249,215); padding: 2px; width: 95%; float: right; border-style: solid; border-width: 1px; border-color: RGB(145,206,191);'> <i class='fa fa-check'></i>  " + item.strEstadoCompletado + "</span>"
                //            : '')));
                //    },
                //},
                {
                    title: " ",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        var controles = "";
                        if (item.strEstadoCompletado == 'PENDIENTE') {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.ActualizarBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información de bases del perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        else {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.VerBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Editar" title="Ver Bases del perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        
                        return controles;
                    }
                },  
                //{
                //    title: " ",
                //    attributes: { style: "text-align:center;" },
                //    width: "30px",
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodBasePerfil, item.iCodPerfil];
                //        controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                //        controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                //        controles += '</button>';
                        
                //        return controles;
                //    }
                //},
                {
                    title: " ",
                    attributes: { style: "text-align:center;" },
                    width: "30px",
                    template: function (item) {
                        var controles = "";
                        if (item.IdTieneArchivo == 1) {
                            controles += '<a href="' + controladorApp.obtenerRutaBase() + 'Bases/DescargarArchivo/?id=' + item.iCodBasePerfil + '" target="_blank" class="btn btn-info btn-xs"><span class="glyphicon glyphicon-download-alt" title="Descargar bases de convocatoria"></span></a>';
                        }
                        else {
                            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.abrirModalFormato(\'' + item.iCodBasePerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Actualizar bases de convocatoria"></span>';
                            controles += '</button>';
                        }
                        
                        return controles;
                    }
                }
                //{
                //    title: " ",
                //    attributes: { style: "text-align:center;" },
                //    width: "30px",
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodBasePerfil, item.iCodPerfil];
                //        if (item.cEstadoPublicacion == 'P') {
                //            controles += '<button type="button" class="btn btn-info btn-xs" onclick="controlador.PublicarBasesPerfilPuesto(\'' + items + '\')">';
                //            controles += '<span class="glyphicon glyphicon-circle-arrow-up" aria-hidden="true" data-uib-tooltip="Publicar" title="Publicar bases perfil de puesto"></span>';
                //            controles += '</button>';
                //        }

                //        return controles;
                //    }
                //}//,
                //{
                //    //INGRESAR DETALLE DE LA EVALUACION
                //    title: '',
                //    attributes: { style: "text-align:center;" },
                //    template: function (item) {
                //        var controles = "";
                //        var items = [item.iCodBasePerfil, item.iCodPerfil];
                //        //controles += '<a href="perfiles/nuevo">';                                          
                //        if (item.cEstadoAprobado == 'P')
                //        {
                //            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.ActualizarBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                //            controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del perfil de puesto"></span>';
                //            controles += '</button>';
                //            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                //            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                //            controles += '</button>';                            
                //        }
                //        else
                //        {
                //            if (item.cEstadoAprobado == 'L') {
                //                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.ActualizarBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                //                controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del perfil de puesto"></span>';
                //                controles += '</button>';                                
                //                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                //                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                //                controles += '</button>';
                //            }
                //            else {
                //                if (item.cEstadoAprobado == 'M') {
                //                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.ActualizarBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                //                    controles += '<span class="glyphicon glyphicon-edit" aria-hidden="true" data-uib-tooltip="Editar" title="Actualizar información del perfil de puesto"></span>';
                //                    controles += '</button>';
                //                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                //                    controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                //                    controles += '</button>';
                //                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                //                    controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                //                    controles += '</button>';
                //                }
                //                else {
                //                    if (item.cEstadoAprobado == 'O') {
                //                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                //                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                //                        controles += '</button>';
                //                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                //                        controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                //                        controles += '</button>';
                //                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                //                        controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                //                        controles += '</button>';
                //                        if (item.cEstadoPublicacion == 'P') {
                //                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.PublicarBasesPerfilPuesto(\'' + items + '\')">';
                //                            controles += '<span class="glyphicon glyphicon-circle-arrow-up" aria-hidden="true" data-uib-tooltip="Publicar" title="Publicar bases perfil de puesto"></span>';
                //                            controles += '</button>';
                //                        }
                //                    }
                //                    else {
                //                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                //                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                //                        controles += '</button>';
                //                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                //                        controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                //                        controles += '</button>';
                //                        if (item.cEstadoPublicacion == 'P') {
                //                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.PublicarBasesPerfilPuesto(\'' + items + '\')">';
                //                            controles += '<span class="glyphicon glyphicon-circle-arrow-up" aria-hidden="true" data-uib-tooltip="Publicar" title="Publicar bases perfil de puesto"></span>';
                //                            controles += '</button>';
                //                        }
                                        
                //                    }                                    
                //                }
                //            }
                            
                //        }
                        
                        
                //        //controles += '</a>';

                //        return controles;
                //    },
                //    width: '30px'
                //}
            ]
        }).data();
        debugger;        
    };

    this.BasesPerfilPuestoJS.prototype.ActualizarBasesPerfilPuesto = function (id) {
        //window.location.assign("Bases/ActualizarBasesPerfilPuesto?id=" + id);
        window.location.href = controladorApp.obtenerRutaBase() + "Bases/ActualizarBasesPerfilPuesto?id=" + id;
    };

    this.BasesPerfilPuestoJS.prototype.GenerarFormatoBases = function () {
        var IdBases = $('#hdIdBasePerfilPuesto').val();

        window.open(controladorApp.obtenerRutaBase() + "Bases/Ficha?idBases=" + IdBases, "_blank");
    }
    this.BasesPerfilPuestoJS.prototype.GenerarPDF = function (item) {

        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        if (item != 0) {
            var _item = item.split(',');

            iCodBasePerfil = _item[0];
            iCodPerfil = _item[1];
        }
        window.open(controladorApp.obtenerRutaBase() + "Bases/PlantillaBasesPerfilPuesto?id=" + iCodBasePerfil + "&id2=" + iCodPerfil, '_blank');
        //window.location.assign("PlantillaBasesPerfilPuesto?id=" + iCodBasePerfil + "&id2=" + iCodPerfil);
    };

    this.BasesPerfilPuestoJS.prototype.PublicarBasesPerfilPuesto = function (item) {
        //e.preventDefault();
        var metodo = 'PlantillaBasesPerfilPuestoConvocatoria';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        if (item != 0) {
            var _item = item.split(',');

            iCodBasePerfil = _item[0];
            iCodPerfil = _item[1];
        }
        if (true) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();

            data_param.append('iCodBasePerfil', iCodBasePerfil);
            data_param.append('iCodTrabajador', $("#hdIdCodTrabajador").val());
            data_param.append('iCodPerfil', iCodPerfil);
            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de publicar las bases del perfil de puesto?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Bases/' + metodo,
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
                                //controlador.GenerarPDFConvocatorias(item);
                                controladorApp.notificarMensajeSatisfactorio("Bases de Perfil de Puesto aprobado correctamente");
                                //controlador.inicializarBandejaUser();                                
                                controlador.CargarBandejaPrincipal(event);
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

    ////////////////////////////BANDEJA JEFE RRHH//////////////////////////////////

    this.BasesPerfilPuestoJS.prototype.inicializarBandejaJefeRRHH = function () {
        debugger;

        $("#txtFechaInicio").kendoDatePicker({ format: "dd/MM/yyyy" });
        $("#txtFechaFin").kendoDatePicker({ format: "dd/MM/yyyy", max: new Date() });
        frmLiberarBase = $("#frmLiberarBase").kendoValidator().data("kendoValidator");
        
    };

    this.BasesPerfilPuestoJS.prototype.CargarBandejaPrincipalJefeRRHH = function (e) {
        e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Bases/ObtenerBasesPerfilesPuesto',
                    type: 'POST',
                    dataType: 'json',
                    cache: false
                },
                parameterMap: function ($options, $operation) {
                    var data_param = {};

                    if ($operation === "read") {
                        //data_param.id = $('#ddlUUOO_busquedaUser').data("kendoDropDownList").value();
                        data_param.fechaIni = $("#txtFechaInicio").data("kendoDatePicker").value();
                        data_param.fechaFin = $("#txtFechaFin").data("kendoDatePicker").value();
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
                    id: "iCodBasePerfil"
                }
            },
            group: {
                field: "strEstadoAprobado", aggregates: [
                   { field: "strEstadoAprobado", aggregate: "count" }
                ]
            },
            aggregate: [
                    { field: "strEstadoAprobado", aggregate: "count" },
                    { field: "strEstadoAprobado", aggregate: "count" }
            ]
        });
        debugger;
        this.$grid = $("#divGridBasesJefeRRHH").kendoGrid({
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
                    field: "iCodBasePerfil",
                    title: "ID",
                    attributes: { style: "text-align:right;" },
                    width: "30px",
                    hidden: true
                },
                {
                    field: "strNroCAS",
                    title: "Nro CAS",
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
                    field: "strUnidadOrganica",
                    title: "Unidad Orgánica",
                    width: "100px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "strEstadoAprobado",
                    title: "Estado Aprobación",
                    width: "100px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    field: "strEstadoPublicacion",
                    title: "Estado Publicación",
                    width: "100px",
                    attributes: { style: "text-align:center;" }

                },
                {
                    //INGRESAR DETALLE DE LA EVALUACION
                    title: '',
                    attributes: { style: "text-align:center;" },
                    template: function (item) {
                        var controles = "";
                        var items = [item.iCodBasePerfil, item.iCodPerfil];
                        //controles += '<a href="perfiles/nuevo">';                                          
                        if (item.cEstadoAprobado == 'P')
                        {
                            //controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.AprobarBasesPerfilPuesto(\'' + item.iCodBasePerfil + '\')">';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.AprobarBasesPerfilPuesto(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Aprobar" title="Aprboar bases del perfil de puesto"></span>';
                            controles += '</button>';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuestoJefeRRHH(\'' + item.iCodBasePerfil + '\')">';
                            controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                            controles += '</button>';
                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                            controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                            controles += '</button>';
                        }
                        else
                        {
                            if (item.cEstadoAprobado == 'L') {
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuestoJefeRRHH(\'' + item.iCodBasePerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                                controles += '</button>';
                                controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                                controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                                controles += '</button>';
                            }
                            else {
                                if (item.cEstadoAprobado == 'M') {
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.AprobarBasesPerfilPuesto(\'' + items + '\')">';
                                    controles += '<span class="glyphicon glyphicon-ok" aria-hidden="true" data-uib-tooltip="Aprobar" title="Aprboar bases del perfil de puesto"></span>';
                                    controles += '</button>';
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuestoJefeRRHH(\'' + item.iCodBasePerfil + '\')">';
                                    controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                                    controles += '</button>';                                    
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                                    controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                                    controles += '</button>';
                                    controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                                    controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                                    controles += '</button>'
                                }
                                else {
                                    if (item.cEstadoAprobado == 'O') {
                                        if (item.cEstadoPublicacion == 'P') {
                                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalLiberarBase(\'' + item.iCodBasePerfil + '\')">';
                                            controles += '<span class="glyphicon glyphicon-open" aria-hidden="true" data-uib-tooltip="Liberar" title="Liberar bases del perfil de puesto"></span>';
                                            controles += '</button>';
                                        }                            
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuestoJefeRRHH(\'' + item.iCodBasePerfil + '\')">';
                                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                                        controles += '</button>';
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                                        controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                                        controles += '</button>';
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalBasesPerfilPuestoObservacion(\'' + item.iCodBasePerfil + '\')">';
                                        controles += '<span class="glyphicon glyphicon-info-sign" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Observaciones"></span>';
                                        controles += '</button>'
                                    }
                                    else {
                                        if (item.cEstadoPublicacion == 'P') {
                                            controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.abrirModalLiberarBase(\'' + item.iCodBasePerfil + '\')">';
                                            controles += '<span class="glyphicon glyphicon-open" aria-hidden="true" data-uib-tooltip="Liberar" title="Liberar bases del perfil de puesto"></span>';
                                            controles += '</button>';
                                        }                                       
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.VerBasesPerfilPuestoJefeRRHH(\'' + item.iCodBasePerfil + '\')">';
                                        controles += '<span class="glyphicon glyphicon-eye-open" aria-hidden="true" data-uib-tooltip="Ver" title="Ver Bases del perfil de puesto"></span>';
                                        controles += '</button>';
                                        controles += '<button type="button" class="btn btn-default btn-xs" onclick="controlador.GenerarPDF(\'' + items + '\')">';
                                        controles += '<span class="glyphicon glyphicon-file" aria-hidden="true" data-uib-tooltip="Descargar" title="Generar PDF bases perfil de puesto"></span>';
                                        controles += '</button>';
                                    }
                                    
                                }
                                
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
    this.BasesPerfilPuestoJS.prototype.abrirModalFormato = function (IdBases) {
        $("#fileFormato").data("kendoUpload").clearAllFiles();
        $('#hdIdBasePerfilPuesto').val(IdBases);

        var modal = $('#divModalFormato').data('kendoWindow');
        //modal.title("Enviar Contrato de Trabajo");

        modal.open().center();
    }

    this.BasesPerfilPuestoJS.prototype.cerrarModalFormato = function () {
        $('#divModalFormato').data('kendoWindow').close();
    }

    this.BasesPerfilPuestoJS.prototype.VerBasesPerfilPuesto = function (id) {
        //window.location.assign("VerBasesPerfilPuesto?id=" + id);
        window.location.href = controladorApp.obtenerRutaBase() + "Bases/VerBasesPerfilPuesto?id=" + id;
    };

    this.BasesPerfilPuestoJS.prototype.agregarBasesArchivo = function (e) {
        e.preventDefault();

        var modal = $('#divModalFormato').data('kendoWindow');
        var esValido = true;
        var mensajeValidacion = '';

        var data_param = new FormData();
        data_param.append('iCodBasePerfil', $('#hdIdBasePerfilPuesto').val());

        var upload1 = $("#fileFormato").getKendoUpload();
        var firmas = upload1.getFiles();
        if (firmas.length == 0) {
            controladorApp.notificarMensajeDeAlerta('Debe seleccionar el archivo de bases revisado y aprobado (Formato PDF)');
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

        controladorApp.abrirMensajeDeConfirmacion('¿Desea actualizar las bases del proceso de convocatoria?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Bases/RegistrarBasesArchivo',
                        type: 'POST',
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        data: arg,
                        success: function (res) {
                            debugger;
                            if (res.success == 'False') {
                                $("#console").append(res.responseText);
                                controladorApp.notificarMensajeDeAlerta("La actualización de las bases no se pudo realizar");
                            }
                            else {
                                controladorApp.notificarMensajeSatisfactorio("Las bases del proceso de convocatoria se actualizó de forma correcta");
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

    this.BasesPerfilPuestoJS.prototype.VerBasesPerfilPuestoJefeRRHH = function (id) {
        window.location.assign("VerBasesPerfilPuestoJefeRRHH?id=" + id);
    };

    this.BasesPerfilPuestoJS.prototype.AprobarBasesPerfilPuesto = function (item) {
        //e.preventDefault();
        var metodo = 'AprobarBasesPerfilPuesto';
        //controladorApp.notificarMensajeDeAlerta('hola');
        //if (frmPerfilesValidador.validate()) {
        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        if (item != 0) {
            var _item = item.split(',');

            iCodBasePerfil = _item[0];
            iCodPerfil = _item[1];
        }
        if (true) {
            var esValido = true;
            var mensajeValidacion = '';
            var data_param = new FormData();

            data_param.append('iCodBasePerfil', iCodBasePerfil);
            //data_param.append('iCodTrabajador', $("#hdIdCodTrabajadorBandUser").val());
            data_param.append('iCodPerfil', iCodPerfil);
            debugger;

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de aprobar las bases del perfil de puesto?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Bases/' + metodo,
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
                                //controlador.GenerarPDFConvocatorias(item);
                                controladorApp.notificarMensajeSatisfactorio("Bases de Perfil de Puesto aprobado correctamente");
                                //controlador.inicializarBandejaUser();                                
                                controlador.CargarBandejaPrincipalJefeRRHH(event);
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
    
    this.BasesPerfilPuestoJS.prototype.GenerarPDFConvocatorias = function (item) {

        var iCodBasePerfil = 0;
        var iCodPerfil = 0;
        if (item != 0) {
            var _item = item.split(',');

            iCodBasePerfil = _item[0];
            iCodPerfil = _item[1];
        }
        var data_param = new FormData();
        data_param.append('iCodBasePerfil', iCodBasePerfil);
        data_param.append('iCodPerfil', iCodPerfil);
        $.ajax({
            url: controladorApp.obtenerRutaBase() + 'Bases/PlantillaBasesPerfilPuestoConvocatoria',
            type: 'POST',
            dataType: 'json',
            contentType: false,
            processData: false,
            data: data_param,
            success: function (res) {
                controladorApp.notificarMensajeSatisfactorio("Bases de Perfil de Puesto aprobado correctamente");
                //controlador.inicializarBandejaUser();                                
                controlador.CargarBandejaPrincipalJefeRRHH(event);

                
                debugger;
            },
            error: function (res) {
                debugger;
            }
        });


        //window.location.assign("PlantillaBasesPerfilPuestoConvocatoria?id=" + iCodBasePerfil + "&id2=" + iCodPerfil);
    };

    this.BasesPerfilPuestoJS.prototype.abrirModalLiberarBase = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalLiberarBase').data('kendoWindow');
        $("#txtObservacion").val('');
        //LimpiarModalRegistroPersona();

        if (id != 0) {

            $("#hdIdBasePerfilPuesto").val(id);
            modal.title("Liberar Base");

            modal.open();
        }
    };

    this.BasesPerfilPuestoJS.prototype.cerrarModalLiberarBase = function (e) {
        debugger;
        e.preventDefault();
        var modal = $('#ModalLiberarBase').data('kendoWindow');
        modal.close();
    };

    $('#ModalLiberarBase').kendoWindow({
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

    this.BasesPerfilPuestoJS.prototype.LiberarBases = function (e) {
        e.preventDefault();
        var metodo = 'LiberarBases';

        if (frmLiberarBase.validate()) {
            var modal = $('#ModalLiberarBase').data('kendoWindow');
            var esValido = true;
            var mensajeValidacion = '';

            //var dr = $("#divGrid").data("kendoGrid").dataSource.getByUid($('#hdnUid').val());
            var data_param = new FormData();
            if ($("#hdIdBasePerfilPuesto").val() != 0) {
                //$("#hdIdPerfilPuestoJefe").val(id);
                data_param.append('iCodBasePerfil', $("#hdIdBasePerfilPuesto").val());
                //metodo = 'Guardar';
            }
            
            data_param.append('strObservacion', $("#txtObservacion").val());

            controladorApp.abrirMensajeDeConfirmacion(
                '¿Está seguro de realiar la operación?', 'SI', 'NO'
                , function (arg) {
                    $.ajax({
                        url: controladorApp.obtenerRutaBase() + 'Bases/' + metodo,
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
                                controladorApp.notificarMensajeSatisfactorio("Operación registrada correctamente");

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

    this.BasesPerfilPuestoJS.prototype.CargarBasesPerfilPuestoObservacion = function (id) {
        //e.preventDefault();
        this.$dataSource = [];
        this.$dataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            batch: false,
            transport: {
                read: {
                    url: controladorApp.obtenerRutaBase() + 'Bases/ListarBasesPerfilPuestoObservacion',
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
        this.$grid = $("#divGridObservaciones").kendoGrid({
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
                    width: "10px"
                },                
                {
                    field: "strObservacion",
                    title: "Observacion",
                    attributes: { style: "text-align:center;" },
                    width: "70px"

                },
                {
                    field: "strFechaAprobacionAnterior",
                    title: "Fecha Última Aprobación",
                    width: "10px",
                    attributes: { style: "text-align:center;" }
                },
            ]
        }).data();
        debugger;
    };

    this.BasesPerfilPuestoJS.prototype.abrirModalBasesPerfilPuestoObservacion = function (id) {
        debugger;
        //var items = new Object();
        //if (item != 0) {
        //    var _item = item.split(',');

        //    items.iCodPerfil = _item[0];
        //    items.iCodTrabajador = _item[1];                  
        //}        


        debugger;
        //console.log(items);

        var modal = $('#ModalBasesPerfilPuestoObservacion').data('kendoWindow');

        //LimpiarModalRegistroPersona();

        if (id != 0) {

            //$("#hdIdPerfilPuestoJefe").val(id);
            modal.title("Listado de Observaciones");

            modal.open();
            controlador.CargarBasesPerfilPuestoObservacion(id);
        }
    }

    this.BasesPerfilPuestoJS.prototype.cerrarModalBasesPerfilPuestoObservacion = function () {
        debugger;
        var modal = $('#ModalBasesPerfilPuestoObservacion').data('kendoWindow');
        modal.close();
    }

    $('#ModalBasesPerfilPuestoObservacion').kendoWindow({
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

}(jQuery));