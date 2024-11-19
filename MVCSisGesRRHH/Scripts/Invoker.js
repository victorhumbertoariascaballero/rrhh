var documentName_ = null;
//
window.addEventListener('getArguments', function (e) {
    type = e.detail;
    if (type === 'W') {
        ObtieneArgumentosParaFirmaDesdeLaWeb(); // Llama a getArguments al terminar.
    }
    if (type === 'E') {
        ObtieneArgumentosParaFirmaEntrevista(); // Llama a getArguments al terminar.
    }
    else if (type === 'L') {
        MiFuncionCancel();// Llama a getArguments al terminar.
    }
});
function sendArguments(arg) {
    //arg = document.getElementById("argumentos").value;
    //alert("Enviando: " + arg);
    dispatchEventClient('sendArguments', arg);
}

window.addEventListener('invokerOk', function (e) {
    //alert(e.detail);
    type = e.detail;
    if (type === 'W') {
        MiFuncionOkWeb();
    }
    if (type === 'E') {
        MiFuncionEntrevistaOk();
    } else if (type === 'L') {
        MiFuncionCancel();
    }
});

window.addEventListener('invokerCancel', function (e) {
    //alert(e.detail);
    MiFuncionCancel();
});

//::LÓGICA DEL PROGRAMADOR::

function ObtieneArgumentosParaFirmaDesdeLaWeb() {
    //document.getElementById("signedDocument").href = "#";

    //var NroSolicitud = sessionStorage.getItem("NumeroSolicitud");
    //$.get(URL + "Boletas/getArguments", { id: NroSolicitud }, function (data, status) {
    //    documentName_ = data;
    //    //Obtiene argumentos
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
function ObtieneArgumentosParaFirmaEntrevista() {
    $.post(URL + "Convocatoria/postArgumentsEntrevistaPDF", {
        type: "W",
        IdEvaluacion: document.getElementById('hdIdEvaluacion').value,
        IdTrabajador: document.getElementById('hdIdTrabajador').value,
        IdExamen: document.getElementById('hdIdExamen').value,
        IdPresento: document.getElementById('hdIdPresento').value
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
function MiFuncionEntrevistaOk() {
    alert("Formato de entrevista firmado correctamente.");
    location.reload();
    //document.getElementById("signedDocument").href = "controller/getFile.php?documentName=" + documentName_;
}

function MiFuncionCancel() {
    alert("El proceso de firma digital fue cancelado.");
    //document.getElementById("signedDocument").href = "#";
}