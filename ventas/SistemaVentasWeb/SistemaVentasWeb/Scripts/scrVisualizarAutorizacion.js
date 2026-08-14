$.fx.speeds._default = 600;
$(function () {
    $("#divLoader2").hide();
    $("#divLoader2").bind({
        ajaxStart: function () { $(this).show(); },
        ajaxStop: function () { $(this).hide(); }
    });
});
$(document).ready(function () {
    CargarCuadroAutorizacion();
    document.getElementById("divLoader").style.display = "none";
});
function CargarCuadroAutorizacion() {
    $('#divAutorizacion').html("");
    var tblModal = '<table width="100%" border="1px"><tr class="trTituloSeccion"><td colspan="4">AUTORIZACIONES</td></tr><tr class="trTituloCabecera"><td>Autorizador</td><td>Cargo</td><td>Tipo</td><td>BackUp</td></tr>';
    var strDatos = ObtenerAutorizacionEspecial("credito");
    var arrDatos = strDatos.split("</tr>");
    tblModal += arrDatos[0];
    var porCredito = arrDatos[1];
    tblModal += "</table>";
    $('#divAutorizacion').html(tblModal);
}
function ObtenerAutorizacionEspecial(strTipo) {
    var strOperador = parent.document.getElementById("hdnUser").value;
    var strRetorno = "";
    $.ajax({
        type: "POST",
        url: "/Services/srvAutorizacion.asmx/ObtenerAutorizacionEspecial",
        dataType: "xml",
        data: { "strOperador": strOperador, "strTipo": strTipo },
        async: false,
        success: function (data) {
            strRetorno = $(data).find("string").text();
        }
    });
    return strRetorno;
}
function CargarPedido() {
    document.getElementById("btnCargarPedido").style.visibility = "hidden";
    RegistrarOrdenVentaEnBorradorVenta();
}
function RegistrarOrdenVentaEnBorradorVenta() {
    var intUsuario = parseInt(parent.document.getElementById("hdnUsuario").value);
    var intDocEntryOV = parseInt(document.getElementById("hdnDocEntryOV").value);
    var intDocNumOV = parseInt(document.getElementById("hdnDocNumOV").value);
    MostrarProgreso2();
    $.ajax({
        type: "post",
        async: true,
        contentType: "application/json; charset=utf-8",
        url: "http://wssap_test.agroinsumos.cl/Services/srvOrdenVenta.asmx/RegistrarOrdenVentaEnBorradorVenta",
        dataType: "json",
        data: "{'intUsuario':'" + intUsuario + "','intDocEntry':'" + intDocEntryOV + "'}",
        dataFilter: function (data) { return data; },
        success: function (xml) {
            OcultarProgreso2();
            var arrDatos = (xml.d).split("|");
            var intStatus = parseInt(arrDatos[0]);
            var intDocEntry = parseInt(arrDatos[1]);
            var intDocNum = parseInt(arrDatos[2]);
            var strStatusMsg = arrDatos[3];
            if (intStatus == 0) {
                $("#divCrearPedido").html("Se ha generado el borrador de venta N° " + intDocEntry);
                if (intDocEntry != 0) {
                    EnviarAutorizacionCorreoCredito(intDocEntry);
                    CancelarPedido(intDocNumOV,intUsuario);
                    $("#divCrearPedido").html("Se ha generado el borrador de venta N° " + intDocEntry + " y se ha(n) enviado correo(s) por autorizacion(es).");
                }
            }
            else {
                $("#divCrearPedido").html("Error: " + intStatus + " - " + strStatusMsg + ".");
                document.getElementById("btnCargarPedido").style.visibility = "visible";
            }
        }
    });
}
function EnviarAutorizacionCorreoCredito(intDocEntry) {
    var strOperador = parent.document.getElementById("hdnUser").value;
    MostrarProgreso2();
    $.ajax({
        type: "post",
        async: true,
        url: "/Services/srvAutorizacion.asmx/EnviarAutorizacionCorreoCredito",
        dataType: "xml",
        data: { "strOperador": strOperador, "intDocEntry": intDocEntry },
        success: function (xml) {
            OcultarProgreso2();
            var strRespuesta = $(xml).find('string').text();
        }
    });
}
function CancelarPedido(intDocCodigo, intUsuario) {
    $.ajax({
        type: "post",
        async: false,
        url: "/Services/srvMonitorDocumento.asmx/CancelarOrdenVenta",
        dataType: "xml",
        data: { "intDocCodigo": intDocCodigo, "intUsuario": intUsuario },
        success: function (xml) {
            var strRespuesta = $(xml).find('string').text();
            var arrDatos = strRespuesta.split("|");
            var intDocNum = arrDatos[0];
            var intStatus = parseInt(arrDatos[1]);
            var strStatus = arrDatos[2];
            if (intStatus === 0) {
                $("#divCrearPedido").html("Se ha cancelado la orden de venta N° " + intDocNum + " para dar origen a un nuevo borrador de venta.");
            } else {
                $("#divCrearPedido").html("Hubo un error al cancelar la orden de venta N° " + intDocNum + ", "+ strStatus + ".");
            }
        }
    });
}
function MostrarProgreso() {
    document.getElementById("divLoader").style.display = "block";
}
function OcultarProgreso() {
    document.getElementById("divLoader").style.display = "none";
}
function MostrarProgreso2() {
    $(document).ajaxStart(function () {
        $("#divLoader2").show();
    });
}
function OcultarProgreso2() {
    $(document).ajaxStop(function () {
        $("#divLoader2").hide();
    });
}