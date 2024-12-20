﻿var documentName_ = null;
//
window.addEventListener('getArguments', function (e) {
    type = e.detail;
    //alert('tipo ' + type);
    if (type === 'W') {
        ObtieneArgumentosParaFirmaDesdeLaWeb(); // Llama a getArguments al terminar.
    } else if (type === 'L') {
        MiFuncionCancel();// Llama a getArguments al terminar.
    }
});
function sendArguments(arg) {
    //arg = document.getElementById("argumentos").value;
    //alert("Enviando: " + arg);
    dispatchEventClient('sendArguments', arg);
}

window.addEventListener('invokerOk', function (e) {
    //alert('detalle ' + e.detail);
    type = e.detail;
    if (type === 'W') {
        MiFuncionOkWeb();
    } else if (type === 'L') {
        MiFuncionCancel();
    }
});

window.addEventListener('invokerCancel', function (e) {
    //alert('cancel ' + e.detail);
    MiFuncionCancel();
});

//::LÓGICA DEL PROGRAMADOR::

function ObtieneArgumentosParaFirmaDesdeLaWeb() {
    //document.getElementById("signedDocument").href = "#";

    //var NroSolicitud = sessionStorage.getItem("NumeroSolicitud");
    //$.get(URL + "Boletas/getArguments", { id: NroSolicitud }, function (data, status) {
    //    documentName_ = data;
    //    //Obtiene argumentos
    //alert(URL);
    //alert("http://app_desarrollo.midis.gob.pe" + URL + "Boletas/postArgumentsLotePDF")
    $.post(URL + "Boletas/postArgumentsLotePDF", {
            type: "W",
            documentName: document.getElementById('hdArchivoFirmado').value
        }, function (data, status) {
            //alert("Web: " + data + "\nStatus: " + status);
            //document.getElementById("argumentos").value = data;
            sendArguments(data);
        });

    //});
}

function MiFuncionOkWeb() {
    alert("Documento firmado desde una URL correctamente.");
    location.reload();
    //document.getElementById("signedDocument").href = "controller/getFile.php?documentName=" + documentName_;
}

function MiFuncionCancel() {
    alert("El proceso de firma digital fue cancelado.");
    //document.getElementById("signedDocument").href = "#";
}