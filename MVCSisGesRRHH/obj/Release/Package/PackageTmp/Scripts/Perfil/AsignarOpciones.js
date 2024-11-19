function CambiarUrl(IdPerfil, ids) {

    var IdPerilOpcion = ids.split("_")[1]
    var IdOpcion = ids.split("_")[0]

    var atributo = $("#btnPermisos").attr("data-url");
    var atributonuevo = globalRutaServidor + "Perfil/VerPermisos?Id_Perfil=" + IdPerfil + "&Id_PerfilOpcion=" + IdPerilOpcion;

    $("#btnPermisos").attr("data-url", atributonuevo);
}

//Boton Cerrar
$(document).ready(function () {
    $("#btnCerrarModal").on("click", function () {
        document.location = globalRutaServidor + "Perfil/Index/";
    });

    $("#btnGrabar").on("click", function () {

        //var valor = ("#__id").val;
        var IdPerfil = $("#__id").val();
        var idaplicacion = $("#__idaplicacion").val();
        var opciones = $("#__opciones").val();

        jqMensaje("Confirmar", "¿Desea Grabar el perfil - opcion?", function (result) {
            if (result) {
                GrabarPerfilOpcion(IdPerfil, idaplicacion, opciones);
            }
        });
    });
});
/*****************************************************/
//Grabar Perfil Opcion
function GrabarPerfilOpcion(IdPerfil, IdAplicacion, opciones) {
    $.getJSON(globalRutaServidor + "Perfil/GrabarPerfilOpcion",
    {
        id_perfil: IdPerfil,
        id_aplicacion: IdAplicacion,
        opciones: opciones
    }, function (data) {
        window.location = globalRutaServidor + "Perfil/Index";
    });
}
/*****************************************************/
function checkedNodeIds(nodes, checkedNodes) {
    for (var i = 0; i < nodes.length; i++) {

        if (nodes[i].checked) {
            checkedNodes.push(nodes[i].id);
        }
        if (nodes[i].hasChildren) {
            checkedNodeIds(nodes[i].children.view(), checkedNodes);
        }
    }
}
/*****************************************************/
$(document).ready(function () {
    CargarTreeView();


    $("#btnGrabarOperacion").click(function () {
        var codigo = "";
        $('input[name="chkSeleccione"]:checked').each(function () {
            codigo += $(this).val() + "|";
        });
        debugger;
        var IdOpcion = $("#__opciones").val();
        var _idPerfil = $("#__id").val();

        jqMensaje("Confirmar acción", "¿Desear grabar la información seleccionada?", function (rest) {
            if (rest) {
                $.getJSON(globalRutaServidor + "Perfil/GrabarPerfilOperacion",
                    { codigos: codigo, idOpcion: IdOpcion, idPerfil: _idPerfil },function (json) {
                    if (json.rpta) {

                        $("#frmListaTreeViewOperacion").submit();
                    }
                });
            }

        });

    });
});


function CargarTreeView() {
    var IdAplicacion = $("#__idaplicacion").val();
    var IdPerfil = $("#__id").val();

    var data = new kendo.data.HierarchicalDataSource({
        transport: {
            read: {
                url: globalRutaServidor + "Perfil/ListarTree",
                data: { 'IdPerfil': IdPerfil, 'IdAplicacion': IdAplicacion },
                dataType: 'json'
            }
        },
        schema: {
            model: {
                id: 'id',
                hasChildren: 'hasChildren',
                children: 'items',
                fields: {
                    text: 'text'
                }
            }
        }
    });

    var treeview = $("#treeview").kendoTreeView({
        dataSource: data,
        checkboxes: {
            checkChildren: true
        },
        select: function (e) {
            var id = $("#treeview").getKendoTreeView().dataItem(e.node).id
            ;
            var IdPerfilOpcion = id.split("_")[1];
            var IdOpcion = id.split("_")[0];
            $("#__opciones").val(IdOpcion);
            var _idPerfil = $("#__id").val();

            if (IdPerfilOpcion != "0") {
                CambiarUrl(IdPerfil, id);
                $("#btnPermisos").click();
            }


            ///cargando grilla de operaciones

            var url = $("#frmListaTreeViewOperacion")[0].action;
            var posicion = url.indexOf("=", 0);
            url = url.substring(0, posicion);
            $("#frmListaTreeViewOperacion")[0].action = url + "=" + _idPerfil.toString() + "&idOpcion=" + IdOpcion;
            $("#frmListaTreeViewOperacion").submit();
            //refrescarfrmListaPermisos();

            //var atributoEditar = globalRutaServidor + "Opcion/EditarOperacion?idOpcion=0" + id;
            //$("#btnBaseOperaciones").attr("data-url", atributoEditar);



        },
        messages: {
            loading: "Cargando..."
        }
    });

    var tree = $("#treeview").data("kendoTreeView");
    var checkedNodes = [];
    var message = "";
    // show checked node IDs on datasource change
    $("#treeview").data("kendoTreeView").dataSource.bind("change", function (e) {
        checkedNodes = [];

        if (e.field == "checked") {
            treeView = $("#treeview").data("kendoTreeView"),

            checkedNodeIds(treeView.dataSource.view(), checkedNodes);

            if (checkedNodes.length > 0) {
                message = "" + checkedNodes.join(",");
            } else {
                message = "";
            }

            $("#__opciones").val(message);
        }
    });

    window.setTimeout(function () {
        $("#treeview").find("input[type=checkbox]:checked").each(
            function () {
                var e = tree.dataItem(this);

                checkedNodes.push(e.id);
            }
        );

        $("#__opciones").val(checkedNodes.join(","));
    }, 500);
}