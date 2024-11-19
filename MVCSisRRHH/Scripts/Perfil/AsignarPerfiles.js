var IdAplicacion = 0;
function fSeleccionar(Id) {
    $("#Aplicaciones table tbody tr").removeClass("fila_seleccionada");
    IdAplicacion = Id;

    $("#tr" + Id).addClass("fila_seleccionada");

    CargarTreeView();
}

$(document).ready(function () {
    $("#btnCerrarModal").on("click", function () {
        document.location = globalRutaServidor + "Usuario/Index/";
    });

    $("#btnGrabar").on("click", function () {

        //var valor = ("#__id").val;
        var IdUsuario = $("#__id").val();
        var Opciones = $("#__perfiles").val();
        //jqMensaje("Confirmar", "¿Desea Grabar el usuario perfil?", function (result) {
        jqMensaje("Confirmar", "¿Desea Grabar el usuario perfil?", function (result) {
            if (result) {
                GrabarUsuarioPerfil(IdUsuario, IdAplicacion, Opciones);
            }
        });
    });
});
/*****************************************************/
function CambiarUrl(IdUsuario, ids) {
    var IdUsuarioOpcion = ids.split("_")[1]
    var IdOpcion = ids.split("_")[0]

    var atributo = $("#btnPermisos").attr("data-url");
    var atributonuevo = atributo + "?Id_Usuario=" + IdUsuario + "&Id_UsuarioOpcion=" + IdUsuarioOpcion;
    $("#btnPermisos").attr("data-url", atributonuevo);
}
/*****************************************************/
//Grabar Perfil Opcion
function GrabarUsuarioPerfil(idusuario, idaplicacion, opciones) {
    $.getJSON(globalRutaServidor + "Perfil/GrabarUsuarioPerfil",
    {
        Id_Usuario: idusuario,
        Id_Aplicacion: idaplicacion,
        Perfil: opciones
    }, function (data) {
        if (data.resp == "1") {
            bootbox.alert("Datos grabados correctamente")
        }
        else {
            bootbox.alert("No se pudo grabar. Errores encontrado " + data.errores)
        }
        //alert("Datos grabados correctamente");
        
        //window.location = globalRutaServidor + "Perfil/AsignarPerfiles?IdUsuario=" + idusuario + "&IdAplicacion=0";
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
function CargarTreeView() {
    //var IdAplicacion = $("#ddlAplicacion").val();
    var IdUsuario = $("#__id").val();

    var data = new kendo.data.HierarchicalDataSource({
        transport: {
            read: {
                url: globalRutaServidor + "Perfil/ListarPerfilTree",
                data: { 'IdUsuario': IdUsuario, 'IdAplicacion': IdAplicacion },
                dataType: 'json',
                cache: false
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

            var IdUsuarioOpcion = id.split("_")[1]
            var IdOpcion = id.split("_")[0]

            if (IdUsuarioOpcion != "0") {
                CambiarUrl(IdUsuario, id);
                $("#btnPermisos").click();
            }

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

            $("#__perfiles").val(message);
        }
    });

    window.setTimeout(function () {
        $("#treeview").find("input[type=checkbox]:checked").each(
            function () {
                var e = tree.dataItem(this);

                checkedNodes.push(e.id);
            }
        );

        $("#__perfiles").val(checkedNodes.join(","));
    }, 500);
}