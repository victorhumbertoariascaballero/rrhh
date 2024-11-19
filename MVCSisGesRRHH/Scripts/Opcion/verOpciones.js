$(document).ready(function () {

    $("#contenedorMenu").attr("display", "inherit");
    CargarTreeView();
    //
});

function EliminarOpcionPermiso(idOpcionPermiso) {
    jqMensaje("Confirmar", "¿Está seguro anular el elemento seleccionado?", function (data) {
        if (data) {
            $.getJSON(globalRutaServidor + 'Opcion/EliminarOpcionPermiso', { IdOpcionPermiso: idOpcionPermiso }, function (rest) {
                refrescarfrmListaPermisos();
            });
        }
    });

}

function refrescarfrmListaPermisos() {
    $("#frmListaPermisos").submit();
}

function RefrescarTreeView() {
    $("#frmListaTreeView").submit();
    CargarTreeView();
}

function NuevaOpcion() {
    CambiarUrl(true);
    $("#btnEditarOpcion").click();
};

function CambiarUrl(EsNuevo) {
    var id = $("#IdOpcion").val();

    if (!EsNuevo) {
        atributoEditar = globalRutaServidor + "Opcion/Editar?IdOpcion=" + id + "&IdGrupoOpcion=0&IdAplicacion=" + $("#Id_aplicacion").val();
    }
    else {
        atributoEditar = globalRutaServidor + "Opcion/Editar?IdOpcion=0&IdGrupoOpcion=" + id + "&IdAplicacion=" + $("#Id_aplicacion").val();
    }

    $("#btnEditarOpcion").attr("data-url", atributoEditar);
};

function CambiarUrlPermiso() {

    var IdOpcion = $("#IdOpcion").val();
    var atributo = $("#btnPermisos").attr("data-urlorigen");
    var atributonuevo = atributo + "?Id_Opcion=" + IdOpcion;
    $("#btnPermisos").attr("data-url", atributonuevo);
}

function EditarOpcion() {
    CambiarUrl(false);
    $("#btnEditarOpcion").click();
};

function NuevaOpcion() {
    CambiarUrl(true);
    $("#btnEditarOpcion").click();
};

function EliminarOpcion() {
    var Id = $("#IdOpcion").val();

    //jqMensaje("Confirmar", "¿Está seguro de eliminar la Opción? " +Id, function (result) {
    jqMensaje("Confirmar", "¿Está seguro de eliminar la Opción?", function (result) {
            if (result) {
                $.getJSON(globalRutaServidor + "Opcion/EliminarOpcionPermiso", {IdOpcionPermiso: Id
                }, function (data) {
                    if (data.rpta)
                        window.location.reload(true);
                    else
                        bootbox.alert(data.errores);
                    //jqMensaje("Advertencia", data.errores);
                    ////$("form:last").submit();
                    //if (data == "0") {
                    //    $("form:last").submit();
                    // }
                    // else {
                    //      alert(data.split("|")[1]); 
                    // }
                });
            }
            });
    //RefrescarTreeView();
};

function VerPermisos() {

};

function MoverOpcion(IdSource, IdTarget, Psc) {

    $.getJSON(globalRutaServidor + "Opcion/MoverOpcionGrupo",
        {
            Id_Opcion_Source: IdSource,
            Id_Opcion_Target: IdTarget,
            Posicion: Psc
        }, function (data) {

            if (!data.rpta) {
                $("#divError").removeAttr("style");
                $("#ulListaError").empty();
                $.each(data.errores, function (key, value) {
                    if (value != null) {
                        $("#ulListaError").append("<li>" + value + "</li>");
                    }
                });
            }
            else {
                $("form:last").submit();
            }
        });
}

function ondata() {

    var tree = $("#treeview").data("kendoTreeView");
    window.setTimeout(function () {
        $("#treeview").find(".k-item").each(
            function () {
                var e = tree.dataItem(this);
            });

        $('#context-menu').kendoContextMenu({
            target: ".k-item",
            select: function (e) {
                var sel = $(e.item).attr('id');

                if (sel == "modalNuevaOpcion") {
                    NuevaOpcion();
                }
                else if (sel == "modalEditarOpcion") {
                    EditarOpcion();
                }
                else if (sel == "modalEliminarOpcion") {
                    EliminarOpcion();
                } else if (sel == "modalPermisoOpcion") {
                    VerPermisos();
                }
                else {
                    return false;
                }
            },
            alignToAnchor: true,
        });
    }, 0);
}

function onDrop(e) {

    var IdSource = $("#treeview").getKendoTreeView().dataItem(e.sourceNode).id;

    if ($("#treeview").getKendoTreeView().dataItem(e.dropTarget) == null) {
        return;
    }

    var IdTarget = $("#treeview").getKendoTreeView().dataItem(e.dropTarget).id;

    if (IdSource == IdTarget) {
        return;
    }

    var r = confirm("¿Está seguro de mover la opcion?");
    if (!r) {
        e.setValid();
        return;
    }

    MoverOpcion(IdSource, IdTarget, e.dropPosition)

}

function CargarTreeView() {

    var IdAplicacion = $("#Id_aplicacion").val();
    var datasource = new kendo.data.HierarchicalDataSource({
        transport: {
            read: {
                url: globalRutaServidor + "Opcion/ListarTree",
                data: { 'IdAplicacion': IdAplicacion },
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
        dataSource: datasource,
        dragAndDrop: true,
        drop: onDrop,
        dataBound: ondata,
        select: function (e) {

            var id = $("#treeview").getKendoTreeView().dataItem(e.node).id;
            $("#IdOpcion").val(id);
            var url = $("#frmListaTreeViewOperacion")[0].action;
            var posicion = url.indexOf("=", 0);
            url = url.substring(0, posicion);
            $("#frmListaTreeViewOperacion")[0].action = url + "=" + id.toString();
            $("#frmListaTreeViewOperacion").submit();
            //refrescarfrmListaPermisos();

            var atributoEditar = globalRutaServidor + "Opcion/EditarOperacion?idOpcion=0" + id;
            $("#btnBaseOperaciones").attr("data-url", atributoEditar);
        },
        messages: {
            loading: "Cargando..."
        }
    });
}

/*

function CambiarUrl(EsNuevo) {
    var id = $("#IdOpcion").val();

    if (!EsNuevo) {
        atributoEditar = globalRutaServidor + "Opcion/Editar?IdOpcion=" + id + "&IdGrupoOpcion=0&IdAplicacion=" + $("#Id_Aplicacion").val();
    }
    else {
        atributoEditar = globalRutaServidor + "Opcion/Editar?IdOpcion=0&IdGrupoOpcion=" + id + "&IdAplicacion=" + $("#Id_Aplicacion").val();
    }

    $("#btnEditarOpcion").attr("data-url", atributoEditar);
};

*/